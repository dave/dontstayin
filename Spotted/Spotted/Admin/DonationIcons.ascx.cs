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
	public partial class DonationIcons : AdminUserControl
	{
		protected void Load(object sender, EventArgs e)
		{
			var d = new DonationIcon(int.Parse(uiK.Text));
			this.uiName.Text = d.IconName;
			this.uiText.Text = d.IconText;
			this.uiGuid.Text = d.ImgGuid.Value.ToString();
			this.uiExtension.Text = d.ImgExtension;
			this.uiThreadK.Text = d.ThreadK.ToString();
			this.uiActivationDate.Date = d.StartDateTime;
			this.uiActivationTime.Time = d.StartDateTime;
			this.uiEnabled.Checked = d.Enabled;
			this.uiVatable.Checked = d.Vatable.Value;
			SetLink(d.K);
		}

		protected void Save(object sender, EventArgs e)
		{
			var d = new DonationIcon() {PriceWhenActive = 5, PriceWhenRetroactive = 10, DonatePageControl = DonationIcon.Control.Basic};
			if (!string.IsNullOrEmpty(uiK.Text))
			{
				d = new DonationIcon(int.Parse(uiK.Text));
			}
			d.IconName = this.uiName.Text;
			d.IconText = this.uiText.Text;
			d.ImgGuid = new Guid(this.uiGuid.Text);
			d.ImgExtension = this.uiExtension.Text;
			d.ThreadK = (!string.IsNullOrEmpty(this.uiThreadK.Text)) ? int.Parse(this.uiThreadK.Text) : 0;
			d.StartDateTime = uiActivationDate.Date.AddHours(uiActivationTime.Time.Hour).AddMinutes(uiActivationTime.Minute);
			d.Enabled = this.uiEnabled.Checked;
			d.Vatable = this.uiVatable.Checked;

			d.Update();
			this.uiK.Text = d.K.ToString();
			SetLink(d.K);
		}

		private void SetLink(int k)
		{
			this.uiLink.Text = "/pages/icons/k-" + k;
			this.uiLink.NavigateUrl = "/pages/icons/k-" + k;
		}
	}
}
