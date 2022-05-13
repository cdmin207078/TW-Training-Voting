using System.Runtime.Serialization;

namespace TW.Infrastructure.Core.Exceptions;

[Serializable]
public class TWException : Exception
{
    public virtual int Code { get; }

    public TWException()
    {

    }

    public TWException(int code)
    {
        Code = code;
    }

    public TWException(string message)
        : base(message)
    {
    }

    public TWException(string messageFormat, params object[] args)
        : base(string.Format(messageFormat, args))
    {
    }

    public TWException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public TWException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public TWException(int code, string message)
        : base(message)
    {
        Code = code;
    }

    public TWException(int code, string messageFormat, params object[] args)
        : base(string.Format(messageFormat, args))
    {
        Code = code;
    }

    public TWException(int code, SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        Code = code;
    }

    public TWException(int code, string message, Exception innerException)
        : base(message, innerException)
    {
        Code = code;
    }
}