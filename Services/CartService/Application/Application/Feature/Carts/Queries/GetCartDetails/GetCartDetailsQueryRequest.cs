using MediatR;

namespace Application.Features.Carts.Queries.GetCartDetails
{
    public class GetCartDetailsQueryRequest : IRequest<GetCartDetailsQueryResponse>
    {
        public int UserId { get; set; }
    }
}
