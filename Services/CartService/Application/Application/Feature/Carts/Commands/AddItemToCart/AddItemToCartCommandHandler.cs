using Core.Repositories;
using Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Carts.Commands.AddItemToCart
{
    public class AddItemToCartCommandHandler : IRequestHandler<AddItemToCartCommandRequest, AddItemToCartCommandResponse>
    {
        private readonly IReadRepository<Cart> _cartReadRepository;
        private readonly IWriteRepository<Cart> _cartWriteRepository;
        private readonly IWriteRepository<CartDetail> _cartDetailWriteRepository;
        private readonly ProductApiClient _productApiClient;

        public AddItemToCartCommandHandler(
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

        public async Task<AddItemToCartCommandResponse> Handle(AddItemToCartCommandRequest request, CancellationToken cancellationToken)
        {
            var cart = await _cartReadRepository.GetAsync(c => c.UserId == request.UserId && c.Status == CartStatus.Active);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = request.UserId,
                    Status = CartStatus.Active
                };
                await _cartWriteRepository.AddAsync(cart);
                await _cartWriteRepository.SaveAsync();
            }

            var cartDetail = cart.CartDetails.FirstOrDefault(cd => cd.ProductId == request.ProductId);

            if (cartDetail == null)
            {
                var product = await _productApiClient.GetProductByIdAsync(request.ProductId);
                if (product == null)
                {
                    throw new InvalidOperationException("Product not found.");
                }

                cartDetail = new CartDetail
                {
                    CartId = cart.Id,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    PricePerUnit = product.Price
                };
                await _cartDetailWriteRepository.AddAsync(cartDetail);
            }
            else
            {
                cartDetail.Quantity += request.Quantity;
                await _cartDetailWriteRepository.UpdateAsync(cartDetail);
            }

            await _cartDetailWriteRepository.SaveAsync();

            return new AddItemToCartCommandResponse
            {
                CartId = cart.Id,
                UserId = request.UserId,
                ProductId = request.ProductId,
                Quantity = cartDetail.Quantity,
                TotalPrice = cart.TotalPrice
            };
        }
    }
}