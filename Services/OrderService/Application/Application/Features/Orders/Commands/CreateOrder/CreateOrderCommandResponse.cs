using Domain.Entities;

namespace Application.Features.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandResponse
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
