namespace TW.Infrastructure.Core.AbstractModelObjects;

public interface IAuditedObject : ICreationAuditedObject, IModificationAuditedObject
{

}

public interface IAuditedObject<TAuditor> : ICreationAuditedObject<TAuditor>, IModificationAuditedObject<TAuditor>
{

}
