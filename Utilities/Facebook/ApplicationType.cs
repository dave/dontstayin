using System;

namespace Facebook
{
    /// <summary>Represents the type of Facebook application that is communicating with the Facebook API.</summary>
    [Flags]
    public enum ApplicationType : byte
    {
        /// <summary>The application is a Desktop application.</summary>
        Desktop = 1,

        /// <summary>The application is a Website.</summary>
        Website = 2
    }
}
