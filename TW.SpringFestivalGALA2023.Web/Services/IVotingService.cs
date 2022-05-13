using TW.SpringFestivalGALA2023.Web.Models.Contracts.Request;
using TW.SpringFestivalGALA2023.Web.Models.Contracts.Response;

namespace TW.SpringFestivalGALA2023.Web.Services;

public interface IVotingService
{
    Task SubmitVoting(string programmeCode, SubmitVotingRequest request);
    Task<GetProgrammeStatisticResponse> GetProgrammeStatistic(string programmeCode);
    Task<GetProgrammeFortuneResponse> GetProgrammeFortune(string programmeCode);
}