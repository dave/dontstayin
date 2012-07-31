using System;

namespace Facebook.Api
{
    /// <summary>Represents a user's gender</summary>
    public enum Gender : byte
    {
        /// <summary>The user's gender is unknown or not specified.</summary>
        Unknown = 0,

        /// <summary>Male.</summary>
        Male = 1,

        /// <summary>Female.</summary>
        Female = 2
    }
}
