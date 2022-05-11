namespace TW.Infrastructure.Core.AbstractModelObjects;

public interface IModelObject
{
    object[] GetKeys();
}

public interface IModelObject<TKey> : IModelObject
{
    TKey Id { get; set; }
}
