namespace TW.Infrastructure.Core.AbstractModelObjects;

public interface IFullAuditedObject : IAuditedObject, IDeletionAuditedObject
{

}

public interface IFullAuditedObject<TAuditor> : IAuditedObject<TAuditor>, IDeletionAuditedObject<TAuditor>
{

}
