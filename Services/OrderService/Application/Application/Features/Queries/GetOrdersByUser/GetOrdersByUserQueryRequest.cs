using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.GetOrdersByUser
{
    public class GetOrdersByUserQueryRequest : IRequest<List<GetOrdersByUserQueryResponse>>
    {
        public int UserId { get; set; }
    }
}
