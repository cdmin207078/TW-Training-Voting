using TW.Training.Vote.Domain.Programmes;

namespace TW.Training.Vote.Domain.Votings;

public class VotingService : IVotingService
{
    private readonly IVotingRepository _votingRepository;
    private readonly IProgrammeRepository _programmeRepository;

    public VotingService(IVotingRepository votingRepository, IProgrammeRepository programmeRepository)
    {
        _votingRepository = votingRepository;
        _programmeRepository = programmeRepository;
    }
    
    public async Task Voting(SubmitVotingInput input)
    {
        var voting = new Voting(input, _programmeRepository, _votingRepository);

        var existVoting = await _votingRepository.GetVoting();

        if (existVoting is null)
            await _votingRepository.Voting(voting);
        else
            await _votingRepository.Greeting();
    }

    public async Task Submit()
    {
        var existSubmitVotingModel = _votingRepository.GetSubmitVotingModel(string.Empty);
        
        if(existSubmitVotingModel is null)
            await _votingRepository.Voting(null);
        else 
            await _votingRepository.Greeting();
    }
}