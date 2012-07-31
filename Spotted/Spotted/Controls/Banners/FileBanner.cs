using System;
using System.Collections.Generic;
using System.Text;

namespace Spotted.Controls.Banners
{
	public class FileBanner : System.Web.UI.UserControl
	{
		public FileBanner()
		{
			FlashVersion = String.Empty;
		}

		public string LinkUrl { get; set; }
		public bool LinkTargetBlank { get; set; }
		public string BannerUrl { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
		public string FlashVersion { get; set; }

		public int ClickHelperTopOffset { get; set; }
		public int ClickHelperLeftOffset { get; set; }
		public bool ShowClickHelper { get; set; }

	}
}
