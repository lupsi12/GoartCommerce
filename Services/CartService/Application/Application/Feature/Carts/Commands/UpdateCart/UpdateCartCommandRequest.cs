using Application.Features.Carts.Commands.UpdateCart;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Carts.Commands.UpdateCart
{
    public class UpdateCartCommandRequest : IRequest<UpdateCartCommandResponse>
    {
        [DefaultValue(1)]
        public int CartId { get; set; }

        [DefaultValue(1)]
        public int UserId { get; set; }

        public List<CartItemUpdateDto> Items { get; set; } = new List<CartItemUpdateDto>();

        [DefaultValue(CartStatus.Active)]
        public CartStatus Status { get; set; } = CartStatus.Active;
    }

    public class CartItemUpdateDto
    {
        [DefaultValue(1)]
        public int ProductId { get; set; }

        [DefaultValue(1)]
        public int Quantity { get; set; } = 1;
    }
}
