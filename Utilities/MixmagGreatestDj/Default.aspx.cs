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
	public partial class _Default : System.Web.UI.Page
	{
		public DateTime? IssueDate = null;
		public int? PageNumber = null;
		public int? CoverId = null;

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
					sbDjs.Append("<h2 style=\"margin-top: 35px; margin-bottom: -5px;\">" + dj.Name + "</h2>");
					sbDjs.Append("<div style=\"height: 100px; clear:both; margin-bottom: 20px;\">");
					{
						sbDjs.Append("<div style=\"height: 100px; width: 100px; float: left; margin-right: 10px;\"><a href=\"/" + dj.UrlName + "\"><img src=\"" + dj.ImageUrl + "\" width=\"100\" height=\"100\" /></a></div>");
						//sbDjs.Append("<div style=\"height: 100px; width: 150px; float: right; margin-left: 10px; \">" + h.GetFlash("djvid" + dj.K.ToString(), 100, 150, false, "load", "http://www.youtube.com/v/" + dj.YoutubeId + "&rel=1") + "</div>");
						sbDjs.Append("<p>" + dj.Description + "<br /><a href=\"/" + dj.UrlName + "\"><b>Vote for " + dj.Name + "</b></a></p>");
					}
					sbDjs.Append("</div>");
					//sbDjs.Append("<div style=\"clear: both;\"></div>");
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

	}
}
