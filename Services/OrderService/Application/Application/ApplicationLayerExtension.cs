using Application.Clients;
using Core.CQRS.Behaviors;
using Core.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Reflection;

namespace Application
{
    public static class ApplicationLayerExtension
    {
        public static void AddApplicationLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Core.CQRS.Behaviors.FluentValidationBehevior<,>));

            services.AddValidatorsFromAssembly(assembly);

            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");

            //cart api client
            services.AddHttpClient<CartApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["CartService:BaseUrl"]);
            });

            //product api client
            services.AddHttpClient<ProductApiClient>(client =>
            {
                client.BaseAddress = new Uri(configuration["ProductService:BaseUrl"]);
            });
        }
    }
}
