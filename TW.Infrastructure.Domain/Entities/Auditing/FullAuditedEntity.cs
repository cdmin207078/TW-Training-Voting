using System;
using TW.Infrastructure.Core.Primitives;

namespace TW.Infrastructure.Domain.Entities.Auditing;

[Serializable]
public abstract class FullAuditedEntity : AuditedEntity, IFullAuditedEntity
{
    public bool IsDeleted { get; protected set; }
    public Id<int> DeleterId { get; protected set; }
    public DateTime? DeletionTime { get; protected set; }
}

[Serializable]
public abstract class FullAuditedEntity<TKey> : AuditedEntity<TKey>, IFullAuditedEntity
{
    protected FullAuditedEntity()
    {

    }

    protected FullAuditedEntity(Id<TKey> id) : base(id)
    {

    }

    public bool IsDeleted { get; protected set; }
    public Id<int> DeleterId { get; protected set; }
    public DateTime? DeletionTime { get; protected set; }
}
