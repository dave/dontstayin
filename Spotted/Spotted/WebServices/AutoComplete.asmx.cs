using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using Bobs;
using System.Web.UI;
using System.Web.Script.Services;
using System.Reflection;
using Js.ClientControls;
using System.Data.SqlClient;
using Cambro.Misc;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
using Caching;
using Spotted.Admin;
using OrderPair = System.Collections.Generic.KeyValuePair<string, Bobs.OrderBy.OrderDirection>;
using Spotted.WebServices.CometAutoComplete;
using Js.AutoCompleteLibrary;



namespace Spotted.WebServices
{
	/// <summary>
	/// Summary description for AutoComplete
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	[ScriptService]
	public class AutoComplete : WebService
	{
		const int defaultPriority = 0;
		const int clientSideCacheDurationInSeconds = 120;

		IEnumerable<Suggestion> GetSuggestionsAndSetClientSideCaching(int maxNumberOfItemsToGet, params Func<IEnumerable<Suggestion>>[] getters)
		{
			return this.GetSuggestionsAndSetClientSideCaching(maxNumberOfItemsToGet, (IEnumerable<Func<IEnumerable<Suggestion>>>) getters);
		}
		IEnumerable<Suggestion> GetSuggestionsAndSetClientSideCaching(int maxNumberOfItemsToGet, IEnumerable<Func<IEnumerable<Suggestion>>> getters)
		{
			if (!Vars.DevEnv)
			{
				TimeSpan cacheDuration = TimeSpan.FromSeconds(clientSideCacheDurationInSeconds);

				Context.Response.Cache.SetCacheability(HttpCacheability.Public);
				Context.Response.Cache.SetExpires(DateTime.Now.Add(cacheDuration));
				Context.Response.Cache.AppendCacheExtension("must-revalidate, proxy-revalidate");
				FieldInfo maxAge = HttpContext.Current.Response.Cache.GetType().GetField("_maxAge", BindingFlags.Instance | BindingFlags.NonPublic);
				maxAge.SetValue(HttpContext.Current.Response.Cache, cacheDuration);
			}
			return this.GetSuggestions(getters, maxNumberOfItemsToGet);
		}

		private IEnumerable<Suggestion> GetSuggestions(IEnumerable<Func<IEnumerable<Suggestion>>> getters, int maxNumberOfItemsToGet)
		{
			return getters.Select(g => g()).Flatten().Distinct(new Suggestion.SuggestionsComparer()).Take(maxNumberOfItemsToGet);
		}
		
		 
	
		#region GetTags
		[WebMethod, ScriptMethod/*(UseHttpGet = true)*/]
		public Suggestion[] GetTags(string text, int maxNumberOfItemsToGet, Dictionary<string, object> parameters)
		{
			return SuggestionGetters.GetTags(text, maxNumberOfItemsToGet, parameters).ToArray();
		}
		#endregion
		#region GetTagSearchString - old style
		[WebMethod, ScriptMethod]
		public string[] GetTagSearchString(string prefixText, int count)
		{
			return SuggestionGetters.GetTagSearchString(prefixText, count).ToArray();
		}
		#endregion

		#region GetGroupMembers
		[WebMethod, ScriptMethod/*(UseHttpGet = true)*/]
		public Suggestion[] GetGroupMembers(int maxNumberOfItemsToGet, string text, Dictionary<string, object> parameters)
		{
			int? groupK = parameters.GroupK();
			return this.GetSuggestionsAndSetClientSideCaching(maxNumberOfItemsToGet, 
				() => SuggestionGetters.GetGroupMembersWithOption(maxNumberOfItemsToGet, text, QueryOperator.TextStartsWith, groupK), 
				() => SuggestionGetters.GetGroupMembersWithOption(maxNumberOfItemsToGet, text, QueryOperator.TextContains, groupK)
			).ToArray();
		}



		Suggestion[] GetGroupMembersWithOption(int maxNumberOfItemsToGet, string text, Dictionary<string, object> parameters, QueryOperator option)
		{
			return SuggestionGetters.GetGroupMembersWithOption(maxNumberOfItemsToGet, text, option, parameters.GroupK() ).ToArray();
		}
		#endregion
		#region GetUsrsPublic
		[WebMethod, ScriptMethod/*(UseHttpGet = true)*/]
		public Suggestion[] GetUsrsPublic(string text, int maxNumberOfItemsToGet, Dictionary<string, object> parameters)
		{
			return this.GetSuggestionsAndSetClientSideCaching(maxNumberOfItemsToGet, SuggestionGetters.GetUsrsPublic(Usr.Current, text, maxNumberOfItemsToGet, parameters.ShowK(), parameters.ReturnType(), parameters.SearchType())).ToArray();
			
		}

		 
	 
