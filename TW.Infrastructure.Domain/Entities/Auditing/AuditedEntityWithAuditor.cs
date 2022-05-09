using System;
using TW.Infrastructure.Domain.Primitives;

namespace TW.Infrastructure.Domain.Entities.Auditing;

[Serializable]
public abstract class AuditedEntityWithAuditor<TAuditor> : AuditedEntity, IAuditedEntity<TAuditor>
{
    public TAuditor Creator { get; protected set; }
    public TAuditor LastModifier { get; protected set; }
}

[Serializable]
public abstract class AuditedEntityWithAuditor<TKey, TAuditor> : AuditedEntity<TKey>, IAuditedEntity<TAuditor>
{
    protected AuditedEntityWithAuditor()
    {

    }

    protected AuditedEntityWithAuditor(Id<TKey> id) : base(id)
    {

    }

    public TAuditor Creator { get; protected set; }
    public TAuditor LastModifier { get; protected set; }
}
