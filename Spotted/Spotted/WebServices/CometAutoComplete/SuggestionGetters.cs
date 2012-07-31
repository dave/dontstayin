using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using Bobs;
using Caching;
using OrderPair = System.Collections.Generic.KeyValuePair<object, Bobs.OrderBy.OrderDirection>;
using Cambro.Misc;
using System.Data.SqlClient;
using System.Text;
using Js.ClientControls;
using Js.AutoCompleteLibrary;

namespace Spotted.WebServices.CometAutoComplete
{
	public static class SuggestionGetters
	{
		public enum ReturnType
		{
			K,
			Url,
			Info
		}

		internal class SuggestionsComparer : IEqualityComparer<Suggestion>
		{
			public bool Equals(Suggestion x, Suggestion y)
			{
				return (x.text.Equals(y.text, StringComparison.InvariantCultureIgnoreCase) || x.value.Equals(y.value, StringComparison.InvariantCultureIgnoreCase));
			}
			public int GetHashCode(Suggestion obj)
			{
				return obj.GetHashCode();
			}
		}
		#region GetTags
		internal static IEnumerable<Suggestion> GetTags(string text, int maxNumberOfItemsToGet, Dictionary<string, object> parameters)
		{
			Query query = new Query(
				new And
					(
					new Q(Bobs.Tag.Columns.TagText, QueryOperator.TextStartsWith, text),
					new Q(Bobs.Tag.Columns.Blocked, false)
				)
			);
			query.CacheDuration = new TimeSpan(0, 1, 0);
			query.TopRecords = maxNumberOfItemsToGet;
			TagSet set = new TagSet(query);
			return set.ToList().ConvertAll(tag => new Suggestion() { html = tag.TagText, text = tag.TagText, value = tag.K.ToString() });

		}
		#endregion
		#region GetTagSearchString - old style
		internal static string[] GetTagSearchString(string prefixText, int count)
		{
			Query query = new Query(
				new And
					(
					new Q(Bobs.Tag.Columns.TagText, QueryOperator.TextStartsWith, prefixText),
					new Q(Bobs.Tag.Columns.Blocked, false)
				)
			);
			query.TopRecords = count;
			TagSet set = new TagSet(query);
			return set.ToList().ConvertAll(tag => tag.TagText.Contains(" ") ? "\"" + tag.TagText + "\"" : tag.TagText).ToArray();

		}
		#endregion

		#region GetGroupMembers
		internal static IEnumerable<Func<IEnumerable<Suggestion>>> GetGroupMembers(int maxNumberOfItemsToGet, string text, int? groupK, SearchType searchType)
		{

			if (searchType == SearchType.All || searchType == SearchType.TextStartsWith) yield return () => GetGroupMembersWithOption(maxNumberOfItemsToGet, text, QueryOperator.TextStartsWith, groupK);
			if (searchType == SearchType.All || searchType == SearchType.TextContains) yield return () => GetGroupMembersWithOption(maxNumberOfItemsToGet, text, QueryOperator.TextContains, groupK);
		}

		internal static IEnumerable<Suggestion> GetGroupMembersWithOption(int maxNumberOfItemsToGet, string text, QueryOperator option, int? groupK)
		{
			string extraWhereClause = "";
			string extraFromClause = "";
			if (groupK.HasValue)
			{
				extraWhereClause = " AND GroupUsr.GroupK=" + groupK.Value.ToString() + " AND GroupUsr.Status = 1 ";
				extraFromClause = " INNER JOIN GroupUsr ON Usr.K = GroupUsr.UsrK ";
			}
			return GetSuggestionsFromDbComboSql("select top " + Db.PNum(maxNumberOfItemsToGet) + " NickName as DbComboText, K as DbComboValue from Usr " + extraFromClause + " where NickName like '" + ((option == QueryOperator.TextContains) ? "%" : "") + Db.PStr(text) + "%' AND NOT NickName='' " + extraWhereClause + " order by NickName");
		}
		#endregion
		internal enum SearchType
		{
			All = 0,
			TextStartsWith = QueryOperator.TextStartsWith,
			TextContains = QueryOperator.TextContains
		}
		internal enum GroupSearchType
		{
			Member = 1,
			Public = 2
		}
		#region GetUsrsPublic
		internal static IEnumerable<Func<IEnumerable<Suggestion>>> GetUsrsPublic(Usr currentUsr, string text, int maxNumberOfItemsToGet, bool showK, ReturnType returnType, SearchType searchType)
		{
			bool fullSearch = Usr.Current != null && Usr.Current.IsAdmin;
			Func<Usr, Suggestion> convertToSuggestion = u => ConvertUsrToSuggestion(u, fullSearch, showK, returnType);

			if (searchType == SearchType.All || searchType == SearchType.TextStartsWith) yield return () => GetVerifiedUsersWithOption(currentUsr, fullSearch, text, maxNumberOfItemsToGet, QueryOperator.TextStartsWith, true).Select(convertToSuggestion);
			if (searchType == SearchType.All || searchType == SearchType.TextStartsWith) yield return () => GetVerifiedUsersWithOption(currentUsr, fullSearch, text, maxNumberOfItemsToGet, QueryOperator.TextStartsWith, false).Select(convertToSuggestion);
			if (searchType == SearchType.All || searchType == SearchType.TextContains) yield return () => GetVerifiedUsersWithOption(currentUsr, fullSearch, text, maxNumberOfItemsToGet, QueryOperator.TextContains, true).Select(convertToSuggestion);
			if (searchType == SearchType.All || searchType == SearchType.TextContains) yield return () => GetVerifiedUsersWithOption(currentUsr, fullSearch, text, maxNumberOfItemsToGet, QueryOperator.TextContains, false).Select(convertToSuggestion);
		}

