using MediatR;
using System.Collections.Generic;
using System.ComponentModel;

namespace Application.Feature.Products.Commands.CreateProduct
{
    public class CreateProductCommandRequest : IRequest<CreateProductResponse>
    {
        [DefaultValue("Default Product Name")]
        public string Name { get; set; } 

        [DefaultValue("Default Description")]
        public string Description { get; set; } 

        [DefaultValue(10)]
        public int Stock { get; set; } = 10;

        [DefaultValue(9.99)]
        public decimal Price { get; set; } = 9.99m;

        [DefaultValue(1)]
        public int UserId { get; set; } = 1;

        [DefaultValue(new int[] { 1, 2 })]
        public IList<int> CategoryIds { get; set; } = new List<int> { 1, 2 };
    }
}
