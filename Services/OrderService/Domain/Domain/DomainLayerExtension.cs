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
            services.AddTransient<IReadRepository<Order>, ReadRepository<Order>>();
            services.AddTransient<IWriteRepository<Order>, WriteRepository<Order>>();
            services.AddScoped<IDbContext, ApplicationDbContextAdapter>(); 
        }
    }
}
