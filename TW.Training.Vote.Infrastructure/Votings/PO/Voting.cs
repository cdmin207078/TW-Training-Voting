using TW.Infrastructure.Core.AbstractModelObjects;
using TW.Training.Vote.Infrastructure.Programmes.PO;

namespace TW.Training.Vote.Infrastructure.Votings.PO;

public class Voting : CreationAuditedObject<int>
{
    public string Name { get; set; }
    public string MobilePhoneNumber { get; set; }
    
    public ProgrammeItem ProgrammeItem { get; set; }
}