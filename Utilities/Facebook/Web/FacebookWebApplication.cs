using System;
using System.Web;
using Facebook.Api;

namespace Facebook.Web
{
    [Serializable]
    public class FacebookWebApplication : AppInfo
    {
        private const String APP_STATE_KEY_FORMAT = "__FB_APP_INFO_{0}";

        public FacebookWebApplication(AppInfo appInfo)
            : base(appInfo.XmlContent.Root) { }

        internal static FacebookWebApplication Get(FacebookHttpContext fbContext)
        {
            var key = GetAppStateKey(fbContext.ApiKey);
            return (FacebookWebApplication)HttpContext.Current.Application[key];
        }

        internal static void Init(FacebookHttpContext fbContext)
        {
            FacebookWebApplication.Init(null, fbContext);
        }

        private static void Init(String key, FacebookHttpContext fbContext)
        {
            if (HttpContext.Current.Application[key] == null)
            {
                var appInfoResult = fbContext.Application.GetPublicInfoByApiKey(fbContext.ApiKey);
                if (appInfoResult.IsError) throw appInfoResult.ResponseException;
                else HttpContext.Current.Application[key ?? FacebookWebApplication.GetAppStateKey(fbContext.ApiKey)] = new FacebookWebApplication(appInfoResult.Value);
            }
        }

        private static String GetAppStateKey(String apiKey)
        {
            return String.Format(APP_STATE_KEY_FORMAT, apiKey);
        }
    }
}
