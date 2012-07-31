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

namespace Spotted.Templates.Photos
{
	public partial class IconRollover : System.Web.UI.UserControl
	{
		protected Label StatsLabel;
		protected PlaceHolder UsrPh;

		private void Page_Load(object sender, System.EventArgs e)
		{


		}

		protected string Attribs
		{
			get
			{
				string rolloverHtml = "";

				if (CurrentPhoto.UsrCount > 0)
					rolloverHtml += CurrentPhoto.UsrString;

				if (rolloverHtml.Length > 0)
					rolloverHtml = "This is: " + rolloverHtml;

				string totalsText = "";
				if (CurrentPhoto.Views > 0 || CurrentPhoto.TotalComments > 0)
				{
					if (CurrentPhoto.Views > 0)
						totalsText += CurrentPhoto.Views.ToString() + " view" + (CurrentPhoto.Views == 1 ? "" : "s");
					if (CurrentPhoto.Views > 0 && CurrentPhoto.TotalComments > 0)
						totalsText += ", ";
					if (CurrentPhoto.TotalComments > 0)
						totalsText += CurrentPhoto.TotalComments.ToString() + " comment" + (CurrentPhoto.TotalComments == 1 ? "" : "s");
				}
				if (totalsText.Length > 0)
					rolloverHtml = rolloverHtml + (rolloverHtml.Length > 0 ? "<br>" : "") + totalsText;
				if (rolloverHtml.Length > 0)
					rolloverHtml = HttpUtility.UrlEncodeUnicode("<b>" + rolloverHtml + "</b>").Replace("'", "\\'");

				string html = "";
				if (rolloverHtml.Length > 0)
				{
					html += " onmouseover=\"stt('" + rolloverHtml + "');\" onmouseout=\"htm();\"";
				}
				return html;
			}
		}

		protected Photo CurrentPhoto
		{
			get
			{
				if (currentPhoto == null)
					currentPhoto = ((Photo)((DataListItem)NamingContainer).DataItem);
				return currentPhoto;
			}
		}
		Photo currentPhoto;
	}
}
