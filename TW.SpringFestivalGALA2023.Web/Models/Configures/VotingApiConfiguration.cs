namespace TW.SpringFestivalGALA2023.Web.Models.Configures;

public class VotingApiConfiguration
{
    public string ProgrammeCode { get; set; }
    public string BaseURL { get; set; }
    public List<EndPoint> EndPoints { get; set; }
    
    public class EndPoint
    {
        public string Code { get; set; }
        public string Path { get; set; }
        public string Descirption { get; set; }
    }
}