using MediatR;

namespace Application.Features.Orders.CreateOrder
{
    public class CreateOrderCommandRequest : IRequest<CreateOrderCommandResponse>
    {
        public int UserId { get; set; }
        public int CartId { get; set; }
    }
}
