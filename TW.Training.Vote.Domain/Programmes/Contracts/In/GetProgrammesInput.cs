using TW.Infrastructure.Domain.Models;
using TW.Infrastructure.Domain.Primitives;

namespace TW.Training.Vote.Domain.Programmes;

public class GetProgrammesInput
{
    public CodeNumber CodeNumber { get; set; }
    public DateTimeSpan creationDateTimeSpan { get; set; }
}