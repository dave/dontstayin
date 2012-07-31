using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using AjaxControlToolkit;
using System.Collections.Generic;
using Bobs;
using System.Collections.Specialized;

namespace Spotted.WebServices
{
	/// <summary>
	/// Summary description for CascadingDropDowns
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
	[System.Web.Script.Services.ScriptService]
	public class CascadingDropDowns : System.Web.Services.WebService
	{

		[WebMethod]
		public CascadingDropDownNameValue[] GetCountries(string knownCategoryValues, string category)
		{
			List<CascadingDropDownNameValue> items = new List<CascadingDropDownNameValue>();
			items.Add(new CascadingDropDownNameValue("Select a country...", "0", true));
			CountrySet csTop = Country.GetTop(10);
			items.Add(new CascadingDropDownNameValue("", "0"));
			items.Add(new CascadingDropDownNameValue("--- POPULAR COUNTRIES ---", "0"));
			foreach (Country c in csTop)
				items.Add(new CascadingDropDownNameValue(c.FriendlyName, c.K.ToString()));

			CountrySet csAll = Country.GetAll();
			items.Add(new CascadingDropDownNameValue("", "0"));
			items.Add(new CascadingDropDownNameValue("--- ALL COUNTRIES ---", "0"));
			foreach (Country c in csAll)
				items.Add(new CascadingDropDownNameValue(c.FriendlyName, c.K.ToString()));

			return items.ToArray();
		}

		[WebMethod]
		public CascadingDropDownNameValue[] GetPlaces(string knownCategoryValues, string category)
		{
			StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
			int countryK;
			if (!kv.ContainsKey("Country") || !Int32.TryParse(kv["Country"], out countryK))
			{
				return null;
			}

			List<CascadingDropDownNameValue> items = new List<CascadingDropDownNameValue>();

			PlaceSet psTop = Place.GetTop(countryK, 10);
			if (psTop.Count == 0)
			{
				items.Add(new CascadingDropDownNameValue("No towns in our database for this country.", "0"));
			}
			else
			{
				items.Add(new CascadingDropDownNameValue("Select a town...", "0", true));
				items.Add(new CascadingDropDownNameValue("", "0"));
				items.Add(new CascadingDropDownNameValue("--- POPULAR TOWNS ---", "0"));
				foreach (Place p in psTop)
					items.Add(new CascadingDropDownNameValue(p.Name, p.K.ToString()));

				PlaceSet psAll = Place.GetAll(countryK);
				items.Add(new CascadingDropDownNameValue("", "0"));
				items.Add(new CascadingDropDownNameValue("--- ALL TOWNS ---", "0"));
				foreach (Place p in psAll)
					items.Add(new CascadingDropDownNameValue(p.Name, p.K.ToString()));
			}

			return items.ToArray();
		}

		[WebMethod]
		public CascadingDropDownNameValue[] GetVenues(string knownCategoryValues, string category)
		{
			StringDictionary kv = CascadingDropDown.ParseKnownCategoryValuesString(knownCategoryValues);
			int placeK;
			if (!kv.ContainsKey("Place") || !Int32.TryParse(kv["Place"], out placeK))
			{
				return null;
			}

			List<CascadingDropDownNameValue> items = new List<CascadingDropDownNameValue>();

			VenueSet vsTop = Venue.GetTop(placeK, 10);
			if (vsTop.Count == 0)
			{
				items.Add(new CascadingDropDownNameValue("No venues in our database for this town.", "0"));
			}
			else
			{
				items.Add(new CascadingDropDownNameValue("Select a venue...", "0", true));
				items.Add(new CascadingDropDownNameValue("", "0"));
				items.Add(new CascadingDropDownNameValue("--- POPULAR VENUES ---", "0"));
				foreach (Venue v in vsTop)
					items.Add(new CascadingDropDownNameValue(v.Name, v.K.ToString()));

				VenueSet vsAll = Venue.GetAll(placeK);
				items.Add(new CascadingDropDownNameValue("", "0"));
				items.Add(new CascadingDropDownNameValue("--- ALL VENUES ---", "0"));
				foreach (Venue v in vsAll)
					items.Add(new CascadingDropDownNameValue(v.Name, v.K.ToString()));
			}

			return items.ToArray();
		}
	}
}
