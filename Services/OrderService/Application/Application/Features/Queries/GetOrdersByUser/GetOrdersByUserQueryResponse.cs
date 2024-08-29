using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetOrdersByUser
{
    public class GetOrdersByUserQueryResponse
    {
        public int OrderId { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public int CartId { get; set; }
    }
}
