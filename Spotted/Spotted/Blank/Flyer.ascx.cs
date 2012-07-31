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

namespace Spotted.Blank
{
	public partial class Flyer : BlankUserControl
	{
		protected string Src { get; set; }
		protected string Href { get; set; }
		protected string BackgroundColorStyle { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				int flyerK = ContainerPage.Url["k"].ValueInt;
				if (flyerK > 0)
				{
					Bobs.Flyer f = new Bobs.Flyer(flyerK);
					Src = f.ImgSrcTrackingViews;
					Href = f.LinkTargetUrlTrackingClicks;
					if (f.BackgroundColor != "") BackgroundColorStyle = @"style=""background-color:" + f.BackgroundColor + @"""";
				}
			}
		}
	}
}
