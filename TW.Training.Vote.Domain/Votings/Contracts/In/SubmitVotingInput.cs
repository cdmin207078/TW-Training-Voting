using TW.Infrastructure.Core.Primitives;

namespace TW.Training.Vote.Domain.Votings;

public class SubmitVotingInput
{
    public string Name { get; set; }
    public MobilePhoneNumber MobilePhoneNumber { get;  set; }
    public CodeNumber ProgrammeCodeNumber { get; set; }
    public List<CodeNumber> ProgrammeItemCodeNumbers { get; set; }
}