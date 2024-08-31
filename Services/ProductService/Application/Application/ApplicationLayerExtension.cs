using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Application.Feature.Products.Queries.GetProductById;
using Application.Features.Products.Rules;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System.Reflection;
using Application.MassTransit.GetProductById;

namespace Application
{
    public static class ApplicationLayerExtension
    {
        public static void AddApplicationLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(assembly);
            services.AddTransient<ProductRules>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Core.CQRS.Behaviors.FluentValidationBehevior<,>));
            services.AddValidatorsFromAssembly(assembly);
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");

            services.AddMassTransit(x =>
            {
                x.AddConsumer<GetProductByIdConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq://localhost", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    cfg.ReceiveEndpoint("product-service-get-product-by-id-queue", e =>
                    {
                        e.ConfigureConsumer<GetProductByIdConsumer>(context);
                    });
                });
            });
            services.AddMassTransitHostedService();
        }
    }
}
