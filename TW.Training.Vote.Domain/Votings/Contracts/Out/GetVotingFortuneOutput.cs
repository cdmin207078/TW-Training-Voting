using System.Security.AccessControl;
using TW.Infrastructure.Core.Primitives;

namespace TW.Training.Vote.Domain.Votings;

public class GetVotingFortuneOutput
{
    public List<string> FortunerMobilePhoneNumber { get; set; }
}