		internal static Func<string, bool> MatchText(QueryOperator option, string text)
		{
			switch (option)
			{
				case QueryOperator.TextStartsWith: return (s) => s.StartsWith(text, StringComparison.InvariantCultureIgnoreCase);
				case QueryOperator.TextContains: return (s) => s.IndexOf(text, StringComparison.InvariantCultureIgnoreCase) > -1;
				default: throw new NotImplementedException();
			}
		}
		internal static CacheKey GetVerifiedUsersWithOptionCacheKey(string text, int maxNumberOfItemsToGet, QueryOperator option)
		{
			return new CacheKey(CacheKeyPrefix.GetVerifiedUsersWithOption, text, maxNumberOfItemsToGet.ToString(), option.ToString());
		}
		static IEnumerable<T> CachedGet<T>(Func<string, CacheKey> getCacheKey, Func<string, IEnumerable<T>> getItems, string text, Func<T, string, bool> testForMatch, TimeSpan cacheDuration)
		{

			if (text.Length > 0)
			{
				var parentCacheKey = getCacheKey(text.Substring(0, text.Length - 1));
				var parentResult = Instances.Main.Get<List<T>>(parentCacheKey);
				if (parentResult != null && parentResult.Count < 15)
				{
					return parentResult.Where(t => testForMatch(t, text));
				}
			}

			var items = new List<T>(getItems(text));
			Caching.Instances.Main.Store(getCacheKey(text), items, cacheDuration);
			return items;
		}

		internal static IEnumerable<Usr> GetVerifiedUsersWithOption(Usr currentUsr, bool fullSearch, string text, int maxNumberOfItemsToGet, QueryOperator option, bool hasPicture)
		{
			var q = new Query();
			q.QueryCondition = new Q(Usr.Columns.NickName, option, text);
			if (fullSearch && false) //disabled as too slow. would need a computed column between firstname/lastname
			{
				string parameterName = "@TextSearch" + Guid.NewGuid().ToString().Replace('-', '_');
				q.QueryCondition = new Or(
					q.QueryCondition,
					new StringQueryCondition(Usr.Columns.FirstName + "  + ' ' + " + Usr.Columns.LastName + " COLLATE database_default LIKE " + parameterName)
				);
				q.ExtraParameters.Add(new SqlParameter(parameterName, "%" + text + (option == QueryOperator.TextStartsWith ? "" : "%")));

			}
			q.QueryCondition = new And
			(
				q.QueryCondition,
				new Q(Usr.Columns.NickName, QueryOperator.NotEqualTo, ""),
				new Q(Usr.Columns.IsEmailVerified, true)
			);
			q.Columns = new ColumnSet(Usr.Columns.K, Usr.Columns.NickName, Usr.Columns.LastName, Usr.Columns.FirstName, Usr.Columns.Email, Usr.Columns.Pic, Usr.Columns.FacebookUID);
			if (hasPicture)
			{
				q.QueryCondition = new And
				(
					q.QueryCondition,
					new Or
					(
						new And(new Q(Usr.Columns.Pic, QueryOperator.IsNotNull, null), new Q(Usr.Columns.Pic, QueryOperator.NotEqualTo, Guid.Empty)),
						new And(new Q(Usr.Columns.FacebookUID, QueryOperator.IsNotNull, null), new Q(Usr.Columns.FacebookUID, QueryOperator.NotEqualTo, 0))
					)
				);
			}
			else
			{
				q.QueryCondition = new And
				(
					q.QueryCondition,
					new Or(new Q(Usr.Columns.Pic, QueryOperator.IsNull, null), new Q(Usr.Columns.Pic, Guid.Empty)),
					new Or(new Q(Usr.Columns.FacebookUID, QueryOperator.IsNull, null), new Q(Usr.Columns.FacebookUID, QueryOperator.EqualTo, 0))
				);
			}
			q.OrderBy = new OrderBy(Usr.Columns.NickName);
			q.TopRecords = maxNumberOfItemsToGet;
			var usrSet = new UsrSet(q);

			return usrSet;

		}

		#endregion

		#region GetBuddies
		static System.Text.RegularExpressions.Regex emailRegex = new System.Text.RegularExpressions.Regex(Cambro.Misc.RegEx.Email);



		internal static IEnumerable<Usr> GetBuddiesWithOptions(Usr currentUsr, int maxNumberOfItemsToGet, QueryOperator queryOperator, string text)
		{
			Query q = new Query();
			q.Columns = new ColumnSet(Buddy.Columns.FullBuddy, Buddy.Columns.SkeletonName, Usr.Columns.K, Usr.Columns.NickName, Usr.Columns.FirstName, Usr.Columns.LastName, Usr.Columns.Email, Usr.Columns.Pic, Usr.Columns.FacebookUID);
			q.TopRecords = maxNumberOfItemsToGet;
			q.NoLock = true;
			q.TableElement = new Join(Buddy.Columns.BuddyUsrK, Usr.Columns.K);
			q.QueryCondition =
				new And(
					new Q(Buddy.Columns.UsrK, currentUsr.K),
					new Q(Usr.Columns.NickName, queryOperator, text)
					);

			q.CacheDuration = TimeSpan.FromMinutes(1);
			var bs = new UsrSet(q);

			return bs;
		}
		internal static bool MatchBuddy(Buddy b, Func<string, bool> matchString)
		{
			if (b.FullBuddy)
			{
				return matchString(b.BuddyUsr.NickName) || matchString((b.BuddyUsr.FirstName + " " + b.BuddyUsr.LastName));
			}
			else
			{
				return matchString(b.BuddyUsr.NickName) || (b.BuddyUsr.IsEmailStubUser && (matchString(b.SkeletonName) || matchString(b.BuddyUsr.Email)));
			}
		}


