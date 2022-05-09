namespace TW.Infrastructure.Domain.Entities.Auditing;

public interface IAuditedEntity : ICreationAuditedEntity, IModificationAuditedEntity
{

}

public interface IAuditedEntity<TAuditor> : ICreationAuditedEntity<TAuditor>, IModificationAuditedEntity<TAuditor>
{

}
