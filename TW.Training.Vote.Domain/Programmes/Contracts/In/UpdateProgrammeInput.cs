using TW.Infrastructure.Domain.Primitives;

namespace TW.Training.Vote.Domain.Programmes;

public class UpdateProgrammeInput
{
    public Id<int> Id { get; set; }
    public CodeNumber Code { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int PerPersonMaxVotingCount { get; set; }
}