using Domain.Entities;
using System;
using System.Collections.Generic;

namespace Application.Feature.Carts.Queries.GetAllCarts
{
    public class GetAllCartsResponse
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public CartStatus Status { get; set; }
        public decimal TotalPrice { get; set; }
        public IList<CartDetailResponse> CartItems { get; set; } = new List<CartDetailResponse>();
        public DateTime CreatedDate { get; set; }
    }

    public class CartDetailResponse
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal Subtotal { get; set; }
    }
}
