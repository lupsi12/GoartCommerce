using Core.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feature.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, List<GetAllProductsResponse>>
    {
        private readonly IReadRepository<Product> _productReadRepository;

        public GetAllProductsQueryHandler(IReadRepository<Product> productReadRepository)
        {
            _productReadRepository = productReadRepository;
        }

        public async Task<List<GetAllProductsResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await _productReadRepository.GetAllByPagingAsync(
                predicate: p =>
                    (!request.UserId.HasValue || p.UserId == request.UserId) &&
                    (!request.CategoryId.HasValue || p.ProductCategories.Any(pc => pc.CategoryId == request.CategoryId)),
                include: q => q.Include(p => p.ProductCategories),
                orderBy: q => q.OrderBy(p => p.Name),
                enableTracking: false,
                currentPage: request.PageNumber,
                pageSize: request.PageSize);

            return products.Select(product => new GetAllProductsResponse
            {
                ProductId = product.Id,
                Name = product.Name,
                Description = product.Description,
                Stock = product.Stock,
                Price = product.Price,
                UserId = product.UserId,
                CategoryIds = product.ProductCategories.Select(pc => pc.CategoryId).ToList(),
                CreatedDate = product.CreatedDate
            }).ToList();
        }
    }
}
