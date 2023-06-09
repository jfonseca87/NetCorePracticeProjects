using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RefreshJWT.API.Controllers;

[ApiController]
[Route("api/test")]
[Authorize]
public class TestControler: ControllerBase
{

    [HttpGet]
    public IActionResult GetSomeData()
    {
        return Ok("This is a response from Test Controller");
    }
}