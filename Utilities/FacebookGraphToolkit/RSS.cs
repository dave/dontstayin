using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using JSON;

namespace FacebookGraphToolkit {
    /// <summary>
    /// To be used in a Generic Handler class to provide RSS funtionality
    /// </summary>
    public class RSS{
        /// <summary>
        /// Render a RSS feed from a Facebook Page's Posts on its Wall
        /// </summary>
        /// <param name="context">HttpContext object</param>
        /// <param name="FacebookID">Page ID</param>
        public static void RenderRSSFeed(HttpContext context, string FacebookID) {
            string StringResponse = GetWebResponse(String.Format("https://graph.facebook.com/{0}/posts",FacebookID));
            StringResponse = StringResponse.Replace("&", "&amp;");
            //string Metadata = GetWebResponse(String.Format("https://graph.facebook.com/{0}",FacebookID));
            GraphApi.Page FacebookPage = new GraphApi.Page(FacebookID);

            JsonObject JO = new JsonObject(StringResponse);

            //JsonObject MetadataJO = new JsonObject(Metadata);

            context.Response.Clear();
            context.Response.ContentType = "application/rss+xml";

            //context.Response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>");
            context.Response.Write("<rss version=\"2.0\">");
            context.Response.Write("<channel>");
            context.Response.Write(WriteTag("title", FacebookPage.name));
            context.Response.Write(WriteTag("link", FacebookPage.link));
            context.Response.Write(WriteTag("description", "some description"));
            context.Response.Write(WriteTag("category", "link/photo/status/video"));
            if (FacebookPage.picture != null) {
                context.Response.Write("<image>");
                context.Response.Write(WriteTag("url", FacebookPage.picture));
                context.Response.Write(WriteTag("title", FacebookPage.name));
                context.Response.Write(WriteTag("link", FacebookPage.link));
                context.Response.Write("</image>");
            }

            JsonArray Posts = (JsonArray)JO["data"];
            foreach (JsonObject Post in Posts.JsonObjects) {
                context.Response.Write("\r\n");
                context.Response.Write("<item>");

                if (Post["name"] != null) context.Response.Write(WriteTag("title", (string)Post["name"]));
                else context.Response.Write(WriteTag("title", FacebookPage.name));

                if (Post["link"] != null) context.Response.Write(WriteTag("link", (string)Post["link"]));
                else context.Response.Write(WriteTag("link", FacebookPage.link));

                //description
                context.Response.Write("<description><![CDATA[");

                if (Post["picture"] != null) context.Response.Write(String.Format("<img src=\"{0}\" />", (string)Post["picture"]));

                context.Response.Write(WriteTag("p", (string)Post["message"]));

                if (Post["description"] != null) {
                    context.Response.Write("<p style=\"color:#505050\">");
                    context.Response.Write((string)Post["description"]);
                    context.Response.Write("</p>");
                }

                context.Response.Write("]]></description>");

                context.Response.Write(WriteTag("category", (string)Post["type"]));
                context.Response.Write(WriteTag("pubDate", Helpers.Generic.RFC3339ToDateTime((string)Post["created_time"]).ToString("d MMM yyyy HH:mm:ss")));
                context.Response.Write("</item>");
            }

            context.Response.Write("</channel>");
            context.Response.Write("</rss>");
            context.Response.End();
        }
        private static string WriteTag(string tag, string message) {
            return String.Format("<{0}>{1}</{0}>", tag, message);
        }
        private static string GetWebResponse(string url) {
            string StringResponse = null;
            string RequestURL = url;
            WebRequest Request = HttpWebRequest.Create(RequestURL);
            WebResponse objResponse = Request.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream())) {
                StringResponse = sr.ReadToEnd();
                sr.Close();
            }
            return StringResponse;
        }
    }
}
