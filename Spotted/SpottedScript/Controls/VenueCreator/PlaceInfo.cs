

namespace SpottedScript.Controls.VenueCreator
{
	public class PlaceInfo
	{
		public string name;
		public int k;
		public CountryInfo country;
		
#if !SCRIPT
		public static readonly Bobs.ColumnSet Columns = new Bobs.ColumnSet(CountryInfo.Columns, Bobs.Place.Columns.UniqueName, Bobs.Place.Columns.K, Bobs.Place.Columns.CountryK, Bobs.Place.Columns.Pic, Bobs.Place.Columns.RegionAbbreviation, Bobs.Place.Columns.UrlFragment, Bobs.Place.Columns.UrlName);
		public PlaceInfo(Bobs.Place place)
		{
			this.name = place.UniqueName;
			this.k = place.K;
			this.country = new CountryInfo(place.Country);
		}
#else
		
#endif
		public static string NameWithCountry(PlaceInfo pi)
		{
			return pi.name + ", " + pi.country.name;
		}
	}
}
