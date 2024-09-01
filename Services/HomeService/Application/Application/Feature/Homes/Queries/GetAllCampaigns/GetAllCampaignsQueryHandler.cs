
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.MongoRepositories;

namespace Application.Feature.Homes.Queries.GetAllCampaigns
{
    public class GetAllCampaignsQueryHandler : IRequestHandler<GetAllCampaignsQueryRequest, List<GetAllCampaignsResponse>>
    {
        private readonly IMongoReadRepository<Campaign> _campaignReadRepository;

        public GetAllCampaignsQueryHandler(IMongoReadRepository<Campaign> campaignReadRepository)
        {
            _campaignReadRepository = campaignReadRepository;
        }

        public async Task<List<GetAllCampaignsResponse>> Handle(GetAllCampaignsQueryRequest request, CancellationToken cancellationToken)
        {
            var campaigns = await _campaignReadRepository.GetAllByPagingAsync(
                //enableTracking: false,
                //orderBy: q => q.OrderByDescending(p => p.CreatedDate),
                currentPage: request.PageNumber,
                pageSize: request.PageSize);

            return campaigns.Select(campaign => new GetAllCampaignsResponse
            {
                CampaignId = campaign.Id.ToString(),
                Name = campaign.Name,
                CreatedDate = campaign.CreatedDate
            }).ToList();
        }
    }
}
