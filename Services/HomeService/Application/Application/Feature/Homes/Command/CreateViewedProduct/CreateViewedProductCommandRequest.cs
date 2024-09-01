
using MediatR;
using System.Collections.Generic;
using System.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.Feature.Homes.Command.CreateViewedProduct
{
    public class CreateViewedProductCommandRequest : IRequest<CreateViewedProductResponse>
    {
        //[DefaultValue("")]
        public ObjectId MongoId { get; set; }  = ObjectId.GenerateNewId();
        
        [DefaultValue(1)]
        public int ProductId { get; set; }
        [DefaultValue(1)]
        public int UserId { get; set; }
    }
}
