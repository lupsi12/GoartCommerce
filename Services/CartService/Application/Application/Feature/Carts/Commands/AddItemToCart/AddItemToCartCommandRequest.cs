using MediatR;
using System.ComponentModel;

namespace Application.Features.Carts.Commands.AddItemToCart
{
    public class AddItemToCartCommandRequest : IRequest<AddItemToCartCommandResponse>
    {
        [DefaultValue(1)]
        public int UserId { get; set; }

        [DefaultValue(1)]
        public int ProductId { get; set; }

        [DefaultValue(1)]
        public int Quantity { get; set; } = 1;
    }
}
