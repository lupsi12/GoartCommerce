
using Application.Feature.Homes.Command.CreateCampaignProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CampaignProductController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateCampaignProduct([FromBody] CreateCampaignProductCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
        
        //  [HttpGet]
        // public async Task<IActionResult> GetAllCampaignProducts([FromQuery] GetAllCampaignProductsQueryRequest request)
        // {
        //     var result = await _mediator.Send(request);
        //     return Ok(result);
        // }
        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetCampaignProductById(string id)
        // {
        //     var query = new GetCampaignProductByIdQueryRequest { CampaignProductId = id };
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
        //     var command = new DeleteCampaignProductCommandRequest { CampaignProductId = id };
        //     var result = await _mediator.Send(command);
        //     if (result.IsDeleted)
        //     {
        //         return Ok(result);
        //     }
        //     return NotFound();
        // }
        // [HttpPut]
        // public async Task<IActionResult> UpdateCampaignProduct([FromBody] UpdateCampaignProductCommandRequest request)
        // {
        //     var result = await _mediator.Send(request);
        //     return Ok(result);
        // }
    }
}