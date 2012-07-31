using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using Bobs;
using System.Text.RegularExpressions;
using System.Drawing;
using SpottedScript.Controls.ChatClient.Shared;
using System.Web.Services;
//using Facebook.Web;
using Facebook.Api;
using Common;
using System.Collections.Generic;
using Facebook;
using Newtonsoft.Json.Linq;



namespace Spotted.Controls
{
	[ClientScript]
	public partial class Login : MobileEnhancedUserControl
	{

		public Login()
		{
		}

		protected LinkButton ToggleAdminLinkButton;

		#region CheckEmail
		[Client]
		public static Hashtable CheckEmail(string email)
		{
			UsrSet us = new UsrSet(new Query(new Q(Usr.Columns.Email, email), true));

			Hashtable ret = new Hashtable();

			ret["Found"] = us.Count > 0;

			return ret;
		}
		#endregion

		#region validateAndUpdateNoFacebookSignupPanelData
		static void validateAndUpdateNoFacebookSignupPanelData(Hashtable noFacebookSignupPanelData, Usr u)
		{
			string firstName = noFacebookSignupPanelData["FirstName"].ToString();
			string lastName = noFacebookSignupPanelData["LastName"].ToString();
			string nickname = noFacebookSignupPanelData["Nickname"].ToString();
			int dateOfBirthYear = (int)noFacebookSignupPanelData["DateOfBirthYear"];
			int dateOfBirthMonthJanZero = (int)noFacebookSignupPanelData["DateOfBirthMonth"];
			int dateOfBirthDay = (int)noFacebookSignupPanelData["DateOfBirthDay"];
			bool sexIsMale = (bool)noFacebookSignupPanelData["SexMale"];

			#region First name
			firstName = Cambro.Web.Helpers.StripHtml(firstName);

			if (firstName.Length == 0)
				throw new Exception("First name missing");

			if (firstName.Length > 20)
				throw new Exception("First name too long");
			#endregion

			#region Last name
			lastName = Cambro.Web.Helpers.StripHtml(lastName);

			if (lastName.Length == 0)
				throw new Exception("Last name missing");

			if (lastName.Length > 20)
				throw new Exception("Last name too long");
			#endregion

			#region Nickname
			if (nickname.Length == 0)
				throw new Exception("Nickname missing");

			nickname = Usr.GetCompliantNickName(nickname);

			if (nickname.Length < 2)
				throw new Exception("Nickname too short");

			if (nickname.Length > 20)
				throw new Exception("Nickname too long");

			nickname = Usr.GetUniqueNickName(nickname, u == null ? 0 : u.K);
			#endregion

			#region Date of birth
			DateTime dateOfBirth = DateTime.MinValue;
			try
			{
				dateOfBirth = new DateTime(dateOfBirthYear, dateOfBirthMonthJanZero + 1, dateOfBirthDay);
			}
			catch
			{
				throw new Exception("Date of birth invalid");
			}
			#endregion

			if (u != null)
			{
				u.FirstName = firstName;
				u.LastName = lastName;
				u.IsMale = sexIsMale;
				u.IsFemale = !sexIsMale;
				u.DateOfBirth = dateOfBirth;
				u.NickName = nickname;
			}
		}
		#endregion

		#region NoFacebookNewAccount
		[Client]
		public static Hashtable NoFacebookNewAccount(
			Hashtable noFacebookSignupPanelData,
			Hashtable detailsPanelData, 
			Hashtable captchaData)
		{

			string email = noFacebookSignupPanelData["Email"].ToString();
			string password = noFacebookSignupPanelData["Password"].ToString();

			#region Email
			if (email.Length == 0)
				throw new Exception("Email missing");

			if (!Cambro.Misc.RegEx.IsEmail(email))
				throw new Exception("Email wrong format");

			UsrSet us = new UsrSet(new Query(new Q(Usr.Columns.Email, email), true));
			if (us.Count > 0)
				throw new Exception("Email already in database");
			#endregion

			#region Password
			if (password.Length == 0)
				throw new Exception("Password missing");

			if (password.Length < 4)
				throw new Exception("Password too short");
			#endregion

			validateAndUpdateNoFacebookSignupPanelData(noFacebookSignupPanelData, null);
			
			Hashtable ret = new Hashtable();

			bool passedCaptcha = false;
			if (!captchaIsOk(null, email, ret, captchaData, ref passedCaptcha))
				return ret;

			Usr u = Usr.CreateNewUsrMaster(email, password, "", "", "", false);
			u.IsEmailVerified = false;
			u.IsSkeleton = true;
			u.NeedsCaptcha = !passedCaptcha;
			u.PassedCaptcha = passedCaptcha;
			u.Update();

			validateAndUpdateNoFacebookSignupPanelData(noFacebookSignupPanelData, u);

			u.AgreeTerms = true;
			u.LegalTermsUser2 = true;
			u.IsSkeleton = false;

			noFacebookLogin(u, ret, null, detailsPanelData);
			
			try
			{
			    u.SendWelcomeEmail(null, null, "");
			}
			catch { }

			return ret;
		}
		#endregion

