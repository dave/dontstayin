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

namespace Spotted.Controls.Banners
{
	public partial class AutoLeaderboard : AutoBanner
	{
		protected HtmlTable BannerTable;
		protected HtmlTableCell PicCell, TextCell;
		protected HtmlImage Pic;
		protected HtmlGenericControl TextDiv;

		bool DoneBind = false;

		private void Page_Load(object sender, System.EventArgs eargs)
		{
			Bind();
		}

		public override void Bind()
		{
			if (!DoneBind)
			{
				if (FirstLine != null)
				{

					if (LinkTargetBlank)
						BannerTable.Attributes["onclick"] = "window.open('" + BannerUrl + "');";
					else
						BannerTable.Attributes["onclick"] = "document.location='" + BannerUrl + "';";

					if (!PicGuid.Equals(Guid.Empty))
					{
						Pic.Src = Storage.Path(PicGuid);
						if (!Vars.Netscape)
						{
							TextDiv.Style["width"] = "637";
							//TextDiv.Style["background-image"] = "url(/gfx/auto-leaderboard-bg.gif)";
						}
						else
							TextDiv.Style["width"] = "622";
					}
					else
					{
						PicCell.Visible = false;
						if (!Vars.Netscape)
						{
							TextDiv.Style["width"] = "728";
							//TextDiv.Style["background-image"] = "url(/gfx/auto-leaderboard-bg-big.gif)";
						}
						else
							TextDiv.Style["width"] = "711";
					}
					//TextDiv.Style["background-repeat"] = "none";
					//TextDiv.Style["background-attachment"] = "fixed";
					//TextDiv.Style["background-position"] = "left top";
					if (Vars.Netscape)
					{
						TextDiv.Style["height"] = "79px";
					}

					if (FirstLine.Length > 0)
						TextDiv.Controls.Add(new LiteralControl("<b style=\"font-size:" + FontSize + "px;\">" + FirstLine + "</b>"));

					if (SecondLine.Length > 0)
						TextDiv.Controls.Add(new LiteralControl("<div style=\"margin-top:2px;font-size:12px;\">" + SecondLine + "</div>"));

					if (ThirdLine.Length > 0)
						TextDiv.Controls.Add(new LiteralControl("<div style=\"color:#A58319;margin-top:2px;font-size:12px;\">" + ThirdLine + "</div>"));
				}
				DoneBind = true;
			}

		}

		#region FontSize
		public override int FontSize
		{
			get
			{
				if (fontSize == 0)
				{
					if (FirstLine.Length <= 30)
						return 35;
					else if (FirstLine.Length <= 33)
						return 31;
					else if (FirstLine.Length <= 36)
						return 27;
					else if (FirstLine.Length <= 38)
						return 24;
					else if (FirstLine.Length <= 40)
						return 22;
					else if (FirstLine.Length <= 50)
						return 20;
					else
						return 18;
				}
				else
					return fontSize;
			}
			set
			{
				fontSize = value;
			}
		}
		int fontSize;
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion
	}

}
