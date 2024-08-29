using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Clients;
using Application.Features.Orders.Commands.CreateOrder;
using Core.Repositories;
using Domain.Entities;
using MediatR;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommandRequest, CreateOrderCommandResponse>
{
    private readonly IWriteRepository<Order> _orderWriteRepository;
    private readonly CartApiClient _cartApiClient;
    private readonly ProductApiClient _productApiClient;

    public CreateOrderCommandHandler(
        IWriteRepository<Order> orderWriteRepository,
        CartApiClient cartApiClient,
        ProductApiClient productApiClient)
    {
        _orderWriteRepository = orderWriteRepository;
        _cartApiClient = cartApiClient;
        _productApiClient = productApiClient;
    }

    public async Task<CreateOrderCommandResponse> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
    {
        var activeCart = await _cartApiClient.GetActiveCartByUserIdAsync(request.UserId);

        if (activeCart == null)
        {
            throw new InvalidOperationException("No active cart found for this user.");
        }

        if (activeCart.Status != CartStatus.Active)
        {
            throw new InvalidOperationException("Only active carts can be converted to an order.");
        }

        var order = new Order
        {
            UserId = activeCart.UserId,
            CartId = activeCart.Id,
            Status = OrderStatus.Pending,
            CreatedDate = DateTime.UtcNow,
            TotalPrice = activeCart.TotalPrice
        };

        await _orderWriteRepository.AddAsync(order);
        await _orderWriteRepository.SaveAsync();

        foreach (var cartItem in activeCart.CartItems)
        {
            var stockUpdateSuccess = await _productApiClient.ReduceProductStockAsync(cartItem.ProductId, cartItem.Quantity);
            if (!stockUpdateSuccess)
            {
                throw new InvalidOperationException($"Failed to update stock for product {cartItem.ProductId}.");
            }
        }

        var statusUpdateSuccess = await _cartApiClient.UpdateCartStatusAsync(
            activeCart.Id,
            activeCart.UserId,
            CartStatus.Ordered,
            activeCart.CartItems);

        if (!statusUpdateSuccess)
        {
            throw new InvalidOperationException("An error occurred while updating the cart status.");
        }

        await _cartApiClient.CreateCartAsync(activeCart.UserId);

        return new CreateOrderCommandResponse
        {
            OrderId = order.Id,
            TotalPrice = activeCart.TotalPrice,
            Status = order.Status
        };
    }
}
