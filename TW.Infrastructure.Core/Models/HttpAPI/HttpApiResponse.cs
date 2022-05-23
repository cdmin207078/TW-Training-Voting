namespace TW.Infrastructure.Core.Models.HttpAPI;

public class HttpApiResponse
{
    public HttpApiResponseCode Code { get; set; }
    public string? Message { get; set; }

    public bool Success
    {
        get => HttpApiResponseCode.Success.Equals(Code);
    }
}

public class HttpApiResponse<T> : HttpApiResponse
{
    public T Data { get; set; }
}