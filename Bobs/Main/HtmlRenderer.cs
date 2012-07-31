using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web.UI;
using System.Text.RegularExpressions;
using Sgml;
using Bobs;

namespace Bobs
{
	public class HtmlRenderer
	{
		string rawHtml;
		public bool Formatting { get; set; }
		public bool Container { get; set; }
		public bool RenderAllFlashTags { get; set; }
		public bool RenderFlashTagsRaw { get; set; }

		#region AddPTagsWhenRenderingFormattedHtmlInContainer
		public bool AddPTagsWhenRenderingFormattedHtmlInContainer
		{
			get
			{
				return addPTagsWhenRenderingFormattedHtmlInContainer;
			}
			set
			{
				addPTagsWhenRenderingFormattedHtmlInContainer = value;
			}
		}
		bool addPTagsWhenRenderingFormattedHtmlInContainer = true;
		#endregion

        #region Html and Script to render
		private bool hasAlreadyRendered;
		private string script;
		public string Script
		{
			get
			{
				if (!hasAlreadyRendered)
				{
					Render();
				}
				return script;
			}
			private set { script = value; }
		}
		private string html;
		public string Html
		{
			get
			{
				if (!hasAlreadyRendered)
				{
					Render();
				}
				return html;
			}
			private set { html = value; }
		}
		#endregion
        
		public HtmlRenderer()
		{
			Formatting = true;
			Container = true;
		}

