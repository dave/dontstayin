using System;
using System.DHTML;
using Sys.UI;
 
namespace ScriptSharpLibrary
{
	public class WatermarkExtender
	{
		InputElement el;
		string watermark;
		public WatermarkExtender(InputElement el, string watermark)
		{
			this.el = el;
			this.watermark = watermark;
			DomEvent.AddHandler(el, "focus", OnFocus);
			DomEvent.AddHandler(el, "blur", OnBlur);
			((Dictionary)(object)el)["readOnly"] = null;
			
			
			
		}
		int timeoutId;
		public void OnBlur(DomEvent ev)
		{
			timeoutId = Window.SetTimeout(AddWatermark, 300);
		}
		public void OnFocus(DomEvent ev)
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
