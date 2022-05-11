using TW.Infrastructure.Core.Primitives;

namespace TW.Training.Vote.Domain.Votings;

public class GetVotingStatisticInput
{
    public GetVotingStatisticInput(CodeNumber programmeCodeNumber)
    {
        ProgrammeCodeNumber = programmeCodeNumber ?? throw new ArgumentNullException(nameof(programmeCodeNumber));
    }

    public CodeNumber ProgrammeCodeNumber { get; }
}