
using MediatR;
using System.Collections.Generic;
using System.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.Feature.Homes.Command.CreateCampaign
{
    public class CreateCampaignCommandRequest : IRequest<CreateCampaignResponse>
    {
        public ObjectId MongoId { get; set; }  = ObjectId.GenerateNewId();
        
        [DefaultValue("Default User Name")]
        public string Name { get; set; }
        [DefaultValue("Default description")]
        public string Description { get; set; }
    }
}
