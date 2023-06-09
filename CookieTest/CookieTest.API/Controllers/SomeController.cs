using CookieTest.API.Utils.CustomActionFilters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CookieTest.API.Controllers
{
    [ApiController]
    [Authorize]
    [CompareSession()]
    [Route("api/some")]
    public class SomeController : ControllerBase
    {
        public SomeController()
        {}

        [HttpGet]
        public IActionResult GetSomeData()
        {
            string userName = HttpContext.User.Claims.Where(x => x.Type == "UserName").Select(x => x.Value).FirstOrDefault();
            string sessionId = HttpContext.User.Claims.Where(x => x.Type == "SessionId").Select(x => x.Value).FirstOrDefault();

            return Ok(new { Message = $"some data returned... requested by the user: {userName} with session id: {sessionId}" });
        }
    }
}
