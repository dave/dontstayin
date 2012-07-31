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
using Spotted.Controls.ClientSideRepeater;

namespace Spotted.Controls.PagedData.Templates.Galleries
{
	public partial class ItemTemplate : Template
	{
		public enum Columns
		{
			Name,
			Url,
			PicPath,
			OwnerUrl,
			OwnerRollover,
			OwnerNickNameSafe,
			PagedUrl,
			LivePhotos,
			NewHtmlTitle,
			VenueName,
			VenueUrl,
			Date,
			EventName,
			EventUrl
		}
		public static Dictionary<string, string> GetDataItem(Bobs.Gallery g)
		{
			return new Dictionary<string, string>()
			{
				{Columns.Name.ToString(), g.Name},
				{Columns.Url.ToString(), g.Url()},
				{Columns.PicPath.ToString(), g.PicPath},
				{Columns.OwnerUrl.ToString(), g.Owner.Url()},
				{Columns.OwnerRollover.ToString(), g.Owner.Rollover},
				{Columns.OwnerNickNameSafe.ToString(), g.Owner.NickName},
				{Columns.PagedUrl.ToString(), g.PagedUrl()},
				{Columns.EventName.ToString(), g.Event.Name},
				{Columns.EventUrl.ToString(), g.Event.Url()},
				{Columns.VenueName.ToString(), g.Event.Venue.Name},
				{Columns.VenueUrl.ToString(), g.Event.Venue.Url()},
				{Columns.Date.ToString(), Cambro.Misc.Utility.FriendlyDate(g.Event.DateTime.Date)},
				{Columns.LivePhotos.ToString(), g.LivePhotos.ToString()},
			};

		}
	}
}
