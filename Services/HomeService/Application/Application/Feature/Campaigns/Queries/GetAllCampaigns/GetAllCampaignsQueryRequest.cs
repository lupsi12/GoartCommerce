using MediatR;
using System.Collections.Generic;
using System.ComponentModel;

namespace Application.Feature.Campaigns.Queries.GetAllCampaigns
{
    public class GetAllCampaignsQueryRequest : IRequest<List<GetAllCampaignsResponse>>
    {
        [DefaultValue(1)]
        public int PageNumber { get; set; } = 1;

        [DefaultValue(10)]
        public int PageSize { get; set; } = 10;
    }
}
