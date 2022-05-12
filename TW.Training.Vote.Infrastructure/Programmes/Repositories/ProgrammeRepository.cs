using Microsoft.EntityFrameworkCore;
using TW.Infrastructure.Core.Components;
using TW.Infrastructure.Core.Primitives;
using TW.Infrastructure.EFCore.Repository;
using TW.Training.Vote.Domain.Programmes;

namespace TW.Training.Vote.Infrastructure.Programmes;

public class ProgrammeRepository : EntityframeworkRepositoryAbstract, IProgrammeRepository
{
    private readonly VoteDbContext _ctx;
    private readonly IObjectMapperComponent _mapper;

    public ProgrammeRepository(VoteDbContext dbContext, IObjectMapperComponent mapper)
        : base(dbContext)
    {
        _ctx = dbContext;
        _mapper = mapper;
    }
    
    public Task Create(Programme programme)
    {
        var programmePO = _mapper.Map<PO.Programme>(programme);
        
        _ctx.Attach(programmePO);
        
        return _ctx.SaveChangesAsync();
    }

    public async Task Update(Programme programme)
    {
        var source = await _ctx.Programmes
                               .Include(x => x.ProgrammeItems)
                               .AsSplitQuery()
                               .FirstOrDefaultAsync(x => x.Id == programme.Id.Value);
        
        var destination = _mapper.Map<PO.Programme>(programme);

        Update(source, destination);
        Update(source.ProgrammeItems, destination.ProgrammeItems);
        
        await SaveChanges();
    }

    public async Task Delete(DeleteProgrammeInput input)
    {
        var programme = await _ctx.Programmes
            .Include(x => x.ProgrammeItems)
            .AsSplitQuery()
            .Where(x => x.Id == input.Id.Value)
            .Select(x => new PO.Programme
            {
                Id = x.Id,
                ProgrammeItems = x.ProgrammeItems.Select(i => new PO.ProgrammeItem { Id = i.Id }).ToList()
            })
            .FirstOrDefaultAsync();
        
        _ctx.Programmes.Remove(programme);
        
        await _ctx.SaveChangesAsync();
    }

    public Task<bool> IsExists(Id<int> id)
    {
        return _ctx.Programmes.AnyAsync(x => x.Id == id.Value);
    }

    public Task<bool> IsExists(CodeNumber codeNumber)
    {
        return _ctx.Programmes.AnyAsync(x => x.Code == codeNumber.Value);
    }

    public async Task<Programme> Get(Id<int> id)
    {
        var programme = await _ctx.Programmes
            .Include(x => x.ProgrammeItems)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Id == id.Value);

        return _mapper.Map<Programme>(programme);
    }

    public async Task<Programme> Get(CodeNumber codeNumber)
    {
        var programme = await _ctx.Programmes
            .Include(x => x.ProgrammeItems)
            .AsSplitQuery()
            .FirstOrDefaultAsync(x => x.Code == codeNumber.Value);

        return _mapper.Map<Programme>(programme);
    }

    public async Task<GetProgrammesOutput> Get(GetProgrammesInput input)
    {
        var query = _ctx.Programmes
            .WhereIf(input.CodeNumber.IsNotNull(), x => x.Code.Contains(input.CodeNumber.Value))
            .WhereIf(input.CreationDateTimeSpan.IsNotNull(), x => input.CreationDateTimeSpan.Beginning <= x.CreationTime && x.CreationTime <= input.CreationDateTimeSpan.Ending);
        
        var (count, items) = await QueryPagination(query.Select(x => new GetProgrammesOutput.Item
        {
            Id = x.Id,
            Code = x.Code,
            Title = x.Title,
            Description = x.Description,
            CreationTime = x.CreationTime,
            PersonalMaxVotingCount = x.PersonalMaxVotingCount
        }), input.PageSize, input.CurrentPage);

        return new GetProgrammesOutput(input.PageSize, input.CurrentPage, count, items);
    }
}