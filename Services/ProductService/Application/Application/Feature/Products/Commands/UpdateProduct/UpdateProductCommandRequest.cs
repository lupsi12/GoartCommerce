using MediatR;
using System.Collections.Generic;
using System.ComponentModel;

namespace Application.Feature.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandRequest : IRequest<UpdateProductResponse>
    {
        [DefaultValue(1)]
        public int ProductId { get; set; }

        [DefaultValue("Updated Product Name")]
        public string Name { get; set; } = "Updated Product Name";

        [DefaultValue("Updated Description")]
        public string Description { get; set; } = "Updated Description";

        [DefaultValue(15)]
        public int Stock { get; set; } = 15;

        [DefaultValue(19.99)]
        public decimal Price { get; set; } = 19.99m;

        [DefaultValue(1)]
        public int UserId { get; set; } = 1;

        [DefaultValue(new int[] { 1, 2 })]
        public IList<int> CategoryIds { get; set; } = new List<int> { 1, 2 };
    }
}
