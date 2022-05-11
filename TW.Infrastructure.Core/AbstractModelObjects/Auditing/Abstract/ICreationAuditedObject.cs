using System;

namespace TW.Infrastructure.Core.AbstractModelObjects;

public interface ICreationAuditedObject
{
    int CreatorId { get; set; }
    DateTime CreationTime { get; set; }
}

public interface ICreationAuditedObject<TAuditor> : ICreationAuditedObject
{
    TAuditor Creator { get; set; }
}
