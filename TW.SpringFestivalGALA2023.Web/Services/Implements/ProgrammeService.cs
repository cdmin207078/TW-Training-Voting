using TW.SpringFestivalGALA2023.Web.Infrastructures.VotingApi.Constracts.Response;
using TW.SpringFestivalGALA2023.Web.Services.VotingApi;

namespace TW.SpringFestivalGALA2023.Web.Services;

public class ProgrammeService : IProgrammeService
{
    private readonly IVotingApiProxy _votingApiProxy;

    public ProgrammeService(IVotingApiProxy votingApiProxy)
    {
        _votingApiProxy = votingApiProxy;
    }
    
    public Task<GetProgrammeResponse> GetProgramme(string programmeCode)
    {
        return _votingApiProxy.GetProgramme(programmeCode);
    }
}