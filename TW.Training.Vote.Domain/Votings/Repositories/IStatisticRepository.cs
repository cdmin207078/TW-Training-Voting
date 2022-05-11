namespace TW.Training.Vote.Domain.Votings;

public interface IStatisticRepository
{
    Task<GetVotingStatisticOutput> GetVotingStatistic(GetVotingStatisticInput input);
    Task<GetVotingFortuneOutput> GenerateVotingFortune(GetVotingFortuneInput input);
}