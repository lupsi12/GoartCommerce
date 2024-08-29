using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Queries.GetOrdersByUser;
using Core.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Orders.Queries.GetOrdersByUser
{
    public class GetOrdersByUserQueryHandler : IRequestHandler<GetOrdersByUserQueryRequest, List<GetOrdersByUserQueryResponse>>
    {
        private readonly IReadRepository<Order> _orderReadRepository;

        public GetOrdersByUserQueryHandler(IReadRepository<Order> orderReadRepository)
        {
            _orderReadRepository = orderReadRepository;
        }

        public async Task<List<GetOrdersByUserQueryResponse>> Handle(GetOrdersByUserQueryRequest request, CancellationToken cancellationToken)
        {
            var orders = await _orderReadRepository.GetAllAsync(
                predicate: o => o.UserId == request.UserId,
                include: null,
                orderBy: null,
                enableTracking: false);

            var orderResponses = orders.Select(order => new GetOrdersByUserQueryResponse
            {
                OrderId = order.Id,
                CreatedDate = order.CreatedDate,
                TotalPrice = order.TotalPrice,  
                Status = order.Status.ToString(),
                CartId = order.CartId
            }).ToList();

            return orderResponses;
        }
    }
}
