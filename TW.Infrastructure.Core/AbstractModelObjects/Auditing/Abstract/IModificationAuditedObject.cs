using System;

namespace TW.Infrastructure.Core.AbstractModelObjects;

public interface IModificationAuditedObject
{
    int? LastModifierId { get; set; }
    DateTime? LastModificationTime { get; set; }
}

public interface IModificationAuditedObject<TAuditor> : IModificationAuditedObject
{
    TAuditor LastModifier { get; set; }
}
