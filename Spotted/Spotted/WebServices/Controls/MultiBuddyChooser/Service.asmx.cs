using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using Bobs;
using System.Web.Script.Services;
using Js.Controls.MultiBuddyChooser;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using SpottedLibrary.Controls.BuddyImporter;
using Octazen.AddressBook;
using Microsoft.JScript;
using System.Web.Script.Serialization;

namespace Spotted.WebServices.Controls.MultiBuddyChooser
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


		[WebMethod, ScriptMethod/*(UseHttpGet = true)*/]
		public GetMusicTypesAndPlacesResult GetPlacesAndMusicTypes()
		{
			List<Pair> places = new List<Pair>();
			var placesSet = new PlaceSet(new Query(new Q(Place.Columns.CountryK, Country.FilterK)) { OrderBy = new OrderBy(Place.Columns.Name) });
			places.Add(new Pair() {key = "Anywhere", value = "-1"});
			foreach (var place in placesSet)
			{
				places.Add(new Pair() {key = GlobalObject.escape(place.Name), value = place.K.ToString()});
			}
			
			var musicTypesSet = new MusicTypeSet(new Query() { OrderBy = new OrderBy(MusicType.Columns.Order) });
			var musicTypes = new List<Pair>();
			foreach (var musicType in musicTypesSet)
			{
				string leadingSpaces = new string(' ', musicType.Order.ToString().IndexOf('.') == -1 ? 0 : 1);
				musicTypes.Add(new Pair() {key = GlobalObject.escape(leadingSpaces + musicType.GenericName), value = musicType.K.ToString()});
			}
			
			return new GetMusicTypesAndPlacesResult() { musicTypes = musicTypes.ToArray(), places = places.ToArray() };
		}
		
		//JavaScriptSerializer javaScriptSerializer;
		//private JavaScriptSerializer JavaScriptSerializer { get { return javaScriptSerializer ?? (javaScriptSerializer = new JavaScriptSerializer()); } }

		[WebMethod, ScriptMethod]
		public Dictionary<string, object> ResolveUsrsFromMultiBuddyChooserValues(string[] values)
		{
			Dictionary<string, object> returnDictionary = new Dictionary<string, object>();
			foreach (var pair in ResolveUsrsFromMultiBuddyChooserValues(values as IEnumerable<string>))
			{
				returnDictionary.Add(pair.Key, pair.Value as object);
			}
			return returnDictionary;
		}
		public Dictionary<string, int> ResolveUsrsFromMultiBuddyChooserValues(IEnumerable<string> values)
		{
			return ResolveUsrsFromMultiBuddyChooserValues(values, 0, 0, null);
		}

		private JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
		public Dictionary<string, int> ResolveUsrsFromMultiBuddyChooserValues(IEnumerable<string> values, int restrictionGroupK, int threadK, Q restrictionGroupQ)
		{
			Dictionary<string, int> hash = new Dictionary<string, int>();

			foreach (var item in values)
			{
				if (item.StartsWith("{'email'"))
				{
					var esv = javaScriptSerializer.Deserialize<EmailSuggestionValue>(item);
					hash[esv.email] = CreateUsrFromEmail(esv.email, true);
					
				}else if (item.StartsWith("{'emails'"))
				{
					var esv = javaScriptSerializer.Deserialize<EmailsSuggestionValue>(item);
					string[] emails = GlobalObject.unescape(esv.emails).Split(' ');
					foreach (var email in emails)
					{
						hash[email] = CreateUsrFromEmail(email, esv.buddies);
					}
				}
				else if (item.StartsWith("{'MusicTypeK'"))
				{
					var musicTypeAndPlaceK = javaScriptSerializer.Deserialize<MusicTypeKAndPlaceK>(item);
					Query q = new Query();
					Join j = new Join(
						Usr.BuddyUsrJoin,
						new TableElement(TablesEnum.UsrMusicTypeFavourite),
						QueryJoinType.Left,
						Usr.Columns.K,
						UsrMusicTypeFavourite.Columns.UsrK);
					q.TableElement = new Join(
						j,
						new TableElement(TablesEnum.UsrPlaceVisit),
						QueryJoinType.Left,
						Usr.Columns.K,
						UsrPlaceVisit.Columns.UsrK);

					if (threadK > 0)
					{
						q.TableElement = new Join(
							q.TableElement,
							new TableElement(TablesEnum.ThreadUsr),
							QueryJoinType.Left,
							new And(
								new Q(Usr.Columns.K, ThreadUsr.Columns.UsrK, true),
								new Q(ThreadUsr.Columns.ThreadK, threadK)
							)
						);

					}
					else if (restrictionGroupK > 0)
					{
						q.TableElement = new Join(
							q.TableElement,
							new TableElement(TablesEnum.GroupUsr),
							QueryJoinType.Left,
							new And(
								new Q(Usr.Columns.K, GroupUsr.Columns.UsrK, true),
								new Q(GroupUsr.Columns.GroupK, restrictionGroupK)
							)
						);
					}

					Q placeQ = new Q(true);
					if (musicTypeAndPlaceK.PlaceK > 0)
					{
						placeQ = new Or(
							new Q(Usr.Columns.HomePlaceK, musicTypeAndPlaceK.PlaceK),
							new Q(UsrPlaceVisit.Columns.PlaceK, musicTypeAndPlaceK.PlaceK));
					}
					Q musicQ = new Q(true);
					if (musicTypeAndPlaceK.MusicTypeK > 1)
					{
						ArrayList musicQs = new ArrayList();
						MusicType mt = new MusicType(musicTypeAndPlaceK.MusicTypeK);
						musicQs.Add(new Q(Usr.Columns.FavouriteMusicTypeK, 1));
						musicQs.Add(new Q(UsrMusicTypeFavourite.Columns.MusicTypeK, 1));
						musicQs.Add(new Q(Usr.Columns.FavouriteMusicTypeK, musicTypeAndPlaceK.MusicTypeK));
						musicQs.Add(new Q(UsrMusicTypeFavourite.Columns.MusicTypeK, musicTypeAndPlaceK.MusicTypeK));
						foreach (MusicType mtChild in mt.Children)
						{
							musicQs.Add(new Q(Usr.Columns.FavouriteMusicTypeK, mtChild.K));
							musicQs.Add(new Q(UsrMusicTypeFavourite.Columns.MusicTypeK, mtChild.K));
						}
						musicQ = new Or((Q[])musicQs.ToArray(typeof(Q)));
					}

					Q restrictionQ = new Q(true);
					if (threadK > 0)
					{
						restrictionQ = new Q(ThreadUsr.Columns.UsrK, QueryOperator.IsNull, null);
					}
					else if (restrictionGroupK > 0)
					{
						restrictionQ = restrictionGroupQ;
					}

					q.QueryCondition = new And(
						new Q(Buddy.Columns.BuddyUsrK, Usr.Current.K),
						new Q(Buddy.Columns.FullBuddy, true),
						new Q(Buddy.Columns.CanBuddyInvite, true),
						musicQ,
						placeQ);
					if (restrictionGroupQ != null) q.QueryCondition = new And(q.QueryCondition, restrictionGroupQ);
					q.Columns = new ColumnSet(Usr.Columns.NickName, Usr.Columns.K, Usr.Columns.Pic, Usr.Columns.FacebookUID, Usr.Columns.Email);
					q.Distinct = true;
					q.OrderBy = new OrderBy(Usr.Columns.NickName);
					q.DistinctColumn = Usr.Columns.K;
					UsrSet us = new UsrSet(q);
					foreach (Usr u in us)
					{
						hash[u.NickName] = u.K;
					}

				}
				else if (item.IsNumeric())
				{
					int usrK = int.Parse(item);
					Usr usr = new Usr(usrK);
					if (usr.NickName == "")
					{
						hash["User" + usr.K] = usrK;
					}
					else
					{
						hash[usr.NickName] = usrK;
					}


				}
				else
				{
					throw new NotImplementedException();
				}
			}
			return hash;
		}

		[WebMethod, ScriptMethod]
		public string GetBuddiesSelectListHtml()
		{
			Query q = new Query();
			q.Columns = new ColumnSet(Usr.Columns.K, Usr.Columns.NickName, Usr.Columns.Email);
			q.NoLock = true;
			q.TableElement = Usr.BuddyJoin;
			q.QueryCondition = Usr.Current.BuddiesFullQ;
			UsrSet bs = new UsrSet(q);
			var results = from b in bs select new {NickName = b.NickName == "" ? b.Email : b.NickName, b.K};
			var orderedResults = from r in results orderby r.NickName select r;
			string s = String.Join("", orderedResults.Select(b => String.Format("<option value=\"{0}\">{1}</option>", b.K, b.NickName)).ToArray());
			return s;

		}

		[WebMethod, ScriptMethod]
		public int CreateUsrFromEmailAndReturnK(string textEnteredByUser)
		{
			string text = GlobalObject.unescape(textEnteredByUser).Trim();
			return CreateUsrFromEmail(text, true);
		}

		private int CreateUsrFromEmail(string text, bool addAsBuddy)
		{
			string email = text;
			string name = name = email.TruncateWithDots(10);
			int indexOfFirstSpace = text.IndexOf(' ');
			if (indexOfFirstSpace >= 0)
			{
				email = text.Substring(0, indexOfFirstSpace);
				name = text.Substring(indexOfFirstSpace + 1);
			}
			Regex emailRegex = new Regex(Cambro.Misc.RegEx.Email);
			if (!emailRegex.IsMatch(email))
			{
				throw new Exception(email + "is not a valid email address. ALERT");
			}
			else
			{
				var usrSet = new UsrSet(new Query(new Q(Bobs.Usr.Columns.Email, email)));
				if (usrSet.Count == 0)
				{
					var service = new BuddyImporterService();
					service.InviteContacts(Usr.Current, new List<Contact>() { new Contact(name, email) });
					usrSet = new UsrSet(new Query(new Q(Bobs.Usr.Columns.Email, email)));
				}
				Usr newUsr = usrSet[0];
				if (addAsBuddy)
				{
					try
					{
						Buddy buddy = new Buddy(Usr.Current.K, newUsr.K);
					}
					catch (BobNotFound)
					{
						Usr.Current.AddBuddy(newUsr, true, true, Usr.AddBuddySource.BuddyAutoComplete, Buddy.BuddyFindingMethod.EmailAddress, name);
					}
				}
				return usrSet[0].K;
			}
		}

		[WebMethod, ScriptMethod]
		public int CreateUsrsFromEmails(string spaceSeparatedListOfEmailAddresses, bool addAsBuddies)
		{
			return -1;
		}
	}
}
