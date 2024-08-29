using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Products.Commands.ReduceProductStock
{
    public class ReduceProductStockCommandResponse
    {
        public int ProductId { get; set; }
        public int RemainingStock { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
