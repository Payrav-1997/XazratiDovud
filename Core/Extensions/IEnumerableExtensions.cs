using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Joins elements with a Char
        /// </summary>
        /// <returns>Returns joined elements</returns>
        public static string Implode<TValue>(this IEnumerable<TValue> source, char glue)
        {
            return string.Join(glue, source);
        }



        /// <summary>
        /// Joins elements with a string
        /// </summary>
        /// <returns>Returns joined elements</returns>
        public static string Implode<TValue>(this IEnumerable<TValue> source, string glue)
        {
            return string.Join(glue, source);
        }



        public static IEnumerable<TA> Except<TA, TB, TK>(
                this IEnumerable<TA> a,
                IEnumerable<TB> b,
                Func<TA, TK> selectKeyA,
                Func<TB, TK> selectKeyB,
                IEqualityComparer<TK> comparer = null)
        {
            return a.Where(aItem => !b.Select(bItem => selectKeyB(bItem)).Contains(selectKeyA(aItem), comparer));
        }


        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
