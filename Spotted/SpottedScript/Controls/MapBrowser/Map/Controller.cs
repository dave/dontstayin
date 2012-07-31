using System;
using ScriptSharpLibrary;
using SpottedScript.Controls.PagedData;
using SpottedScript.Controls.PagedData.Display;
using SpottedScript.GoogleMaps;
using Sys;
using Utils;

namespace SpottedScript.Controls.MapBrowser.Map
{
	public class Controller : IParameterSource
	{
		
		public View view;
		public Controller(View view)
		{
			this.view = view;
			GEvent.addListener(this.view.uiMapControl.Gmap2, "moveend", delegate
			{
				Trace.Write(this.view.uiMapControl.Gmap2.getBounds().getNorthEast().lat() + "," + 
					this.view.uiMapControl.Gmap2.getBounds().getNorthEast().lng() + "," + 
				this.view.uiMapControl.Gmap2.getBounds().getSouthWest().lat() + "," + 
				this.view.uiMapControl.Gmap2.getBounds().getSouthWest().lng() + ": ZOOM:" +
				this.view.uiMapControl.Gmap2.getBoundsZoomLevel(this.view.uiMapControl.Gmap2.getBounds()));
				if (parametersUpdated != null) parametersUpdated(this, null);
			});

		}

		public Dictionary Parameters
		{
			get
			{
				Dictionary parameters = new Dictionary();
				GLatLngBounds bounds = this.view.uiMapControl.Gmap2.getBounds();
				parameters["north"] = bounds.getNorthEast().lat();
				parameters["south"] = bounds.getSouthWest().lat();
				parameters["east"] = bounds.getNorthEast().lng();
				parameters["west"] = bounds.getSouthWest().lng();
				return parameters;
			}
		}




		private EventHandler parametersUpdated;
		public EventHandler ParametersUpdated
		{
			get { return parametersUpdated; }
			set { parametersUpdated = value; }
		}
	}
}
