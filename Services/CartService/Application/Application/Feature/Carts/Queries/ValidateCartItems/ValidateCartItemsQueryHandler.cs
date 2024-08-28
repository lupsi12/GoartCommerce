using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Feature.Carts.Queries.ValidateCartItems;
using Core.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Carts.Queries.ValidateCartItems
{
    public class ValidateCartItemsQueryHandler : IRequestHandler<ValidateCartItemsQuery, ValidateCartItemsResponse>
    {
        private readonly IReadRepository<Cart> _cartReadRepository;
        private readonly IWriteRepository<Cart> _cartWriteRepository;
        private readonly IWriteRepository<CartDetail> _cartDetailWriteRepository;
        private readonly ProductApiClient _productApiClient;

        public ValidateCartItemsQueryHandler(
            IReadRepository<Cart> cartReadRepository,
            IWriteRepository<Cart> cartWriteRepository,
            IWriteRepository<CartDetail> cartDetailWriteRepository,
            ProductApiClient productApiClient)
        {
            _cartReadRepository = cartReadRepository;
            _cartWriteRepository = cartWriteRepository;
            _cartDetailWriteRepository = cartDetailWriteRepository;
            _productApiClient = productApiClient;
        }

        public async Task<ValidateCartItemsResponse> Handle(ValidateCartItemsQuery request, CancellationToken cancellationToken)
        {
            var cart = await _cartReadRepository.GetAsync(
                c => c.Id == request.CartId && c.Status == CartStatus.Active,
                include: c => c.Include(cart => cart.CartDetails)
            );

            if (cart == null)
            {
                throw new InvalidOperationException("Cart not found.");
            }

            var response = new ValidateCartItemsResponse
            {
                CartId = cart.Id
            };

            bool cartUpdated = false;

            foreach (var cartDetail in cart.CartDetails.ToList())
            {
                var product = await _productApiClient.GetProductByIdAsync(cartDetail.ProductId);

                if (product == null || product.Stock < cartDetail.Quantity)
                {
                    await _cartDetailWriteRepository.HardDeleteAsync(cartDetail);
                    cart.CartDetails.Remove(cartDetail);
                    response.Items.Add(new CartItemValidationResult
                    {
                        ProductId = cartDetail.ProductId,
                        Status = "Deleted"
                    });
                    cartUpdated = true;
                }
                else if (product.Price != cartDetail.PricePerUnit)
                {
                    cartDetail.PricePerUnit = product.Price;
                    cartDetail.Subtotal = cartDetail.Quantity * product.Price;
                    await _cartDetailWriteRepository.UpdateAsync(cartDetail);

                    response.Items.Add(new CartItemValidationResult
                    {
                        ProductId = cartDetail.ProductId,
                        Status = "Updated",
                        NewSubtotal = cartDetail.Subtotal
                    });
                    cartUpdated = true;
                }
            }

            if (cartUpdated)
            {
                cart.TotalPrice = cart.CartDetails.Sum(cd => cd.Subtotal);
                await _cartWriteRepository.UpdateAsync(cart);
                await _cartDetailWriteRepository.SaveAsync();
            }

            response.TotalPrice = cart.TotalPrice;

            return response;
        }
    }
}
