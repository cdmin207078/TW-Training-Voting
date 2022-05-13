using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TW.SpringFestivalGALA2023.Web.Models.Configures;
using TW.SpringFestivalGALA2023.Web.Services;

namespace TW.SpringFestivalGALA2023.Web.Controllers;

[ApiController]
[Route("api/programme")]
public class ProgrammeController : ControllerBase
{
    private readonly string _programmeCode;
    private readonly IProgrammeService _programmeService;

    
    public ProgrammeController(IOptions<VotingApiConfiguration> votingApiConfigurationOptions, IProgrammeService programmeService)
    {
        _programmeCode = votingApiConfigurationOptions.Value.ProgrammeCode;
        _programmeService = programmeService;
    }
    
    public async Task<IActionResult> GetProgramme()
    {
        var response = await _programmeService.GetProgramme(_programmeCode);
        return Ok(response);
    }
}