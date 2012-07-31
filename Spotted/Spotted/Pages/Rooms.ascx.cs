using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using Bobs;
using SpottedScript.Controls.ChatClient.Shared;

namespace Spotted.Pages
{
	public partial class Rooms : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();

			Testing.Visible = (Usr.Current != null && Usr.Current.IsAdmin) || ContainerPage.Url["System"].Exists;

			Query q = new Query();
			if (!Vars.DevEnv)
				q.CacheDuration = TimeSpan.FromMinutes(5);
			q.QueryCondition = new Q(RoomPin.Columns.Pinned, true);
			//q.QueryCondition = new Q(RoomPin.Columns.Pinned, true);
			q.GroupBy = new GroupBy(RoomPin.Columns.RoomGuid);
			q.ExtraSelectElements.Add("count", "COUNT(*)");
			q.OrderBy = new OrderBy("COUNT(*) DESC");
			q.TopRecords = 100;
			q.TableElement = new Join(RoomPin.Columns.UsrK, Usr.Columns.K);
			q.Columns = new ColumnSet(RoomPin.Columns.RoomGuid);
			RoomPinSet rps = new RoomPinSet(q);
			StringBuilder sb = new StringBuilder();
			int count = 1;
			foreach (RoomPin rp in rps)
			{
				Bobs.Chat.RoomSpec spec = Bobs.Chat.RoomSpec.FromGuid(rp.RoomGuid);
				if (spec != null && spec.RoomType == RoomType.Normal && !spec.IsDefaultRoom && spec.CheckPermission(Usr.Current, false))
				{
					sb.Append("<p class=\"CleanLinks\">");
					spec.LinkHtmlAppend(sb);
					sb.Append("<small> - ");
					int pins = ((int)rp.ExtraSelectElements["count"]); 
					sb.Append(pins.ToString("#,##0"));
					sb.Append(" pin");
					if (pins != 1)
						sb.Append("s");
					sb.Append("</small>");
					sb.Append("</p>");
					count++;
				}
				if (count > 20)
					break;
			}
			PopularRooms.Controls.Add(new LiteralControl(sb.ToString()));

			if (!Page.IsPostBack)
			{
				PopupAminationsOff.Checked = Prefs.Current["ChatClientAnimatePopups"].Exists && Prefs.Current["ChatClientAnimatePopups"] == 0;
				PopupAminationsOn.Checked = !PopupAminationsOff.Checked;
			}
		}
		public void PopupAminationsChange(object o, EventArgs e)
		{
			Prefs.Current["ChatClientAnimatePopups"] = PopupAminationsOn.Checked ? "1" : "0";
			Response.Redirect("/pages/rooms");
		}
		public string ChatRoomLink(Bobs.Chat.RoomSpec spec)
		{
			//Guid roomGuid = spec.Guid;
			StringBuilder sb = new StringBuilder();
			spec.LinkHtmlAppend(sb);
			return sb.ToString();
			//return "<a href=\"#\" onclick=\"chatClientPinRoom('" + roomGuid.Pack() + "');return false;\">" + spec.GetName(Usr.Current) + "<img id=\"RoomsPage" + roomGuid.Pack() + "\" src=\"/gfx/pin2.gif\" style=\"margin-left:2px;\" border=\"0\" width=\"9\" height=\"8\" /></a>";
		}

	}
}
