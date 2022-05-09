namespace TW.Infrastructure.Domain.Primitives;

public abstract class AbstractPrimitiveObject<TValue>
{
    public AbstractPrimitiveObject(TValue value)
    {
        Value = TryGetValue(value);
    }

    public TValue Value { get; protected set; }

    protected abstract TValue TryGetValue(TValue value);

    public override bool Equals(object obj)
    {
        return obj is not null && obj is AbstractPrimitiveObject<TValue> target && Value.Equals(target.Value);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Value?.ToString();
    }

    public static bool operator ==(AbstractPrimitiveObject<TValue> a, AbstractPrimitiveObject<TValue> b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(AbstractPrimitiveObject<TValue> a, AbstractPrimitiveObject<TValue> b)
    {
        return !a.Equals(b);
    }
}