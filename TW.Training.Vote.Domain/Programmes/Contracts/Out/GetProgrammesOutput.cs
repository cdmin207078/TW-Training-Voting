using TW.Infrastructure.Domain.Pagination;

namespace TW.Training.Vote.Domain.Programmes;

public sealed class GetProgrammesOutput:PaginationResponse<GetProgrammesOutput.Item>
{
    public GetProgrammesOutput(int pageSize, int currentPage, int totalItemsCount, List<Item> items)
        : base(pageSize, currentPage, totalItemsCount, items) { }

    public class Item
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int PersonalMaxVotingCount { get; set; }
        public DateTime CreationTime { get; set; }
    }
}