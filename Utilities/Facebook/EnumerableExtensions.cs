using System;
using System.Collections;
using System.Linq;

namespace Facebook
{
    /// <summary>Provides extended functionaly to types that implement <see cref="IEnumerable" />.</summary>
    public static class EnumerableExtensions
    {
        /// <summary>Returns <paramref name="input" /> as an <see cref="IEnumerable" />.</summary>
        /// <param name="input">An <see cref="Object" /> whose type implements <see cref="IEnumerable" />.</param>
        /// <param name="col">When this method returns, <paramref name="input"/> cast as an <see cref="IEnumerable" />.</param>
        /// <returns><c>true</c> if <paramref name="Object"/> was able to be cast as an <see cref="IEnumerable" />; otherwise, <c>false</c>.</returns>
        public static Boolean TryMakeEnumerable(this Object input, out IEnumerable col)
        {
            var type = input.GetType();
            if (type == typeof(String))
            {
                col = null;
                return false;
            }
            else
            {
                col = input as IEnumerable;
                return col != null;
            }
        }

        /// <summary>Returns a <see cref="String" /> containing the values in <paramref name="input"/>, separated by <paramref name="delimiter" />.</summary>
        /// <param name="input">Any collection of values.</param>
        /// <param name="delimiter">A <see cref="String" /> value that will be used to delimited the values in <paramref name="input"/>.</param>
        /// <returns>A <see cref="String" /> containing the values in <paramref name="input"/>, separated by <paramref name="delimiter" />.</returns>
        /// <remarks>If a value in <paramref name="input"/> is not a <see cref="String" />, the output of the object's <see cref="Object.ToString()" /> method will be used.</remarks>
        public static String ToDelimitedString(this IEnumerable input, String delimiter)
        {
            return String.Join(delimiter, (
                from obj in input.OfType<Object>()
                select obj == null ? String.Empty : obj.ToString()).ToArray());
        }

        /// <summary>Returns a <see cref="String" /> containing the values in <paramref name="input"/>, separated by <paramref name="delimiter" />.</summary>
        /// <param name="input">Any collection of values.</param>
        /// <param name="delimiter">A <see cref="Char" /> value that will be used to delimited the values in <paramref name="input"/>.</param>
        /// <returns>A <see cref="String" /> containing the values in <paramref name="input"/>, separated by <paramref name="delimiter" />.</returns>
        /// <remarks>If a value in <paramref name="input"/> is not a <see cref="String" />, the output of the object's <see cref="Object.ToString()" /> method will be used.</remarks>
        public static String ToDelimitedString(this IEnumerable input, Char delimiter)
        {
            return input.ToDelimitedString(delimiter.ToString());
        }
    }
}
