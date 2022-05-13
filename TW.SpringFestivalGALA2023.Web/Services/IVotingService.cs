using TW.SpringFestivalGALA2023.Web.Infrastructures.VotingApi.Constracts.Request;
using TW.SpringFestivalGALA2023.Web.Infrastructures.VotingApi.Constracts.Response;

namespace TW.SpringFestivalGALA2023.Web.Services;

public interface IVotingService
{
    Task SubmitVoting(string programmeCode, SubmitVotingRequest request);
    Task<GetProgrammeStatisticResponse> GetProgrammeStatistic(string programmeCode);
    Task<GetProgrammeFortuneResponse> GetProgrammeFortune(string programmeCode);
}