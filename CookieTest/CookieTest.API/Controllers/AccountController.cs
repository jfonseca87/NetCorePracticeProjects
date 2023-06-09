using CookieTest.API.Business;
using CookieTest.API.Models;
using CookieTest.API.Utils.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace CookieTest.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AccountController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult LogIn([FromBody] User user)
        {
            if (!user.Name.Equals("admin") && !user.Password.Equals("Abc.123456"))
            {
                return Unauthorized();
            }

            user.Id = 1;
            user.Email = "admin@test.com";

            Guid sessionId = Guid.NewGuid();
            string key = _configuration["JsonWebTokenKey:IssuerSigningKey"];
            string token = JwtHandler.CreateJwtToken(key, user, sessionId);

            // Create and set a cookie with the session id
            this.SetCookie("SessionId", sessionId.ToString());

            return Ok( new { Token = token });
        }
    }
}
