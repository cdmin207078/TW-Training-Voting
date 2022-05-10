namespace TW.Training.Vote.Domain.Voting;

public interface IStatisticService
{
    Task<GetVotingStatisticOutput> GetVotingStatistic(GetVotingStatisticInput input);
    Task<GetVotingFortuneOutput> GenerateVotingFortune(GetVotingFortuneInput input);
}