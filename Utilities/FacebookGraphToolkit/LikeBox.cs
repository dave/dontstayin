using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using JSON;
using System.Net;
using System.Web.UI.WebControls;

namespace FacebookGraphToolkit {
    /// <summary>
    /// A LikeBox control
    /// </summary>
    [ToolboxData("<{0}:LikeBox runat=server></{0}:LikeBox>")]
    public class LikeBox : WebControl {

        #region Private Variables
        private string _FacebookID = null;
        private int? _Limit=null;
        private int _FeedHeight = 300;
        private int _Padding = 10;
        private LikeBoxMode _LikeBoxMode = LikeBoxMode.Both;
        private string _DateFormatString = "g";
        private TimeSpan _UTCOffset = new TimeSpan(0, 0, 0);
        private bool _ShowTitleInFeed = true;

        private Style _HeaderStyle = new Style();
        private Style _HeaderCaptionStyle = new Style();

        //private JsonObject MetadataJO;
        private GraphApi.Page FacebookPage;
        #endregion

        #region Attributes

        #region Data
        /// <summary>
        /// Facebook Page ID
        /// </summary>
        [Bindable(true)] 
        [Category("Data")] 
        [DefaultValue("")] 
        [Localizable(false)]
        public string FacebookID {
            get {
                return _FacebookID;
            }
            set {
                _FacebookID = value;
            }
        }

        /// <summary>
        /// Maximum number of posts to display
        /// </summary>
        [Bindable(true)] 
        [Category("Data")] 
        [Localizable(true)]
        [Description("Maximum number of posts to display")]
        [DefaultValue(10)]
        public int? Limit {
            get {
                return _Limit;
            }
            set {
                _Limit = value;
            }
        }

        #endregion

        #region Layout
        /// <summary>
        /// How the LikeBox is rendered
        /// </summary>
        [Bindable(true)]
        [Category("Layout")]
        [Localizable(true)]
        [Description("Display mode")]
        public LikeBoxMode LikeBoxMode {
            get {
                return _LikeBoxMode;
            }
            set {
                _LikeBoxMode = value;
            }
        }

        /// <summary>
        /// Whether to show Page's title in feeds
        /// </summary>
        [Bindable(true)]
        [Category("Layout")]
        [Localizable(true)]
        [Description("Whether to show Page's title in feeds")]
        public bool ShowTitleInFeed {
            get {
                return _ShowTitleInFeed;
            }
            set {
                _ShowTitleInFeed = value;
            }
        }

        private bool _ShowLogo = true;
        /// <summary>
        /// Whether to show Page's logo
        /// </summary>
        [Bindable(true)]
        [Category("Layout")]
        [Localizable(true)]
        [Description("Whether to show Page's logo")]
        public bool ShowLogo {
            get {
                return _ShowLogo;
            }
            set {
                _ShowLogo = value;
            }
        }

        #endregion

        #region Appearance

        /// <summary>
        /// Height of Feed Box
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Localizable(true)]
        [Description("Height of Feed Box")]
        [DefaultValue(300)]
        public int FeedHeight {
            get {
                return _FeedHeight;
            }
            set {
                _FeedHeight = value;
            }
        }

