using Microsoft.EntityFrameworkCore;
using TW.Infrastructure.Core.Primitives;
using TW.Training.Vote.Domain.Votings;

namespace TW.Training.Vote.Infrastructure.Votings;

public class VotingRepository : IVotingRepository
{
    private readonly VoteDbContext _ctx;

    public VotingRepository(VoteDbContext dbContext)
    {
        _ctx = dbContext;
    }
    
    public Task<int> GetVotingCount(MobilePhoneNumber mobilePhoneNumber, CodeNumber programmeCodeNumber)
    {
        return _ctx.Votings
            .Where(x => x.MobilePhoneNumber == mobilePhoneNumber.Value &&
                        x.ProgrammeItem.Programme.Code == programmeCodeNumber.Value)
            .CountAsync();
    }

    public Task Voting(Voting voting)
    {
        var programmeItmes = voting.VoteItems
            .Select(x => new Programmes.PO.ProgrammeItem { Id = x.Id.Value })
            .Distinct().ToList();

        var votes = voting.VoteItems.Select(x => new PO.Voting
        {
            CreatorId = x.CreatorId.Value,
            CreationTime = x.CreationTime,
            Name = voting.Name,
            MobilePhoneNumber = voting.MobilePhoneNumber.Value,
            ProgrammeItem = programmeItmes.FirstOrDefault(d => d.Id == x.Id.Value)
        });
        
        _ctx.Votings.AttachRange(votes);
        Console.WriteLine(_ctx.ChangeTracker.DebugView.LongView);

        return _ctx.SaveChangesAsync();
    }

    public Task Greeting()
    {
        throw new NotImplementedException();
    }
}