using TW.SpringFestivalGALA2023.Web.Models.Contracts.Request;
using TW.SpringFestivalGALA2023.Web.Models.Contracts.Response;

namespace TW.SpringFestivalGALA2023.Web.Services.VotingApi;

public interface IVotingApiProxy
{
    Task SubmitVoting(string programmeCode, SubmitVotingRequest request);
    Task<GetProgrammeResponse> GetProgramme(string programmeCode);
    Task<GetProgrammeStatisticResponse> GetProgrammeStatistic(string programmeCode);
    Task<GetProgrammeFortuneResponse> GetProgrammeVotingFortune(string programmeCode);
}