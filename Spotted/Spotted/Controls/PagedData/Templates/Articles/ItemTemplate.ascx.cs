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

namespace Spotted.Controls.PagedData.Templates.Articles
{
	public partial class ItemTemplate : Template
	{
		public enum Columns
		{
			PicPath,
			Title,
			Summary,
			OwnerNickName,
			OwnerUrl,
			DiscussionUrl,
			Comments,
			Views,
			Url,
			Date
		}
		public static Dictionary<string, string> GetDataItem(Article a)
		{
			return new Dictionary<string, string>()
			{
				{Columns.PicPath.ToString(), a.PicPath},
				{Columns.Title.ToString(), a.Title},
				{Columns.Summary.ToString(), a.Summary},
				{Columns.OwnerUrl.ToString(), a.Owner.Url()},
				{Columns.OwnerNickName.ToString(), a.Owner.NickName},
				{Columns.DiscussionUrl.ToString(), a.UrlDiscussion()},
				{Columns.Comments.ToString(), a.TotalComments.ToString()},
				{Columns.Views.ToString(), a.Views.ToString()},
				{Columns.Url.ToString(), a.Url()},
				{Columns.Date.ToString(), Cambro.Misc.Utility.FriendlyDate(a.EnabledDateTime)},
			};

		}
	}
}
