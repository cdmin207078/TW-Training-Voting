namespace TW.Training.Vote.Domain.Votings;

public interface IVotingService
{
    Task Voting(CreateVotingInput input);
}