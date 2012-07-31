using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;

namespace Spotted.Pages
{
	public partial class AreYouDj : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("You must be logged in to use this page.");

			if (!Page.IsPostBack)
			{
				IsDjYes.Checked = Usr.Current.IsDj.HasValue && Usr.Current.IsDj.Value;
				IsDjNo.Checked = Usr.Current.IsDj.HasValue && !Usr.Current.IsDj.Value;
			}
		}
		public void SaveNow(object sender, EventArgs e)
		{
			if (Page.IsValid)
			{
				Usr.Current.IsDj = IsDjYes.Checked;
				Usr.Current.Update();
				SavedPanel.Visible = true;
			}
		}
		public void IsDjVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = (IsDjYes.Checked || IsDjNo.Checked);
		}
	}
}
