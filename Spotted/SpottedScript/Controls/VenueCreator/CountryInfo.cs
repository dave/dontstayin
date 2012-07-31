
namespace SpottedScript.Controls.VenueCreator
{
	public class CountryInfo
	{
		public string name;
		public int k;
#if SCRIPT
#else
		public static readonly Bobs.ColumnSet Columns = new Bobs.ColumnSet(Bobs.Country.Columns.Name, Bobs.Country.Columns.K, Bobs.Country.Columns.FriendlyName, Bobs.Country.Columns.Code2Letter, Bobs.Country.Columns.UrlName);
		public CountryInfo(Bobs.Country country)
		{
			this.name = country.Name;
			this.k = country.K;
		}
#endif
	}
}
