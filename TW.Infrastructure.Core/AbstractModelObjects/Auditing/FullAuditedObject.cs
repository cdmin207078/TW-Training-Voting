using System;

namespace TW.Infrastructure.Core.AbstractModelObjects;

[Serializable]
public abstract class FullAuditedObject : AuditedObject, IFullAuditedObject
{
    public bool IsDeleted { get; set; }
    public int? DeleterId { get; set; }
    public DateTime? DeletionTime { get; set; }
}

[Serializable]
public abstract class FullAuditedObject<TKey> : AuditedObject<TKey>, IFullAuditedObject
{
    public bool IsDeleted { get; set; }
    public int? DeleterId { get; set; }
    public DateTime? DeletionTime { get; set; }
}

