using TW.Infrastructure.Domain.Primitives;

namespace TW.Training.Vote.Domain.Voting;

public class GetVotingStatisticOutput
{
    public string ProgrammeCode { get; set; }
    public string Title { get; set; }
    public List<VotingStatitic> VotingStatitics { get; set; }
    
    public class VotingStatitic
    {
        public string ProgrammeItemCode { get; set; }
        public string Title { get; set; }
        public int VotingCount  { get; set; }
    }
}