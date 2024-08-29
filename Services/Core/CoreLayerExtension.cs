using Core.CQRS.Bases;
using Core.CQRS.Behaviors;
using Core.Middlewares;
using Core.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.MongoRepositories;

namespace Core
{
    public static class CoreLayerExtension
    {
        public static void AddCoreLayerServices(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();


            object value = services.AddRulesFromAssemblyContaining(assembly, typeof(BaseRules));

            services.AddMediatR(assembly);


            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehevior<,>));

            //   app.UseMiddleware<ExceptionHandlingMiddleware>();
            services.AddTransient(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddTransient(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            services.AddTransient(typeof(IMongoReadRepository<>), typeof(MongoReadRepository<>));
            services.AddTransient(typeof(IMongoWriteRepository<>), typeof(MongoWriteRepository<>));


        }

        private static IServiceCollection AddRulesFromAssemblyContaining(
          this IServiceCollection services,
          Assembly assembly,
          Type type)
        {
            var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
            foreach (var item in types)
                services.AddTransient(item);

            return services;
        }
    }
}
