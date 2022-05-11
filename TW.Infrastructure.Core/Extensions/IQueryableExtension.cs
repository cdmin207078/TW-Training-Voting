using System.Linq.Expressions;

namespace System.Linq
{
    public static class IQueryableExtension
    {
        /// <summary>
        /// WhereIf
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> source, bool condition, Expression<Func<TSource, bool>> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            if (condition)
                source = source.Where(predicate);

            return source;
        }
    }
}
