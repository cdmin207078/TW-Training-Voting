namespace TW.Training.Vote.Domain.Programmes;

public class GetProgrammeOutput
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int PersonalMaxVotingCount { get; set; }
    public List<Item> ProgrammeItems { get;set; }

    public class Item
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}