using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Products.Commands.ReduceProductStock
{
    public class ReduceProductStockCommandRequest : IRequest<ReduceProductStockCommandResponse>
    {
        public int ProductId { get; set; }
        public int QuantityToReduce { get; set; }
    }
}
