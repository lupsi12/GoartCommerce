using Domain.Entities;
using MediatR;
using System;
using Application.Feature.Homes.Rules;
using Core.MongoRepositories;
using MongoDB.Bson;


namespace Application.Feature.Homes.Command.CreateCampaignProduct
{
    public class CreateCampaignProductCommandHandler : IRequestHandler<CreateCampaignProductCommandRequest, CreateCampaignProductResponse>
    {
        private readonly CampaignProductRules _campaignProductRules;
        private readonly IMongoReadRepository<CampaignProduct> _campaignProductMongoReadRepository;
        private readonly IMongoWriteRepository<CampaignProduct> _campaignProductMongoWriteRepository;
        public CreateCampaignProductCommandHandler(
            CampaignProductRules campaignProductRules,
            IMongoReadRepository<CampaignProduct> campaignProductMongoReadRepository,
            IMongoWriteRepository<CampaignProduct> campaignProductMongoWriteRepository)
        {
            _campaignProductRules = campaignProductRules;
            _campaignProductMongoReadRepository = campaignProductMongoReadRepository;
            _campaignProductMongoWriteRepository = campaignProductMongoWriteRepository;
        }

        public async Task<CreateCampaignProductResponse> Handle(CreateCampaignProductCommandRequest request, CancellationToken cancellationToken)
        {
            CampaignProduct campaignProduct = new CampaignProduct()
            {
                Id = request.MongoId,
                ProductId = request.ProductId,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            await _campaignProductMongoWriteRepository.AddAsync(campaignProduct);

            return new CreateCampaignProductResponse()
            {
                Id = campaignProduct.Id.ToString(),
                ProductId = campaignProduct.ProductId,
                CreatedDate = campaignProduct.CreatedDate
            };
        }
    }
}