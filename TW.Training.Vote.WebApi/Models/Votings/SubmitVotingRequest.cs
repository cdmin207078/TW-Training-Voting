namespace TW.Training.Vote.WebApi.Models.Votings;

public class SubmitVotingRequest
{
    public string UserName { get; set; }
    public string UserMobilePhoneNumber { get;  set; }
    public List<string> VotingCodeNumbers { get; set; }
}