		#endregion
		#region GetBuddiesThenUsers
		[ScriptMethod/*(UseHttpGet = true)*/, WebMethod]
		public Suggestion[] GetBuddiesThenUsrs(string text, int maxNumberOfItemsToGet, Dictionary<string, object> parameters)
		{
			return this.GetSuggestionsAndSetClientSideCaching(maxNumberOfItemsToGet, SuggestionGetters.GetBuddiesThenUsrs(Usr.Current, text, maxNumberOfItemsToGet, parameters.ReturnType(), parameters.ShowK(), parameters.SearchType())).ToArray();	
		}
		#endregion
		#region GetBuddies
		Regex emailRegex = new Regex(Cambro.Misc.RegEx.Email);
		[ScriptMethod/*(UseHttpGet = true)*/, WebMethod]
		public Suggestion[] GetBuddies(string text, int maxNumberOfItemsToGet, Dictionary<string, object> parameters)
		{
			if (Usr.Current == null) return new Suggestion[] {};
			return this.GetSuggestionsAndSetClientSideCaching(maxNumberOfItemsToGet, SuggestionGetters.GetBuddies(Usr.Current, text, maxNumberOfItemsToGet, parameters.ReturnType(), parameters.ShowK(), parameters.SearchType())).ToArray();
		}

 
  

		#endregion
		#region GetBrands
		[ScriptMethod/*(UseHttpGet = true)*/, WebMethod]
		public Suggestion[] GetBrands(string text, int maxNumberOfItemsToGet, Dictionary<string, object> parameters)
		{
			Func<Brand, Suggestion> getSuggestion = b => SuggestionGetters.ConvertBrandToSuggestion(
			                                             	b, parameters.ShowK(),
			                                             	parameters.ShowPromoters(),
			                                             	parameters.ReturnType()
			                                             	);
			var suggestionGetters =
				from getter in SuggestionGetters.GetBrands(text, maxNumberOfItemsToGet, parameters.SearchType())
			    select new Func<IEnumerable<Suggestion>> (() => getter().Select(getSuggestion));

			return GetSuggestionsAndSetClientSideCaching(maxNumberOfItemsToGet, suggestionGetters).ToArray();
		}

		#endregion
		#region GetPromotersWithK
		[ScriptMethod/*(UseHttpGet = true)*/, WebMethod]
		public Suggestion[] GetPromotersWithK(string text, int maxNumberOfItemsToGet, Dictionary<string, object> parameters)
		{
			if (!Usr.Current.IsAdmin)
				throw new Exception("Only admin");
			return this.GetSuggestionsAndSetClientSideCaching(maxNumberOfItemsToGet, 
				() => SuggestionGetters.GetPromotersByK(maxNumberOfItemsToGet, text, QueryOperator.EqualTo, true),
				() => SuggestionGetters.GetPromotersByK(maxNumberOfItemsToGet, text, QueryOperator.TextStartsWith, true),
				() => SuggestionGetters.GetPromotersByName(maxNumberOfItemsToGet, text, QueryOperator.TextStartsWith, true),
				() => SuggestionGetters.GetPromotersByName(maxNumberOfItemsToGet, text, QueryOperator.TextContains, true)
			).ToArray();

		}

		 
		#endregion
		#region GetUsersWithK
		[ScriptMethod/*(UseHttpGet = true)*/, WebMethod]
		public Suggestion[] GetUsersWithK(string text, int maxNumberOfItemsToGet, Dictionary<string, object> parameters)
		{
			return this.GetSuggestionsAndSetClientSideCaching(maxNumberOfItemsToGet, SuggestionGetters.GetUsersWithK(Usr.Current, text, maxNumberOfItemsToGet, parameters.ShowK(), parameters.ReturnType(), parameters.SearchType())).ToArray();
		}
		#endregion


		#region GetPlacesEnabled
		[ScriptMethod/*(UseHttpGet = true)*/, WebMethod]
		public Suggestion[] GetPlacesEnabled(int maxNumberOfItemsToGet, string text, Dictionary<string, object> parameters)
		{

			return SuggestionGetters.GetPlacesEnabled(maxNumberOfItemsToGet, text, parameters).ToArray();

		}

		#endregion
		#region GetVenuesFull
		[WebMethod, ScriptMethod/*(UseHttpGet = true)*/]
		public Suggestion[] GetVenuesFull(int maxNumberOfItemsToGet, string text, Dictionary<string, object> parameters)
		{
			return SuggestionGetters.GetVenuesFull(maxNumberOfItemsToGet, text, parameters).ToArray();
		}
		#endregion
		#region GetVenues
 
