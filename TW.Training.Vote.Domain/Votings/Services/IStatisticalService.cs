namespace TW.Training.Vote.Domain.Votings;

public interface IStatisticService
{
    Task<GetVotingStatisticOutput> GetVotingStatistic(GetVotingStatisticInput input);
    Task<GetVotingFortuneOutput> GenerateVotingFortune(GetVotingFortuneInput input);
}