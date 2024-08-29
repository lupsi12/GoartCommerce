using Core.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feature.Carts.Queries.GetAllCarts
{
    public class GetAllCartsQueryHandler : IRequestHandler<GetAllCartsQueryRequest, List<GetAllCartsResponse>>
    {
        private readonly IReadRepository<Cart> _cartReadRepository;

        public GetAllCartsQueryHandler(IReadRepository<Cart> cartReadRepository)
        {
            _cartReadRepository = cartReadRepository;
        }

        public async Task<List<GetAllCartsResponse>> Handle(GetAllCartsQueryRequest request, CancellationToken cancellationToken)
        {
            var carts = await _cartReadRepository.GetAllAsync(
                predicate: c =>
                    (!request.UserId.HasValue || c.UserId == request.UserId) &&
                    (!request.Status.HasValue || c.Status == request.Status),
                include: q => q.Include(c => c.CartDetails),
                enableTracking: false);

            return carts.Select(cart => new GetAllCartsResponse
            {
                CartId = cart.Id,
                UserId = cart.UserId,
                Status = cart.Status,
                TotalPrice = cart.TotalPrice,
                CartItems = cart.CartDetails.Select(cd => new CartDetailResponse
                {
                    ProductId = cd.ProductId,
                    Quantity = cd.Quantity,
                    PricePerUnit = cd.PricePerUnit,
                    Subtotal = cd.Subtotal
                }).ToList(),
                CreatedDate = cart.CreatedDate
            }).ToList();
        }
    }
}
