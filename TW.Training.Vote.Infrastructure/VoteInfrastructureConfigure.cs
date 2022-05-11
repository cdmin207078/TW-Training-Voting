namespace TW.Training.Vote.Infrastructure;

public class VoteInfrastructureConfigure
{
    public DatabaseSetting Database { get; set; }
    
    public class DatabaseSetting
    {
        public string ConnectionString { get; set; }
    }
}