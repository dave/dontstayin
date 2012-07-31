using Sys.UI;

namespace SpottedScript.Pages.Promoters.BannerTargetting
{
	public class Controller
	{
		private readonly View view;

		public Controller(View view)
		{
			this.view = view;
			DomEvent.AddHandler(view.uiUseCustomBannerRotationRadio, "click", OnBannerRotationClick);
			DomEvent.AddHandler(view.uiUseDefaultBannerRotationRadio, "click", OnBannerRotationClick);
		}

		private void OnBannerRotationClick(DomEvent e)
		{
			this.view.uiCustomRotationValue.Style.Visibility= view.uiUseDefaultBannerRotationRadio.Checked ? "hidden" : "";
		}
	}
}
