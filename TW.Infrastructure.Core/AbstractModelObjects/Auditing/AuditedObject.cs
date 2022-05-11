using System;

namespace TW.Infrastructure.Core.AbstractModelObjects;

[Serializable]
public abstract class AuditedObject : CreationAuditedObject, IAuditedObject
{
    public int? LastModifierId { get; set; }
    public DateTime? LastModificationTime { get; set; }
}

[Serializable]
public abstract class AuditedObject<TKey> : CreationAuditedObject<TKey>, IAuditedObject
{
    public int? LastModifierId { get; set; }
    public DateTime? LastModificationTime { get; set; }
}

