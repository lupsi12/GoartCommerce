using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Products.Commands.CreateProduct
{
    public class CreateProductResponse
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int UserId { get; set; } 
        public IList<int> CategoryIds { get; set; } 
        public DateTime CreatedDate { get; set; }
    }
}
