using System.Net;
using Application.Messages.Dto.User.Response;
using Application.Messages.Query.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(UserResponseDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> Get()
    {
        var response = await mediator.Send(new UserQuery());

        if (response.Count() < 1)
            return NotFound();

        return Ok(response);
    }
}