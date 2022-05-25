using TW.Infrastructure.Core.Primitives;

namespace TW.Training.Vote.Domain.Votings;

public interface IVotingRepository
{
    Task<int> GetVotingCount(MobilePhoneNumber mobilePhoneNumber, CodeNumber programmeCodeNumber);
    Task Voting(Voting voting);
    Task Greeting();
}