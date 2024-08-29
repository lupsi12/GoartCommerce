
using Domain.Entities;
using MediatR;
using System;
using Application.Feature.Rules;
using Application.Feature.Users.Commands.CreateUser;
using Core.MongoRepositories;


namespace Application.Feature.Users.Command.CreateCampaign
{
    public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommandRequest, CreateCampaignResponse>
    {
        private readonly CampaignRules _campaignRules;
        private readonly IMongoReadRepository<Campaign> _campaignMongoReadRepository;
        private readonly IMongoWriteRepository<Campaign> _campaignMongoWriteRepository;
        public CreateCampaignCommandHandler(
            CampaignRules campaignRules,
            IMongoReadRepository<Campaign> campaignMongoReadRepository,
            IMongoWriteRepository<Campaign> campaignMongoWriteRepository)
        {
            _campaignRules = campaignRules;
            _campaignMongoReadRepository = campaignMongoReadRepository;
            _campaignMongoWriteRepository = campaignMongoWriteRepository;
        }

        public async Task<CreateCampaignResponse> Handle(CreateCampaignCommandRequest request, CancellationToken cancellationToken)
        {
            Campaign campaign = new Campaign()
            {
                Name = request.Name,
                Description = request.Description,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            await _campaignMongoWriteRepository.AddAsync(campaign);

            return new CreateCampaignResponse()
            {
                CampaignId = campaign.Id,
                Name = campaign.Name,
                Description = campaign.Description,
                //CreatedDate = campaign.CreatedDate
            };
        }
    }
}