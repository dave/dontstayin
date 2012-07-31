using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;
using System.Collections.Generic;
using System.Text;
using Spotted.Support;

namespace Spotted.Pages
{
	public partial class Online : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			List<Usr> onlineUsrs = Usr.GetOnlineUsers().ToList();

			if (Usr.Current != null && !onlineUsrs.Exists(u => u.K == Usr.Current.K))
			{
				onlineUsrs.Add(Usr.Current);
				onlineUsrs.Sort(new UsrNickNameComparer());
			}
			OnlineLabel.Text = onlineUsrs.Count.ToString() + " member" + (onlineUsrs.Count == 1 ? "" : "s") + " online:";

			OnlineDataList.Visible = false;
			StringBuilder sb = new StringBuilder();
			bool doneOne = false;
			foreach (Usr u in onlineUsrs)
			{
				bool isBuddy = Usr.Current != null && Usr.Current.IsBuddy(u);

				if (doneOne)
					sb.Append(", ");

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

				//if (u.ChattingNow)
				//	u.PresenceIconAppend(sb, "selected-onyellow");

				//if (Usr.Current != null && u.K != Usr.Current.K)
				//{
				//    Chat.RoomSpec r = new Chat.RoomSpec(u.K, Usr.Current.K);
				//    r.PinHtmlAppend(sb, "selected-onyellow");
				//}
				doneOne = true;
			}
			OnlineP.InnerHtml = sb.ToString();
		}
	}
}
