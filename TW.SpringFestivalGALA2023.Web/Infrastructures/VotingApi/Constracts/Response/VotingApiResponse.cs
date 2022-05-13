namespace TW.SpringFestivalGALA2023.Web.Infrastructures.VotingApi.Constracts.Response;

public abstract class VotingApiResponse
{
    public int code { get; set; }
    public string message { get; set; }
}

public abstract class VotingApiResponse<T> : VotingApiResponse
{
    public T data { get; set; }
}