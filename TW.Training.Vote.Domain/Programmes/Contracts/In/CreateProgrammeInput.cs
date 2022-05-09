using TW.Infrastructure.Domain.Primitives;

namespace TW.Training.Vote.Domain.Programmes;

public class CreateProgrammeInput
{
    public CodeNumber Code { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int PerPersonMaxVotingCount { get; set; }
    public List<Item> Items { get; set; }
    
    public class Item
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}