using TW.Infrastructure.Core.Exceptions;
using TW.Infrastructure.Core.Primitives;

namespace TW.Training.Vote.Domain.Votings;

public class GetVotingFortuneInput
{
    public GetVotingFortuneInput(CodeNumber programmeCodeNumber)
    {
        ProgrammeCodeNumber = programmeCodeNumber ?? throw new TWException(nameof(programmeCodeNumber));
        
        // FortuneCount = fortuneCount;
    }

    public CodeNumber ProgrammeCodeNumber { get; }
    // public int FortuneCount { get; }
}