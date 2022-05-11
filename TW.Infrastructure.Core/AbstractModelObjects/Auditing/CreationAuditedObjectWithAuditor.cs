using System;

namespace TW.Infrastructure.Core.AbstractModelObjects;

[Serializable]
public abstract class CreationAuditedObjectWithAuditor<TAuditor> : CreationAuditedObject, ICreationAuditedObject<TAuditor>
{
    public TAuditor Creator { get; set; }
}

[Serializable]
public abstract class CreationAuditedObjectWithAuditor<TKey, TAuditor> : CreationAuditedObject<TKey>, ICreationAuditedObject<TAuditor>
{
    public TAuditor Creator { get; set; }
}

