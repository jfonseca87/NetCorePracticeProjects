using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace DataProtectorTest.Controllers;

[ApiController]
[Route("test")]
public class TestController : ControllerBase
{
    private readonly IDataProtectionProvider _idp;

    public TestController(IDataProtectionProvider idp)
    {
        _idp = idp;
    }

    [HttpPost("set")]
    public IActionResult SetCookie()
    {
        var protector = _idp.CreateProtector("cookie");
        string user = "jorgefon";
        Response.Cookies.Append("user", protector.Protect(user));

        return Ok();
    }

    [HttpGet("get")]
    public IActionResult GetCookie()
    {
        var protector = _idp.CreateProtector("cookie");
        string cookie = Request.Cookies["user"];
        string user = protector.Unprotect(cookie);
        return Ok(user);
    }
}