using Application.Features.Carts.Rules;
using Application.MassTransit.GetActiveCartByUserId;
using Application.MassTransit.UpdateCartStatus;
using Core.CQRS.Behaviors;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Reflection;

namespace Application
{
    public static class ApplicationLayerExtension
    {
        public static void AddApplicationLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();

            // MediatR, FluentValidation ve diğer servisleri kaydetme
            services.AddMediatR(assembly);
            services.AddTransient<CartRules>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehevior<,>));
            services.AddValidatorsFromAssembly(assembly);

            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");

            // ProductApiClient'ı kaydetme
            services.AddHttpClient<ProductApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["ProductService:BaseUrl"]);
            });

            // MassTransit konfigürasyonu
            services.AddMassTransit(x =>
            {
                // Consumers'ları ekleme
                x.AddConsumer<GetActiveCartByUserIdConsumer>();
                x.AddConsumer<UpdateCartStatusConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq://localhost");

                    // GetActiveCartByUserIdConsumer için endpoint tanımı
                    cfg.ReceiveEndpoint("get-active-cart-queue", e =>
                    {
                        e.ConfigureConsumer<GetActiveCartByUserIdConsumer>(context);
                    });

                    // UpdateCartStatusConsumer için endpoint tanımı
                    cfg.ReceiveEndpoint("update-cart-status-queue", e =>
                    {
                        e.ConfigureConsumer<UpdateCartStatusConsumer>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();
        }
    }
}