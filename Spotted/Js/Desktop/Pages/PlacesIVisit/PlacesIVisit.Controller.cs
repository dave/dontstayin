using System;
using System.Html;
using Js.Library;
using Js.GoogleMaps;

namespace Js.Pages.PlacesIVisit
{
	delegate void MapClickDelegate(GMarker marker, GLatLng latLng);
	public class Controller
	{
		private readonly View view;
		
		public Controller(View view)
		{
			this.view = view;
		
			this.view.uiPlacesChooser.view.uiPlacesMultiSelector.ItemAdded = SetSaveButtonDisabledState;
			this.view.uiPlacesChooser.view.uiPlacesMultiSelector.ItemRemoved = SetSaveButtonDisabledState;
		}

		void SetSaveButtonDisabledState(string a, string b)
		{
			if (this.view.uiPlacesChooser.view.uiPlacesMultiSelector.GetSelections().Count > 0)
			{
				this.view.uiSaveButton.Disabled = false;
			}
			else
			{
				this.view.uiSaveButton.Disabled = true;
			}
		}
		
	}
}
