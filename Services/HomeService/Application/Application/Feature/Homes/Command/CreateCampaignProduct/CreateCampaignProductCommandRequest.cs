
using MediatR;
using System.Collections.Generic;
using System.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.Feature.Homes.Command.CreateCampaignProduct
{
    public class CreateCampaignProductCommandRequest : IRequest<CreateCampaignProductResponse>
    {
        //[DefaultValue("")]
        public ObjectId MongoId { get; set; }  = ObjectId.GenerateNewId();
        
        [DefaultValue(1)]
        public int ProductId { get; set; }
    }
}