		#region captchaIsOk
		static bool captchaIsOk(Usr u, string email, Hashtable ret, Hashtable captchaData, ref bool passedCaptcha)
		{
			#region Captcha
			if (Settings.CaptchaEnabledStatus == Settings.CaptchaEnabledStatusOption.On)
			{
				if (u == null || (u.NeedsCaptcha.HasValue && u.NeedsCaptcha.Value))
				{
					string dataEntered = "";
					try
					{
						dataEntered = captchaData["Entered"].ToString();
					}
					catch { }

					if (dataEntered.Length == 0)
					{
						ret["NeedsCaptcha"] = true;
						ret["CaptchaEncrypted"] = getNewEncryptedText(email);
						return false;
					}
					else
					{
						try
						{
							
							string dataEncryptedPassthrough = captchaData["Passthrough"].ToString();
							string dataUnencryptedPassthrough = Cambro.Misc.Utility.Decrypt(dataEncryptedPassthrough);
							string lettersUnencryptedPassthrough = dataUnencryptedPassthrough.Split('|')[0];
							string emailUnencryptedPassthrough = HttpUtility.UrlDecode(dataUnencryptedPassthrough.Split('|')[1]);
							if (dataEntered.Length == 5 && lettersUnencryptedPassthrough.ToLower() == dataEntered.ToLower() && emailUnencryptedPassthrough.ToLower() == email.ToLower())
								passedCaptcha = true;
						}
						catch { }

						if (!passedCaptcha)
						{
							ret["NeedsCaptcha"] = true;
							ret["CaptchaEncrypted"] = getNewEncryptedText(email);
							ret["CaptchaFailed"] = true;
							return false;
						}
					}
				}
			}
			return true;
			#endregion
		}
		static string getNewEncryptedText(string email)
		{
			string text = Cambro.Misc.Utility.GenRandomChars(5).ToUpper() + "|" + HttpUtility.UrlEncode(email.ToLower());
			return Cambro.Misc.Utility.Encrypt(text, DateTime.Now.AddHours(1));
		}
		#endregion

		#region GetUniqueNickName
		[Client]
		public static Hashtable GetUniqueNickName(string nickname, int usrK)
		{
			string newNickname = Usr.GetUniqueNickName(nickname, usrK);

			Hashtable ret = new Hashtable();

			ret["Nickname"] = newNickname;
			
			return ret;
		}
		#endregion

		#region NoFacebookLogin
		[Client]
		public static Hashtable NoFacebookLogin(
			string nickNameOrEmail, 
			string password, 
			Hashtable captchaData, 
			Hashtable noFacebookSignupPanelData, 
			Hashtable detailsPanelData, 
			bool autoLogin, 
			int autoLoginUsrK, 
			string autoLoginString)
		{
			Hashtable ret = new Hashtable();

			Usr u = null;

			if (!autoLogin)
			{
				if (nickNameOrEmail.Trim().Length == 0)
				{
					ret["Error"] = true;
					return ret;
				}

				Q q = null;
				if (nickNameOrEmail.Contains("@"))
					q = new Q(Usr.Columns.Email, nickNameOrEmail.Trim());
				else
					q = new Q(Usr.Columns.NickName, nickNameOrEmail.Trim());
				UsrSet us = new UsrSet(new Query(q));

				if (us.Count == 0)
				{
					ret["Error"] = true;
					return ret;
				}

				u = us[0];

				if (u.HasNullPassword)
				{
					u.SendPasswordResetLink();
					ret["Error"] = true;
					ret["HasNullPassword"] = true;
					return ret;
				}

				if (!u.CheckPassword(password.Trim()))
				{
					ret["Error"] = true;
					return ret;
				}
			}
			else
			{
				if (autoLoginUsrK == 0 || autoLoginString.Length == 0)
				{
					ret["Error"] = true;
					return ret;
				}

				try
				{
					u = new Usr(autoLoginUsrK);
				}
				catch
				{
					ret["Error"] = true;
					return ret;
				}

				if (u.LoginString.ToLower() != autoLoginString.ToLower())
				{
					ret["Error"] = true;
					return ret;
				}

				if (u.IsSkeleton)
				{
					string passwordFromAutoLoginSkeletonData = noFacebookSignupPanelData["Password"].ToString();
					if (passwordFromAutoLoginSkeletonData.Length > 0)
					{
						if (passwordFromAutoLoginSkeletonData.Length < 4)
							throw new Exception("Password too short");

						u.SetPassword(passwordFromAutoLoginSkeletonData, false);
					}
				}
			}

			bool passedCaptcha = false;
			if (!captchaIsOk(u, u.Email, ret, captchaData, ref passedCaptcha))
				return ret;

			if (passedCaptcha)
			{
				u.NeedsCaptcha = false;
				u.PassedCaptcha = true;
			}

			noFacebookLogin(u, ret, noFacebookSignupPanelData, detailsPanelData);

			return ret;
		}
		#endregion

