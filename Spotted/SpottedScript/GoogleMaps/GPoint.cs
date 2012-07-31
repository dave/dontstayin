using System;

namespace SpottedScript.GoogleMaps
{
	[Imported, IgnoreNamespace]
	public class GPoint
	{
		public GPoint(int x, int y)
		{
		}

		internal int x;
		internal int y;
		bool equals(GSize other) 
		{
			return false;
		}
	}
}
