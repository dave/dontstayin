using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using Facebook.Api;
using Bobs;
using System.Text;
using System.Text.RegularExpressions;

namespace MixmagGreatest
{
	public partial class Default1 : System.Web.UI.Page
	{
		public DateTime? IssueDate = null;
		public int? PageNumber = null;
		public int? CoverId = null;
		public bool IsFacebook = false;

		public string url
		{
			get 
			{ 
				//return IsFacebook ? "apps.facebook.com/mixmag-dj-test" : Vars.DevEnv ? "dev1.dontstayin.com" : "greatest.dj"; 
				//return Vars.DevEnv ? "dev1.dontstayin.com" : "greatest.dj"; 
				return "greatest.dj"; 
			}
		}
		public bool newWindow
		{
			get
			{
				return IsFacebook;
			}
		}
		public string auth
		{
			get
			{
				//return "";
				return "?auth=" + Request.Form["signed_request"];
			}
		}
		

		protected void Page_Load(object sender, EventArgs e)
		{
			{


				StringBuilder sbDjs = new StringBuilder();

				Query q1 = new Query();
				q1.OrderBy = new OrderBy(OrderBy.OrderDirection.Random);
				MixmagGreatestDjSet djs = new MixmagGreatestDjSet(q1);

				HtmlRenderer h = new HtmlRenderer();
				h.RenderFlashTagsRaw = true;

				foreach (MixmagGreatestDj dj in djs)
				{
					string link = "";
			
					sbDjs.Append(@"
					<p>
						<div class=""Spacer""/>
					</p>
					<p class=""Header"">
						" + dj.Name.ToUpper() + @"
					</p>
					<p>
						<div class=""SpacerDotted""/>
					</p>
					<div style=""height:100px; width:100px; float:left;"">
						" + "<a href=\"http://" + url + "/" + dj.UrlName + auth + "\"" + (newWindow ? " target=\"_blank\"" : "") + " class=\"NoStyle\">" + @"<img src=""" + dj.Image100Url + @""" width=""100"" height=""100"" /></a>
					</div>
					<div style=""min-height:100px; width:405px; padding-right:0px; padding-left:15px; float:left;"">
						<div class=""TextQuote"">
							" + dj.ShortDescription + @"
						</div>
						<div style=""margin-top:5px; text-align:right;"" class=""Text"">" + "<a href=\"http://" + url + "/" + dj.UrlName + auth + "\"" + (newWindow ? " target=\"_blank\"" : "") + ">" + @"More details, watch the video and vote</a></div>
					</div>
					<div style=""clear: both;""></div>
");

					//sbDjs.Append("<h2 style=\"margin-top: 35px; margin-bottom: -5px;\">" + dj.Name + "</h2>");
					//sbDjs.Append("<div style=\"height: 100px; clear:both; margin-bottom: 20px;\">");
					//{
					//    sbDjs.Append("<div style=\"height: 100px; width: 100px; float: left; margin-right: 10px;\"><a href=\"http://" + url + "/" + dj.UrlName + auth + "\"" + (newWindow ? " target=\"_blank\"" : "") + "><img src=\"" + dj.ImageUrl + "\" width=\"100\" height=\"100\" /></a></div>");
					//    //sbDjs.Append("<div style=\"height: 100px; width: 150px; float: right; margin-left: 10px; \">" + h.GetFlash("djvid" + dj.K.ToString(), 100, 150, false, "load", "http://www.youtube.com/v/" + dj.YoutubeId + "&rel=1") + "</div>");
					//    sbDjs.Append("<p>" + dj.Description + "<br /><a href=\"http://" + url + "/" + dj.UrlName + auth + "\"" + (newWindow ? " target=\"_blank\"" : "") + "><b>More info about " + dj.Name + "</b></a></p>");
					//}
					//sbDjs.Append("</div>");
					////sbDjs.Append("<div style=\"clear: both;\"></div>");
				}
				DjsPh.Controls.Add(new LiteralControl(sbDjs.ToString()));
			}


		}


		

		public static bool IsEmail(string Email)
		{
			string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
				@"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
				@".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
			Regex re = new Regex(strRegex);
			if (re.IsMatch(Email))
				return (true);
			else
				return (false);
		}

		/*
		 * 
		

			<div class="GreatestBox" id="PanelVoteConnect" runat="server">
				<p>
					To vote for [DJ NAME], click the "connect" button below:
				</p>
				<p>
					[CANCEL BUTTON] [CONNECT BUTTON]
				</p>
			</div>

			<div class="GreatestBox" id="PanelThanks" runat="server">
				<p style="<%= Request["canvas"]==null ? "" : "display:none;" %>">
					Your Facebook name: <fb:name uid="loggedinuser" useyou="false" linked="true" class=""></fb:name>.
					If this isn't you, you can <a href="/" onclick="FacebookLogoutClick();return false;">log out</a>
				</p>
				<p>
					Thanks for your vote. You voted for [DJ NAME].
				</p>
			</div>

			<div class="GreatestBox" id="PanelVoted" runat="server">
				<p style="<%= Request["canvas"]==null ? "" : "display:none;" %>">
					Your Facebook name: <fb:name uid="loggedinuser" useyou="false" linked="true" class=""></fb:name>.
					If this isn't you, you can <a href="/" onclick="FacebookLogoutClick();return false;">log out</a>
				</p>
				<p>
					<!-- Already logged in, already voted -->
					You voted for [DJ NAME].
				</p>
			</div>

			<div class="GreatestBox" id="PanelErrorAlreadyVoted" runat="server">
				<p style="<%= Request["canvas"]==null ? "" : "display:none;" %>">
					Your Facebook name: <fb:name uid="loggedinuser" useyou="false" linked="true" class=""></fb:name>.
					If this isn't you, you can <a href="/" onclick="FacebookLogoutClick();return false;">log out</a>
				</p>
				<p>
					<!-- Try to vote twice -->
					Sorry, you can't vote twice. You already voted for [DJ NAME].
				</p>
			</div>
		  
		  <div style="margin-left:15px; margin-right:15px;">
				<p xstyle="display:none;">
					<textarea id="debug" cols="40" rows="10"></textarea>
				</p>
			</div>
		*/

	}
}
