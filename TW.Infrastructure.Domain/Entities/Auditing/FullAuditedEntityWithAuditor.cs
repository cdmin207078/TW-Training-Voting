using System;
using TW.Infrastructure.Core.Primitives;

namespace TW.Infrastructure.Domain.Entities.Auditing;

[Serializable]
public abstract class FullAuditedEntityWithAuditor<TAuditor> : FullAuditedEntity, IFullAuditedEntity<TAuditor>
{
    public TAuditor Creator { get; protected set; }
    public TAuditor LastModifier { get; protected set; }
    public TAuditor Deleter { get; protected set; }
}

[Serializable]
public abstract class FullAuditedEntityWithAuditor<TKey, TAuditor> : FullAuditedEntity<TKey>, IFullAuditedEntity<TAuditor>
{
    protected FullAuditedEntityWithAuditor()
    {

    }

    protected FullAuditedEntityWithAuditor(Id<TKey> id) : base(id)
    {

    }

    public TAuditor Creator { get; protected set; }
    public TAuditor LastModifier { get; protected set; }
    public TAuditor Deleter { get; protected set; }
}
