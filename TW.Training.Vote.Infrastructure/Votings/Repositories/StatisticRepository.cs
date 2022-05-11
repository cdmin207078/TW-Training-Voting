using Microsoft.EntityFrameworkCore;
using TW.Training.Vote.Domain.Votings;

namespace TW.Training.Vote.Infrastructure.Votings;

public class StatisticRepository : IStatisticRepository
{
    private readonly VoteDbContext _ctx;

    public StatisticRepository(VoteDbContext dbContext)
    {
        _ctx = dbContext;
    }
    
    public async Task<GetVotingStatisticOutput> GetVotingStatistic(GetVotingStatisticInput input)
    {
        var items = await _ctx.ProgrammeItems.Where(x => x.Programme.Code == input.ProgrammeCodeNumber.Value)
            .Select(x => new GetVotingStatisticOutput.Item
            {
                ProgrammeItemCode = x.Code,
                Title = x.Title,
                VotingCount = x.Votings.Count
            }).ToListAsync();


        return new GetVotingStatisticOutput { Items = items };
    }

    public Task<GetVotingFortuneOutput> GenerateVotingFortune(GetVotingFortuneInput input)
    {
        throw new NotImplementedException();
    }
}