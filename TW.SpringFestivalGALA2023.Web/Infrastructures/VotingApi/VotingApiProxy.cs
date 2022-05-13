using System.Net;
using Microsoft.Extensions.Options;
using RestSharp;
using RestSharp.Authenticators;
using TW.SpringFestivalGALA2023.Web.Infrastructures.VotingApi.Constracts.Request;
using TW.SpringFestivalGALA2023.Web.Infrastructures.VotingApi.Constracts.Response;
using TW.SpringFestivalGALA2023.Web.Models.Configures;
using TW.SpringFestivalGALA2023.Web.Services.VotingApi.Constracts.Constants;

namespace TW.SpringFestivalGALA2023.Web.Services.VotingApi;

public class VotingApiProxy : IVotingApiProxy
{
    private readonly ILogger<VotingApiProxy> _logger;
    private readonly RestClient _client;
    private readonly VotingApiConfiguration _votingApiConfiguration;

    public VotingApiProxy(ILogger<VotingApiProxy> logger, IOptions<VotingApiConfiguration> votingApiConfigureOption)
    {
        _logger = logger;
        _votingApiConfiguration = votingApiConfigureOption.Value;
        _client = new RestClient(_votingApiConfiguration.BaseURL);
    }

    private string GetRequestPath(string programmeCode, string pathCode)
    {
        var path = _votingApiConfiguration.EndPoints.FirstOrDefault(x => x.Code == pathCode)?.Path;
        path = string.Format(path, programmeCode);

        _logger.LogInformation($"Path: {path}");
        return path;
    }

    public async Task SubmitVoting(string programmeCode, SubmitVotingRequest submitVoting)
    {
        var request = new RestRequest(GetRequestPath(programmeCode, "submit-voting"));
        request.AddJsonBody(submitVoting);

        var response = await _client.ExecuteGetAsync<VotingApiResponse>(request);
        if (response?.Data?.code ==(int)VotingApiResponseCode.Failure)
            throw new ArgumentException(response.Data.message);
    }

    public async Task<GetProgrammeResponse> GetProgramme(string programmeCode)
    {
        var request = new RestRequest(GetRequestPath(programmeCode, "get-programme"));
        var response = await _client.ExecuteGetAsync<VotingApiResponse<GetProgrammeResponse>>(request);
        if (response?.Data?.code ==(int)VotingApiResponseCode.Failure)
            throw new ArgumentException(response.Data.message);

        return response?.Data?.data;
    }
    
    public async Task<GetProgrammeStatisticResponse> GetProgrammeStatistic(string programmeCode)
    {
        var request = new RestRequest(GetRequestPath(programmeCode, "get-programme-statistic"));
        var response = await _client.ExecuteGetAsync<VotingApiResponse<GetProgrammeStatisticResponse>>(request);
        if (response?.Data?.code ==(int)VotingApiResponseCode.Failure)
            throw new ArgumentException(response.Data.message);

        return response?.Data?.data;
    }

    public async Task<GetProgrammeFortuneResponse> GetProgrammeVotingFortune(string programmeCode)
    {
        var request = new RestRequest(GetRequestPath(programmeCode, "get-programme-fortune"));
        var response = await _client.ExecuteGetAsync<VotingApiResponse<GetProgrammeFortuneResponse>>(request);
        if (response?.Data?.code ==(int)VotingApiResponseCode.Failure)
            throw new ArgumentException(response.Data.message);

        return response?.Data?.data;
    }
}