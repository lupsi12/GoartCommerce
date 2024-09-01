
using Application.Feature.Homes.Command.CreateViewedProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ViewedProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ViewedProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateViewedProduct([FromBody] CreateViewedProductCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
        
        //  [HttpGet]
        // public async Task<IActionResult> GetAllViewedProducts([FromQuery] GetAllViewedProductsQueryRequest request)
        // {
        //     var result = await _mediator.Send(request);
        //     return Ok(result);
        // }
        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetViewedProductById(string id)
        // {
        //     var query = new GetViewedProductByIdQueryRequest { ViewedProductId = id };
        //     var result = await _mediator.Send(query);
        //     if (result != null)
        //     {
        //         return Ok(result);
        //     }
        //     return NotFound();
        // }
        // [HttpDelete("{id}")]
        // public async Task<IActionResult> Delete(string id)
        // {
        //     var command = new DeleteViewedProductCommandRequest { ViewedProductId = id };
        //     var result = await _mediator.Send(command);
        //     if (result.IsDeleted)
        //     {
        //         return Ok(result);
        //     }
        //     return NotFound();
        // }
        // [HttpPut]
        // public async Task<IActionResult> UpdateViewedProduct([FromBody] UpdateViewedProductCommandRequest request)
        // {
        //     var result = await _mediator.Send(request);
        //     return Ok(result);
        // }
    }
}