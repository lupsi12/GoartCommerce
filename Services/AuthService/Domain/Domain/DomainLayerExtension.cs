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
            services.AddTransient<IReadRepository<User>, ReadRepository<User>>();
            services.AddTransient<IWriteRepository<User>, WriteRepository<User>>();
            services.AddScoped<IDbContext, ApplicationDbContextAdapter>(); 
        }
    }
}
