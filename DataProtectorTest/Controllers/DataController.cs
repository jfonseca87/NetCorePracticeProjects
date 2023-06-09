using DataProtectorTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace DataProtectorTest.Controllers;

[ApiController]
[Route("data")]
public class DataController : ControllerBase
{
    public DataController()
    {

    }

    [HttpPost]
    public async Task<IActionResult> StartProcess()
    {
        using var accessData = new AccessData();
        await accessData.AddSequence();
        await accessData.AddSequence();
        await accessData.AddSequence();
        await accessData.AddSequence();
        await accessData.AddSequence();
        await accessData.AddSequence();
        await accessData.AddSequence();
        await accessData.AddSequence();


        return Ok();
    }
}