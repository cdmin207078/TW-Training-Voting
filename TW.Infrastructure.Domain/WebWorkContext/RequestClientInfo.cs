namespace TW.Infrastructure.Domain.WebWorkContext;

public class RequestClientInfo
{
    public string IP { get; protected set; }
    public string UserAgent { get; protected set; }
    public string Browser { get; protected set; }
    public string OS { get; protected set; }
}