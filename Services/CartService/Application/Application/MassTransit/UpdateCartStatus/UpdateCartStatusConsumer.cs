using Core.Repositories;
using Domain.Entities;
using MassTransit;
using System.Threading.Tasks;

namespace Application.MassTransit.UpdateCartStatus
{
    public class UpdateCartStatusConsumer : IConsumer<UpdateCartStatusRequest>
    {
        private readonly IReadRepository<Cart> _cartReadRepository;
        private readonly IWriteRepository<Cart> _cartWriteRepository;

        public UpdateCartStatusConsumer(IReadRepository<Cart> cartReadRepository, IWriteRepository<Cart> cartWriteRepository)
        {
            _cartReadRepository = cartReadRepository;
            _cartWriteRepository = cartWriteRepository;
        }

        public async Task Consume(ConsumeContext<UpdateCartStatusRequest> context)
        {
            var request = context.Message;

            var cart = await _cartReadRepository.GetAsync(c => c.Id == request.CartId && c.UserId == request.UserId);

            if (cart == null)
            {
                await context.RespondAsync(new UpdateCartStatusResponse { Success = false });
                return;
            }

            cart.Status = request.NewStatus;

            await _cartWriteRepository.UpdateAsync(cart);
            await _cartWriteRepository.SaveAsync();

            await context.RespondAsync(new UpdateCartStatusResponse { Success = true });
        }
    }
}
