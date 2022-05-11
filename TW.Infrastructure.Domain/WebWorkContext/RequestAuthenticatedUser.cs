using TW.Infrastructure.Core.Primitives;

namespace TW.Infrastructure.Domain.WebWorkContext;

public class RequestAuthenticatedUser
{
    public Id<int> Id { get; protected set; }
    public string Account { get; protected set; }
    public string Nickname { get; protected set; }
    public string Email { get; protected set; }
}