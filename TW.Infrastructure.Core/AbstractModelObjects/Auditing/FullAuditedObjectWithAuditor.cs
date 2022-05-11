using System;

namespace TW.Infrastructure.Core.AbstractModelObjects;

[Serializable]
public abstract class FullAuditedObjectWithAuditor<TAuditor> : FullAuditedObject, IFullAuditedObject<TAuditor>
{
    public TAuditor Creator { get; set; }
    public TAuditor LastModifier { get; set; }
    public TAuditor Deleter { get; set; }
}

[Serializable]
public abstract class FullAuditedObjectWithAuditor<TKey, TAuditor> : FullAuditedObject<TKey>, IFullAuditedObject<TAuditor>
{
    public TAuditor Creator { get; set; }
    public TAuditor LastModifier { get; set; }
    public TAuditor Deleter { get; set; }
}
