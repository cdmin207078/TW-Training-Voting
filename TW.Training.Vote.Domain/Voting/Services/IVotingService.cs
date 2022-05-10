using TW.Training.Vote.Domain.Voting;

namespace TW.Training.Vote.Domain.Voting;

public interface IVotingService
{
    Task Voting(CreateVotingInput input);
}