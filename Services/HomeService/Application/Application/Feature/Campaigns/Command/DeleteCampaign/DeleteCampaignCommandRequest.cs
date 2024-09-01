using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Feature.Campaigns.Command.DeleteCampaign;

namespace Application.Feature.Campaigns.Command.DeleteCampaign
{
    public class DeleteCampaignCommandRequest : IRequest<DeleteCampaignResponse>
    {
        public string CampaignId { get; set; }
    }
}
