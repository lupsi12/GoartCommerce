using Core.Repositories;
using Domain.Context;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class DomainLayerExtension
    {
        public static void AddDomainLayerServices(this IServiceCollection services)
        {
            services.AddTransient<IReadRepository<Product>, ReadRepository<Product>>();
            services.AddTransient<IWriteRepository<Product>, WriteRepository<Product>>();
            services.AddScoped<IDbContext, ApplicationDbContextAdapter>(); 
        }
    }
}
