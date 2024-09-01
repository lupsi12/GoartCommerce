using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.Feature.Homes.Command.CreateCampaignProduct
{
    public class CreateCampaignProductResponse
    {
        public string Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public int ProductId { get; set; }
    }
}
