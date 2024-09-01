using Domain.Entities;
using MediatR;
using System;
using Application.Feature.Homes.Rules;
using Core.MongoRepositories;
using MongoDB.Bson;


namespace Application.Feature.Homes.Command.CreateViewedProduct
{
    public class CreateViewedProductCommandHandler : IRequestHandler<CreateViewedProductCommandRequest, CreateViewedProductResponse>
    {
        private readonly ViewedProductRules _viewedProductRules;
        private readonly IMongoReadRepository<ViewedProduct> _viewedProductMongoReadRepository;
        private readonly IMongoWriteRepository<ViewedProduct> _viewedProductMongoWriteRepository;
        public CreateViewedProductCommandHandler(
            ViewedProductRules viewedProductRules,
            IMongoReadRepository<ViewedProduct> viewedProductMongoReadRepository,
            IMongoWriteRepository<ViewedProduct> viewedProductMongoWriteRepository)
        {
            _viewedProductRules = viewedProductRules;
            _viewedProductMongoReadRepository = viewedProductMongoReadRepository;
            _viewedProductMongoWriteRepository = viewedProductMongoWriteRepository;
        }

        public async Task<CreateViewedProductResponse> Handle(CreateViewedProductCommandRequest request, CancellationToken cancellationToken)
        {
            ViewedProduct viewedProduct = new ViewedProduct()
            {
                Id = request.MongoId,
                ProductId = request.ProductId,
                UserId = request.UserId,
                CreatedDate = DateTime.UtcNow,
                ViewedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            await _viewedProductMongoWriteRepository.AddAsync(viewedProduct);

            return new CreateViewedProductResponse()
            {
                Id = viewedProduct.Id.ToString(),
                ProductId = viewedProduct.ProductId,
                UserId = viewedProduct.UserId,
                ViewedDate = viewedProduct.ViewedDate,
                CreatedDate = viewedProduct.CreatedDate
            };
        }
    }
}