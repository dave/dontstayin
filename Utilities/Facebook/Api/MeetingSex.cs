using System;

namespace Facebook.Api
{
    [Flags]
    public enum MeetingForGender : byte
    {
        Unknown = 1,
        Men = 2,
        Women = 4
    }
}
