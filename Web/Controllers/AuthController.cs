using Application.BusinessLogic.Auth.Commands;
using Application.BusinessLogic.Auth.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(ISender sender) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto regInfo)
    {
        Dictionary<string, string> tokens = await sender.Send(new RegisterCommand(regInfo));
        HttpContext.Response.Cookies.Append("accessToken",  tokens["accessToken"]);
        HttpContext.Response.Cookies.Append("refreshToken", tokens["refreshToken"]);
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserCredentialsDto loginInfo)
    {
        Dictionary<string, string> tokens = await sender.Send(new LoginCommand(loginInfo));
        HttpContext.Response.Cookies.Append("accessToken",  tokens["accessToken"]);
        HttpContext.Response.Cookies.Append("refreshToken", tokens["refreshToken"]);
        return Ok();
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await sender.Send(new LogoutCommand(HttpContext.Request.Cookies["refreshToken"]));
        return Ok();
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        Dictionary<string, string> tokens = await sender.Send(new RefreshTokensCommand(HttpContext.Request.Cookies["refreshToken"]));
        HttpContext.Response.Cookies.Append("accessToken",  tokens["accessToken"]);
        HttpContext.Response.Cookies.Append("refreshToken", tokens["refreshToken"]);
        return Ok();
    }
}