using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Bobs;


namespace Spotted.Pages.Promoters
{
	[ClientScript]
	public class PromoterUserControl : DsiUserControl
	{
		public PromoterUserControl()
		{
		}

		protected virtual void Page_Init(object sender, System.EventArgs e)
		{
			VerifyUserPermissions();
		}

		public void VerifyUserPermissions()
		{
			if (!ContainerPage.Url.HasPromoterObjectFilter)
				Response.Redirect("/pages/promoters/intro");

			Usr.KickUserIfNotLoggedIn();

			if (CurrentPromoter == null)
				throw new Exception("VerifyUserPermissions(): CurrentPromoter==null");

			if (!CurrentPromoter.IsUsrAllowedAccess(Usr.Current))
				throw new DsiUserFriendlyException(Vars.CANT_VIEW_DETAILS);
		}

		#region Properties
		public Promoter CurrentPromoter
		{
			get
			{
				if (currentPromoter == null)
				{
					if (ContainerPage.Url.HasPromoterObjectFilter)
						currentPromoter = ContainerPage.Url.ObjectFilterPromoter;
				}
				return currentPromoter;
			}
			set
			{
				if (ContainerPage.Url.HasPromoterObjectFilter)
					ContainerPage.Url.ObjectFilterBob = value;

				currentPromoter = value;
			}
		}
		Promoter currentPromoter;
		#endregion
	}
}
