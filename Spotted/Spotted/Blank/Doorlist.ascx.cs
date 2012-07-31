using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Bobs;

namespace Spotted.Blank
{
	public partial class Doorlist : BlankUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack && !ContainerPage.Url["EventK"].IsNull && ContainerPage.Url["EventK"].IsInt)
			{
				try
				{
					uiDoorlist.CurrentEvent = new Event(Convert.ToInt32(ContainerPage.Url["EventK"].Value));
				}
				catch
				{
					throw new Exception("Invalid event");
				}
				uiDoorlist.ForExport = true;
				uiDoorlist.DataBind();
			}
		}


	}
}
