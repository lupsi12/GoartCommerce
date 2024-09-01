using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Feature.Campaigns.Queries.GetCampaignById;
using MongoDB.Bson;

namespace Application.Feature.Campaigns.Queries.GetCampaignById
{
    public class GetCampaignByIdQueryRequest : IRequest<GetCampaignByIdResponse>
    {
        public string CampaignId { get; set; }
    }
}