		#region SendPassword
		[Client]
		public static Hashtable SendPassword(string emailOrNickname)
		{
			if (emailOrNickname.Length == 0)
				throw new Exception("Password missing.");

			Query q = new Query();
			q.QueryCondition = new Or(new Q(Usr.Columns.NickName, emailOrNickname.Trim()), new Q(Usr.Columns.Email, emailOrNickname.Trim()));
			UsrSet us = new UsrSet(q);

			Hashtable ret = new Hashtable();

			if (us.Count != 1)
			{
				ret["Done"] = false;
			}
			else
			{
				us[0].SendPasswordResetLink();
				ret["Done"] = true;
			}
			return ret;
		}
		#endregion

		#region SwitchAccounts
		[Client]
		public static Hashtable SwitchAccounts(string currentUIDFromFacebook, int autoLoginUsrK, string autoLoginUsrLoginString, Hashtable detailsPanelData)
		{
			var facebook = new FacebookGraphAPI(Facebook.Apps.Dsi);
			JObject user = facebook.GetObject("me", null);
			
			if (facebook.Uid != long.Parse(currentUIDFromFacebook))
				throw new Exception("Inconsistant facebook login");

			Usr u = new Usr(autoLoginUsrK);
			if (u.LoginString.ToLower() == autoLoginUsrLoginString.ToLower())
			{
				Hashtable ret = new Hashtable();

				if (checkSkeleton(u, ret, null, detailsPanelData, user))
					return ret;

				UsrSet usOld = new UsrSet(new Query(new Q(Usr.Columns.FacebookUID, facebook.Uid)));
				foreach (Usr uOld in usOld)
				{
					uOld.FacebookUID = null;
					uOld.FacebookConnected = false;
					if (!uOld.PassedCaptcha.HasValue || !uOld.PassedCaptcha.Value)
						uOld.NeedsCaptcha = true;
					uOld.Update();
				}

				linkAndLogin(u, ret, facebook, user, detailsPanelData);

				return ret;
			}
			else
				throw new Exception();
		}
		#endregion

		#region CreateNewAccount
		[Client]
		public static Hashtable CreateNewAccount(Hashtable detailsPanelData)
		{

			var facebook = new FacebookGraphAPI(Facebook.Apps.Dsi);
			JObject user = facebook.GetObject("me", null);
			string email = user.Value<string>("email");
			string firstName = user.Value<string>("first_name");
			string lastName = user.Value<string>("last_name");

			Hashtable ret = new Hashtable();

			Query qUID = new Query();
			qUID.QueryCondition = new Q(Usr.Columns.FacebookUID, facebook.Uid);
			UsrSet usUID = new UsrSet(qUID);
			if (usUID.Count == 0)
			{
				Usr u = Usr.CreateNewUsrMaster(email, "", firstName, lastName, "", false);
				u.IsEmailVerified = true;
				u.IsSkeleton = true;
				u.NeedsCaptcha = false;
				u.Update();

				try
				{
					linkAndLogin(u, ret, facebook, user, detailsPanelData); //this will always run u.Update()
				}
				catch (Exception ex)
				{
					//maybe we created a duplicate?
					//try to find usr by facebook id...
					UsrSet us = new UsrSet(new Query(new Q(Usr.Columns.FacebookUID, facebook.Uid)));
					if (us.Count > 0)
					{
						u = us[0];
						loginAndSetAuthCookie(u, ret, facebook.AccessToken); //this will always run u.Update()
					}
					else
						throw ex;
				}

				try
				{
					u.SendWelcomeEmail(null, null, "");
				}
				catch { }

				return ret;
			}
			else
				throw new Exception("Can't create new user - already have one.");

		}
		#endregion

