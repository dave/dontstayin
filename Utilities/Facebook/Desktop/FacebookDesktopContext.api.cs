using System;
using Facebook.Api;
using System.Collections.Generic;

namespace Facebook.Desktop
{
    public partial class FacebookDesktopContext : FacebookContextBase<StaticDictionaryCache, SessionInfo>
    {
        private static Dictionary<string, FacebookDesktopContext> _current = new Dictionary<string,FacebookDesktopContext>();
		public static FacebookDesktopContext Current(Facebook.Apps app)
        {
            if (!FacebookDesktopContext._current.ContainsKey(app.ToString()))
            {
				FacebookDesktopContext._current[app.ToString()] = new FacebookDesktopContext(FacebookCommon.Common(app).ApiKey, FacebookCommon.Common(app).Secret);
            }
            return FacebookDesktopContext._current[app.ToString()];
        }
    }
}
