using TW.Infrastructure.Core.Primitives;

namespace TW.Training.Vote.Domain.Programmes;

public class CreateProgrammeItemInput
{
    public int Order { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public CodeNumber Code { get; set; }
    public CodeNumber ProgrammeCode { get; set; }
    public Id<int> CreatorId { get; set; }
}