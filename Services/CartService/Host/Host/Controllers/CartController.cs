using Application.Features.Carts.Commands.AddItemToCart;
using Application.Features.Carts.Commands.UpdateCart;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Feature.Carts.Commands.CreateCart;
using Application.Feature.Carts.Commands.UpdateCart;

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
    }
}