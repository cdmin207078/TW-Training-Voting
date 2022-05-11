using System;
using TW.Infrastructure.Core.Primitives;

namespace TW.Infrastructure.Domain.Entities.Auditing;

public interface IDeletionAuditedEntity
{
    bool IsDeleted { get; }
    Id<int> DeleterId { get; }
    DateTime? DeletionTime { get; }
}

public interface IDeletionAuditedEntity<TAuditor> : IDeletionAuditedEntity
{
    TAuditor Deleter { get; }
}
