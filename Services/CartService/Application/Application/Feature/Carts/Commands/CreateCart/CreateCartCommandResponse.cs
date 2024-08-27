namespace Application.Feature.Carts.Commands.CreateCart
{
    public class CreateCartCommandResponse
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