		#endregion
		#region GetBrands
		internal static IEnumerable<Func<IEnumerable<Brand>>> GetBrands(string text, int maxNumberOfItemsToGet, SearchType searchType)
		{


			if (searchType == SearchType.All || searchType == SearchType.TextStartsWith) yield return () => GetBrandsByK(maxNumberOfItemsToGet, text, QueryOperator.TextStartsWith);
			if (searchType == SearchType.All || searchType == SearchType.TextContains) yield return () => GetBrandsByK(maxNumberOfItemsToGet, text, QueryOperator.TextContains);
			if (searchType == SearchType.All || searchType == SearchType.TextStartsWith) yield return () => GetBrandsByName(maxNumberOfItemsToGet, text, QueryOperator.EqualTo, 0, false);
			if (searchType == SearchType.All || searchType == SearchType.TextStartsWith) yield return () => GetBrandsByName(maxNumberOfItemsToGet, text, QueryOperator.TextStartsWith, 10, true);
			if (searchType == SearchType.All || searchType == SearchType.TextStartsWith) yield return () => GetBrandsByName(maxNumberOfItemsToGet, text, QueryOperator.TextStartsWith, 10, false);
			if (searchType == SearchType.All || searchType == SearchType.TextStartsWith) yield return () => GetBrandsByName(maxNumberOfItemsToGet, text, QueryOperator.TextStartsWith, 0, false);
			if (searchType == SearchType.All || searchType == SearchType.TextContains) yield return () => GetBrandsByName(maxNumberOfItemsToGet, text, QueryOperator.TextContains, 0, false);
		}

		private static IEnumerable<Brand> GetBrandsByName(int maxNumberOfItemsToGet, string text,
			QueryOperator textOperator, int minNumberOfEvents, bool picExists)
		{
			var query = new Query(new Q(Brand.Columns.Name, textOperator, text));
			if (picExists)
			{
				query.QueryCondition = new And(query.QueryCondition,
					new Q(Brand.Columns.Pic, QueryOperator.IsNotNull, null),
					new Q(Brand.Columns.Pic, QueryOperator.NotEqualTo, Guid.Empty.ToString()));
			}
			query.TableElement = new Join(Brand.Columns.K, EventBrand.Columns.BrandK, QueryJoinType.Left);
			query.TopRecords = maxNumberOfItemsToGet;
			query.Columns = new ColumnSet(Brand.Columns.K, Brand.Columns.Name, Brand.Columns.PromoterK, Brand.Columns.Pic, Brand.Columns.UrlName);
			query.GroupBy = new GroupBy(new GroupBy(Brand.Columns.K), new GroupBy(Brand.Columns.Name),
										new GroupBy(Brand.Columns.PromoterK), new GroupBy(Brand.Columns.Pic), new GroupBy(Brand.Columns.UrlName));
			query.ExtraSelectElements["NumberOfEvents"] = "COUNT(DISTINCT [EventK])";
			query.Having = new Q("COUNT(DISTINCT [EventK]) >= " + minNumberOfEvents, null, null);
			query.OrderBy = new OrderBy(Brand.Columns.Name);
			return new BrandSet(query);
		}

		internal static IEnumerable<Brand> GetBrandsByK(int maxNumberOfItemsToGet, string text, QueryOperator option)
		{
			if (!text.IsNumeric()) { return new Brand[] { }; }
			var query = new Query(new Q("CAST({0} as VARCHAR(20)) LIKE '" + (option == QueryOperator.TextContains ? "%" : "") + Db.PStr(text) + ((option == QueryOperator.TextContains || option == QueryOperator.TextStartsWith) ? "%" : "") + "'", Brand.Columns.K, null));
			query.TopRecords = maxNumberOfItemsToGet;
			query.OrderBy = new OrderBy(Brand.Columns.Name, OrderBy.OrderDirection.Ascending);
			return new BrandSet(query);
		}

