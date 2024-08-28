using System.Collections.Generic;

namespace Application.Features.Carts.Queries.GetCartDetails
{
    public class GetCartDetailsQueryResponse
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<CartDetailDto> CartDetails { get; set; }
    }

    public class CartDetailDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal Subtotal { get; set; }
    }
}
