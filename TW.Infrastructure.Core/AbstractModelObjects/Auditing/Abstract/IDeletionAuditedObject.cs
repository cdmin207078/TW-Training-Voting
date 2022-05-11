using System;

namespace TW.Infrastructure.Core.AbstractModelObjects;

public interface IDeletionAuditedObject
{
    bool IsDeleted { get; set; }
    int? DeleterId { get; set; }
    DateTime? DeletionTime { get; set; }
}

public interface IDeletionAuditedObject<TAuditor> : IDeletionAuditedObject
{
    TAuditor Deleter { get; set; }
}
