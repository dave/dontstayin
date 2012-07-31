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

namespace Spotted.Templates.Usrs
{
	public partial class DateEdit : System.Web.UI.UserControl
	{
		protected Usr CurrentUsr
		{
			get
			{
				if (currentUsr == null)
					currentUsr = (Usr)((DataListItem)NamingContainer).DataItem;
				return currentUsr;
			}
		}
		Usr currentUsr;
	}
}
