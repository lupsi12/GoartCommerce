using System.Threading;
using System.Threading.Tasks;
using Application.Clients;
using Application.Features.Orders.CreateOrder;
using Core.Repositories;
using Domain.Entities;
using MediatR;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
{
    private readonly IWriteRepository<Order> _orderWriteRepository;
    private readonly CartApiClient _cartApiClient;

    public CreateOrderCommandHandler(IWriteRepository<Order> orderWriteRepository, CartApiClient cartApiClient)
    {
        _orderWriteRepository = orderWriteRepository;
        _cartApiClient = cartApiClient;
    }

    public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
    {
        var cart = await _cartApiClient.GetCartByIdAsync(request.CartId);

        if (cart == null)
        {
            throw new InvalidOperationException("Cart not found.");
        }

        if (cart.Status != "Active")
        {
            throw new InvalidOperationException("Only active carts can be converted to an order.");
        }

        var order = new Order
        {
            UserId = cart.UserId,
            CartId = cart.Id,
            Status = OrderStatus.Pending,
            CreatedDate = DateTime.UtcNow,
        };

        await _orderWriteRepository.AddAsync(order);
        await _orderWriteRepository.SaveAsync();

        var statusUpdateDto = new CartStatusUpdateDto { Status = "Ordered" };
        await _cartApiClient.UpdateCartStatusAsync(cart.Id, statusUpdateDto);

        return new CreateOrderCommandResponse
        {
            OrderId = order.Id,
            TotalPrice = cart.TotalPrice,
            Status = order.Status
        };
    }
}
