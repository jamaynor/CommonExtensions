using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAMaynor
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Returns true if an item is found within an IEnumerable<typeparamref name="T"/> based on a linq function (predicate).
        /// </summary>
        /// <param name="matchingFunction">A predicate function that evaluates to the match.</param>
        /// <returns></returns>
        public static bool Contains<T>(this IEnumerable<T> enumerable, Func<T, bool> matchingFunction)
        {
            foreach (var item in enumerable)
            {
                if (matchingFunction(item)) return true;
            }
            return false;
        }
    }
}
