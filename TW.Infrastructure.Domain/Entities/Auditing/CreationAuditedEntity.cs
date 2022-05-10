using System;
using TW.Infrastructure.Domain.Primitives;

namespace TW.Infrastructure.Domain.Entities.Auditing;

[Serializable]
public abstract class CreationAuditedEntity : Entity, ICreationAuditedEntity
{
    public Id<int> CreatorId { get; protected set; }
    public DateTime CreationTime { get; protected set; }

    protected void SetCreation(Id<int> creatorId)
    {
        CreatorId = creatorId ?? throw new ArgumentNullException(nameof(creatorId));
        CreationTime = DateTime.UtcNow;
    }

    protected void SetCreation(Id<int> creatorId, DateTime creationTime)
    {
        CreatorId = creatorId ?? throw new ArgumentNullException(nameof(creatorId));
        CreationTime = creationTime;
    }
}

[Serializable]
public abstract class CreationAuditedEntity<TKey> : Entity<TKey>, ICreationAuditedEntity
{
    protected CreationAuditedEntity()
    {

    }

    protected CreationAuditedEntity(Id<TKey> id) : base(id)
    {

    }

    public Id<int> CreatorId { get; protected set; }
    public DateTime CreationTime { get; protected set; }

    protected void SetCreation(Id<int> creatorId)
    {
        CreatorId = creatorId ?? throw new ArgumentNullException(nameof(creatorId));
        CreationTime = DateTime.UtcNow;
    }
    
    protected void SetCreation(Id<int> creatorId, DateTime creationTime)
    {
        CreatorId = creatorId ?? throw new ArgumentNullException(nameof(creatorId));
        CreationTime = creationTime;
    }
}
