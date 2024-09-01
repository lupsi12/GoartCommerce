using MediatR;
using System.Collections.Generic;
using System.ComponentModel;
using Application.Feature.Campaigns.Command.UpdateCampaign;

namespace Application.Feature.campaigns.Command.UpdateCampaign
{
    public class UpdateCampaignCommandRequest : IRequest<UpdateCampaignResponse>
    {
        [DefaultValue("66d13d3c82460d742d179e3b")]
        public string CampaignId { get; set; }
        
        [DefaultValue("Updated Description")]
        public string Description { get; set; } = "Updated Description";

    }
}
