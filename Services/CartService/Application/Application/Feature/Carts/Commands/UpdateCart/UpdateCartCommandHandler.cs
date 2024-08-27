using Application.Feature.Carts.Commands.UpdateCart;
using Core.Repositories;
using Domain.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Carts.Commands.UpdateCart
{
    public class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommandRequest, UpdateCartCommandResponse>
    {
        private readonly IReadRepository<Cart> _cartReadRepository;
        private readonly IWriteRepository<Cart> _cartWriteRepository;
        private readonly IWriteRepository<CartDetail> _cartDetailWriteRepository;
        private readonly ProductApiClient _productApiClient;

        public UpdateCartCommandHandler(
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

        public async Task<UpdateCartCommandResponse> Handle(UpdateCartCommandRequest request, CancellationToken cancellationToken)
        {
            var cart = await _cartReadRepository.GetAsync(c => c.Id == request.CartId && c.UserId == request.UserId);

            if (cart == null)
            {
                throw new InvalidOperationException("Cart not found.");
            }

            cart.Status = request.Status;

            foreach (var item in request.Items)
            {
                var cartDetail = cart.CartDetails.FirstOrDefault(cd => cd.ProductId == item.ProductId);

                if (cartDetail == null)
                {
                    var product = await _productApiClient.GetProductByIdAsync(item.ProductId);
                    if (product == null)
                    {
                        throw new InvalidOperationException("Product not found.");
                    }

                    cartDetail = new CartDetail
                    {
                        CartId = cart.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        PricePerUnit = product.Price
                    };
                    await _cartDetailWriteRepository.AddAsync(cartDetail);
                }
                else
                {
                    cartDetail.Quantity = item.Quantity;

                    if (item.Quantity == 0)
                    {
                        await _cartDetailWriteRepository.HardDeleteAsync(cartDetail);
                    }
                    else
                    {
                        await _cartDetailWriteRepository.UpdateAsync(cartDetail);
                    }
                }
            }

            await _cartDetailWriteRepository.SaveAsync();
            await _cartWriteRepository.SaveAsync();

            return new UpdateCartCommandResponse
            {
                CartId = cart.Id,
                UserId = cart.UserId,
                Items = request.Items,
                Status = cart.Status,
                TotalPrice = cart.TotalPrice
            };
        }
    }
}
