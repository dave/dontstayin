using System;
using System.DHTML;
using SpottedScript.GoogleMaps;

namespace SpottedScript.GoogleMaps
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
		internal GMap2(DOMElement el)
		{
			Exception.Create();
		}

		internal void setCenter(GLatLng latLng, int zoom)
		{
			Exception.Create();
		}

        internal void addControl(GControl gControl)
        {
			Exception.Create();
        }

        internal void checkResize()
        {
        }

		internal void addOverlay(GMarker marker)
		{
		}
		internal void removeOverlay(GMarker marker)
		{
		}
		internal void clearOverlays()
		{
		}

		internal void enableScrollWheelZoom()
		{
		}

		internal GLatLngBounds getBounds()
		{
			return null;
		}

		internal DivElement getContainer()
		{
			return null;
		}

		internal GDraggableObject getDragObject()
		{
			return null;
		}
		internal GPoint fromLatLngToContainerPixel(GLatLng latLng)
		{
			return null;
		}
		internal int getBoundsZoomLevel(GLatLngBounds bounds)
		{
			return -1;
		}

		internal void enableContinuousZoom()
		{
		}

		internal void enableDoubleClickZoom()
		{
		}

		internal void enableGoogleBar()
		{
		}

    	public GLatLng getCenter()
    	{
			return null;
    	}

		internal void panTo(GLatLng gLatLng)
		{
			
		}

		internal int getZoom()
		{
			return 0;
		}
	}
}
