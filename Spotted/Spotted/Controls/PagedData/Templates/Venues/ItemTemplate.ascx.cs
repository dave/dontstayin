using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Bobs;
using Spotted.Controls.ClientSideRepeater;

namespace Spotted.Controls.PagedData.Templates.Venues
{
	public partial class ItemTemplate : Template
	{
		public enum Columns
		{
			AnyPicPath,
			Url,
			Name,
			NextEventDate,
			NextEventUrl,
			NextEventName,
			LastChatUrl,
			LastChatName,
			LastChatDate
		}
		public static Dictionary<string, string> GetDataItem(Venue v)
		{
			var nextEvents = v.ChildEvents(new Q(Event.Columns.DateTime, QueryOperator.GreaterThanOrEqualTo, DateTime.Now.Day), new KeyValuePair<object, OrderBy.OrderDirection>(Event.Columns.DateTime, OrderBy.OrderDirection.Descending)).Page(1, 10);
			var lastChat = v.ChildThreads(new Q(Thread.Columns.Enabled, true), new KeyValuePair<object, OrderBy.OrderDirection>(Thread.Columns.LastPost, OrderBy.OrderDirection.Descending));
			return new Dictionary<string, string>()
			{
				{Columns.AnyPicPath.ToString(), v.AnyPicPath},
				{Columns.Url.ToString(), v.Url()},
				{Columns.Name.ToString(), v.Name},
				{Columns.NextEventDate.ToString(), nextEvents.Any() ? Cambro.Misc.Utility.FriendlyDate(nextEvents[0].DateTime) : ""},
				{Columns.NextEventName.ToString(), nextEvents.Any() ? nextEvents[0].Name : ""},
				{Columns.NextEventUrl.ToString(), nextEvents.Any() ? nextEvents[0].Url() : ""},

				{Columns.LastChatDate.ToString(), lastChat.Any() ? Cambro.Misc.Utility.FriendlyDate(lastChat[0].DateTime) : ""},
				{Columns.LastChatName.ToString(), lastChat.Any() ? lastChat[0].Name : ""},
				{Columns.LastChatUrl.ToString(), lastChat.Any() ? lastChat[0].Url() : ""},


			};
		}
	}
}
