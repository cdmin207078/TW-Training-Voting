using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TW.SpringFestivalGALA2023.Web.Models.Configures;
using TW.SpringFestivalGALA2023.Web.Models.Contracts;
using TW.SpringFestivalGALA2023.Web.Models.Contracts.Request;
using TW.SpringFestivalGALA2023.Web.Services;

namespace TW.SpringFestivalGALA2023.Web.Controllers;

[ApiController]
[Route("api/voting")]
public class VotingController : ControllerBase
{
    private readonly string _programmeCode;
    private readonly IVotingService _votingService;

    public VotingController(IOptions<VotingApiConfiguration> votingApiConfigurationOptions, IVotingService votingService)
    {
        _programmeCode = votingApiConfigurationOptions.Value.ProgrammeCode;
        _votingService = votingService;
    }
    
    [HttpPost]
    public async Task<IActionResult> SubmitVoting(SubmitVotingRequest request)
    { 
        await _votingService.SubmitVoting(_programmeCode, request);
        return Ok();
    }
    
    [HttpGet("statistic")]
    public async Task<IActionResult> GetVotingStatistic()
    {
        var response = await _votingService.GetProgrammeStatistic(_programmeCode);
        return Ok(response);
    }
    
    [HttpGet("fortune")]
    public async Task<IActionResult> GetVotingFortune()
    {
        var response = await _votingService.GetProgrammeFortune(_programmeCode);
        return Ok(response);
    }
}