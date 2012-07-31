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
	public partial class Captcha : BlankUserControl
	{


		protected void Page_Init(object sender, EventArgs e)
		{
			Cambro.Web.Helpers.TieButton(HipChallengeTextBox, DoneButton);
			uint attempts = Caching.Instances.MainCounterStore.GetCounter(Usr.GetSpamBotDefeaterAttemptCountCacheKey(Usr.Current.K, DateTime.Today), () => 0);
			uint fails = Caching.Instances.MainCounterStore.GetCounter(Usr.GetSpamBotDefeaterFailCountCacheKey(Usr.Current.K, DateTime.Today), () => 0);
			if (attempts > 200 || fails > 30)
				throw new Exception("Oops! - too many fails or attempts. The counter will reset at midnight.");

		}
		protected void Page_Load(object sender, EventArgs e)
		{
			Caching.Instances.MainCounterStore.Increment(Usr.GetSpamBotDefeaterAttemptCountCacheKey(Usr.Current.K, DateTime.Today), () => 0);
			if (!Page.IsPostBack)
			{
				string text = Cambro.Misc.Utility.GenRandomChars(5).ToUpper();
				string encryptedText = Cambro.Misc.Utility.Encrypt(text, DateTime.Now.AddHours(1));
				this.ViewState["HipChallengeExcryptedText"] = encryptedText;
				HipImage.Src = "/support/hipimage.aspx?a=" + encryptedText;
			}
		}
		#region Done_Click
		protected void Done_Click(object sender, EventArgs eventArgs)
		{
			if (Page.IsValid)
			{
				Usr.ResetAllSpamBotDefeaterCounters(Usr.Current.K);

				if (Request.QueryString["Url"] != null && Request.QueryString["Url"].Length > 0)
					Response.Redirect(Request.QueryString["Url"]);
				else
					Response.Redirect("/");
			}
		}
		#endregion

		public void HipVal(object o, ServerValidateEventArgs e)
		{
			string realText = Cambro.Misc.Utility.Decrypt((string)this.ViewState["HipChallengeExcryptedText"]);
			e.IsValid = HipChallengeTextBox.Text.ToUpper().Equals(realText);
			if (Page.IsPostBack && !e.IsValid)
			{
				Caching.Instances.MainCounterStore.Increment(Usr.GetSpamBotDefeaterFailCountCacheKey(Usr.Current.K, DateTime.Today), () => 0);

				string text = Cambro.Misc.Utility.GenRandomChars(5).ToUpper();
				string encryptedText = Cambro.Misc.Utility.Encrypt(text, DateTime.Now.AddHours(1));
				this.ViewState["HipChallengeExcryptedText"] = encryptedText;
				HipImage.Src = "/support/hipimage.aspx?a=" + encryptedText;
				HipChallengeTextBox.Text = string.Empty;
			}
		}
	}
}
