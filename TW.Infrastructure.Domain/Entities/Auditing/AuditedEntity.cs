using System;
using TW.Infrastructure.Domain.Primitives;

namespace TW.Infrastructure.Domain.Entities.Auditing;

[Serializable]
public abstract class AuditedEntity : CreationAuditedEntity, IAuditedEntity
{
    public Id<int> LastModifierId { get; protected set; }
    public DateTime? LastModificationTime { get; protected set; }

    protected void SetLastModified(Id<int> lastModifierId)
    {
        LastModifierId = lastModifierId ?? throw new ArgumentNullException(nameof(lastModifierId));
        LastModificationTime = DateTime.UtcNow;
    }
}

[Serializable]
public abstract class AuditedEntity<TKey> : CreationAuditedEntity<TKey>, IAuditedEntity
{
    protected AuditedEntity()
    {

    }

    protected AuditedEntity(Id<TKey> id) : base(id)
    {

    }

    public Id<int> LastModifierId { get; protected set; }
    public DateTime? LastModificationTime { get; protected set; }

    protected void SetLastModified(Id<int> lastModifierId)
    {
        LastModifierId = lastModifierId ?? throw new ArgumentNullException(nameof(lastModifierId));
        LastModificationTime = DateTime.UtcNow;
    }
}
