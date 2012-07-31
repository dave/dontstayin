using System;

namespace Facebook
{
    /// <summary>Provides extension methods for the <see cref="DateTime" /> class.</summary>
    public static class DateTimeExtensions
    {
        /// <summary>Converts the <see cref="DateTime" /> value to its <see cref="UnixDateTime" /> equivalent.</summary>
        /// <param name="dateTime">A <see cref="DateTime" /> value that will converted to a Unix timestamp.</param>
        /// <returns>A <see cref="UnixDateTime" /> value that is the equivalent of the <see cref="DateTime" /> value specified in the <paramref name="dateTime" /> parameter.</returns>
        public static UnixDateTime ToUnixDateTime(this DateTime dateTime)
        {
            return new UnixDateTime(dateTime);
        }
    }
}
