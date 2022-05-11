using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TW.Infrastructure.Core.Primitives;

public class Id<TValue> : AbstractPrimitiveObject<TValue>
{
    public Id(TValue value) : base(value) { }

    protected override TValue TryGetValue(TValue value)
    {
        if (value == null)
            throw new ArgumentException("id cannot be null");

        var vType = typeof(TValue);

        if (vType.Equals(typeof(short)))
        {
            short.TryParse(value.ToString(), out var val);
            if (val < 1)
                throw new ArgumentException("id is illegal.");
        }
        else if (vType.Equals(typeof(int)))
        {
            int.TryParse(value.ToString(), out var val);
            if (val < 1)
                throw new ArgumentException("id is illegal.");
        }
        else if (vType.Equals(typeof(long)))
        {
            long.TryParse(value.ToString(), out var val);
            if (val < 1)
                throw new ArgumentException("id is illegal.");
        }
        else if (vType.Equals(typeof(string)))
        {
            if (string.IsNullOrWhiteSpace(value.ToString()))
                throw new ArgumentException("id cannot be empty");

        }
        else if (vType.Equals(typeof(ushort)) || vType.Equals(typeof(uint)) || vType.Equals(typeof(ulong)) || vType.Equals(typeof(Guid)))
        {
            // Only determine is not null.
        }
        else
        {
            throw new ArgumentException($"id type {vType} are not support");
        }

        return value;
    }
}