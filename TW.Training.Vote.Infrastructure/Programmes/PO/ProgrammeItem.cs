using TW.Infrastructure.Core.AbstractModelObjects;
using TW.Training.Vote.Infrastructure.Votings.PO;

namespace TW.Training.Vote.Infrastructure.Programmes.PO;

public class ProgrammeItem : FullAuditedObject<int>
{
    public string Code { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public Programme Programme { get; set; }
    public List<Voting> Votings { get; set; }
}