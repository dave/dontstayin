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
using SpottedLibrary.Controls.BuddyChooser;
using Bobs;
using Common;
using System.Collections.Generic;

namespace Spotted.Controls
{
	[ClientScript]
	public partial class BuddyChooser : GenericUserControl, IBuddyChooser, IIncludesJs
	{
		public BuddyChooser()
		{
			this.Init += new EventHandler(BuddyChooser_Init);
		}

		public void IncludeJsInternal() { IncludeJs(this.Page); }
		public static void IncludeJs(Page page)
		{
			ScriptSharp.RegisterInclude(page, typeof(Spotted.Controls.MultiBuddyChooser));

			ScriptSharp.RegisterInclude(page, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		}

		void BuddyChooser_Init(object sender, EventArgs e)
		{
			//if (Common.Settings.UseCometToServeRequests)
			//{
			//    this.uiHtmlAutoComplete.WebServiceMethod = "";
			//    this.uiHtmlAutoComplete.WebServiceUrl = "";
			//    this.uiHtmlAutoComplete.CometServiceUrl = @"/WebServices/CometAutoComplete/GetBuddiesThenUsrs.ashx";
			//}
			
		}
		public int? SelectedBuddyK
		{
			get
			{
				try
				{
					return int.Parse(this.uiHtmlAutoComplete.Value);
				}
				catch
				{
					return null;
				}
			}
			set
			{
				if (value != null)
				{
					Usr usr = new Usr(value.Value);
					this.uiHtmlAutoComplete.Text = usr.NickName;
					this.uiHtmlAutoComplete.Value = usr.K.ToString();
				}
				else
				{
					this.uiHtmlAutoComplete.Text = "";
					this.uiHtmlAutoComplete.Value = "";
				}
			}
		}
	}
}
