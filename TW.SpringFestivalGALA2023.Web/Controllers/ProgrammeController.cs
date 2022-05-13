using Microsoft.AspNetCore.Mvc;
using TW.SpringFestivalGALA2023.Web.Services;

namespace TW.SpringFestivalGALA2023.Web.Controllers;

[ApiController]
[Route("api/programme")]
public class ProgrammeController : ControllerBase
{
    private readonly IProgrammeService _programmeService;

    public ProgrammeController(IProgrammeService programmeService)
    {
        _programmeService = programmeService;
    }
    
    [HttpGet("{programmeCode}")]
    public async Task<IActionResult> GetProgramme([FromRoute] string programmeCode)
    {
        return Ok();
    }
}