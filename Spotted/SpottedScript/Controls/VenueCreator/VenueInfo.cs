


namespace SpottedScript.Controls.VenueCreator
{
#if SCRIPT
	public delegate void VenueInfoCallback(VenueInfo eventInfo);
#endif
	public class VenueInfo
	{
#if !SCRIPT
		public VenueInfo(Bobs.Venue venue)
		{
			this.name = venue.Name;
			this.k = venue.K;
			this.place = new PlaceInfo(venue.Place);
			this.url = venue.Url();
			this.picPath = venue.AnyPicPath;
		}
		public static readonly Bobs.ColumnSet Columns = new Bobs.ColumnSet(PlaceInfo.Columns, Bobs.Venue.Columns.Name, Bobs.Venue.Columns.K, Bobs.Venue.Columns.PlaceK, Bobs.Venue.Columns.UrlFragment, Bobs.Venue.Columns.PlaceK, Bobs.Venue.Columns.UrlName);

#else
		
#endif
		public static string NameWithPlace(VenueInfo vi)
		{
			return vi.name + ", " + PlaceInfo.NameWithCountry(vi.place);
		}
		public string name;
		public int k;
		public PlaceInfo place;
		public string url;
		public string picPath;
	}
}
