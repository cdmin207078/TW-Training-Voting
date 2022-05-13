using TW.SpringFestivalGALA2023.Web.Models.Contracts.Response;

namespace TW.SpringFestivalGALA2023.Web.Services;

public interface IProgrammeService
{
    Task<GetProgrammeResponse> GetProgramme(string programmeCode);
}