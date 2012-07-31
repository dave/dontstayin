using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Spotted.Pages
{
	public partial class AutoLogin : DsiUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			ContainerPage.UseLeftHandSideForContent = true;
			AutoLogin_RedirectUrl.Value = ContainerPage.Url.LoggedInPlainUrl.Length > 0 ? ContainerPage.Url.LoggedInPlainUrl : "/";
			AutoLogin_UsrK.Value = ContainerPage.Url.LoginPartUsrK.ToString();
			AutoLogin_String.Value = ContainerPage.Url.LoginPartLoginString;
			AutoLogin_LogOutFirst.Value = ContainerPage.Url.LoginPartLogOutFirst.ToString();
			AutoLogin_UsrEmail.Value = ContainerPage.Url.LoginPartUsrEmail;
			AutoLogin_UsrIsSkeleton.Value = ContainerPage.Url.LoginPartUsrIsSkeleton.ToString();
			AutoLogin_UsrIsEnhancedSecurity.Value = ContainerPage.Url.LoginPartUsrIsEnhancedSecurity.ToString();
			AutoLogin_UsrIsFacebookNotConfirmed.Value = ContainerPage.Url.LoginPartUsrIsFacebookNotConfirmed.ToString();
			AutoLogin_UsrNeedsCaptcha.Value = ContainerPage.Url.LoginPartUsrNeedsCaptcha.ToString();
			AutoLogin_UsrCaptchaEncrypted.Value = ContainerPage.Url.LoginPartUsrCaptchaEncrypted.ToString();

			Bobs.Place home = null;
			try
			{
				home = new Bobs.Place(ContainerPage.Url.LoginPartUsrHomePlaceK);
			}
			catch { }

			if (home != null)
			{
				AutoLogin_HomePlaceK.Value = home.K.ToString();
				AutoLogin_HomePlaceName.Value = home.NamePlain;
				AutoLogin_HomeCountryK.Value = home.Country.K.ToString();
				AutoLogin_HomeCountryName.Value = home.Country.FriendlyName;
				AutoLogin_HomeGoodMatch.Value = "true";
			}
			else
			{
				AutoLogin_HomeGoodMatch.Value = "false";
			}

			Bobs.MusicType mt = null;
			try
			{
				mt = new Bobs.MusicType(ContainerPage.Url.LoginPartUsrFavouriteMusicK);
			}
			catch { }

			AutoLogin_FavouriteMusicK.Value = mt == null ? "1" : mt.K.ToString();
			AutoLogin_SendSpottedEmails.Value = ContainerPage.Url.LoginPartUsrSendSpottedEmails.ToString();
			AutoLogin_SendEflyers.Value = ContainerPage.Url.LoginPartUsrSendEflyers.ToString();

		}
	}
}