		#region GetHomePlaceFromFacebook
		[Client]
		public static Hashtable GetHomePlaceFromFacebook()
		{
			var facebook = new FacebookGraphAPI(Facebook.Apps.Dsi);
			JObject user = facebook.GetObject("me", null);

			Hashtable ret = new Hashtable();
			ret["HomePlace"] = getHomePlaceFromFacebookInternal(user);
			return ret;
		}
		static Hashtable getHomePlaceFromFacebookInternal(JObject user)
		{

			bool locationGoodMatch = false;
			Place locationPlace = null;

			try
			{
				locationPlace = getPlaceFromString(user["location"].Value<string>("name"), ref locationGoodMatch);
			}
			catch { }

			bool homeTownGoodMatch = false;
			Place homeTownPlace = null;
			if (locationPlace == null || !locationGoodMatch)
			{
				try
				{
					homeTownPlace = getPlaceFromString(user["hometown"].Value<string>("name"), ref homeTownGoodMatch);
				}
				catch { }
			}

			Place place = null;
			bool goodMatch = false;
			if (locationPlace != null)
			{
				if (homeTownGoodMatch && !locationGoodMatch)
				{
					place = homeTownPlace;
					goodMatch = homeTownGoodMatch;
				}
				else
				{
					place = locationPlace;
					goodMatch = locationGoodMatch;
				}
			}
			else if (homeTownPlace != null)
			{
				place = homeTownPlace;
				goodMatch = homeTownGoodMatch;
			}
			else
			{
				place = new Place(1); // if all else fails, use London.
				goodMatch = false;
			}


			Hashtable ret = new Hashtable();
			ret["PlaceName"] = place.NamePlain;
			ret["CountryName"] = place.Country.FriendlyName;
			ret["PlaceK"] = place.K;
			ret["CountryK"] = place.CountryK;
			ret["GoodMatch"] = goodMatch;
			return ret;
		}
		#endregion

		#region getPlaceFromString
		static Place getPlaceFromString(string homeTownName, ref bool goodMatch)
		{
			if (homeTownName.Trim().Length == 0)
				return null;

			string placeName = "";
			string countryName = "";
			Place homePlace = null;
			Country homeCountry = null;

			#region split into country and place name
			if (homeTownName.Length > 0 && homeTownName.Contains(","))
			{
				placeName = homeTownName.Substring(0, homeTownName.LastIndexOf(',')).Trim();
				countryName = homeTownName.Substring(homeTownName.LastIndexOf(',') + 1).Trim();
			}
			else
			{
				placeName = homeTownName;
			}
			#endregion

			if (countryName.Length > 0)
			{
				#region get country by name
				CountrySet csName = new CountrySet(
					new Query(
						new Q(Country.Columns.Name, countryName.Trim())
					)
				);
				if (csName.Count > 0)
				{
					homeCountry = csName[0];
				}
				#endregion
				else
				{
					#region get country by friendly name (abbreviation)
					CountrySet csFriendlyName = new CountrySet(
						new Query(
							new Q(Country.Columns.FriendlyName, countryName.Trim())
						)
					);
					if (csFriendlyName.Count > 0)
					{
						homeCountry = csFriendlyName[0];
					}
					#endregion
				}
			}
			#region if we haven't found a country yet, use the IPcountry
			if (homeCountry == null)
			{
				try
				{
					homeCountry = new Country(IpCountry.ClientCountryK());
				}
				catch
				{
					homeCountry = new Country(224);
				}
			}
			#endregion

			if (placeName.Length > 0)
			{
				#region lookup enabled places in the home country
				PlaceSet psNameWithCountry = new PlaceSet(
					new Query(
						new And(
							new Q(Place.Columns.Name, placeName.Trim()),
							new Q(Place.Columns.CountryK, homeCountry.K),
							new Q(Place.Columns.Enabled, true)
						)
					)
				);
				if (psNameWithCountry.Count > 0)
				{
					homePlace = psNameWithCountry[0];
					goodMatch = true;
				}
				#endregion
				else
				{
					#region lookup non-enabled places and find the nearest enabled place
					PlaceSet psNameWithCountryNotEnalbed = new PlaceSet(
						new Query(
							new And(
								new Q(Place.Columns.Name, placeName.Trim()),
								new Q(Place.Columns.CountryK, homeCountry.K)
							)
						)
					);
					if (psNameWithCountryNotEnalbed.Count > 0)
					{
						PlaceSet ps = new PlaceSet(
							new Query(
								new Q(Place.Columns.Enabled, true),
								psNameWithCountryNotEnalbed[0].NearestPlacesOrderBy,
								1
							)
						);
						if (ps.Count > 0)
						{
							homePlace = ps[0];
							goodMatch = true;
						}
					}
					#endregion
					else
					{
						#region lookup global enabled places by name
						PlaceSet psName = new PlaceSet(
							new Query(
								new And(
									new Q(Place.Columns.Name, placeName.Trim()),
									new Q(Place.Columns.Enabled, true)
								),
								new OrderBy(Place.Columns.Population, OrderBy.OrderDirection.Descending),
								1
							)
						);
						if (psName.Count > 0)
						{
							homePlace = psName[0];
						}
						#endregion
						else
						{
							#region lookup global non-enabled places and find the nearest enabled place
							PlaceSet psNameNotEnalbed = new PlaceSet(
								new Query(
									new Q(Place.Columns.Name, placeName.Trim())
								)
							);
							if (psNameNotEnalbed.Count > 0)
							{
								PlaceSet ps = new PlaceSet(
									new Query(
										new Q(Place.Columns.Enabled, true),
										psNameNotEnalbed[0].NearestPlacesOrderBy,
										1
									)
								);
								if (ps.Count > 0)
								{
									homePlace = ps[0];
								}
							}
							#endregion
						}
					}
				}
			}
			#region if we haven't found a place yet, use the capital (or largest enabled place) from the home country. If all fails, use London.
			if (homePlace == null)
			{
				if (homeCountry != null)
				{
					homePlace = homeCountry.GetCapitalOrLargestEnabledPlace();
				}
			}

			if (homePlace == null)
				homePlace = new Place(1);
			#endregion

			return homePlace;
		}
		#endregion

