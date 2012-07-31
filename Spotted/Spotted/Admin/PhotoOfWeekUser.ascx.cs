using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Bobs;

namespace Spotted.Admin
{
	public partial class PhotoOfWeekUser : AdminUserControl
	{
		protected void GetPhoto(object o, EventArgs e)
		{
			Photo p = new Photo(int.Parse(this.uiPhotoK.Text));
			this.uiPhotoDetails.Visible = true;
			this.uiPhotoKLabel.Text = p.K.ToString();
			this.uiPhotoImg.Src = p.WebPath;
			this.uiPhotoImg.Width = p.WebWidth;
			this.uiPhotoImg.Height = p.WebHeight;
			this.uiPhotoOfWeek.Checked = p.PhotoOfWeekUser;
			this.uiPhotoOfWeekUserCaption.Text = p.PhotoOfWeekUserCaption;
			this.uiPhotoOfWeekUserBlocked.Checked = p.BlockedFromPhotoOfWeekUser;
		}

		protected void UpdatePhoto(object o, EventArgs e)
		{
			Photo p = new Photo(int.Parse(this.uiPhotoKLabel.Text));
			p.SetAsPhotoOfWeek(uiPhotoOfWeek.Checked, uiPhotoOfWeekUserCaption.Text, false, uiResetDateTime.Checked);

			p.BlockedFromPhotoOfWeekUser = uiPhotoOfWeekUserBlocked.Checked;

			p.Update();
		}
	}
}
