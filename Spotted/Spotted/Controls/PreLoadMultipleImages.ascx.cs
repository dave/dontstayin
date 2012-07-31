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

namespace Spotted.Controls
{
	public partial class PreLoadMultipleImages : System.Web.UI.UserControl
	{
		public string[] ImageSrcs
		{
			set
			{
				// image references:
				PreLoadImage[] uiPreLoadImages = new PreLoadImage[]
					{
						uiPreLoadImage0,
						uiPreLoadImage1,
						uiPreLoadImage2,
						uiPreLoadImage3,
						uiPreLoadImage4,
						uiPreLoadImage5,
						uiPreLoadImage6,
						uiPreLoadImage7
					};

				int i= 0;
				// for all the image sources we are given, set pre-load image control sources
				while (value != null && i < uiPreLoadImages.Length && i < value.Length)
				{
					uiPreLoadImages[i].ImageSrc = value[i];
					i++;
				}
				// set no visibility on any remaining pre-load image controls
				while (i < uiPreLoadImages.Length)
				{
					uiPreLoadImages[i].Visible = false;
					i++;
				}
				// any extra ImageSrcs will just be ignored
			}
		}
	}
}
