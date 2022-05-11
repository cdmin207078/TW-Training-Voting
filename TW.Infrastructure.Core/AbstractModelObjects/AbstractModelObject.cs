namespace TW.Infrastructure.Core.AbstractModelObjects;

public abstract class AbstractModelObject : IModelObject
{
    public abstract object[] GetKeys();

    public override bool Equals(object obj)
    {
        if (obj is not null && obj is AbstractModelObject)
        {
            var sourceKey = string.Join(",", GetKeys());
            var destinationKey = string.Join(",", (obj as AbstractModelObject).GetKeys());
            return sourceKey.Equals(destinationKey);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return GetKeys().GetHashCode();
    }

    public override string ToString()
    {
        return $"[ModelObject: {GetType().Name}] Keys = {string.Join(",", GetKeys())}";
    }
}

public abstract class AbstractModelObject<TKey> : AbstractModelObject, IModelObject<TKey>
{
    public TKey Id { get; set; }

    protected AbstractModelObject() { }

    public override object[] GetKeys()
    {
        return new object[] { Id };
    }

    public override bool Equals(object obj)
    {
        return obj is not null && obj is AbstractModelObject<TKey> target && Id.Equals(target.Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public override string ToString()
    {
        return $"[AbstractModelObject: {GetType().Name}] Id = {Id}";
    }
}
