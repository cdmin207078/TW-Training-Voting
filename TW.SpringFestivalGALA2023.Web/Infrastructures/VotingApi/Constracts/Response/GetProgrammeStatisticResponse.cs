namespace TW.SpringFestivalGALA2023.Web.Infrastructures.VotingApi.Constracts.Response;

public class GetProgrammeStatisticResponse
{
    public List<Item> Items { get; set; }
    
    public int VotingTotal
    {
        get
        {
            return Items is not null && Items.Any() ? Items.Sum(x => x.VotingCount) : 0;
        }
    }

    public class Item
    {
        public string ProgrammeItemCode { get; set; }
        public string Title { get; set; }
        public int VotingCount  { get; set; }
    }
}