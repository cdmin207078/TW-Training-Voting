using TW.Infrastructure.Core.Primitives;

namespace TW.Training.Vote.Domain.Votings;

public interface IVotingRepository
{
    Task<int> GetVotingCount(MobilePhoneNumber mobilePhoneNumber, CodeNumber programmeCodeNumber);
    Task Voting(Voting voting);

    Task<Voting> GetVoting();
    Task Greeting();

    ISubmitVotingModel GetSubmitVotingModel(string name);
}

public interface ISubmitVotingModel
{
    
}

public class SubmitVotingModel : ISubmitVotingModel
{
    
}