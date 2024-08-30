using System;
using Domain.Entities;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Domain.Configuration
{
    public class CampaignConfiguration
    { public static void RegisterClassMaps()
    {
        BsonClassMap.RegisterClassMap<Campaign>(cm =>
        {
            cm.AutoMap();
            cm.SetIdMember(cm.GetMemberMap(c => c.Id)); // Özelleştirilmiş ID ayarı
            // Burada diğer özelleştirmeleri de yapabilirsiniz
        });
    }

    }
}