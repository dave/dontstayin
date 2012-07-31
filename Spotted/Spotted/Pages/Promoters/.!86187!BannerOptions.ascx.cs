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
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Bobs.DataHolders;
using Common.Clocks;
using Common;

namespace Spotted.Pages.Promoters
{
    public partial class BannerOptions : PromoterUserControl
    {
        #region Page_Init
        protected override void Page_Init(object sender, System.EventArgs e)
        {
            base.Page_Init(sender, e);
        }
        #endregion

		#region Page_Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			if (!Usr.Current.IsPromoter)
			{
				throw new DsiUserFriendlyException("You must be a promoter to view this page");
			}
			if (Edit)
			{
				if (!Usr.Current.CanEdit(CurrentBanner))
					throw new DsiUserFriendlyException("You can't edit this banner!");
			}
			if (Mode.Equals(Modes.Delete))
			{
				if (!Usr.Current.CanDelete(CurrentBanner))
					throw new DsiUserFriendlyException("You can't delete this banner!");
			}

			if (Edit && Usr.Current.IsAdmin)
			{
				ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a href=\"http://old.dontstayin.com/login-" + Usr.Current.K + "- " + Usr.Current.LoginString + "/admin/banner?ID=" + CurrentBanner.K + "\">Edit banner (admin)</a></p>"));
			}

			ContainerPage.SetPageTitle("Banner administration");

			if (!Page.IsPostBack)
			{
				if (Mode.Equals(Modes.Edit))
					ChangePanel(PanelEdit);
				else if (Mode.Equals(Modes.Delete))
					ChangePanel(PanelDelete);
				else
					throw new Exception("Wrong mode");

