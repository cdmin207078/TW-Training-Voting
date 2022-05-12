using System.Security.AccessControl;
using TW.Infrastructure.Core.Primitives;

namespace TW.Training.Vote.Domain.Votings;

public class GetVotingFortuneOutput
{
    public List<string> FortunerMobilePhoneNumber { get; set; }
    // public string ProgrammeCode { get; set; }
    // public string ProgrammeTitle { get; set; }
    // public List<VotingFortuner> Fortuners { get; set; }
    //
    // public class VotingFortuner
    // {
    //     public string ProgrammeItemCode { get; set; }
    //     public string ProgrammeItemTitle { get; set; }
    //     public string FortunerName { get; set; }
    //     public string FortunerMobilePhoneNumber { get; set; }
    // }
}