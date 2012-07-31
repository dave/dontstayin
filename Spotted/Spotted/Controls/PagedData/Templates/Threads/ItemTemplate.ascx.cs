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

namespace Spotted.Controls.PagedData.Templates.Threads
{
	public partial class ItemTemplate : Template
	{
		public enum Columns
		{
			Title,
			User
		}
		public static Dictionary<string, string> GetDataItem(Thread g)
		{
			return new Dictionary<string, string>()
			{
				 {Columns.Title.ToString(), g.Name},
				 {Columns.User.ToString(), g.Usr.NickName}
			};

		}
	}
}
