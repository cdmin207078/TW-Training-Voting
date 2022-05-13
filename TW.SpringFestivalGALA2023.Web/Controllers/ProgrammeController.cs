using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TW.Infrastructure.ApsNetCore.Controllers;
using TW.SpringFestivalGALA2023.Web.Models.Configures;
using TW.SpringFestivalGALA2023.Web.Services;

namespace TW.SpringFestivalGALA2023.Web.Controllers;

public class ProgrammeController : ApiBaseController
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
        return Success(data: response);
    }
}