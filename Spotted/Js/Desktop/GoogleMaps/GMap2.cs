using System;
using System.Html;
using System.Runtime.CompilerServices;

namespace Js.GoogleMaps
{
    /// <summary>
    /// Stub for http://code.google.com/apis/maps/documentation/reference.html#GMap2
    /// </summary>
	[Imported, IgnoreNamespace]
	public class GMap2
	{
		//public static class GEvents
		//{
		//    public const string click = "click";
		//    public const string moveend = "moveend";
		//}

		//[Script(UseCompilerConstants = true, OptimizedCode = "return new GMap2({arg0})")]
		public GMap2(Element el)
		{
			throw new Exception("");
		}

		public void setCenter(GLatLng latLng, int zoom)
		{
			throw new Exception("");
		}

		public void addControl(GControl gControl)
        {
			throw new Exception("");
        }

		public void checkResize()
        {
        }

		public void addOverlay(GMarker marker)
		{
		}
		public void removeOverlay(GMarker marker)
		{
		}
		public void clearOverlays()
		{
		}

		public void enableScrollWheelZoom()
		{
		}

		public GLatLngBounds getBounds()
		{
			return null;
		}

		public DivElement getContainer()
		{
			return null;
		}

		public GDraggableObject getDragObject()
		{
			return null;
		}
		public GPoint fromLatLngToContainerPixel(GLatLng latLng)
		{
			return null;
		}
		public int getBoundsZoomLevel(GLatLngBounds bounds)
		{
			return -1;
		}

		public void enableContinuousZoom()
		{
		}

		public void enableDoubleClickZoom()
		{
		}

		public void enableGoogleBar()
		{
		}

    	public GLatLng getCenter()
    	{
			return null;
    	}

		public void panTo(GLatLng gLatLng)
		{
			
		}

		public int getZoom()
		{
			return 0;
		}
	}
}
