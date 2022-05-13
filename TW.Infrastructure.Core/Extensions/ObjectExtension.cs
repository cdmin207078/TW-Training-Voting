using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TW.Infrastructure.Core.Exceptions;

namespace System
{
    public static class ObjectExtension
    {
        public static bool IsNull<TSource>(this TSource source) where TSource : class
        {
            return source is null;
        }

        public static void IsNullThrowException<TSource>(this TSource source) where TSource : class
        {
            if (source.IsNull())
                throw new TWException(nameof(source));
        }

        public static bool IsNotNull<TSource>(this TSource source) where TSource : class
        {
            return source is not null;
        }
    }
}
