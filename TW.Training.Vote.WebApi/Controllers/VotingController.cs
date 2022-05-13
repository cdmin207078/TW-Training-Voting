using Microsoft.AspNetCore.Mvc;
using TW.Infrastructure.ApsNetCore.Controllers;
using TW.Infrastructure.Core.Primitives;
using TW.Training.Vote.Domain.Votings;
using TW.Training.Vote.WebApi.Models.Votings;

namespace TW.Training.Vote.WebApi.Controllers;

[Route("api/voting")]
public class VotingController : ApiBaseController
{
    private readonly IVotingService _votingService;
    private readonly IStatisticService _statisticService;

    public VotingController(IVotingService votingService, IStatisticService statisticService)
    {
        _votingService = votingService;
        _statisticService = statisticService;
    }

    [HttpPost("{programmeCode}")]
    public async Task<IActionResult> SubmitVoting([FromRoute] string programmeCode, SubmitVotingRequest request)
    {
        var input = new SubmitVotingInput
        {
            Name = request.UserName,
            MobilePhoneNumber = new MobilePhoneNumber(request.UserMobilePhoneNumber),
            ProgrammeCodeNumber = new CodeNumber(programmeCode),
            ProgrammeItemCodeNumbers = request.VotingCodeNumbers.Select(x => new CodeNumber(x)).ToList()
        };

        await _votingService.Voting(input);

        return Success("voting success");
    }

    [HttpGet("{programmeCode}/statistic")]
    public async Task<IActionResult> GetVotingStatistic([FromRoute] string programmeCode)
    {
        var input = new GetVotingStatisticInput(new CodeNumber(programmeCode));
        var result = await _statisticService.GetVotingStatistic(input);
        
        return Success(data: result);
    }

    [HttpGet("{programmeCode}/fortune")]
    public async Task<IActionResult> GetVotingFortune([FromRoute] string programmeCode)
    {
        var input = new GetVotingFortuneInput(new CodeNumber(programmeCode));
        var result = await _statisticService.GenerateVotingFortune(input);
        
        return Success(data: result);
    }
}