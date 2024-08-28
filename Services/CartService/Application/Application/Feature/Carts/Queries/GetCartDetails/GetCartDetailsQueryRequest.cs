using MediatR;
using System.ComponentModel;

namespace Application.Features.Carts.Queries.GetCartDetails
{
    public class GetCartDetailsQueryRequest : IRequest<GetCartDetailsQueryResponse>
    {
        [DefaultValue(1)]
        public int UserId { get; set; }
    }
}
