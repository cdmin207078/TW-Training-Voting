using Microsoft.AspNetCore.Mvc;
using TW.Infrastructure.Core.Components;
using TW.Infrastructure.Core.Primitives;
using TW.Infrastructure.Domain.Models;
using TW.Infrastructure.Domain.WebWorkContext;
using TW.Training.Vote.Domain.Programmes;
using TW.Training.Vote.WebApi.Models.Programmes;

namespace TW.Training.Vote.WebApi.Controllers;

[ApiController]
[Route("api/programmes")]
public class ProgrammeController : ControllerBase
{
    private readonly IObjectMapperComponent _mapper;
    private readonly IProgrammeService _programmeService;
    private readonly WebWorkContext _webWorkContext;

    public ProgrammeController(IProgrammeService programmeService,IObjectMapperComponent mapper, WebWorkContext webWorkContext)
    {
        _programmeService = programmeService;
        _mapper = mapper;
        _webWorkContext = webWorkContext;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProgrammeRequest request)
    {
        // var input = new CreateProgrammeInput
        // {
        //     Code = new CodeNumber(request.Code),
        //     Title = request.Title,
        //     Description = request.Description,
        //     PerPersonMaxVotingCount = request.PerPersonMaxVotingCount,
        //     CreatorId = _webWorkContext.User?.Id ?? new Id<int>(1),
        //     Items = request.Items?.Select(x => new CreateProgrammeInput.Item
        //     {
        //         Code = new CodeNumber(x.Code),
        //         Title = x.Title,
        //         Description = x.Description,
        //         Order = x.Order
        //     }).ToList()
        // };
        
        var input = _mapper.Map<CreateProgrammeInput>(request);
        input.CreatorId = _webWorkContext.User?.Id ?? new Id<int>(1);
       
        await _programmeService.Create(input);
        
        return Ok("create success");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var input = new DeleteProgrammeInput
        {
            Id = new Id<int>(id)
        };
        
        await _programmeService.Delete(input);
        
        return Ok("delete success");
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, UpdateProgrammeRequest request)
    {
        var input = _mapper.Map<UpdateProgrammeInput>(request);
        input.Id = new Id<int>(id);
        input.LastModifierId = _webWorkContext.User?.Id ?? new Id<int>(1);
        
        await _programmeService.Update(input);
        
        return Ok("update success");
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
    
    [HttpGet("d-{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var result = await _programmeService.GetProgramme(new Id<int>(id));
        return Ok(result);
    }
    
    [HttpGet("c-{programmeCode}")]
    public async Task<IActionResult> Get([FromRoute] string programmeCode)
    {
        var result = await _programmeService.GetProgramme(new CodeNumber(programmeCode));
        return Ok(result);
    }
}