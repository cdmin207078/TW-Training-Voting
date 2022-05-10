namespace TW.Training.Vote.Domain.Voting;

public interface IStatisticRepository
{
    Task<GetVotingStatisticOutput> GetVotingStatistic(GetVotingStatisticInput input);
    Task<GetVotingFortuneOutput> GenerateVotingFortune(GetVotingFortuneInput input);
}