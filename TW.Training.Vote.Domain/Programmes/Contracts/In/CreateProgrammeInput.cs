using TW.Infrastructure.Domain.Primitives;

namespace TW.Training.Vote.Domain.Programmes;

public class CreateProgrammeInput
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int PerPersonMaxVotingCount { get; set; }
    
    public CodeNumber Code { get; set; }
    public List<Item> Items { get; set; }
    public Id<int> CreatorId { get; set; }

    public class Item
    {
        public int Order { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        public CodeNumber Code { get; set; }
    }
}