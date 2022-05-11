namespace TW.Infrastructure.Domain.WebWorkContext;

public class WebWorkContext
{
    public Guid CorrelationId { get; protected set; }
    public string Token { get; protected set; }
    public RequestAuthenticatedUser User { get; protected set; }
    public RequestClientInfo Client { get; protected set; }
}