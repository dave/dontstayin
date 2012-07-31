using System;
using Js.Library;
using jQueryApi;
using System.Html;
using Js.Controls.Picker;
using Js.jQueryGetAPI;

namespace Js.Pages.FindYourPhoto
{
	public class Controller
	{
		public View view;
		public Controller(View v)
		{
			this.view = v;

			view.Picker.SelectedSpotterChanged = new StringEvent(spotterChanged);
			view.Picker.SelectedEventChanged = new ObjectEvent(eventChanged);

			if (Misc.BrowserIsIE)
				jQuery.OnDocumentReady(initialise);
			else
				initialise();
		}
		void initialise()
		{
			
		}
		void spotterChanged(object o, StringArgs e)
		{
			int usrK = 0;
			try
			{
				usrK = int.Parse(e.Val.Replace("-", ""));
			}
			catch{}

			if (usrK > 0)
			{
				changeNow(usrK, 0);
			}
			else
			{
				view.Result.InnerHTML = "Can't find this spotter. The spotter code should be 8 digits, like 1234-5678.";
				view.ResultOuter.Style.Display = "";
				view.LoadingOverlay.Style.Display = "none";
			}
		}
		void eventChanged(object o, ObjectArgs e)
		{
			changeNow(0, e.Object == null ? 0 : e.Object.K);
		}
		void changeNow(int usrK, int eventK)
		{
			if (usrK == 0 && eventK == 0)
			{
				view.ResultOuter.Style.Display = "none";
				return;
			}

			string url = "/pages/findyourphotocontent.aspx?" + (usrK > 0 ? ("usrk=" + usrK.ToString()) : ("eventk=" + eventK.ToString()));

			requestId++;

			int currentRequestId = requestId;
			int currentLoadId = loadId;

			jQueryGet.Get(
				url,
				null,
				new ActionGet(gotContent),
				null,
				currentRequestId.ToString());

			Window.SetTimeout(
				delegate
				{
					if (loadId == currentLoadId)
					{
						view.LoadingOverlay.Style.Height = view.Result.OffsetHeight.ToString() + "px";
						view.LoadingOverlay.Style.Display = "";
					}
				},
				100);

		}
		int requestId = 0;
		int loadId = 0;

		void gotContent(string data, string textStatus, jQueryXmlHttpRequest req, string args)
		{
			int requestIdFromArgs = int.Parse((string)args);
			if (requestId == requestIdFromArgs)
			{
				loadId++;
				view.Result.InnerHTML = data;
				view.ResultOuter.Style.Display = "";
				view.LoadingOverlay.Style.Display = "none";
			}
		}
	}
}
