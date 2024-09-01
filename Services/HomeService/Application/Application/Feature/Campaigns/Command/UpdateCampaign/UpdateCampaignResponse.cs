using System;
using System.Collections.Generic;

namespace Application.Feature.Campaigns.Command.UpdateCampaign
{
    public class UpdateCampaignResponse
    {
        public string CampaignId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedDate { get; set; } 
    }
}
