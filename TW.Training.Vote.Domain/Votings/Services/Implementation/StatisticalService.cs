namespace TW.Training.Vote.Domain.Votings;

public class StatisticService : IStatisticService
{
    private readonly IStatisticRepository _statisticRepository;

    public StatisticService(IStatisticRepository statisticRepository)
    {
        _statisticRepository = statisticRepository;
    }
    
    public Task<GetVotingStatisticOutput> GetVotingStatistic(GetVotingStatisticInput input)
    {
        if (input == null) 
            throw new ArgumentNullException(nameof(input));

        return _statisticRepository.GetVotingStatistic(input);
    }

    public Task<GetVotingFortuneOutput> GenerateVotingFortune(GetVotingFortuneInput input)
    {
        if (input == null) 
            throw new ArgumentNullException(nameof(input));
        
        return _statisticRepository.GenerateVotingFortune(input);
    }
}