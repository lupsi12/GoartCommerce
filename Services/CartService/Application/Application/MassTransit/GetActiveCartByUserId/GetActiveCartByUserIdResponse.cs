using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MassTransit.GetActiveCartByUserId
{
    public class GetActiveCartByUserIdResponse
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public CartStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public List<CartItemDto> CartItems { get; set; }
    }

    public class CartItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

}
