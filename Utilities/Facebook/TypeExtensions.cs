using System;

namespace Facebook
{
    /// <summary>Provides extended functionality for <see cref="Type" /> objects.</summary>
    public static class TypeExtensions
    {
        /// <summary>Gets a value representing if <paramref name="type" /> is an integral type.</summary>
        /// <param name="type">A <see cref="Type" /> object.</param>
        /// <returns><c>true</c> if <paramref name="type"/> is integral; otherwise, <c>false</c>.</returns>
        public static Boolean IsIntegral(this Type type)
        {
            if (type == typeof(Boolean) ||
                type == typeof(Byte) ||
                type == typeof(Int16) ||
                type == typeof(Int32) ||
                type == typeof(Int64) ||
                type == typeof(UInt16) ||
                type == typeof(UInt32) ||
                type == typeof(UInt64)) return true;
            else return false;                
        }
    }
}
