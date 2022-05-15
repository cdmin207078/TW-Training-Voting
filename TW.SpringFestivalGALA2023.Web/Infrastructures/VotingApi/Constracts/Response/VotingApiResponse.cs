namespace TW.SpringFestivalGALA2023.Web.Infrastructures.VotingApi.Constracts.Response;

public class VotingApiResponse
{
    public int code { get; set; }
    public string message { get; set; }
}

public class VotingApiResponse<T> : VotingApiResponse
{
    public T data { get; set; }
}