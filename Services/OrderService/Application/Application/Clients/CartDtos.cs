using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Clients
{

    public class CartDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
        public List<CartDetailDto> CartDetails { get; set; }
    }

    public class CartDetailDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal Subtotal { get; set; }
    }
    public class CartStatusUpdateDto
    {
        public string Status { get; set; }
    }

}
