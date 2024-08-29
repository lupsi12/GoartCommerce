using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Carts.Queries.GetAllCarts
{
    public class GetAllCartsQueryRequest : IRequest<List<GetAllCartsResponse>>
    {
        [DefaultValue(1)]
        public int? UserId { get; set; }
        [DefaultValue("active")]
        public CartStatus? Status { get; set; } 
    }
}
