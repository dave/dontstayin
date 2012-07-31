using System;
using ScriptSharpLibrary;

namespace SpottedScript.GoogleMaps
{
	[Imported, IgnoreNamespace]
	public class GMarker
	{
		//public static class GEvents
		//{
		//    public const string click = "click";
		//    public const string dblclick = "dblclick";
		//    public const string mousedown = "mousedown";
		//    public const string mouseup = "mouseup";
		//    public const string mouseover = "mouseover";
		//    public const string mouseout = "mouseout";
		//    public const string infowindowopen = "infowindowopen";
		//    public const string infowindowbeforeclose = "infowindowbeforeclose";
		//    public const string infowindowclose = "infowindowclose";
		//    public const string remove = "remove";
		//    public const string dragstart = "dragstart";
		//    public const string drag = "drag";
		//    public const string dragend = "dragend";
		//    public const string visibilitychanged = "visibilitychanged";
		//}
		public GMarker(GLatLng gLatLng)
		{
			
		}
		public GMarker(GLatLng gLatLng, GMarkerOptions options)
		{
		}

		/// <summary>
		/// 	  	Chooses the Icon for this class. If not specified, G_DEFAULT_ICON is used. (Since 2.50)
		/// </summary>
		public GIcon icon;
		/// <summary>
		/// When dragging markers normally, the marker floats up and away from the cursor. Setting this value to true keeps the marker underneath the cursor, and moves the cross downwards instead. The default value for this option is false. (Since 2.63)
		/// </summary>
		public bool dragCrossMove;
		/// <summary>
		/// This string will appear as tooltip on the marker, i.e. it will work just as the title attribute on HTML elements. (Since 2.50)
		/// </summary>
		public string title;
		/// <summary>
		/// Toggles whether or not the marker is clickable. Markers that are not clickable or draggable are inert, consume less resources and do not respond to any events. The default value for this option is true, i.e. if the option is not specified, the marker will be clickable. (Since 2.50)
		/// </summary>
		public bool clickable;
		/// <summary>
		/// Toggles whether or not the marker will be draggable by users. Markers set up to be dragged require more resources to set up than markers that are clickable. Any marker that is draggable is also clickable, bouncy and auto-pan enabled by default. The default value for this option is false. (Since 2.61)
		/// </summary>
		public bool draggable;
		/// <summary>
		/// Toggles whether or not the marker should bounce up and down after it finishes dragging. The default value for this option is false. (Since 2.61)
		/// </summary>
		public bool bouncy;
		/// <summary>
		/// When finishing dragging, this number is used to define the acceleration rate of the marker during the bounce down to earth. The default value for this option is 1. (Since 2.61)
		/// </summary>
		double bounceGravity;
		/// <summary>
		/// Auto-pan the map as you drag the marker near the edge. If the marker is draggable the default value for this option is true. (Since 2.87)
		/// </summary>
		public bool autoPan;
		/// <summary>
		/// This function is used for changing the z-Index order of the markers when they are overlaid on the map and is also called when their infowindow is opened. The default order is that the more southerly markers are placed higher than more northerly markers. This function is passed in the GMarker object and returns a number indicating the new z-index. (Since 2.98)
		/// </summary>
		public Action zIndexProcess;

		/// <summary>
		/// Returns the geographical coordinates at which this marker is anchored, as set by the constructor or by setLatLng(). (Since 2.88)
		/// </summary>
		/// <returns></returns>
		internal GLatLng getLatLng()
		{
			return null;
		}

		
	}
}