        /// <summary>
        /// Padding of contents
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Localizable(true)]
        [Description("Padding of contents")]
        [DefaultValue(10)]
        public int Padding {
            get {
                return _Padding;
            }
            set {
                _Padding = value;
            }
        }

        /// <summary>
        /// Format String of Posts' pulishing dates
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Localizable(true)]
        [Description("Format String of Posts' publishing DateTimes")]
        public string DateFormatString {
            get {
                return _DateFormatString;
            }
            set {
                _DateFormatString = value;
            }
        }

        /// <summary>
        /// UTC Offset of publishing DateTimes
        /// </summary>
        [Bindable(true)]
        [Category("Appearance")]
        [Localizable(true)]
        [Description("UTC Offset of publishing DateTimes")]
        public TimeSpan UTCOffset {
            get {
                return _UTCOffset;
            }
            set {
               _UTCOffset = value;
            }
        }

        /// <summary>
        /// Style of the Header of the LikeBox, which displays the Page's information
        /// </summary>
        [Category("Appearance")]
        [Description("Header Style")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public Style HeaderStyle {
            get {
                return _HeaderStyle;
            }
            set {
                _HeaderStyle = value;
            }
        }

        /// <summary>
        /// Style of Page title in Header
        /// </summary>
        [Category("Appearance")]
        [Description("Style of page title in Header")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public Style HeaderCaptionStyle {
            get {
                return _HeaderCaptionStyle;
            }
            set {
                _HeaderCaptionStyle = value;
            }
        }

        private Style _LinkStyle = new Style();
        /// <summary>
        /// Style of links to Facebook Page
        /// </summary>
        [Category("Appearance")]
        [Description("Link Style")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public Style LinkStyle {
            get {
                return _LinkStyle;
            }
            set {
                _LinkStyle = value;
            }
        }

        private Style _AttachmentStyle = new Style();
        /// <summary>
        /// Style of attachment in posts
        /// </summary>
        [Category("Appearance")]
        [Description("Attachment Style")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public Style AttachmentStyle {
            get {
                return _AttachmentStyle;
            }
            set {
                _AttachmentStyle = value;
            }
        }

        private Style _DateStyle = new Style();
        /// <summary>
        /// Style of publishing dates
        /// </summary>
        [Category("Appearance")]
        [Description("Attachment Style")]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public Style DateStyle {
            get {
                return _DateStyle;
            }
            set {
                _DateStyle = value;
            }
        }
        #endregion

        #endregion

        /// <summary>
        /// Initializes a LikeBox control
        /// </summary>
        public LikeBox() : base(HtmlTextWriterTag.Div) { }

        /// <summary>
        /// Initialize Html attributes of the control
        /// </summary>
        /// <param name="writer"></param>
        protected override void AddAttributesToRender(HtmlTextWriter writer) {

            writer.AddStyleAttribute(HtmlTextWriterStyle.Width, "300px");
            writer.AddStyleAttribute(HtmlTextWriterStyle.BorderColor, "#1B8BFC");
            writer.AddStyleAttribute(HtmlTextWriterStyle.BorderStyle, "Solid");
            writer.AddStyleAttribute(HtmlTextWriterStyle.BorderWidth, "1px");
            writer.AddStyleAttribute(HtmlTextWriterStyle.FontFamily, "Tahoma");
            writer.AddStyleAttribute(HtmlTextWriterStyle.FontSize, "11px");
            base.AddAttributesToRender(writer);

        }
        
        /// <summary>
        /// Render the control
        /// </summary>
        /// <param name="output"></param>
        protected override void RenderContents(HtmlTextWriter output) {
            //output.Write(String.Format("<div style=\"border: {0}px solid #1B8BFC;width:{1}px;\">",BorderWidth, _Width));
            if (_FacebookID != null) _FacebookID = _FacebookID.Trim();
            if (string.IsNullOrEmpty(_FacebookID)) throw new ArgumentNullException("FacebookID", "Required attribute missing.");

            try {
                //string Metadata = Helpers.WebResponseHelper.GetWebResponse(String.Format("https://graph.facebook.com/{0}", FacebookID));
                //MetadataJO = new JsonObject(Metadata);

                FacebookPage = new GraphApi.Page(FacebookID);

                if (_LikeBoxMode != LikeBoxMode.FeedOnly) OutputHeader(output);

                if (_LikeBoxMode != LikeBoxMode.HeaderOnly) OutputFeed(output);

                
            }
            catch (WebException ex) {
                output.Write(ex.Message);
            }

            //output.Write("</div>");
        }
        private void OutputHeader(HtmlTextWriter output) {
            string ImgUrl = Helpers.WebResponseHelper.GetWebResponseRedirectURL(String.Format("https://graph.facebook.com/{0}/picture", FacebookID));

            HtmlTextWriter H = new HtmlTextWriter(output.InnerWriter);
            H.AddStyleAttribute(HtmlTextWriterStyle.BackgroundColor, "#DFF1FF");
            HeaderStyle.AddAttributesToRender(H);
            H.AddStyleAttribute(HtmlTextWriterStyle.Width, "100%");


            H.RenderBeginTag(HtmlTextWriterTag.Div);

            H.Write(String.Format("<div style=\"padding:{0}px\">",_Padding)+
                "<table><tr>");
            if (ShowLogo) H.Write(String.Format("<td><img src=\"{0}\" alt=\"\"/></td>", ImgUrl));
            H.Write("<td style=\"width:100%\">");

            Style DefaultHeaderCaptionStyle = new Style();
            DefaultHeaderCaptionStyle.Font.Size = FontUnit.Parse("150%");
            HeaderCaptionStyle.MergeWith(LinkStyle);
            HeaderCaptionStyle.MergeWith(DefaultHeaderCaptionStyle);

            H.AddAttribute("href", FacebookPage.link);
            H.EnterStyle(HeaderCaptionStyle, HtmlTextWriterTag.A);
            //WriteLink((string)MetadataJO["name"], (string)MetadataJO["link"], H);
            H.Write(FacebookPage.name);
            H.ExitStyle(HeaderCaptionStyle, HtmlTextWriterTag.A);

            H.Write(" on Facebook");
            H.Write("</td></tr></table></div>");

            H.RenderEndTag();
        }
        private void OutputFeed(HtmlTextWriter output) {
            string Posts = Helpers.WebResponseHelper.GetWebResponse(String.Format("https://graph.facebook.com/{0}/posts?{1}", FacebookID, _Limit != null ? "limit=" + _Limit.ToString() : ""));

            JsonObject PostJO = new JsonObject(Posts);

            JsonArray PostJA = (JsonArray)PostJO["data"];

            output.Write(String.Format("<div style=\"width:100%; height:{0}px; overflow:scroll; overflow-x:hidden;\">", _FeedHeight));
            output.Write(String.Format("<div style=\"padding:{0}px\">", _Padding));
            for (int i = 0; i < PostJA.JsonObjects.Count; i++) {
                JsonObject Post = PostJA.JsonObjects[i];
                JsonObject From = (JsonObject)Post["from"];
                if ((string)From["id"] == FacebookPage.id) {
                    if (ShowTitleInFeed) {
                        WriteLink(FacebookPage.name, FacebookPage.link, output);
                        output.Write(" ");
                    }
                    output.Write((string)Post["message"] + "<br />");
                    if ((string)Post["picture"] != null) output.Write(String.Format("<img src=\"{0}\" style=\"margin-left:10px\"/><br />", Post["picture"]));
                    if ((string)Post["type"] == "link") WriteLinkAttachment((string)Post["name"], (string)Post["link"], (string)Post["caption"], (string)Post["description"],output);
                    DateTime date = Helpers.Generic.RFC3339ToDateTime((string)Post["created_time"]);
                    output.EnterStyle(DateStyle);
                    output.Write(date.Add(UTCOffset).ToString(DateFormatString));
                    output.ExitStyle(DateStyle);
                }
                if (i != PostJA.JsonObjects.Count - 1) output.Write("<hr />");
            }
            output.Write("</div>");
            output.Write("</div>");

            //output.Write(JsonHelper.JsonToHtml(PostJO));
        }
        private void WriteLink(string name, string href, HtmlTextWriter output) {
            output.AddAttribute("href", href);
            output.EnterStyle(LinkStyle,HtmlTextWriterTag.A);
            output.Write(name);
            output.ExitStyle(LinkStyle,HtmlTextWriterTag.A);
            
        }
        private void WriteLinkAttachment(string name, string link, string caption, string description, HtmlTextWriter output) {
            output.Write("<div style=\"margin-left:10px\">");
            WriteLink(name, link, output);
            output.Write("<br />");
            //html += FormatLink(name, link, output) + "<br />";
            output.EnterStyle(AttachmentStyle);
            output.Write(caption);
            output.Write("<br />");
            output.Write(description);
            output.ExitStyle(AttachmentStyle);
            output.Write("</div>");
            //html += "<span class=\"LikeBox_Attachment\">" + caption + "</span><br />";
            //html += "<span class=\"LikeBox_Attachment\">" + description + "</span><br />";
            //html += "</div>";
        }
    }
    /// <summary>
    /// How the LikeBox is rendered
    /// </summary>
    public enum LikeBoxMode {
        /// <summary>
        /// Only renders the Page's information
        /// </summary>
        HeaderOnly,
        /// <summary>
        /// Only renders the Page's feed
        /// </summary>
        FeedOnly,
        /// <summary>
        /// Renders both the Page's information and feed
        /// </summary>
        Both };
}
