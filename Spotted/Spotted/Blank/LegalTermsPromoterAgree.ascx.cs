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

namespace Spotted.Blank
{
	public partial class LegalTermsPromoterAgree : BlankUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{

		}
		public void TermsVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = TermsCheckbox.Checked;
		}
		#region PrefsUpdateClick
		protected void PrefsUpdateClick(object sender, EventArgs eventArgs)
		{
			if (Page.IsValid)
			{
				Usr.Current.LegalTermsPromoter2 = true;
				Usr.Current.Update();

				if (Request.QueryString["Url"] != null && Request.QueryString["Url"].Length > 0)
					Response.Redirect(Request.QueryString["Url"]);
				else
					Response.Redirect("/pages/home");
			}
		}
		#endregion
	}
}
