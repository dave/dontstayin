using System;

namespace Facebook.Api
{
    /// <summary>Represents a user's relationship status.</summary>
    public enum RelationshipStatus : byte
    {
        /// <summary>User's relationship status is unknown or unspecified.</summary>
        Unknown = 0,

        /// <summary>Single.</summary>
        Single = 1,

        /// <summary>In a relationship.</summary>
        InARelationship = 2,

        /// <summary>Engaged.</summary>
        Engaged = 3,

        /// <summary>Married.</summary>
        Married = 4,

        /// <summary>"It's complicated."</summary>
        ItsComplicated = 5,

        /// <summary>In an "open" relationship.</summary>
        InAnOpenRelationship = 6
    }
}
