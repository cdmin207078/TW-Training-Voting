namespace TW.Training.Vote.WebApi.Models.Programmes;

public class UpdateProgrammeRequest
{
    public string Code { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public int PerPersonMaxVotingCount { get; set; }
    public List<Item> Items { get; set; }
    
    public class Item
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}