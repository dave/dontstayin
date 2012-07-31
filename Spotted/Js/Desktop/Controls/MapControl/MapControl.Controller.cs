using System;
using System.Collections.Generic;
using System.Html;
using Js.GoogleMaps;

namespace Js.Controls.MapControl
{
	public class Controller
	{
		private View view;
		public GMap2 Gmap2;
		public Controller(View view)
		{
			this.view = view;
			Gmap2 = new GMap2(view.map);

			Gmap2.addControl(new GLargeMapControl());
			double north = Number.ParseFloat(view.uiNorth.Value);
			double south = Number.ParseFloat(view.uiSouth.Value);
			double west = Number.ParseFloat(view.uiWest.Value);
			double east = Number.ParseFloat(view.uiEast.Value);
			double deltaLat = north - south;
			double delatLng = east - west;
			GLatLng ne = new GLatLng(north + deltaLat / 20, east + delatLng / 20, true);
			GLatLng sw = new GLatLng(south - deltaLat / 20, west - delatLng / 20, true);
			GLatLngBounds bounds = new GLatLngBounds(sw, ne);
			bounds.extend(sw);
			bounds.extend(ne);
			int zoom = Gmap2.getBoundsZoomLevel(bounds);
			if (zoom > 12) zoom = 12;
			GLatLng centre = new GLatLng((north + south) / 2, (east + west) / 2, true);
			Gmap2.enableScrollWheelZoom();
			Gmap2.setCenter(centre, zoom);
			Gmap2.enableContinuousZoom();
			Gmap2.enableDoubleClickZoom();
			Gmap2.enableGoogleBar();
			Gmap2.checkResize();
			
		}


		private Dictionary<object, object> markers = new Dictionary<object, object>();
		public void AddMapMarkers(MapItem[] items)
		{
			if (items != null)
			{
				for (int i = 0; i < items.Length; i++)
				{
					Marker marker = GetMarker(items[i].lat, items[i].lon);
					Element div = Document.CreateElement("LI");
					div.InnerHTML = items[i].hoverText;
					marker.Hover.AppendChild(div);
				}
			}
		}

		public void ClearMarkers()
		{
			foreach (object key in markers.Keys)
			{
				Marker marker = (Marker)markers[key];
				marker.Dispose();
			}
			this.Gmap2.clearOverlays();
			markers = new Dictionary<object, object>();
		}

		Marker GetMarker(double lat, double lng)
		{
			string key = lat.ToString() + "," + lng.ToString();
			if (markers[key] == null)
			{
				GMarker gMarker = new GMarker(new GLatLng(lat, lng, true));
				this.Gmap2.addOverlay(gMarker);
				Marker marker = new Marker(gMarker);
				markers[key] = marker;
				
				return marker;
			}
			else
			{
				return (Marker) markers[key];
			}
		}
 

	}
}
