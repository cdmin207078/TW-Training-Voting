using Microsoft.AspNetCore.Mvc;
using TW.SpringFestivalGALA2023.Web.Models.Contracts;
using TW.SpringFestivalGALA2023.Web.Services;

namespace TW.SpringFestivalGALA2023.Web.Controllers;

[ApiController]
[Route("api/voting")]
public class VotingController : ControllerBase
{
    private readonly IVotingService _votingService;

    public VotingController(IVotingService votingService)
    {
        _votingService = votingService;
    }
    
    [HttpPost("{programmeCode}")]
    public async Task<IActionResult> SubmitVoting([FromRoute] string programmeCode, SubmitVotingRequest request)
    {
        // var input = new SubmitVotingInput
        // {
        //     Name = request.UserName,
        //     MobilePhoneNumber = new MobilePhoneNumber(request.UserMobilePhoneNumber),
        //     ProgrammeCodeNumber = new CodeNumber(programmeCode),
        //     ProgrammeItemCodeNumbers = request.VotingCodeNumbers.Select(x => new CodeNumber(x)).ToList()
        // };
        //
        // await _votingService.Voting(input);

        return Ok("voting success");
    }
    
    [HttpPost("{programmeCode}/statistic")]
    public async Task<IActionResult> GetVotingStatistic([FromRoute] string programmeCode)
    {
        // var input = new SubmitVotingInput
        // {
        //     Name = request.UserName,
        //     MobilePhoneNumber = new MobilePhoneNumber(request.UserMobilePhoneNumber),
        //     ProgrammeCodeNumber = new CodeNumber(programmeCode),
        //     ProgrammeItemCodeNumbers = request.VotingCodeNumbers.Select(x => new CodeNumber(x)).ToList()
        // };
        //
        // await _votingService.Voting(input);

        return Ok("statistic");
    }
    
    
    [HttpPost("{programmeCode}/fortune")]
    public async Task<IActionResult> GetVotingFortune([FromRoute] string programmeCode)
    {
        // var input = new SubmitVotingInput
        // {
        //     Name = request.UserName,
        //     MobilePhoneNumber = new MobilePhoneNumber(request.UserMobilePhoneNumber),
        //     ProgrammeCodeNumber = new CodeNumber(programmeCode),
        //     ProgrammeItemCodeNumbers = request.VotingCodeNumbers.Select(x => new CodeNumber(x)).ToList()
        // };
        //
        // await _votingService.Voting(input);

        return Ok("fortune");
    }
}