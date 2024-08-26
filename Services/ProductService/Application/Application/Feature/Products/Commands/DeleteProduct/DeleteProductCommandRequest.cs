using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandRequest : IRequest<DeleteProductResponse>
    {
        public int ProductId { get; set; }
    }
}