		internal static Suggestion ConvertBrandToSuggestion(Brand b, bool showK, bool showPromoter, ReturnType returnType)
		{
			StringBuilder displayText = new StringBuilder();
			displayText.Append(b.Name);
			if (b.Promoter == null) { showPromoter = false; }
			if (showK || showPromoter)
			{
				displayText.Append(" (");
				if (showK) { displayText.AppendFormat("k: {0}", b.K.ToString()); }
				if (showK && showPromoter) { displayText.Append(", "); }
				if (showPromoter) { displayText.AppendFormat("promoter: {0}", b.Promoter.Name); }
				displayText.Append(")");
			}
			Func<Brand, string> getReturnString;
			switch (returnType)
			{
				case ReturnType.K:
					{
						getReturnString = user => user.K.ToString();
						break;
					}
				case ReturnType.Url:
					{
						getReturnString = user => user.Url();
						break;
					}
				case ReturnType.Info:
				default:
					throw new NotImplementedException();
			}
			return new Suggestion()
			{
				html = Suggestion.GetPicTitleDetailTemplateHtml(b.PicPath,
						displayText.ToString(),
						b.ExtraSelectElements["NumberOfEvents"] + " events"
					),
				text = b.Name,
				value = getReturnString(b)

			};
		}
		#endregion
		#region GetPromotersWithK
		internal static IEnumerable<Func<IEnumerable<Suggestion>>> GetPromotersWithK(Usr currentUsr, string text, int maxNumberOfItemsToGet, SearchType searchType)
		{
			if (!currentUsr.IsAdmin)
				throw new Exception("Only admin");

			if (searchType == SearchType.All) yield return () => GetPromotersByK(maxNumberOfItemsToGet, text, QueryOperator.EqualTo, true);
			if (searchType == SearchType.All || searchType == SearchType.TextStartsWith) yield return () => GetPromotersByK(maxNumberOfItemsToGet, text, QueryOperator.TextStartsWith, true);
			if (searchType == SearchType.All || searchType == SearchType.TextStartsWith) yield return () => GetPromotersByName(maxNumberOfItemsToGet, text, QueryOperator.TextStartsWith, true);
			if (searchType == SearchType.All || searchType == SearchType.TextContains) yield return () => GetPromotersByName(maxNumberOfItemsToGet, text, QueryOperator.TextContains, true);


		}

		internal static IEnumerable<Suggestion> GetPromotersByK(int maxNumberOfItemsToGet, string text, QueryOperator option, bool showK)
		{
			if (!text.IsNumeric()) { return new Suggestion[] { }; }
			var query = new Query(new Q("CAST({0} as VARCHAR(20)) LIKE '" + (option == QueryOperator.TextContains ? "%" : "") + Db.PStr(text) + ((option == QueryOperator.TextContains || option == QueryOperator.TextStartsWith) ? "%" : "") + "'", Promoter.Columns.K, null));
			query.TopRecords = maxNumberOfItemsToGet;
			query.OrderBy = new OrderBy(Promoter.Columns.Name, OrderBy.OrderDirection.Ascending);

			return new PromoterSet(query).ToList().ConvertAll(p => ConvertPromoterToSuggestion(p, showK)).ToArray();
		}
		internal static IEnumerable<Suggestion> GetPromotersByName(int maxNumberOfItemsToGet, string text, QueryOperator option, bool showK)
		{
			var query = new Query(new Q(Promoter.Columns.Name, QueryOperator.TextStartsWith, text));
			query.TopRecords = maxNumberOfItemsToGet;
			query.OrderBy = new OrderBy(Promoter.Columns.Name, OrderBy.OrderDirection.Ascending);

			return new PromoterSet(query).ToList().ConvertAll(p => ConvertPromoterToSuggestion(p, showK)).ToArray();
		}
		internal static Suggestion ConvertPromoterToSuggestion(Promoter p, bool showK)
		{
			return new Suggestion()
			{
				html = p.Name.Trim() + (showK ? " (K=" + p.K.ToString() + ")" : "") + ((p.Status == Promoter.StatusEnum.Disabled) ? " *DISABLED*" : ""),
				text = p.Name.Trim() + (showK ? " (K=" + p.K.ToString() + ")" : "") + ((p.Status == Promoter.StatusEnum.Disabled) ? " *DISABLED*" : ""),
				value = p.K.ToString()

			};
		}
		#endregion
		#region GetUsersWithK
		internal static IEnumerable<Func<IEnumerable<Suggestion>>> GetUsersWithK(Usr currentUsr, string text, int maxNumberOfItemsToGet, bool showK, ReturnType returnType, SearchType searchType)
		{
			bool showRealName = true;
			if (currentUsr != null && !currentUsr.IsAdmin) throw new Exception("Only admin");
			yield return () => GetUsrsByK(text, maxNumberOfItemsToGet, showRealName);
			foreach (var func in GetUsrsPublic(currentUsr, text, maxNumberOfItemsToGet, showK, returnType, searchType))
			{
				yield return func;
			}

		}

		internal static IEnumerable<Suggestion> GetUsrsByK(string text, int maxNumberOfItemsToGet, bool showRealName)
		{
			if (!text.IsNumeric()) return new Suggestion[] { };
			var query = new Query(new Q(Usr.Columns.K, int.Parse(text)));
			query.TopRecords = maxNumberOfItemsToGet;
			query.OrderBy = new OrderBy(Usr.Columns.K);
			return new UsrSet(query).ToList().ConvertAll(u => ConvertUsrToSuggestion(u, showRealName, true, ReturnType.K)).ToArray();
		}
		#endregion

