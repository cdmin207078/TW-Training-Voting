
using Microsoft.AspNetCore.Mvc;
using TW.Infrastructure.Core.Models.HttpAPI;

namespace TW.Infrastructure.ApsNetCore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ApiBaseController : ControllerBase
{
    [NonAction]
    protected IActionResult Success(string? message = null, object data = null)
    {
        return new JsonResult(new { code = HttpApiResponseCode.Success, message, data });
    }

    [NonAction]
    protected IActionResult Failure(HttpApiResponseCode? code = null, string? message = null, object data = null)
    {
        code = code.HasValue ? code.Value : HttpApiResponseCode.Failure;
        return new JsonResult(new { code , message, data });
    }
}