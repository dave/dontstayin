﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Spotted.Admin
{
	public partial class MemcachedStats : AdminUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			uiMemcachedStatsGridView.DataSource = Bobs.Vars.GetMemcachedStats();
			uiMemcachedStatsGridView.DataBind();
		}
		protected void FlushAll(object sender, EventArgs e)
		{
			if (!Bobs.Usr.Current.IsADeveloper)
			{
			//	throw new Exception("I told you not to!!!");
			}

			Bobs.Vars.FlushAllMemcached();
		}
	}
}
