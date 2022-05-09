using System;
using TW.Infrastructure.Domain.Primitives;

namespace TW.Infrastructure.Domain.Entities.Auditing;

public interface ICreationAuditedEntity
{
    Id<int> CreatorId { get; }
    DateTime CreationTime { get; }
}

public interface ICreationAuditedEntity<TAuditor> : ICreationAuditedEntity
{
    TAuditor Creator { get; }
}
