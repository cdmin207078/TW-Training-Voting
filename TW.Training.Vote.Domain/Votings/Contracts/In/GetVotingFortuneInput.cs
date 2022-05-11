using TW.Infrastructure.Core.Primitives;

namespace TW.Training.Vote.Domain.Votings;

public class GetVotingFortuneInput
{
    public GetVotingFortuneInput(CodeNumber programmeCodeNumber, int fortuneCount)
    {
        ProgrammeCodeNumber = programmeCodeNumber ?? throw new ArgumentNullException(nameof(programmeCodeNumber));
        
        if (fortuneCount < 1) 
            throw new ArgumentOutOfRangeException(nameof(fortuneCount));
        
        FortuneCount = fortuneCount;
    }

    public CodeNumber ProgrammeCodeNumber { get; }
    public int FortuneCount { get; }
}