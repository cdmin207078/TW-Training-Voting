using Microsoft.AspNetCore.Mvc;
using TW.Training.Vote.Domain.Votings;
using TW.Training.Vote.WebApi.Models.Votings;

namespace TW.Training.Vote.WebApi.Controllers;

[ApiController]
[Route("api/voting")]
public class VotingController : ControllerBase
{
    private readonly IVotingService _votingService;
    private readonly IStatisticService _statisticService;

    public VotingController(IVotingService votingService, IStatisticService statisticService)
    {
        _votingService = votingService;
        _statisticService = statisticService;
    }
    
    [HttpPost("{programmeCode}")]
    public async Task<IActionResult> SubmitVoting(string programmeCode, SubmitVotingRequest request)
    {
        return Ok("welcome");
    }
    
    [HttpGet("{programmeCode}/statistic")]
    public async Task<IActionResult> GetVotingStatistic(string programmeCode)
    {
        return Ok("welcome");
    }

    [HttpGet("{programmeCode}/fortune")]
    public async Task<IActionResult> GetVotingFortune(string programmeCode)
    {
        return Ok("welcome");
    }
}