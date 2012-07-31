using System;

namespace Facebook.Web
{
    public interface IFacebookHttpAccessor
    {
        FacebookHttpRequest FbRequest { get; }
        FacebookHttpSession FbSession { get; }
        FacebookHttpContext FbContext { get; }
    }
}
