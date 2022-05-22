using System.Text.Json;
using Microsoft.Extensions.Options;
using RestSharp;
using TW.Infrastructure.Core.Components;
using TW.Infrastructure.Core.Components.HttpClients;
using TW.Infrastructure.Core.Components.TransientFalutProcess;
using TW.Infrastructure.Core.Exceptions;
using TW.Infrastructure.Core.Models.HttpAPI;
using TW.SpringFestivalGALA2023.Web.Infrastructures.VotingApi.Constracts.Request;
using TW.SpringFestivalGALA2023.Web.Infrastructures.VotingApi.Constracts.Response;
using TW.SpringFestivalGALA2023.Web.Models.Configures;

namespace TW.SpringFestivalGALA2023.Web.Services.VotingApi;

public class VotingApiProxy : IVotingApiProxy
{
    private readonly ILogger<VotingApiProxy> _logger;
    private readonly IHttpClientService _httpClient;
    private readonly IRetryProcessor _retryProcessor;
    private readonly VotingApiConfiguration _votingApiConfiguration;

    public VotingApiProxy(ILogger<VotingApiProxy> logger,
        IOptions<VotingApiConfiguration> votingApiConfigureOption,
        IHttpClientService httpClient,
        IRetryProcessor retryProcessor)
    {
        _logger = logger;
        _httpClient = httpClient;
        _retryProcessor = retryProcessor;
        _votingApiConfiguration = votingApiConfigureOption.Value;
    }

    private string GetUrl(string programmeCode, string pathCode)
    {
        var path = _votingApiConfiguration.EndPoints.FirstOrDefault(x => x.Code == pathCode)?.Path;
        return _votingApiConfiguration.BaseURL + string.Format(path, programmeCode);
    }

    public async Task SubmitVoting(string programmeCode, SubmitVotingRequest submitVoting)
    { 
        var url = GetUrl(programmeCode, "submit-voting");
        var response = await _httpClient.Post<HttpApiResponse>(url, submitVoting); 
        if (!response.Success) 
            throw new TWException(response.Message);
    }

    public async Task<GetProgrammeResponse> GetProgramme(string programmeCode)
    {
        // var response = await _retryProcessor.Execute(_GetProgramme(programmeCode), x => x?.code == (int)VotingApiResponseCode.Failure);
        // return response.data;
        
        var url = GetUrl(programmeCode, "get-programme");
        var response = await _httpClient.Get<HttpApiResponse<GetProgrammeResponse>>(url);
        if(!response.Success)
            throw new TWException(response.Message);

        return response.Data;
    }
    
    public async Task<GetProgrammeStatisticResponse> GetProgrammeStatistic(string programmeCode)
    {
        // var xx = (HttpApiResponse response) => response?.code == (int)HttpApiResponseCode.Failure;
        
        var url = GetUrl(programmeCode, "get-programme-statistic");
        var response = await _httpClient.Get<HttpApiResponse<GetProgrammeStatisticResponse>>(url);
        if (!response.Success)
            throw new TWException(response.Message);

        return response.Data;
    }

    public async Task<GetProgrammeFortuneResponse> GetProgrammeVotingFortune(string programmeCode)
    {
        var url = GetUrl(programmeCode, "get-programme-fortune");
        var response = await _httpClient.Get<HttpApiResponse<GetProgrammeFortuneResponse>>(url);
        if(!response.Success)
            throw new TWException(response.Message);

        return response.Data;
    }
}