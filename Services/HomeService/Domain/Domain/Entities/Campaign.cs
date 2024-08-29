using System;
using System.Collections.Generic;
using Core.Shared.EntityBase;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class Campaign : EntityBase
    {
        // [BsonId]
        // [BsonRepresentation(BsonType.ObjectId)]
        // public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CampaignProduct> CampaignProducts { get; set; } = new List<CampaignProduct>();
    }
}