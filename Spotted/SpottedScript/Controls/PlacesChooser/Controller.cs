using System;
using SpottedScript.GoogleMaps;
using Spotted.WebServices.Controls.PlacesChooser;
using Sys.UI;
using Utils;

namespace SpottedScript.Controls.PlacesChooser
{
	public class Controller
	{
		public View view;
		public Controller(View view)
		{
			this.view = view;
			DomEvent.AddHandler(view.uiAddRadiusButton, "click", OnAddNearbyClick);
			AddMapMarkers();
			GEvent.addListener(this.view.uiMap.Gmap2, "moveend", AddMapMarkers);
			GEvent.addListener(this.view.uiMap.Gmap2, "zoomend", AddMapMarkers);
		}

		private void OnAddNearbyClick(DomEvent e)
		{
			try
			{
				Service.GetSurroundingPlaces
					(
					int.ParseInvariant(this.view.uiRadiusPlaceAutoComplete.Value),
					int.ParseInvariant(this.view.uiNumberOfSurroundingTownsDropDown.Value),
					delegate(PlaceStub[] result, object context, string name)
					{
						for (int i = 0; i < result.Length; i++)
						{
							this.view.uiPlacesMultiSelector.AddItem(result[i].name, result[i].k.ToString());
						}
						this.view.uiRadiusPlaceAutoComplete.Clear();
					},
					Trace.WebServiceFailure,
					null,
					5000
					);
				
			}catch(Exception)
			{
				
			}
			e.PreventDefault();
		}

		private void AddMapMarkers()
		{
			GLatLngBounds bounds = this.view.uiMap.Gmap2.getBounds();
			Service.GetPlaces(
				bounds.getNorthEast().lat(),
				bounds.getSouthWest().lat(),
				bounds.getNorthEast().lng(),
				bounds.getSouthWest().lng(),
				20,
				AddMapMarkersCallback, Trace.WebServiceFailure, null, 3000
				);

		}
		GMarker[] markers = null;

		private void AddMapMarkersCallback(PlaceStub[] result, object userContext, string methodName)
		{
			if (markers != null)
			{
				for (int i = 0; i < markers.Length; i++)
				{
					GEvent.clearListeners(markers[i], "click");
					GEvent.clearListeners(markers[i], "onmouseover");
					GEvent.clearListeners(markers[i], "onmouseout");
				}
			}
			this.view.uiMap.Gmap2.clearOverlays();
			markers = new GMarker[] { };
			for (int i = 0; i < result.Length; i++)
			{
				PlaceStub placeStub = result[i];
				CreateMarker(placeStub.lat, placeStub.lng, placeStub.name, placeStub.k);
			}
		}
		void CreateMarker(double lat, double lng, string name, int k)
		{
			GLatLng latLng = new GLatLng(lat, lng, true);
			GMarker marker = new GMarker(latLng);
			Script.Literal("{0}.k = {1};", marker, k);
			Script.Literal("{0}.name = {1};", marker, name);


			GEvent.addListener(marker, "click", delegate()
			{
				this.view.uiPlacesMultiSelector.AddItem(name, k.ToString());
			});
			GEvent.addListener(marker, "mouseover", delegate()
			{
				Script.Literal("stt({0});", name);
			});

			GEvent.addListener(marker, "mouseout", delegate()
			{
				try
				{
					Script.Literal("htm();");
				}
				catch (Exception e)
				{

				}
			});
			this.view.uiMap.Gmap2.addOverlay(marker);
			markers[markers.Length] = marker;
		}
	}
}
