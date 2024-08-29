using Core.Repositories;
using Domain.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Feature.Carts.Commands.CreateCart
{
    public class CreateCartCommandHandler : IRequestHandler<CreateCartCommandRequest, CreateCartCommandResponse>
    {
        private readonly IWriteRepository<Cart> _cartWriteRepository;

        public CreateCartCommandHandler(IWriteRepository<Cart> cartWriteRepository)
        {
            _cartWriteRepository = cartWriteRepository;
        }

        public async Task<CreateCartCommandResponse> Handle(CreateCartCommandRequest request, CancellationToken cancellationToken)
        {
            var cart = new Cart
            {
                UserId = request.UserId,
                Status = CartStatus.Active,
                CreatedDate = DateTime.UtcNow,
                TotalPrice = 0
            };

            await _cartWriteRepository.AddAsync(cart);
            await _cartWriteRepository.SaveAsync();

            return new CreateCartCommandResponse
            {
                CartId = cart.Id,
                UserId = cart.UserId,
                Status = cart.Status.ToString(),
                TotalPrice = cart.TotalPrice
            };
        }
    }
}
