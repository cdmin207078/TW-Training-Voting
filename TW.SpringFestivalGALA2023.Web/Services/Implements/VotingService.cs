using TW.SpringFestivalGALA2023.Web.Infrastructures.VotingApi.Constracts.Request;
using TW.SpringFestivalGALA2023.Web.Infrastructures.VotingApi.Constracts.Response;
using TW.SpringFestivalGALA2023.Web.Services.VotingApi;

namespace TW.SpringFestivalGALA2023.Web.Services;

public class VotingService : IVotingService
{
    private readonly IVotingApiProxy _votingApiProxy;

    public VotingService(IVotingApiProxy votingApiProxy)
    {
        _votingApiProxy = votingApiProxy;
    }
    
    public Task SubmitVoting(string programmeCode, SubmitVotingRequest request)
    {
        return _votingApiProxy.SubmitVoting(programmeCode, request);
    }

    public Task<GetProgrammeStatisticResponse> GetProgrammeStatistic(string programmeCode)
    {
        return _votingApiProxy.GetProgrammeStatistic(programmeCode);
    }

    public Task<GetProgrammeFortuneResponse> GetProgrammeFortune(string programmeCode)
    {
        return _votingApiProxy.GetProgrammeVotingFortune(programmeCode);
    }
}