using Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.Feature.Users.Commands.CreateUser
{
    public class CreateCampaignResponse
    {
        public int CampaignId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<CampaignProduct> CampaignProducts { get; set; }
    }
}
