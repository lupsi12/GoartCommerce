using System.Globalization;
using System.Reflection;
using Application.Feature.Rules;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationLayerExtension
    {
        public static void AddApplicationLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(assembly);

            services.AddTransient<CampaignRules>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Core.CQRS.Behaviors.FluentValidationBehevior<,>));

            services.AddValidatorsFromAssembly(assembly);

            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");


        }
    }
}
