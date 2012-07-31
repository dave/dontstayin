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
	public partial class BannerEditAutomatic : BlankUserControl
	{
		public string SaveScript { get; set; }

		// won't be used for database, just manipulating data
		public Banner CurrentBanner
		{
			get
			{
				if (ViewState["CurrentBanner"] == null)
				{
					ViewState["CurrentBanner"] = new Banner();
				}
				return (Banner)ViewState["CurrentBanner"];
			}
			set
			{
				ViewState["CurrentBanner"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				string text = Request.Params["text"];
				if (text != "")
				{
					CurrentBanner.SetAutomaticBannerText(text);
				}
				CurrentBanner.Position = (Banner.Positions)Convert.ToInt32(Request.Params["position"]);
				CurrentBanner.EventK = Convert.ToInt32(Request.Params["eventk"]);

				CustomiseFirstLine.Text = CurrentBanner.CustomiseFirstLine;
				CustomiseFirstLineSize.Text = CurrentBanner.CustomiseFirstLineSize.ToString();
				CustomiseSecondLine.Text = CurrentBanner.CustomiseSecondLine;
				CustomiseThirdLine.Text = CurrentBanner.CustomiseThirdLine;
			}
			CustomisePreview();
		}


		public void Customise_Preview(object o, System.EventArgs e)
		{
			CustomisePreview();
		}
		void CustomisePreview()
		{
			Spotted.Controls.Banners.AutoBanner autoBannerControl = null;

			switch (CurrentBanner.Position)
			{
				case Banner.Positions.Leaderboard:
					autoBannerControl = (Spotted.Controls.Banners.AutoLeaderboard)this.LoadControl("/Controls/Banners/AutoLeaderboard.ascx");
					//autoBannerControl.PicGuid = CurrentBanner.Event.AnyPic;
					break;
				case Banner.Positions.PhotoBanner:
					autoBannerControl = (Spotted.Controls.Banners.AutoPhotoBanner)this.LoadControl("/Controls/Banners/AutoPhotoBanner.ascx");
					//autoBannerControl.PicGuid = CurrentBanner.Event.AnyPic;
					break;
				default: break;
			}
			if (autoBannerControl != null)
			{
				if (CurrentBanner.Event.HasAnyPic)
					autoBannerControl.PicGuid = CurrentBanner.Event.AnyPic;

				autoBannerControl.FirstLine = CurrentBanner.CustomReplacements(CustomiseFirstLine.Text);
				autoBannerControl.SecondLine = CurrentBanner.CustomReplacements(CustomiseSecondLine.Text);
				autoBannerControl.ThirdLine = CurrentBanner.CustomReplacements(CustomiseThirdLine.Text);
				int fontsize;
				if (int.TryParse(CustomiseFirstLineSize.Text, out fontsize))
				{
					autoBannerControl.FontSize = fontsize;
				}
				autoBannerControl.DataBind();
				CustomiseBanner.Controls.Clear();
				CustomiseBanner.Controls.Add(autoBannerControl);
			}
		}

		public void SaveButton_Click(object o, System.EventArgs e)
		{
			CurrentBanner.CustomiseFirstLine = Cambro.Web.Helpers.StripHtml(CustomiseFirstLine.Text);

			int fontsize;
			if (int.TryParse(CustomiseFirstLineSize.Text, out fontsize))
			{
				CurrentBanner.CustomiseFirstLineSize = fontsize;
			}

			CurrentBanner.CustomiseSecondLine = Cambro.Web.Helpers.StripHtml(CustomiseSecondLine.Text);
			CurrentBanner.CustomiseThirdLine = Cambro.Web.Helpers.StripHtml(CustomiseThirdLine.Text);
			CurrentBanner.DisplayType = Banner.DisplayTypes.CustomAutoEventBanner;

			EmbedSaveScript(CurrentBanner.GetAutomaticBannerTextXml());
		}
		public void RemoveButton_Click(object o, System.EventArgs e)
		{
			CurrentBanner.CustomiseFirstLine = "";
			CurrentBanner.CustomiseFirstLineSize = 0;
			CurrentBanner.CustomiseSecondLine = "";
			CurrentBanner.CustomiseThirdLine = "";
			CurrentBanner.DisplayType = Banner.DisplayTypes.AutoEventBanner;

			EmbedSaveScript(CurrentBanner.GetAutomaticBannerTextXml());
		}

		private void EmbedSaveScript(string xml)
		{
			SaveScript = "<script>Save(escape(\"" + xml.Replace("\"", "\\\"") + "\"));</script>";
		}
	}
}
