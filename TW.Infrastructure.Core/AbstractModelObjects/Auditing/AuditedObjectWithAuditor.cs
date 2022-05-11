using System;

namespace TW.Infrastructure.Core.AbstractModelObjects;

[Serializable]
public abstract class AuditedObjectWithAuditor<TAuditor> : AuditedObject, IAuditedObject<TAuditor>
{
    public TAuditor Creator { get; set; }
    public TAuditor LastModifier { get; set; }
}

[Serializable]
public abstract class AuditedObjectWithAuditor<TKey, TAuditor> : AuditedObject<TKey>, IAuditedObject<TAuditor>
{
    public TAuditor Creator { get; set; }
    public TAuditor LastModifier { get; set; }
}
