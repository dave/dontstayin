using System;

namespace Facebook
{
    /// <summary>Provides minimal functionality for converting between a Unix timestamp and a <see cref="DateTime" /> value.</summary>
    /// <remarks><para>The <see cref="DateTime" /> struct allows for significanty greater precision than a Unix timestamp. As such,
    /// this struct uses a <see cref="Double" /> to store and convert values, which minimizes the loss in precision when converting to
    /// and from a Unix timestamp.</para></remarks>
    public struct UnixDateTime
    {
        private const Double DIFF = 621355968000000000d;

        private const Double MULTIPLIER = 10000000d;

        /// <summary>Initializes a <see cref="UnixDateTime" /> value using the specified Unix timestamp.</summary>
        /// <param name="unixTime">A <see cref="Double" /> value representing a Unix timestamp.</param>
        public UnixDateTime(Double unixTime)
            : this()
        {
            this.Value = unixTime;
        }

        /// <summary>Initializes a <see cref="UnixDateTime" /> value using the specified Unix timestamp.</summary>
        /// <param name="unixTime">An <see cref="Int64" /> value representing a Unix timestamp.</param>
        public UnixDateTime(Int64 unixTime)
            : this((Double)unixTime) { }

        /// <summary>Initializes a <see cref="UnixDateTime" /> value using the specified Unix timestamp.</summary>
        /// <param name="dateTime">A <see cref="DateTime" /> value that will converted to a Unix timestamp.</param>
        public UnixDateTime(DateTime dateTime)
            : this(((Double)dateTime.Ticks - DIFF) / MULTIPLIER) { }

        /// <summary>Gets or sets the value Unix timestamp value.</summary>
        public Double Value { get; set; }

        /// <summary>Converts the <see cref="UnixDateTime" /> value to a <see cref="DateTime" /> value.</summary>
        /// <returns>A <see cref="DateTime" /> value converted from the <see cref="UnixDateTime" />'s value.</returns>
        public DateTime ToDateTime()
        {
            return UnixDateTime.ToDateTime(this.Value);
        }

        /// <summary>Converts the <see cref="UnixDateTime" /> value to a <see cref="DateTime" /> value.</summary>
        /// <returns>A <see cref="DateTime" /> value converted from the <see cref="UnixDateTime" />'s value.</returns>
        public static DateTime ToDateTime(Double unixTime)
        {
            if (unixTime == 0) return DateTime.MinValue;
            else return new DateTime((Int64)((unixTime * MULTIPLIER) + DIFF), DateTimeKind.Utc);
        }

        /// <summary>Converts the <see cref="String" /> representation of a <see cref="UnixDateTime" /> to its <see cref="UnixDateTime" /> equivalent.</summary>
        /// <param name="input">A <see cref="String" /> containing the vaule to convert.</param>
        /// <param name="unixDateTime">When this method returns, contains the <see cref="UnixDateTime" /> equivalent to the <paramref name="input"/> parameter.</param>
        /// <returns>A <see cref="Boolean" /> value representing whether the conversion succeeded.</returns>
        public static Boolean TryParse(String input, out UnixDateTime unixDateTime)
        {
            Double value;
            if (Double.TryParse(input, out value))
            {
                unixDateTime = new UnixDateTime(value);
                return true;
            }
            else
            {
                unixDateTime = new UnixDateTime(0);
                return false;
            }
        }

        /// <summary>Gets a <see cref="UnixDateTime" /> representing the current date and time.</summary>
        public static UnixDateTime Now
        {
            get { return DateTime.Now.ToUnixDateTime(); }
        }

        /// <summary>Implicitly converts a <see cref="UnixDateTime" /> value to its <see cref="Int64" /> equivalent.</summary>
        /// <param name="unixDateTime">A <see cref="UnixDateTime" /> value.</param>
        /// <returns>A <see cref="Int64" /> value that is the equivalent of the <paramref name="unixDateTime" /> parameter.</returns>
        public static implicit operator Int64(UnixDateTime unixDateTime)
        {
            return (Int64)unixDateTime.Value;
        }

        /// <summary>Implicitly converts a <see cref="UnixDateTime" /> value to its <see cref="Double" /> equivalent.</summary>
        /// <param name="unixDateTime">A <see cref="UnixDateTime" /> value.</param>
        /// <returns>A <see cref="Int64" /> value that is the equivalent of the <paramref name="unixDateTime" /> parameter.</returns>
        public static implicit operator Double(UnixDateTime unixDateTime)
        {
            return unixDateTime.Value;
        }

        /// <summary>Converts the numeric value of this instance to a <see cref="String" />.</summary>
        /// <returns>A <see cref="String" /> containing the numeric value of this instance.</returns>
        public override String ToString()
        {
            return this.Value.ToString();
        }
    }
}