		#region LoadHtml
		public void LoadHtml(string rawHtml)
		{
			if (rawHtml.StartsWith("<dsi:html"))
			{
				//parse and remove dsi:html tag...
				string tag = rawHtml.Substring(0, rawHtml.IndexOf('>') + 1) + "</dsi:html>";
				rawHtml = rawHtml.Substring(rawHtml.IndexOf('>') + 1);
				rawHtml = rawHtml.Substring(0, rawHtml.Length - 11);

				/*
				<dsi:html 
					formatting = [true | false] // do we convert line-breaks to br tags?
					container = [true | false]  // do we render the html in a container div?
					></dsi:html>
				*/

				SgmlReader sgml = new SgmlReader();
				sgml.InputStream = new StringReader(tag);
				sgml.DocType = "HTML";
				sgml.Read();

				if (sgml.GetAttribute("formatting") != null)
					Formatting = bool.Parse(sgml.GetAttribute("formatting"));

				if (sgml.GetAttribute("container") != null)
					Container = bool.Parse(sgml.GetAttribute("container"));

			}

			this.rawHtml = rawHtml;
		}
		#endregion
		#region Render
		public string Render(Control controlForScripts)
		{
			return Render(controlForScripts, false);
		}
		public string Render(Control controlForScripts, bool returnAllScriptInline)
		{
			if (!returnAllScriptInline)
			{
				if (controlForScripts != null)
					ScriptManager.RegisterStartupScript(controlForScripts, typeof(Page), Guid.NewGuid().ToString("N"), this.Script, true);
			}

			return this.Html + (returnAllScriptInline ? "<script>" + this.Script + "</script>" : ""); 
		}
		private void Render()
		{
			hasAlreadyRendered = true;

			if (rawHtml.Contains("<dsi:"))
			{
				Regex dsiTagRegex = new Regex(@"<dsi:(video|audio|flash|object).*?/>");
				MatchEvaluator dsiTagMatchEval = dsiTagReplacement;
				rawHtml = dsiTagRegex.Replace(rawHtml, dsiTagMatchEval);
			}
			if (rawHtml.Contains("<dsi:quote") || rawHtml.Contains("<dsi:link"))
			{
				Regex linkRegex = new Regex(@"<dsi:(link|quote).*?>");
				MatchEvaluator linkMatchEval = new MatchEvaluator(dsiTagReplacement);
				rawHtml = linkRegex.Replace(rawHtml, linkMatchEval);
				rawHtml = rawHtml.Replace("</dsi:link>", "</a>");
				rawHtml = rawHtml.Replace("</dsi:quote>", "</div>");
			}
			Regex protocolRegex = new Regex(@"~(https?://)", RegexOptions.IgnoreCase);
			rawHtml = protocolRegex.Replace(rawHtml, "$1");

			if (Formatting)
			{
				string start = Container && AddPTagsWhenRenderingFormattedHtmlInContainer ? "<p>" : "";
				string end = Container && AddPTagsWhenRenderingFormattedHtmlInContainer ? "</p>" : "";

				//\n's following the tags below should be converted to <br>'s
				//\n's following all oter tags (block / invisible) should not be convetrted into <br>'s
				//Inline elements: a|b|big|em|font|i|img|s|small|spacer|span|strike|strong|sub|sup|u
				//Block / invisible elements: area|blockquote|br|center|col|colgroup|div|h1|h2|h3|h4|h5|h6|hr|li|map|ol|p|pre|table|tbody|td|tfoot|th|thead|tr|ul
				
				rawHtml = rawHtml.Trim();

				//Lets remove any \n's directly before or after block / invisible elements
				//Regex r = new Regex(@"(?:\r?\n)?([ \t]*</?(area|blockquote|br|center|col|colgroup|div|h1|h2|h3|h4|h5|h6|hr|li|map|ol|p|pre|table|tbody|td|tfoot|th|thead|tr|ul)[^>]*>[ \t]*)(?:\r?\n)?", RegexOptions.IgnoreCase | RegexOptions.Multiline);
				Regex r = new Regex(@"([ \t]*</?(area|blockquote|br|center|col|colgroup|div|h1|h2|h3|h4|h5|h6|hr|li|map|ol|p|pre|table|tbody|td|tfoot|th|thead|tr|ul)[^>]*>[ \t]*)(?:\r?\n)?", RegexOptions.IgnoreCase | RegexOptions.Multiline);
				rawHtml = r.Replace(rawHtml, "$1");

				//...then change the remaining \n's for <br \>'s
				Regex r1 = new Regex(@"\r?\n");
				rawHtml = r1.Replace(rawHtml, "<br />\r\n");

				this.Html = start + rawHtml + end;

			}
			else
				this.Html = rawHtml;
		}
		#endregion
		#region dsiTagReplacement
		public string dsiTagReplacement(Match m)
		{
			string tagName = "dsi";
			try
			{
				//string[] arrParts = m.Groups[1].Value.Split[" "];
				//Dictionary<string, string> parts = new Dictionary<string, string>();
				SgmlReader sgml = new SgmlReader();
				string inStr = m.Groups[0].Value;
				if (inStr.StartsWith("<dsi:link"))
					inStr += "</dsi:link>";
				sgml.InputStream = new StringReader(inStr);
				sgml.DocType = "HTML";
				sgml.Read();

				tagName = sgml.Name;
				string uniqueId = Guid.NewGuid().ToString("N");

				#region Parse attributes
				Dictionary<string, string> attributes = new Dictionary<string, string>();
				while (sgml.MoveToNextAttribute())
				{
					attributes.Add(sgml.Name.ToLower(), sgml.Value);
				}
				#endregion

				string typeAtt = attributes.ContainsKey("type") ? attributes["type"] : null;
				string refAtt = attributes.ContainsKey("ref") ? attributes["ref"] : null;

				#region Parse styles
				Dictionary<string, string> style = new Dictionary<string, string>();
				if (attributes.ContainsKey("style"))
				{
					foreach (string s in attributes["style"].Split(';'))
					{
						try
						{
							if (s.Contains(":"))
								style[s.Split(':')[0].Trim()] = s.Split(':')[1].Trim();
						}
						catch
						{
						}
					}
				}
				#endregion

				#region Parse class
				List<string> classes = new List<string>();
				if (attributes.ContainsKey("class"))
				{
					foreach (string s in attributes["class"].Split(' '))
					{
						try
						{
							classes.Add(s);
						}
						catch
						{
						}
					}
				}
				#endregion

				if (tagName == "dsi:video")
				{
					#region dsi:video
					/*
					<dsi:video 
						type = [dsi | flv | youtube | google | metacafe | myspace | break | collegehumor | redtube | ebaumsworld | dailymotion] 
						ref = [dsi-photo-k | site-ref]
						src = [flv-url]
						width = [width] (optional)
						height = [height] (optional)
						nsfw = [true | false] (optional)
						/> 
					*/
					bool nsfw = attributes.ContainsKey("nsfw") ? bool.Parse(attributes["nsfw"].ToLower()) : false;
					string draw = attributes.ContainsKey("draw") ? attributes["draw"].ToLower() : "auto";
					if (typeAtt == "youtube")
					{
						#region youtube
						int width = attributes.ContainsKey("width") ? int.Parse(attributes["width"]) : 425;
						int height = attributes.ContainsKey("height") ? int.Parse(attributes["height"]) : 355;

						//<object width="425" height="355"><param name="movie" value="http://www.youtube.com/v/8VtWo8tFdPQ&rel=1"></param><param name="wmode" value="transparent"></param><embed src="http://www.youtube.com/v/8VtWo8tFdPQ&rel=1" type="application/x-shockwave-flash" wmode="transparent" width="425" height="355"></embed></object>
						return GetFlash(uniqueId, height, width, nsfw, draw, "http://www.youtube.com/v/" + refAtt + "&rel=1");
						#endregion
					}
					else if (typeAtt == "metacafe")
					{
						#region metacafe
						int width = attributes.ContainsKey("width") ? int.Parse(attributes["width"]) : 400;
						int height = attributes.ContainsKey("height") ? int.Parse(attributes["height"]) : 345;

						//<embed src="http://www.metacafe.com/fplayer/1029494/how_to_make_fire_balls.swf" width="400" height="345" wmode="transparent" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash"> </embed><br><font size = 1><a href="http://www.metacafe.com/watch/1029494/how_to_make_fire_balls/">How To Make Fire Balls</a> - <a href="http://www.metacafe.com/">The funniest videos clips are here</a></font>
						return GetFlash(uniqueId, height, width, nsfw, draw, "http://www.metacafe.com/fplayer/" + refAtt + ".swf");
						#endregion
					}
					else if (typeAtt == "google")
					{
						#region google
						int width = attributes.ContainsKey("width") ? int.Parse(attributes["width"]) : 400;
						int height = attributes.ContainsKey("height") ? int.Parse(attributes["height"]) : 326;

						//<embed style="width:400px; height:326px;" id="VideoPlayback" type="application/x-shockwave-flash" src="http://video.google.com/googleplayer.swf?docId=-7477616603879486362&hl=en-GB" flashvars=""> </embed>
						return GetFlash(uniqueId, height, width, nsfw, draw, "http://video.google.com/googleplayer.swf?docId=" + refAtt + "&hl=en-GB");
						#endregion
					}
					else if (typeAtt == "flv")
					{
						#region flv
						string flvUrl = attributes.ContainsKey("src") ? attributes["src"] : null;
						int width = attributes.ContainsKey("width") ? int.Parse(attributes["width"]) : 450;
						int height = attributes.ContainsKey("height") ? (int.Parse(attributes["height"]) + 20) : 357;

						return GetFlash(uniqueId, height, width, nsfw, draw, "/misc/flvplayer.swf", "file", flvUrl, "autoStart", "0");
						#endregion
					}
					else if (typeAtt == "dsi")
					{
						#region dsi
						try
						{
							Photo p = new Photo(int.Parse(refAtt));
							if (p.MediaType != Photo.MediaTypes.Video)
							{
								return "[Invalid ref " + refAtt + " - this is not a video]";
							}
							else
							{
								if (p.ContentDisabled)
								{
									return "[Invalid ref " + refAtt + " - video disabled]";
								}
								else
								{
									if (p.Status == Photo.StatusEnum.Enabled)
									{
										//int width = p.VideoMedWidth;
										//int height = p.VideoMedHeight + 20;

										int width = attributes.ContainsKey("width") ? int.Parse(attributes["width"]) : p.VideoMedWidth;
										int height = attributes.ContainsKey("height") ? (int.Parse(attributes["height"]) + 20) : (p.VideoMedHeight + 20);

										return GetFlash(uniqueId, height, width, nsfw, draw, "/misc/flvplayer.swf", "file", p.VideoMedPath, "autoStart", "0", "jpg", p.WebPath);
									}
									else if (p.Status == Photo.StatusEnum.Moderate)
									{
										return "[Invalid ref " + refAtt + " - video waiting for moderation]";
									}
									else if (p.Status == Photo.StatusEnum.Processing)
									{
										return "[Invalid ref " + refAtt + " - video still processing]";
									}
								}
							}
						}
						catch
						{
							return "[Invalid ref " + refAtt + " - video not found]";
						}
						return "[Invalid ref " + refAtt + " - error]";
						#endregion
					}
					else if (typeAtt == "collegehumor")
					{
						#region collegehumor
						int width = attributes.ContainsKey("width") ? int.Parse(attributes["width"]) : 450;
						int height = attributes.ContainsKey("height") ? int.Parse(attributes["height"]) : 337;

						//<object type="application/x-shockwave-flash" data="http://www.collegehumor.com/moogaloop/moogaloop.swf?clip_id=1754304&fullscreen=1" width="480" height="360" ><param name="allowfullscreen" value="true" /><param name="movie" quality="best" value="http://www.collegehumor.com/moogaloop/moogaloop.swf?clip_id=1754304&fullscreen=1" /></object>
						return GetFlash(uniqueId, height, width, nsfw, draw, "http://www.collegehumor.com/moogaloop/moogaloop.swf?clip_id=" + refAtt + "&fullscreen=1");
						#endregion
					}
					else if (typeAtt == "myspace")
					{
						#region myspace
						int width = attributes.ContainsKey("width") ? int.Parse(attributes["width"]) : 430;
						int height = attributes.ContainsKey("height") ? int.Parse(attributes["height"]) : 346;

						//<embed src="http://lads.myspace.com/videos/vplayer.swf" flashvars="m=25330587&v=2&type=video" type="application/x-shockwave-flash" width="430" height="346"></embed>
						return GetFlash(uniqueId, height, width, nsfw, draw, "http://lads.myspace.com/videos/vplayer.swf", "m", refAtt, "v", "2", "type", "video");
						#endregion
					}
					else if (typeAtt == "break")
					{
						#region break
						int width = attributes.ContainsKey("width") ? int.Parse(attributes["width"]) : 464;
						int height = attributes.ContainsKey("height") ? int.Parse(attributes["height"]) : 392;

						//<object width="464" height="392"><param name="movie" value="http://embed.break.com/NDMyNjg3"></param><embed src="http://embed.break.com/NDMyNjg3" type="application/x-shockwave-flash" width="464" height="392"></embed></object>
						return GetFlash(uniqueId, height, width, nsfw, draw, "http://embed.break.com/" + refAtt);
						#endregion
					}
					else if (typeAtt == "redtube")
					{
						#region redtube
						int width = attributes.ContainsKey("width") ? int.Parse(attributes["width"]) : 434;
						int height = attributes.ContainsKey("height") ? int.Parse(attributes["height"]) : 344;

						//<object height="344" width="434"><param name="movie" value="http://embed.redtube.com/player/"><param name="FlashVars" value="id=2394&style=redtube"><embed src="http://embed.redtube.com/player/?id=2394&style=redtube" pluginspage="http://www.adobe.com/shockwave/download/download.cgi?P1_Prod_Version=ShockwaveFlash" type="application/x-shockwave-flash" height="344" width="434"></object>
						return GetFlash(uniqueId, height, width, true, draw, "http://embed.redtube.com/player/?id=" + refAtt + "&style=redtube", "id", refAtt, "style", "redtube");
						#endregion
					}
					else if (typeAtt == "ebaumsworld")
					{
						#region ebaumsworld
						int width = attributes.ContainsKey("width") ? int.Parse(attributes["width"]) : 425;
						int height = attributes.ContainsKey("height") ? int.Parse(attributes["height"]) : 345;

						//<embed src="http://www.ebaumsworld.com/mediaplayer.swf" flashvars="file=http://media.ebaumsworld.com/2008/01/trouble-leaving-parking-lot.flv&displayheight=321&image=http://media.ebaumsworld.com/2008/01/trouble-leaving-parking-lot.jpg" loop="false" menu="false" quality="high" bgcolor="#ffffff" width="425" height="345" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />
						return GetFlash(uniqueId, height, width, nsfw, draw, "http://www.ebaumsworld.com/mediaplayer.swf", "file", "http://media.ebaumsworld.com/" + refAtt + ".flv", "displayheight", (height - 24).ToString(), "image", "http://media.ebaumsworld.com/" + refAtt + ".jpg");
						#endregion
					}
					else if (typeAtt == "dailymotion")
					{
						#region dailymotion
						int width = attributes.ContainsKey("width") ? int.Parse(attributes["width"]) : 420;
						int height = attributes.ContainsKey("height") ? int.Parse(attributes["height"]) : 331;

						//<div><object width="420" height="331"><param name="movie" value="http://www.dailymotion.com/swf/x3xmzx"></param><param name="allowFullScreen" value="true"></param><param name="allowScriptAccess" value="always"></param><embed src="http://www.dailymotion.com/swf/x3xmzx" type="application/x-shockwave-flash" width="420" height="331" allowFullScreen="true" allowScriptAccess="always"></embed></object><br /><b><a href="http://www.dailymotion.com/video/x3xmzx_time-attack-evo-crash-knockhill-200_auto">TIME ATTACK EVO CRASH KNOCKHILL 2007</a></b><br /><i>Uploaded by <a href="http://www.dailymotion.com/TIMEATTACKTV">TIMEATTACKTV</a></i></div>
						return GetFlash(uniqueId, height, width, nsfw, draw, "http://www.dailymotion.com/swf/" + refAtt);
						#endregion
					}
					else if (typeAtt == "veoh")
					{
						return "[Veoh videos disabled]";
						#region veoh
						int width = attributes.ContainsKey("width") ? int.Parse(attributes["width"]) : 450;
						int height = attributes.ContainsKey("height") ? int.Parse(attributes["height"]) : 365;

						//<embed src="http://www.veoh.com/videodetails2.swf?player=videodetailsembedded&type=v&permalinkId=v1644215kQ3H8PG2&id=anonymous" allowFullScreen="true" width="540" height="438" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer"></embed><br/><a href="http://www.veoh.com/">Online Videos by Veoh.com</a>
						return GetFlash(uniqueId, height, width, nsfw, draw, "http://www.veoh.com/videodetails2.swf?player=videodetailsembedded&type=v&permalinkId=" + refAtt + "&id=anonymous");
						#endregion
					}
					else
					{
						return "[Invalid type attribute]";
					}
					#endregion
				}
				else if (tagName == "dsi:audio")
				{
					#region dsi:audio
					/*
					<dsi:audio 
						type = [mp3]
						src = [mp3-url]
						nsfw = [true | false] (optional)
					/>
					*/
					if (typeAtt == "mp3")
					{
						#region mp3
						string mp3Url = attributes.ContainsKey("src") ? attributes["src"] : null;
						int width = attributes.ContainsKey("width") ? int.Parse(attributes["width"]) : 290;
						int height = attributes.ContainsKey("height") ? int.Parse(attributes["height"]) : 24;

						string audioPlayerSwfPath = Storage.Path(new Guid("7abb3119-f8ad-43ff-be01-764b2ae111fc"), "swf", Storage.Stores.Pix);

						return GetFlash(uniqueId, height, width, false, "load", audioPlayerSwfPath, "soundFile", mp3Url, "autoStart", "no") +
								@"<a href=""" + mp3Url + @"""><img src=""/gfx/download-button2.png"" width=""73"" height=""16"" border=""0"" /></a>";
						#endregion
					}
					else
					{
						return "[Invalid type attribute]";
					}
					#endregion
				}
				else if (tagName == "dsi:flash")
				{
					#region dsi:flash
					/*
					<dsi:flash 
						src = [swf-url]
						width = [width] 
						height = [height]
						nsfw = [true | false] (optional)
						play = [true | false] (optional)
						loop = [true | false] (optional)
						menu = [true | false] (optional)
						quality = [low | autolow | autohigh | medium | high | best] (optional)
						scale = [default | noorder | exactfit] (optional)
						align = [l | t | r | b] (optional)
						salign = [l | t | r | b | tl | tr | bl | br] (optional)
						wmode = [window | opaque | transparent] (optional)
						bgcolor = [colour] (optional)
						base = [base-url] (optional)
						flashvars = [flashvars] (optional)
						/>
					*/
					string swfUrl = attributes.ContainsKey("src") ? attributes["src"] : null;
					return getFlashAttributesFromSgml(uniqueId, swfUrl, attributes);
					#endregion
				}
				else if (tagName == "dsi:quote")
				{
					#region dsi:quote
					Usr u = null;
					try
					{
						u = new Usr(int.Parse(refAtt));
					}
					catch { }

					if (u != null)
					{
						StringBuilder sb = new StringBuilder();
						sb.Append("<div class=\"QuoteName\">");
						sb.Append(u.Link());
						sb.Append(" said:");
						sb.Append("</div>");
						sb.Append("<div class=\"QuoteBody\">");
						return sb.ToString();
					}
					else
					{
						return "<div class=\"QuoteBody\">";
					}
					#endregion
				}
				else if (tagName == "dsi:object" || tagName == "dsi:link")
				{
					#region dsi:object, dsi:link
					/*
					<dsi:object
						type = [usr | event | venue | place | group | brand | photo | misc]
						ref = [object-k]
						style = [
							content: {text* | icon | text-under-icon};   // for type=usr, event, venue, place, group, brand
							details: {none* | venue | place | country};  // for type=event, venue, place
							date: {false* | true};                       // for type=event
							snip: {number};                              // for type=event
							rollover: {true* | false}                    // for type=usr, photo
							photo: {icon* | thumb | web}                 // for type=photo
							link: {true* | false}
						]
					/>
					*/

					string app = attributes.ContainsKey("app") ? (attributes["app"] == "home" ? null : attributes["app"]) : null;
					string date = attributes.ContainsKey("date") ? attributes["date"].Replace('-', '/') : null;
					string jump = attributes.ContainsKey("jump") ? "#" + attributes["jump"] : "";
					string parStr = attributes.ContainsKey("par") ? attributes["par"] : null;
					#region Decode par array
					string[] par = null;
					if (parStr != null)
					{
						List<string> parList = new List<string>();
						foreach (string s in parStr.Split('&'))
						{
							if (s.Contains("="))
							{
								parList.Add(System.Web.HttpUtility.UrlDecode(s.Split('=')[0]));
								parList.Add(System.Web.HttpUtility.UrlDecode(s.Split('=')[1]));
							}
							else
							{
								parList.Add(System.Web.HttpUtility.UrlDecode(s));
								parList.Add("");
							}
						}
						par = parList.ToArray();
					}
					#endregion

					string styleContent = style.ContainsKey("content") ? style["content"] : "text";
					string styleDetails = style.ContainsKey("details") ? style["details"] : "none";
					bool styleDate = style.ContainsKey("date") ? bool.Parse(style["date"]) : false;
					int styleSnip = style.ContainsKey("snip") ? int.Parse(style["snip"]) : 0;
					bool styleRollover = style.ContainsKey("rollover") ? bool.Parse(style["rollover"]) : true;
					string stylePhoto = style.ContainsKey("photo") ? style["photo"] : "icon";
					bool styleLink = style.ContainsKey("link") ? bool.Parse(style["link"]) : true;

					string extraHtmlAttributes = "";
					string extraStyleAttribute = "";
					string extraStyleElements = "";
					string extraClassAttribute = "";
					string extraClassElements = "";
					foreach (string k in attributes.Keys)
					{
						if (k != "href" && k != "src" && k != "type" && k != "ref" && k != "style" && k != "app" && k != "date" && k != "par" && k != "class")
						{
							extraHtmlAttributes += " " + k + "=\"" + attributes[k] + "\"";
						}
					}

					foreach (string s in style.Keys)
					{
						if (s != "content" && s != "details" && s != "date" && s != "snip" && s != "rollover" && s != "photo" && s != "link")
						{
							extraStyleElements += s + ":" + style[s] + ";";
						}
					}
					if (extraStyleElements.Length > 0)
					{
						extraStyleAttribute = " style=\"" + extraStyleElements + "\"";
					}

					foreach (string s in classes)
					{
						extraClassElements += " " + s;
					}
					if (extraClassElements.Length > 0)
					{
						extraClassAttribute = " class=\"" + extraClassElements + "\"";
					}

					if (typeAtt == "usr")
					{
						#region Usr
						Usr u = new Usr(int.Parse(refAtt));
						string url = getObectPageUrl(u, app, date, par) + jump;

						if (tagName == "dsi:link")
							return "<a href=\"" + url + "\"" + extraHtmlAttributes + extraStyleAttribute + extraClassAttribute + ">";

						StringBuilder sb = new StringBuilder();

						string rolloverHtml = styleRollover ? ((styleContent == "icon" || styleContent == "text-under-icon") ? u.RolloverNoPic : u.Rollover) : "";

						if (styleContent == "icon" || styleContent == "text-under-icon")
						{
							#region Pic
							if (styleLink)
							{
								sb.Append("<a href=\"");
								sb.Append(url);
								sb.Append("\"");
								sb.Append(rolloverHtml);
								sb.Append(extraHtmlAttributes);
								sb.Append(extraStyleAttribute);
								sb.Append(extraClassAttribute);
								sb.Append(">");
							}
							sb.Append("<img src=\"");
							sb.Append(u.AnyPicPath);
							sb.Append("\"");
							if (styleContent == "icon" && !styleLink)
							{
								//Just image with no link around the image... lets apply any extra html to the image tag.
								if (!attributes.ContainsKey("width"))
									sb.Append(" width=\"100\"");

								if (!attributes.ContainsKey("height"))
									sb.Append(" height=\"100\"");

								sb.Append(extraHtmlAttributes);
								sb.Append(extraStyleAttribute);
								sb.Append(" class=\"BorderBlack All" + extraClassElements + "\"");
								sb.Append(" />");
							}
							else
								sb.Append(" width=\"100\" height=\"100\" class=\"BorderBlack All\" />");
							if (styleLink)
								sb.Append("</a>");
							if (styleContent == "text-under-icon")
								sb.Append("<br />");
							#endregion
						}
						if (styleContent == "text" || styleContent == "text-under-icon")
						{
							#region Nickname
							if (styleLink)
							{
								sb.Append("<a href=\"");
								sb.Append(url);
								sb.Append("\"");
								sb.Append(rolloverHtml);
								sb.Append(extraHtmlAttributes);
								sb.Append(extraStyleAttribute);
								sb.Append(extraClassAttribute);
								sb.Append(">");
							}
							sb.Append(getObjectName(u.NickNameDisplay, app, styleSnip));
							if (styleLink)
								sb.Append("</a>");
							#endregion
						}
						return sb.ToString();
						#endregion
					}
					else if (typeAtt == "event")
					{
						#region Event
						Event e = new Event(int.Parse(refAtt));
						string url = getObectPageUrl(e, app, date, par) + jump;

						if (tagName == "dsi:link")
							return "<a href=\"" + url + "\"" + extraHtmlAttributes + extraStyleAttribute + extraClassAttribute + ">";

						StringBuilder sb = new StringBuilder();

						#region Container span
						if ((styleContent == "text" || styleContent == "text-under-icon") && !styleLink && (extraHtmlAttributes.Length > 0 || extraStyleAttribute.Length > 0 || extraClassAttribute.Length > 0) && (styleDate || styleDetails == "venue" || styleDetails == "place" || styleDetails == "country"))
						{
							sb.Append("<span");
							sb.Append(extraHtmlAttributes);
							sb.Append(extraStyleAttribute);
							sb.Append(extraClassAttribute);
							sb.Append(">");
						}
						#endregion

						if (styleContent == "icon" || styleContent == "text-under-icon")
						{
							#region Pic
							if (styleLink)
							{
								sb.Append("<a href=\"");
								sb.Append(url);
								sb.Append("\"");
								sb.Append(extraHtmlAttributes);
								sb.Append(extraStyleAttribute);
								sb.Append(extraClassAttribute);
								sb.Append(">");
							}
							sb.Append("<img src=\"");
							sb.Append(e.AnyPicPath);
							sb.Append("\"");
							if (styleContent == "icon" && !styleLink)
							{
								//Just image with no link around the image... lets apply any extra html to the image tag.
								if (!attributes.ContainsKey("width"))
									sb.Append(" width=\"100\"");

								if (!attributes.ContainsKey("height"))
									sb.Append(" height=\"100\"");

								sb.Append(extraHtmlAttributes);
								sb.Append(extraStyleAttribute);
								sb.Append(" class=\"BorderBlack All" + extraClassElements + "\"");
								sb.Append(" />");
							}
							else
								sb.Append(" width=\"100\" height=\"100\" class=\"BorderBlack All\" />");

							if (styleLink)
								sb.Append("</a>");

							if (styleContent == "text-under-icon")
								sb.Append("<br />");

							#endregion
						}
						if (styleContent == "text" || styleContent == "text-under-icon")
						{
							#region Event link
							if (styleLink)
							{
								sb.Append("<a href=\"");
								sb.Append(url);
								sb.Append("\"");
								sb.Append(extraHtmlAttributes);
								sb.Append(extraStyleAttribute);
								sb.Append(extraClassAttribute);
								sb.Append(">");
							}

							sb.Append(getObjectName(e.Name, app, styleSnip));

							if (styleLink)
								sb.Append("</a>");
							#endregion

							#region Venue link
							if (styleDetails == "venue" || styleDetails == "place" || styleDetails == "country")
							{
								sb.Append(" @ ");
								if (styleLink)
								{
									sb.Append("<a href=\"");
									sb.Append(e.Venue.Url());
									sb.Append("\"");
									sb.Append(extraHtmlAttributes);
									sb.Append(extraStyleAttribute);
									sb.Append(extraClassAttribute);
									sb.Append(">");
								}
								sb.Append(Cambro.Misc.Utility.Snip(e.Venue.Name, styleSnip));
								if (styleLink)
									sb.Append("</a>");
							}
							#endregion

							#region Place link
							if (styleDetails == "place" || styleDetails == "country")
							{
								sb.Append(" in ");
								if (styleLink)
								{
									sb.Append("<a href=\"");
									sb.Append(e.Venue.Place.Url());
									sb.Append("\"");
									sb.Append(extraHtmlAttributes);
									sb.Append(extraStyleAttribute);
									sb.Append(extraClassAttribute);
									sb.Append(">");
								}
								if (styleDetails == "country")
								{
									sb.Append(Cambro.Misc.Utility.Snip(e.Venue.Place.Name, styleSnip));
								}
								else
								{
									sb.Append(Cambro.Misc.Utility.Snip(e.Venue.Place.NamePlain, styleSnip));
								}
								if (styleLink)
									sb.Append("</a>");

							}
							#endregion

							#region Date
							if (styleDate)
							{
								sb.Append(", ");
								sb.Append(e.FriendlyDate(false));
							}
							#endregion
						}

						#region End container span
						if ((styleContent == "text" || styleContent == "text-under-icon") && !styleLink && (extraHtmlAttributes.Length > 0 || extraStyleAttribute.Length > 0 || extraClassAttribute.Length > 0) && (styleDetails == "venue" || styleDetails == "place" || styleDetails == "country"))
						{
							sb.Append("</span>");
						}
						#endregion

						return sb.ToString();

						#endregion
					}
					else if (typeAtt == "venue")
					{
						#region Venue
						Venue v = new Venue(int.Parse(refAtt));
						string url = getObectPageUrl(v, app, date, par) + jump;

						if (tagName == "dsi:link")
							return "<a href=\"" + url + "\"" + extraHtmlAttributes + extraStyleAttribute + extraClassAttribute + ">";

						StringBuilder sb = new StringBuilder();

						#region Container span
						if ((styleContent == "text" || styleContent == "text-under-icon") && !styleLink && (extraHtmlAttributes.Length > 0 || extraStyleAttribute.Length > 0 || extraClassAttribute.Length > 0) && (styleDetails == "place" || styleDetails == "country"))
						{
							sb.Append("<span");
							sb.Append(extraHtmlAttributes);
							sb.Append(extraStyleAttribute);
							sb.Append(extraClassAttribute);
							sb.Append(">");
						}
						#endregion

						if (styleContent == "icon" || styleContent == "text-under-icon")
						{
							#region Pic
							if (styleLink)
							{
								sb.Append("<a href=\"");
								sb.Append(url);
								sb.Append("\"");
								sb.Append(extraHtmlAttributes);
								sb.Append(extraStyleAttribute);
								sb.Append(extraClassAttribute);
								sb.Append(">");
							}
							sb.Append("<img src=\"");
							sb.Append(v.AnyPicPath);
							sb.Append("\"");
							if (styleContent == "icon" && !styleLink)
							{
								//Just image with no link around the image... lets apply any extra html to the image tag.
								if (!attributes.ContainsKey("width"))
									sb.Append(" width=\"100\"");

								if (!attributes.ContainsKey("height"))
									sb.Append(" height=\"100\"");

								sb.Append(extraHtmlAttributes);
								sb.Append(extraStyleAttribute);
								sb.Append(" class=\"BorderBlack All" + extraClassElements + "\"");
								sb.Append(" />");
							}
							else
								sb.Append(" width=\"100\" height=\"100\" class=\"BorderBlack All\" />");
							if (styleLink)
								sb.Append("</a>");

							if (styleContent == "text-under-icon")
								sb.Append("<br />");
							#endregion
						}

						if (styleContent == "text" || styleContent == "text-under-icon")
						{
							#region Venue link
							if (styleLink)
							{
								sb.Append("<a href=\"");
								sb.Append(url);
								sb.Append("\"");
								sb.Append(extraHtmlAttributes);
								sb.Append(extraStyleAttribute);
								sb.Append(extraClassAttribute);
								sb.Append(">");
							}

							sb.Append(getObjectName(v.Name, app, styleSnip));

							if (styleLink)
								sb.Append("</a>");
							#endregion

							#region Place link
							if (styleDetails == "place" || styleDetails == "country")
							{
								sb.Append(" in ");
								if (styleLink)
								{
									sb.Append("<a href=\"");
									sb.Append(v.Place.Url());
									sb.Append("\"");
									sb.Append(extraHtmlAttributes);
									sb.Append(extraStyleAttribute);
									sb.Append(extraClassAttribute);
									sb.Append(">");
								}
								if (styleDetails == "country")
								{
									sb.Append(Cambro.Misc.Utility.Snip(v.Place.Name, styleSnip));
								}
								else
								{
									sb.Append(Cambro.Misc.Utility.Snip(v.Place.NamePlain, styleSnip));
								}
								if (styleLink)
									sb.Append("</a>");
							}
							#endregion
						}

						#region End container span
						if ((styleContent == "text" || styleContent == "text-under-icon") && !styleLink && (extraHtmlAttributes.Length > 0 || extraStyleAttribute.Length > 0 || extraClassAttribute.Length > 0) && (styleDetails == "place" || styleDetails == "country"))
						{
							sb.Append("</span>");
						}
						#endregion

						return sb.ToString();

						#endregion
					}
					else if (typeAtt == "place")
					{
						#region Place
						Place p = new Place(int.Parse(refAtt));
						string url = getObectPageUrl(p, app, date, par) + jump;

						if (tagName == "dsi:link")
							return "<a href=\"" + url + "\"" + extraHtmlAttributes + extraStyleAttribute + extraClassAttribute + ">";

						StringBuilder sb = new StringBuilder();

						#region Container span
						if ((styleContent == "text" || styleContent == "text-under-icon") && !styleLink && (extraHtmlAttributes.Length > 0 || extraStyleAttribute.Length > 0 || extraClassAttribute.Length > 0))
						{
							sb.Append("<span");
							sb.Append(extraHtmlAttributes);
							sb.Append(extraStyleAttribute);
							sb.Append(extraClassAttribute);
							sb.Append(">");
						}
						#endregion

						if (styleContent == "icon" || styleContent == "text-under-icon")
						{
							#region Pic
							if (styleLink)
							{
								sb.Append("<a href=\"");
								sb.Append(url);
								sb.Append("\"");
								sb.Append(extraHtmlAttributes);
								sb.Append(extraStyleAttribute);
								sb.Append(extraClassAttribute);
								sb.Append(">");
							}
							sb.Append("<img src=\"");
							sb.Append(p.AnyPicPath);
							sb.Append("\"");
							if (styleContent == "icon" && !styleLink)
							{
								//Just image with no link around the image... lets apply any extra html to the image tag.
								if (!attributes.ContainsKey("width"))
									sb.Append(" width=\"100\"");

								if (!attributes.ContainsKey("height"))
									sb.Append(" height=\"100\"");

								sb.Append(extraHtmlAttributes);
								sb.Append(extraStyleAttribute);
								if (style.ContainsKey("border"))
								{
									sb.Append(extraClassAttribute);
								}
								else
								{
									sb.Append(" class=\"BorderBlack All" + extraClassElements + "\"");
								}
								
								sb.Append(" />");
							}
							else
								sb.Append(" width=\"100\" height=\"100\" class=\"BorderBlack All\" />");
							if (styleLink)
								sb.Append("</a>");
							if (styleContent == "text-under-icon")
								sb.Append("<br />");
							#endregion
						}

						if (styleContent == "text" || styleContent == "text-under-icon")
						{
							#region Place link
							if (styleLink)
							{
								sb.Append("<a href=\"");
								sb.Append(url);
								sb.Append("\"");
								sb.Append(extraHtmlAttributes);
								sb.Append(extraStyleAttribute);
								sb.Append(extraClassAttribute);
								sb.Append(">");
							}
							if (styleDetails == "country")
							{
								sb.Append(getObjectName(p.Name, app, styleSnip));
							}
							else
							{
								sb.Append(getObjectName(p.NamePlain, app, styleSnip));
							}
							if (styleLink)
								sb.Append("</a>");
							#endregion
						}

						#region End container span
						if ((styleContent == "text" || styleContent == "text-under-icon") && !styleLink && (extraHtmlAttributes.Length > 0 || extraStyleAttribute.Length > 0 || extraClassAttribute.Length > 0))
						{
							sb.Append("</span>");
						}
						#endregion

						return sb.ToString();

						#endregion
					}
					else if (typeAtt == "group")
					{
						#region Group
						Group g = new Group(int.Parse(refAtt));
						string url = getObectPageUrl(g, app, date, par) + jump;

						if (tagName == "dsi:link")
							return "<a href=\"" + url + "\"" + extraHtmlAttributes + extraStyleAttribute + extraClassAttribute + ">";

						StringBuilder sb = new StringBuilder();

						#region Group link
						if (styleLink)
						{
							sb.Append("<a href=\"");
							sb.Append(url);
							sb.Append("\"");
							sb.Append(extraHtmlAttributes);
							sb.Append(extraStyleAttribute);
							sb.Append(extraClassAttribute);
							sb.Append(">");
						}
						sb.Append(getObjectName(g.FriendlyName, app, styleSnip));
						if (styleLink)
							sb.Append("</a>");
						#endregion

						return sb.ToString();

						#endregion
					}
					else if (typeAtt == "brand")
					{
						#region Brand
						Brand b = new Brand(int.Parse(refAtt));
						string url = getObectPageUrl(b, app, date, par) + jump;

						if (tagName == "dsi:link")
							return "<a href=\"" + url + "\"" + extraHtmlAttributes + extraStyleAttribute + extraClassAttribute + ">";

						StringBuilder sb = new StringBuilder();

						#region Brand link
						if (styleLink)
						{
							sb.Append("<a href=\"");
							sb.Append(url);
							sb.Append("\"");
							sb.Append(extraHtmlAttributes);
							sb.Append(extraStyleAttribute);
							sb.Append(extraClassAttribute);
							sb.Append(">");
						}
						sb.Append(getObjectName(b.FriendlyName, app, styleSnip));
						if (styleLink)
							sb.Append("</a>");
						#endregion

						return sb.ToString();

						#endregion
					}
					else if (typeAtt == "photo")
					{
						#region Photo
						Photo p = new Photo(int.Parse(refAtt));
						string url = getObectPageUrl(p, app, date, par) + jump;

						if (tagName == "dsi:link")
							return "<a href=\"" + url + "\"" + extraHtmlAttributes + extraStyleAttribute + extraClassAttribute + ">";

						StringBuilder sb = new StringBuilder();

						#region Link
						if (styleLink)
						{
							sb.Append("<a href=\"");
							sb.Append(url);
							sb.Append("\"");
							sb.Append(extraHtmlAttributes);
							sb.Append(extraStyleAttribute);
							sb.Append(extraClassAttribute);
							sb.Append(">");
						}
						#endregion

						if (app != null && app == "chat")
						{
							#region For chat app, just show the name of the parent...
							sb.Append(Cambro.Misc.Utility.Snip(((IName)p.ParentObject).Name, styleSnip) + " (chat)");
							#endregion
						}
						else
						{
							#region Image tag
							sb.Append("<img");

							#region Src attribute
							sb.Append(" src=\"");
							if (stylePhoto == "thumb")
								sb.Append(p.ThumbPath);
							else if (stylePhoto == "icon")
								sb.Append(p.IconPath);
							else if (stylePhoto == "web")
								sb.Append(p.WebPath);
							sb.Append("\"");
							#endregion

							#region Width attribute
							if (styleLink || !attributes.ContainsKey("width"))
							{
								sb.Append(" width=\"");
								if (stylePhoto == "thumb")
									sb.Append(p.ThumbWidth);
								else if (stylePhoto == "icon")
									sb.Append("100");
								else if (stylePhoto == "web")
									sb.Append(p.WebWidth);
								sb.Append("\"");
							}
							#endregion

							#region Height attribute
							if (styleLink || !attributes.ContainsKey("height"))
							{
								sb.Append(" height=\"");
								if (stylePhoto == "thumb")
									sb.Append(p.ThumbHeight);
								else if (stylePhoto == "icon")
									sb.Append("100");
								else if (stylePhoto == "web")
									sb.Append(p.WebHeight);
								sb.Append("\"");
							}
							#endregion

							#region Extra html attributes
							if (!styleLink)
							{
								sb.Append(extraHtmlAttributes);
								sb.Append(extraStyleAttribute);
							}
							#endregion

							#region Style attribute
							if (styleLink)
							{
								
								sb.Append(" class=\"BorderBlack All\"");
							}
							else if (style.ContainsKey("border"))
							{
								sb.Append(extraClassAttribute);
							}
							else
							{
								sb.Append(" class=\"BorderBlack All" + extraClassElements + "\"");
							}
							#endregion

							#region Rollover
							if (styleRollover && stylePhoto != "web")
							{
								sb.Append(" onmouseover=\"stm('<img src=" + p.WebPath + " width=" + p.WebWidth + " height=" + p.WebHeight + " class=Block />');\" onmouseout=\"htm();\"");
							}
							#endregion

							sb.Append(" />");
							#endregion
						}

						#region End link
						if (styleLink)
							sb.Append("</a>");
						#endregion

						return sb.ToString();

						#endregion
					}
					else if (typeAtt == "misc")
					{
						#region Misc
						Misc mi = new Misc(int.Parse(refAtt));

						if (tagName == "dsi:link")
							return "<a href=\"" + mi.Url() + "\"" + extraHtmlAttributes + extraStyleAttribute + extraClassAttribute + ">";

						if (mi.Extention.ToLower() == "jpg" || mi.Extention.ToLower() == "jpeg" || mi.Extention.ToLower() == "gif" || mi.Extention.ToLower() == "png")
						{
							StringBuilder sb = new StringBuilder();

							#region Width and height
							int width = attributes.ContainsKey("width") ? int.Parse(attributes["width"]) : mi.Width;
							int height = attributes.ContainsKey("height") ? int.Parse(attributes["height"]) : mi.Height;
							#endregion

							#region Image tag
							sb.Append("<img src=\"");
							sb.Append(mi.Url());
							sb.Append("\" width=\"");
							sb.Append(width);
							sb.Append("\" height=\"");
							sb.Append(height);
							sb.Append("\" border=\"0\"");
							sb.Append(extraHtmlAttributes);
							sb.Append(extraStyleAttribute);
							sb.Append(extraClassAttribute);
							sb.Append(" />");
							#endregion

							return sb.ToString();

						}
						else if (mi.Extention.ToLower() == "swf")
						{
							#region Swf
							return getFlashAttributesFromSgml(uniqueId, mi.Url(), attributes);
							#endregion
						}
						#endregion
					}
					else if (typeAtt == "article")
					{
						#region Article
						Article a = new Article(int.Parse(refAtt));
						string url = getObectPageUrl(a, app, date, par) + jump;

						if (tagName == "dsi:link")
							return "<a href=\"" + url + "\"" + extraHtmlAttributes + extraStyleAttribute + extraClassAttribute + ">";

						StringBuilder sb = new StringBuilder();

						#region Article link
						if (styleLink)
						{
							sb.Append("<a href=\"");
							sb.Append(url);
							sb.Append("\"");
							sb.Append(extraHtmlAttributes);
							sb.Append(extraStyleAttribute);
							sb.Append(extraClassAttribute);
							sb.Append(">");
						}
						sb.Append(getObjectName(a.FriendlyName, app, styleSnip));
						if (styleLink)
							sb.Append("</a>");
						#endregion

						return sb.ToString();

						#endregion
					}
					else if (typeAtt == "url") //if (attributes.ContainsKey("href"))
					{
						#region Url
						string url = attributes.ContainsKey("href") ? attributes["href"] : "";
						#region Get name
						string name = url;
						string path = url;
						string domain = url;
						string targetAttribute = "";
						try
						{
							if (UrlRegex.IsMatch(url))
							{
								Match urlMatch = UrlRegex.Match(url);
								if (urlMatch.Groups[3].Value.StartsWith("www."))
									name = urlMatch.Groups[3].Value.Substring(4);
								else
									name = urlMatch.Groups[3].Value;

								domain = urlMatch.Groups[3].Value;
								path = urlMatch.Groups[4].Value;
								if (!domain.ToLower().EndsWith(".dontstayin.com") && !attributes.ContainsKey("target"))
									targetAttribute = " target=\"_blank\"";

							}
						}
						catch
						{ }
						#endregion

						if (tagName == "dsi:link")
							return "<a href=\"" + url + "\"" + targetAttribute + extraHtmlAttributes + extraStyleAttribute + extraClassAttribute + ">";

						StringBuilder sb = new StringBuilder();

						if (path.ToLower().EndsWith(".jpg") || path.ToLower().EndsWith(".jpeg") || path.ToLower().EndsWith(".gif") || path.ToLower().EndsWith(".png"))
						{
							#region Image tag
							sb.Append("<img");

							#region Src attribute
							sb.Append(" src=\"");
							sb.Append(url);
							sb.Append("\"");
							#endregion

							sb.Append(extraHtmlAttributes);

							#region Style attribute
							if (domain.EndsWith(".dontstayin.com") && !style.ContainsKey("border"))
							{
								sb.Append(" class=\"BorderBlack All" + extraClassElements + "\"");
							}
							else
							{
								sb.Append(extraClassAttribute);
							}
							#endregion

							sb.Append(extraStyleAttribute);

							sb.Append(" />");
							#endregion
						}
						else if (path.ToLower().EndsWith(".mp3") || path.ToLower().EndsWith(".wav"))
						{
							#region Audio
							int width = attributes.ContainsKey("width") ? int.Parse(attributes["width"]) : 290;
							int height = attributes.ContainsKey("height") ? int.Parse(attributes["height"]) : 24;
							string audioPlayerSwfPath = Storage.Path(new Guid("7abb3119-f8ad-43ff-be01-764b2ae111fc"), "swf", Storage.Stores.Pix);
							return GetFlash(uniqueId, height, width, false, "load", audioPlayerSwfPath, "soundFile", url, "autoStart", "no") +
								@"<a href=""" + url + @"""><img src=""/gfx/download-button2.png"" width=""73"" height=""16"" border=""0"" /></a>";
							#endregion
						}
						else if (path.ToLower().EndsWith(".swf"))
						{
							#region Swf
							return getFlashAttributesFromSgml(uniqueId, path, attributes);
							#endregion
						}
						else if (path.ToLower().EndsWith(".flv"))
						{
							#region Flv
							int width = attributes.ContainsKey("width") ? int.Parse(attributes["width"]) : 450;
							int height = attributes.ContainsKey("height") ? (int.Parse(attributes["height"]) + 20) : 357;
							bool nsfw = attributes.ContainsKey("nsfw") ? bool.Parse(attributes["nsfw"].ToLower()) : false;
							string draw = attributes.ContainsKey("draw") ? attributes["draw"].ToLower() : "auto";
							return GetFlash(uniqueId, height, width, nsfw, draw, "/misc/flvplayer.swf", "file", url, "autoStart", "0");
							#endregion
						}
						else
						{
							#region Link
							if (styleLink)
							{
								sb.Append("<a href=\"");
								sb.Append(url);
								sb.Append("\"");
								sb.Append(targetAttribute);
								sb.Append(extraHtmlAttributes);
								sb.Append(extraStyleAttribute);
								sb.Append(extraClassAttribute);
								sb.Append(">");
							}
							#endregion
							#region Name
							sb.Append(name);
							#endregion
							#region End link
							if (styleLink)
								sb.Append("</a>");
							#endregion
						}

						return sb.ToString();

						#endregion
					}
					else if (typeAtt == "room")
					{
						#region Chat room
						Guid guid = refAtt.UnPackGuid();
						Chat.RoomSpec room = Chat.RoomSpec.FromGuid(guid);

						if (room == null)
							return "[Room not found]";
						
						StringBuilder sb = new StringBuilder();
						
						if (tagName == "dsi:link")
							room.LinkHtmlAppendJustStartOfAnchorTag(sb, "selected-onyellow", extraHtmlAttributes, extraStyleAttribute, extraClassAttribute);
						else
							room.LinkHtmlAppend(sb, "selected-onyellow", extraHtmlAttributes, extraStyleAttribute, extraClassAttribute);

						return sb.ToString();

						#endregion
					}
					#endregion
				}
				return "[Invalid tag " + tagName + "]";
			}
			catch (Exception ex)
			{
				if (Vars.DevEnv)
					throw ex;

				return "[Error in " + tagName + " tag]";
			}
		}
		string getObectPageUrl(IObjectPage o, string app, string datePart, string[] par)
		{
			if (app == null || app.Length == 0 || (app == "home" && par.Length == 0))
				app = "";

			if (datePart != null && datePart.Length > 0)
				datePart = "/" + datePart;

			return UrlInfo.MakeUrl(o.UrlFilterPart + datePart, app, par);
		}
		string getObjectName(string s, string app, int styleSnip)
		{
			string appString = "";
			if (app != null && app.Length > 0)
			{
				if (app.StartsWith("my"))
					appString = " (" + app.Substring(2) + ")";
				else
					appString = " (" + app + ")";
			}
			return Cambro.Misc.Utility.Snip(s, styleSnip) + appString;
		}
		#region UrlRegex
		public Regex UrlRegex
		{
			get
			{
				if (urlRegex == null)
				{
					urlRegex = new Regex(@"((http|https)://([0-9a-zA-Z\._-]+)(.*?))(\s|$)", RegexOptions.IgnoreCase);
				}
				return urlRegex;
			}
			set
			{
				urlRegex = value;
			}
		}

		Regex urlRegex;
		#endregion
		#endregion
		#region getFlashAttributesFromSgml
		string getFlashAttributesFromSgml(string uniqueId, string swfUrl, Dictionary<string, string> attributes)
		{
			int width = attributes.ContainsKey("width") ? int.Parse(attributes["width"]) : 450;
			int height = attributes.ContainsKey("height") ? int.Parse(attributes["height"]) : 450;
			bool nsfw = attributes.ContainsKey("nsfw") ? bool.Parse(attributes["nsfw"]) : false;
			string draw = attributes.ContainsKey("draw") ? attributes["draw"] : "auto";
			string play = attributes.ContainsKey("play") ? attributes["play"] : "true";
			string loop = attributes.ContainsKey("loop") ? attributes["loop"] : "";
			string menu = attributes.ContainsKey("menu") ? attributes["menu"] : "false";
			string quality = attributes.ContainsKey("quality") ? attributes["quality"] : "";
			string scale = attributes.ContainsKey("scale") ? attributes["scale"] : "";
			string align = attributes.ContainsKey("align") ? attributes["align"] : "";
			string salign = attributes.ContainsKey("salign") ? attributes["salign"] : "";
			string wmode = attributes.ContainsKey("wmode") ? attributes["wmode"] : "transparent";
			string bgcolor = attributes.ContainsKey("bgcolor") ? attributes["bgcolor"] : "";
			string baseParam = attributes.ContainsKey("base") ? attributes["base"] : "";
			string flashvars = attributes.ContainsKey("flashvars") ? attributes["flashvars"] : "";
			List<string> vars = new List<string>();
			if (flashvars.Length > 0)
			{
				flashvars = flashvars.Replace("&amp;", "\n[ampersand-replacement]\n").Replace("&amp%3b", "\n[ampersand-replacement-encoded]\n");
				foreach (string s in flashvars.Split('&'))
				{
					if (s.Length > 0 && s.Contains("="))
					{
						vars.Add(s.Substring(0, s.IndexOf("=")));
						vars.Add(s.Substring(s.IndexOf("=") + 1).Replace("\n[ampersand-replacement]\n", "&amp;").Replace("\n[ampersand-replacement-encoded]\n", "&amp%3b"));
					}

				}
			}
			return GetFlashGeneric(uniqueId, 9, swfUrl, height, width, nsfw, draw, "true", "never", play, loop, menu, quality, scale, align, salign, wmode, bgcolor, baseParam, vars.ToArray());
		}
		#endregion
		#region GetFlash
		public string GetFlash(string uniqueId, int height, int width, bool nsfw, string draw, string url, params string[] varsAry)
		{
			return GetFlashGeneric(uniqueId, 5, url, height, width, nsfw, draw, "true", "never", "true", "", "false", "", "", "", "", "transparent", "", "", varsAry);
		}
		#endregion
		#region GetFlashGeneric
		public string GetFlashGeneric(
			string uniqueId, 
			int minVersion, 
			string url, 
			int height, 
			int width, 
			bool nsfw, 
			string draw, 
			string allowFullScreen, 
			string allowScriptAccess, 
			string play, 
			string loop, 
			string menu, 
			string quality, 
			string scale, 
			string align, 
			string salign, 
			string wmode, 
			string bgcolor, 
			string baseParam, 
			string[] varsAry)
		{
			if (RenderFlashTagsRaw || (draw != null && draw == "raw"))
			{

				string flashVarsFull = "";
				for (int i = 0; i < varsAry.Length; i = i + 2)
				{
					flashVarsFull += (flashVarsFull.Length == 0 ? "" : "&") + varsAry[i] + "=" + varsAry[i + 1];
				}

				//******************int minVersion, ????

				StringBuilder sb = new StringBuilder();
				sb.Append(@"<object classid=""clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"" codebase=""http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,40,0""");
				appendFlashAttribute(sb, "id", "Ob" + uniqueId);
				appendFlashAttribute(sb, "width", width == 0 ? null : width.ToString());
				appendFlashAttribute(sb, "height", height == 0 ? null : height.ToString());
				sb.Append(@">");
				appendFlashParam(sb, "movie", url);
				appendFlashParam(sb, "allowFullScreen", allowFullScreen);
				appendFlashParam(sb, "allowScriptAccess", allowScriptAccess);
				appendFlashParam(sb, "play", play);
				appendFlashParam(sb, "loop", loop);
				appendFlashParam(sb, "menu", menu);
				appendFlashParam(sb, "quality", quality);
				appendFlashParam(sb, "scale", scale);
				appendFlashParam(sb, "align", align);
				appendFlashParam(sb, "salign", salign);
				appendFlashParam(sb, "wmode", wmode);
				appendFlashParam(sb, "bgcolor", bgcolor);
				appendFlashParam(sb, "base", baseParam);
				appendFlashParam(sb, "flashvars", flashVarsFull);
				sb.Append(@"<embed");
				appendFlashAttribute(sb, "name", "Ob" + uniqueId);
				appendFlashAttribute(sb, "src", url);
				appendFlashAttribute(sb, "width", width == 0 ? null : width.ToString());
				appendFlashAttribute(sb, "height", height == 0 ? null : height.ToString());
				appendFlashAttribute(sb, "allowFullScreen", allowFullScreen);
				appendFlashAttribute(sb, "allowScriptAccess", allowScriptAccess);
				appendFlashAttribute(sb, "play", play);
				appendFlashAttribute(sb, "loop", loop);
				appendFlashAttribute(sb, "menu", menu);
				appendFlashAttribute(sb, "quality", quality);
				appendFlashAttribute(sb, "scale", scale);
				appendFlashAttribute(sb, "align", align);
				appendFlashAttribute(sb, "salign", salign);
				appendFlashAttribute(sb, "wmode", wmode);
				appendFlashAttribute(sb, "bgcolor", bgcolor);
				appendFlashAttribute(sb, "base", baseParam);
				appendFlashAttribute(sb, "flashvars", flashVarsFull);
				sb.Append(@" type=""application/x-shockwave-flash"" pluginspace=""http://www.macromedia.com/go/getflashplayer""></embed></object>");
				return sb.ToString();
			}
			else
			{
				string varsFull = "";
				for (int i = 0; i < varsAry.Length; i = i + 2)
				{
					varsFull += "Ob" + uniqueId + @"Object.addVariable(""" + varsAry[i] + @""", """ + varsAry[i + 1] + @""");";
				}
				int flashItemsOnThisPage = 0;
				if (System.Web.HttpContext.Current != null)
				{
					flashItemsOnThisPage = System.Web.HttpContext.Current.Items["FlashItems"] as int? ?? 0;
				}

				string txt = @"Loading, please wait... If this fails to load, you either have JavaScript turned off or an old version of Macromedia's Flash Player. <a href=""http://www.macromedia.com/go/getflashplayer/"" target=""_blank"">Click here</a> to get the latest flash player.";
				string autoload = @"Ob" + uniqueId + @"Object.write('Ob" + uniqueId + @"');";
				if (!RenderAllFlashTags)
				{
					if (nsfw)
					{
						txt = @"This probably isn't safe for viewing at work.<br /><button onclick=""Ob" + uniqueId + @"Object.write('Ob" + uniqueId + @"');return false;"">show me</button>";
						autoload = "";
					}
					else if (draw == "click" || (draw == "auto" && flashItemsOnThisPage > 0))
					{
						txt = @"Flash hidden to speed up your browsing experience.<br /><button onclick=""Ob" + uniqueId + @"Object.write('Ob" + uniqueId + @"');return false;"">load now</button>";
						autoload = "";
					}
					else if (System.Web.HttpContext.Current != null)
					{
						System.Web.HttpContext.Current.Items["FlashItems"] = (System.Web.HttpContext.Current.Items["FlashItems"] as int? ?? 0) + 1;
					}
				}
				//string volume = "0";
				string paramsFull =
					(allowFullScreen.Length > 0 ? (@"Ob" + uniqueId + @"Object.addParam(""allowFullScreen"", """ + allowFullScreen + @""");") : "") +
					(allowScriptAccess.Length > 0 ? (@"Ob" + uniqueId + @"Object.addParam(""allowScriptAccess"", """ + allowScriptAccess + @""");") : "") +
					(play.Length > 0 ? (@"Ob" + uniqueId + @"Object.addParam(""play"", """ + play + @""");") : "") +
					(loop.Length > 0 ? (@"Ob" + uniqueId + @"Object.addParam(""loop"", """ + loop + @""");") : "") +
					(menu.Length > 0 ? (@"Ob" + uniqueId + @"Object.addParam(""menu"", """ + menu + @""");") : "") +
					(quality.Length > 0 ? (@"Ob" + uniqueId + @"Object.addParam(""quality"", """ + quality + @""");") : "") +
					(scale.Length > 0 ? (@"Ob" + uniqueId + @"Object.addParam(""scale"", """ + scale + @""");") : "") +
					(align.Length > 0 ? (@"Ob" + uniqueId + @"Object.addParam(""align"", """ + align + @""");") : "") +
					(salign.Length > 0 ? (@"Ob" + uniqueId + @"Object.addParam(""salign"", """ + salign + @""");") : "") +
					(wmode.Length > 0 ? (@"Ob" + uniqueId + @"Object.addParam(""wmode"", """ + wmode + @""");") : "") +
					(bgcolor.Length > 0 ? (@"Ob" + uniqueId + @"Object.addParam(""bgcolor"", """ + bgcolor + @""");") : "") +
					//(volume.Length > 0 ? (@"Ob" + uniqueId + @"Object.addParam(""volume"", """ + volume + @""");") : "") +
					(baseParam.Length > 0 ? (@"Ob" + uniqueId + @"Object.addParam(""base"", """ + baseParam + @""");") : "");

				this.Script += @"try { var Ob" + uniqueId + @"Object = new SWFObject(""" + url + @""", ""Ob" + uniqueId + @"Movie"", " +
				              width.ToString() + @", " + height.ToString() + @", """ + minVersion.ToString() + @""", ""#FFFFFF"");" +
				              paramsFull + varsFull + autoload + "} catch(e) {} ";

				return @"<div id=""Ob" + uniqueId + @"""><table style=""background-color:#FFFFFF;width:" + width.ToString() + @"px;height:" + height.ToString() + @"px;""><tr><td valign=""middle"" align=""center"" style=""padding:10px;"">" + txt + @"</td></tr></table></div>";

				//<script>var Ob" + uniqueId + @"Object = new SWFObject(""" + url + @""", ""Ob" + uniqueId + @"Movie"", " + width.ToString() + @", " + height.ToString() + @", """ + minVersion.ToString() + @""", ""#FFFFFF"");" + paramsFull + varsFull + autoload + @"</script>";
			}
		}
		void appendFlashAttribute(StringBuilder sb, string name, string value)
		{
			if (value != null && value.Length > 0)
			{
				sb.Append(" ");
				sb.Append(name);
				sb.Append("=\"");
				sb.Append(value);
				sb.Append("\"");
			}
		}
		void appendFlashParam(StringBuilder sb, string name, string value)
		{
			if (value != null && value.Length > 0)
			{
				sb.Append("<param name=\"");
				sb.Append(name);
				sb.Append("\" value=\"");
				sb.Append(value);
				sb.Append("\">");
			}
		}
		#endregion
		#region GetHtmlForEditorControl
		public string GetHtmlForEditorControl()
		{
			return rawHtml;
		}
		#endregion

		
	}
	
}


