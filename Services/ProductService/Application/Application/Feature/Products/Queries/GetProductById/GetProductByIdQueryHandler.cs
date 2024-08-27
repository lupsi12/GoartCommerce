using Core.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feature.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, GetProductByIdResponse>
    {
        private readonly IReadRepository<Product> _productReadRepository;

        public GetProductByIdQueryHandler(IReadRepository<Product> productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<GetProductByIdResponse> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var product = await _productReadRepository.GetAsync(
                predicate: p => p.Id == request.ProductId,
                include: q => q.Include(p => p.ProductCategories),
                enableTracking: false);

            if (product == null)
            {
                return null; 
            }

            return new GetProductByIdResponse
            {
                ProductId = product.Id,
                Name = product.Name,
                Description = product.Description,
                Stock = product.Stock,
                Price = product.Price,
                UserId = product.UserId,
                CategoryIds = product.ProductCategories.Select(pc => pc.CategoryId).ToList(),
                CreatedDate = product.CreatedDate
            };
        }
    }
}
