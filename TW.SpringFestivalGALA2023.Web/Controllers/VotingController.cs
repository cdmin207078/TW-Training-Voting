using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TW.Infrastructure.ApsNetCore.Controllers;
using TW.SpringFestivalGALA2023.Web.Infrastructures.VotingApi.Constracts.Request;
using TW.SpringFestivalGALA2023.Web.Models.Configures;
using TW.SpringFestivalGALA2023.Web.Services;

namespace TW.SpringFestivalGALA2023.Web.Controllers;

public class VotingController : ApiBaseController
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
        return Success("voting succcess");
    }
    
    [HttpGet("statistic")]
    public async Task<IActionResult> GetVotingStatistic()
    {
        var response = await _votingService.GetProgrammeStatistic(_programmeCode);
        return Success(data: response);
    }
    
    [HttpGet("fortune")]
    public async Task<IActionResult> GetVotingFortune()
    {
        var response = await _votingService.GetProgrammeFortune(_programmeCode);
        return Success(data: response);
    }
}