using TW.Infrastructure.Domain.Primitives;

namespace TW.Training.Vote.Domain.Voting;

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