		internal static Suggestion ConvertUsrToSuggestion(Usr u, bool showRealName, bool showK, ReturnType returnType)
		{
			string nickName = u.NickName;
			string details = showRealName ? u.FirstName + " " + u.LastName : "";
			if (nickName == "")
			{

				nickName = u.ExtraSelectElements[new Column(Buddy.Columns.SkeletonName).ExternalSqlColumnName] as string;
				details = nickName == u.Email ? "" : u.Email;
			}
			Func<Usr, string> getReturnString;
			switch (returnType)
			{
				case ReturnType.K:
					{
						getReturnString = user => user.K.ToString();
						break;
					}
				case ReturnType.Url:
					{
						getReturnString = user => user.Url();
						break;
					}
				case ReturnType.Info:
				default:
					throw new NotImplementedException();
			}

			return new Suggestion()
			{
				html = Suggestion.GetPicTitleDetailTemplateHtml(
					u.AnyPicPath,
					nickName + (showK ? " (k:" + u.K + ")" : ""),
					details
					),
				text = nickName,
				value = getReturnString(u)
			};
		}
		internal static Suggestion ConvertVenueToSuggestion(Venue venue, ReturnType returnType)
		{
			Func<Venue, string> getValue = (x) => x.K.ToString();

			if (returnType == ReturnType.Info)
			{
				JavaScriptSerializer ser = new JavaScriptSerializer();
				getValue = x => ser.Serialize(new VenueInfo(x));
			}
			else if (returnType == ReturnType.Url)
			{
				getValue = x => x.Url();
			}
			return new Suggestion()
			{
				html = Suggestion.GetPicTitleDetailTemplateHtml(venue.AnyPicPath, venue.Name, venue.Place.UniqueName + ", " + venue.Place.Country.Name),
				text = VenueInfo.NameWithPlace(new VenueInfo(venue)),
				value = getValue(venue)
			};
		}
		internal static Suggestion ConvertEventToSuggestion(Event ev, ReturnType returnType)
		{
			Func<Event, string> getValue = (x) => x.K.ToString();

			if (returnType == ReturnType.Info)
			{
				JavaScriptSerializer ser = new JavaScriptSerializer();
				getValue = x => ser.Serialize(new EventInfo(x));
			}
			else if (returnType == ReturnType.Url)
			{
				getValue = x => x.Url();
			}
			return new Suggestion()
			{
				html = Suggestion.GetPicTitleDetailTemplateHtml(ev.AnyPicPath, ev.Name, ev.DateTime.ToString("ddd dd/MM/yyyy") + " " + ev.Venue.Name + ", " + ev.Venue.Place.UniqueName),
				text = EventInfo.EventFullName(new EventInfo(ev)),
				value = getValue(ev)
			};
		}

		internal static Suggestion ConvertPlaceToSuggestion(Place p, ReturnType returnType)
		{
			Func<Place, string> getValue = (x) => x.K.ToString();

			if (returnType == ReturnType.Info)
			{
				JavaScriptSerializer ser = new JavaScriptSerializer();
				getValue = x => ser.Serialize(new PlaceInfo(x));
			}
			else if (returnType == ReturnType.Url)
			{
				getValue = x => x.Url();
			}
			return new Suggestion
			{
				html = Suggestion.GetPicTitleDetailTemplateHtml(p.AnyPicPath, p.UniqueName, p.Country.Name),
				text = PlaceInfo.NameWithCountry(new PlaceInfo(p)),
				value = getValue(p)
			};
		}

		internal static Suggestion ConvertCountryToSuggestion(Country p, ReturnType returnType)
		{
			Func<Country, string> getValue = (Country pl) => pl.K.ToString();

			if (returnType == ReturnType.Info)
			{
				JavaScriptSerializer ser = new JavaScriptSerializer();
				getValue = country => ser.Serialize(new CountryInfo(country));
			}
			else if (returnType == ReturnType.Url)
			{
				getValue = x => x.Url();
			}

			return new Suggestion
			{
				html = Suggestion.GetPicTitleDetailTemplateHtml(p.FlagUrl(), p.Name, "Events: " + p.TotalEvents),
				text = new CountryInfo(p).name,
				value = getValue(p)
			};
		}

		internal static Suggestion ConvertGroupToSuggestion(Group g, ReturnType returnType)
		{
			Func<Group, string> getValue = (x) => x.K.ToString();

			if (returnType == ReturnType.Info)
			{
				throw new NotImplementedException();
			}
			else if (returnType == ReturnType.Url)
			{
				getValue = x => x.Url();
			}

			return new Suggestion
			{
				html = Suggestion.GetPicTitleDetailTemplateHtml(g.AnyPicPath, g.Name, ""),
				text = g.Name,
				value = getValue(g)
			};
		}





		internal static IEnumerable<Func<IEnumerable<Suggestion>>> GetBuddiesThenUsrs(Usr currentUsr, string text, int maxNumberOfItemsToGet, ReturnType returnType, bool showK, SearchType searchType)
		{
			foreach (var item in GetBuddies(currentUsr, text, maxNumberOfItemsToGet, returnType, showK, searchType))
			{
				yield return item;
			}
			foreach (var item in GetUsers(currentUsr, text, maxNumberOfItemsToGet, showK, returnType, searchType))
			{
				yield return item;
			}
		}

		internal static IEnumerable<Func<IEnumerable<Suggestion>>> GetBuddies(Usr currentUsr, string text, int maxNumberOfItemsToGet, ReturnType returnType, bool showK, SearchType searchType)
		{
			if (currentUsr == null) yield break;
			bool currentUsrIsAdmin = currentUsr != null && currentUsr.IsAdmin;
			Func<Usr, Suggestion> convertToSuggestion = buddyUsr => ConvertUsrToSuggestion(buddyUsr, ((buddyUsr.ExtraSelectElements[new Column(Buddy.Columns.FullBuddy).ExternalSqlColumnName] as bool?) ?? false) || currentUsrIsAdmin, showK, returnType);
			if (searchType == SearchType.All || searchType == SearchType.TextStartsWith) yield return () => GetBuddiesWithOptions(currentUsr, maxNumberOfItemsToGet, QueryOperator.TextStartsWith, text).Select(convertToSuggestion);
			if (searchType == SearchType.All || searchType == SearchType.TextContains) yield return () => GetBuddiesWithOptions(currentUsr, maxNumberOfItemsToGet, QueryOperator.TextContains, text).Select(convertToSuggestion);
		}
		internal static IEnumerable<Func<IEnumerable<Suggestion>>> GetUsers(Usr currentUsr, string text, int maxNumberOfItemsToGet, bool showK, ReturnType returnType, SearchType searchType)
		{
			return GetUsrsPublic(currentUsr, text, maxNumberOfItemsToGet, showK, returnType, searchType);
		}





