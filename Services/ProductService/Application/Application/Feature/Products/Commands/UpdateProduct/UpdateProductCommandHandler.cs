using Application.Features.Products.Rules;
using Core.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feature.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest, UpdateProductResponse>
    {
        private readonly ProductRules _productRules;
        private readonly IReadRepository<Product> _productReadRepository;
        private readonly IWriteRepository<Product> _productWriteRepository;
        private readonly IReadRepository<ProductCategory> _productCategoryReadRepository; 
        private readonly IWriteRepository<ProductCategory> _productCategoryWriteRepository;

        public UpdateProductCommandHandler(
            ProductRules productRules,
            IReadRepository<Product> productReadRepository,
            IWriteRepository<Product> productWriteRepository,
            IReadRepository<ProductCategory> productCategoryReadRepository, 
            IWriteRepository<ProductCategory> productCategoryWriteRepository)
        {
            _productRules = productRules;
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _productCategoryReadRepository = productCategoryReadRepository;
            _productCategoryWriteRepository = productCategoryWriteRepository;
        }

        public async Task<UpdateProductResponse> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            _productRules.CheckIfCategoryIdsAreValid(request.CategoryIds);

            var product = await _productReadRepository.GetAsync(p => p.Id == request.ProductId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.Stock = request.Stock;
            product.Price = request.Price;
            product.UserId = request.UserId;

            await _productWriteRepository.UpdateAsync(product);
            await _productWriteRepository.SaveAsync();

            var existingCategories = await _productCategoryReadRepository.Find(pc => pc.ProductId == product.Id).ToListAsync(); 
            foreach (var existingCategory in existingCategories)
            {
                await _productCategoryWriteRepository.HardDeleteAsync(existingCategory);
            }
            await _productCategoryWriteRepository.SaveAsync();

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

            return new UpdateProductResponse
            {
                ProductId = product.Id,
                Name = product.Name,
                Description = product.Description,
                Stock = product.Stock,
                Price = product.Price,
                UserId = product.UserId,
                CategoryIds = request.CategoryIds,
                UpdatedDate = DateTime.UtcNow
            };
        }
    }
}
