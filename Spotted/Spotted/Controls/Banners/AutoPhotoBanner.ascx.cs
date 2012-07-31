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
	public partial class AutoPhotoBanner : AutoBanner
	{
		protected HtmlGenericControl TextDiv;
		protected PlaceHolder ContentPh;

		bool DoneBind = false;

		private void Page_Load(object sender, System.EventArgs eargs)
		{
			Bind();
		}

		public override void Bind()
		{
			if (!DoneBind)
			{
				if (LinkTargetBlank)
					TextDiv.Attributes["onclick"] = "window.open('" + BannerUrl + "');";
				else
					TextDiv.Attributes["onclick"] = "document.location='" + BannerUrl + "';";

				if (!Vars.Netscape)
				{
					TextDiv.Style["width"] = "450px";
					TextDiv.Style["height"] = "50px";
				}
				else
				{
					TextDiv.Style["width"] = "450px";
					TextDiv.Style["height"] = "50px";
				}

				if (FirstLine.Length > 0)
					ContentPh.Controls.Add(new LiteralControl("<b style=\"font-size:" + FontSize + "px;\">" + FirstLine + "</b>"));

				if (SecondLine.Length > 0)
					ContentPh.Controls.Add(new LiteralControl("<div style=\"margin-top:2px;\">" + SecondLine + "</div>"));

				if (ThirdLine.Length > 0 && SecondLine.Length == 0)
					ContentPh.Controls.Add(new LiteralControl("<div style=\"color:#A58319;margin-top:2px;\">" + ThirdLine + "</div>"));

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
						return 18;
					else if (FirstLine.Length <= 33)
						return 17;
					else if (FirstLine.Length <= 36)
						return 16;
					else if (FirstLine.Length <= 38)
						return 15;
					else if (FirstLine.Length <= 40)
						return 14;
					else if (FirstLine.Length <= 50)
						return 12;
					else
						return 10;
					
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