		#region updateFromDetailsData
		static void updateFromDetailsData(Usr u, Hashtable detailsPanelData)
		{

			Place p = new Place((int)detailsPanelData["PlaceK"]);
			u.HomePlaceK = p.K;
			u.AddressCountryK = p.CountryK;

			MusicType mt = new MusicType((int)detailsPanelData["MusicTypeK"]);
			u.FavouriteMusicTypeK = mt.K;

			#region update UsrMusicTypeFavourite table
			try
			{
				UsrMusicTypeFavourite umf = new UsrMusicTypeFavourite();
				umf.UsrK = u.K;
				umf.MusicTypeK = mt.K;
				umf.Update();

				u.UpdateMusicTypesFavouriteCount(false);
			}
			catch { }
			#endregion

			#region update UsrPlaceVisit table
			try
			{
				UsrPlaceVisit upv = new UsrPlaceVisit();
				upv.UsrK = u.K;
				upv.PlaceK = p.K;
				upv.Update();

				u.UpdatePlacesVisitCount(false);
			}
			catch { }
			#endregion

			#region Facebook
			u.FacebookStory = (bool)detailsPanelData["Facebook"];
			u.FacebookStory1 = (bool)detailsPanelData["Facebook"];

			u.FacebookEventAdd = (bool)detailsPanelData["Facebook"];
			u.FacebookEventAttend = (bool)detailsPanelData["Facebook"];

			u.FacebookStoryAttendEvent = (bool)detailsPanelData["Facebook"];
			u.FacebookStoryBuyTicket = (bool)detailsPanelData["Facebook"];
			u.FacebookStoryEventReview = (bool)detailsPanelData["Facebook"];
			u.FacebookStoryFavourite = (bool)detailsPanelData["Facebook"];
			u.FacebookStoryJoinGroup = (bool)detailsPanelData["Facebook"];
			u.FacebookStoryLaugh = (bool)detailsPanelData["Facebook"];
			u.FacebookStoryNewBuddy = (bool)detailsPanelData["Facebook"];
			u.FacebookStoryNewTopic = (bool)detailsPanelData["Facebook"];
			u.FacebookStoryPhotoFeatured = (bool)detailsPanelData["Facebook"];
			u.FacebookStoryPostNews = (bool)detailsPanelData["Facebook"];
			u.FacebookStoryPublishArticle = (bool)detailsPanelData["Facebook"];
			u.FacebookStorySpotted = (bool)detailsPanelData["Facebook"];
			u.FacebookStoryUploadPhoto = (bool)detailsPanelData["Facebook"];

			#endregion

			#region WeeklyEmail
			u.SendSpottedEmails = (bool)detailsPanelData["WeeklyEmail"];
			#endregion

			#region PartyInvites
			u.SendSpottedTexts = (bool)detailsPanelData["PartyInvites"];
			u.SendFlyers = (bool)detailsPanelData["PartyInvites"];
			u.SendInvites = (bool)detailsPanelData["PartyInvites"];
			#endregion

			u.AgreeTerms = true;
			u.LegalTermsUser2 = true;
			u.IsSkeleton = false;
			u.Update();

			#region update Prefs

			if (mt.K != 1)
				Prefs.Current["MusicPref"] = mt.K;

			if (p.CountryK != 224)
				Prefs.Current["HomeCountryK"] = p.CountryK;

			if (mt.K != 1 || p.CountryK != 224)
				Prefs.Current.Update();

			#endregion
		}
		#endregion

		#region getInformationFromFacebook
		static void getInformationFromFacebook(Usr u, JObject user)
		{
			string email = user.Value<string>("email");
			string firstName = user.Value<string>("first_name");
			string lastName = user.Value<string>("last_name");
			bool isMale = user.Value<string>("gender") == "male";
			bool isFemale = user.Value<string>("gender") == "female";
			string dateOfBirthString = user.Value<string>("birthday");
			DateTime dateOfBirth = new DateTime(
				int.Parse(dateOfBirthString.Substring(6,4)),
				int.Parse(dateOfBirthString.Substring(0,2)), 
				int.Parse(dateOfBirthString.Substring(3,2)));

			u.FirstName = firstName;
			u.LastName = lastName;
			u.IsMale = isMale;
			u.IsFemale = isFemale;
			u.DateOfBirth = dateOfBirth;
			u.NickName = Usr.GetUniqueNickName(firstName + "-" + lastName, 0);
			
			#region Misc
			u.AgreeTerms = true;
			u.LegalTermsUser2 = true;
			u.IsSkeleton = false;
			#endregion
		}
		#endregion