		#region GetPlacesEnabled
		internal static IEnumerable<Suggestion> GetPlacesEnabled(int maxNumberOfItemsToGet, string text, Dictionary<string, object> parameters)
		{

			Query q = new Query
			(
				new And
				(
					new Q(Place.Columns.Enabled, true),
					new Q(Place.Columns.Name, QueryOperator.TextContains, text)
				)
			);
			q.TopRecords = maxNumberOfItemsToGet;
			q.Columns = new ColumnSet(Place.Columns.Name, Place.Columns.K, Place.Columns.CountryK, Country.Columns.K, Country.Columns.FriendlyName);
			if (parameters.ContainsKey("countryK"))
			{
				q.QueryCondition = new And
				(
					q.QueryCondition,
					new Q(Country.Columns.K, int.Parse(parameters["countryK"] as string))
				);
			}

			q.TableElement = new Join
			(
				new TableElement(TablesEnum.Place),
				new TableElement(TablesEnum.Country),
				QueryJoinType.Inner,
				Place.Columns.CountryK,
				Country.Columns.K
			);
			q.OrderBy = new OrderBy(Place.Columns.Name);
			PlaceSet placeSet = new PlaceSet(q);
			return placeSet.ToArray().ConvertAll(
				p => new Suggestion()
				{
					html = p.Name + ", " + p.Country.FriendlyName,
					text = p.Name + ", " + p.Country.FriendlyName,
					value = p.K.ToString()
				}
			);


		}

		#endregion
		#region GetVenuesFull
		internal static IEnumerable<Suggestion> GetVenuesFull(int maxNumberOfItemsToGet, string text, Dictionary<string, object> parameters)
		{
			return GetSuggestionsFromDbComboSql("select top " + Cambro.Misc.Db.PNum(maxNumberOfItemsToGet) + " Venue.UrlFragment + '/' +Venue.UrlName as DbComboText, Venue.K as DbComboValue from Venue WHERE Venue.UrlFragment + '/' +Venue.UrlName like '%" + Cambro.Misc.Db.PStr(text) + "%' order by Venue.UrlFragment, Venue.UrlName");
		}
		#endregion
		#region GetVenues
		internal static IEnumerable<Func<IEnumerable<Suggestion>>> GetVenues(int maxNumberOfItemsToGet, string text, ReturnType returnType, int? placeK, SearchType searchType)
		{
			if (searchType == SearchType.All || searchType == SearchType.TextStartsWith) yield return () => GetVenuesWithOption(maxNumberOfItemsToGet, text, QueryOperator.TextStartsWith, placeK, returnType).Select(v => ConvertVenueToSuggestion(v, returnType));
			if (searchType == SearchType.All || searchType == SearchType.TextContains) yield return () => GetVenuesWithOption(maxNumberOfItemsToGet, text, QueryOperator.TextContains, placeK, returnType).Select(v => ConvertVenueToSuggestion(v, returnType));

		}
		internal static IEnumerable<Venue> GetVenuesWithOption(int maxNumberOfItemsToGet, string text, QueryOperator textSearchOption, int? placeK, ReturnType returnType)
		{

			//return GetSuggestionsFromDbComboSql("select top " + Db.PNum(maxNumberOfItemsToGet) + " Venue.Name+' in '+Place.Name as DbComboText, Venue.K as DbComboValue from Venue INNER JOIN Place ON Venue.PlaceK=Place.K where Venue.Name+' in '+Place.Name like '%" + Db.PStr(text) + "%' order by Venue.Name");
			var textParts = text.Split(',');
			Query q = new Query();
			q.Columns = new ColumnSet(Venue.Columns.Pic);
			q.Columns.Columns.AddRange(VenueInfo.Columns.Columns);

			q.TableElement = Bobs.Venue.CountryJoin;

			q.QueryCondition = new Q(Venue.Columns.Name, textSearchOption, textParts[0].Trim());
			if (placeK.HasValue)
			{
				q.QueryCondition = new And(q.QueryCondition, new Q(Place.Columns.K, placeK));
			}
			else
			{
				if (textParts.Length > 1) q.QueryCondition = new And(q.QueryCondition, new Q(Place.Columns.UniqueName, textSearchOption, textParts[1].Trim()));
				if (textParts.Length > 2) q.QueryCondition = new And(q.QueryCondition, new Q(Country.Columns.Name, textSearchOption, textParts[2].Trim()));
			}
			q.OrderBy = new OrderBy(
				new OrderBy(Venue.Columns.Name, OrderBy.OrderDirection.Ascending),
				new OrderBy(Place.Columns.Name, OrderBy.OrderDirection.Ascending)
				);
			var venueSet = new VenueSet(q);
			var getValue = new Func<Venue, string>(e => e.K.ToString());

			if (returnType == ReturnType.Info)
			{
				JavaScriptSerializer ser = new JavaScriptSerializer();
				getValue = e => ser.Serialize(new VenueInfo(e));

			}


			return venueSet;
		}
		#endregion
		#region GetEvents
		internal static IEnumerable<Func<IEnumerable<Suggestion>>> GetEvents(Usr currentUsr, int maxNumberOfItemsToGet, string text, bool attend, int? groupK, DateTime? date, int? venueK, bool future, ReturnType returnType, SearchType searchType)
		{
			if (searchType == SearchType.All || searchType == SearchType.TextStartsWith) yield return () => GetEventsWithOption(currentUsr, maxNumberOfItemsToGet, text, QueryOperator.TextStartsWith, attend, groupK, date, venueK, future, returnType).Select(e => ConvertEventToSuggestion(e, returnType));
			if (searchType == SearchType.All || searchType == SearchType.TextContains) yield return () => GetEventsWithOption(currentUsr, maxNumberOfItemsToGet, text, QueryOperator.TextContains, attend, groupK, date, venueK, future, returnType).Select(e => ConvertEventToSuggestion(e, returnType));

		}


