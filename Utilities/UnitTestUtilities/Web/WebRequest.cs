using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace UnitTestUtilities.Web
{
    public class WebRequest
    {
        public WebRequest(Uri uri, int timeoutInMs)
        {
            HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(uri);
            request.Method = "GET";
            request.Timeout = timeoutInMs;
            request.UserAgent = "Mozilla/4.0+";
            request.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.NoCacheNoStore);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader responseReader = new StreamReader(responseStream);
            this.Response = responseReader.ReadToEnd();
        }
        public WebRequest(string address, int timeoutInMs)
            : this(new Uri(address), timeoutInMs)
        {

        }
        public string Response { get; set; }
    }
}
