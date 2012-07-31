using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Bobs;
using System.Collections.Generic;
using System.Text;

namespace Spotted.Support
{
	public partial class Online : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			time.Text = DateTime.Now.ToString("F");

			List<Usr> onlineUsrs = Usr.GetOnlineUsers().ToList();

			if (Usr.Current != null && !onlineUsrs.Exists(u => u.K == Usr.Current.K))
			{
				onlineUsrs.Add(Usr.Current);
				onlineUsrs.Sort(new UsrNickNameComparer());
			}
			OnlineLabel.Text = onlineUsrs.Count.ToString() + " user" + (onlineUsrs.Count == 1 ? "" : "s") + " online";

			OnlineDataList.Visible = false;
			StringBuilder sb = new StringBuilder();
			bool doneOne = false;
			foreach (Usr u in onlineUsrs)
			{
				bool isBuddy = Usr.Current != null && Usr.Current.IsBuddy(u);

				if (doneOne)
					sb.Append("<br />");

				sb.Append("<a");
				sb.AppendAttribute("href", u.Url());
				u.RolloverAppend(sb);
				sb.Append(">");

				if (isBuddy)
					sb.Append("<b>");

				sb.Append(u.NickName);

				if (isBuddy)
					sb.Append("</b>");

				sb.Append("</a>");

				if (u.ChattingNow)
					u.PresenceIconAppend(sb, "selected-onyellow");

				if (Usr.Current != null && u.K != Usr.Current.K)
				{
					Chat.RoomSpec r = new Chat.RoomSpec(u.K, Usr.Current.K);
					r.PinHtmlAppend(sb, "selected-onyellow");
				}
				doneOne = true;
			}
			OnlineP.InnerHtml = sb.ToString();

			if (Vars.DevEnv)
				Response.Expires = 0;
			else
				Response.Expires = 3;
		}


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion
	}

	class UsrNickNameComparer : IComparer<Usr>
	{
		#region IComparer<Usr> Members

		public int Compare(Usr x, Usr y)
		{
			return x.NickName.CompareTo(y.NickName);
		}

		#endregion
	}
}
