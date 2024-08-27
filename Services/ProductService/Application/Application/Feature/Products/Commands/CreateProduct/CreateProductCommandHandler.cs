using Application.Features.Products.Rules;
using Core.Repositories;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feature.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductResponse>
    {
        private readonly ProductRules _productRules;
        private readonly IReadRepository<Product> _productReadRepository;
        private readonly IWriteRepository<Product> _productWriteRepository;
        private readonly IWriteRepository<ProductCategory> _productCategoryWriteRepository;

        public CreateProductCommandHandler(
            ProductRules productRules,
            IReadRepository<Product> productReadRepository,
            IWriteRepository<Product> productWriteRepository,
            IWriteRepository<ProductCategory> productCategoryWriteRepository)
        {
            _productRules = productRules;
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _productCategoryWriteRepository = productCategoryWriteRepository;
        }

        public async Task<CreateProductResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            _productRules.CheckIfCategoryIdsAreValid(request.CategoryIds);

            Product product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Stock = request.Stock,
                Price = request.Price,
                UserId = request.UserId,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false
            };

            await _productWriteRepository.AddAsync(product);
            await _productWriteRepository.SaveAsync(); 

            foreach (var categoryId in request.CategoryIds)
            {
                var productCategory = new ProductCategory
                {
                    ProductId = product.Id,
                    CategoryId = categoryId
                };
                await _productCategoryWriteRepository.AddAsync(productCategory);
            }

            await _productCategoryWriteRepository.SaveAsync();

            return new CreateProductResponse
            {
                ProductId = product.Id,
                Name = product.Name,
                Description = product.Description,
                Stock = product.Stock,
                Price = product.Price,
                UserId = product.UserId,
                CategoryIds = request.CategoryIds,
                CreatedDate = product.CreatedDate
            };
        }

    }
}