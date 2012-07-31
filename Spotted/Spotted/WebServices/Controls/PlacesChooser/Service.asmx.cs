using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using Bobs;
using Model.Entities.Properties;
using SpottedScript.Controls.PlacesChooser;

namespace Spotted.WebServices.Controls.PlacesChooser
{
	/// <summary>
	/// Summary description for Service
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	[System.Web.Script.Services.ScriptService]
	public class Service : System.Web.Services.WebService
	{

		[WebMethod, ScriptMethod]
		public PlaceStub[] GetPlaces(double north, double south, double east, double west, int maximumNumber)
		{
			var query = new Bobs.Query
				(
				new Bobs.And
					(
					new Q(Place.Columns.LatitudeDegreesNorth, QueryOperator.LessThanOrEqualTo, north),
					new Q(Place.Columns.LatitudeDegreesNorth, QueryOperator.GreaterThanOrEqualTo, south),
					new Q(Place.Columns.LongitudeDegreesWest, QueryOperator.GreaterThanOrEqualTo, east * -1), //our longitudes are negative googles
					new Q(Place.Columns.LongitudeDegreesWest, QueryOperator.LessThanOrEqualTo, west * -1),
					new Q(Place.Columns.Enabled, true)
					)
				);
			query.TopRecords = maximumNumber;
			query.OrderBy = new OrderBy(Place.Columns.Population, OrderBy.OrderDirection.Descending);
			var placeSet = new PlaceSet(query);
			return placeSet.Select(p => GetPlaceStub(p)).ToArray();
		}
		[WebMethod, ScriptMethod]
		public PlaceStub[] GetSurroundingPlaces(int centredOnPlaceK, int numberOfPlacesToGet)
		{
			numberOfPlacesToGet = numberOfPlacesToGet < 100 ? numberOfPlacesToGet : 100;


			Place p = new Place(centredOnPlaceK);
			Query q = new Query();
			q.QueryCondition = new Q(Place.Columns.Enabled, true);
			q.OrderBy = p.NearestPlacesOrderBy;
			q.TopRecords = numberOfPlacesToGet;
			PlaceSet ps = new PlaceSet(q);
			return ps.Select(place => GetPlaceStub(place)).ToArray();
		}

		PlaceStub GetPlaceStub(Place place)
		{
			var spatialData = (IHasSpatialData)place;
			return new PlaceStub() { k = place.K, lat = spatialData.Lat, lng = spatialData.Lon, name = place.FriendlyName };
		}
	}
}
