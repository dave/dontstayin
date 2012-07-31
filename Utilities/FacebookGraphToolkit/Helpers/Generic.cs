using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JSON;

namespace FacebookGraphToolkit.Helpers {
    internal static class Generic {
        internal static DateTime RFC3339ToDateTime(string RFC3339) {
            DateTime date = new DateTime(Convert.ToInt32(RFC3339.Substring(0, 4)),
                Convert.ToInt32(RFC3339.Substring(5, 2)), Convert.ToInt32(RFC3339.Substring(8, 2)),
                Convert.ToInt32(RFC3339.Substring(11, 2)), Convert.ToInt32(RFC3339.Substring(14, 2)),
                Convert.ToInt32(RFC3339.Substring(17, 2)));
            return date;
        }
        internal static DateTime UnixTimestampToDateTime(double UnixTimestamp) {
            DateTime D = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return D.AddSeconds(UnixTimestamp);
        }
        internal static string Base64UrlDecode(string str) {
            str = str.Replace('+', '-').Replace('/', '_').Trim();
            int pad = str.Length % 4;
            if (pad > 0) {
                pad = 4 - pad;
            }
            str = str.PadRight(str.Length + pad, '=');

            byte[] decbuff = Convert.FromBase64String(str);
            return System.Text.Encoding.UTF8.GetString(decbuff);
        }
        internal static JsonObject ParseSignedRequest(string SignedRequest) {
            int SeperatorIndex = SignedRequest.IndexOf(".");
            string data = SignedRequest.Substring(SeperatorIndex + 1);
            string JSONString = Helpers.Generic.Base64UrlDecode(data);
            return new JsonObject(JSONString);
        }
        internal static string BuildUrlQuery(string url, IDictionary<string, string> parameters) {
            bool firstvalue = false;
            foreach (KeyValuePair<string, string> kvp in parameters) {
                if (string.IsNullOrEmpty(kvp.Value)) continue;
                if (firstvalue) {
                    url += "?" + kvp.Key + "=" + kvp.Value;
                    firstvalue = false;
                } else url += "&" + kvp.Key + "=" + kvp.Value;
            }
            return url;
        }
    }
}
