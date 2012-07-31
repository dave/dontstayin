using System;
using System.Runtime.CompilerServices;

namespace Js.GoogleMaps
{
	[Imported, IgnoreNamespace]
	public class GIcon
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="copy">Optional. will copy GIcon as a template</param>
		/// <param name="image">Optional. path to icon</param>
		public GIcon(GIcon copy, string image) { }
		/// <summary>
		/// The foreground image URL of the icon.
		/// </summary>
		public string image;
		/// <summary>
		/// The shadow image URL of the icon.
		/// </summary>
		public string shadow;
		/// <summary>
		/// The pixel size of the foreground image of the icon.
		/// </summary>
		public GSize iconSize;
		/// <summary>
		/// The pixel size of the shadow image.
		/// </summary>
		public GSize shadowSize;
		/// <summary>
		/// The pixel coordinate relative to the top left corner of the icon image at which this icon is anchored to the map.
		/// </summary>
		public GPoint iconAnchor;
		/// <summary>
		/// The pixel coordinate relative to the top left corner of the icon image at which the info window is anchored to this icon.
		/// </summary>
		public GPoint infoWindowAnchor;
		/// <summary>
		/// The URL of the foreground icon image used for printed maps. It must be the same size as the main icon image given by image.
		/// </summary>
		public string printImage;
		/// <summary>
		/// The URL of the foreground icon image used for printed maps in Firefox/Mozilla. It must be the same size as the main icon image given by image.
		/// </summary>
		public string mozPrintImage;
		/// <summary>
		/// The URL of the shadow image used for printed maps. It should be a GIF image since most browsers cannot print PNG images.
		/// </summary>
		string printShadow;
		/// <summary>
		/// The URL of a virtually transparent version of the foreground icon image used to capture click events in Internet Explorer. This image should be a 24-bit PNG version of the main icon image with 1% opacity, but the same shape and size as the main icon.
		/// </summary>
		public string transparent;
		/// <summary>
		/// An array of integers representing the x/y coordinates of the image map we should use to specify the clickable part of the icon image in browsers other than Internet Explorer.
		/// </summary>
		public int[] imageMap;
		/// <summary>
		/// Specifies the distance in pixels in which a marker will visually "rise" vertically when dragged. (Since 2.79)
		/// </summary>
		public int maxHeight;
		/// <summary>
		/// Specifies the cross image URL when an icon is dragged. (Since 2.79)
		/// </summary>
		public string dragCrossImage;
		/// <summary>
		/// Specifies the pixel size of the cross image when an icon is dragged. (Since 2.79)
		/// </summary>
		public GSize dragCrossSize;
		/// <summary>
		/// Specifies the pixel coordinate offsets (relative to the iconAnchor) of the cross image when an icon is dragged. (Since 2.79)
		/// </summary>
		public GPoint dragCrossAnchor;

	}
}
