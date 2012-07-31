using System;
#if !SCRIPT
using System.Collections.Generic;
using System.Text;
using Dictionary = System.Collections.Generic.Dictionary<string, object>;
#endif
namespace SpottedScript.Controls.MultiBuddyChooser
{
	public class GetMusicTypesAndPlacesResult
	{
		public Pair[] places;
		public Pair[] musicTypes;
	}
	public class Pair
	{
		public string key;
		public string value;
	}
}
