
using System;
using System.Collections.Generic;

namespace Application.Feature.Products.Queries.GetAllProducts
{
    public class GetAllProductsResponse
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