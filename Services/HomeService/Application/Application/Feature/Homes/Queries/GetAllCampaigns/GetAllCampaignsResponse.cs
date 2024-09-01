
using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace Application.Feature.Homes.Queries.GetAllCampaigns
{
    public class GetAllCampaignsResponse
    {
        public string CampaignId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}