using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.Feature.Campaigns.Command.CreateCampaign
{
    public class CreateCampaignResponse
    {
        public ObjectId Id { get; set; }
       // public int CampaignId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CampaignProduct> CampaignProducts { get; set; }
    }
}