		[WebMethod, ScriptMethod/*(UseHttpGet = true)*/]
		public Suggestion[] GetVenues(string text, int maxNumberOfItemsToGet, Dictionary<string, object> parameters)
		{
			return this.GetSuggestionsAndSetClientSideCaching(maxNumberOfItemsToGet, SuggestionGetters.GetVenues(maxNumberOfItemsToGet, text, parameters.ReturnType(), parameters.PlaceK(), parameters.SearchType())).ToArray();

		}
		#endregion
		#region GetEvents
		[WebMethod, ScriptMethod/*(UseHttpGet = true)*/]
		public Suggestion[] GetEvents(int maxNumberOfItemsToGet, string text, Dictionary<string, object> parameters)
		{
			return this.GetSuggestionsAndSetClientSideCaching(maxNumberOfItemsToGet, SuggestionGetters.GetEvents(Usr.Current, maxNumberOfItemsToGet, text, parameters.Attend(), parameters.GroupK(), parameters.Date(), parameters.VenueK(), parameters.Future(), parameters.ReturnType(), parameters.SearchType())).ToArray();
			
		}

		#endregion
		#region GetTowns
		[WebMethod, ScriptMethod/*(UseHttpGet = true)*/]
		public Suggestion[] GetPlaces(int maxNumberOfItemsToGet, string text, Dictionary<string, object> parameters)
		{
			return this.GetSuggestionsAndSetClientSideCaching(maxNumberOfItemsToGet, SuggestionGetters.GetPlaces(maxNumberOfItemsToGet, text, parameters.ReturnType(), parameters.SearchType())).ToArray();

		}
		#endregion
		#region SiteSearch
		[WebMethod, ScriptMethod(UseHttpGet=true)]
		public Suggestion[] GetSiteSearchResults(string text, int maxNumberOfItemsToGet, Dictionary<string, object> parameters)
		{
			if (text.Length < 2) return new Suggestion[] {};
			parameters.Add("returnUrl", true);

			var suggestionGetters = SiteSearchSuggestionGettersB(text, parameters);
			var textStartsWith = suggestionGetters.Select(sg =>
			{
				parameters["searchType"] = SuggestionGetters.SearchType.TextStartsWith;
				return sg;
			});
			var textContains = suggestionGetters.Select(sg =>
			{
				parameters["searchType"] = SuggestionGetters.SearchType.TextContains;
				return sg;
			});
			var combined = textStartsWith.CombineWith(textContains).Flatten();
			
			return this.GetSuggestionsAndSetClientSideCaching(maxNumberOfItemsToGet, combined).ToArray();
		}
		 


		private IEnumerable<Func<IEnumerable<Suggestion>>> SiteSearchSuggestionGettersB(string text, Dictionary<string, object> parameters)
		{
			yield return () => this.GetBuddies(text, 3, parameters);
			yield return () =>
			{
				parameters["groupSearchType"] = SuggestionGetters.GroupSearchType.Member;
				return this.GetGroups(text, 3, parameters);
			};
			yield return () => this.GetBrands(text, 3, parameters);
			yield return () => this.GetVenues(text, 3, parameters);
			yield return () => this.GetPlaces(3, text, parameters);
			yield return () => this.GetCountries(3, text, parameters);
			yield return () =>
			{
				parameters["groupSearchType"] = SuggestionGetters.GroupSearchType.Public;
				parameters["excludeBrands"] = true;
				return this.GetGroups(text, 3, parameters);
			};
			yield return () => this.GetUsrsPublic(text, 3, parameters);
			
		}

		#endregion
		#region Groups
		[WebMethod, ScriptMethod/*(UseHttpGet = true)*/]
		public Suggestion[] GetGroups(string text, int maxNumberOfItemsToGet, Dictionary<string, object> parameters)
		{
			return GetSuggestions(SuggestionGetters.GetGroups(maxNumberOfItemsToGet, text, parameters.ReturnType(), parameters.SearchType(), parameters.GroupSearchType(), parameters.ExcludeBrands()), maxNumberOfItemsToGet).ToArray();
		}
		#endregion
		#region Groups
		[WebMethod, ScriptMethod/*(UseHttpGet = true)*/]
		public Suggestion[] GetGroupsNoBrands(int maxNumberOfItemsToGet, string text, Dictionary<string, object> parameters)
		{
			return SuggestionGetters.GetGroupsNoBrands(maxNumberOfItemsToGet, text, parameters).ToArray();
		}
		#endregion

		
		#region GetSuggestionsFromDbComboSql
		 
