using TW.Infrastructure.Domain.Primitives;

namespace TW.Training.Vote.Domain.Voting;

public interface IVotingRepository
{
    Task<int> GetVotingCount(MobilePhoneNumber mobilePhoneNumber, Id<int> programmeId);
    Task Voting(Voting voting);
}