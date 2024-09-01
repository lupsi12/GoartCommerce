

using Application.Feature.Homes.Command.CreateCampaign;
using Application.Feature.Homes.Command.DeleteCampaign;
using Application.Feature.Homes.Command.UpdateCampaign;
using Application.Feature.Homes.Queries.GetAllCampaigns;
using Application.Feature.Homes.Queries.GetCampaignById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CampaignController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateCampaign([FromBody] CreateCampaignCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }

         [HttpGet]
        public async Task<IActionResult> GetAllCampaigns([FromQuery] GetAllCampaignsQueryRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCampaignById(string id)
        {
            var query = new GetCampaignByIdQueryRequest { CampaignId = id };
            var result = await _mediator.Send(query);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteCampaignCommandRequest { CampaignId = id };
            var result = await _mediator.Send(command);
            if (result.IsDeleted)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCampaign([FromBody] UpdateCampaignCommandRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }
    }
}