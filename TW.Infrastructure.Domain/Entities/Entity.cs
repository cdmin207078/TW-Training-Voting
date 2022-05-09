using System;
using TW.Infrastructure.Domain.Primitives;

namespace TW.Infrastructure.Domain.Entities;

[Serializable]
public abstract class Entity : IEntity
{
    public abstract object[] GetKeys();

    public override string ToString()
    {
        return $"[ENTITY: {GetType().Name}] Keys = {string.Join(",", GetKeys())}";
    }
}

[Serializable]
public abstract class Entity<TKey> : Entity, IEntity<TKey>
{
    public Id<TKey> Id { get; protected set; }

    protected Entity() { }

    protected Entity(Id<TKey> id)
    {
        Id = id;
    }

    public override object[] GetKeys()
    {
        return new object[] { Id };
    }

    public override string ToString()
    {
        return $"[ENTITY: {GetType().Name}] Id = {Id}";
    }
}