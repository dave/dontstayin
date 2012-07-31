using System;
using System.DHTML;
using SpottedScript.GoogleMaps;
using Sys.UI;

namespace SpottedScript.Controls.MapControl
{
	class Marker
	{
		private readonly GMarker gMarker;
		public DOMElement Hover;
		public Marker(GMarker gMarker)
		{
			this.gMarker = gMarker;
			Hover = Document.CreateElement("UL");
			GEvent.addListener(gMarker, "mouseout", delegate()
			{
				try
				{
					Script.Literal("htm();");
				}
				catch (Exception e)
				{

				}
			});
			GEvent.addListener(gMarker, "click", delegate()
			{
				//this.view.uiPlacesMultiSelector.AddItem(name, k.ToString());
			});
			GEvent.addListener(gMarker, "mouseover", delegate()
			{
				Script.Literal("stt({0});", Hover.InnerHTML);
			});

			GEvent.addListener(gMarker, "mouseout", delegate()
			{
				try
				{
					Script.Literal("htm();");
				}
				catch (Exception e)
				{

				}
			});
		}




		public void Dispose()
		{
			GEvent.clearListeners(gMarker, "click");
			GEvent.clearListeners(gMarker, "onmouseover");
			GEvent.clearListeners(gMarker, "onmouseout");
			
		}
	}
}
