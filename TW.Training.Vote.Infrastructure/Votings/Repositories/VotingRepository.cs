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
        foreach (var item in voting.VoteItems)
        {
            var vote = new PO.Voting
            {
                CreatorId = item.CreatorId.Value,
                CreationTime = item.CreationTime,
                Name = voting.Name,
                MobilePhoneNumber = voting.MobilePhoneNumber.Value,
                ProgrammeItem = new Programmes.PO.ProgrammeItem{ Id = item.Id.Value }
            };
            
            _ctx.Attach(vote);
        }

        return _ctx.SaveChangesAsync();
    }
}