using TW.Infrastructure.Core.Primitives;

namespace TW.Training.Vote.Domain.Programmes;

public class DeleteProgrammeInput
{
    public Id<int> Id { get; set; }
}