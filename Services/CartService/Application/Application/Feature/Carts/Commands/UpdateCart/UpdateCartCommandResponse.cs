using Application.Feature.Carts.Commands.UpdateCart;
using Domain.Entities;

namespace Application.Features.Carts.Commands.UpdateCart
{
    public class UpdateCartCommandResponse
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public List<CartItemUpdateDto> Items { get; set; }
        public CartStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
