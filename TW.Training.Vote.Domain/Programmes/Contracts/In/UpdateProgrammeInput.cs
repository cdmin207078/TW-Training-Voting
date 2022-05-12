using TW.Infrastructure.Core.Primitives;

namespace TW.Training.Vote.Domain.Programmes;

public class UpdateProgrammeInput
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int PerPersonMaxVotingCount { get; set; }

    public Id<int> Id { get; set; }
    public Id<int> LastModifierId { get; set; }
    public CodeNumber Code { get; set; }

    // public CodeNumber OriginalCode { get; set; }

    public List<Item> Items { get; set; }
    
    public class Item
    {
        public int Order { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        public Id<int> Id { get; set; }
        public CodeNumber Code { get; set; }
    }
}