
using Core.MongoRepositories;
using Core.Repositories;
using Domain.Entities;
using MediatR;
using MongoDB.Driver;

namespace Application.Feature.Homes.Command.UpdateCampaign
{
    public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommandRequest, UpdateCampaignResponse>
    {
        private readonly IMongoReadRepository<Campaign> _campaignReadRepository;
        private readonly IMongoWriteRepository<Campaign> _campaignWriteRepository;

        public UpdateCampaignCommandHandler(
            IMongoReadRepository<Campaign> campaignReadRepository,
            IMongoWriteRepository<Campaign> campaignWriteRepository)
        {
            _campaignReadRepository = campaignReadRepository;
            _campaignWriteRepository = campaignWriteRepository;
        }

        public async Task<UpdateCampaignResponse> Handle(UpdateCampaignCommandRequest request, CancellationToken cancellationToken)
        {
            var campaign = await _campaignReadRepository.GetAsync(p => p.Id.ToString() == request.CampaignId);
            if (campaign == null)
            {
                throw new Exception("campaign not found");
            }

            campaign.Description = request.Description;
            var filter = Builders<Campaign>.Filter.Eq(c => c.Id, campaign.Id);
            
            var update = Builders<Campaign>.Update.Set(c => c.Description, campaign.Description);
            
            await _campaignWriteRepository.UpdateAsync(filter,update);

            return new UpdateCampaignResponse
            {
                CampaignId = campaign.Id.ToString(),
                Name = campaign.Name,
                Description = campaign.Description,
                UpdatedDate = DateTime.UtcNow
            };
        }
    }
}
