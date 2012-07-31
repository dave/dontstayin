using System;

namespace Facebook.Api
{
    [Flags]
    public enum MeetingFor : byte
    {
        Unknown = 1,
        Friendship = 2,
        Dating = 4,
        Networking = 8
    }
}
