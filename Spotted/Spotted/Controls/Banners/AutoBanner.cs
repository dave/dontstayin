using System;
using System.Collections.Generic;
using System.Text;

namespace Spotted.Controls.Banners
{
	public abstract class AutoBanner : System.Web.UI.UserControl
	{
		public AutoBanner() { }

		public bool LinkTargetBlank { get; set; }
		public string BannerUrl { get; set; }
		public Guid PicGuid { get; set; }
		public string FirstLine { get; set; }
		public string SecondLine { get; set; }
		public string ThirdLine { get; set; }
		public abstract int FontSize { get; set; }

		public virtual void Bind()
		{
		}

	}
}
