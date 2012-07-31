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
using System.IO;

namespace Spotted.Styled
{
	public partial class Setup : StyledUserControl
	{
		Misc logoFile = null;
		Misc cssFile = null;
		Misc backgroundFile = null;

		protected void Page_Load(object sender, EventArgs e)
		{

		}

		#region Upload Files
		protected void UploadLogoButton_Click(object sender, EventArgs eventArgs)
		{
			logoFile = Misc.UploadFile(InputLogoFile, Usr.Current);
			LogoUrlHiddenTextBox.Text = logoFile != null ? logoFile.Url() : "";
		}
		protected void UploadCssButton_Click(object sender, EventArgs eventArgs)
		{
			cssFile = Misc.UploadFile(InputCssFile, Usr.Current);
			CssUrlHiddenTextBox.Text = cssFile != null ? cssFile.Url() : "";
		}
		protected void UploadBackgroundButton_Click(object sender, EventArgs eventArgs)
		{
			backgroundFile = Misc.UploadFile(InputBackgroundFile, Usr.Current);
			BackgroundUrlHiddenTextBox.Text = backgroundFile != null ? backgroundFile.Url() : "";
		}
		#endregion
	}
}
