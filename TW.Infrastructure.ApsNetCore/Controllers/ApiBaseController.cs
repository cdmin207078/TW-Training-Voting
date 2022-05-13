
using Microsoft.AspNetCore.Mvc;

namespace TW.Infrastructure.ApsNetCore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApiBaseController : ControllerBase
{
    [NonAction]
    protected IActionResult Success(string message = "", object data = null)
    {
        return new JsonResult(new { code = 200, message, data });
    }

    [NonAction]
    protected IActionResult Failure(int code = -1, string message = "", object data = null)
    {
        return new JsonResult(new { code, message, data });
    }
}