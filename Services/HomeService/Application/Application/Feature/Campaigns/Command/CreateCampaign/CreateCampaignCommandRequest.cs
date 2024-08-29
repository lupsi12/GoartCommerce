
using MediatR;
using System.Collections.Generic;
using System.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.Feature.Users.Commands.CreateUser
{
    public class CreateCampaignCommandRequest : IRequest<CreateCampaignResponse>
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int CampaignId { get; set; }
        [DefaultValue("Default User Name")]
        public string Name { get; set; }
        [DefaultValue("Default description")]
        public string Description { get; set; }
    }
}
