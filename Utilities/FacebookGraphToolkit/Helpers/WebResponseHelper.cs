using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using JSON;
using System.Configuration;

namespace FacebookGraphToolkit.Helpers {
    internal class WebResponseHelper {
        /// <summary>
        /// Gets the JSON Object which represents the Facebook Graph Api Object. Not for connections.
        /// </summary>
        /// <param name="ID">Facebook ID</param>
        /// <param name="AccessToken">Graph Api Access Token</param>
        /// <returns></returns>
        internal static JsonObject GetJsonFromFacebookObject(string ID, string AccessToken) {
            string url = string.Format("https://graph.facebook.com/{0}?access_token={1}", ID, AccessToken);
            return new JsonObject(GetWebResponse(url));
        }
        internal static string GetWebResponse(string url) {
            string StringResponse;
            int CacheTime = 0;
            FacebookGraphToolkitConfiguration Config = (FacebookGraphToolkitConfiguration)ConfigurationManager.GetSection("FacebookGraphToolkitConfiguration");
            if (Config != null) CacheTime = Config._CachePolicy._Time;
            if (CacheTime == 0) StringResponse = null;
            else StringResponse = (string)HttpContext.Current.Cache["Web" + url];
            if (StringResponse == null) {
                WebResponse objResponse = GetWebResponseObject(url);
                using (StreamReader sr = new StreamReader(objResponse.GetResponseStream())) {
                    StringResponse = sr.ReadToEnd();
                    sr.Close();
                }
                if (CacheTime != 0) {
                    if (!Config._CachePolicy._SlidingExpiration) HttpContext.Current.Cache.Insert("Web" + url, StringResponse, null, DateTime.Now.AddSeconds(CacheTime), System.Web.Caching.Cache.NoSlidingExpiration);
                    else HttpContext.Current.Cache.Insert("Web" + url, StringResponse, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, CacheTime));
                }
            }
            return StringResponse;
        }
        internal static WebResponse GetWebResponseObject(string url) {
            string RequestURL = url;
            WebRequest Request = HttpWebRequest.Create(RequestURL);
            Request.Timeout = 5000;
            FacebookGraphToolkitConfiguration Config = (FacebookGraphToolkitConfiguration)ConfigurationManager.GetSection("FacebookGraphToolkitConfiguration");
            if (Config != null) if (Config._webtimeout!=0) Request.Timeout = Config._webtimeout;
            try {
                return Request.GetResponse();
            }
            catch (WebException WebEx) {
                if (WebEx.Status == WebExceptionStatus.ProtocolError) {
                    HttpWebResponse ErrorResponse = (HttpWebResponse)WebEx.Response;
                    if (ErrorResponse.StatusCode != HttpStatusCode.BadRequest) throw WebEx;
                    string ErrorType = "";
                    string ErrorMessage = "";
                    using (StreamReader sr = new StreamReader(ErrorResponse.GetResponseStream())) {
                        JsonObject ErrorJO = new JsonObject(sr.ReadToEnd());
                        ErrorType = (string)((JsonObject)ErrorJO["error"])["type"];
                        ErrorMessage = (string)((JsonObject)ErrorJO["error"])["message"];
                        sr.Close();
                    }
                    throw new FacebookException(string.Format("{0} : {1}", ErrorType, ErrorMessage));
                } else throw WebEx;
            }
        }
        internal static string GetWebResponseRedirectURL(string url) {
            string Response = (string)HttpContext.Current.Cache["WebRedirect" + url];
            if (Response == null) {
                WebResponse objResponse = GetWebResponseObject(url);
                Response = objResponse.ResponseUri.ToString();
                //HttpContext.Current.Cache.Insert("WebRedirect" + url, Response, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            return Response;
        }
    }
}
