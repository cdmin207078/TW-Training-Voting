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

    public async Task<GetVotingFortuneOutput> GenerateVotingFortune(GetVotingFortuneInput input)
    {
        var top3VotingItmes = await _ctx.Votings
            .Where(x => x.ProgrammeItem.Programme.Code == input.ProgrammeCodeNumber.Value)
            .GroupBy(x => x.ProgrammeItem.Code)
            .OrderByDescending(x => x.Count())
            .Take(3)
            .Select(x => x.FirstOrDefault().ProgrammeItem.Code)
            .ToListAsync();

        var top3fortuner = await _ctx.Votings
            .Where(x => x.ProgrammeItem.Programme.Code == input.ProgrammeCodeNumber.Value && top3VotingItmes.Contains(x.ProgrammeItem.Code))
            .Select(x => x.MobilePhoneNumber)
            .Distinct()
            .OrderBy(x => EF.Functions.Random())
            .Take(3)
            .ToListAsync();
        
        var result = new GetVotingFortuneOutput
        {
            FortunerMobilePhoneNumber = top3fortuner
        };

        return result;
    }
}