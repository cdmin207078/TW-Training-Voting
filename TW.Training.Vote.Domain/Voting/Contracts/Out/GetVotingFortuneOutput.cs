using System.Security.AccessControl;
using TW.Infrastructure.Domain.Primitives;

namespace TW.Training.Vote.Domain.Voting;

public class GetVotingFortuneOutput
{
    public string ProgrammeCode { get; set; }
    public string ProgrammeTitle { get; set; }
    public List<VotingFortuner> Fortuners { get; set; }
    
    public class VotingFortuner
    {
        public string ProgrammeItemCode { get; set; }
        public string ProgrammeItemTitle { get; set; }
        public string FortunerName { get; set; }
        public string FortunerMobilePhoneNumber { get; set; }
    }
}