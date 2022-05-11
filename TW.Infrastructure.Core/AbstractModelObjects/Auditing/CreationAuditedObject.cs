using System;

namespace TW.Infrastructure.Core.AbstractModelObjects;

[Serializable]
public abstract class CreationAuditedObject : AbstractModelObject, ICreationAuditedObject
{
    public int CreatorId { get; set; }
    public DateTime CreationTime { get; set; }
}

[Serializable]
public abstract class CreationAuditedObject<TKey> : AbstractModelObject<TKey>, ICreationAuditedObject
{
    public int CreatorId { get; set; }
    public DateTime CreationTime { get; set; }
}
