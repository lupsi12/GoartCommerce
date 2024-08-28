using Core.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Carts.Queries.GetCartDetails
{
    public class GetCartDetailsQueryHandler : IRequestHandler<GetCartDetailsQueryRequest, GetCartDetailsQueryResponse>
    {
        private readonly IReadRepository<Cart> _cartReadRepository;
        private readonly ProductApiClient _productApiClient;

        public GetCartDetailsQueryHandler(IReadRepository<Cart> cartReadRepository, ProductApiClient productApiClient)
        {
            _cartReadRepository = cartReadRepository;
            _productApiClient = productApiClient;
        }

        public async Task<GetCartDetailsQueryResponse> Handle(GetCartDetailsQueryRequest request, CancellationToken cancellationToken)
        {
            var cart = await _cartReadRepository
                .GetAsync(c => c.UserId == request.UserId && c.Status == CartStatus.Active,
                         include: c => c.Include(cart => cart.CartDetails));

            if (cart == null)
            {
                return null;

            }

            var cartDetailsDtos = cart.CartDetails.Select(async cd =>
            {
                var product = await _productApiClient.GetProductByIdAsync(cd.ProductId);
                return new CartDetailDto
                {
                    ProductId = cd.ProductId,
                    ProductName = product?.Name ?? "Unknown Product",
                    Quantity = cd.Quantity,
                    PricePerUnit = cd.PricePerUnit,
                    Subtotal = cd.Subtotal
                };
            }).Select(task => task.Result).ToList();

            return new GetCartDetailsQueryResponse
            {
                CartId = cart.Id,
                UserId = cart.UserId,
                TotalPrice = cart.TotalPrice,
                CartDetails = cartDetailsDtos
            };
        }
    }
}
