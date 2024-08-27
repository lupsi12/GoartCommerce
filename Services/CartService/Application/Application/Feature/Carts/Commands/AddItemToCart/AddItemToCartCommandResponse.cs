namespace Application.Features.Carts.Commands.AddItemToCart
{
    public class AddItemToCartCommandResponse
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