		#endregion
		#region DbComboArticleObject
		[WebMethod, ScriptMethod/*(UseHttpGet = true)*/]
		public Suggestion[] GetObjects(string text, int maxNumberOfItemsToGet, Dictionary<string, object> parameters)
		{
			switch (parameters.Type() ?? -1)
			{

				case 1:
					return GetEvents(maxNumberOfItemsToGet, text, parameters);
				case 2:
					return this.GetVenues(text, maxNumberOfItemsToGet, parameters);
				case 3:
					return GetPlaces(maxNumberOfItemsToGet, text, parameters);
				case 4:
					return GetCountries(maxNumberOfItemsToGet, text, parameters);
				case 5:
					return new Suggestion[]
					{
						new Suggestion()
						{
							html = "No need to select anything in here, you've selected 'General' above.",
							text = "",
							value = ""
						}
					};
				default:
					return new Suggestion[]
					{
						new Suggestion()
						{
							html = "Please select an article subject matter type by clicking an option above",
							text = "",
							value = ""
						}
					};
			}


		}
		[WebMethod, ScriptMethod(UseHttpGet=true)]
		public Suggestion[] GetCountries(int get, string text, Dictionary<string, object> parameters)
		{
			return this.GetSuggestionsAndSetClientSideCaching(get, SuggestionGetters.GetCountries(text, get, parameters.ReturnType(), parameters.SearchType())).ToArray();
		}

		#endregion
	
	}
	static class DictionaryExtensions
	{
		internal static T Get<T>(this Dictionary<string, object> parameters, string parameterName, Func<string, T> converter, T defaultValue) where T : struct
		{
			return parameters.Get(parameterName, converter) ?? defaultValue;
		}
		internal static T? Get<T>(this Dictionary<string, object> parameters, string parameterName, Func<string, T> converter) where T : struct
		{
			if (parameters.ContainsKey(parameterName))
			{
				return converter((string)parameters[parameterName]);
			}
			else
			{
				return null;
			}
		}
		static int? nullInt(this Dictionary<string, object> parameters, string text)
		{
			return parameters.Get(text, s => int.Parse(s));
		}
		static DateTime? nullDate(this Dictionary<string, object> parameters, string text)
		{
			return parameters.Get(text, s => DateTime.Parse(s));
		}
		internal static int? GroupK(this Dictionary<string, object> parameters)
		{
			return parameters.nullInt("groupK");
		}
		internal static DateTime? Date(this Dictionary<string, object> parameters)
		{
			return parameters.nullDate("date");
		}
		internal static int? VenueK(this Dictionary<string, object> parameters)
		{
			return parameters.nullInt("venueK");
		}
		internal static int? PlaceK(this Dictionary<string, object> parameters)
		{
			return parameters.nullInt("placeK");
		}
		internal static int? Type(this Dictionary<string, object> parameters)
		{
			return parameters.nullInt("type") ?? parameters.nullInt("Type");;
		}
		
		
		

		internal static bool Get(this Dictionary<string, object> parameters, string text)
		{
			return parameters.ContainsKey(text);
		}
		internal static bool ShowK(this Dictionary<string, object> parameters)
		{
			return parameters.Get("showK");
		}
		internal static bool Future(this Dictionary<string, object> parameters)
		{
			return parameters.Get("future");
		}
		internal static bool ShowPromoters(this Dictionary<string, object> parameters)
		{
			return parameters.Get("ShowPromoters");
		}
		internal static bool Attend(this Dictionary<string, object> parameters)
		{
			return parameters.Get("attend");
		}
		internal static bool ExcludeBrands(this Dictionary<string, object> parameters)
		{
			return parameters.Get("excludeBrands");
		}
		internal static SuggestionGetters.SearchType SearchType(this Dictionary<string, object> parameters)
		{
			if (!parameters.ContainsKey("searchType")) return SuggestionGetters.SearchType.All;
			return (SuggestionGetters.SearchType)parameters["searchType"];
		}
		internal static SuggestionGetters.GroupSearchType GroupSearchType(this Dictionary<string, object> parameters)
		{
			if (!parameters.ContainsKey("groupSearchType")) return SuggestionGetters.GroupSearchType.Public;
			return (SuggestionGetters.GroupSearchType)parameters["groupSearchType"];
		}

		internal static SuggestionGetters.ReturnType ReturnType(this Dictionary<string, object> parameters)
		{
			if (parameters.ContainsKey("returnInfo")) return SuggestionGetters.ReturnType.Info;
			if (parameters.ContainsKey("returnUrl")) return SuggestionGetters.ReturnType.Url;
			return SuggestionGetters.ReturnType.K;


		}

	}

	

}
