using TW.Infrastructure.Core.Primitives;

namespace TW.Infrastructure.Domain.Entities;

// 还不知该何时, 如何使用
public interface IEntity
{
    object[] GetKeys();
}

public interface IEntity<TKey> : IEntity
{
    Id<TKey> Id { get; }
}