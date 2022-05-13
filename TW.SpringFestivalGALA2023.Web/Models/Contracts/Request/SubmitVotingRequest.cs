namespace TW.SpringFestivalGALA2023.Web.Models.Contracts.Request;

public class SubmitVotingRequest
{
    public string UserName { get; set; }
    public string UserMobilePhoneNumber { get;  set; }
    public List<string> VotingCodeNumbers { get; set; }
}