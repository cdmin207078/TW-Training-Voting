using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class ListExtension
    {
        /// <summary>
        /// IsNullOrEmpty
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<TSource>(this List<TSource> source)
        {
            return source is null || !source.Any();
        }

        /// <summary>
        /// IsNullOrEmpty
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<TSource>(this List<TSource> source, Func<TSource, bool> predicate)
        {
            if (predicate.IsNull())
                return source.IsNullOrEmpty();
            else
                return source.Where(predicate).ToList().IsNullOrEmpty();
        }

        /// <summary>
        /// IsNotNullOrEmpty
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty<TSource>(this List<TSource> source)
        {
            return !source.IsNullOrEmpty();
        }

        /// <summary>
        /// IsNotNullOrEmpty
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty<TSource>(this List<TSource> source, Func<TSource, bool> predicate)
        {
            return !source.IsNullOrEmpty(predicate);
        }

        /// <summary>
        /// IsNullOrEmptyThrowException
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static void IsNullOrEmptyThrowException<TSource>(this List<TSource> source)
        {
            if (source.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(source));
        }

        /// <summary>
        /// IsNullOrEmptyThrowException
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static void IsNullOrEmptyThrowException<TSource>(this List<TSource> source, Func<TSource, bool> predicate)
        {
            if (source.IsNullOrEmpty(predicate))
                throw new ArgumentNullException(nameof(source));
        }

        /// <summary>
        /// Confuse
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        public static List<TSource> Confuse<TSource>(this List<TSource> source)
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source));

            if (source.Any())
            {
                var rand = new Random();
                var result = new List<TSource>();
                foreach (var item in source)
                {
                    result.Insert(rand.Next(0, result.Count), item);
                }
                return result;
            }
            else
            {
                return source;
            }
        }
    }
}
