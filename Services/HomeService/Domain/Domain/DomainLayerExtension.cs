using Core.MongoRepositories;
using Domain.Context;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class DomainLayerExtension
    {
        public static void AddDomainLayerServices(this IServiceCollection services)
        {
            services.AddTransient<IMongoReadRepository<Campaign>, MongoReadRepository<Campaign>>();
            services.AddTransient<IMongoWriteRepository<Campaign>, MongoWriteRepository<Campaign>>();
            services.AddScoped<IMongoDbContext, ApplicationDbContextAdapter>(); 
        }
    }
}
