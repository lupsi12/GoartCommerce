using Core.MongoRepositories;
using Core.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Application.Feature.Homes.Command.DeleteCampaign
{
    public class DeleteCampaignCommandHandler : IRequestHandler<DeleteCampaignCommandRequest, DeleteCampaignResponse>
    {
        private readonly IMongoReadRepository<Campaign> _campaignReadRepository;
        private readonly IMongoWriteRepository<Campaign> _campaignWriteRepository;

        public DeleteCampaignCommandHandler(
            IMongoReadRepository<Campaign> campaignReadRepository,
            IMongoWriteRepository<Campaign> campaignWriteRepository)
        {
            _campaignReadRepository = campaignReadRepository;
            _campaignWriteRepository = campaignWriteRepository;
           
        }

        public async Task<DeleteCampaignResponse> Handle(DeleteCampaignCommandRequest request, CancellationToken cancellationToken)
        {
            var campaign = await _campaignReadRepository.GetAsync(c => c.Id.ToString() == request.CampaignId);

            if (campaign == null)
            {
                return null; 
            }
            var filter = Builders<Campaign>.Filter.Eq(c => c.Id, campaign.Id);
            await _campaignWriteRepository.HardDeleteAsync(filter);

            return new DeleteCampaignResponse
            {
                CampaignId = campaign.Id.ToString(),
                IsDeleted = true
            };
        }
    }
}
