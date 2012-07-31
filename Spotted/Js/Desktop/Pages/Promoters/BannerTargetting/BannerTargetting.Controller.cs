using System;
using jQueryApi;

namespace Js.Pages.Promoters.BannerTargetting
{
	public class Controller
	{
		private readonly View view;

		public Controller(View view)
		{
			this.view = view;
			view.uiUseCustomBannerRotationRadioJ.Click(OnBannerRotationClick);
			view.uiUseDefaultBannerRotationRadioJ.Click(OnBannerRotationClick);
		}

		private void OnBannerRotationClick(jQueryEvent e)
		{
			this.view.uiCustomRotationValue.Style.Visibility= view.uiUseDefaultBannerRotationRadio.Checked ? "hidden" : "";
		}
	}
}
