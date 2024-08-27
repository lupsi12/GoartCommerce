using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Products.Queries.GetProductById
{
    public class GetProductByIdQueryRequest : IRequest<GetProductByIdResponse>
    {
        public int ProductId { get; set; }
    }
}
