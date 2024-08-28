using Application.Features.Carts.Queries.ValidateCartItems;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Feature.Carts.Queries.ValidateCartItems
{
    public class ValidateCartItemsQuery : IRequest<ValidateCartItemsResponse>
    {
        [DefaultValue(1)]
        public int CartId { get; set; }

        public ValidateCartItemsQuery(int cartId)
        {
            CartId = cartId;
        }
    }
}
