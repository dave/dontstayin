using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bobs;

namespace GeoCoder
{
	class Program
	{
		static void Main(string[] args)
		{
			VenueSet vs = new VenueSet(new Query(new Q(Venue.Columns.Postcode, QueryOperator.NotEqualTo, "")));
			foreach (Venue v in vs)
			{
				try
				{
					KeyValuePair<double, double> latLng = GoogleGeoCoder.GeoCodeAddress(v.Postcode + ", " + v.Place.Country.Code2Letter);
					if (latLng.Key != 0 || latLng.Value != 0)
					{
						Console.WriteLine("{0},{1},{2},UPDATE VENUE WITH (ROWLOCK) SET Lat = {0}, Lon = {1} WHERE K = {2}", latLng.Key, latLng.Value, v.K);
					}
				}
				catch { }
			}

			Console.ReadLine();
		}
	}
}
