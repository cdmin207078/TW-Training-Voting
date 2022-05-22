using System.Text.Json;
using Microsoft.Extensions.Options;
using RestSharp;
using TW.Infrastructure.Core.Components;
using TW.Infrastructure.Core.Components.TransientFalutProcess;
using TW.Infrastructure.Core.Exceptions;
using TW.SpringFestivalGALA2023.Web.Infrastructures.VotingApi.Constracts.Request;
using TW.SpringFestivalGALA2023.Web.Infrastructures.VotingApi.Constracts.Response;
using TW.SpringFestivalGALA2023.Web.Models.Configures;
using TW.SpringFestivalGALA2023.Web.Services.VotingApi.Constracts.Constants;

namespace TW.SpringFestivalGALA2023.Web.Services.VotingApi;

public class VotingApiProxy : IVotingApiProxy
{
    private readonly ILogger<VotingApiProxy> _logger;
    private readonly IRetryProcessor _retryProcessor;
    private readonly RestClient _client;
    private readonly VotingApiConfiguration _votingApiConfiguration;

    public VotingApiProxy(ILogger<VotingApiProxy> logger,
        IOptions<VotingApiConfiguration> votingApiConfigureOption,
        IRetryProcessor retryProcessor)
    {
        _logger = logger;
        _retryProcessor = retryProcessor;
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

        var response = await _client.PostAsync<VotingApiResponse>(request);
        if (response?.code ==(int)VotingApiResponseCode.Failure)
            throw new TWException(response.message);
    }

    private async Task<VotingApiResponse<GetProgrammeResponse>> _GetProgramme(string programmeCode)
    {
        var request = new RestRequest(GetRequestPath(programmeCode, "get-programme"));
        var response = await _client.GetAsync<VotingApiResponse<GetProgrammeResponse>>(request);
        if (response?.code ==(int)VotingApiResponseCode.Failure)
            throw new TWException(response.message);
        
        return response;
    }

    public async Task<GetProgrammeResponse> GetProgramme(string programmeCode)
    {
        // var xx = await _faultProcessor
        //     .RetryForResult(
        //         SomeTask("name"),
        //         // () => Task.FromResult("asdasd"),
        //         // response => response.,
        //         response => true,
        //         3, "Getxxx"
        //     );
        // var result = await _faultProcessor.Execute(_GetProgramme(programmeCode), x => x.code == (int)VotingApiResponseCode.Failure);
        //
        
        var response = await _retryProcessor.Execute(_GetProgramme(programmeCode), x => x?.code == (int)VotingApiResponseCode.Failure);
        return response.data;
    }
    
    public async Task<GetProgrammeStatisticResponse> GetProgrammeStatistic(string programmeCode)
    {
        var request = new RestRequest(GetRequestPath(programmeCode, "get-programme-statistic"));
        var response = await _client.GetAsync<VotingApiResponse<GetProgrammeStatisticResponse>>(request);
        if (response?.code ==(int)VotingApiResponseCode.Failure)
            throw new TWException(response.message);
        
        return response?.data;
    }

    public async Task<GetProgrammeFortuneResponse> GetProgrammeVotingFortune(string programmeCode)
    {
        var request = new RestRequest(GetRequestPath(programmeCode, "get-programme-fortune"));
        var response = await _client.GetAsync<VotingApiResponse<GetProgrammeFortuneResponse>>(request);
        if (response?.code ==(int)VotingApiResponseCode.Failure)
            throw new TWException(response.message);

        return response?.data;
    }
}