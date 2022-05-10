using TW.Infrastructure.Domain.Primitives;

namespace TW.Training.Vote.Domain.Voting;

public class CreateVotingInput
{
    public string Name { get; set; }
    
    public MobilePhoneNumber MobilePhoneNumber { get;  set; }
    public CodeNumber ProgrammeCodeNumber { get; set; }
    public List<CodeNumber> ProgrammeItemCodeNumbers { get; set; }
}