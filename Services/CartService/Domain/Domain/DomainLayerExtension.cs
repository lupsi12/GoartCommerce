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

            services.AddTransient<IReadRepository<Cart>, ReadRepository<Cart>>();
            services.AddTransient<IWriteRepository<Cart>, WriteRepository<Cart>>();

            services.AddTransient<IReadRepository<CartDetail>, ReadRepository<CartDetail>>();
            services.AddTransient<IWriteRepository<CartDetail>, WriteRepository<CartDetail>>();

            services.AddScoped<IDbContext, ApplicationDbContextAdapter>();
        }
    }
}
