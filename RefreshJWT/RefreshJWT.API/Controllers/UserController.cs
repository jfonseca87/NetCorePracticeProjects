using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RefreshJWT.API.Models;
using RefreshJWT.API.Services;

namespace RefreshJWT.API.Controllers;

[ApiController]
[Route("api/user")]
[Authorize]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsersAsync() =>
        Ok(await _userService.GetUsersAsync());

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> LoginUserAsync([FromBody] AuthenticateRequest authenticate)
    {
        AuthenticateReponse response = await _userService.LoginAsync(authenticate, GetIpAddress());
        
        if (response.Id == 0)
        {
            return Unauthorized();
        }

        SetTokenCookie(response.RefreshToken);

        return Ok(response);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshTokenAsync()
    {
        string refreshToken = Request.Cookies["refreshToken"];
        AuthenticateReponse response = await _userService.RefreshTokenAsync(refreshToken, GetIpAddress());
        SetTokenCookie(response.RefreshToken);
        return Ok(response);
    }

    [HttpGet("{userId}/refresh-tokens")]
    public async Task<IActionResult> GetRefreshTokenByUserId(int userId)
    {
        User user = await _userService.GetUserByIdAsync(userId);
        return Ok(user.RefreshTokens);
    }

    private void SetTokenCookie(string refreshToken)
    {
        CookieOptions options = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7)
        };

        Response.Cookies.Append("refreshToken", refreshToken, options);
    }

    private string GetIpAddress() =>
        Request.Headers.ContainsKey("X-Forwarded-For") 
            ? Request.Headers["X-Forwarded-For"]
            : HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
}