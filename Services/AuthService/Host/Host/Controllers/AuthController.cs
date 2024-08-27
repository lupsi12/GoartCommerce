using Application.Feature.Users.Commands.CreateUser;
using Application.Feature.Users.Commands.UpdateUser;
using Application.Feature.Users.DeleteUser;
using Application.Feature.Users.Queries.GetAllUsers;
using Application.Feature.Users.Queries.GetUserById;
using Enum;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Application.Auth;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthManager authManager) : ControllerBase
    {
        [HttpPost]
    public IActionResult Login([FromBody] AuthLoginRequest authLoginRequest)
    {
        var response = authManager.Login(authLoginRequest);

        if (string.IsNullOrWhiteSpace(response))
        {
            return Unauthorized();
        }

        return Ok(response);
    }
    }
}