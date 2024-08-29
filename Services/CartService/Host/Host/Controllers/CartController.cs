using Application.Features.Carts.Commands.AddItemToCart;
using Application.Features.Carts.Commands.UpdateCart;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Feature.Carts.Commands.CreateCart;
using Application.Features.Carts.Queries.GetCartDetails;
using Application.Features.Carts.Queries.ValidateCartItems;
using Application.Feature.Carts.Queries.ValidateCartItems;
using Application.Feature.Carts.Commands.UpdateCart;
using Application.Feature.Carts.Queries.GetAllCarts;
using Domain.Entities;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userId}/details")]
        public async Task<IActionResult> GetCartDetails(int userId=1)
        {
            var query = new GetCartDetailsQueryRequest { UserId = userId };
            var result = await _mediator.Send(query);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCart([FromBody] CreateCartCommandRequest request)
        {
            var result = await _mediator.Send(request);
            if (result.CartId != default)
            {
                return Ok(result);
            }
            return BadRequest("Failed to create cart.");
        }

        [HttpPost("add-item")]
        public async Task<IActionResult> AddItemToCart([FromBody] AddItemToCartCommandRequest request)
        {
            var result = await _mediator.Send(request);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Failed to add item to cart.");
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCart([FromBody] UpdateCartCommandRequest request)
        {
            var result = await _mediator.Send(request);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Failed to update cart.");
        }

        [HttpGet("{cartId}/validate")]
        public async Task<IActionResult> ValidateCartItems(int cartId=1)
        {
            var query = new ValidateCartItemsQuery(cartId);
            var result = await _mediator.Send(query);
            return Ok(result); 
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCarts([FromQuery] int? userId = null, [FromQuery] CartStatus? status = null)
        {
            var query = new GetAllCartsQueryRequest { UserId = userId, Status = status };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
