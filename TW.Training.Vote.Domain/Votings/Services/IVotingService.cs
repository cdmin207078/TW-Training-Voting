namespace TW.Training.Vote.Domain.Votings;

public interface IVotingService
{
    Task Voting(SubmitVotingInput input);

    Task<bool> ChoiceRightWay(string flag);
}