		internal static IEnumerable<Event> GetEventsWithOption(Usr currentUsr, int maxNumberOfItemsToGet, string text, QueryOperator textSearchOption, bool attend, int? groupK, DateTime? date, int? venueK, bool future, ReturnType returnType)
		{
			var textParts = text.Split(',');
			Query q = new Query();
			q.Columns = new ColumnSet(Event.Columns.Pic, Venue.Columns.Pic);
			q.Columns.Columns.AddRange(EventInfo.Columns.Columns);

			q.TableElement = Event.CountryAllJoin;

			if (attend)
			{
				q.TableElement = new Join(
					q.TableElement,
					new TableElement(TablesEnum.UsrEventAttended),
					QueryJoinType.Inner,
					new And(new Q(Event.Columns.K, UsrEventAttended.Columns.EventK, true), new Q(UsrEventAttended.Columns.UsrK, currentUsr.K)));

			}
			if (groupK.HasValue)
			{
				q.TableElement = new Join(
					q.TableElement,
					new TableElement(TablesEnum.GroupEvent),
					QueryJoinType.Inner,
					new And(new Q(Event.Columns.K, GroupEvent.Columns.EventK, true), new Q(GroupEvent.Columns.GroupK, groupK.Value)));
			}
			q.QueryCondition = new Q(true);
			if (date.HasValue) q.QueryCondition = new And(q.QueryCondition, new Q(Event.Columns.DateTime, date.Value));
			if (venueK.HasValue) q.QueryCondition = new And(q.QueryCondition, new Q(Venue.Columns.K, venueK.Value));
			if (venueK == null || date == null)
			{
				q.QueryCondition = new And(q.QueryCondition, new Q(Event.Columns.Name, textSearchOption, textParts[0].Trim()));
				if (textParts.Length > 1) q.QueryCondition = new And(q.QueryCondition, new Q(Venue.Columns.Name, textSearchOption, textParts[1].Trim()));
				if (textParts.Length > 2) q.QueryCondition = new And(q.QueryCondition, new Q(Place.Columns.Name, textSearchOption, textParts[2].Trim()));
				if (textParts.Length > 3) q.QueryCondition = new And(q.QueryCondition, new Q(Country.Columns.Name, textSearchOption, textParts[3].Trim()));
			}

			if (future) q.QueryCondition = new And(q.QueryCondition, new Q(Event.Columns.DateTime, QueryOperator.GreaterThanOrEqualTo, DateTime.Now.Date));
			q.OrderBy = new OrderBy(
				new OrderBy(Event.Columns.Name, OrderBy.OrderDirection.Ascending),
				new OrderBy(Venue.Columns.Name, OrderBy.OrderDirection.Ascending),
				new OrderBy(Place.Columns.Name, OrderBy.OrderDirection.Ascending),
				new OrderBy(Event.Columns.DateTime, OrderBy.OrderDirection.Descending)
				);
			var eventSet = new EventSet(q);
			var getValue = new Func<Event, string>(e => e.K.ToString());
			if (returnType == ReturnType.Info)
			{
				JavaScriptSerializer ser = new JavaScriptSerializer();
				getValue = e => ser.Serialize(new EventInfo(e));
			}
			return eventSet;
		}
		#endregion
		#region Groups
		internal static IEnumerable<Func<IEnumerable<Suggestion>>> GetGroups(int maxNumberOfItemsToGet, string text, ReturnType returnType, SearchType searchType, GroupSearchType groupSearchType, bool excludeBrands)
		{
			if (Usr.Current == null && groupSearchType == GroupSearchType.Member) yield break;
			if (searchType == SearchType.All || searchType == SearchType.TextStartsWith) yield return () => GetGroupsWithOption(QueryOperator.TextStartsWith, text, maxNumberOfItemsToGet, groupSearchType, excludeBrands).Select(g => ConvertGroupToSuggestion(g, returnType));
			if (searchType == SearchType.All || searchType == SearchType.TextContains) yield return () => GetGroupsWithOption(QueryOperator.TextContains, text, maxNumberOfItemsToGet, groupSearchType, excludeBrands).Select(g => ConvertGroupToSuggestion(g, returnType));


		}


