using System.Collections.Generic;

namespace Application.Features.Carts.Queries.ValidateCartItems
{
    public class ValidateCartItemsResponse
    {
        public int CartId { get; set; }
        public decimal TotalPrice { get; set; }
        public List<CartItemValidationResult> Items { get; set; } = new List<CartItemValidationResult>();
    }

    public class CartItemValidationResult
    {
        public int ProductId { get; set; }
        public string Status { get; set; } 
        public decimal? NewSubtotal { get; set; } 
    }
}
