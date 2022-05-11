using Microsoft.AspNetCore.Mvc;
using TW.Infrastructure.Core.Primitives;
using TW.Infrastructure.Domain.Models;
using TW.Training.Vote.Domain.Programmes;
using TW.Training.Vote.WebApi.Models.Programmes;

namespace TW.Training.Vote.WebApi.Controllers;

[ApiController]
[Route("api/programmes")]
public class ProgrammeController : ControllerBase
{
    private readonly IProgrammeService _programmeService;

    public ProgrammeController(IProgrammeService programmeService)
    {
        _programmeService = programmeService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProgrammeRequest request)
    {
        return Ok();
    }

    [HttpDelete("{programmeCode}")]
    public async Task<IActionResult> Delete([FromRoute] string programmeCode)
    {
        return Ok();
    }
    
    [HttpPut("{programmeCode}")]
    public async Task<IActionResult> Update([FromRoute] string programmeCode, UpdateProgrammeRequest request)
    {
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] string? code,
        [FromQuery] DateTime? creationDateTimeBegin,
        [FromQuery] DateTime? creationDateTimeEnd,
        [FromQuery] int pageSize,
        [FromQuery] int currentPage)
    {
        var creationTimeSpan = creationDateTimeBegin.HasValue || creationDateTimeEnd.HasValue ? new DateTimeSpan(creationDateTimeBegin, creationDateTimeEnd) : null;
        var codeNumber = string.IsNullOrWhiteSpace(code) ? null : new CodeNumber(code);
        
        var input = new GetProgrammesInput(codeNumber, creationTimeSpan, pageSize, currentPage);
        var result = await _programmeService.GetProgrammes(input);
        
        return Ok(result);
    }
    
    [HttpGet("{programmeCode}")]
    public async Task<IActionResult> Get([FromRoute] string programmeCode)
    {
        return Ok(programmeCode);
    }
}