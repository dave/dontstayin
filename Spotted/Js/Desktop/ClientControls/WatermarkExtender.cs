using System;
using System.Collections.Generic;
using System.Html;
using Js.Library;
using jQueryApi;

namespace Js.ClientControls
{
	public class WatermarkExtender
	{
		InputElement el;
		string watermark;
		public WatermarkExtender(InputElement el, string watermark)
		{
			this.el = el;
			this.watermark = watermark;
			jQuery.FromElement(el).Focus(OnFocus);
			jQuery.FromElement(el).Blur(OnBlur);
			((Dictionary<object, object>)(object)el)["readOnly"] = null;
			
			
			
		}
		int timeoutId;
		public void OnBlur(jQueryEvent ev)
		{
			timeoutId = Window.SetTimeout(AddWatermark, 300);
		}
		public void OnFocus(jQueryEvent ev)
		{
			Window.ClearTimeout(timeoutId);
			if (el.Value == watermark)
			{
				el.ClassName = "";
				el.Value = "";
			}
		}
		public void AddWatermark()
		{
			if (el.Value == "")
			{
				el.ClassName = "Watermark";
				el.Value = watermark;
			}

		}

	}
}
