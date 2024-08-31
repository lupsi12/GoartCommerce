using System;
using System.Collections.Generic;
using Core.Shared.EntityBase;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class Campaign : EntityMongoBase
    {
        // // [BsonId]
        // // [BsonRepresentation(BsonType.Int32)]
        // // public int CampaignId { get; set; }
        // // [BsonId]
        // // public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
        // [BsonId]
        // [BsonRepresentation(BsonType.ObjectId)]
        // public string MongoId { get; set; }  // MongoDB'nin ObjectId'si
        // [BsonId]
        // [BsonRepresentation(BsonType.ObjectId)]
        // public ObjectId _id { get; set; }  // MongoDB'nin ObjectId'si
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CampaignProduct> CampaignProducts { get; set; } = new List<CampaignProduct>();
    }
}