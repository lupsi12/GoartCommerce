
using Application.Feature.Campaigns.Command.CreateCampaign;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}