using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Web;

namespace Facebook_Graph_Toolkit {
    public class Helper {
        internal static DateTime ToDateTime(string RFC3339) {
            DateTime date = new DateTime(Convert.ToInt32(RFC3339.Substring(0, 4)),
                Convert.ToInt32(RFC3339.Substring(5, 2)), Convert.ToInt32(RFC3339.Substring(8, 2)),
                Convert.ToInt32(RFC3339.Substring(11, 2)), Convert.ToInt32(RFC3339.Substring(14, 2)),
                Convert.ToInt32(RFC3339.Substring(17, 2)));
            return date;
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
    }
}
