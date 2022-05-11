using System;
using TW.Infrastructure.Core.Primitives;

namespace TW.Infrastructure.Domain.Entities.Auditing;

public interface IModificationAuditedEntity
{
    Id<int> LastModifierId { get; }
    DateTime? LastModificationTime { get; }
}

public interface IModificationAuditedEntity<TAuditor> : IModificationAuditedEntity
{
    TAuditor LastModifier { get; }
}
