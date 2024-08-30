using Core.MongoRepositories;
using Domain.Context;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;

namespace Domain
{
    public static class DomainLayerExtension
    {
        public static void AddDomainLayerServices(this IServiceCollection services)
        {
           
            BsonClassMap.RegisterClassMap<Campaign>(cm =>
            {
                cm.AutoMap();
                cm.SetIdMember(cm.GetMemberMap(c => c.Id)); // Özelleştirilmiş ID ayarı
                // Diğer özelleştirmeleri buraya ekleyebilirsiniz
            });

            // Diğer sınıflar için class map kayıtlarını burada yapabilirsiniz
        
    
            services.AddTransient<IMongoReadRepository<Campaign>, MongoReadRepository<Campaign>>();
            services.AddTransient<IMongoWriteRepository<Campaign>, MongoWriteRepository<Campaign>>();
            services.AddScoped<IMongoDbContext, ApplicationDbContextAdapter>(); 
        }
    }
}
