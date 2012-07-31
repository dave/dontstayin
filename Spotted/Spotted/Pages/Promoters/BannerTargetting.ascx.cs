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

namespace Spotted.Pages.Promoters
{
	[ClientScript]
	public partial class BannerTargetting : PromoterUserControl
	{
		public BannerTargetting()
		{
		}

		#region BannerK
		protected int BannerK
		{
			get
			{
				return ContainerPage.Url["BannerK"];
			}
		}
		#endregion
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			if (!Usr.Current.IsAdmin)
			{
				throw new DsiUserFriendlyException("You can't view these details.");
			}

			ContainerPage.SetPageTitle("Promoter administration - Advanced banner options");
			
			if (!IsPostBack)
			{
				Banner b = new Banner(BannerK);
				SetUpCheckBoxSelectedStatuses(b.TargettingPropertiesToExclude);
				SetFrequencyCappingValue(b.FrequencyCapPerIdentifierPerDay);
				txtPriority.Text = b.Priority.ToString();
				cbAlwaysShow.Checked = b.AlwaysShow;
				if (b.DisplayDuration == null)
				{
					this.uiUseDefaultBannerRotationRadio.Checked = true;
					this.uiCustomRotationValue.Text = Common.Settings.DefaultBannerDurationInSeconds.ToString();
					this.uiCustomRotationValue.Style["visibility"] = "hidden";
				}
				else
				{
					this.uiUseCustomBannerRotationRadio.Checked = true;
					this.uiCustomRotationValue.Text = b.DisplayDuration.ToString();
					this.uiCustomRotationValue.Style["visibility"] = "";
				}
			}
			
		}

		#region SetUpCheckBoxSelectedStatuses and SetFrequencyCappingValue logic
		private void SetUpCheckBoxSelectedStatuses(bool[] targettingPropertiesToExclude)
		{
			foreach (Control c in pnlTargettingProperties.Controls)
			{
				if (c is CheckBoxList)
				{
					foreach (ListItem li in ((CheckBoxList)c).Items)
					{
						li.Selected = !targettingPropertiesToExclude[int.Parse(li.Value)];
					}
				}
			}
		}
		private void SetFrequencyCappingValue(int frequencyCapPerIdentifierPerDay)
		{
			if (frequencyCapPerIdentifierPerDay > 0)
			{
				this.txtFrequencyCap.Text = frequencyCapPerIdentifierPerDay.ToString();
			}
		}
		#endregion

		protected void btnSave_Click(object sender, EventArgs e)
		{
			Banner b = new Banner(BannerK);

			foreach (Control c in pnlTargettingProperties.Controls)
			{
				if (c is CheckBoxList)
				{
					foreach (ListItem li in ((CheckBoxList)c).Items)
					{
						b.SetTargettingProperty((Banner.TargettingProperty)Enum.ToObject(typeof(Banner.TargettingProperty), int.Parse(li.Value)), !li.Selected);
					}
				}
			}

			if (this.txtFrequencyCap.Text.Trim().Length > 0)
			{
				b.FrequencyCapPerIdentifierPerDay = int.Parse(this.txtFrequencyCap.Text);
			}
			else
			{
				b.FrequencyCapPerIdentifierPerDay = -1;
			}

			b.Priority = int.Parse(txtPriority.Text);
			b.AlwaysShow = cbAlwaysShow.Checked;
			b.DisplayDuration = this.uiUseCustomBannerRotationRadio.Checked ? int.Parse(this.uiCustomRotationValue.Text) : null as int?;
			b.Update();

			Response.Redirect(CurrentPromoter.UrlApp("banneroptions", "mode", "edit", "bannerk", BannerK.ToString()));
		}

	}
}