		private static IEnumerable<Group> GetGroupsWithOption(QueryOperator option, string text, int maxNumberOfItemsToGet, GroupSearchType groupSearchType, bool excludeBrands)
		{
			Query q = new Query();
			q.Columns = new ColumnSet(Group.Columns.K, Group.Columns.Name, Group.Columns.UrlName, Group.Columns.Pic, Group.Columns.BrandK);
			q.QueryCondition = new Q(Group.Columns.UrlName, option, text);

			if (groupSearchType == GroupSearchType.Member)
			{
				q.TableElement = Group.UsrMemberJoin;
				q.QueryCondition = new And(q.QueryCondition, new Q(GroupUsr.Columns.UsrK, Usr.Current.K));
			}
			else if (groupSearchType == GroupSearchType.Public)
			{
				q.QueryCondition = new And(
					q.QueryCondition,
					new Q(Group.Columns.PrivateGroupPage, 0),
					new Q(Group.Columns.PrivateChat, 0),
					new Q(Group.Columns.PrivateMemberList, 0)
				);
			}
			else
			{
				throw new NotImplementedException();
			}
			if (excludeBrands)
			{
				q.QueryCondition = new And(q.QueryCondition, new Q(Group.Columns.BrandK, 0));
			}
			q.TopRecords = maxNumberOfItemsToGet;
			q.OrderBy = new OrderBy(Bobs.Group.Columns.Name, OrderBy.OrderDirection.Ascending);
			return new GroupSet(q);
		}

		#endregion
		#region Groups
		internal static IEnumerable<Suggestion> GetGroupsNoBrands(int maxNumberOfItemsToGet, string text, Dictionary<string, object> parameters)
		{
			return GetSuggestionsFromDbComboSql("select top " + Db.PNum(maxNumberOfItemsToGet) + " [Group].[UrlName] as DbComboText, [Group].[K] as DbComboValue from [Group] where [Group].[UrlName] like '%" + Db.PStr(text) + "%' AND (BrandK=0 OR BrandK IS NULL) AND PrivateGroupPage=0 AND PrivateChat=0 AND PrivateMemberList=0 order by [Group].[UrlName]");
		}
		#endregion


		#region GetSuggestionsFromDbComboSql
		internal static IEnumerable<Suggestion> GetSuggestionsFromDbComboSql(string sql)
		{
			List<Suggestion> suggestions = new List<Suggestion>();
			using (SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString))
			{
				using (SqlCommand myCommand = new SqlCommand(sql, conn))
				{
					conn.Open();
					using (SqlDataReader reader = myCommand.ExecuteReader())
					{
						Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
						while (reader.Read())
						{
							suggestions.Add(
								new Suggestion()
								{
									html = reader["DbComboText"].ToString(),
									text = reader["DbComboText"].ToString(),
									value = reader["DbComboValue"].ToString(),

								}
							);
						}
					}
				}
			}
			return suggestions.ToArray();
		}

		#endregion



		internal static IEnumerable<Func<IEnumerable<Suggestion>>> GetPlaces(int maxNumberOfItemsToGet, string text, ReturnType returnType, SearchType searchType)
		{
			if (searchType == SearchType.All || searchType == SearchType.TextStartsWith) yield return () => GetPlacesWithOption(maxNumberOfItemsToGet, text, QueryOperator.TextStartsWith).Select(p => ConvertPlaceToSuggestion(p, returnType));
			if (searchType == SearchType.All || searchType == SearchType.TextContains) yield return () => GetPlacesWithOption(maxNumberOfItemsToGet, text, QueryOperator.TextContains).Select(p => ConvertPlaceToSuggestion(p, returnType));
		}




		internal static CacheKey GetPlacesCacheKey(string text, int maxNumberOfItemsToGet, QueryOperator option)
		{
			return new CacheKey(CacheKeyPrefix.GetPlaces, text, maxNumberOfItemsToGet.ToString(), option.ToString());
		}
		private static IEnumerable<Place> GetPlacesWithOption(int maxNumberOfItemsToGet, string text, QueryOperator option)
		{

			string[] parts = text.Split(',');
			var q = new Query
				(
				new And
					(
					new Q(Place.Columns.UniqueName, option, parts[0].Trim()),
					new Q(Place.Columns.Enabled, true)
					)
				);
			if (parts.Length > 1) q.QueryCondition = new And(q.QueryCondition, new Q(Country.Columns.Name, parts[1].Trim()));
			q.TableElement = Place.RegionCountryJoin;
			q.Columns = PlaceInfo.Columns;
			q.OrderBy = new OrderBy(Place.Columns.UniqueName);
			q.TopRecords = maxNumberOfItemsToGet;
			var placeSet = new PlaceSet(q);

			return placeSet;

		}


		internal static IEnumerable<Func<IEnumerable<Suggestion>>> GetCountries(string text, int maxNumberOfItemsToGet, ReturnType returnType, SearchType searchType)
		{

			if (searchType == SearchType.All || searchType == SearchType.TextStartsWith) yield return () => GetCountriesWithOption(text, QueryOperator.TextStartsWith, maxNumberOfItemsToGet).Select(p => ConvertCountryToSuggestion(p, returnType));
			if (searchType == SearchType.All || searchType == SearchType.TextContains) yield return () => GetCountriesWithOption(text, QueryOperator.TextContains, maxNumberOfItemsToGet).Select(p => ConvertCountryToSuggestion(p, returnType));

		}

		private static IEnumerable<Country> GetCountriesWithOption(string text, QueryOperator option, int maxNumberOfItemsToGet)
		{

			string[] parts = text.Split(',');
			var q = new Query
				(
				new And
					(
					new Q(Country.Columns.Name, option, parts[0].Trim()),
					new Q(Country.Columns.Enabled, true)
					)
				);
			q.Columns = new ColumnSet(Country.Columns.TotalEvents);
			q.Columns.Columns.AddRange(CountryInfo.Columns.Columns);

			q.OrderBy = new OrderBy(Country.Columns.Name);
			q.TopRecords = maxNumberOfItemsToGet;
			return new CountrySet(q);
		}
	}
}