		#region GetUserByFacebookUID
		[Client]
		public static Hashtable GetUserByFacebookUID(int autoLoginUsrK, string autoLoginUsrLoginString)
		{

			var facebook = new FacebookGraphAPI(Facebook.Apps.Dsi);
			
			Hashtable ret = new Hashtable();
			ret["FacebookUIDMatch"] = false;
			ret["FacebookAutoLoginUsrMatch"] = false;
			ret["FacebookEmailMatch"] = false;
			ret["FacebookEmailMatchToCurrentUser"] = false;

			#region add data about the AutoLoginUsr (so we can display this if we don't end up logged in as it)
			if (autoLoginUsrK > 0)
			{
				Usr u = null;
				try
				{
					u = new Usr(autoLoginUsrK);
				}
				catch { }

				if (u != null)
				{
					Hashtable autoLoginUsrHash = new Hashtable();

					autoLoginUsrHash["NickName"] = u.NickName;

					if (u.LoginString.ToLower() == autoLoginUsrLoginString.ToLower())
					{
						autoLoginUsrHash["LoginStringMatch"] = true;
						autoLoginUsrHash["Email"] = u.Email;
					}
					else
						autoLoginUsrHash["LoginStringMatch"] = false;
					
					if (u.NickName.Length > 0)
						autoLoginUsrHash["Link"] = u.Link();

					autoLoginUsrHash["HasNullPassword"] = u.HasNullPassword;
					ret["AutoLoginUsr"] = autoLoginUsrHash;
				}
			}
			#endregion

			#region try to find usr linked by UID
			Query qUID = new Query();
			qUID.QueryCondition = new Q(Usr.Columns.FacebookUID, facebook.Uid);
			UsrSet usUID = new UsrSet(qUID);
			if (usUID.Count > 0)
			{
				Usr u = usUID[0];

				if (!u.IsEmailVerified && 
					autoLoginUsrK > 0 && 
					autoLoginUsrK == u.K &&
					u.LoginString.ToLower() == autoLoginUsrLoginString.ToLower())
				{
					u.IsEmailVerified = true;
					u.Update();
				}

				ret["FacebookUIDMatch"] = true;
				loginAndSetAuthCookie(u, ret, facebook.AccessToken);
				return ret;
			}
			#endregion

			#region if we have a forced usr (auto login link), we should link it immediatly
			if (autoLoginUsrK > 0)
			{
				Usr u = null;
				try
				{
					u = new Usr(autoLoginUsrK);
				}
				catch
				{
				}

				if (u != null && u.LoginString.ToLower() == autoLoginUsrLoginString.ToLower() && !u.EnhancedSecurity)
				{
					if (!u.IsEmailVerified)
					{
						u.IsEmailVerified = true;
						u.Update();
					}

					ret["FacebookAutoLoginUsrMatch"] = true;
					return ret;
				}
			}
			#endregion

			#region if we have a user that matches the email from facebook, we should suggest it, but not log in
			var user = facebook.GetObject("me", null);
			string email = user.Value<string>("email");

			if (email.Length > 0)
			{
				Query qEmail = new Query();
				qEmail.QueryCondition = new Q(Usr.Columns.Email, email);
				UsrSet usEmail = new UsrSet(qEmail);
				if (usEmail.Count > 0)
				{
					ret["FacebookEmailMatch"] = true;
					ret["FacebookEmailMatchToCurrentUser"] = !usEmail[0].IsSkeleton && usEmail[0].DateTimeLastAccess.AddMonths(2) > DateTime.Now;

					if (usEmail[0].EnhancedSecurity)
						ret["EnhancedSecurity"] = true;

					Hashtable emailMatchUsr = new Hashtable();
					emailMatchUsr["NickName"] = usEmail[0].NickName;
					emailMatchUsr["Email"] = usEmail[0].Email;
					if (usEmail[0].NickName.Length > 0)
						emailMatchUsr["Link"] = usEmail[0].Link();
					emailMatchUsr["HasNullPassword"] = usEmail[0].HasNullPassword;
					ret["EmailMatchUsr"] = emailMatchUsr;

					return ret;
				}
			}
			#endregion

			return ret;
			
		}
		#endregion

