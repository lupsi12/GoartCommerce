namespace Host.Controllers
{
    using Application.Features.Orders.Commands.CreateOrder;
    using Application.Features.Queries.GetOrdersByUser;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    namespace WebAPI.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class OrderController : ControllerBase
        {
            private readonly IMediator _mediator;

            public OrderController(IMediator mediator)
            {
                _mediator = mediator;
            }

            [HttpPost("create")]
            public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommandRequest request)
            {
                var result = await _mediator.Send(request);
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest("Failed to create order.");
            }

            [HttpGet("user/{userId}/orders")]
            public async Task<IActionResult> GetOrdersByUser(int userId)
            {
                var query = new GetOrdersByUserQueryRequest { UserId = userId };
                var result = await _mediator.Send(query);
                return Ok(result);
            }




        }
    }

}
