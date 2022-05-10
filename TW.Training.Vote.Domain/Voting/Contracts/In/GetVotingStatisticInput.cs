using TW.Infrastructure.Domain.Primitives;

namespace TW.Training.Vote.Domain.Voting;

public class GetVotingStatisticInput
{
    public GetVotingStatisticInput(CodeNumber programmeCodeNumber)
    {
        ProgrammeCodeNumber = programmeCodeNumber ?? throw new ArgumentNullException(nameof(programmeCodeNumber));
    }

    public CodeNumber ProgrammeCodeNumber { get; }
}