		#region AutoLinkByAutoLoginUsr
		[Client]
		public static Hashtable AutoLinkByAutoLoginUsr(int autoLoginUsrK, string autoLoginUsrLoginString, Hashtable detailsPanelData)
		{
			var facebook = new FacebookGraphAPI(Facebook.Apps.Dsi);
			JObject user = facebook.GetObject("me", null);

			Hashtable ret = new Hashtable();

			if (autoLoginUsrK > 0)
			{
				Usr u = null;
				try
				{
					u = new Usr(autoLoginUsrK);
				}
				catch
				{
				}

				if (u != null && !u.EnhancedSecurity && u.LoginString.ToLower() == autoLoginUsrLoginString.ToLower())
				{
					if (!u.IsEmailVerified)
					{
						u.IsEmailVerified = true;
						u.Update();
					}

					ret["FacebookAutoLoginUsrMatch"] = true;

					linkAndLogin(u, ret, facebook, user, detailsPanelData);
					return ret;
				}
			}

			ret["FacebookAutoLoginUsrMatch"] = false;
			return ret;

		}
		#endregion

		#region AutoLinkByEmail
		[Client]
		public static Hashtable AutoLinkByEmail(Hashtable detailsPanelData)
		{
			var facebook = new FacebookGraphAPI(Facebook.Apps.Dsi);

			Hashtable ret = new Hashtable();

			var user = facebook.GetObject("me", null);
			string email = user.Value<string>("email");

			if (email.Length > 0)
			{
				Query qEmail = new Query();
				qEmail.QueryCondition = new Q(Usr.Columns.Email, email);
				UsrSet usEmail = new UsrSet(qEmail);
				if (usEmail.Count > 0 && !usEmail[0].EnhancedSecurity)
				{

					ret["FacebookEmailMatch"] = true;
					linkAndLogin(usEmail[0], ret, facebook, user, detailsPanelData);
					return ret;

				}
			}

			ret["FacebookEmailMatch"] = false;
			return ret;

		}
		#endregion

		#region LinkAccounts
		[Client]
		public static Hashtable LinkAccounts(string nickNameOrEmail, string password, Hashtable detailsPanelData)
		{
			var facebook = new FacebookGraphAPI(Facebook.Apps.Dsi);
			var user = facebook.GetObject("me", null);
			string email = user.Value<string>("email");

			Hashtable ret = new Hashtable();

			if (nickNameOrEmail.Trim().Length == 0)
			{
				ret["Error"] = true;
				return ret;
			}

			Q q = null;
			if (nickNameOrEmail.Contains("@"))
				q = new Q(Usr.Columns.Email, nickNameOrEmail.Trim());
			else
				q = new Q(Usr.Columns.NickName, nickNameOrEmail.Trim());
			UsrSet us = new UsrSet(new Query(q));

			if (us.Count == 0)
			{
				//throw new Exception("User not found");
				ret["Error"] = true;
				return ret;
			}

			Usr u = us[0];

			if (!u.CheckPassword(password.Trim()))
			{
				//throw new Exception("Wrong password");
				ret["Error"] = true;
				return ret;
			}

			linkAndLogin(u, ret, facebook, user, detailsPanelData);

			return ret;
		}
		#endregion

		#region checkSkeleton
		static bool checkSkeleton(Usr u, Hashtable ret, Hashtable noFacebookSignupPanelData, Hashtable detailsPanelData, JObject user)
		{
			if (noFacebookSignupPanelData == null && u.IsSkeleton && user == null)
			{
				ret["IsSkeleton"] = true;

				Hashtable details = new Hashtable();
				details["Nickname"] = u.NickName;
				details["UsrK"] = u.K;
				ret["Details"] = details;

				return true;
			}
			else if (detailsPanelData == null && u.IsSkeleton)
			{
				ret["NeedsConfirmation"] = true;
				
				Hashtable details = new Hashtable();
				if (user != null)
					details["HomePlace"] = getHomePlaceFromFacebookInternal(user);
				ret["Details"] = details;

				return true;
			}
			else if (detailsPanelData == null && !u.FacebookStory.HasValue)
			{
				ret["NeedsConfirmation"] = true;

				Hashtable details = new Hashtable();
				#region home
				Hashtable home = new Hashtable();
				home["PlaceName"] = u.Home.NamePlain;
				home["CountryName"] = u.Home.Country.FriendlyName;
				home["PlaceK"] = u.Home.K;
				home["CountryK"] = u.Home.CountryK;
				home["GoodMatch"] = true;
				#endregion
				details["HomePlace"] = home;
				details["FavouriteMusicK"] = u.FavouriteMusicTypeK;
				details["SendSpottedEmails"] = u.SendSpottedEmails;
				details["SendEflyers"] = u.SendFlyers;
				ret["Details"] = details;

				return true;
			}
			else
				return false;
		}
		#endregion

