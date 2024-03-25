using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevJoy.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Converts an IEnumerable to a CSV string. Allows you to specify header names and the properties to include in the CSV.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The IEnumerable that will vbe serialized to CSV.</param>
        /// <param name="headers">The names of the column headers preferred. (optional)</param>
        /// <param name="propertyNames">The names of the properties to include. (optional)</param>
        /// <returns>A csv formatted string containing all items in the source IEnumerable.</returns>
        /// <exception cref="ArgumentNullException">Parameters headers and propertyNames must both be present (or null).</exception>
        /// <exception cref="ArgumentException">Parameters headers and propertyNames must have the same number of elements.</exception>
        public static string ToCsvString<T>(this IEnumerable<T> source, IList<string>? headers = null, IList<string>? propertyNames = null)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (headers == null && propertyNames != null || headers != null && propertyNames == null)
            {
                throw new ArgumentNullException("Parameters headers and propertyNames must both be present or both be null.");
            }
            if (headers != null && propertyNames != null && headers.Count() != propertyNames.Count())
            {
                throw new ArgumentException("Parameters headers and propertyNames must have the same number of elements.");
            }

            if (headers is null) headers = typeof(T).GetProperties().Select(p => p.Name).ToList();
            if (propertyNames is null) propertyNames = typeof(T).GetProperties().Select(p => p.Name).ToList();


            List<PropertyInfo> props = new List<PropertyInfo>();

            foreach (var prop in typeof(T).GetProperties())
            {
                if (propertyNames.Contains(prop.Name))
                {
                    props.Add(prop);
                }
            }


            // Build the header row
            var header = string.Join(",", headers);

            // Build the CSV data
            var builder = new StringBuilder();
            builder.AppendLine(header);

            foreach (var item in source)
            {
                var values = props.Select(p => p.GetValue(item)?.ToString() ?? "");
                builder.AppendLine(string.Join(",", values));
            }

            return builder.ToString();
        }
    }
}
