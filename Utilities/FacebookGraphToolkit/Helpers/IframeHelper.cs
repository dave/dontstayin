using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace FacebookGraphToolkit.Helpers {
    /// <summary>
    /// Provides methods to redirect a user in Iframe Applications
    /// </summary>
    public class IframeHelper {
        /// <summary>
        /// Redirects the user to the specified URL
        /// </summary>
        /// <param name="Url">URL to redirect</param>
        /// <param name="relative">Is the URL relative to Facebook Canvas App address?</param>
        public static void IframeRedirect(string Url, bool relative) {
            string finalUrl = "";
            if (relative) {
                FacebookGraphToolkitConfiguration Config = (FacebookGraphToolkitConfiguration)WebConfigurationManager.GetSection("FacebookGraphToolkitConfiguration");
                finalUrl = Config._FacebookAppAddress + Url;
            } else finalUrl = Url;


            string _script = string.Format("<script type=\"text/javascript\">\n" +
            "if (parent != self) \n" +
            "top.location.href = \"{0}\";\n" +
            "else self.location.href = \"{0}\";\n" +
            "</script>", finalUrl);

            HttpContext.Current.Response.Write(_script);
            HttpContext.Current.Response.End();
        }
    }
}