		#region linkAndLogin
		static void linkAndLogin(Usr u, Hashtable returnValue, FacebookGraphAPI facebook, JObject user, Hashtable detailsPanelData)
		{
			if (checkSkeleton(u, returnValue, null, detailsPanelData, user))
				return;

			if (u.IsSkeleton)
			{
				getInformationFromFacebook(u, user);
			}

			if (detailsPanelData != null)
			{
				updateFromDetailsData(u, detailsPanelData);
			}

			if (u.FacebookUID != facebook.Uid && u.FacebookStory.HasValue && u.FacebookStory.Value)
			{
				FacebookPost.CreateNewConnect(facebook, u.K);
			}
			
			u.FacebookUID = facebook.Uid;
			u.FacebookConnected = true;
			u.FacebookConnectedDateTime = DateTime.Now;

			loginAndSetAuthCookie(u, returnValue, facebook.AccessToken); //this will always run u.Update()
		}
		#endregion

		#region noFacebookLogin
		static void noFacebookLogin(Usr u, Hashtable returnValue, Hashtable noFacebookSignupPanelData, Hashtable detailsPanelData)
		{
			if (checkSkeleton(u, returnValue, noFacebookSignupPanelData, detailsPanelData, null))
				return;

			if (noFacebookSignupPanelData != null)
				validateAndUpdateNoFacebookSignupPanelData(noFacebookSignupPanelData, u);

			if (detailsPanelData != null)
				updateFromDetailsData(u, detailsPanelData);

			loginAndSetAuthCookie(u, returnValue, ""); //this will always run u.Update()
		}
		#endregion

		#region loginAndSetAuthCookie
		static void loginAndSetAuthCookie(Usr u, Hashtable returnValue, string facebookAccessToken)
		{
			//normal login stuff
			u.LoginCount++;
			u.DateTimeLastAccess = DateTime.Now;
			if (facebookAccessToken.Length > 0)
				u.FacebookAccessToken = facebookAccessToken;
			u.Update();
			u.LogInAsThisUserDontSetCookieNew();
			

			Hashtable authUsr = new Hashtable();
			authUsr["NickName"] = u.NickName;
			if (u.NickName.Length > 0)
				authUsr["Link"] = u.Link();
			authUsr["Email"] = u.Email;
			authUsr["HasNullPassword"] = u.HasNullPassword;
			returnValue["AuthUsr"] = authUsr;

			returnValue["AuthCookie"] = getAuthCookie(u.GetAuthCookie(), u.K);

		}
		#endregion

		#region getAuthCookie
		static Hashtable getAuthCookie(HttpCookie cookie, int usrK)
		{
			Hashtable authCookie = new Hashtable();
			authCookie["UsrK"] = usrK.ToString();
			authCookie["Name"] = cookie.Name;
			authCookie["Value"] = cookie.Value;
			authCookie["HttpOnly"] = cookie.HttpOnly.ToString();
			authCookie["Path"] = cookie.Path;
			authCookie["Secure"] = cookie.Secure;
			authCookie["Expires"] = cookie.Expires;
			return authCookie;
		}
		#endregion

		#region UnlinkAccount
		[Client]
		public static Hashtable UnlinkAccount(string password)
		{
			if (Usr.Current == null)
				throw new Exception("Not logged in");

			if (password.Trim().Length > 0)
			{
				if (!Usr.Current.HasNullPassword)
					throw new Exception("Already has a password");

				if (password.Trim().Length < 4)
					throw new Exception("Password must be over 4 characters");

				Usr.Current.SetPassword(password, false);
			}

			Usr.Current.FacebookConnected = false;
			if (!Usr.Current.PassedCaptcha.HasValue || !Usr.Current.PassedCaptcha.Value)
				Usr.Current.NeedsCaptcha = true;
			Usr.Current.FacebookUID = null;
			Usr.Current.Update();
			
			Hashtable ret = new Hashtable();

			ret["DoneUnlink"] = true;

			return ret;
		}
		#endregion

		#region Page_Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			JQuery.Include(Page, "selectboxes", "selectboxes");
			JQuery.Include(Page, "ui.core");
			JQuery.Include(Page, "ui.widget");
			JQuery.Include(Page, "ui.button");
			JQuery.Include(Page, "ui.mouse");
			JQuery.Include(Page, "ui.draggable");
			JQuery.Include(Page, "ui.mouse");
			JQuery.Include(Page, "ui.position");
			JQuery.Include(Page, "ui.resizable");
			JQuery.Include(Page, "ui.dialog");
		}
		#endregion

		#region ContainerPage
		Spotted.Master.DsiPage ContainerPage
		{
			get
			{
				return (Spotted.Master.DsiPage)Page;
			}
		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

	}
}

public static class JSONHelper
{
	public static string ToJSON(this object obj)
	{
		System.Web.Script.Serialization.JavaScriptSerializer serializerObj = new System.Web.Script.Serialization.JavaScriptSerializer();
		return serializerObj.Serialize(obj);
	}
}
