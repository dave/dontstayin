using System.Linq;
using System.Collections.Generic;
using Bobs;

namespace Spotted.Controls
{
	[ClientScript]
	public partial class PlacesChooser : EnhancedUserControl
	{
		public PlacesChooser()
		{
		}

		public IEnumerable<Place> SelectedPlaces
		{
			set
			{
				if (value.Any())
				{
					uiMap.North = value.Max(p => p.LatitudeDegreesNorth);
					uiMap.South = value.Min(p => p.LatitudeDegreesNorth);
					uiMap.East = (-1 * value.Min(p => p.LongitudeDegreesWest));
					uiMap.West = (-1 * value.Max(p => p.LongitudeDegreesWest));

					foreach (var item in value)
					{
						this.uiPlacesMultiSelector.Add(item.FriendlyName, item.K.ToString());
					}
				}
			}
		}
		public IEnumerable<int> SelectedPlaceKs
		{
			get
			{
				return this.uiPlacesMultiSelector.Selections.Select(kvp => int.Parse(kvp.Value));
				
			}
		}
		
	}
}
