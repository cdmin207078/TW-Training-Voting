namespace TW.Infrastructure.Domain.Entities.Auditing;

public interface IFullAuditedEntity : IAuditedEntity, IDeletionAuditedEntity
{

}

public interface IFullAuditedEntity<TAuditor> : IAuditedEntity<TAuditor>, IDeletionAuditedEntity<TAuditor>
{

}
