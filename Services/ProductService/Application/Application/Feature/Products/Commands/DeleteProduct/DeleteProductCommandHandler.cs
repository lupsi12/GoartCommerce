using Core.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feature.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductResponse>
    {
        private readonly IReadRepository<Product> _productReadRepository;
        private readonly IWriteRepository<Product> _productWriteRepository;
        private readonly IReadRepository<ProductCategory> _productCategoryReadRepository;
        private readonly IWriteRepository<ProductCategory> _productCategoryWriteRepository;

        public DeleteProductCommandHandler(
            IReadRepository<Product> productReadRepository,
            IWriteRepository<Product> productWriteRepository,
            IReadRepository<ProductCategory> productCategoryReadRepository,
            IWriteRepository<ProductCategory> productCategoryWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _productCategoryReadRepository = productCategoryReadRepository;
            _productCategoryWriteRepository = productCategoryWriteRepository;
        }

        public async Task<DeleteProductResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.GetAsync(p => p.Id == request.ProductId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            var productCategories = await _productCategoryReadRepository.Find(pc => pc.ProductId == product.Id).ToListAsync();

            foreach (var productCategory in productCategories)
            {
                await _productCategoryWriteRepository.HardDeleteAsync(productCategory);
            }
            await _productCategoryWriteRepository.SaveAsync();

            await _productWriteRepository.HardDeleteAsync(product);
            await _productWriteRepository.SaveAsync();

            return new DeleteProductResponse
            {
                ProductId = product.Id,
                IsDeleted = true
            };
        }
    }
}
