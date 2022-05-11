using System;
using TW.Infrastructure.Core.Primitives;

namespace TW.Infrastructure.Domain.Entities.Auditing;

[Serializable]
public abstract class CreationAuditedEntityWithAuditor<TAuditor> : CreationAuditedEntity, ICreationAuditedEntity<TAuditor>
{
    public TAuditor Creator { get; protected set; }
}

[Serializable]
public abstract class CreationAuditedEntityWithAuditor<TKey, TAuditor> : CreationAuditedEntity<TKey>, ICreationAuditedEntity<TAuditor>
{
    protected CreationAuditedEntityWithAuditor()
    {

    }

    protected CreationAuditedEntityWithAuditor(Id<TKey> id) : base(id)
    {

    }

    public TAuditor Creator { get; protected set; }
}
