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
using Newtonsoft.Json.Linq;

namespace MixmagGreatest
{
	public partial class Default3 : System.Web.UI.Page
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
				return false;
			}
		}
		public string auth
		{
			get
			{
				return "";
				//return "?auth=" + Request.Form["signed_request"];
			}
		}

		public static JObject DecodeFacebookRequest(string signed_request)
		{
			string secret = Facebook.FacebookCommon.Common(Facebook.Apps.GreatestDj).Secret;
			var facebookData = new Dictionary<string, string>();

			var requestArray = signed_request.Split('.');

			var sig = Base64_Url_Decode(requestArray[0]);

			var dataString = Base64_Url_Decode(requestArray[1]);

			var data = JObject.Parse(dataString);

			var algo = data["algorithm"].ToString().Replace("\"", "");

			if (algo != "HMAC-SHA256")
			{
				return null;
			}

			var hmacsha256 = new System.Security.Cryptography.HMACSHA256(Encoding.UTF8.GetBytes(secret));
			hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(requestArray[1]));

			var expected_sig = Encoding.UTF8.GetString(hmacsha256.Hash);

			if (sig != expected_sig)
			{
				return null;
			}

			return data;
		}

		public static string Base64_Url_Decode(string input)
		{
			var fixedString = string.Empty;
			var fixedDashString = input.Replace('-', '+');
			var fixedUnderscoreString = fixedDashString.Replace('_', '/');
			if (fixedUnderscoreString.Length % 4 != 0)
			{
				fixedString = String.Format("{0}", fixedUnderscoreString);
				int paddingCount = fixedString.Length % 4;
				while (paddingCount % 4 != 0)
				{
					fixedString += '=';
					paddingCount++;
				}
			}
			else
			{
				fixedString = fixedUnderscoreString;
			}
			var inputBytes = Convert.FromBase64String(fixedString);
			return Encoding.UTF8.GetString(inputBytes);
		}



		void redirectNow()
		{
			if (Vars.DevEnv)
				Response.Redirect("http://www.facebook.com/pages/MixmagTest/135045886539757?v=app_137494456291657");
			else
				Response.Redirect("http://www.facebook.com/MixmagMagazine?v=app_4949752878");
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			JObject req = DecodeFacebookRequest(HttpContext.Current.Request["signed_request"]);

			/*
			{
			  "algorithm": "HMAC-SHA256",
			  "expires": 1312902000,
			  "issued_at": 1312897172,
			  "oauth_token": "217063325011494|2.AQBQXYMXowmqEDC4.3600.1312902000.0-513584417|H4gnYUMiuQm9FmttzwxZRjxhdtU",
			  "page": {
				"id": "247863881900953",
				"liked": false,
				"admin": true
			  },
			  "user": {
				"country": "gb",
				"locale": "en_US",
				"age": {
				  "min": 21
				}
			  },
			  "user_id": "513584417"
			}
			*/

			bool liked = ((JObject)req["page"])["liked"].Value<bool>();

			TestPh.Controls.Add(new LiteralControl("<p>Liked: " + liked.ToString() + "</p>"));
			

			//if (Request.Cookies["MixmagGreatestAuth"] != null && Request.Cookies["MixmagGreatestAuth"].Value != null && Request.Cookies["MixmagGreatestAuth"].Value.Length > 0)//has cookie
			//{
			//    HttpCookie cookie = Request.Cookies["MixmagGreatestAuth"];

			//    if (cookie.Value != "1")
			//    {
			//        redirectNow();
			//    }
			//}
			//else
			//{
			//    redirectNow();
			//}


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
						<div class=""Spacer""></div>
					</p>
					<p class=""Header"">
						" + dj.Name.ToUpper() + @"
					</p>
					<p>
						<div class=""SpacerDotted""></div>
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

					//sbDjs.Append(dj.Name);
					//sbDjs.Append("<BR />");
					//sbDjs.Append(dj.ShortDescription);
					//sbDjs.Append("<BR />");
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
