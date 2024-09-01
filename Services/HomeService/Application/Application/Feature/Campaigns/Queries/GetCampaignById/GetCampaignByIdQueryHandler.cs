using Core.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.MongoRepositories;

namespace Application.Feature.Campaigns.Queries.GetCampaignById
{
    public class GetCampaignByIdQueryHandler : IRequestHandler<GetCampaignByIdQueryRequest, GetCampaignByIdResponse>
    {
        private readonly IMongoReadRepository<Campaign> _campaignReadRepository;

        public GetCampaignByIdQueryHandler(IMongoReadRepository<Campaign> campaignReadRepository)
        {
            _campaignReadRepository = campaignReadRepository;
        }

        public async Task<GetCampaignByIdResponse> Handle(GetCampaignByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var campaign = await _campaignReadRepository.GetAsync(c => c.Id.ToString() == request.CampaignId);

            if (campaign == null)
            {
                return null; 
            }

            return new GetCampaignByIdResponse
            {
                CampaignId = campaign.Id.ToString(),
                Name = campaign.Name,
                Description = campaign.Description,
                CreatedDate = campaign.CreatedDate
            };
        }
    }
}
