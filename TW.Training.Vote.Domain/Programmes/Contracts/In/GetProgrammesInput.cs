using TW.Infrastructure.Domain.Models;
using TW.Infrastructure.Core.Primitives;
using TW.Infrastructure.Domain.Pagination;

namespace TW.Training.Vote.Domain.Programmes;

public sealed class GetProgrammesInput : PaginationRequest
{
    public GetProgrammesInput(
        CodeNumber codeNumber,
        DateTimeSpan creationDateTimeSpan,
        int pageSize,
        int currentPage) 
        : base(pageSize, currentPage)
    {
        CodeNumber = codeNumber;
        CreationDateTimeSpan = creationDateTimeSpan;
    }
    public CodeNumber CodeNumber { get; protected set; }
    public DateTimeSpan CreationDateTimeSpan { get; protected set; }
}