using TW.Infrastructure.Core.AbstractModelObjects;

namespace TW.Training.Vote.Infrastructure.Programmes.PO;

public class Programme : FullAuditedObject<int>
{
    public string Code { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public int PersonalMaxVotingCount { get; set; }
    
    public List<ProgrammeItem> ProgrammeItems { get; set; } = new List<ProgrammeItem>();
}