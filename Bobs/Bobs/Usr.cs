using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Collections;
using System.Collections.Generic;
using Cambro;
using Cambro.Web;
using Cambro.Misc;

using System.Net;
using System.IO;
using System.Text;
using System.Net.Sockets;

using System.Configuration;
using System.Diagnostics;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Xml;
using System.Text.RegularExpressions;

using System.Runtime.Remoting;
using System.Runtime.Serialization;
using Bobs.DataHolders;
using Common;
using Bobs.JobProcessor;
using Bobs.CachedDataAccess;
using Facebook;
using Newtonsoft.Json.Linq;

namespace Bobs
{


	#region Usr
	/// <summary>
	/// Each registered user of the system gets a record in this table.
	/// </summary>
	[Serializable]
	public partial class Usr : IPage, IName, IReadableReference, IBobType, IObjectPage, IRelevanceContributor, IDeleteAll, /*IBuyable, */IBuyer, IBobAsHTML, ILinkable, IPic, IPicObjectPage, IPicHasIconObjectPage, IHasAddress
	{

		#region simple members
		/// <summary>
		/// Key
		/// </summary>
		public override int K
		{
			get { return this[Usr.Columns.K] as int? ?? 0; }
			set { this[Usr.Columns.K] = value; }
		}
		/// <summary>
		/// Email
		/// </summary>
		public override string Email
		{
			get { return (string)this[Usr.Columns.Email]; }
			set { this[Usr.Columns.Email] = value; }
		}
		/// <summary>
		/// Password
		/// </summary>
		public override string Password
		{
			get { return (string)this[Usr.Columns.Password]; }
			set { this[Usr.Columns.Password] = value; }
		}
		/// <summary>
		/// The number of times the user has logged in
		/// </summary>
		public override int LoginCount
		{
			get { return (int)this[Usr.Columns.LoginCount]; }
			set { this[Usr.Columns.LoginCount] = value; }
		}
		/// <summary>
		/// Is the user an admin user?
		/// </summary>
		public override bool IsAdmin
		{
			get { return (bool)this[Usr.Columns.IsAdmin]; }
			set { this[Usr.Columns.IsAdmin] = value; }
		}
		/// <summary>
		/// Has the email address been verified?
		/// </summary>
		public override bool IsEmailVerified
		{
			get { return (bool)this[Usr.Columns.IsEmailVerified]; }
			set { this[Usr.Columns.IsEmailVerified] = value; }
		}
		/// <summary>
		/// Cropped image 80*80 for discussion board avatar etc.
		/// </summary>
		public override Guid Pic
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Usr.Columns.Pic]); }
			set { this[Usr.Columns.Pic] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Filename of the original image
		/// </summary>
		public override Guid PicOriginal
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Usr.Columns.PicOriginal]); }
			set { this[Usr.Columns.PicOriginal] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// First name
		/// </summary>
		public override string FirstName
		{
			get { return (string)this[Usr.Columns.FirstName]; }
			set { this[Usr.Columns.FirstName] = value; }
		}
		/// <summary>
		/// Last name
		/// </summary>
		public override string LastName
		{
			get { return (string)this[Usr.Columns.LastName]; }
			set { this[Usr.Columns.LastName] = value; }
		}
		/// <summary>
		/// Nickname for the site
		/// </summary>
		public override string NickName
		{
			get { return (string)this[Usr.Columns.NickName]; }
			set { this[Usr.Columns.NickName] = value; }
		}
		/// <summary>
		/// Full mobile number including country code (e.g. 447971597702)
		/// </summary>
		public override string Mobile
		{
			get { return (string)this[Usr.Columns.Mobile]; }
			set { this[Usr.Columns.Mobile] = value; }
		}
		/// <summary>
		/// Country code of mobile number (e.g. 44)
		/// </summary>
		public override string MobileCountryCode
		{
			get { return (string)this[Usr.Columns.MobileCountryCode]; }
			set { this[Usr.Columns.MobileCountryCode] = value; }
		}
		/// <summary>
		/// Mobile number excluding country code and leading zero (e.g. 7971597702)
		/// </summary>
		public override string MobileNumber
		{
			get { return (string)this[Usr.Columns.MobileNumber]; }
			set { this[Usr.Columns.MobileNumber] = value; }
		}
		/// <summary>
		/// Send me Spotted emails
		/// </summary>
		public override bool SendSpottedEmails
		{
			get { return (bool)this[Usr.Columns.SendSpottedEmails]; }
			set { this[Usr.Columns.SendSpottedEmails] = value; }
		}
		/// <summary>
		/// Send me Spotted texts
		/// </summary>
		public override bool SendSpottedTexts
		{
			get { return (bool)this[Usr.Columns.SendSpottedTexts]; }
			set { this[Usr.Columns.SendSpottedTexts] = value; }
		}
		/// <summary>
		/// DontStayIn and its partners may send me emails about products I might like
		/// </summary>
		public override bool SendPartnerEmails
		{
			get { return (bool)this[Usr.Columns.SendPartnerEmails]; }
			set { this[Usr.Columns.SendPartnerEmails] = value; }
		}
		/// <summary>
		/// DontStayIn and its partners may send me texts about products I might like
		/// </summary>
		public override bool SendPartnerTexts
		{
			get { return (bool)this[Usr.Columns.SendPartnerTexts]; }
			set { this[Usr.Columns.SendPartnerTexts] = value; }
		}
		/// <summary>
		/// Used internally by the bulk email system
		/// </summary>
		public override string UpdateData
		{
			get { return (string)this[Usr.Columns.UpdateData]; }
			set { this[Usr.Columns.UpdateData] = value; }
		}
		/// <summary>
		/// Used internally by the bulk email system
		/// </summary>
		//public bool UpdateError
		//{
		//    get { return (bool)this[Usr.Columns.UpdateError]; }
		//    set { this[Usr.Columns.UpdateError] = value; }
		//}
		/// <summary>
		/// Admin users may add a private comment to any user
		/// </summary>
		public override string AdminNote
		{
			get { return (string)this[Usr.Columns.AdminNote]; }
			set { this[Usr.Columns.AdminNote] = value; }
		}
		/// <summary>
		/// DateTime when the user first signed-up
		/// </summary>
		public override DateTime DateTimeSignUp
		{
			get { return (DateTime)this[Usr.Columns.DateTimeSignUp]; }
			set { this[Usr.Columns.DateTimeSignUp] = value; }
		}
		/// <summary>
		/// DateTime when the user last logged in
		/// </summary>
		public override DateTime DateTimeLastAccess
		{
			get { return (DateTime)this[Usr.Columns.DateTimeLastAccess]; }
			set { this[Usr.Columns.DateTimeLastAccess] = value; }
		}
		/// <summary>
		/// DateTime when the user last requested a page
		/// </summary>
		public override DateTime DateTimeLastPageRequest
		{
			get { return (DateTime)this[Usr.Columns.DateTimeLastPageRequest]; }
			set { this[Usr.Columns.DateTimeLastPageRequest] = value; }
		}
		///<summary>
		/// Used internally to store a serialised hashtable of quick preferences
		/// </summary>
		public override string PrefsText
		{
			get { return (string)this[Usr.Columns.PrefsText]; }
			set { this[Usr.Columns.PrefsText] = value; }
		}
		/// <summary>
		/// Used to verify user on automated login
		/// </summary>
		public override string LoginString
		{
			get { return (string)this[Usr.Columns.LoginString]; }
			set { this[Usr.Columns.LoginString] = value; }
		}
		/// <summary>
		/// Users can edit this themselves
		/// </summary>
		public override string PersonalStatement
		{
			get { return (string)this[Usr.Columns.PersonalStatement]; }
			set { this[Usr.Columns.PersonalStatement] = value; }
		}
		/// <summary>
		/// If this usr was added to the system be email by a logged in usr, this is the owner
		/// </summary>
		public override int AddedByUsrK
		{
			get { return (int)this[Usr.Columns.AddedByUsrK]; }
			set { addedByUsr = null; this[Usr.Columns.AddedByUsrK] = value; }
		}
		/// <summary>
		/// Level of admin trust (0=Nothing, 1=Chat, 2=Photo, 3=Event
		/// </summary>
		public override AdminLevels AdminLevel
		{
			get { return (AdminLevels)this[Usr.Columns.AdminLevel]; }
			set { this[Usr.Columns.AdminLevel] = value; }
		}
		/// <summary>
		/// Random float less than 1 used for fast pseudo-random ordering
		/// </summary>
		public override double RandomNumber
		{
			get { return (double)this[Usr.Columns.RandomNumber]; }
			set { this[Usr.Columns.RandomNumber] = value; }
		}
		/// <summary>
		/// Date / time of the last private comment that was posted to this user
		/// </summary>
		public override DateTime LastPrivateComment
		{
			get { return (DateTime)this[Usr.Columns.LastPrivateComment]; }
			set { this[Usr.Columns.LastPrivateComment] = value; }
		}
		/// <summary>
		/// Date / time of the last private chat message that was posted to this user
		/// </summary>
		public override DateTime LastPrivateChatMessage
		{
			get { return (DateTime)this[Usr.Columns.LastPrivateChatMessage]; }
			set { this[Usr.Columns.LastPrivateChatMessage] = value; }
		}
		/// <summary>
		/// Is the user single? If so, they may participate in the DSI Date section
		/// </summary>
		public override bool IsSingle
		{
			get { return (bool)this[Usr.Columns.IsSingle]; }
			set { this[Usr.Columns.IsSingle] = value; }
		}
		/// <summary>
		/// Is the user male?
		/// </summary>
		public override bool IsMale
		{
			get { return (bool)this[Usr.Columns.IsMale]; }
			set { this[Usr.Columns.IsMale] = value; }
		}
		/// <summary>
		/// Is the user female?
		/// </summary>
		public override bool IsFemale
		{
			get { return (bool)this[Usr.Columns.IsFemale]; }
			set { this[Usr.Columns.IsFemale] = value; }
		}
		/// <summary>
		/// Date of birth
		/// </summary>
		public override DateTime DateOfBirth
		{
			get { return (DateTime)this[Usr.Columns.DateOfBirth]; }
			set { this[Usr.Columns.DateOfBirth] = value; }
		}
		/// <summary>
		/// Is this user looking for a male as a date?
		/// </summary>
		public override bool DateSexMale
		{
			get { return (bool)this[Usr.Columns.DateSexMale]; }
			set { this[Usr.Columns.DateSexMale] = value; }
		}
		/// <summary>
		/// Is this user looking for a female as a date?
		/// </summary>
		public override bool DateSexFemale
		{
			get { return (bool)this[Usr.Columns.DateSexFemale]; }
			set { this[Usr.Columns.DateSexFemale] = value; }
		}
		/// <summary>
		/// Lower acceptable age range for date
		/// </summary>
		public override int DateAgeRangeLow
		{
			get { return (int)this[Usr.Columns.DateAgeRangeLow]; }
			set { this[Usr.Columns.DateAgeRangeLow] = value; }
		}
		/// <summary>
		/// Upper acceptable age range for date
		/// </summary>
		public override int DateAgeRangeHigh
		{
			get { return (int)this[Usr.Columns.DateAgeRangeHigh]; }
			set { this[Usr.Columns.DateAgeRangeHigh] = value; }
		}
		/// <summary>
		/// Is this user looking for relationship 1 (Just new friends)
		/// </summary>
		public override bool Relationship1
		{
			get { return (bool)this[Usr.Columns.Relationship1]; }
			set { this[Usr.Columns.Relationship1] = value; }
		}
		/// <summary>
		/// Is this user looking for relationship 2 (A bit of a fling)
		/// </summary>
		public override bool Relationship2
		{
			get { return (bool)this[Usr.Columns.Relationship2]; }
			set { this[Usr.Columns.Relationship2] = value; }
		}
		/// <summary>
		/// Is this user looking for relationship 3 (Love)
		/// </summary>
		public override bool Relationship3
		{
			get { return (bool)this[Usr.Columns.Relationship3]; }
			set { this[Usr.Columns.Relationship3] = value; }
		}
		/// <summary>
		/// This is a non-authoritave sex for this user, based on another user clicking the "This isn't a xxx!!!" button
		/// </summary>
		public override bool SexHelperMale
		{
			get { return (bool)this[Usr.Columns.SexHelperMale]; }
			set { this[Usr.Columns.SexHelperMale] = value; }
		}
		/// <summary>
		/// This is a non-authoritave sex for this user, based on another user clicking the "This isn't a xxx!!!" button
		/// </summary>
		public override bool SexHelperFemale
		{
			get { return (bool)this[Usr.Columns.SexHelperFemale]; }
			set { this[Usr.Columns.SexHelperFemale] = value; }
		}
		/// <summary>
		/// Number of full buddies this user is connected to
		/// </summary>
		public override int BuddyCount
		{
			get { return (int)this[Usr.Columns.BuddyCount]; }
			set { this[Usr.Columns.BuddyCount] = value; }
		}
		/// <summary>
		/// Number of full chat messages this user has posted
		/// </summary>
		public override int ChatMessageCount
		{
			get { return (int)this[Usr.Columns.ChatMessageCount]; }
			set { this[Usr.Columns.ChatMessageCount] = value; }
		}
		/// <summary>
		/// Number of comments user has posted
		/// </summary>
		public override int CommentCount
		{
			get { return (int)this[Usr.Columns.CommentCount]; }
			set { this[Usr.Columns.CommentCount] = value; }
		}
		/// <summary>
		/// Number of events this user has added
		/// </summary>
		public override int EventCount
		{
			get { return (int)this[Usr.Columns.EventCount]; }
			set { this[Usr.Columns.EventCount] = value; }
		}
		/// <summary>
		/// The users home town
		/// </summary>
		public override int HomePlaceK
		{
			get { return (int) this[Usr.Columns.HomePlaceK]; }
			set { home = null; this[Usr.Columns.HomePlaceK] = value; }
		}
		/// <summary>
		/// Has this user agreed to the terms and conditions?
		/// </summary>
		public override bool AgreeTerms
		{
			get { return (bool)this[Usr.Columns.AgreeTerms]; }
			set { this[Usr.Columns.AgreeTerms] = value; }
		}
		/// <summary>
		/// Does this user map to a client in the text-guest system? If so this is the GuestClientK, if not, then zero.
		/// </summary>
		public override int GuestClientK
		{
			get { return (int)this[Usr.Columns.GuestClientK]; }
			set { this[Usr.Columns.GuestClientK] = value; }
		}
		/// <summary>
		/// Favourite music style (main category).
		/// </summary>
		public override int FavouriteMusicTypeK
		{
			get { return (int)this[Usr.Columns.FavouriteMusicTypeK]; }
			set { favouriteMusicType = null; this[Usr.Columns.FavouriteMusicTypeK] = value; }
		}
		/// <summary>
		/// Total number of times this user has laughed at a comment
		/// </summary>
		public override int TotalLol
		{
			get { return (int)this[Usr.Columns.TotalLol]; }
			set { this[Usr.Columns.TotalLol] = value; }
		}
		/// <summary>
		/// Total number of times this user has made someone laugh
		/// </summary>
		public override int TotalMadeLol
		{
			get { return (int)this[Usr.Columns.TotalMadeLol]; }
			set { this[Usr.Columns.TotalMadeLol] = value; }
		}
		/// <summary>
		/// DateTime of the last time this user laughed
		/// </summary>
		public override DateTime LastLol
		{
			get { return (DateTime)this[Usr.Columns.LastLol]; }
			set { this[Usr.Columns.LastLol] = value; }
		}
		/// <summary>
		/// Total number of users that this user has made laugh
		/// </summary>
		public override int UniqueMadeLol
		{
			get { return (int)this[Usr.Columns.UniqueMadeLol]; }
			set { this[Usr.Columns.UniqueMadeLol] = value; }
		}
		/// <summary>
		/// Chat messages
		/// </summary>
		public override string ChatXml
		{
			get { return (string)this[Usr.Columns.ChatXml]; }
			set { this[Usr.Columns.ChatXml] = value; }
		}
		/// <summary>
		/// Is this user logged on? (This is only set to false when the user actually logs off)
		/// </summary>
		public override bool IsLoggedOn
		{
			get { return (bool)this[Usr.Columns.IsLoggedOn]; }
			set { this[Usr.Columns.IsLoggedOn] = value; }
		}
		/// <summary>
		/// The ticks of the last chat item
		/// </summary>
		public override long LastChatItem
		{
			get { return (long)this[Usr.Columns.LastChatItem]; }
			set { this[Usr.Columns.LastChatItem] = value; }
		}
		/// <summary>
		/// Last IP that the user accessed the site from
		/// </summary>
		public override string LastIp
		{
			get { return (string)this[Usr.Columns.LastIp]; }
			set { this[Usr.Columns.LastIp] = value; }
		}
		/// <summary>
		/// Ignore - this hides chat messages from this user / doesn't send out comment alert emails etc.
		/// </summary>
		public override bool Ignore
		{
			get { return (bool)this[Usr.Columns.Ignore]; }
			set { this[Usr.Columns.Ignore] = value; }
		}
		/// <summary>
		/// Is this user a pro spotter?
		/// </summary>
		public override bool IsProSpotter
		{
			get { return (bool)this[Usr.Columns.IsProSpotter]; }
			set { this[Usr.Columns.IsProSpotter] = value; }
		}
		/// <summary>
		/// ID of the last invite that was sent to this user. Ensures we don't send duplicate event invite PM's to people.
		/// </summary>
		public override int LastInvite
		{
			get { return (int)this[Usr.Columns.LastInvite]; }
			set { this[Usr.Columns.LastInvite] = value; }
		}
		/// <summary>
		/// Who was the first person to take a photo of this user?
		/// </summary>
		public override int IntroducedByUsrK
		{
			get { return (int)this[Usr.Columns.IntroducedByUsrK]; }
			set { this[Usr.Columns.IntroducedByUsrK] = value; }
		}
		/// <summary>
		/// Can we send this user e-flyers?
		/// </summary>
		public override bool SendFlyers
		{
			get { return (bool)this[Usr.Columns.SendFlyers]; }
			set { this[Usr.Columns.SendFlyers] = value; }
		}
		/// <summary>
		/// Can we send this user party invites?
		/// </summary>
		public override bool SendInvites
		{
			get { return (bool)this[Usr.Columns.SendInvites]; }
			set { this[Usr.Columns.SendInvites] = value; }
		}
		/// <summary>
		/// Total number of enabled photos this usr has added
		/// </summary>
		public override int TotalPhotoUploads
		{
			get { return (int)this[Usr.Columns.TotalPhotoUploads]; }
			set { this[Usr.Columns.TotalPhotoUploads] = value; }
		}
		/// <summary>
		/// Temp int used when sending bulk stuff
		/// </summary>
		public override int TempInt
		{
			get { return (int)this[Usr.Columns.TempInt]; }
			set { this[Usr.Columns.TempInt] = value; }
		}
		/// <summary>
		/// Ask the user for their password on each auto-login?
		/// </summary>
		public override bool EnhancedSecurity
		{
			get { return (bool)this[Usr.Columns.EnhancedSecurity]; }
			set { this[Usr.Columns.EnhancedSecurity] = value; }
		}
		/// <summary>
		/// Address - Street
		/// </summary>
		public override string AddressStreet
		{
			get { return (string)this[Usr.Columns.AddressStreet]; }
			set { this[Usr.Columns.AddressStreet] = value; }
		}
		/// <summary>
		/// Address - Area
		/// </summary>
		public override string AddressArea
		{
			get { return (string)this[Usr.Columns.AddressArea]; }
			set { this[Usr.Columns.AddressArea] = value; }
		}
		/// <summary>
		/// Address - Place
		/// </summary>
		public override string AddressTown
		{
			get { return (string)this[Usr.Columns.AddressTown]; }
			set { this[Usr.Columns.AddressTown] = value; }
		}
		/// <summary>
		/// Address - County
		/// </summary>
		public override string AddressCounty
		{
			get { return (string)this[Usr.Columns.AddressCounty]; }
			set { this[Usr.Columns.AddressCounty] = value; }
		}
		/// <summary>
		/// Address - Postcode
		/// </summary>
		public override string AddressPostcode
		{
			get { return (string)this[Usr.Columns.AddressPostcode]; }
			set { this[Usr.Columns.AddressPostcode] = value; }
		}
		/// <summary>
		/// Address - Country (link to Country table)
		/// </summary>
		public override int AddressCountryK
		{
			get { return (int)this[Usr.Columns.AddressCountryK]; }
			set { addressCountry = null; this[Usr.Columns.AddressCountryK] = value; }
		}
		/// <summary>
		/// New=3, WelcomePackInPost=4, HaveCards=0, NeedCards=1, CardsInPost=2
		/// </summary>
		public override CardStatusEnum CardStatus
		{
			get { return (CardStatusEnum)this[Usr.Columns.CardStatus]; }
			set { this[Usr.Columns.CardStatus] = value; }
		}
		/// <summary>
		/// Tracks the total number of cards that we've sent to this spotter.
		/// </summary>
		public override int TotalCardsSent
		{
			get { return (int)this[Usr.Columns.TotalCardsSent]; }
			set { this[Usr.Columns.TotalCardsSent] = value; }
		}
		/// <summary>
		/// Is this user a spotter?
		/// </summary>
		public override bool IsSpotter
		{
			get { return (bool)this[Usr.Columns.IsSpotter]; }
			set { this[Usr.Columns.IsSpotter] = value; }
		}
		/// <summary>
		/// Is this user banned?
		/// </summary>
		public override bool Banned
		{
			get { return (bool)this[Usr.Columns.Banned]; }
			set { this[Usr.Columns.Banned] = value; }
		}
		/// <summary>
		/// Who banned this user?
		/// </summary>
		public override int BannedByUsrK
		{
			get { return (int)this[Usr.Columns.BannedByUsrK]; }
			set { bannedByUsr = null; this[Usr.Columns.BannedByUsrK] = value; }
		}
		/// <summary>
		/// When were they banned?
		/// </summary>
		public override DateTime BannedDateTime
		{
			get { return (DateTime)this[Usr.Columns.BannedDateTime]; }
			set { this[Usr.Columns.BannedDateTime] = value; }
		}
		/// <summary>
		/// Why were they banned?
		/// </summary>
		public override string BannedReason
		{
			get { return (string)this[Usr.Columns.BannedReason]; }
			set { this[Usr.Columns.BannedReason] = value; }
		}
		/// <summary>
		/// Send generic music types in the update email?
		/// </summary>
		public override bool UpdateSendGenericMusic
		{
			get { return (bool)this[Usr.Columns.UpdateSendGenericMusic]; }
			set { this[Usr.Columns.UpdateSendGenericMusic] = value; }
		}
		/// <summary>
		/// Include large events in other parts of the country in the update email?
		/// </summary>
		public override int UpdateLargeEvents
		{
			get { return (int)this[Usr.Columns.UpdateLargeEvents]; }
			set { this[Usr.Columns.UpdateLargeEvents] = value; }
		}
		/// <summary>
		/// Include all events that my buddies are going to in the update email?
		/// </summary>
		public override bool UpdateSendBuddies
		{
			get { return (bool)this[Usr.Columns.UpdateSendBuddies]; }
			set { this[Usr.Columns.UpdateSendBuddies] = value; }
		}
		/// <summary>
		/// State var used to reconstruct cropper when re-cropping
		/// </summary>
		public override string PicState
		{
			get { return (string)this[Usr.Columns.PicState]; }
			set { this[Usr.Columns.PicState] = value; }
		}
		/// <summary>
		/// The Photo that was used to create the Pic.
		/// </summary>
		public override int PicPhotoK
		{
			get { return (int)this[Usr.Columns.PicPhotoK]; }
			set { picPhoto = null; this[Usr.Columns.PicPhotoK] = value; }
		}
		/// <summary>
		/// The Misc that was used to create the Pic.
		/// </summary>
		public override int PicMiscK
		{
			get { return (int)this[Usr.Columns.PicMiscK]; }
			set { picMisc = null; this[Usr.Columns.PicMiscK] = value; }
		}
		/// <summary>
		/// Is the live chat box turned on?
		/// </summary>
		public override bool IsChatting
		{
			get { return (bool)this[Usr.Columns.IsChatting]; }
			set { this[Usr.Columns.IsChatting] = value; }
		}
		/// <summary>
		/// When was the last time this user gained / lost a buddy? - used when caching the buddies drop-down on the photo page
		/// </summary>
		public override DateTime LastBuddyChange
		{
			get { return (DateTime)this[Usr.Columns.LastBuddyChange]; }
			set { this[Usr.Columns.LastBuddyChange] = value; }
		}
		/// <summary>
		/// News moderator - they can moderate news
		/// </summary>
		public override bool NewsModerator
		{
			get { return (bool)this[Usr.Columns.NewsModerator]; }
			set { this[Usr.Columns.NewsModerator] = value; }
		}
		/// <summary>
		/// News permission - they can post news up to this level
		/// </summary>
		public override int NewsPermissionLevel
		{
			get { return (int)this[Usr.Columns.NewsPermissionLevel]; }
			set { this[Usr.Columns.NewsPermissionLevel] = value; }
		}
		/// <summary>
		/// Is this user a beta tester?
		/// </summary>
		public override bool IsBetaTester
		{
			get { return (bool)this[Usr.Columns.IsBetaTester]; }
			set { this[Usr.Columns.IsBetaTester] = value; }
		}
		/// <summary>
		/// How many places does this user visit?
		/// </summary>
		public override int PlacesVisitCount
		{
			get { return (int)this[Usr.Columns.PlacesVisitCount]; }
			set { this[Usr.Columns.PlacesVisitCount] = value; }
		}
		/// <summary>
		/// How many places does this user visit?
		/// </summary>
		public override int MusicTypesFavouriteCount
		{
			get { return (int)this[Usr.Columns.MusicTypesFavouriteCount]; }
			set { this[Usr.Columns.MusicTypesFavouriteCount] = value; }
		}
		/// <summary>
		/// How many places does this user visit?
		/// </summary>
		public override int PhotosMeCount
		{
			get { return (int)this[Usr.Columns.PhotosMeCount]; }
			set { this[Usr.Columns.PhotosMeCount] = value; }
		}
		/// <summary>
		/// How many places does this user visit?
		/// </summary>
		public override bool IsSkeleton
		{
			get { return (bool)this[Usr.Columns.IsSkeleton]; }
			set { this[Usr.Columns.IsSkeleton] = value; }
		}
		/// <summary>
		/// Don't send inbox emails?
		/// </summary>
		public override bool NoInboxEmails
		{
			get { return (bool)this[Usr.Columns.NoInboxEmails]; }
			set { this[Usr.Columns.NoInboxEmails] = value; }
		}
		/// <summary>
		/// Number of abuse reports this user has submitted that are pending
		/// </summary>
		public override int AbuseReportsPending
		{
			get { return (int)this[Usr.Columns.AbuseReportsPending]; }
			set { this[Usr.Columns.AbuseReportsPending] = value; }
		}
		/// <summary>
		/// Number of abuse reports this user has submitted that were useful (abuse or noabuse)
		/// </summary>
		public override int AbuseReportsUseful
		{
			get { return (int)this[Usr.Columns.AbuseReportsUseful]; }
			set { this[Usr.Columns.AbuseReportsUseful] = value; }
		}
		/// <summary>
		/// Number of abuse reports this user has submitted that were overturned
		/// </summary>
		public override int AbuseReportsOverturned
		{
			get { return (int)this[Usr.Columns.AbuseReportsOverturned]; }
			set { this[Usr.Columns.AbuseReportsOverturned] = value; }
		}
		/// <summary>
		/// Number of abuse accusations this user has had made against them that are pending
		/// </summary>
		public override int AbuseAccusationsPending
		{
			get { return (int)this[Usr.Columns.AbuseAccusationsPending]; }
			set { this[Usr.Columns.AbuseAccusationsPending] = value; }
		}
		/// <summary>
		/// Number of abuse accusations this user has had made against them that are were found to be abuse
		/// </summary>
		public override int AbuseAccusationsAbuse
		{
			get { return (int)this[Usr.Columns.AbuseAccusationsAbuse]; }
			set { this[Usr.Columns.AbuseAccusationsAbuse] = value; }
		}
		/// <summary>
		/// Number of abuse accusations this user has had made against them that are were found to not be abusive
		/// </summary>
		public override int AbuseAccusationsNoAbuse
		{
			get { return (int)this[Usr.Columns.AbuseAccusationsNoAbuse]; }
			set { this[Usr.Columns.AbuseAccusationsNoAbuse] = value; }
		}
		/// <summary>
		/// Should this users photos be moderated prior to going live?
		/// </summary>
		public override bool ModeratePhotos
		{
			get { return (bool)this[Usr.Columns.ModeratePhotos]; }
			set { this[Usr.Columns.ModeratePhotos] = value; }
		}
		/// <summary>
		/// Id used for stopping multiple browser pages hogging chat bandwidth
		/// </summary>
		public override int ChatSessionId
		{
			get { return (int)this[Usr.Columns.ChatSessionId]; }
			set { this[Usr.Columns.ChatSessionId] = value; }
		}
		/// <summary>
		/// If the group invite page was used to add this user, this is the inviting group
		/// </summary>
		public override int AddedByGroupK
		{
			get { return (int)this[Usr.Columns.AddedByGroupK]; }
			set { addedByGroup = null; this[Usr.Columns.AddedByGroupK] = value; }
		}
		/// <summary>
		/// Which icon is displayed? 0=None, 1=Duck, 2=Penguin
		/// </summary>
		public override int DonateIcon
		{
			get { return (int)this[Usr.Columns.DonateIcon]; }
			set { this[Usr.Columns.DonateIcon] = value; }
		}
		/// <summary>
		/// When does the icon expire?
		/// </summary>
		public override DateTime DonateExpire
		{
			get { return (DateTime)this[Usr.Columns.DonateExpire]; }
			set { this[Usr.Columns.DonateExpire] = value; }
		}
		/// <summary>
		/// Date/time when the email was last changed
		/// </summary>
		public override DateTime EmailDateTime
		{
			get { return (DateTime)this[Usr.Columns.EmailDateTime]; }
			set { this[Usr.Columns.EmailDateTime] = value; }
		}
		/// <summary>
		/// Ip of the client when the email address was entered
		/// </summary>
		public override string EmailIp
		{
			get { return (string)this[Usr.Columns.EmailIp]; }
			set { this[Usr.Columns.EmailIp] = value; }
		}
		/// <summary>
		/// Is the user unsubscribed from emails?
		/// </summary>
		public override bool EmailHold
		{
			get { return (bool)this[Usr.Columns.EmailHold]; }
			set { this[Usr.Columns.EmailHold] = value; }
		}
		/// <summary>
		/// Can this user edit plain HTML?
		/// </summary>
		public override bool IsHtmlEditor
		{
			get { return (bool)this[Usr.Columns.IsHtmlEditor]; }
			set { this[Usr.Columns.IsHtmlEditor] = value; }
		}
		/// <summary>
		/// Is this user a moderator of any groups?
		/// </summary>
		public override bool IsGroupModerator
		{
			get { return (bool)this[Usr.Columns.IsGroupModerator]; }
			set { this[Usr.Columns.IsGroupModerator] = value; }
		}
		/// <summary>
		/// Is this skeleton user from the sign-up page - e.g. Entered their email address and password ONCE.
		/// </summary>
		public override bool IsSkeletonFromSignup
		{
			get { return (bool)this[Usr.Columns.IsSkeletonFromSignup]; }
			set { this[Usr.Columns.IsSkeletonFromSignup] = value; }
		}
		/// <summary>
		/// Extra icon displayed on the profile
		/// </summary>
		public override int ExtraIcon
		{
			get { return (int)this[Usr.Columns.ExtraIcon]; }
			set { this[Usr.Columns.ExtraIcon] = value; }
		}
		/// <summary>
		/// When does the extra icon expire?
		/// </summary>
		public override DateTime ExtraExpire
		{
			get { return (DateTime)this[Usr.Columns.ExtraExpire]; }
			set { this[Usr.Columns.ExtraExpire] = value; }
		}
		/// <summary>
		/// Total number of distinct users I have taken a photo of
		/// </summary>
		public override int SpottingsTotal
		{
			get { return (int)this[Usr.Columns.SpottingsTotal]; }
			set { this[Usr.Columns.SpottingsTotal] = value; }
		}
		/// <summary>
		/// Total number of distinct users I have taken a photo of in the last month (updated hourly?)
		/// </summary>
		public override int SpottingsMonth
		{
			get { return (int)this[Usr.Columns.SpottingsMonth]; }
			set { this[Usr.Columns.SpottingsMonth] = value; }
		}
		/// <summary>
		/// Ranking in bussiest spotters list (month only)
		/// </summary>
		public override int SpottingsMonthRank
		{
			get { return (int)this[Usr.Columns.SpottingsMonthRank]; }
			set { this[Usr.Columns.SpottingsMonthRank] = value; }
		}
		///// <summary>
		///// Is the first donation icon displayed?
		///// </summary>
		//public bool Donate1Icon
		//{
		//    get { return (bool)this[Usr.Columns.Donate1Icon]; }
		//    set { this[Usr.Columns.Donate1Icon] = value; }
		//}
		///// <summary>
		///// When does the first donation icon expire?
		///// </summary>
		//public DateTime Donate1Expire
		//{
		//    get { return (DateTime)this[Usr.Columns.Donate1Expire]; }
		//    set { this[Usr.Columns.Donate1Expire] = value; }
		//}
		///// <summary>
		///// Is the second donation icon displayed?
		///// </summary>
		//public bool Donate2Icon
		//{
		//    get { return (bool)this[Usr.Columns.Donate2Icon]; }
		//    set { this[Usr.Columns.Donate2Icon] = value; }
		//}
		///// <summary>
		///// When does the second donation icon expire?
		///// </summary>
		//public DateTime Donate2Expire
		//{
		//    get { return (DateTime)this[Usr.Columns.Donate2Expire]; }
		//    set { this[Usr.Columns.Donate2Expire] = value; }
		//}
		/// <summary>
		/// Is this user part of a promoter account?
		/// </summary>
		public override bool IsPromoter
		{
			get { return (bool)this[Usr.Columns.IsPromoter]; }
			set { this[Usr.Columns.IsPromoter] = value; }
		}
		/// <summary>
		/// Number of Camp DSI tickets this user has bought
		/// </summary>
		public override int CampTickets
		{
			get { return (int)this[Usr.Columns.CampTickets]; }
			set { this[Usr.Columns.CampTickets] = value; }
		}
		/// <summary>
		/// Has this user bought a dsi ticket?
		/// </summary>
		public override bool HasTicket
		{
			get { return (bool)this[Usr.Columns.HasTicket]; }
			set { this[Usr.Columns.HasTicket] = value; }
		}
		/// <summary>
		/// What's the date/time of the last event the member has a ticket for?
		/// </summary>
		public override DateTime LastTicketEventDateTime
		{
			get { return (DateTime)this[Usr.Columns.LastTicketEventDateTime]; }
			set { this[Usr.Columns.LastTicketEventDateTime] = value; }
		}
		/// <summary>
		/// Cryptographic hash of the password
		/// </summary>
		public override Guid PasswordHash
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Usr.Columns.PasswordHash]); }
			set { this[Usr.Columns.PasswordHash] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Cryptographic salt used to generate password hash
		/// </summary>
		public override Guid PasswordSalt
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Usr.Columns.PasswordSalt]); }
			set { this[Usr.Columns.PasswordSalt] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Secret data used to verify password reset page
		/// </summary>
		public override string PasswordResetEmailSecret
		{
			get { return (string)this[Usr.Columns.PasswordResetEmailSecret]; }
			set { this[Usr.Columns.PasswordResetEmailSecret] = value; }
		}
		/// <summary>
		/// Has this user agreed to the new terms of use
		/// </summary>
		public override bool LegalTermsUser1
		{
			get { return (bool)this[Usr.Columns.LegalTermsUser1]; }
			set { this[Usr.Columns.LegalTermsUser1] = value; }
		}
		/// <summary>
		/// Has this user agreed to the new terms for promoters
		/// </summary>
		public override bool LegalTermsPromoter1
		{
			get { return (bool)this[Usr.Columns.LegalTermsPromoter1]; }
			set { this[Usr.Columns.LegalTermsPromoter1] = value; }
		}
		/// <summary>
		/// Is the user a super admin user?
		/// </summary>
		public override bool IsSuperAdmin
		{
			get { return (bool)this[Usr.Columns.IsSuperAdmin]; }
			set { this[Usr.Columns.IsSuperAdmin] = value; }
		}

		/// <summary>
		/// Is the user a sales person?
		/// </summary>
		public override bool IsSalesPerson
		{
			get { return (bool)this[Usr.Columns.IsSalesPerson]; }
			set { this[Usr.Columns.IsSalesPerson] = value; }
		}
		/// <summary>
		/// Time stamp to record when someone is trying to purchase an IBuyable item that is linked to this Bob.
		/// </summary>
		public override DateTime BuyableLockDateTime
		{
			get { return (DateTime)this[Usr.Columns.BuyableLockDateTime]; }
			set { this[Usr.Columns.BuyableLockDateTime] = value; }
		}
		/// <summary>
		/// For sales people: their sales team number
		/// </summary>
		public override int SalesTeam
		{
			get { return (int)this[Usr.Columns.SalesTeam]; }
			set { this[Usr.Columns.SalesTeam] = value; }
		}
		/// <summary>
		/// A guid identifier for the Usr
		/// </summary>
		public override Guid Guid
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Usr.Columns.Guid]); }
			set { this[Usr.Columns.Guid] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Has the user agreed to the new (2007-06) user terms?
		/// </summary>
		public override bool LegalTermsUser2
		{
			get { return (bool)this[Usr.Columns.LegalTermsUser2]; }
			set { this[Usr.Columns.LegalTermsUser2] = value; }
		}
		/// <summary>
		/// Has the user agreed to the new (2007-06) promoter terms?
		/// </summary>
		public override bool LegalTermsPromoter2
		{
			get { return (bool)this[Usr.Columns.LegalTermsPromoter2]; }
			set { this[Usr.Columns.LegalTermsPromoter2] = value; }
		}
		/// <summary>
		/// Datetime of this users last photo upload
		/// </summary>
		public override DateTime LastPhotoUpload
		{
			get { return (DateTime)this[Usr.Columns.LastPhotoUpload]; }
			set { this[Usr.Columns.LastPhotoUpload] = value; }
		}
		/// <summary>
		/// Date/time of the tuesday that the last update email that was delivered to this user. Used to make th
		/// </summary>
		public override DateTime DateTimeLastUpdateEmail
		{
			get { return (DateTime)this[Usr.Columns.DateTimeLastUpdateEmail]; }
			set { this[Usr.Columns.DateTimeLastUpdateEmail] = value; }
		}
		/// <summary>
		/// Was this usr invited using the contact importer device?
		/// </summary>
		public override bool InvitedViaContactImporter
		{
			get { return (bool)this[Usr.Columns.InvitedViaContactImporter]; }
			set { this[Usr.Columns.InvitedViaContactImporter] = value; }
		}
		/// <summary>
		/// Has the user registered through a styled tickets microsite?
		/// </summary>
		public override bool IsTicketsRegistered
		{
			get { return (bool)this[Usr.Columns.IsTicketsRegistered]; }
			set { this[Usr.Columns.IsTicketsRegistered] = value; }
		}
		/// <summary>
		/// Disallow this usr's profile to be found by searching name or email address?
		/// </summary>
		public override bool ExDirectory
		{
			get { return (bool)this[Usr.Columns.ExDirectory]; }
			set { this[Usr.Columns.ExDirectory] = value; }
		}
		/// <summary>
		/// Did a recent email to this user suffer a hard bounce?
		/// </summary>
		public override bool IsEmailBroken
		{
			get { return (bool)this[Usr.Columns.IsEmailBroken]; }
			set { this[Usr.Columns.IsEmailBroken] = value; }
		}
		/// <summary>
		/// When was the last chat message posted?
		/// </summary>
		public override DateTime? DateTimeLastChatMessage
		{
			get { return (DateTime?)this[Usr.Columns.DateTimeLastChatMessage]; }
			set { this[Usr.Columns.DateTimeLastChatMessage] = value; }
		}
		/// <summary>
		/// The Donation Icon, if applicable, which the user will have appear in their rollover
		/// </summary>
		public override int? RolloverDonationIconK
		{
			get { return (int?)this[Usr.Columns.RolloverDonationIconK]; }
			set { this[Usr.Columns.RolloverDonationIconK] = value; }
		}
		/// <summary>
		/// Guid of the chat pic (300px x 75px)
		/// </summary>
		public override Guid? ChatPic
		{
			get { return (Guid?)this[Usr.Columns.ChatPic]; }
			set { this[Usr.Columns.ChatPic] = value; }
		}
		/// <summary>
		/// PhotoK that the chat pic was cropped from
		/// </summary>
		public override int? ChatPicPhotoK
		{
			get { return (int?)this[Usr.Columns.ChatPicPhotoK]; }
			set { this[Usr.Columns.ChatPicPhotoK] = value; }
		}
		/// <summary>
		/// State of the cropper for the chat pic
		/// </summary>
		public override string ChatPicState
		{
			get { return (string)this[Usr.Columns.ChatPicState]; }
			set { this[Usr.Columns.ChatPicState] = value; }
		}
		/// <summary>
		/// Date / time that the buddy alerts room was last refreshed
		/// </summary>
		public override DateTime? DateTimeLastBuddyAlertsRoomRefresh
		{
			get { return (DateTime?)this[Usr.Columns.DateTimeLastBuddyAlertsRoomRefresh]; }
			set { this[Usr.Columns.DateTimeLastBuddyAlertsRoomRefresh] = value; }
		}
		/// <summary>
		/// Photo usage permission
		/// </summary>
		public override PhotoUsageEnum PhotoUsage
		{
			get { return (PhotoUsageEnum)this[Usr.Columns.PhotoUsage]; }
			set { this[Usr.Columns.PhotoUsage] = value; }
		}
		/// <summary>
		/// Facebook user id
		/// </summary>
		public override long? FacebookUID
		{
			get { return (long?)this[Usr.Columns.FacebookUID]; }
			set { this[Usr.Columns.FacebookUID] = value; }
		}
		/// <summary>
		/// Facebook connect linked
		/// </summary>
		public override bool FacebookConnected
		{
			get { return (bool)this[Usr.Columns.FacebookConnected]; }
			set { this[Usr.Columns.FacebookConnected] = value; }
		}
		/// <summary>
		/// Facebook connect linked date/time
		/// </summary>
		public override DateTime? FacebookConnectedDateTime
		{
			get { return (DateTime?)this[Usr.Columns.FacebookConnectedDateTime]; }
			set { this[Usr.Columns.FacebookConnectedDateTime] = value; }
		}
		/// <summary>
		/// Facebook email extended permission
		/// </summary>
		public override bool FacebookPermissionEmail
		{
			get { return (bool)this[Usr.Columns.FacebookPermissionEmail]; }
			set { this[Usr.Columns.FacebookPermissionEmail] = value; }
		}
		/// <summary>
		/// Facebook publish_stream extended permission
		/// </summary>
		public override bool FacebookPermissionPublish
		{
			get { return (bool)this[Usr.Columns.FacebookPermissionPublish]; }
			set { this[Usr.Columns.FacebookPermissionPublish] = value; }
		}
		/// <summary>
		/// Facebook create_event extended permission
		/// </summary>
		public override bool FacebookPermissionEvent
		{
			get { return (bool)this[Usr.Columns.FacebookPermissionEvent]; }
			set { this[Usr.Columns.FacebookPermissionEvent] = value; }
		}
		/// <summary>
		/// Facebook rsvp_event extended permission
		/// </summary>
		public override bool FacebookPermissionRsvp
		{
			get { return (bool)this[Usr.Columns.FacebookPermissionRsvp]; }
			set { this[Usr.Columns.FacebookPermissionRsvp] = value; }
		}
		/// <summary>
		/// Post a facebook stream story when I attend events
		/// </summary>
		public override bool FacebookStoryAttendEvent
		{
			get { return (bool)this[Usr.Columns.FacebookStoryAttendEvent]; }
			set { this[Usr.Columns.FacebookStoryAttendEvent] = value; }
		}
		/// <summary>
		/// Post a facebook stream story when I buy tickets
		/// </summary>
		public override bool FacebookStoryBuyTicket
		{
			get { return (bool)this[Usr.Columns.FacebookStoryBuyTicket]; }
			set { this[Usr.Columns.FacebookStoryBuyTicket] = value; }
		}
		/// <summary>
		/// Post a facebook stream story when I upload photos
		/// </summary>
		public override bool FacebookStoryUploadPhoto
		{
			get { return (bool)this[Usr.Columns.FacebookStoryUploadPhoto]; }
			set { this[Usr.Columns.FacebookStoryUploadPhoto] = value; }
		}
		/// <summary>
		/// Post a facebook stream story when I get spotted in photos
		/// </summary>
		public override bool FacebookStorySpotted
		{
			get { return (bool)this[Usr.Columns.FacebookStorySpotted]; }
			set { this[Usr.Columns.FacebookStorySpotted] = value; }
		}
		/// <summary>
		/// Post a facebook stream story when I have a photo featured on the front page
		/// </summary>
		public override bool FacebookStoryPhotoFeatured
		{
			get { return (bool)this[Usr.Columns.FacebookStoryPhotoFeatured]; }
			set { this[Usr.Columns.FacebookStoryPhotoFeatured] = value; }
		}
		/// <summary>
		/// Post a facebook stream story when I make a new buddy
		/// </summary>
		public override bool FacebookStoryNewBuddy
		{
			get { return (bool)this[Usr.Columns.FacebookStoryNewBuddy]; }
			set { this[Usr.Columns.FacebookStoryNewBuddy] = value; }
		}
		/// <summary>
		/// Post a facebook stream story when I publish an article
		/// </summary>
		public override bool FacebookStoryPublishArticle
		{
			get { return (bool)this[Usr.Columns.FacebookStoryPublishArticle]; }
			set { this[Usr.Columns.FacebookStoryPublishArticle] = value; }
		}
		/// <summary>
		/// Post a facebook stream story when I join a group
		/// </summary>
		public override bool FacebookStoryJoinGroup
		{
			get { return (bool)this[Usr.Columns.FacebookStoryJoinGroup]; }
			set { this[Usr.Columns.FacebookStoryJoinGroup] = value; }
		}
		/// <summary>
		/// Post a facebook stream story when I put photos on my favourites
		/// </summary>
		public override bool FacebookStoryFavourite
		{
			get { return (bool)this[Usr.Columns.FacebookStoryFavourite]; }
			set { this[Usr.Columns.FacebookStoryFavourite] = value; }
		}
		/// <summary>
		/// Post a facebook stream story when I put topics on my favourites
		/// </summary>
		public override bool FacebookStoryFavouriteTopic
		{
			get { return (bool)this[Usr.Columns.FacebookStoryFavouriteTopic]; }
			set { this[Usr.Columns.FacebookStoryFavouriteTopic] = value; }
		}
		/// <summary>
		/// Post a facebook stream story when I post new topics
		/// </summary>
		public override bool FacebookStoryNewTopic
		{
			get { return (bool)this[Usr.Columns.FacebookStoryNewTopic]; }
			set { this[Usr.Columns.FacebookStoryNewTopic] = value; }
		}
		/// <summary>
		/// Post a facebook stream story when I post an event review
		/// </summary>
		public override bool FacebookStoryEventReview
		{
			get { return (bool)this[Usr.Columns.FacebookStoryEventReview]; }
			set { this[Usr.Columns.FacebookStoryEventReview] = value; }
		}
		/// <summary>
		/// Post a facebook stream story when I post news
		/// </summary>
		public override bool FacebookStoryPostNews
		{
			get { return (bool)this[Usr.Columns.FacebookStoryPostNews]; }
			set { this[Usr.Columns.FacebookStoryPostNews] = value; }
		}
		/// <summary>
		/// Post a facebook stream story when I laugh at a comment
		/// </summary>
		public override bool FacebookStoryLaugh
		{
			get { return (bool)this[Usr.Columns.FacebookStoryLaugh]; }
			set { this[Usr.Columns.FacebookStoryLaugh] = value; }
		}
		/// <summary>
		/// Add an event to facebook when I add an event
		/// </summary>
		public override bool FacebookEventAdd
		{
			get { return (bool)this[Usr.Columns.FacebookEventAdd]; }
			set { this[Usr.Columns.FacebookEventAdd] = value; }
		}
		/// <summary>
		/// Add me on Facebook when I attend an event
		/// </summary>
		public override bool FacebookEventAttend
		{
			get { return (bool)this[Usr.Columns.FacebookEventAttend]; }
			set { this[Usr.Columns.FacebookEventAttend] = value; }
		}
		/// <summary>
		/// Facebook email
		/// </summary>
		public override string FacebookEmail
		{
			get { return (string)this[Usr.Columns.FacebookEmail]; }
			set { this[Usr.Columns.FacebookEmail] = value; }
		}
		/// <summary>
		/// Is this user a DJ?
		/// </summary>
		public override bool? IsDj
		{
			get { return (bool?)this[Usr.Columns.IsDj]; }
			set { this[Usr.Columns.IsDj] = value; }
		}
		/// <summary>
		/// Main Facebook Story permission - Well update your Facebook wall when you create stuff
		/// </summary>
		public override bool? FacebookStory
		{
			get { return (bool?)this[Usr.Columns.FacebookStory]; }
			set { this[Usr.Columns.FacebookStory] = value; }
		}
		/// <summary>
		/// Main Facebook Story permission - Well update your Facebook wall when you create stuff
		/// </summary>
		public override bool FacebookStory1
		{
			get { return (bool)this[Usr.Columns.FacebookStory1]; }
			set { this[Usr.Columns.FacebookStory1] = value; }
		}
		/// <summary>
		/// Access token needed for offline access
		/// </summary>
		public override string FacebookAccessToken
		{
			get { return (string)this[Usr.Columns.FacebookAccessToken]; }
			set { this[Usr.Columns.FacebookAccessToken] = value; }
		}
		/// <summary>
		/// Does this user need a captcha for security?
		/// </summary>
		public override bool? NeedsCaptcha
		{
			get { return (bool?)this[Usr.Columns.NeedsCaptcha]; }
			set { this[Usr.Columns.NeedsCaptcha] = value; }
		}
		/// <summary>
		/// Has this user correctly passed the captcha test?
		/// </summary>
		public override bool? PassedCaptcha
		{
			get { return (bool?)this[Usr.Columns.PassedCaptcha]; }
			set { this[Usr.Columns.PassedCaptcha] = value; }
		}
		/// <summary>
		/// Date/time of the start of the month that bounce mails are being counted
		/// </summary>
		public override DateTime? BouncePeriodDateTime
		{
			get { return (DateTime?)this[Usr.Columns.BouncePeriodDateTime]; }
			set { this[Usr.Columns.BouncePeriodDateTime] = value; }
		}
		/// <summary>
		/// Total emails sent in the bounce mail period
		/// </summary>
		public override int? TotalEmailsSentInPeriod
		{
			get { return (int?)this[Usr.Columns.TotalEmailsSentInPeriod]; }
			set { this[Usr.Columns.TotalEmailsSentInPeriod] = value; }
		}
		/// <summary>
		/// Total hard bounces with a matching string detected in the bounce period
		/// </summary>
		public override int? MatchedHardBounceInPeriod
		{
			get { return (int?)this[Usr.Columns.MatchedHardBounceInPeriod]; }
			set { this[Usr.Columns.MatchedHardBounceInPeriod] = value; }
		}
		/// <summary>
		/// Total hard bounces without a matching string detected in the bounce period
		/// </summary>
		public override int? UnmatchedHardBounceInPeriod
		{
			get { return (int?)this[Usr.Columns.UnmatchedHardBounceInPeriod]; }
			set { this[Usr.Columns.UnmatchedHardBounceInPeriod] = value; }
		}
		/// <summary>
		/// Total soft bounces detected in the bounce period
		/// </summary>
		public override int? SoftBounceInPeriod
		{
			get { return (int?)this[Usr.Columns.SoftBounceInPeriod]; }
			set { this[Usr.Columns.SoftBounceInPeriod] = value; }
		}
		#endregion

		public void RegisterMailSentForBounceTracking()
		{
			DateTime thisPeriod = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
			DateTime? periodFromUsr = BouncePeriodDateTime;

			if (!periodFromUsr.HasValue || thisPeriod != periodFromUsr.Value)
			{
				BouncePeriodDateTime = thisPeriod;
				TotalEmailsSentInPeriod = 0;
				MatchedHardBounceInPeriod = 0;
				UnmatchedHardBounceInPeriod = 0;
				SoftBounceInPeriod = 0;
			}


			TotalEmailsSentInPeriod++;
			this.Update();
		}



		#region SignOut()
		public static void SignOut()
		{

			Usr.Current.SendLogoutChatAlert();
			Usr.Current.IsLoggedOn = false;
			Usr.Current.Update();

			Cambro.Web.Helpers.DeleteCookie("SpottedAuthFix");

			Usr.Current = null;

			HttpContext.Current.Response.Redirect("/pages/logout");
		}
		#endregion

		#region ClearMyInbox()
		public void ClearMyInbox()
		{
			{
				Update u = new Update();

				u.Where = new And(
					new Q(ThreadUsr.Columns.UsrK, Usr.Current.K),
					new Or(
						new Q(ThreadUsr.Columns.Status, ThreadUsr.StatusEnum.NewComment),
						new Q(ThreadUsr.Columns.Status, ThreadUsr.StatusEnum.UnArchived),
						new And(
							new Q(ThreadUsr.Columns.Status, ThreadUsr.StatusEnum.NewInvite),
							new Q(Thread.Columns.TotalWatching, QueryOperator.LessThanOrEqualTo, 10)
						)
					)
				);
				u.From = new JoinLeft(ThreadUsr.Columns.ThreadK, Thread.Columns.K);
				u.Table = TablesEnum.ThreadUsr;
				u.Changes.Add(new Assign(ThreadUsr.Columns.Status, ThreadUsr.StatusEnum.Archived));
				u.Changes.Add(new Assign(ThreadUsr.Columns.StatusChangeDateTime, DateTime.Now));
				u.CommandTimeout = 3600;
				u.Run();

			}

			{
				Update u = new Update();

				u.Where = new And(
					new Q(ThreadUsr.Columns.UsrK, Usr.Current.K),
					new Or(
						new Q(ThreadUsr.Columns.Status, ThreadUsr.StatusEnum.NewGroupNewsAlert),
						new Q(ThreadUsr.Columns.Status, ThreadUsr.StatusEnum.NewWatchedForumAlert),
						new And(
							new Q(ThreadUsr.Columns.Status, ThreadUsr.StatusEnum.NewInvite),
							new Q(Thread.Columns.TotalWatching, QueryOperator.GreaterThan, 10)
						)
					)
				);
				u.From = new JoinLeft(ThreadUsr.Columns.ThreadK, Thread.Columns.K);
				u.Table = TablesEnum.ThreadUsr;
				u.Changes.Add(new Assign(ThreadUsr.Columns.Status, ThreadUsr.StatusEnum.Ignore));
				u.Changes.Add(new Assign(ThreadUsr.Columns.StatusChangeDateTime, DateTime.Now));
				u.CommandTimeout = 3600;
				u.Run();

			}

			//	NewComment - archive
			//	UnArchived - archive
			//	NewGroupNewsAlert - ignore
			//	NewInvite - Archive if under 10 watching, else ignore
			//	NewWatchedForumAlert - ignore
		}
		#endregion

		#region DonationIcon
		private DonationIcon rolloverDonationIcon;
		public DonationIcon RolloverDonationIcon
		{
			get
			{
				if (!this.RolloverDonationIconK.HasValue)
				{
					return null;
				}
				return rolloverDonationIcon ?? (rolloverDonationIcon = new DonationIcon(this.RolloverDonationIconK.Value));
			}
		}

		private bool? isDonationIconLegend;
		public bool IsDonationIconLegend
		{
			get
			{
				if (!isDonationIconLegend.HasValue)
				{
					var q = new Query
					{
						QueryCondition = new And(
								new Q(DonationIcon.Columns.Enabled, true),
								new Q(UsrDonationIcon.Columns.UsrK, this.K),
								new Q(UsrDonationIcon.Columns.Enabled, true)
								),
						TableElement = new Join(DonationIcon.Columns.K, UsrDonationIcon.Columns.DonationIconK),
						OrderBy = new OrderBy(DonationIcon.Columns.K, OrderBy.OrderDirection.Descending),
						Distinct = true,
						DistinctColumn = DonationIcon.Columns.K,
						ReturnCountOnly = true
					};
					isDonationIconLegend = (new DonationIconSet(q).Count == DonationIcon.GetAllDonationIcons().Count);
				}
				return isDonationIconLegend.Value;
			}
		}
		#endregion

		#region LinkedTables
		#region ChildPhotosOfMe
		public CachedSqlSelect<Photo> ChildPhotosOfMe()
		{
			return this.ChildPhotosOfMe(null, null);
		}
		public CachedSqlSelect<Photo> ChildPhotosOfMe(Q where)
		{
			return this.ChildPhotosOfMe(where, null);
		}
		public CachedSqlSelect<Photo> ChildPhotosOfMe(params KeyValuePair<object, OrderBy.OrderDirection>[] orderBy)
		{
			return this.ChildPhotosOfMe(null, orderBy);
		}
		public CachedSqlSelect<Photo> ChildPhotosOfMe(Q where, params KeyValuePair<object, OrderBy.OrderDirection>[] orderBy)
		{
			PhotoTableDef def = new PhotoTableDef();
			UsrPhotoMeTableDef def2 = new UsrPhotoMeTableDef();
			return new CachedSqlSelect<Photo>(
				new LinkedChildren<Photo>
				(
					TablesEnum.Usr,
					this.K,
					TablesEnum.Photo,
					def.TableCacheKey,
					dr =>
					{
						Photo newPhoto = new Photo();
						newPhoto.Initialise(dr);
						return newPhoto;
					},
					where,
					orderBy,
					TablesEnum.UsrPhotoMe,
					def2.TableCacheKey
				)
			);
		}
		#endregion
		#region ChildFavouritePhotos
		public CachedSqlSelect<Photo> ChildFavouritePhotos()
		{
			return this.ChildFavouritePhotos(null, null);
		}
		public CachedSqlSelect<Photo> ChildFavouritePhotos(Q where)
		{
			return this.ChildFavouritePhotos(where, null);
		}
		public CachedSqlSelect<Photo> ChildFavouritePhotos(params KeyValuePair<object, OrderBy.OrderDirection>[] orderBy)
		{
			return this.ChildFavouritePhotos(null, orderBy);
		}
		public CachedSqlSelect<Photo> ChildFavouritePhotos(Q where, params KeyValuePair<object, OrderBy.OrderDirection>[] orderBy)
		{
			PhotoTableDef def = new PhotoTableDef();
			UsrPhotoFavouriteTableDef def2 = new UsrPhotoFavouriteTableDef();
			return new CachedSqlSelect<Photo>(
				new LinkedChildren<Photo>
				(
					TablesEnum.Usr,
					this.K,
					TablesEnum.Photo,
					def.TableCacheKey,
					dr =>
					{
						Photo newPhoto = new Photo();
						newPhoto.Initialise(dr);
						return newPhoto;
					},
					where,
					orderBy,
					TablesEnum.UsrPhotoFavourite,
					def2.TableCacheKey
				)
			);
		}
		#endregion
		#endregion

		#region PhotoUsageString
		public string PhotoUsageString
		{
			get
			{
				if (PhotoUsage == PhotoUsageEnum.Use)
					return "I am happy for my photos to be used in print, credited with my name. Send me an email if my photo is used.";
				else if (PhotoUsage == PhotoUsageEnum.Contact)
					return "Please contact me by email, and don't use my photo until I respond.";
				else if (PhotoUsage == PhotoUsageEnum.DoNotUse)
					return "Please DO NOT use my photos.";
				else
					return "";
			}
		}
		#endregion

		#region PhotoUsageAdminString
		public string PhotoUsageAdminString
		{
			get
			{
				if (PhotoUsage == PhotoUsageEnum.Use)
					return "Use with credit '" + FullName + "', email: " + Email;
				else if (PhotoUsage == PhotoUsageEnum.Contact)
					return "Contact before use: " + Email;
				else if (PhotoUsage == PhotoUsageEnum.DoNotUse)
					return "Do not use";
				else
					return "";
			}
		}
		#endregion

		#region RegisterPageRequest
		public void RegisterPageRequest(bool update, DateTime now)
		{
			bool doJob = !this.DateTimeLastBuddyAlertsRoomRefresh.HasValue || this.DateTimeLastBuddyAlertsRoomRefresh.Value.AddMinutes(5) < now;
			bool sendLoginChatItem = this.DateTimeLastPageRequest.AddMinutes(15) < now;

			this.DateTimeLastPageRequest = now;
			this.IsLoggedOn = true;
			if (doJob)
				this.DateTimeLastBuddyAlertsRoomRefresh = now;
	
			if (update)
				this.Update();

			if (doJob || sendLoginChatItem)
			{
				ChatServerBuddyAlertsRoomRefresh job = new ChatServerBuddyAlertsRoomRefresh(this.K, sendLoginChatItem);
				job.ExecuteAsynchronously();
			}
		}
		#endregion

		#region ChatServerBuddyAlertsRoomRefresh
		public class ChatServerBuddyAlertsRoomRefresh : Job
		{
			JobDataMapItemProperty<int> UsrK { get { return new JobDataMapItemProperty<int>("UsrK", JobDataMap); } }
			JobDataMapItemProperty<bool> SendLoginChatItem { get { return new JobDataMapItemProperty<bool>("SendLoginChatItem", JobDataMap); } }

			public ChatServerBuddyAlertsRoomRefresh()
			{ }
			public ChatServerBuddyAlertsRoomRefresh(int usrK, bool sendLoginChatItem)
			{
				UsrK.Value = usrK;
				SendLoginChatItem.Value = sendLoginChatItem;
			}

			protected override void Execute()
			{
				Usr myUsr = new Usr(UsrK);
				//get logged on buddies...
				Query q = new Query();
				q.TableElement = Usr.BuddyJoin;
				q.QueryCondition = new And(myUsr.BuddiesFullQ, Usr.LoggedInChatQ);
				q.Columns = new ColumnSet(Usr.Columns.K);
				UsrSet loggedOnBuddies = new UsrSet(q);
				Guid myRoom = new Chat.RoomSpec(SpottedScript.Controls.ChatClient.Shared.RoomType.BuddyAlerts, Model.Entities.ObjectType.Usr, myUsr.K).Guid;
				if (loggedOnBuddies.Count > 0)
				{
					int[] loggedOnBuddyKs = loggedOnBuddies.ToList().ConvertAll(u1 => u1.K).ToArray();
					List<Guid> buddyRooms = new List<Guid>();
					foreach (int buddyK in loggedOnBuddyKs)
					{
						buddyRooms.Add(new Chat.RoomSpec(SpottedScript.Controls.ChatClient.Shared.RoomType.BuddyAlerts, Model.Entities.ObjectType.Usr, buddyK).Guid);
					}
					
					ChatLibrary.ChatServerInterface cs = (ChatLibrary.ChatServerInterface)Activator.GetObject(typeof(ChatLibrary.ChatServerInterface), Bobs.Vars.ChatServerAddress);
					cs.JoinRoom(buddyRooms.ToArray(), myUsr.K);
					cs.JoinRoom(myRoom, loggedOnBuddyKs);
				}
				if (SendLoginChatItem)
				{
					SpottedScript.Controls.ChatClient.Shared.AlertStub alertStub = new SpottedScript.Controls.ChatClient.Shared.AlertStub(
						Guid.NewGuid().Pack(),
						SpottedScript.Controls.ChatClient.Shared.ItemType.LoginAlert,
						DateTime.Now.Ticks.ToString(),
						myRoom.Pack(),
						myUsr.NickName,
						myUsr.StmuParams,
						myUsr.K,
						myUsr.HasPic ? myUsr.Pic.ToString() : "0");
					Chat.SendJsonChatItem(alertStub);
				}
			}
		}
		#endregion

		#region SendLogoutChatAlert()
		public void SendLogoutChatAlert()
		{
			SpottedScript.Controls.ChatClient.Shared.AlertStub alertStub = new SpottedScript.Controls.ChatClient.Shared.AlertStub(
				Guid.NewGuid().Pack(),
				SpottedScript.Controls.ChatClient.Shared.ItemType.LogoutAlert,
				DateTime.Now.Ticks.ToString(),
				new Chat.RoomSpec(SpottedScript.Controls.ChatClient.Shared.RoomType.BuddyAlerts, Model.Entities.ObjectType.Usr, this.K).Guid.Pack(),
				this.NickName,
				this.StmuParams,
				this.K,
				this.HasPic ? this.Pic.ToString() : "0");
			Chat.SendJsonChatItem(alertStub);
		}
		#endregion

		#region UpdateChildUrlFragments
		public void UpdateChildUrlFragments(bool Cascade)
		{
		}
		#endregion

		#region SpamBotDefeater cache keys
		public static string GetSpamBotDefeaterItemCountCacheKey(SpamBotDefeaterCounter item, int usrK, DateTime today)
		{
			return Caching.CacheKeyPrefix.SpamBotDefeater + "-UsrK-" + Bobs.Usr.Current.K + "-Date-" + DateTime.Today.ToString("yyyyMMdd") + "-Count-" + ((int)item).ToString();
		}
		public static string GetSpamBotDefeaterFailCountCacheKey(int usrK, DateTime today)
		{
			return Caching.CacheKeyPrefix.SpamBotDefeater + "-UsrK-" + Bobs.Usr.Current.K + "-Date-" + DateTime.Today.ToString("yyyyMMdd") + "-FailCount";
		}
		public static string GetSpamBotDefeaterAttemptCountCacheKey(int usrK, DateTime today)
		{
			return Caching.CacheKeyPrefix.SpamBotDefeater + "-UsrK-" + Bobs.Usr.Current.K + "-Date-" + DateTime.Today.ToString("yyyyMMdd") + "-AttemptCount";
		}
		#endregion

		#region ResetAllSpamBotDefeaterCounters
		public static void ResetAllSpamBotDefeaterCounters(int usrK)
		{
			Caching.Instances.MainCounterStore.SetCounter(Usr.GetSpamBotDefeaterItemCountCacheKey(SpamBotDefeaterCounter.Comments, usrK, DateTime.Today), 0);
			Caching.Instances.MainCounterStore.SetCounter(Usr.GetSpamBotDefeaterItemCountCacheKey(SpamBotDefeaterCounter.BuddyRequests, usrK, DateTime.Today), 0);
			Caching.Instances.MainCounterStore.SetCounter(Usr.GetSpamBotDefeaterItemCountCacheKey(SpamBotDefeaterCounter.GroupInvites, usrK, DateTime.Today), 0);
			Caching.Instances.MainCounterStore.SetCounter(Usr.GetSpamBotDefeaterItemCountCacheKey(SpamBotDefeaterCounter.PrivateLiveChats, usrK, DateTime.Today), 0);

			Caching.Instances.MainCounterStore.SetCounter(Usr.GetSpamBotDefeaterItemCountCacheKey(SpamBotDefeaterCounter.Comments, usrK, DateTime.Today.AddDays(1)), 0);
			Caching.Instances.MainCounterStore.SetCounter(Usr.GetSpamBotDefeaterItemCountCacheKey(SpamBotDefeaterCounter.BuddyRequests, usrK, DateTime.Today.AddDays(1)), 0);
			Caching.Instances.MainCounterStore.SetCounter(Usr.GetSpamBotDefeaterItemCountCacheKey(SpamBotDefeaterCounter.GroupInvites, usrK, DateTime.Today.AddDays(1)), 0);
			Caching.Instances.MainCounterStore.SetCounter(Usr.GetSpamBotDefeaterItemCountCacheKey(SpamBotDefeaterCounter.PrivateLiveChats, usrK, DateTime.Today.AddDays(1)), 0);
		}
		#endregion

		#region IsEmailStubUser
		public bool IsEmailStubUser
		{
			get { return NickName == ""; }
		}
		#endregion

		#region CheckForSpamBot
		public static bool CheckForSpamBot(bool IsPreCheck)
		{
			if (HttpContext.Current.User.Identity.IsAuthenticated && Usr.Current != null)
			{
				int numberOfItems = 50;
				if (IsPreCheck)
					numberOfItems = 70;

				uint comments = Caching.Instances.MainCounterStore.GetCounter(Usr.GetSpamBotDefeaterItemCountCacheKey(SpamBotDefeaterCounter.Comments, Usr.Current.K, DateTime.Today), () => 0);
				uint buddyRequests = Caching.Instances.MainCounterStore.GetCounter(Usr.GetSpamBotDefeaterItemCountCacheKey(SpamBotDefeaterCounter.BuddyRequests, Usr.Current.K, DateTime.Today), () => 0);
				uint groupInvites = Caching.Instances.MainCounterStore.GetCounter(Usr.GetSpamBotDefeaterItemCountCacheKey(SpamBotDefeaterCounter.GroupInvites, Usr.Current.K, DateTime.Today), () => 0);
				uint privateLiveChats = Caching.Instances.MainCounterStore.GetCounter(Usr.GetSpamBotDefeaterItemCountCacheKey(SpamBotDefeaterCounter.PrivateLiveChats, Usr.Current.K, DateTime.Today), () => 0);
				return !HttpContext.Current.Request.Url.PathAndQuery.StartsWith("/popup/captcha") && (
					comments > numberOfItems ||
					buddyRequests > numberOfItems ||
					groupInvites > numberOfItems ||
					privateLiveChats > numberOfItems);
			}
			else
				return false;
		}
		#endregion

		#region IncrementSpamBotDefeaterCounter
		public static void IncrementSpamBotDefeaterCounter(SpamBotDefeaterCounter item, int usrK)
		{
			Caching.Instances.MainCounterStore.Increment(Usr.GetSpamBotDefeaterItemCountCacheKey(item, usrK, DateTime.Today), () => 0);
		}
		#endregion

		#region ToString()
		public override string ToString()
		{
			return base.ToString() + String.Format("(K={0}, Nickname={1})", this.K.ToString(), this.NickName.ToString());
		}
		#endregion

		#region IBobAsHTML methods
		public string AsHTML()
		{
			string lineReturn = Vars.HTML_LINE_RETURN;
			StringBuilder sb = new StringBuilder();

			sb.Append(lineReturn);
			sb.Append(lineReturn);
			sb.Append("<u>Usr details</u>");
			sb.Append(lineReturn);
			sb.Append("K: ");
			sb.Append(this.K.ToString());
			sb.Append(lineReturn);
			sb.Append("Nickname: ");
			sb.Append(this.NickNameDisplay);
			sb.Append(lineReturn);
			sb.Append("Name: ");
			sb.Append(this.FullName);
			sb.Append(lineReturn);
			sb.Append("Address: ");
			sb.Append(this.AddressHtml());
			sb.Append(lineReturn);

			return sb.ToString();
		}
		#endregion

		#region BuddiesFullKs
		public Common.Classes.FastLookup<int> BuddiesFullKs
		{
			get
			{
				Common.Classes.FastLookup<int> lookuper = new Common.Classes.FastLookup<int>();
				foreach (Usr u in this.BuddiesFull)
				{
					lookuper.Add(u.K);
				}
				return lookuper;
			}
		}
		#endregion
		#region GetOnlineUsers
		public static UsrSet GetOnlineUsers()
		{
			Query onlineUsrsQuery = new Query();
			onlineUsrsQuery.Columns = new ColumnSet(Usr.LinkColumns);
			onlineUsrsQuery.QueryCondition = Usr.LoggedIn30MinQ;
			onlineUsrsQuery.NoLock = true;
			onlineUsrsQuery.OrderBy = new OrderBy(Usr.Columns.NickName);
			//onlineUsrsQuery.CacheDuration = new TimeSpan(0, 0, 0);
			UsrSet onlineUsrs = new UsrSet(onlineUsrsQuery);

			return onlineUsrs;
		}
		#endregion

		#region Phone
		public Phone Phone
		{
			get
			{
				if (phone == null)
				{
					PhoneSet ps = new PhoneSet(new Query(new Q(Phone.Columns.UsrK, this.K)));
					if (ps.Count > 0)
						phone = ps[0];
				}
				return phone;
			}
			set
			{
				phone = value;
			}
		}
		Phone phone;
		#endregion

		#region InvalidUsrK
		public const int InvalidUsrK = -1;
		#endregion

		#region GetLegalDrinkingAge(int CountryK)
		public static int GetLegalDrinkingAge(int CountryK)
		{
			switch (CountryK)
			{
				case 9: return 16;
				case 10: return 18;
				case 11: return 0;
				case 13: return 18;
				case 14: return 16;
				case 15: return 0;
				case 16: return 18;
				case 19: return 18;
				case 21: return 16;
				case 24: return 18;
				case 30: return 18;
				case 31: return 18;
				case 38: return 18;
				case 43: return 18;
				case 44: return 0;
				case 47: return 18;
				case 57: return 18;
				case 58: return 18;
				case 67: return 18;
				case 71: return 0;
				case 72: return 18;
				case 73: return 16;
				case 80: return 0;
				case 81: return 16;
				case 84: return 16;
				case 97: return 18;
				case 98: return 18;
				case 99: return 20;
				case 101: return 18;
				case 104: return 18;
				case 105: return 18;
				case 106: return 16;
				case 107: return 18;
				case 108: return 20;
				case 114: return 19;
				case 118: return 18;
				case 124: return 18;
				case 125: return 18;
				case 130: return 18;
				case 139: return 18;
				case 141: return 18;
				case 143: return 18;
				case 151: return 16;
				case 154: return 18;
				case 157: return 0;
				case 161: return 16;
				case 169: return 18;
				case 170: return 18;
				case 172: return 16;
				case 173: return 0;
				case 174: return 18;
				case 176: return 18;
				case 191: return 18;
				case 192: return 18;
				case 195: return 18;
				case 197: return 16;
				case 205: return 18;
				case 206: return 16;
				case 211: return 0;
				case 217: return 16;
				case 218: return 18;
				case 222: return 18;
				case 224: return 18;
				case 225: return 21;
				case 227: return 18;
				case 230: return 18;
				case 231: return 0;
				default: return 21;
			}
		}
		#endregion

		#region IsOfLegalDrinkingAgeInHomeCountry
		public bool IsOfLegalDrinkingAgeInHomeCountry
		{
			get
			{
				return IsOfLegalDrinkingAgeInHomeCountryStatic(Home == null ? -1 : Home.CountryK, DateOfBirth);
			}
		}
		public static bool IsOfLegalDrinkingAgeInHomeCountryStatic(int countryK, DateTime dateOfBirth)
		{
			if (dateOfBirth.Equals(DateTime.MinValue))
				return false;

			int LegalDrinkingAge = 21;
			if (countryK > -1)
				LegalDrinkingAge = GetLegalDrinkingAge(countryK);

			// get the difference in years
			int years = DateTime.Now.Year - dateOfBirth.Year;
			// subtract another year if we're before the
			// birth day in the current year
			if (DateTime.Now.Month < dateOfBirth.Month || (DateTime.Now.Month == dateOfBirth.Month && DateTime.Now.Day < dateOfBirth.Day))
				years--;

			return years >= LegalDrinkingAge;
		}
		#endregion

		#region Permissions
		#endregion

		#region SendPasswordResetLink()
		public void SendPasswordResetLink()
		{
			if (this.PasswordResetEmailSecret.Length == 0)
			{
				this.PasswordResetEmailSecret = Cambro.Misc.Utility.GenRandomText(10).ToLower();
				this.Update();
			}

			Mailer m = new Mailer();
			m.Subject = "DontStayIn password reset request";
			m.SendEvenIfUnverifiedOrBroken = true;
			
			string nick = "";
			if (this.NickName.Length > 0)
				nick += ", and nickname <b>" + this.NickName + "</b>";

			m.Body += "<p>This email is about the DontStayIn account with the email address <b>" + this.Email + "</b>" + nick + ". If this isn't you, click the 'Cancel' link below.</p>";
			m.Body += "<p>We have received a request to reset your password. If you're expecting this email, just click the link below to reset your password. If you weren't expecting this email, click the 'Cancel' link below.</p>";
			m.Body += "<p align=\"center\" style=\"margin:8px 0px 4px 0px;\"><span style=\"font-size:18px;font-weight:bold;\"><a href=\"http://" + Vars.DomainName + this.PasswordResetUrl() + "\">Reset my password</a> or <a href=\"http://" + Vars.DomainName + this.PasswordResetUrl("cancel", "1") + "\">cancel this request</a></span></p>";

			m.ShowQuickLink = false;

			m.UsrRecipient = this;
			m.Send();
		}
		#endregion

		#region PasswordResetUrl
		public string PasswordResetUrl(params string[] par)
		{
			par = Cambro.Misc.Utility.JoinStringArrays(par, new string[] { "k", this.K.ToString(), "reset", this.PasswordResetEmailSecret });
			return UrlInfo.PageUrl("password", par);
		}
		#endregion

		#region SetPassword
		public void SetPassword(string newPassword, bool update)
		{
			if (newPassword.Trim().Length == 0)
			{
				throw new Exception("Password can't be null");
			}

			PasswordSalt = Guid.NewGuid();
			PasswordHash = Cambro.Misc.Utility.Hash(newPassword.Trim(), PasswordSalt);
			if (update)
				this.Update();
		}
		#endregion
		#region CheckPassword
		public bool CheckPassword(string password)
		{
			if (PasswordHash == Guid.Empty || 
				PasswordSalt == Guid.Empty || 
				password.Trim().Length == 0)
				return false;

			Guid hash = Cambro.Misc.Utility.Hash(password.Trim(), PasswordSalt);
			return PasswordHash.Equals(hash);
		}
		#endregion
		#region HasNullPassword
		public bool HasNullPassword
		{
			get
			{
				return PasswordHash == Guid.Empty;
			}
		}
		#endregion
		#region SetNullPassword
		public void SetNullPassword(bool update)
		{
			PasswordSalt = Guid.Empty;
			PasswordHash = Guid.Empty;
			if (update)
				this.Update();
		}
		#endregion
		#region GetAuthenticationHash
		/// <summary>
		/// This is used for the forms authentication token
		/// </summary>
		/// <returns></returns>
		public Guid GetAuthenticationHash()
		{
			string passwordHash = PasswordHash.ToString();

			return Cambro.Misc.Utility.Hash(passwordHash, new Guid("89585ce3-b996-4b4a-bf46-2a7cdee08ebf"));

		}
		#endregion

		#region MobileDial
		/// <summary>
		/// The mobile number that you dial from the UK, e.g. without the 44 if it's a UK number, and with a leading '00' if it's international
		/// </summary>
		public string MobileDial
		{
			get
			{
				if (Mobile.StartsWith("44"))
					return "0" + Mobile.Substring(2);
				else
					return "00" + Mobile;
			}
		}
		#endregion

		#region DeleteAllSavedCards
		public void DeleteAllSavedCards()
		{
			Update u = new Update();
			u.Table = TablesEnum.Transfer;
			u.Changes.Add(new Assign(Transfer.Columns.CardSaved, false));
			u.Where = new And(
				new Q(Transfer.Columns.CardSaved, true),
				new Q(Transfer.Columns.UsrK, this.K)
			);
			u.Run();
		}
		#endregion

		#region GetFromNickName
		const string GetFromNickNameCacheKey = "FindUsrKFromNickName:";
		public static Usr GetFromNickName(string nickname)
		{
			nickname = nickname.ToLower();
			try
			{
				Usr usr;
				int? k = Caching.Instances.Main.Get(GetFromNickNameCacheKey + nickname) as int?;
				if (k == null)
				{
					usr = GetUsrBobFromNickName(nickname);
					if (usr == null)
					{
						return null;
					}
					Caching.Instances.Main.Store(GetFromNickNameCacheKey + nickname, usr.K);
				}
				else
				{
					usr = new Usr(k.Value);
					if (String.Compare(usr.NickName, nickname, true) != 0)
					{
						Caching.Instances.Main.Delete(GetFromNickNameCacheKey + nickname);
						Caching.Instances.Main.Store(GetFromNickNameCacheKey + usr.NickName.ToLower(), usr.K);
					}
				}
				return usr;
			}
			catch (BobNotFound)
			{
				return null;
			}
		}

		private static Usr GetUsrBobFromNickName(string nickname)
		{
			UsrSet us = new UsrSet(new Query(new Q(Usr.Columns.NickName, nickname)));
			if (us.Count > 0)
				return us[0];
			else
				return null;
		}
		#endregion

		#region AdminPhoneCallUrl
		public string AdminPhoneCallUrl(string number)
		{
			return "http://phone-" + Usr.Current.NickName.ToLower() + "/index.htm?number=" + AdminPhoneCallRegex.Replace(number, String.Empty);
		}
		Regex AdminPhoneCallRegex
		{
			get
			{
				if (adminPhoneCallRegex == null)
				{
					adminPhoneCallRegex = new System.Text.RegularExpressions.Regex("[^0-9]");
				}
				return adminPhoneCallRegex;
			}
		}
		Regex adminPhoneCallRegex;
		#endregion

		#region PerformUsrBuddyLink
		public static TableElement PerformUsrBuddyLink(TableElement tableElement)
		{
			return new Bobs.Join(
					tableElement,
					new TableElement(TablesEnum.Buddy),
					QueryJoinType.Left,
					new And(
						new Q(Usr.Columns.K, Buddy.Columns.BuddyUsrK, true),
						new Q(Buddy.Columns.FullBuddy, true),
						new Q(Buddy.Columns.UsrK, (Usr.Current == null ? 0 : Usr.Current.K))
					)
				);
		}
		#endregion

		#region RecomputeSpottingsMonth
		public static void RecomputeSpottingsMonth()
		{
			System.Console.WriteLine("Updating SpottingsMonth...");

			try
			{
				//                Db.Qu(@"
				//UPDATE [Usr] WITH (ROWLOCK) SET [SpottingsMonth] = 
				//	(
				//	SELECT COUNT(DISTINCT [UsrPhotoMe].[UsrK]) AS [Spottings] 
				//	FROM [Photo] WITH (NOLOCK) 
				//	LEFT JOIN [UsrPhotoMe] WITH (NOLOCK) ON [Photo].[K] = [UsrPhotoMe].[PhotoK] 
				//	LEFT JOIN [Usr] AS [SpottedUsr] WITH (NOLOCK) ON [UsrPhotoMe].[UsrK] = [SpottedUsr].[K]
				//	WHERE [Photo].[ParentDateTime] > DATEADD(month, -1, GETDATE()) 
				//	AND [SpottedUsr].[IsSkeleton]=0	
				//	AND [SpottedUsr].[IsEmailVerified]=1 
				//	AND [Photo].[UsrK]=[Usr].[K]
				//	)
				//WHERE NOT [SpottingsMonth] = 
				//	(
				//	SELECT COUNT(DISTINCT [UsrPhotoMe].[UsrK]) AS [Spottings] 
				//	FROM [Photo] WITH (NOLOCK) 
				//	LEFT JOIN [UsrPhotoMe] WITH (NOLOCK) ON [Photo].[K] = [UsrPhotoMe].[PhotoK] 
				//	LEFT JOIN [Usr] AS [SpottedUsr] WITH (NOLOCK) ON [UsrPhotoMe].[UsrK] = [SpottedUsr].[K]
				//	WHERE [Photo].[ParentDateTime] > DATEADD(month, -1, GETDATE()) 
				//	AND [SpottedUsr].[IsSkeleton]=0	
				//	AND [SpottedUsr].[IsEmailVerified]=1 
				//	AND [Photo].[UsrK]=[Usr].[K]
				//	);", 3600);
				Db.Qu(@"
WITH Cte([Spottings], [UsrK]) AS
	(
	SELECT COUNT(DISTINCT [UsrPhotoMe].[UsrK]) AS [Spottings] , [Photo].[UsrK] 
	FROM [Photo] WITH ( NOLOCK ) 
	INNER JOIN [UsrPhotoMe] WITH (NOLOCK) ON [Photo].[K] = [UsrPhotoMe].[PhotoK] 
	INNER JOIN [Usr] AS [SpottedUsr] WITH (NOLOCK) ON [UsrPhotoMe].[UsrK] = [SpottedUsr].[K] 
	WHERE [Photo].[ParentDateTime] > DATEADD(month, -1, GETDATE()) AND [SpottedUsr].[IsSkeleton]=0 AND [SpottedUsr].[IsEmailVerified]=1 
	GROUP BY [Photo].[UsrK] 
	)
UPDATE [Usr] WITH (ROWLOCK) 
SET [SpottingsMonth] = (SELECT Spottings FROM Cte WHERE Usr.k = Cte.UsrK)
WHERE [Usr].[LastPhotoUpload] > DATEADD(month, -2, GETDATE()) 
AND NOT [SpottingsMonth] = (SELECT Spottings FROM Cte WHERE Usr.k = Cte.UsrK)
", 3600);
			}
			catch (Exception ex)
			{
				Mailer admin = new Mailer();
				admin.TemplateType = Mailer.TemplateTypes.AdminNote;
				admin.Body = "<p>Exception recomputing SpottingsMonth</p>";
				admin.Body += "<p>" + ex.ToString() + "</p>";
				admin.Subject = "Exception recomputing SpottingsMonth";
				admin.To = "d.brophy@dontstayin.com";
				admin.Send();
			}
			System.Console.WriteLine("Done updating SpottingsMonth...", 1);


		}
		#endregion
		#region RecomputeSpottingsMonthRank
		public static void RecomputeSpottingsMonthRank()
		{

			System.Console.WriteLine("Updating SpottingsMonthRank...", 1);

			try
			{
				Db.Qu(
				/*	@"
UPDATE u SET SpottingsMonthRank = Ranks.Rank
FROM Usr u (NOLOCK)
LEFT JOIN (SELECT TOP 1000 K, ROW_NUMBER() OVER (ORDER BY SpottingsMonth DESC, SpottingsTotal DESC) AS [Rank] FROM Usr (NOLOCK)) Ranks 
ON u.K = Ranks.K 
WHERE ISNULL(u.SpottingsMonthRank, 0) <> 0 
OR ISNULL(Ranks.[Rank], 0) <> 0
",
					7200);
				*/
					@"
DECLARE @found int
SET @found = 1
WHILE @found > 0
BEGIN
	UPDATE [Usr]
		SET [SpottingsMonthRank] = (1+(SELECT COUNT(Usr1.[K]) AS Rank FROM [Usr] AS Usr1 WITH (NOLOCK) WHERE Usr1.[SpottingsMonth]>[Usr].[SpottingsMonth]))
	WHERE [Usr].[K] IN 
		(SELECT TOP 1000 K FROM [Usr] WHERE NOT [SpottingsMonthRank] = (1+(SELECT COUNT(Usr1.[K]) AS Rank FROM [Usr] AS Usr1 WITH (NOLOCK) WHERE Usr1.[SpottingsMonth]>[Usr].[SpottingsMonth])))
	SET @found = @@ROWCOUNT
END", 7200);

				/*
				 
UPDATE [Usr] WITH (ROWLOCK) SET [SpottingsMonthRank] = 
	(
	1+(SELECT COUNT(Usr1.[K]) AS Rank FROM [Usr] AS Usr1 WITH (NOLOCK) WHERE Usr1.[SpottingsMonth]>[Usr].[SpottingsMonth])
	)
WHERE NOT [SpottingsMonthRank] = 
	(
	1+(SELECT COUNT(Usr1.[K]) AS Rank FROM [Usr] AS Usr1 WITH (NOLOCK) WHERE Usr1.[SpottingsMonth]>[Usr].[SpottingsMonth])
	);
				 */
			}
			catch (Exception ex)
			{
				Mailer admin = new Mailer();
				admin.TemplateType = Mailer.TemplateTypes.AdminNote;
				admin.Body = "<p>Exception recomputing SpottingsMonthRank</p>";
				admin.Body += "<p>" + ex.ToString() + "</p>";
				admin.Subject = "Exception recomputing SpottingsMonthRank";
				admin.To = "d.brophy@dontstayin.com";
				admin.Send();
			}

			System.Console.WriteLine("Done updating SpottingsMonthRank...", 1);


		}
		#endregion
		#region RecomputeSpottingsTotal
		public static void RecomputeSpottingsTotal()
		{
			System.Console.WriteLine("Updating SpottingsTotal...");

			try
			{
				Db.Qu(@"
UPDATE [Usr] WITH (ROWLOCK) SET [SpottingsTotal] = 
	(
	SELECT COUNT(DISTINCT [UsrPhotoMe].[UsrK]) AS Spottings FROM [Photo] WITH (NOLOCK)
	INNER JOIN [UsrPhotoMe] WITH (NOLOCK) ON [Photo].[K] = [UsrPhotoMe].[PhotoK]
	INNER JOIN [Usr] AS SpottedUsr WITH (NOLOCK) ON [UsrPhotoMe].[UsrK] = SpottedUsr.[K]
	WHERE SpottedUsr.[IsSkeleton]=0 
	AND SpottedUsr.[IsEmailVerified]=1 
	AND [Photo].[UsrK]=[Usr].[K]
	)
WHERE ([Usr].[TotalPhotoUploads]>0 OR [Usr].[SpottingsTotal]>0)
AND NOT [SpottingsTotal] = 
	(
	SELECT COUNT(DISTINCT [UsrPhotoMe].[UsrK]) AS Spottings FROM [Photo] WITH (NOLOCK)
	INNER JOIN [UsrPhotoMe] WITH (NOLOCK) ON [Photo].[K] = [UsrPhotoMe].[PhotoK]
	INNER JOIN [Usr] AS SpottedUsr WITH (NOLOCK) ON [UsrPhotoMe].[UsrK] = SpottedUsr.[K]
	WHERE SpottedUsr.[IsSkeleton]=0 
	AND SpottedUsr.[IsEmailVerified]=1 
	AND [Photo].[UsrK]=[Usr].[K]
	)
", 3600);
			}
			catch (Exception ex)
			{
				Mailer admin = new Mailer();
				admin.TemplateType = Mailer.TemplateTypes.AdminNote;
				admin.Body = "<p>Exception recomputing SpottingsTotal</p>";
				admin.Body += "<p>" + ex.ToString() + "</p>";
				admin.Subject = "Exception recomputing SpottingsTotal";
				admin.To = "d.brophy@dontstayin.com";
				admin.Send();
			}
			System.Console.WriteLine("Done updating SpottingsTotal...", 1);


		}
		#endregion

		#region IsADeveloper
		public bool IsADeveloper
		{
			get
			{
				//			Dave				Neil			   TimI				   Harry
				return this.K == 4 || this.K == 59354 || this.K == 469031 || this.K == 469033;
			}
		}
		#endregion
		#region Demographics
		public Demographics Demographics
		{
			get
			{
				try
				{
					if (demographics == null)
						demographics = new Demographics(this.Guid);
					return demographics;
				}
				catch (BobNotFound)
				{
					return null;
				}
			}
		}
		private Demographics demographics;

		public bool HasCompletedDemographics
		{
			get
			{
				if (Prefs.Current["CompletedDemographics"].IsNull)
				{
					Prefs.Current["CompletedDemographics"] = (this.Demographics != null).ToString();
				}
				return Convert.ToBoolean(Prefs.Current["CompletedDemographics"].Value);
			}
			set
			{
				Prefs.Current["CompletedDemographics"] = value.ToString();
			}
		}
		#endregion

		#region UpdateIsGroupModerator
		public void UpdateIsGroupModerator()
		{
			Query q = new Query();
			q.QueryCondition = new And(new Q(GroupUsr.Columns.UsrK, this.K), new Q(GroupUsr.Columns.Moderator, true));
			q.TopRecords = 1;
			GroupUsrSet gus = new GroupUsrSet(q);
			bool newVal = gus.Count != 0;
			if (this.IsGroupModerator != newVal)
			{
				this.IsGroupModerator = newVal;
				this.Update();
			}
		}
		#endregion

		#region ExtraIconInUse
		public int ExtraIconInUse
		{
			get
			{
				if (this.ExtraIcon > 0)
				{
					if (this.ExtraExpire > DateTime.Now)
						return this.ExtraIcon;
				}
				return 0;
			}
		}
		#endregion

		#region JoinedBuddy
		public Buddy JoinedBuddy
		{
			get
			{
				if (joinedBuddy == null)
				{
					joinedBuddy = new Buddy(this, Usr.Columns.K);
				}
				return joinedBuddy;
			}
			set
			{
				joinedBuddy = value;
			}
		}
		private Buddy joinedBuddy;
		#endregion

		#region JoinedUsrEventAttend
		public UsrEventAttended JoinedUsrEventAttend
		{
			get
			{
				if (joinedUsrEventAttend == null)
				{
					joinedUsrEventAttend = new UsrEventAttended(this, Usr.Columns.K);
				}
				return joinedUsrEventAttend;
			}
			set
			{
				joinedUsrEventAttend = value;
			}
		}
		private UsrEventAttended joinedUsrEventAttend;
		#endregion

		#region UpdateAbuseTrackers
		public void UpdateAbuseTrackers()
		{
			#region AbuseReportsPending
			if (true)
			{
				Query q = new Query();
				q.ReturnCountOnly = true;
				q.QueryCondition = new And(
					new Q(Abuse.Columns.ReportUsrK, this.K),
					new Q(Abuse.Columns.Status, Abuse.StatusEnum.New)
					);
				AbuseSet abs = new AbuseSet(q);
				this.AbuseReportsPending = abs.Count;
			}
			#endregion
			#region AbuseReportsUseful
			if (true)
			{
				Query q = new Query();
				q.ReturnCountOnly = true;
				q.QueryCondition = new And(
					new Q(Abuse.Columns.ReportUsrK, this.K),
					new Q(Abuse.Columns.Status, Abuse.StatusEnum.Done),
					new Or(new Q(Abuse.Columns.ResolveStatus, Abuse.ResolveStatusEnum.Abuse), new Q(Abuse.Columns.ResolveStatus, Abuse.ResolveStatusEnum.NoAbuse))
					);
				AbuseSet abs = new AbuseSet(q);
				this.AbuseReportsUseful = abs.Count;
			}
			#endregion
			#region AbuseReportsOverturned
			if (true)
			{
				Query q = new Query();
				q.ReturnCountOnly = true;
				q.QueryCondition = new And(
					new Q(Abuse.Columns.ReportUsrK, this.K),
					new Q(Abuse.Columns.Status, Abuse.StatusEnum.Done),
					new Q(Abuse.Columns.ResolveStatus, Abuse.ResolveStatusEnum.Overturned)
					);
				AbuseSet abs = new AbuseSet(q);
				this.AbuseReportsOverturned = abs.Count;
			}
			#endregion

			#region AbuseAccusationsPending
			if (true)
			{
				Query q = new Query();
				q.ReturnCountOnly = true;
				q.QueryCondition = new And(
					new Q(Abuse.Columns.AbuseUsrK, this.K),
					new Q(Abuse.Columns.Status, Abuse.StatusEnum.New)
					);
				AbuseSet abs = new AbuseSet(q);
				this.AbuseAccusationsPending = abs.Count;
			}
			#endregion
			#region AbuseAccusationsAbuse
			if (true)
			{
				Query q = new Query();
				q.ReturnCountOnly = true;
				q.QueryCondition = new And(
					new Q(Abuse.Columns.AbuseUsrK, this.K),
					new Q(Abuse.Columns.Status, Abuse.StatusEnum.Done),
					new Q(Abuse.Columns.ResolveStatus, Abuse.ResolveStatusEnum.Abuse)
					);
				AbuseSet abs = new AbuseSet(q);
				this.AbuseAccusationsAbuse = abs.Count;
			}
			#endregion
			#region AbuseAccusationsNoAbuse
			if (true)
			{
				Query q = new Query();
				q.ReturnCountOnly = true;
				q.QueryCondition = new And(
					new Q(Abuse.Columns.AbuseUsrK, this.K),
					new Q(Abuse.Columns.Status, Abuse.StatusEnum.Done),
					new Or(new Q(Abuse.Columns.ResolveStatus, Abuse.ResolveStatusEnum.NoAbuse), new Q(Abuse.Columns.ResolveStatus, Abuse.ResolveStatusEnum.Overturned))
					);
				AbuseSet abs = new AbuseSet(q);
				this.AbuseAccusationsNoAbuse = abs.Count;
			}
			#endregion

			this.Update();
		}
		#endregion

		#region UpdatePlacesVisitCount
		public void UpdatePlacesVisitCount(bool update)
		{
			Query q = new Query();
			q.QueryCondition = new Q(UsrPlaceVisit.Columns.UsrK, this.K);
			q.ReturnCountOnly = true;
			UsrPlaceVisitSet upvs = new UsrPlaceVisitSet(q);
			if (this.PlacesVisitCount != upvs.Count)
			{
				this.PlacesVisitCount = upvs.Count;
				if (update)
					this.Update();
			}
		}
		#endregion
		#region UpdateMusicTypesFavouriteCount
		public void UpdateMusicTypesFavouriteCount(bool update)
		{
			Query q = new Query();
			q.QueryCondition = new Q(UsrMusicTypeFavourite.Columns.UsrK, this.K);
			q.ReturnCountOnly = true;
			UsrMusicTypeFavouriteSet umtfs = new UsrMusicTypeFavouriteSet(q);
			if (this.MusicTypesFavouriteCount != umtfs.Count)
			{
				this.MusicTypesFavouriteCount = umtfs.Count;
				if (update)
					this.Update();
			}
		}
		#endregion
		#region UpdatePhotosMeCount
		public void UpdatePhotosMeCount(bool update, Transaction transaction)
		{
			Query q = new Query();
			q.QueryCondition = new Q(UsrPhotoMe.Columns.UsrK, this.K);
			q.ReturnCountOnly = true;
			UsrPhotoMeSet upms = new UsrPhotoMeSet(q);
			if (this.PhotosMeCount != upms.Count)
			{
				this.PhotosMeCount = upms.Count;
				if (update)
					this.Update(transaction);
			}
		}
		#endregion

		#region UrlBuddies
		public string UrlBuddies()
		{
			return UrlApp("buddies");
		}
		#endregion
		#region UrlBuddyRequests
		public string UrlBuddyRequests()
		{
			return UrlApp("buddyrequests");
		}
		#endregion
		#region UrlBuddyRequestsIveSent
		public string UrlBuddyRequestsIveSent()
		{
			return UrlApp("buddyrequestsivesent");
		}
		#endregion

		#region UrlMyComments
		public string UrlMyComments(params string[] par)
		{
			return UrlInfo.MakeUrl(this.UrlFilterPart, "chat", par);
		}
		#endregion
		#region UrlMyCommentsDate
		public string UrlMyCommentsDate(DateTime d, params string[] par)
		{
			return UrlInfo.MakeUrl(String.Format("{0}/{1}/{2}/{3}", this.UrlFilterPart, d.Year, d.ToString("MMM").ToLower(), d.Day.ToString("00")), "chat", par);
		}
		#endregion
		#region UrlMyCommentsMonth
		public string UrlMyCommentsMonth(DateTime d, params string[] par)
		{
			return UrlInfo.MakeUrl(String.Format("{0}/{1}/{2}", this.UrlFilterPart, d.Year, d.ToString("MMM").ToLower()), "chat", par);
		}
		#endregion

		#region UrlMyPhotos
		public string UrlMyPhotos(params string[] par)
		{
			return UrlInfo.MakeUrl(this.UrlFilterPart, "photos", par);
		}
		#endregion
		#region UrlMyPhotosDate
		public string UrlMyPhotosDate(DateTime d, params string[] par)
		{
			return UrlInfo.MakeUrl(String.Format("{0}/{1}/{2}/{3}", this.UrlFilterPart, d.Year, d.ToString("MMM").ToLower(), d.Day.ToString("00")), "myphotos", par);
		}
		#endregion
		#region UrlMyPhotosMonth
		public string UrlMyPhotosMonth(DateTime d, params string[] par)
		{
			return UrlInfo.MakeUrl(String.Format("{0}/{1}/{2}", this.UrlFilterPart, d.Year, d.ToString("MMM").ToLower()), "myphotos", par);
		}
		#endregion

		#region UrlMyPhotosTakenBy
		public string UrlMyPhotosTakenBy(Usr takenByUsr, params string[] par)
		{
			return UrlMyPhotos("by", takenByUsr.NickName.ToLower().Replace("-", " "));
		}
		#endregion
		#region UrlMyPhotosTakenByDate
		public string UrlMyPhotosTakenByDate(Usr takenByUsr, DateTime d, params string[] par)
		{
			return UrlMyPhotosDate(d, "by", takenByUsr.NickName.ToLower().Replace("-", " "));
		}
		#endregion
		#region UrlMyPhotosTakenByMonth
		public string UrlMyPhotosTakenByMonth(Usr takenByUsr, DateTime d, params string[] par)
		{
			return UrlMyPhotosMonth(d, "by", takenByUsr.NickName.ToLower().Replace("-", " "));
		}
		#endregion

		#region UrlFavouritePhotos
		public string UrlFavouritePhotos(params string[] par)
		{
			return UrlInfo.MakeUrl(this.UrlFilterPart, "favouritephotos", par);
		}
		#endregion
		#region UrlFavouritePhotosDate
		public string UrlFavouritePhotosDate(DateTime d, params string[] par)
		{
			return UrlInfo.MakeUrl(String.Format("{0}/{1}/{2}/{3}", this.UrlFilterPart, d.Year, d.ToString("MMM").ToLower(), d.Day.ToString("00")), "favouritephotos", par);
		}
		#endregion
		#region UrlFavouritePhotosMonth
		public string UrlFavouritePhotosMonth(DateTime d, params string[] par)
		{
			return UrlInfo.MakeUrl(String.Format("{0}/{1}/{2}", this.UrlFilterPart, d.Year, d.ToString("MMM").ToLower()), "favouritephotos", par);
		}
		#endregion

		#region UrlSpottings
		public string UrlSpottings(params string[] par)
		{
			return UrlInfo.MakeUrl(this.UrlFilterPart, "spottings", par);
		}
		#endregion

		#region UrlGalleries
		public string UrlGalleries(params string[] par)
		{
			return UrlInfo.MakeUrl(this.UrlFilterPart, "mygalleries", par);
		}
		#endregion
		#region UrlGalleriesDate
		public string UrlGalleriesDate(DateTime d, params string[] par)
		{
			return UrlInfo.MakeUrl(String.Format("{0}/{1}/{2}/{3}", this.UrlFilterPart, d.Year, d.ToString("MMM").ToLower(), d.Day.ToString("00")), "mygalleries", par);
		}
		#endregion
		#region UrlGalleriesMonth
		public string UrlGalleriesMonth(DateTime d, params string[] par)
		{
			return UrlInfo.MakeUrl(String.Format("{0}/{1}/{2}", this.UrlFilterPart, d.Year, d.ToString("MMM").ToLower()), "mygalleries", par);
		}
		#endregion

		#region MultiUsrDropValue
		public string MultiUsrDropValue
		{
			get
			{
				return K.ToString() + "$" + Pic.ToString().ToLower();
			}
		}
		#endregion

		#region GroupMemberQ
		public Q GroupMemberQ
		{
			get
			{
				return new And(
					new Q(GroupUsr.Columns.UsrK, this.K),
					new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Member)
				);
			}
		}
		#endregion

		#region GroupsNamesAndKs
		public GroupSet GroupsNamesAndKs
		{
			get
			{
				if (groupsNamesAndKs == null)
				{
					Query groupsNamesAndKsQuery = new Query(GroupMemberQ);
					groupsNamesAndKsQuery.TableElement = Usr.GroupJoin;
					groupsNamesAndKsQuery.Columns = new ColumnSet(Group.Columns.K, Group.Columns.Name);
					groupsNamesAndKsQuery.OrderBy = new OrderBy(Group.Columns.Name);
					groupsNamesAndKs = new GroupSet(groupsNamesAndKsQuery);
				}
				return groupsNamesAndKs;
			}
		}
		private GroupSet groupsNamesAndKs;
		#endregion

		#region CardStatusEnum
		#endregion

		#region AdminLevels
		#endregion

		#region Can permissions
		public bool CanNewsModerator()
		{
			return (this.IsSuper);
		}
		public bool CanGroupModerator(GroupUsr gu)
		{
			return (this.IsAdmin || (gu != null && gu.Moderator));
		}
		public bool CanGroupNewsAdmin(GroupUsr gu)
		{
			return (this.IsAdmin || (gu != null && gu.NewsAdmin));
		}
		public bool CanGroupMemberAdmin(GroupUsr gu)
		{
			return (this.IsAdmin || (gu != null && gu.MemberAdmin));
		}
		public bool CanGroupOwner(GroupUsr gu)
		{
			return (this.IsAdmin || (gu != null && gu.Owner));
		}
		public bool CanGroupMember(GroupUsr gu)
		{
			return (this.IsAdmin || (gu != null && gu.IsMember));
		}
		public bool CanGroupRead(Group g, GroupUsr gu)
		{
			return (this.IsAdmin || !g.PrivateChat || (gu != null && gu.IsMember));
		}
		#endregion

		#region GroupJoin
		public static Join GroupJoin
		{
			get
			{
				return new JoinDouble(Usr.Columns.K, GroupUsr.Columns.UsrK, GroupUsr.Columns.GroupK, Group.Columns.K);
			}
		}
		#endregion

		#region GetGroupUsr(int GroupK)
		public GroupUsr GetGroupUsr(int GroupK)
		{
			try
			{
				return new GroupUsr(this.K, GroupK);
			}
			catch
			{
				return null;
			}
		}
		#endregion

		#region EmailColumns
		public static ColumnSet EmailColumns
		{
			get
			{
				return new ColumnSet(
					Usr.Columns.K,
					Usr.Columns.Email,
					Usr.Columns.LoginString,
					Usr.Columns.HomePlaceK,
					Usr.Columns.FavouriteMusicTypeK,
					Usr.Columns.NickName,
					Usr.Columns.IsBetaTester,
					Usr.Columns.IsSkeleton,
					Usr.Columns.IsEmailVerified,
					Usr.Columns.IsEmailBroken,
					Usr.Columns.NoInboxEmails,
					Usr.Columns.DateTimeSignUp,
					Usr.Columns.EmailHold,
					Usr.Columns.AddedByGroupK,
					Usr.Columns.AddedByUsrK,
					Usr.Columns.Banned,
					Usr.Columns.Guid,
					Usr.BannerServerColumns
				);
			}
		}
		#endregion

		#region BannerServerColumns
		public static ColumnSet BannerServerColumns
		{
			get
			{
				return new ColumnSet(
					Usr.Columns.K,
					Usr.Columns.Guid,
					Usr.Columns.IsMale,
					Usr.Columns.IsFemale,
					Usr.Columns.IsPromoter,
					Usr.Columns.DateOfBirth
				);
			}
		}
		#endregion

		#region GetEventModeratorUsrK()
		public static int GetEventModeratorUsrK()
		{
			Query q = new Query();
			q.QueryCondition = new Q(Usr.Columns.AdminLevel, Usr.AdminLevels.Super);
			q.OrderBy = new OrderBy(OrderBy.OrderDirection.Random);
			q.TopRecords = 1;
			q.Columns = new ColumnSet(Usr.Columns.K);
			UsrSet us = new UsrSet(q);
			return us[0].K;
		}
		#endregion
		#region GetNewsModeratorUsrK()
		public static int GetNewsModeratorUsrK()
		{
			Query q = new Query();
			q.QueryCondition = new Q(Usr.Columns.AdminLevel, Usr.AdminLevels.Super);
			q.OrderBy = new OrderBy(OrderBy.OrderDirection.Random);
			q.TopRecords = 1;
			q.Columns = new ColumnSet(Usr.Columns.K);
			UsrSet us = new UsrSet(q);
			return us[0].K;
		}
		#endregion
		#region GetPhotoModeratorUsrK()
		public static int GetPhotoModeratorUsrK()
		{
			Query q = new Query();
			q.QueryCondition = new Or(new Q(Usr.Columns.AdminLevel, Usr.AdminLevels.Super), new Q(Usr.Columns.AdminLevel, Usr.AdminLevels.Senior));
			q.OrderBy = new OrderBy(OrderBy.OrderDirection.Random);
			q.TopRecords = 1;
			q.Columns = new ColumnSet(Usr.Columns.K);
			UsrSet us = new UsrSet(q);
			return us[0].K;
		}
		#endregion
		#region ModerateHtmls
		public string NewNewsToModerateHtml
		{
			get
			{
				Query q = new Query();
				q.QueryCondition = new And(new Q(Thread.Columns.NewsStatus, Thread.NewsStatusEnum.Recommended), new Q(Thread.Columns.NewsModeratorUsrK, this.K));
				q.ReturnCountOnly = true;
				ThreadSet ts = new ThreadSet(q);
				if (ts.Count > 0)
					return "<a href=\"/pages/moderatenews/usrk-" + this.K.ToString() + "/type-1\"><b>" + ts.Count.ToString() + "</b></a>";
				else
					return "<small>0</small>";
			}
		}
		public string NewVenuesToModerateHtml
		{
			get
			{
				Query q = new Query();
				q.QueryCondition = new And(new Q(Venue.Columns.IsNew, true), new Q(Venue.Columns.ModeratorUsrK, this.K));
				q.ReturnCountOnly = true;
				VenueSet vs = new VenueSet(q);
				if (vs.Count > 0)
					return "<a href=\"/pages/venues/moderate/usrk-" + this.K.ToString() + "/type-1\"><b>" + vs.Count.ToString() + "</b></a>";
				else
					return "<small>0</small>";
			}
		}
		#endregion

		#region AddRelevant
		public void AddRelevant(IRelevanceHolder relevanceHolder)
		{
			if (this.HomePlaceK > 0)
			{
				relevanceHolder.RelevantPlacesAdd(this.HomePlaceK);
			}
			Query pQ = new Query();
			pQ.QueryCondition = new Q(UsrPlaceVisit.Columns.UsrK, this.K);
			pQ.NoLock = true;
			pQ.Columns = new ColumnSet(UsrPlaceVisit.Columns.PlaceK);
			UsrPlaceVisitSet upvs = new UsrPlaceVisitSet(pQ);
			foreach (UsrPlaceVisit upv in upvs)
			{
				relevanceHolder.RelevantPlacesAdd(upv.PlaceK);
			}

			if (this.FavouriteMusicTypeK > 1)
			{
				relevanceHolder.RelevantMusicAdd(this.FavouriteMusicTypeK);
			}
			Query mQ = new Query();
			mQ.QueryCondition = new Q(UsrMusicTypeFavourite.Columns.UsrK, this.K);
			mQ.NoLock = true;
			mQ.Columns = new ColumnSet(UsrMusicTypeFavourite.Columns.MusicTypeK);
			UsrMusicTypeFavouriteSet umtfs = new UsrMusicTypeFavouriteSet(mQ);
			foreach (UsrMusicTypeFavourite umtf in umtfs)
			{
				relevanceHolder.RelevantMusicAdd(umtf.MusicTypeK);
			}
		}
		#endregion

		#region IsPromoter
		public bool IsEnabledPromoterOfEvent(Event e)
		{
			return this.EnabledPromoterKOfEvent(e) > 0;
		}
		public int EnabledPromoterKOfEvent(Event e)
		{
			if (!this.IsPromoter)
				return 0;

			foreach (Brand b in e.Brands)
			{
				if (b.Promoter != null && b.Promoter.IsEnabled)
				{
					if (this.IsPromoterK(b.PromoterK))
						return b.PromoterK;
				}
			}

			if (e.Venue.PromoterK > 0 && e.Venue.Promoter != null && this.IsPromoterK(e.Venue.PromoterK))
				return e.Venue.PromoterK;

			return 0;
		}
		public bool IsEnabledPromoterOfEventConfirmed(Event e)
		{
			return this.EnabledPromoterKOfEventConfirmed(e) > 0;
		}
		public int EnabledPromoterKOfEventConfirmed(Event e)
		{
			if (!this.IsPromoter)
				return 0;

			foreach (Brand b in e.Brands)
			{
				if (b.PromoterStatus.Equals(Brand.PromoterStatusEnum.Confirmed) && b.Promoter != null && b.Promoter.IsEnabled)
				{
					if (this.IsPromoterK(b.PromoterK))
						return b.PromoterK;
				}
			}

			if (e.Venue.PromoterK > 0 && e.Venue.Promoter != null && e.Venue.PromoterStatus.Equals(Venue.PromoterStatusEnum.Confirmed) && this.IsPromoterK(e.Venue.PromoterK))
				return e.Venue.PromoterK;

			return 0;
		}
		public bool IsEnabledPromoterOfConfirmedVenue(Venue v)
		{
			if (!this.IsPromoter)
				return false;

			if (v.PromoterK > 0 && v.Promoter != null && v.PromoterStatus.Equals(Venue.PromoterStatusEnum.Confirmed) && this.IsPromoterK(v.PromoterK))
				return true;

			return false;
		}
		public bool IsEnabledPromoterOfConfirmedBrand(Brand b)
		{
			if (!this.IsPromoter)
				return false;

			if (b.PromoterK > 0 && b.Promoter != null && b.PromoterStatus.Equals(Brand.PromoterStatusEnum.Confirmed) && this.IsPromoterK(b.PromoterK))
				return true;

			return false;
		}
		public bool IsPromoterK(int PromoterK)
		{
			try
			{
				PromoterUsr pu = new PromoterUsr(PromoterK, this.K);
				return true;
			}
			catch
			{
				return false;
			}
		}
		public bool IsEnabledPromoter(int PromoterK)
		{
			try
			{
				PromoterUsr pu = new PromoterUsr(PromoterK, this.K);
				return pu.Promoter.IsEnabled;
			}
			catch
			{
				return false;
			}
		}
		public void UpdateIsPromoter()
		{
			Query q = new Query();
			q.QueryCondition = new Q(PromoterUsr.Columns.UsrK, this.K);
			q.TopRecords = 1;
			PromoterUsrSet pus = new PromoterUsrSet(q);
			if (this.IsPromoter != (pus.Count > 0))
			{
				this.IsPromoter = (pus.Count > 0);
				this.Update();
			}

			Query qActive = new Query();
			qActive.QueryCondition = new And(
				new Q(PromoterUsr.Columns.UsrK, this.K),
				new Q(Promoter.Columns.Status, Promoter.StatusEnum.Active)
			);
			qActive.TopRecords = 1;
			qActive.TableElement = new Join(PromoterUsr.Columns.PromoterK, Promoter.Columns.K);
			PromoterUsrSet pusActive = new PromoterUsrSet(qActive);
			bool isActivePromoter = pusActive.Count > 0;

			Group g = new Group(3684); //DontStayIn Promoters group
			GroupUsr gu = this.GetGroupUsr(g.K);
			if (isActivePromoter)
				this.AddToPromotersGroup(gu, g);
			else if (!isActivePromoter && gu != null)
				g.ChangeUsr(true, this.K, false, false, false, false, GroupUsr.StatusEnum.Member, DateTime.Now, true);

		}
		public bool IsEnabledPromoter()
		{
			if (this.IsPromoter)
			{
				foreach (Promoter p in this.Promoters(new ColumnSet(Promoter.Columns.Status)))
				{
					if (p.IsEnabled)
						return true;
				}
				return false;
			}
			else
				return false;
		}
		public void AddToPromotersGroup(GroupUsr gu, Group g)
		{
			if (gu == null || !(gu.Status.Equals(GroupUsr.StatusEnum.Member) || gu.Status.Equals(GroupUsr.StatusEnum.Exited) || gu.Status.Equals(GroupUsr.StatusEnum.Barred)))
			{
				GroupUsr gu1 = g.ChangeUsr(false, this.K, false, false, false, false, Bobs.GroupUsr.StatusEnum.Member, DateTime.Now, false);
				gu1.Favourite = true;
				gu1.Update();
				CommentAlert.Enable(this, g.K, Model.Entities.ObjectType.Group);

				Mailer m = new Mailer();
				m.UsrRecipient = this;
				m.Subject = "We've added you to the private promoters group";
				m.RedirectUrl = g.Url();
				m.Body = @"
<p>
Congratulations on activating your promoter account. We've now given you 
access to the private promoter group, where you can chat with event 
organisers from all around the world.
</p>

<p>
What we are trying to do with this forum is allow people a space to ask 
questions, seek advice and share knowledge about promoting events
across the world.
</p>

<p>
<b>Please do:</b> 
</p>

<p>
* Ask questions <br>
* Give answers <br>
</p>

<p>
<b>Please do not: </b>
</p>

<p>
* Spam this forum with promotional messages <br>
* Post about your upcoming events, <b>this includes 'industry invites'</b> 
</p>

<p>
If you break these rules you will be removed from the group without warning.
</p>
";
				m.Send();

			}
		}
		#endregion

		#region WhereAmI
		public Event WhereAmI(DateTime dateTime)
		{
			//First try to match exactly
			Q eventStartTime = null;


			if (dateTime.Hour < 10)
			{
				//before 9:00am - morning
				//all day event today OR evening event yesterday OR morning event today
				eventStartTime = new Or(
					new And(
					new Q(Event.Columns.DateTime, dateTime.Date.AddDays(-1)),
					new Q(Event.Columns.StartTime, Event.StartTimes.Evening)
					),
					new And(
					new Q(Event.Columns.DateTime, dateTime.Date),
					new Q(Event.Columns.StartTime, Event.StartTimes.Morning)
					)
					);
			}
			else if (dateTime.Hour < 18)
			{
				//before 6:00pm but after 10:00am - late morning / early afternoon
				//all day event today OR morning event today OR daytime event today
				eventStartTime = new Or(
					new And(
					new Q(Event.Columns.DateTime, dateTime.Date),
					new Q(Event.Columns.StartTime, Event.StartTimes.Morning)
					),
					new And(
					new Q(Event.Columns.DateTime, dateTime.Date),
					new Q(Event.Columns.StartTime, Event.StartTimes.Daytime)
					)
					);
			}
			else if (dateTime.Hour < 20)
			{
				//before 8:00pm but after 6:00pm - afternoon / evening
				//all day event today OR daytime event today OR evening event today
				eventStartTime = new Or(
					new And(
					new Q(Event.Columns.DateTime, dateTime.Date),
					new Q(Event.Columns.StartTime, Event.StartTimes.Daytime)
					),
					new And(
					new Q(Event.Columns.DateTime, dateTime.Date),
					new Q(Event.Columns.StartTime, Event.StartTimes.Evening)
					)
					);
			}
			else
			{
				//after 8pm - evening - all day event today OR evening event today
				eventStartTime = new And(
					new Q(Event.Columns.DateTime, dateTime.Date),
					new Q(Event.Columns.StartTime, Event.StartTimes.Evening)
					);
			}
			Query qExact = new Query();
			qExact.TableElement = Event.UsrAttendedJoin;
			qExact.QueryCondition =
				new And(
				new Q(Usr.Columns.K, this.K),
				eventStartTime
				);
			EventSet esExact = new EventSet(qExact);
			if (esExact.Count > 1)
				return null;
			else if (esExact.Count == 1)
				return esExact[0];
			else
			{
				//loose matching
				//Any event today or yesterday
				eventStartTime = new Or(
					new Q(Event.Columns.DateTime, dateTime.Date),
					new Q(Event.Columns.DateTime, dateTime.Date.AddDays(-1))
					);
				Query qLoose = new Query();
				qLoose.TableElement = Event.UsrAttendedJoin;
				qLoose.QueryCondition = new And(
					new Q(Usr.Columns.K, this.K),
					eventStartTime
					);
				EventSet esLoose = new EventSet(qLoose);
				if (esLoose.Count > 1)
					return null;
				else if (esLoose.Count == 1)
					return esLoose[0];
				else
				{
					//very loose matching
					//any event tomorrow, today, yesterday, or the day before
					eventStartTime = new Or(
						new Q(Event.Columns.DateTime, dateTime.Date.AddDays(1)),
						new Q(Event.Columns.DateTime, dateTime.Date),
						new Q(Event.Columns.DateTime, dateTime.Date.AddDays(-1)),
						new Q(Event.Columns.DateTime, dateTime.Date.AddDays(-2))
						);
					Query qVeryLoose = new Query();
					qVeryLoose.TableElement = Event.UsrAttendedJoin;
					qVeryLoose.QueryCondition = new And(
						new Q(Usr.Columns.K, this.K),
						eventStartTime
						);
					EventSet esVeryLoose = new EventSet(qVeryLoose);
					if (esVeryLoose.Count > 1)
						return null;
					else if (esVeryLoose.Count == 1)
						return esVeryLoose[0];
					else
						return null;
				}
			}


		}
		#endregion

		#region CanDelete
		public bool CanDelete(Comment comment)
		{
			return CanDelete(comment, comment.Thread, comment.Thread.Group == null ? null : comment.Thread.Group.GetGroupUsr(this));
		}
		public bool CanDelete(Comment comment, Thread thread, GroupUsr gu)
		{
			if (this.K == comment.UsrK)
				return true;
			if ((thread == null || !thread.Private) && this.IsJunior)
				return true;
			if (gu != null && gu.Moderator)
				return true;
			return false;
		}
		public bool CanDelete(Thread thread)
		{
			if (this.K == thread.UsrK)
				return true;
			if (!thread.Private && this.IsJunior)
				return true;
			return false;
		}
		public bool CanDelete(Banner BannerToDelete)
		{
			if (this.IsAdmin)
				return true;
			else if (this.K == BannerToDelete.UsrK)
				return true;
			else if (BannerToDelete.Promoter != null && this.IsPromoterK(BannerToDelete.PromoterK))
				return true;
			else
				return false;
		}
		#endregion

		#region CanEdit
		public bool CanEdit(Article a)
		{
			if (this.IsAdmin)
				return true;
			if (this.IsSuper)
				return a.Status.Equals(Article.StatusEnum.Edit) || a.Status.Equals(Article.StatusEnum.New);
			if (this.K == a.OwnerUsrK
				|| (a.Event != null && this.IsEnabledPromoterOfEventConfirmed(a.Event))
				|| (a.Venue != null && this.IsEnabledPromoterOfConfirmedVenue(a.Venue)))
				return a.Status.Equals(Article.StatusEnum.New);

			return false;
		}
		public bool CanEdit(Comp c)
		{
			if (this.IsAdmin)
				return true;
			if (this.IsEnabledPromoter(c.PromoterK))
				return c.Status.Equals(Comp.StatusEnum.New);

			return false;
		}
		public bool CanEdit(Misc m)
		{
			if (this.IsAdmin)
				return true;
			if (this.K == m.UsrK)
				return true;
			if (m.Promoter != null && this.IsEnabledPromoter(m.PromoterK))
				return true;

			return false;
		}
		public bool CanEdit(Event e)
		{
			if (this.IsSuper || this.IsAdmin)
				return true;
			if (this.K == e.OwnerUsrK && (e.DateTime > DateTime.Now.AddMonths(-1) || e.AddedDateTime > DateTime.Now.AddMonths(-1)))
				return true;
			if (this.IsEnabledPromoterOfEventConfirmed(e))
				return true;
			return false;
		}
		public bool CanEdit(Venue v)
		{
			if (this.IsSuper || this.IsAdmin)
				return true;
			if (this.K == v.OwnerUsrK && v.AddedDateTime > DateTime.Now.AddMonths(-1))
				return true;
			if (this.IsEnabledPromoterOfConfirmedVenue(v))
				return true;
			return false;
		}
		public bool CanEdit(Banner b)
		{
			if (this.IsAdmin)
				return true;
			else if (this.K == b.UsrK)
				return true;
			else if (b.Promoter != null && this.IsPromoterK(b.PromoterK))
				return true;
			else
				return false;
		}
		public bool CanEdit(Brand b)
		{
			if (this.IsSuper)
				return true;
			else if (this.K == b.OwnerUsrK && b.DateTimeCreated > DateTime.Now.AddMonths(-1))
				return true;
			else if (this.IsEnabledPromoterOfConfirmedBrand(b))
				return true;
			else
				return false;
		}
		#endregion

		#region CanUploadTo(Gallery g)
		public bool CanUploadTo(Gallery g)
		{

		//	int totalPhotosIncludingProcessing = g.GetTotalPhotosIncludingProcessing();

		//	if (totalPhotosIncludingProcessing >= 100)
		//		return false;

			if (this.IsSuper)
				return true;

			if (this.K != g.OwnerUsrK)
				return false;

		//	if (!this.IsSpotter && totalPhotosIncludingProcessing >= 10)
			//	return false;

			return true;
		}
		#endregion

		#region HasEntered
		public bool HasEntered(int CompK)
		{
			try
			{
				CompEntry ce = new CompEntry(CompK, this.K);
				return true;
			}
			catch
			{
				return false;
			}
		}
		public CompEntry CompEntry(int CompK)
		{
			if (HasEntered(CompK))
				return new CompEntry(CompK, this.K);
			else
				return null;
		}
		#endregion

		#region AttendEvent(int EventK, bool AddendOrNot, Transaction transaction)
		public void AttendEvent(int EventK, bool AddendOrNot, object spotterObject, Transaction transaction)
		{
			bool doneChange = false;
			bool spotter = false;
			bool changeSpotter = spotterObject != null;
			if (changeSpotter)
				spotter = (bool)spotterObject;

			Event ev = new Event(EventK);

			try
			{
				UsrEventAttended u = new UsrEventAttended(this.K, EventK);
				if (!AddendOrNot)
				{
					this.Guestlist(EventK, false, transaction);
					u.Delete(transaction);
					doneChange = true;
				}
				if (changeSpotter && spotter != u.Spotter)
				{
					u.Spotter = spotter;
					u.Update();
					u.Event.UpdateHasSpotter(transaction);
				}
			}
			catch
			{
				if (AddendOrNot)
				{
					UsrEventAttended newU = new UsrEventAttended();
					newU.UsrK = this.K;
					newU.EventK = EventK;
					newU.SendUpdate = true;
					newU.Spotter = spotter;
					newU.Update(transaction);
					if (spotter && !newU.Event.HasSpotter)
						newU.Event.UpdateHasSpotter(transaction);

					doneChange = true;

					if (FacebookConnected && FacebookStoryAttendEvent)
					{
						FacebookPost.CreateAttendEvent(this, ev);
					}
				//	if (FacebookConnected && FacebookEventAttend)
				//	{
				//		FacebookPost.AddEvent(this, ev, true);
				//	}
				}
			}
			if (doneChange)
				Event.UpdateUsrAttendCountStatic(EventK);
		}
		#endregion

		#region Facebook
		public FacebookGraphAPI Facebook
		{
			get
			{
				if (facebook == null)
				{
					FacebookGraphAPI facebookTemp = new FacebookGraphAPI(Apps.Dsi, FacebookUID.Value, FacebookAccessToken);
					
					JObject user = facebookTemp.GetObject(this.FacebookUID.ToString(), null);

					if (user.Value<long>("id") != FacebookUID)
					{
						throw new Exception("Inconsistant facebook login");
					}

					facebook = facebookTemp;
				}
				return facebook;
			}
		}
		FacebookGraphAPI facebook;
		#endregion

		#region Guestlist

		public bool HasGuestlistEvents(DateTime date, Event.StartTimes startTime)
		{
			Query q = new Query();
			q.NoLock = true;
			q.Columns = new ColumnSet(UsrEventGuestlist.Columns.EventK);
			q.TopRecords = 1;
			q.TableElement = new Join(UsrEventGuestlist.Columns.EventK, Event.Columns.K);
			q.QueryCondition = new And(
				new Q(UsrEventGuestlist.Columns.UsrK, this.K),
				new Q(Event.Columns.DateTime, date),
				new Q(Event.Columns.StartTime, startTime)
			);
			return (new EventSet(q)).Count == 1;
		}
		public EventSet GuestlistEvents(DateTime date, Event.StartTimes startTime)
		{
			Query q = new Query();
			q.NoLock = true;
			q.TableElement = new Join(UsrEventGuestlist.Columns.EventK, Event.Columns.K);
			q.QueryCondition = new And(
				new Q(UsrEventGuestlist.Columns.UsrK, this.K),
				new Q(Event.Columns.DateTime, date),
				new Q(Event.Columns.StartTime, startTime)
			);
			return new EventSet(q);
		}

		public bool IsOnGuestlist(int EventK)
		{
			try
			{
				UsrEventGuestlist g = new UsrEventGuestlist(this.K, EventK);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public void Guestlist(int EventK, bool GuestlistOrNot, Transaction transaction)
		{
			Event e = new Event(EventK);
			if (!e.HasGuestlist)
				return;
			if (!e.GuestlistOpen)
				return;
			if (e.GuestlistFull && GuestlistOrNot)
				return;

			try
			{
				UsrEventGuestlist u = new UsrEventGuestlist(this.K, EventK);
				if (!GuestlistOrNot)
				{
					u.Delete(transaction);
				}
			}
			catch
			{
				if (GuestlistOrNot)
				{
					UsrEventGuestlist newG = new UsrEventGuestlist();
					newG.DateTime = DateTime.Now;
					newG.UsrK = this.K;
					newG.EventK = EventK;
					newG.Update();

					try
					{
						UsrEventAttended u = new UsrEventAttended(this.K, EventK);
					}
					catch
					{
						this.AttendEvent(EventK, true, null, transaction);
					}
				}
			}
			e.UpdateGuestlistCount(transaction);
		}
		#endregion

		#region PhotoMe(Photo p, bool MeOrNot, Transaction transaction)
		public void PhotoMe(Photo p, bool MeOrNot, Transaction transaction)
		{
			try
			{
				UsrPhotoMe u = new UsrPhotoMe(this.K, p.K);
				if (!MeOrNot)
				{
					u.Delete(transaction);
					p.UpdateUsrCount(transaction);
					this.UpdatePhotosMeCount(true, transaction);
					p.Usr.UpdateSpottings(transaction);
				}
			}
			catch
			{
				if (MeOrNot)
				{
					if (p.EventK > 0)
						this.AttendEvent(p.EventK, true, null, transaction);

					UsrPhotoMe newU = new UsrPhotoMe();
					newU.UsrK = this.K;
					newU.PhotoK = p.K;
					newU.Update(transaction);

					p.UpdateUsrCount(transaction);
					this.UpdatePhotosMeCount(true, transaction);
					p.Usr.UpdateSpottings(transaction);
					
					if (FacebookConnected && FacebookStorySpotted)
					{
						FacebookPost.CreateSpotted(this, p);
					}
				}
			}
		}
		#endregion

		#region DateMatches
		public UsrDateSet DateMatches
		{
			get
			{
				if (dateMatches == null)
				{
					Query q = new Query();
					q.QueryCondition = new And(new Q(UsrDate.Columns.UsrK, this.K), new Q(UsrDate.Columns.Match, UsrDate.MatchEnum.Yes));
					dateMatches = new UsrDateSet(q);
				}
				return dateMatches;
			}
			set
			{
				dateMatches = value;
			}
		}
		private UsrDateSet dateMatches;
		#endregion

		#region UpdateBuddyCount()
		public void UpdateBuddyCount(Transaction transaction)
		{
			Query q = new Query();
			q.NoLock = true;
			q.QueryCondition = new And(new Q(Buddy.Columns.UsrK, this.K), new Q(Buddy.Columns.FullBuddy, true));
			q.ReturnCountOnly = true;
			BuddySet bs = new BuddySet(q);
			if (this.BuddyCount != bs.Count || bs.Count == 0)
			{
				this.BuddyCount = bs.Count;
				this.LastBuddyChange = DateTime.Now;
				this.Update(transaction);
			}
		}
		#endregion
		#region UpdateEventCount(Transaction transaction)
		public void UpdateEventCount(Transaction transaction)
		{
			Query q = new Query();
			q.QueryCondition = new Q(Event.Columns.OwnerUsrK, this.K);
			q.ReturnCountOnly = true;
			EventSet es = new EventSet(q);
			this.EventCount = es.Count;
			this.Update(transaction);
		}
		#endregion
		#region IsChatterBox
		public bool IsChatterBox
		{
			get
			{
				return (this.ChatMessageCount + this.CommentCount) > 200;
			}
		}
		#endregion
		#region IsNewUser
		public bool IsNewUser
		{
			get
			{
				return DateTimeSignUp > DateTime.Now.AddMonths(-2);
			}
		}
		#endregion
		#region IsSuperUser
		public bool IsSuperUser
		{
			get
			{
				return this.ChatMessageCount > 400 && this.CommentCount > 100 && this.BuddyCount > 10 && this.EventCount > 10;
			}
		}
		#endregion
		#region IsDsiRegular
		public bool IsDsiRegular
		{
			get
			{
				return (this.ChatMessageCount > 30 || this.CommentCount > 20) && this.BuddyCount > 2 && this.EventCount > 0;
			}
		}
		#endregion

		#region HeString
		public string HeString(bool capital)
		{
			return (IsMale ? ((capital ? "H" : "h") + "e") : (IsFemale ? ((capital ? "S" : "s") + "he") : ((capital ? "H" : "h") + "e/she")));
		}
		#endregion
		#region HisString
		public string HisString(bool capital)
		{
			return (IsMale ? ((capital ? "H" : "h") + "is") : (IsFemale ? ((capital ? "H" : "h") + "er") : ((capital ? "H" : "h") + "is/her")));
		}
		#endregion
		#region HimString
		public string HimString(bool capital)
		{
			return (IsMale ? ((capital ? "H" : "h") + "im") : (IsFemale ? ((capital ? "H" : "h") + "er") : ((capital ? "H" : "h") + "im/her")));
		}
		#endregion

		#region Rollover
		public static ColumnSet LinkColumns
		{
			get
			{
				return new ColumnSet(
					Usr.Columns.K,
					Usr.Columns.NickName,
					Usr.Columns.FirstName,	// used where unconfirmed ExDirectory Buddy added by real name
					Usr.Columns.LastName,	// used where unconfirmed ExDirectory Buddy added by real name
					Usr.Columns.Email,		// used where unconfirmed ExDirectory Buddy added by Email address
					Usr.Columns.Pic,
					Usr.Columns.FacebookUID,
					Usr.Columns.IsAdmin,
					Usr.Columns.DateTimeSignUp,
					Usr.Columns.AdminLevel,
					Usr.Columns.IsSpotter,
					//Usr.Columns.Donate1Icon,
					//Usr.Columns.Donate1Expire,
					//Usr.Columns.Donate2Icon,
					//Usr.Columns.Donate2Expire,
					Usr.Columns.ChatMessageCount,
					Usr.Columns.CommentCount,
					Usr.Columns.BuddyCount,
					Usr.Columns.EventCount,
					Usr.Columns.IsProSpotter,
					Usr.Columns.IsDj,
					Usr.Columns.SpottingsTotal,
					Usr.Columns.IsLoggedOn,
					Usr.Columns.DateTimeLastPageRequest,
					Usr.Columns.DateTimeLastChatMessage,
					Usr.Columns.ExtraIcon,
					Usr.Columns.ExtraExpire,
					Usr.Columns.HasTicket,
					Usr.Columns.LastTicketEventDateTime,
					Usr.Columns.ExDirectory,
					Usr.Columns.RolloverDonationIconK
				);
			}
		}

		void mouseOverTextAppend(StringBuilder sb)
		{
			sb.Append("stmu('");
			if (HasPic)
			{
				if (!Pic.Equals(Guid.Empty))
					sb.Append(Pic.ToString());
				else
					sb.Append(FacebookUID.ToString());
			}
			else
				sb.Append(Photo.ContentDisabledIcon.ToString());
			sb.Append("',");
			sb.Append(StmuParams);
			sb.Append(");");
		}
		string mouseOverText
		{
			get
			{
				if (HasPic)
					return "stmu('" + PicOrFacebookUID.ToString() + "'," + StmuParams + ");";
				else
					return "stmu('" + Photo.ContentDisabledIcon.ToString() + "'," + StmuParams + ");";

			}
		}
		public string PicOrFacebookUID
		{
			get
			{
				if (!Pic.Equals(Guid.Empty))
					return Pic.ToString();
				else
					return FacebookUID.ToString();
			}
		}
		string mouseOverTextNoPic
		{
			get
			{
				return "stmun(" + StmuParams + ");";
			}
		}

		#region Spotter Stuff
		public int SpotterStars
		{
			get
			{
				if (this.SpottingsTotal < 50) return 0;
				else if (this.SpottingsTotal < 125) return 1;
				else if (this.SpottingsTotal < 250) return 2;
				else if (this.SpottingsTotal < 500) return 3;
				else if (this.SpottingsTotal < 750) return 4;
				else if (this.SpottingsTotal < 1000) return 5;
				else if (this.SpottingsTotal < 1250) return 6;
				else if (this.SpottingsTotal < 1500) return 7;
				else if (this.SpottingsTotal < 1750) return 8;
				else if (this.SpottingsTotal < 2000) return 9;
				else if (this.SpottingsTotal < 2250) return 10;
				else if (this.SpottingsTotal < 2500) return 11;
				else if (this.SpottingsTotal < 2750) return 12;
				else if (this.SpottingsTotal < 3000) return 13;
				else if (this.SpottingsTotal < 3250) return 14;
				else if (this.SpottingsTotal < 3500) return 15;
				else if (this.SpottingsTotal < 3750) return 16;
				else if (this.SpottingsTotal < 4000) return 17;
				else if (this.SpottingsTotal < 4250) return 18;
				else if (this.SpottingsTotal < 4500) return 19;
				else if (this.SpottingsTotal < 4750) return 20;
				else if (this.SpottingsTotal < 5000) return 21;
				else if (this.SpottingsTotal < 7500) return 22;
				else if (this.SpottingsTotal < 10000) return 23;
				else if (this.SpottingsTotal < 20000) return 24;
				else return 25;
			}
		}
		public string SpotterStatus(bool includeImA, bool includeSpotterType, bool fontBold)
		{
			string spotterType = includeSpotterType ? (IsProSpotter ? " Pro Spotter" : " Spotter") : "";
			string boldOpen = fontBold ? "<b>" : "";
			string boldClose = fontBold ? "</b>" : "";

			switch (this.SpotterStars)
			{
				case 0: return (includeImA ? "I'm a " : "") + boldOpen + "Newbie" + boldClose + spotterType;
				case 1: return (includeImA ? "I'm a " : "") + boldOpen + "Good" + boldClose + spotterType;
				case 2: return (includeImA ? "I'm a " : "") + boldOpen + "Great" + boldClose + spotterType;
				case 3: return (includeImA ? "I'm a " : "") + boldOpen + "Super" + boldClose + spotterType;
				case 4: return (includeImA ? "I'm a " : "") + boldOpen + "Super Duper" + boldClose + spotterType;
				case 5: return (includeImA ? "I'm a " : "") + boldOpen + "Mega" + boldClose + spotterType;
				case 6: return (includeImA ? "I'm a " : "") + boldOpen + "Super Mega" + boldClose + spotterType;
				case 7: return (includeImA ? "I'm an " : "") + boldOpen + "Awesome" + boldClose + spotterType;
				case 8: return (includeImA ? "I'm a " : "") + boldOpen + "Super Awesome" + boldClose + spotterType;
				case 9: return (includeImA ? "I'm an " : "") + boldOpen + "Ace" + boldClose + spotterType;
				case 10: return (includeImA ? "I'm a " : "") + boldOpen + "Super Ace" + boldClose + spotterType;
				case 11: return (includeImA ? "I'm a " : "") + boldOpen + "Wicked" + boldClose + spotterType;
				case 12: return (includeImA ? "I'm a " : "") + boldOpen + "Super Wicked" + boldClose + spotterType;
				case 13: return (includeImA ? "I'm a " : "") + boldOpen + "Nitro" + boldClose + spotterType;
				case 14: return (includeImA ? "I'm a " : "") + boldOpen + "Super Nitro" + boldClose + spotterType;
				case 15: return (includeImA ? "I'm an " : "") + boldOpen + "Elite" + boldClose + spotterType;
				case 16: return (includeImA ? "I'm a " : "") + boldOpen + "Super Elite" + boldClose + spotterType;
				case 17: return (includeImA ? "I'm a " : "") + boldOpen + "Hero" + boldClose + spotterType;
				case 18: return (includeImA ? "I'm a " : "") + boldOpen + "Super Hero" + boldClose + spotterType;
				case 19: return (includeImA ? "I'm a " : "") + boldOpen + "Master" + boldClose + spotterType;
				case 20: return (includeImA ? "I'm a " : "") + boldOpen + "Super Master" + boldClose + spotterType;
				case 21: return (includeImA ? "I'm a " : "") + boldOpen + "God" + boldClose + spotterType;
				case 22: return (includeImA ? "I'm a " : "") + boldOpen + "Super God" + boldClose + spotterType;
				case 23: return (includeImA ? "I'm a " : "") + boldOpen + "LEGEND" + boldClose + spotterType;
				case 24: return (includeImA ? "I'm a " : "") + boldOpen + "MYTH" + boldClose + spotterType;
				case 25: return (includeImA ? "I'm a " : "") + boldOpen + "FISTATRON" + spotterType;
				default: throw new NotImplementedException();
			}
		}
		public string SpotterCode
		{
			get
			{
				return this.K.ToString("0000-0000");
			}
		}
		#region SpotterIconPath
		public string SpotterIconPath
		{
			get { return "/gfx/spotters1/" + SpotterIconPathShort + ".png"; }
		}
		private int SpotterIconPathShort
		{
			get
			{
				if (this.IsProSpotter)
				{
					switch (this.SpotterStars)
					{
						case 0: return 44825;
						case 1: return 31051;
						case 2: return 24263;
						case 3: return 75085;
						case 4: return 60316;
						case 5: return 75753;
						case 6: return 23767;
						case 7: return 86447;
						case 8: return 98467;
						case 9: return 36392;
						case 10: return 32079;
						case 11: return 29335;
						case 12: return 86924;
						case 13: return 38837;
						case 14: return 12574;
						case 15: return 73265;
						case 16: return 40854;
						case 17: return 72620;
						case 18: return 64777;
						case 19: return 88649;
						case 20: return 93120;
						case 21: return 12207;
						case 22: return 41618;
						case 23: return 65663;
						case 24: return 83912;
						case 25: return 71368;
						default: throw new NotImplementedException();
					}
				}
				else
				{
					switch (this.SpotterStars)
					{
						case 0: return 87316;
						case 1: return 77075;
						case 2: return 48987;
						case 3: return 59916;
						case 4: return 43069;
						case 5: return 75466;
						case 6: return 56841;
						case 7: return 48035;
						case 8: return 60831;
						case 9: return 20977;
						case 10: return 62468;
						case 11: return 63704;
						case 12: return 16104;
						case 13: return 10073;
						case 14: return 26478;
						case 15: return 32306;
						case 16: return 11845;
						case 17: return 84129;
						case 18: return 95327;
						case 19: return 69120;
						case 20: return 13319;
						case 21: return 58917;
						case 22: return 23666;
						case 23: return 30477;
						case 24: return 70437;
						case 25: return 93303;
						default: throw new NotImplementedException();
					}
				}
			}
		}
		#endregion
		#endregion
		public string StmuParams
		{
			get
			{
				// the order here is important - broken back down in javascript
				List<bool> flags = new List<bool>(13);
				flags.Add(this.IsDsiRegular);
				flags.Add(this.IsChatterBox);
				flags.Add(this.IsSuperUser);
				flags.Add(this.IsSpotter);
				flags.Add(this.IsProSpotter);
				flags.Add(this.LoggedInNow);
				flags.Add(this.ChattingNow);
				flags.Add(this.IsNewUser);
				flags.Add(this.AdminLevel.Equals(Usr.AdminLevels.Junior) || this.AdminLevel.Equals(Usr.AdminLevels.Senior) || this.AdminLevel.Equals(Usr.AdminLevels.Super));
				flags.Add(this.IsAdmin);
				flags.Add(this.HasTicket && this.LastTicketEventDateTime > DateTime.Now.AddDays(-1));
				flags.Add(this.ExtraIconInUse == 1);
				flags.Add(this.IsDonationIconLegend);
				flags.Add(this.IsDj.HasValue && this.IsDj.Value);

				long flagsValue = flags.ToArray().ToBitwiseLong();

				// third parameter optional
				return flagsValue.ToString() + "," + (this.RolloverDonationIconK.HasValue ? this.RolloverDonationIconK.Value.ToString() : "0") +
					(this.IsSpotter || this.IsProSpotter ? "," + this.SpotterIconPathShort.ToString() : "");
			}
		}

		public void LinkAppend(StringBuilder sb, bool newWindow, params string[] par)
		{
			sb.Append("<a");
			sb.AppendAttribute("href", Url(par));
			sb.Append(" ");
			RolloverAppend(sb);
			if (newWindow)
				sb.AppendAttribute("target", "_blank");
			sb.Append(">");
			sb.Append(NickNameDisplay);
			sb.Append("</a>");
		}
		public string Link(params string[] par)
		{
			StringBuilder sb = new StringBuilder();
			LinkAppend(sb, false, par);
			return sb.ToString();
			//return "<a href=\"" + Url(par) + "\" " + Rollover + ">" + NickNameDisplay + "</a>";
		}
		public string LinkNewWindow(params string[] par)
		{
			StringBuilder sb = new StringBuilder();
			LinkAppend(sb, true, par);
			return sb.ToString();
			//return "<a href=\"" + Url(par) + "\" " + Rollover + " target=\"_blank\">" + NickNameDisplay + "</a>";
		}
		public string LinkEmail(params string[] par)
		{
			return "<a href=\"[LOGIN(" + Url(par) + ")]\">" + NickNameDisplay + "</a>";
		}
		public string LinkEmailFull
		{
			get
			{
				return @"<p>User: " + LinkEmail() + "</p>";
			}
		}
		public void RolloverAppend(StringBuilder sb)
		{
			sb.Append(" onmouseover=\"");
			mouseOverTextAppend(sb);
			sb.Append("\"");
			sb.AppendAttribute("onmouseout", "htm();");
		}
		public string Rollover
		{
			get
			{
				return "onmouseover=\"" + mouseOverText + "\" onmouseout=\"htm();\"";

			}
		}
		public string RolloverNoPic
		{
			get
			{
				return "onmouseover=\"" + mouseOverTextNoPic + "\" onmouseout=\"htm();\"";
			}
		}
		public string RolloverMouseOverTextNoPic
		{
			get
			{
				return mouseOverTextNoPic;
			}
		}

		public void MakeRolloverNoPic(HtmlControl c)
		{
			c.Attributes["onmouseover"] = mouseOverTextNoPic;
			c.Attributes["onmouseout"] = "htm();";
		}
		public void MakeRolloverNoPic(WebControl c)
		{
			c.Attributes["onmouseover"] = mouseOverTextNoPic;
			c.Attributes["onmouseout"] = "htm();";
		}

		public void MakeRollover(StringBuilder b)
		{
			b.Append(" onmouseover=\"");
			b.Append(mouseOverText);
			b.Append("\" onmouseout=\"htm();\"");
		}
		public void MakeRollover(HtmlTextWriter w)
		{
			w.AddAttribute("onmouseover", mouseOverText);
			w.AddAttribute("onmouseout", "htm();");
		}
		public void MakeRollover(HtmlControl c)
		{
			c.Attributes["onmouseover"] = mouseOverText;
			c.Attributes["onmouseout"] = "htm();";
		}
		public void MakeRollover(WebControl c)
		{
			c.Attributes["onmouseover"] = mouseOverText;
			c.Attributes["onmouseout"] = "htm();";
		}
		#endregion

		#region PresenceIconAppend
		public void PresenceIconAppend(StringBuilder sb, string shading)
		{
			if (ChattingNow || LoggedInNow)
			{
				sb.Append("<img");
				sb.AppendAttribute("src", ChattingNow ? "/gfx/chat-chatting.png" : "/gfx/chat-online.png");
				sb.AppendAttribute("border", "0");
				sb.AppendAttribute("width", ChattingNow ? "13" : "9");
				sb.AppendAttribute("height", "11");
				sb.AppendAttribute("onmouseover", ChattingNow ? "sttd(4);" : "sttd(3);");
				sb.AppendAttribute("onmouseout", "htm();");
				sb.AppendAttribute("class", "ChatClientRoomPresence");
				sb.Append(" />");
			}
		}
		#endregion

		#region StripAll()
		public void StripAll()
		{
			Query qUsrEventGuestlist = new Query();
			qUsrEventGuestlist.QueryCondition = new Q(UsrEventGuestlist.Columns.UsrK, this.K);
			UsrEventGuestlistSet uegs = new UsrEventGuestlistSet(qUsrEventGuestlist);
			foreach (UsrEventGuestlist ueg in uegs)
			{
				ueg.Delete();
				ueg.Update();
				ueg.Event.UpdateGuestlistCount();
			}

			//UsrPlaceVisit
			Delete UsrPlaceVisitDelete = new Delete(
				TablesEnum.UsrPlaceVisit,
				new Q(UsrPlaceVisit.Columns.UsrK, this.K)
				);
			UsrPlaceVisitDelete.Run();

			//UsrEventAttended
			Delete UsrEventAttendedDelete = new Delete(
				TablesEnum.UsrEventAttended,
				new Q(UsrEventAttended.Columns.UsrK, this.K)
				);
			UsrEventAttendedDelete.Run();

			//UsrPhotoFavourite
			Delete UsrPhotoFavouriteDelete = new Delete(
				TablesEnum.UsrPhotoFavourite,
				new Q(UsrPhotoFavourite.Columns.UsrK, this.K)
				);
			UsrPhotoFavouriteDelete.Run();

			//UsrPhotoMe
			Delete UsrPhotoMeDelete = new Delete(
				TablesEnum.UsrPhotoMe,
				new Q(UsrPhotoMe.Columns.UsrK, this.K)
				);
			UsrPhotoMeDelete.Run();

			//UsrMusicTypeFavourite
			Delete UsrMusicTypeFavouriteDelete = new Delete(
				TablesEnum.UsrMusicTypeFavourite,
				new Q(UsrMusicTypeFavourite.Columns.UsrK, this.K)
				);
			UsrMusicTypeFavouriteDelete.Run();

			//Buddy / Buddy reverse;
			BuddySet bs = new BuddySet(new Query(new Or(new Q(Buddy.Columns.UsrK, this.K), new Q(Buddy.Columns.BuddyUsrK, this.K))));
			foreach (Buddy b in bs)
				b.DeleteAll(null);

			//UsrDate
			Delete UsrDateDelete = new Delete(
				TablesEnum.UsrDate,
				new Or(new Q(UsrDate.Columns.UsrK, this.K), new Q(UsrDate.Columns.DateUsrK, this.K))
				);
			UsrDateDelete.Run();

			//Owners - Event
			EventSet esOwner = new EventSet(new Query(new Q(Event.Columns.OwnerUsrK, this.K)));
			foreach (Event e in esOwner)
			{
				e.DeleteAllUsr(Usr.Current);
				//e.OwnerUsrK=4;
				//e.Update();
			}

			BrandSet bsOwner = new BrandSet(new Query(new Q(Brand.Columns.OwnerUsrK, this.K)));
			foreach (Brand b in bsOwner)
			{
				b.DeleteAll(null);
				//b.OwnerUsrK=4;
				//b.Update();
			}

			//Owners - Venue
			VenueSet vsOwner = new VenueSet(new Query(new Q(Venue.Columns.OwnerUsrK, this.K)));
			foreach (Venue v in vsOwner)
			{
				v.DeleteAllUsr(Usr.Current);
				//v.OwnerUsrK=4;
				//v.Update();
			}

			//PhotoReview ???
			PhotoReviewSet prs = new PhotoReviewSet(new Query(new Q(PhotoReview.Columns.UsrK, this.K)));
			foreach (PhotoReview pr in prs)
			{
				pr.Delete();
				pr.Update();
				pr.Photo.UpdateStats(null);
			}

			//Galleries
			GallerySet gs = new GallerySet(new Query(new Q(Gallery.Columns.OwnerUsrK, this.K)));
			foreach (Gallery g in gs)
				g.DeleteAll(null);

			//Photos
			PhotoSet ps = new PhotoSet(new Query(new Q(Photo.Columns.UsrK, this.K)));
			foreach (Photo p in ps)
				p.DeleteAll(null);

			//Aticles
			ArticleSet ars = new ArticleSet(new Query(new Q(Article.Columns.OwnerUsrK, this.K)));
			foreach (Article a in ars)
				a.DeleteAll(null);

			Guid oldPic = Pic;

			this.PicPhotoK = 0;
			this.PicState = "";
			this.Pic = Guid.Empty;
			this.PicOriginal = Guid.Empty;
			//this.NickName=Usr.GetCompliantNickName("user-"+this.K.ToString());
			this.PersonalStatement = "";
			this.IsSingle = false;
			this.EnhancedSecurity = true;
			this.IsSpotter = false;
			Random r = new Random();
			this.LoginString = Cambro.Misc.Utility.GenRandomText(6, r);
			this.SetPassword(Cambro.Misc.Utility.GenRandomText(10, r), false);

			int k = this.K;

			//Usr
			this.Update();

			if (oldPic != Guid.Empty)
				Storage.RemoveFromStore(Storage.Stores.Pix, oldPic, "jpg");

		}
		#endregion

		#region DeleteAll(Transaction transaction)
		public void DeleteAll(Transaction transaction)
		{
			if (!this.Bob.DbRecordExists)
				return;


			Query qUsrEventGuestlist = new Query();
			qUsrEventGuestlist.QueryCondition = new Q(UsrEventGuestlist.Columns.UsrK, this.K);
			UsrEventGuestlistSet uegs = new UsrEventGuestlistSet(qUsrEventGuestlist);
			foreach (UsrEventGuestlist ueg in uegs)
			{
				ueg.Delete(transaction);
				ueg.Event.UpdateGuestlistCount(transaction);
			}

			//Promoters
			Query PromoterQ = new Query();
			PromoterQ.TableElement = Promoter.UsrJoin;
			PromoterQ.QueryCondition = new Q(Usr.Columns.K, this.K);
			PromoterSet promoters = new PromoterSet(PromoterQ);

			Delete PromoterUsrDelete = new Delete(
				TablesEnum.PromoterUsr,
				new Q(PromoterUsr.Columns.UsrK, this.K)
			);
			PromoterUsrDelete.CommandTimeout = 3600;
			PromoterUsrDelete.Run(transaction);

			foreach (Promoter p in promoters)
			{
				p.AdminUsrs = null;
				if (p.AdminUsrs.Count == 0)
				{
					p.DeleteAll(transaction);
				}
				else if (p.PrimaryUsrK == this.K)
				{
					p.PrimaryUsrK = p.AdminUsrs[0].K;
					p.Update(transaction);
				}
			}

			//UsrPlaceVisit
			Delete UsrPlaceVisitDelete = new Delete(
				TablesEnum.UsrPlaceVisit,
				new Q(UsrPlaceVisit.Columns.UsrK, this.K)
				);
			UsrPlaceVisitDelete.CommandTimeout = 3600;
			UsrPlaceVisitDelete.Run(transaction);

			//UsrEventAttended
			Delete UsrEventAttendedDelete = new Delete(
				TablesEnum.UsrEventAttended,
				new Q(UsrEventAttended.Columns.UsrK, this.K)
				);
			UsrEventAttendedDelete.CommandTimeout = 3600;
			UsrEventAttendedDelete.Run(transaction);

			//UsrPhotoFavourite
			Delete UsrPhotoFavouriteDelete = new Delete(
				TablesEnum.UsrPhotoFavourite,
				new Q(UsrPhotoFavourite.Columns.UsrK, this.K)
				);
			UsrPhotoFavouriteDelete.CommandTimeout = 3600;
			UsrPhotoFavouriteDelete.Run(transaction);

			//UsrPhotoMe
			PhotoSet psMe = this.PhotosMe(new ColumnSet(Photo.Columns.K, Photo.Columns.EventK, Photo.Columns.FirstUsrK, Photo.Columns.UsrCount), 0);
			foreach (Photo p in psMe)
			{
				this.PhotoMe(p, false, transaction);
			}

			//UsrMusicTypeFavourite
			Delete UsrMusicTypeFavouriteDelete = new Delete(
				TablesEnum.UsrMusicTypeFavourite,
				new Q(UsrMusicTypeFavourite.Columns.UsrK, this.K)
				);
			UsrMusicTypeFavouriteDelete.CommandTimeout = 3600;
			UsrMusicTypeFavouriteDelete.Run(transaction);

			//Buddy / Buddy reverse;
			BuddySet bs = new BuddySet(new Query(new Or(new Q(Buddy.Columns.UsrK, this.K), new Q(Buddy.Columns.BuddyUsrK, this.K))));
			foreach (Buddy b in bs)
				b.DeleteAll(transaction);

			//ChatMessageFrom
			//	Delete ChatMessageFromDelete = new Delete(
			//		TablesEnum.ChatMessage,
			//		new Q(ChatMessage.Columns.FromUsrK,this.K)
			//	);
			//	ChatMessageFromDelete.Run(transaction);

			//ChatMessageTo
			//	Delete ChatMessageToDelete = new Delete(
			//		TablesEnum.ChatMessage,
			//		new Q(ChatMessage.Columns.ToUsrK, this.K)
			//	);
			//	ChatMessageToDelete.Run(transaction);

			//UsrDate
			Delete UsrDateDelete = new Delete(
				TablesEnum.UsrDate,
				new Or(new Q(UsrDate.Columns.UsrK, this.K), new Q(UsrDate.Columns.DateUsrK, this.K))
				);
			UsrDateDelete.CommandTimeout = 3600;
			UsrDateDelete.Run(transaction);

			//Owners - Event
			EventSet esOwner = new EventSet(new Query(new Q(Event.Columns.OwnerUsrK, this.K)));
			foreach (Event e in esOwner)
			{
				e.OwnerUsrK = 8;
				e.Update(transaction);
			}

			BrandSet bsOwner = new BrandSet(new Query(new Q(Brand.Columns.OwnerUsrK, this.K)));
			foreach (Brand b in bsOwner)
			{
				b.OwnerUsrK = 8;
				b.Update(transaction);
			}

			//Owners - Venue
			VenueSet vsOwner = new VenueSet(new Query(new Q(Venue.Columns.OwnerUsrK, this.K)));
			foreach (Venue v in vsOwner)
			{
				v.OwnerUsrK = 8;
				v.Update(transaction);
			}

			//Remove EnabledByUsrK in Photos that this usr has enabled
			PhotoSet pEnableds = new PhotoSet(new Query(new Q(Photo.Columns.EnabledByUsrK, this.K)));
			foreach (Photo p in pEnableds)
			{
				p.EnabledByUsrK = 0;
				p.Update(transaction);
			}

			//ThreadUsr
			Delete ThreadUsrDelete = new Delete(
				TablesEnum.ThreadUsr,
				new Or(new Q(ThreadUsr.Columns.UsrK, this.K), new Q(ThreadUsr.Columns.InvitingUsrK, this.K))
				);
			ThreadUsrDelete.CommandTimeout = 3600;
			ThreadUsrDelete.Run(transaction);

			//CommentAlert
			Delete CommentAlertDelete = new Delete(
				TablesEnum.CommentAlert,
				new Q(CommentAlert.Columns.UsrK, this.K)
			);
			CommentAlertDelete.CommandTimeout = 3600;
			CommentAlertDelete.Run(transaction);


			//GroupUsr
			Delete GroupUsrDelete = new Delete(
				TablesEnum.GroupUsr,
				new Q(GroupUsr.Columns.UsrK, this.K)
				);
			GroupUsrDelete.CommandTimeout = 3600;
			GroupUsrDelete.Run(transaction);


			//Threads?
			ThreadSet ts = new ThreadSet(new Query(new Q(Thread.Columns.UsrK, this.K)));
			foreach (Thread t in ts)
				t.DeleteAll(transaction);

			Bobs.Update uLastPostUsrK = new Bobs.Update();
			uLastPostUsrK.Changes.Add(new Assign(Thread.Columns.LastPostUsrK, 0));
			uLastPostUsrK.Table = TablesEnum.Thread;
			uLastPostUsrK.Where = new Q(Thread.Columns.LastPostUsrK, this.K);
			uLastPostUsrK.CommandTimeout = 3600;
			uLastPostUsrK.Run(transaction);

			Bobs.Update uFirstParticipantUsrK = new Bobs.Update();
			uFirstParticipantUsrK.Changes.Add(new Assign(Thread.Columns.FirstParticipantUsrK, 0));
			uFirstParticipantUsrK.Table = TablesEnum.Thread;
			uFirstParticipantUsrK.Where = new Q(Thread.Columns.FirstParticipantUsrK, this.K);
			uFirstParticipantUsrK.CommandTimeout = 3600;
			uFirstParticipantUsrK.Run();

			//Comments?
			CommentSet cs = new CommentSet(new Query(new Q(Comment.Columns.UsrK, this.K)));
			foreach (Comment c in cs)
				c.DeleteAll(transaction);

			//PhotoReview ???
			PhotoReviewSet prs = new PhotoReviewSet(new Query(new Q(PhotoReview.Columns.UsrK, this.K)));
			foreach (PhotoReview pr in prs)
			{
				pr.Delete(transaction);
				pr.Photo.UpdateStats(transaction);
			}

			//Galleries
			GallerySet gs = new GallerySet(new Query(new Q(Gallery.Columns.OwnerUsrK, this.K)));
			foreach (Gallery g in gs)
				g.DeleteAll(transaction);

			//Photos
			PhotoSet ps = new PhotoSet(new Query(new Q(Photo.Columns.UsrK, this.K)));
			foreach (Photo p in ps)
				p.DeleteAll(transaction);

			//Aticles
			ArticleSet ars = new ArticleSet(new Query(new Q(Article.Columns.OwnerUsrK, this.K)));
			foreach (Article a in ars)
				a.DeleteAll(transaction);

			int k = this.K;

			//Usr-AddedByUsrK
			UsrSet usrsAddedByUsr = new UsrSet(new Query(new Q(Usr.Columns.AddedByUsrK, this.K)));
			foreach (Usr u in usrsAddedByUsr)
			{
				u.AddedByUsrK = 0;
				u.Update(transaction);
			}

			Guid oldPic = this.Pic;

			//Usr
			this.Delete(transaction);

			if (oldPic != Guid.Empty)
				Storage.RemoveFromStore(Storage.Stores.Pix, oldPic, "jpg");

		}
		#endregion

		#region Url
		public string UrlFragment
		{
			get
			{
				return "members";
			}
		}
		public string UrlFilterPart
		{
			get
			{
				return UrlFragment + "/" + this.NickName.ToLower();
			}
		}
		public string Url()
		{
			return Url(null);
		}
		public string Url(params string[] par)
		{
			return UrlInfo.MakeUrl(UrlFilterPart, null, par);
		}
		public string UrlApp(string Appilcation, params string[] par)
		{
			return UrlInfo.MakeUrl(UrlFilterPart, Appilcation, par);
		}
		#endregion

		#region IsBuddy
		public bool IsBuddy(Usr potentialBuddy)
		{
			return this.K == potentialBuddy.K || this.BuddiesFullKs.Contains(potentialBuddy.K);
		}
		#endregion

		#region UsrPageAttendedList
		public static ColumnSet UsrPageAttendedListColumns
		{
			get
			{
				return new ColumnSet(
					UsrEventAttended.Columns.Spotter,
					Event.Columns.K,
					Event.Columns.SpotterRequest,
					Event.Columns.IsTicketsAvailable,
					Event.Columns.UrlFragment,
					Event.Columns.VenueK,
					Event.Columns.DateTime,
					Event.Columns.Pic,
					Event.Columns.StartTime,
					Event.Columns.LivePhotos,
					Event.Columns.Name,
					Event.Columns.MusicTypesString,
					Event.Columns.VenueK,
					Venue.Columns.K,
					Venue.LinkColumns,
					Venue.Columns.Pic,
					Venue.Columns.PlaceK,
					Place.Columns.K,
					Place.LinkColumns,
					Place.Columns.Pic,
					Place.Columns.CountryK,
					Country.Columns.K,
					Country.Columns.FriendlyName
				);
			}
		}

		public static TableElement UsrPageAttendedListPerformJoins(TableElement tIn)
		{
			TableElement t = new Join(tIn, Venue.Columns.K, Event.Columns.VenueK);
			t = new Join(t, Place.Columns.K, Venue.Columns.PlaceK);
			t = new Join(t, Country.Columns.K, Place.Columns.CountryK);
			return t;
		}

		#endregion

		#region Joins

		public static Join PrivateMessageThreadJoin
		{
			get
			{
				return new Join(new Join(Usr.Columns.K, ThreadUsr.Columns.UsrK), Thread.Columns.K, ThreadUsr.Columns.ThreadK);
			}
		}

		public static Join CommentAlertJoin
		{
			get
			{
				return new Join(Usr.Columns.K, CommentAlert.Columns.UsrK);
			}
		}

		public static Join ThreadJoin
		{
			get
			{
				return new Join(new Join(Usr.Columns.K, Comment.Columns.UsrK), Thread.Columns.K, Comment.Columns.ThreadK);
			}
		}

		/// <summary>
		/// This retreives the Usr record of the user that is the buddy (not the user that initiated the buddy record).
		/// </summary>
		public static Join BuddyJoin
		{
			get
			{
				return new Join(Usr.Columns.K, Buddy.Columns.BuddyUsrK);
			}
		}

		/// <summary>
		/// This retreives the Usr record of the user that initiated the buddy record.
		/// </summary>
		public static Join BuddyUsrJoin
		{
			get
			{
				return new Join(Usr.Columns.K, Buddy.Columns.UsrK);
			}
		}

		public static Join PlaceVisitJoin
		{
			get
			{
				return new Join(new Join(Usr.Columns.K, UsrPlaceVisit.Columns.UsrK), Place.Columns.K, UsrPlaceVisit.Columns.PlaceK);
			}
		}
		public static Join PlacesMusicTypesJoin
		{
			get
			{
				return new Join(
					new Join(Usr.Columns.K, UsrPlaceVisit.Columns.UsrK),
					new TableElement(TablesEnum.UsrMusicTypeFavourite),
					QueryJoinType.Inner,
					Usr.Columns.K, UsrMusicTypeFavourite.Columns.UsrK);
			}
		}

		public static UsrSet GetUsrsMatchingPlaceAndMusicTypes(int placeK, int[] musicTypeKs, bool usrKsOnly)
		{
			Query q = new Query();
			q.QueryCondition = new And(
				new Or(
					new Q(Usr.Columns.HomePlaceK, placeK),
					new Q(UsrPlaceVisit.Columns.PlaceK, placeK)),
				new Or(
					new Q(Usr.Columns.FavouriteMusicTypeK, musicTypeKs),
					new Q(UsrMusicTypeFavourite.Columns.MusicTypeK, musicTypeKs)));

			if (usrKsOnly)
				q.Columns = new ColumnSet(Usr.Columns.K);

			q.Distinct = true;
			q.DistinctColumn = Usr.Columns.K;
			q.TableElement = PlacesMusicTypesJoin;
			return new UsrSet(q);
		}

		public int[] GetRandomNewBuddyUsrKs(int placeK, int musicTypeK)
		{
			Query q = new Query();
			q.QueryCondition = new And(
				new Or(
					new Q(Usr.Columns.HomePlaceK, placeK),
					new Q(UsrPlaceVisit.Columns.PlaceK, placeK)),
				new Or(
					new Q(Usr.Columns.FavouriteMusicTypeK, musicTypeK),
					new Q(UsrMusicTypeFavourite.Columns.MusicTypeK, musicTypeK)),
				new Q(Usr.Columns.CommentCount, QueryOperator.GreaterThan, 100),
				new Q(Usr.Columns.DateTimeLastAccess, QueryOperator.GreaterThan, Common.Time.Today.AddDays(-31)), // logged in in the last month
				new Q(Usr.Columns.PicPhotoK, QueryOperator.GreaterThan, 0), // has a photo
				new Q(Buddy.Columns.UsrK, QueryOperator.IsNull, null) // not already a buddy
				);

			q.Columns = new ColumnSet(Usr.Columns.K);
			q.Distinct = true;
			q.DistinctColumn = Usr.Columns.K;
			q.TableElement = new Join(
				PlacesMusicTypesJoin,
				new TableElement(TablesEnum.Buddy),
				QueryJoinType.Left,
				new And(new Q(Usr.Columns.K, Buddy.Columns.BuddyUsrK, true), new Q(Buddy.Columns.UsrK, this.K)));

			q.TopRecords = 20;
			q.OrderBy = new OrderBy(OrderBy.OrderDirection.Random);

			return new UsrSet(q).ToList().ConvertAll(u => u.K).ToArray();
		}

		public static Join UsrEventAttendedJoin
		{
			get
			{
				return new Join(Usr.Columns.K, UsrEventAttended.Columns.UsrK);
			}
		}

		public static Join UsrEventGuestlistJoin
		{
			get
			{
				return new Join(Usr.Columns.K, UsrEventGuestlist.Columns.UsrK);
			}
		}

		public static Join EventSpotterJoin
		{
			get
			{
				return new Join(
					Usr.UsrEventAttendedJoin,
					new TableElement(Bobs.TablesEnum.Event),
					QueryJoinType.Inner,
					new And(
						new Q(UsrEventAttended.Columns.EventK, Event.Columns.K, true),
						new Q(UsrEventAttended.Columns.Spotter, true)));
			}
		}

		public static Join EventAttendedJoin
		{
			get
			{
				return new Join(Usr.UsrEventAttendedJoin, Event.Columns.K, UsrEventAttended.Columns.EventK);
			}
		}

		public static Join PhotoMeJoin
		{
			get
			{
				return new Join(new Join(Usr.Columns.K, UsrPhotoMe.Columns.UsrK), Photo.Columns.K, UsrPhotoMe.Columns.PhotoK);
			}
		}
		public static Join PhotoFavouritesJoin
		{
			get
			{
				return new Join(new Join(Usr.Columns.K, UsrPhotoFavourite.Columns.UsrK), Photo.Columns.K, UsrPhotoFavourite.Columns.PhotoK);
			}
		}
		public static Join PromoterJoin
		{
			get
			{
				return new Join(new Join(Usr.Columns.K, PromoterUsr.Columns.UsrK), Promoter.Columns.K, PromoterUsr.Columns.PromoterK);
			}
		}
		#endregion

		#region LoginUrl, LoginAndTransfer
		/// <summary>
		/// This URL will log the user in to the site and transfer them to the User Prefs screen
		/// </summary>
		public string LoginUrl
		{
			get
			{
				return LoginAndTransfer("");
			}
		}
		/// <summary>
		/// As LoginUrl, but without the "http://"+Vars.DomainName
		/// </summary>
		public string LoginUrlShort
		{
			get
			{
				return LoginAndTransferShort("");
			}
		}
		/// <summary>
		/// This URL will log the user in to the site and transfer them to the url specified
		/// </summary>
		public string LoginAndTransfer(string UrlToTransfer)
		{
			if (UrlToTransfer.Length == 0)
				return "http://" + Vars.DomainName + LoginAndTransferShort(UrlToTransfer);
			else
				return "http://" + Vars.DomainName + LoginAndTransferShort(UrlToTransfer);
		}
		/// <summary>
		/// As LoginAndTransferShort, but without the "http://"+Vars.DomainName
		/// </summary>
		public string LoginAndTransferShort(string UrlToTransfer)
		{
			string loginStr = "/login-" + this.K.ToString() + "-" + this.LoginString.ToLower();

			if (!UrlToTransfer.StartsWith("/"))
				loginStr += "/";

			loginStr += UrlToTransfer;

			return loginStr;

			//			ArrayList al = new ArrayList();
			//			al.Add("K");
			//			al.Add(this.LoginString+this.K.ToString());
			//			if (UrlToTransfer.Length>0)
			//			{
			//				al.Add("T");
			//				al.Add(UrlToTransfer);
			//			}
			//			string a = UrlInfo.UrlWithParamsStatic("Login",(string[])al.ToArray(typeof(string)));
			//			return a;
		}
		#endregion

		#region security functions

	//	public void LogOutNow()
	//	{
		//	Cambro.Web.Helpers.DeleteCookie("SpottedAuthFix");
			//HttpContext.Current.Items["Usr_Current"] = null;
	//	}

		#region LogInAsThisUser

		public void LogInAsThisUserDontSetCookieNew()
		{
			HttpContext.Current.Items["Usr_Current"] = this;
			this.LoginCount++;
			this.DateTimeLastAccess = DateTime.Now;
			this.Update();
			Prefs.InitialiseFromUsr(this);
		}
		public void LogInAsThisUserNew()
		{
			LogInAsThisUserDontSetCookieNew();
			SetAuthCookieFixedNew();
		}
		//public void LogInAsThisUser(bool RememberDetails)
		//{
		//    HttpContext.Current.Items["Usr_Current"] = this;
		//    this.LoginCount++;
		//    this.DateTimeLastAccess = DateTime.Now;
		//    this.Update();
		//    Prefs.InitialiseFromUsr(this);
		//    LogInAsThisUserSilent(RememberDetails);
		//}
		#endregion
		#region LogInAsThisUserSilent
		//public void LogInAsThisUserSilent(bool RememberDetails)
		//{
		//    HttpContext.Current.Items["Usr_Current"] = this;
		//    SetAuthCookieFixed();
		//}
		#endregion
		#region SetAuthCookieFixedNew
		public void SetAuthCookieFixedNew()
		{
			HttpCookie cookie = this.GetAuthCookie();
			HttpContext.Current.Response.Cookies.Add(cookie);
		}
		#endregion
		#region GetAuthCookie
		public HttpCookie GetAuthCookie()
		{
			FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
				3,
				this.K.ToString() + 
				"&" + HttpUtility.UrlEncode(this.Email) + 
				"&" + this.GetAuthenticationHash() + 
				"&" + (this.FacebookConnected ? this.FacebookUID.ToString() : "0") + 
				"&" + ((int)Settings.CaptchaEnabledStatus).ToString(),
				DateTime.Now,
				DateTime.Now.AddYears(50),
				true,
				"",
				FormsAuthentication.FormsCookiePath);
			string ticketEncrypted = FormsAuthentication.Encrypt(ticket);
			string fullCookie = ticketEncrypted + "-" + this.K.ToString() + "-" + (this.FacebookConnected ? this.FacebookUID.ToString() : "0");

			//HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, fullCookie);
			HttpCookie cookie = new HttpCookie("SpottedAuthFix", fullCookie);
			cookie.HttpOnly = false;
			cookie.Path = FormsAuthentication.FormsCookiePath;
			cookie.Secure = FormsAuthentication.RequireSSL;
			cookie.Expires = ticket.Expiration;

			return cookie;
		}
		#endregion
		#region RedirectUser
		public static void RedirectUser()
		{
			string s = "/login" + HttpContext.Current.Request.Path.ToString();

			HttpContext.Current.Response.Redirect(s);
		}
		public static void RedirectUserLogout()
		{
			string s = "/logout" + HttpContext.Current.Request.Path.ToString();

			HttpContext.Current.Response.Redirect(s);
		}
		#endregion
		#region KickUserIfNotAdmin
		public static void KickUserIfNotAdmin(string errorText)
		{
			if (Usr.Current != null)
			{
				Usr cUsr = Usr.Current;
				if (cUsr != null && cUsr.IsAdmin)
				{
					return;
				}
			}

			Usr.RedirectUserLogout();
		}
		#endregion
		#region KickUserIfNotLoggedIn
		public static void KickUserIfNotLoggedIn()
		{
			KickUserIfNotLoggedIn("");
		}
		public static void KickUserIfNotLoggedIn(string errorText)
		{
			if (Usr.Current != null)
			{
				return;
			}
			Usr.RedirectUser();
		}
		#endregion
		
		#endregion

		public static bool CurrentAuthCookieHasError()
		{
			Usr u = Usr.Current;

			if (u == null && HttpContext.Current.Items["CurrentAuthCookieHasError"] != null && (bool)HttpContext.Current.Items["CurrentAuthCookieHasError"])
				return true;

			return false;
		}

		#region Usr.Current - the currently logged in user
		public static Usr Current
		{
			get
			{
				return null;

				if (HttpContext.Current == null)
				{
					return null;
				}

				if (HttpContext.Current.Items["Usr_Current"] != null)
					return (Usr)HttpContext.Current.Items["Usr_Current"];

				if (!HttpContext.Current.User.Identity.IsAuthenticated)
				{
					return null;
				}

				try
				{
					if (HttpContext.Current.Items["Usr_Current"] == null)
					{
						string usrStr = HttpContext.Current.User.Identity.Name;
						string[] split = usrStr.Split('&');
						if (split.Length != 5)
						{
							HttpContext.Current.Items["CurrentAuthCookieHasError"] = true;
							return null;
						}
						else
						{
							int usrK = int.Parse(split[0]);
							Usr u = new Usr(int.Parse(split[0]));
							Guid usrAuthHash = u.GetAuthenticationHash();

							string emailFromClient = HttpUtility.UrlDecode(split[1]);
							Guid authHashFromClient = new Guid(split[2]);
							long facebookUidFromClient = long.Parse(split[3]);
							Settings.CaptchaEnabledStatusOption captchaEnabledStatusFromClient = (Settings.CaptchaEnabledStatusOption)int.Parse(split[4]);

							long facebookUidFromUsr = u.FacebookConnected ? (u.FacebookUID.HasValue ? u.FacebookUID.Value : 0) : 0;

							if (u.Email == emailFromClient && usrAuthHash == authHashFromClient && facebookUidFromUsr == facebookUidFromClient && Settings.CaptchaEnabledStatus == captchaEnabledStatusFromClient)
							{
								HttpContext.Current.Items["Usr_Current"] = u;
							}
							else
							{
								HttpContext.Current.Items["CurrentAuthCookieHasError"] = true;
								return null;
							}
						}
					}
					return (Usr)HttpContext.Current.Items["Usr_Current"];
				}
				catch
				{
					HttpContext.Current.Items["CurrentAuthCookieHasError"] = true;
					return null;
				}
			}
			set
			{
				HttpContext.Current.Items.Remove("Usr_Current");
			}
		}
		#endregion

		#region FullName
		/// <summary>
		/// Full name - shown on the site.
		/// </summary>
		public string FullName
		{
			get
			{
				string name = "";
				if (!string.IsNullOrEmpty(this.FirstName))
				{
					name = FirstName;
					if (!string.IsNullOrEmpty(this.LastName))
						name += " ";
				}
				if (!string.IsNullOrEmpty(this.LastName))
					name += LastName;

				return name;
			}
		}
		#endregion

		#region FullNameDetailed
		public string FullNameDetailed
		{
			get
			{
				return FullName + " (" + NickName + ")";
			}
		}
		#endregion

		#region Buddy functions - AddBuddy, RemoveBuddy, DenyBuddy, HasBuddy
		public void AddBuddy(Usr buddyUsr, AddBuddySource source, Buddy.BuddyFindingMethod foundByMethod, string skeletonName)
		{
			AddBuddy(buddyUsr, true, source, foundByMethod, skeletonName);
		}
		public void AddBuddy(Usr buddyUsr, bool canBuddyInvite, AddBuddySource source, Buddy.BuddyFindingMethod foundByMethod, string skeletonName)
		{
			AddBuddy(buddyUsr, canBuddyInvite, true, source, foundByMethod, skeletonName);
		}
		public void AddBuddy(Usr buddyUsr, bool canBuddyInvite, bool sendNotificationEmailIfNewBuddyRequest, AddBuddySource source, Buddy.BuddyFindingMethod foundByMethod, string skeletonName)
		{
			ValidateBuddyUsr(buddyUsr, true);

			Buddy b;
			try
			{
				b = new Buddy(this.K, buddyUsr.K);
			}
			catch (BobNotFound)
			{
				b = new Buddy();
				b.UsrK = this.K;
				b.BuddyUsrK = buddyUsr.K;
				b.CanBuddyInvite = canBuddyInvite;
				b.BuddyFoundByMethod = foundByMethod;
				b.SkeletonName = skeletonName;
				try
				{
					Buddy reverseBuddy = new Buddy(buddyUsr.K, this.K);
					reverseBuddy.FullBuddy = true;
					reverseBuddy.Denied = false;
					reverseBuddy.CanInvite = canBuddyInvite;
					b.CanInvite = reverseBuddy.CanBuddyInvite;
					b.FullBuddy = true;
					b.Denied = false;
					reverseBuddy.Update();

					if (this.FacebookConnected && this.FacebookStoryNewBuddy)
					{
						try
						{
							FacebookPost.CreateNewBuddy(this, buddyUsr, true);
						}
						catch { }
					}

					if (buddyUsr.FacebookConnected && buddyUsr.FacebookStoryNewBuddy)
					{
						try
						{
							FacebookPost.CreateNewBuddy(buddyUsr, this, false);
						}
						catch { }
					}
				}
				catch (BobNotFound)
				{
					if (source == AddBuddySource.BuddyButtonClick ||
						source == AddBuddySource.UsrPageSendPrivateMessage)
					{
						Usr.IncrementSpamBotDefeaterCounter(SpamBotDefeaterCounter.BuddyRequests, this.K);
					}

					b.CanInvite = false;
					b.FullBuddy = false;

					if (sendNotificationEmailIfNewBuddyRequest)
					{
						Mailer m = new Mailer();
						m.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
						m.Subject = this.NickName + " has invited you to " + this.HisString(false) + " buddy list";
						
						if (this.HasPic)
						{
							m.Body += "<a href=\"[LOGIN(" + this.Url() + ")]\"><img src=\"" + this.PicPath + "\" style=\"margin-left:5px;\" class=\"BorderBlack All\" width=\"100\" height=\"100\" align=\"right\" vspace=\"6\" border=\"0\"></a>";
						}
						m.Body += "<h1>" + HttpUtility.HtmlEncode(this.NickName) + " has invited you to " + this.HisString(false) + " buddy list</h1>";
						m.Body += "<p>Name: <a href=\"[LOGIN(" + this.Url() + ")]\">" + HttpUtility.HtmlEncode(this.FirstName) + " " + HttpUtility.HtmlEncode(this.LastName) + "</a>.</p>";
						m.Body += "<p>To add " + HttpUtility.HtmlEncode(this.NickName) + " to your buddy list, view " + this.HisString(false) + " <a href=\"[LOGIN(" + this.Url() + ")]\">profile</a> and click the \"Add " + HttpUtility.HtmlEncode(this.NickName) + " to my buddy list\" button.</p>";
						m.OverrideLoginLink = buddyUsr.LoginAndTransfer(this.Url());
						m.UsrRecipient = buddyUsr;
						m.To = buddyUsr.Email;
						m.Send();
					}
				}
				b.Update();
				b.Usr.UpdateBuddyCount(null);
				b.BuddyUsr.UpdateBuddyCount(null);
			}
		}
		public void RemoveBuddy(int BuddyUsrK)
		{
			try
			{
				Buddy b = new Buddy(this.K, BuddyUsrK);
				b.Delete();
				b.Update();
				b.Usr.UpdateBuddyCount(null);
				b.BuddyUsr.UpdateBuddyCount(null);
			}
			catch { }

			try
			{
				Buddy b = new Buddy(BuddyUsrK, this.K);
				b.FullBuddy = false;
				b.Update();
				b.Usr.UpdateBuddyCount(null);
				b.BuddyUsr.UpdateBuddyCount(null);
			}
			catch { }
		}
		public void DenyBuddy(int buddyUsrK)
		{
			DenyBuddy(buddyUsrK, true);
		}
		public void DenyBuddy(int buddyUsrK, bool deny)
		{
			if (deny == true)
			{
				try
				{
					Buddy b = new Buddy(buddyUsrK, this.K);
					b.Denied = true;
					b.FullBuddy = false;
					b.CanInvite = false;
					b.CanBuddyInvite = false;
					b.Update();
				}
				catch (BobNotFound)
				{
					Buddy b = new Buddy();
					b.UsrK = buddyUsrK;
					b.BuddyUsrK = this.K;
					b.Denied = true;
					b.Update();
				}
				try
				{
					Buddy b = new Buddy(this.K, buddyUsrK);
					b.Delete();
					b.Update();
				}
				catch (BobNotFound) { }
			}
			else
			{
				try
				{
					Buddy b = new Buddy(buddyUsrK, this.K);
					b.Denied = false;
					b.Update();
				}
				catch (BobNotFound) { }
			}
		}
		public bool HasBuddy(int buddyUsrK)
		{
			try
			{
				Buddy b = new Buddy(this.K, buddyUsrK);
				return true;
			}
			catch
			{
				return false;
			}
		}
		public bool CanBuddyInvite(int buddyUsrK)
		{
			try
			{
				Buddy b = new Buddy(this.K, buddyUsrK);
				return b.CanBuddyInvite;
			}
			catch
			{
				return false;
			}
		}
		public bool CanInvite(int buddyUsrK)
		{
			try
			{
				Buddy b = new Buddy(this.K, buddyUsrK);
				return b.CanInvite;
			}
			catch
			{
				return false;
			}
		}
		public bool HasFullBuddy(int buddyUsrK)
		{
			try
			{
				Buddy b = new Buddy(this.K, buddyUsrK);
				return b.FullBuddy;
			}
			catch
			{
				return false;
			}
		}
		private void ValidateBuddyUsr(Usr buddyUsr)
		{
			ValidateBuddyUsr(buddyUsr, false);
		}
		private void ValidateBuddyUsr(Usr buddyUsr, bool allowSkeleton)
		{
			if (buddyUsr.K <= 0 || buddyUsr.K == this.K || (!allowSkeleton && buddyUsr.IsSkeleton) || buddyUsr.Banned || (buddyUsr.K == 8 && !Common.Properties.IsDevelopmentEnvironment))
				throw new DsiUserFriendlyException("Not a valid buddy.");
		}

		public void SetBuddyInvite(Usr buddyUsr, bool allowBuddyInvite, AddBuddySource source, string skeletonName)
		{
			SetBuddyInvite(buddyUsr, allowBuddyInvite, source, null, skeletonName);
		}
		public void SetBuddyInvite(Usr buddyUsr, bool allowBuddyInvite, AddBuddySource source, Buddy.BuddyFindingMethod? foundByMethod, string skeletonName)
		{
			if (allowBuddyInvite && !foundByMethod.HasValue)
			{
				throw new ArgumentNullException("foundByMethod", "Must have a method when allowBuddyInvite is true");
			}

			ValidateBuddyUsr(buddyUsr);

			if (this.HasBuddy(buddyUsr.K))
			{
				try
				{
					Buddy buddy = new Buddy(this.K, buddyUsr.K);
					buddy.CanBuddyInvite = allowBuddyInvite;
					buddy.Update();
					#region update reverse buddy
					try
					{
						Buddy reverseBuddy = new Buddy(buddyUsr.K, this.K);
						reverseBuddy.CanInvite = allowBuddyInvite;
						reverseBuddy.Update();
					}
					catch (BobNotFound) { }
					#endregion
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}
			else if (allowBuddyInvite)
			{
				this.AddBuddy(buddyUsr, source, foundByMethod.Value, skeletonName );
			}
		}

		#region HasBuddiesFull
		public bool HasBuddiesFull
		{
			get
			{
				if (!hasBuddiesFull.HasValue)
				{
					Query q = new Query();
					q.NoLock = true;
					q.QueryCondition = BuddiesFullQ;
					q.ReturnCountOnly = true;
					q.TopRecords = 1;
					BuddySet bs = new BuddySet(q);
					hasBuddiesFull = bs.Count > 0;
				}
				return hasBuddiesFull.Value;
			}
		}
		bool? hasBuddiesFull;
		#endregion
		#region BuddiesFullCount
		public int BuddiesFullCount
		{
			get
			{
				if (!buddiesFullCount.HasValue)
				{
					Query q = new Query();
					q.NoLock = true;
					q.QueryCondition = BuddiesFullQ;
					q.ReturnCountOnly = true;
					BuddySet bs = new BuddySet(q);
					buddiesFullCount = bs.Count;
				}
				return buddiesFullCount.Value;
			}
		}
		int? buddiesFullCount;
		#endregion
		#region BuddiesPendingCount
		public int BuddiesPendingCount
		{
			get
			{
				if (!buddiesPendingCount.HasValue)
				{
					Query q = new Query();
					q.NoLock = true;
					q.QueryCondition = BuddiesPendingQ;
					q.TableElement = BuddiesPendingQJoin;
					q.ReturnCountOnly = true;
					BuddySet bs = new BuddySet(q);
					buddiesPendingCount = bs.Count;
				}
				return buddiesPendingCount.Value;
			}
		}
		int? buddiesPendingCount;
		#endregion
		public UsrSet BuddiesFull
		{
			get
			{
				if (buddiesFull == null)
				{
					buddiesFull = new UsrSet(
						new Query(
							Usr.BuddyJoin,
							BuddiesFullQ,
							new OrderBy(Usr.Columns.NickName),
							-1
						)
					);
				}
				return buddiesFull;
			}
		}
		UsrSet buddiesFull;

		public UsrSet BuddiesNamesAndKs
		{
			get
			{
				if (buddiesNamesAndKs == null)
				{
					Query buddiesNamesAndKsQuery = new Query(BuddiesFullQ);
					buddiesNamesAndKsQuery.TableElement = Usr.BuddyJoin;
					buddiesNamesAndKsQuery.Columns = new ColumnSet(Usr.Columns.K, Usr.Columns.NickName);
					buddiesNamesAndKsQuery.OrderBy = new OrderBy(Usr.Columns.NickName);
					buddiesNamesAndKs = new UsrSet(buddiesNamesAndKsQuery);
				}
				return buddiesNamesAndKs;
			}
		}
		UsrSet buddiesNamesAndKs;

		public Q BuddiesFullQ
		{
			get
			{
				return GetBuddiesFullQ(this.K);
			}
		}

		public static Q GetBuddiesFullQ(int usrK)
		{
			return new And(
				new Q(Buddy.Columns.UsrK, usrK),
				new Q(Buddy.Columns.FullBuddy, true),
				new Q(Buddy.Columns.Denied, false)
			);
		}

		public Q BuddiesPendingQ
		{
			get
			{
				return new And(
					//new Q(Usr.Columns.IsSkeleton, false),
					new Q(Buddy.Columns.UsrK, this.K),
					new Q(Buddy.Columns.FullBuddy, false)
				);
			}
		}
 
		public static TableElement BuddiesPendingQJoin
		{
			get { return new Join(Buddy.Columns.BuddyUsrK, Usr.Columns.K); }
		}

		#region UsrsWhoHavePendingBuddyRequestsForMe
		private List<Usr> usrsWhoHavePendingBuddyRequestsForMe;
		public List<Usr> UsrsWhoHavePendingBuddyRequestsForMe
		{
			get
			{
				if (usrsWhoHavePendingBuddyRequestsForMe == null)
				{
					Query q = new Query();
					q.TableElement = new Join(Usr.Columns.K, Buddy.Columns.UsrK, new And(new Q(Usr.Columns.Banned, false), new Q(Buddy.Columns.BuddyUsrK, this.K)));
					q.QueryCondition = new And(new Q(Buddy.Columns.FullBuddy, false), new Q(Buddy.Columns.Denied, false));
					usrsWhoHavePendingBuddyRequestsForMe = new UsrSet(q).ToList();
					if (this.AddedByUsrK > 0 && usrsWhoHavePendingBuddyRequestsForMe.Find(u => u.K == this.AddedByUsrK) == null)
					{
						bool alreadyFullBuddyOrDenied = false;
						try
						{
							Buddy b = new Buddy(this.AddedByUsrK, this.K);
							alreadyFullBuddyOrDenied = b.FullBuddy || b.Denied;
						}
						catch (BobNotFound) { }
						if (!alreadyFullBuddyOrDenied)
						{
							usrsWhoHavePendingBuddyRequestsForMe.Insert(0, this.AddedByUsr);
						}
					}
				}
				return usrsWhoHavePendingBuddyRequestsForMe;
			}
		}
		#endregion

		#region GroupsWhoHavePendingInvitationsForMe
		private GroupSet groupsWhoHavePendingInvitationsForMe;
		public GroupSet GroupsWhoHavePendingInvitationsForMe
		{
			get
			{
				if (groupsWhoHavePendingInvitationsForMe == null)
				{
					Query q = new Query();
					q.TableElement = new Join(Group.Columns.K, GroupUsr.Columns.GroupK, new Q(GroupUsr.Columns.UsrK, this.K));
					q.QueryCondition = new Q(GroupUsr.Columns.Status, GroupUsr.StatusEnum.Invite);
					groupsWhoHavePendingInvitationsForMe = new GroupSet(q);
				}
				return groupsWhoHavePendingInvitationsForMe;
			}
		}
		#endregion
		#endregion

		#region GetCompliantNickName
		public static string GetUniqueNickName(string inNick, int excludeUsrK)
		{
			string compliantNick = Usr.GetCompliantNickName(inNick);
			if (compliantNick.Length > 20)
				compliantNick = compliantNick.Substring(0, 20);

			int numberToAdd = 0;
			string newNick = compliantNick;

			int count = 1;
			while (count > 0)
			{
				if (numberToAdd > 999)
				{
					if (compliantNick.Length > 15)
						compliantNick = compliantNick.Substring(0, 15);
					newNick = compliantNick + "-" + numberToAdd;
				}
				else if (numberToAdd > 99)
				{
					if (compliantNick.Length > 16)
						compliantNick = compliantNick.Substring(0, 16);
					newNick = compliantNick + "-" + numberToAdd;
				}
				else if (numberToAdd > 9)
				{
					if (compliantNick.Length > 17)
						compliantNick = compliantNick.Substring(0, 17);
					newNick = compliantNick + "-" + numberToAdd;
				}
				else if (numberToAdd > 0)
				{
					if (compliantNick.Length > 18)
						compliantNick = compliantNick.Substring(0, 18);
					newNick = compliantNick + "-" + numberToAdd;
				}
				Query q = new Query();
				if (excludeUsrK > 0)
					q.QueryCondition = new And(new Q(Usr.Columns.NickName, newNick), new Q(Usr.Columns.K, QueryOperator.NotEqualTo, excludeUsrK));
				else
					q.QueryCondition = new Q(Usr.Columns.NickName, newNick);
				q.ReturnCountOnly = true;
				UsrSet us = new UsrSet(q);
				count = us.Count;
				numberToAdd++;
			}
			return newNick;

		}
		public static string GetCompliantNickName(string inNick)
		{
			Regex rReplace = new Regex("[ _\\.]");
			Regex rValidChars = new Regex("[a-zA-Z0-9\\-]");
			Regex rMulti = new Regex("[\\-]{1,}");
			Regex rStart = new Regex("^[0-9\\-]{1,}");
			Regex rEnd = new Regex("[\\-]{1,}$");

			inNick = rReplace.Replace(inNick, "-");

			string tmpName = "";
			for (int i = 0; i < inNick.Length; i++)
			{
				if (rValidChars.IsMatch(inNick.Substring(i, 1)))
					tmpName += inNick.Substring(i, 1);
				else
					tmpName += UrlInfo.UrlNameReplacement(inNick.Substring(i, 1));
			}
			inNick = tmpName;

			inNick = rMulti.Replace(inNick, "-");
			inNick = rStart.Replace(inNick, "");
			inNick = rEnd.Replace(inNick, "");

			return inNick;
		}
		public static Regex NickNameRegex
		{
			get
			{
				return new Regex("^[A-Za-z][A-Za-z0-9 -]{0,18}[A-Za-z0-9]$");
			}
		}
		//public static bool GetCompliantUniqueNickName(string inNickName, out string outNickName)
		//{
		//    outNickName = inNickName;

		//    int index;
		//    // if it's an email address, remove domain
		//    if (index = outNickName.IndexOf("@") > 0)
		//    {
		//        outNickName = outNickName.Substring(0, outNickName.IndexOf(index));
		//    }

		//    outNickName = Usr.GetCompliantNickName(outNickName);
		//    if (!Usr.NickNameRegex.IsMatch(outNickName))
		//        return false;

		//    else
		//    {
		//        Query q = new Query();
		//        q.QueryCondition = new Q(Usr.Columns.NickName, outNickName);
		//        q.ReturnCountOnly = true;
		//        UsrSet us = new UsrSet(q);
		//        us.Count == 0;
		//    }
		//}
		#endregion

		#region CreateNewUsr
		//public static Usr CreateNewUsr(string email, string password)
		//{
		//    return CreateNewUsr(email, password, "", "");
		//}
		//public static Usr CreateNewUsr(string email, string password, string firstName, string lastName)
		//{
		//    return CreateNewUsr(email, password, firstName, lastName, "");
		//}
		//public static Usr CreateNewUsr(string email, string password, string firstName, string lastName, string emailIp)
		//{
		//    return CreateNewUsr(email, password, firstName, lastName, emailIp, true);
		//}
		public static Usr CreateNewUsrMaster(string email, string password, string firstName, string lastName, string emailIp, bool runUpdate)
		{
			Usr u = new Usr();
			Random r = new Random();

			#region Add data from form

			u.DateTimeSignUp = Time.Now;
			u.DateTimeLastAccess = Time.Now;
			u.DateTimeLastPageRequest = Time.Now;

			u.Email = email.Trim();
			u.EmailDateTime = Time.Now;
			u.EmailIp = emailIp;
			if (password.Trim().Length > 0)
				u.SetPassword(password.Trim(), false);
			u.LoginString = Cambro.Misc.Utility.GenRandomText(6, r);
			u.RandomNumber = r.NextDouble();
			u.IsEmailVerified = false;
			u.IsAdmin = false;
			u.LoginCount = 0;
			u.FirstName = firstName;
			u.LastName = lastName;
			u.NickName = "";
			u.IsSkeleton = true;

			u.SendSpottedEmails = true;
			u.UpdateLargeEvents = 5000;
			u.UpdateSendBuddies = false;
			u.UpdateSendGenericMusic = true;
			u.SendSpottedTexts = true;
			u.SendFlyers = true;
			u.SendInvites = true;
			u.SendPartnerEmails = false;
			u.SendPartnerTexts = false;

			#endregion

			if (runUpdate)
				u.Update();

			return u;
		}
		#endregion

		#region Add skeleton user from email address
		public static Usr GetOrCreateSkeletonUser(Usr invitingUsr, string email, string nickName, Group invitingGroup, string inviteMessage)
		{
			return GetOrCreateSkeletonUser(invitingUsr, email, nickName, invitingGroup, inviteMessage, false, false);
		}
		public static Usr GetOrCreateSkeletonUser(Usr invitingUsr, string email, string nickName, Group invitingGroup, string inviteMessage, bool invitedViaContactImporter, bool sendWelcomeEmailEvenIfSkeletonUsrAlreadyExists)
		{
			UsrSet us = new UsrSet(new Query(new Q(Usr.Columns.Email, email)));
			if (us.Count > 0)
			{
				if (us[0].NickName.Length == 0 && nickName.Length > 0)
				{
					//Duplicate nick check
					Query qNick = new Query();
					qNick.QueryCondition = new Q(Usr.Columns.NickName, Usr.GetCompliantNickName(nickName));
					qNick.NoLock = true;
					qNick.ReturnCountOnly = true;
					UsrSet usNick = new UsrSet(qNick);
					if (usNick.Count == 0)
					{
						us[0].NickName = Usr.GetCompliantNickName(nickName);
						if (invitedViaContactImporter)
						{
							us[0].InvitedViaContactImporter = true;
						}
						us[0].Update();
					}
				}
				if (sendWelcomeEmailEvenIfSkeletonUsrAlreadyExists)
				{
					us[0].SendWelcomeEmail(invitingUsr, invitingGroup, inviteMessage);
				}
				return us[0];
			}
			else
				return CreateSkeletonUserAndSendWelcomeEmail(invitingUsr, email, nickName, invitingGroup, inviteMessage, invitedViaContactImporter);
		}
		private static Usr CreateSkeletonUserAndSendWelcomeEmail(Usr invitingUsr, string email, string nickName, Group invitingGroup, string inviteMessage, bool invitedViaContactImporter)
		{
			Usr u = new Usr();
			Random r = new Random();

			u.DateTimeSignUp = DateTime.Now;
			u.DateTimeLastAccess = DateTime.Now;
			u.DateTimeLastPageRequest = DateTime.Now;

			u.IsSkeleton = true;
			u.Email = Cambro.Web.Helpers.StripHtml(email);
			u.EmailDateTime = DateTime.Now;
			if (HttpContext.Current != null)
				u.EmailIp = Utilities.TruncateIp(HttpContext.Current.Request.ServerVariables["REMOTE_HOST"]);
			u.LoginString = Cambro.Misc.Utility.GenRandomText(6, r);
			u.RandomNumber = r.NextDouble();
			u.IsAdmin = false;
			u.LoginCount = 0;
			u.FirstName = "";
			u.LastName = "";

			u.NickName = Cambro.Web.Helpers.StripHtml(nickName);
			u.Mobile = "";
			u.AddressPostcode = "";

			u.SendSpottedEmails = true;
			u.UpdateLargeEvents = 5000;
			u.UpdateSendBuddies = false;
			u.UpdateSendGenericMusic = false;
			u.SendSpottedTexts = true;
			u.SendFlyers = true;
			u.SendInvites = true;
			u.SendPartnerEmails = false;
			u.SendPartnerTexts = false;
			u.IsEmailVerified = true;

			if (invitingUsr != null)
				u.AddedByUsrK = invitingUsr.K;

			if (invitingGroup != null)
				u.AddedByGroupK = invitingGroup.K;

			u.InvitedViaContactImporter = invitedViaContactImporter;

			u.Update();
			
			u.SendWelcomeEmail(invitingUsr, invitingGroup, inviteMessage);

			u = new Usr(u.K);
			return u;

		}

		public bool SendWelcomeEmail(Usr invitingUsr, Group invitingGroup, string inviteMessage)
		{
			if (this.EmailHold)
				return false;

			Mailer sm = new Mailer();
			sm.UsrRecipient = this;
			if (invitingGroup == null)
			{
				if (invitingUsr == null)
				{
					sm.Subject = "We've created you a Don't Stay In account!";
					sm.Body = @"
<p>
Don't Stay In is <i>the</i> place to plan your clubbing calendar. Our 
team of spotters are busy taking photos of clubbers at the hottest 
parties in your area and throughout the world. 
</p>
<p>
If you've had your photo taken, you can look through our galleries 
to find it. You can chat live with other clubbers and catch up with 
the latest gossip.
</p>
<p align=""center"" style=""margin:10px 0px 8px 0px;"">
<a href=""[LOGIN]"" style=""font-size:18px;font-weight:bold;"">Click here 
to go to DontStayIn</a>
</p>
";
				}
				else
				{
					sm.Subject = invitingUsr.NickName + " has invited you to DontStayIn!";
					sm.Body = "<p><b>" + invitingUsr.NickName + @" has invited you to DontStayIn!</b></p>
<p>
DontStayIn is <i>the</i> place to plan your clubbing calendar. Our 
team of spotters are busy taking photos of clubbers at the hottest 
parties in your area and throughout the world. 
</p>
<p>
If you've had your photo taken, you can look through our galleries 
to find it. You can chat live with other clubbers and catch up with 
the latest gossip.
</p>
<p align=""center"" style=""margin:10px 0px 8px 0px;"">
<a href=""[LOGIN]"" style=""font-size:18px;font-weight:bold;"">Click here 
to go to DontStayIn</a>
</p>
";
				}
			}
			else
			{
				GroupUsr invitingGroupUsr = invitingGroup.GetGroupUsr(invitingUsr);
				sm.RedirectUrl = invitingGroup.Url();
				sm.Subject = invitingUsr.NickName + @" has invited you to " + (invitingGroupUsr != null && invitingGroupUsr.Moderator ? invitingUsr.HisString(false) : "a") + @" DontStayIn group: " + invitingGroup.FriendlyName;
				string pic = @"<table cellspacing=""0"" cellpadding=""0"" border=""0"" style=""margin:10px 5px 5px 1px;""><tr><td valign=""top"">";
				string picEnd = "</td></tr></table>";
				if (invitingUsr.HasPic)
				{
					pic = @"<table cellspacing=""0"" cellpadding=""0"" border=""0"" style=""margin:10px 5px 5px 1px;""><tr><td valign=""top"" style=""padding:0px 10px 0px 0px;"">";
					pic += "<a href=\"[LOGIN]\"><img src=\"" + invitingUsr.PicPath + "\" class=\"BorderBlack All\" width=\"100\" height=\"100\" vspace=\"3\" border=\"0\"></a></td><td valign=\"top\">";
					picEnd = "</td></tr></table>";
				}
				string members = "";
				if (invitingGroup.TotalMembers > 5)
				{
					Query q = new Query();
					q.TableElement = Usr.GroupJoin;
					q.QueryCondition = new And(new Q(Group.Columns.K, invitingGroup.K), new Q(Usr.Columns.Pic, QueryOperator.NotEqualTo, Guid.Empty));
					q.TopRecords = 5;
					q.OrderBy = new OrderBy(OrderBy.OrderDirection.Random);
					q.Columns = Usr.LinkColumns;
					UsrSet us = new UsrSet(q);
					if (us.Count == 5)
					{
						members = @"<p><b>" + invitingGroup.FriendlyName + @"</b> has " + invitingGroup.TotalMembers.ToString("#,##0") + @" members. Here's a few of them:</p>";
						members += @"<table cellspacing=""4"" cellpadding=""4"" border=""0"" width=""100%""><tr>";
						foreach (Usr uPic in us)
						{
							members += "<td width=\"20%\" valign=\"top\"><center><a href=\"[LOGIN(" + uPic.Url() + ")]\"><img src=\"" + uPic.PicPath + "\" width=\"75\" height=\"75\" style=\"margin:0px 0px 5px 0px;\" class=\"BorderBlack All\"><br>" + Cambro.Misc.Utility.Snip(uPic.NickName, 12) + "</a></center></td>";
						}
						members += @"</tr></table>";
					}
				}
				sm.Body = @"
" + pic + @"
<i style=""font-size:18px;""><b>""</b>" + Cambro.Web.Helpers.Strip(inviteMessage, true, true, false, true).Replace("\n", "<br>") + @"<b>""</b></i>
" + picEnd + @"
<p>" + invitingUsr.LinkEmail() + @" (" + invitingUsr.FirstName + @") has invited you to " +
	 (invitingGroupUsr != null && invitingGroupUsr.Moderator ? invitingUsr.HisString(false) : "a") + @" DontStayIn group!
You can use this to keep in contact with your friends. Here's a quick description 
of <b>" + invitingGroup.FriendlyName + @"</b>:</p>
<p>
<i>" + invitingGroup.Description + @"</i>
</p>
" + members + @"
";
			}

			sm.Send();
			return true;
		}
		#endregion

		#region IsEmailVerifiedQ
		public static Q IsEmailVerifiedQ
		{
			get
			{
				return new Q(Usr.Columns.IsEmailVerified, true);
			}
		}
		#endregion

		#region Skeleton

		public bool IsSkeletonTimedOut
		{
			get
			{
				if (IsSkeleton)
				{
					if (DateTimeSignUp.AddDays(7.0) < DateTime.Now)
						return true;
					else
						return false;
				}
				else
					return false;
			}
		}

		public static Q IsNotSkeletonQ
		{
			get
			{
				return new Q(Usr.Columns.IsSkeleton, false);
			}
		}
		public static Q IsSkeletonWithNickNameQ
		{
			get
			{
				return new And(
					new Q(Usr.Columns.IsSkeleton, true),
					new Q(Usr.Columns.NickName, QueryOperator.NotEqualTo, "")
				);
			}
		}
		public static Q IsNewSkeletonWithNickNameQ
		{
			get
			{
				return new And(
					new Q(Usr.Columns.IsSkeleton, true),
					new Q(Usr.Columns.NickName, QueryOperator.NotEqualTo, ""),
					new Q(Usr.Columns.DateTimeSignUp, QueryOperator.GreaterThan, DateTime.Now.AddDays(-7.0))
				);
			}
		}
		public static Q IsDisplayedInUsrLists
		{
			get
			{
				return new Or(
					Usr.IsNotSkeletonQ,
					Usr.IsNewSkeletonWithNickNameQ
				);
			}
		}
		#endregion

		#region IsSpotterQueryCondition
		public static Q IsSpotterQueryCondition
		{
			get
			{
				return new Q(Usr.Columns.IsSpotter, true);
			}
		}
		#endregion

		#region Pic
		public bool HasPicNotFacebook
		{
			get
			{
				return !Pic.Equals(Guid.Empty);
			}
		}
		public bool HasPic
		{
			get
			{
				return !Pic.Equals(Guid.Empty) || (this.FacebookUID.HasValue && this.FacebookUID.Value > 0 && (HttpContext.Current == null || !HttpContext.Current.Request.IsSecureConnection));
			}
		}
		public string PicPath 
		{
			get 
			{
				if (!Pic.Equals(Guid.Empty))
					return Storage.Path(Pic);
				else if (this.FacebookUID.HasValue && this.FacebookUID.Value > 0 && (HttpContext.Current == null || !HttpContext.Current.Request.IsSecureConnection))
					return "http://graph.facebook.com/" + this.FacebookUID.ToString() + "/picture";
				else
					return "";
			} 
		}
		public string AnyPicPath
		{
			get
			{
				if (!Pic.Equals(Guid.Empty))
					return Storage.Path(Pic);
				else if (this.FacebookUID.HasValue && this.FacebookUID.Value > 0 && (HttpContext.Current == null || !HttpContext.Current.Request.IsSecureConnection))
					return "http://graph.facebook.com/" + this.FacebookUID.ToString() + "/picture";
				else
					return "/gfx/dsi-sign-100.png";
			}
		}
		#endregion

		#region ChatPic
		public bool HasChatPic
		{
			get
			{
				return ChatPic.HasValue && ChatPic != Guid.Empty;
			}
		}
		public string ChatPicPath { get { return Storage.Path(ChatPic ?? Guid.Empty); } }
		public string AnyChatPicPath
		{
			get
			{
				if (HasChatPic)
					return Storage.Path(ChatPic ?? Guid.Empty);
				else
					return "/gfx/dsi-sign-300.png";
			}
		}
		#endregion

		#region LoggedInQ
		public static Q LoggedInQ
		{
			get
			{
				return new And(
					new Q("DATEADD(minute, -" + Common.Settings.OnlineTimeoutMinutes + ", GETDATE()) < {0}", Usr.Columns.DateTimeLastPageRequest, null as object),
					new Q(Usr.Columns.IsLoggedOn, true),
					Usr.IsNotSkeletonQ
				);
			}
		}
		public static Q LoggedIn30MinQ
		{
			get
			{
				return new And(
					new Q(Usr.Columns.DateTimeLastPageRequest, QueryOperator.GreaterThan, DateTime.Now.AddMinutes(-Common.Settings.OnlineTimeoutMinutes)),
					new Q(Usr.Columns.IsLoggedOn, true),
					Usr.IsNotSkeletonQ
				);
			}
		}
		public static Q LoggedIn30MinIncludeNonEmailVerifiedQ
		{
			get
			{
				return new And(
					new Q(Usr.Columns.DateTimeLastPageRequest, QueryOperator.GreaterThan, DateTime.Now.AddMinutes(-Common.Settings.OnlineTimeoutMinutes)),
					new Q(Usr.Columns.IsLoggedOn, true),
					Usr.IsNotSkeletonQ
				);
			}
		}
		public static Q LoggedInChatQ
		{
			get
			{
				return new And(
					new Q(Usr.Columns.DateTimeLastPageRequest, QueryOperator.GreaterThan, DateTime.Now.AddMinutes(-Common.Settings.OnlineTimeoutMinutes)),
					new Q(Usr.Columns.IsLoggedOn, true)
				);
			}
		}
		#endregion

		#region DonatedQ
		//public static Q DonatedQ
		//{
		//    get
		//    {
		//        return new Or(
		//            new And(
		//                new Q(Usr.Columns.Donate1Icon, true),
		//                new Q(Usr.Columns.Donate1Expire, QueryOperator.GreaterThan, DateTime.Now)
		//            ),
		//            new And(
		//                new Q(Usr.Columns.Donate2Icon, true),
		//                new Q(Usr.Columns.Donate2Expire, QueryOperator.GreaterThan, DateTime.Now)
		//            ));
		//    }
		//}
		#endregion

		#region LoggedInNow
		public bool LoggedInNow
		{
			get
			{
				return (IsLoggedOn && DateTimeLastPageRequest > DateTime.Now.AddMinutes(-Common.Settings.OnlineTimeoutMinutes));
			}
		}
		#endregion

		#region ChattingNow
		public bool ChattingNow
		{
			get
			{
				return (IsLoggedOn && DateTimeLastChatMessage.HasValue && DateTimeLastChatMessage > DateTime.Now.AddMinutes(-Common.Settings.OnlineTimeoutMinutes));
			}
		}
		#endregion

		#region IsSuper
		public bool IsSuper
		{
			get
			{
				if (IsAdmin)
					return true;
				else if (this.AdminLevel.Equals(AdminLevels.Super))
					return true;
				else
					return false;
			}
		}
		#endregion

		#region IsSenior
		public bool IsSenior
		{
			get
			{
				if (IsAdmin)
					return true;
				else if (
					this.AdminLevel.Equals(AdminLevels.Super) ||
					this.AdminLevel.Equals(AdminLevels.Senior)
					)
					return true;
				else
					return false;
			}
		}
		#endregion

		#region IsJunior
		public bool IsJunior
		{
			get
			{
				if (IsAdmin)
					return true;
				else if (
					this.AdminLevel.Equals(AdminLevels.Super) ||
					this.AdminLevel.Equals(AdminLevels.Senior) ||
					this.AdminLevel.Equals(AdminLevels.Junior)
					)
					return true;
				else
					return false;
			}
		}
		#endregion

		#region NickNameSafe
		public string NickNameSafe
		{
			get
			{
				return NickName;
			}
		}
		#endregion

		#region NickNameDisplay
		public string NickNameDisplay
		{
			get
			{
				if (NickName.Length > 0)
					return HttpUtility.HtmlEncode(NickName);
				else
					return "???";
			}
		}
		#endregion

		#region UpdateTotalPhotos(Transaction transaction)
		public void UpdateTotalPhotos(Transaction transaction)
		{
			Query q = new Query();
			q.QueryCondition = new And(new Q(Photo.Columns.UsrK, this.K), Photo.EnabledQueryCondition);
			q.ReturnCountOnly = true;
			PhotoSet ps = new PhotoSet(q);
			this.TotalPhotoUploads = ps.Count;
			this.Update(transaction);
		}
		#endregion

		#region UpdateSpottings(Transaction transaction)
		public void UpdateSpottings(Transaction transaction)
		{
			if (true)
			{
				Query q = new Query();
				q.TableElement = new Join(UsrPhotoMe.Columns.PhotoK, Photo.Columns.K);
				q.TableElement = new Join(q.TableElement, new TableElement(TablesEnum.Usr), QueryJoinType.Inner, UsrPhotoMe.Columns.UsrK, Usr.Columns.K);
				q.QueryCondition = new And(
					new Q(Usr.Columns.IsEmailVerified, true),
					new Q(Usr.Columns.IsSkeleton, false),
					new Q(Photo.Columns.UsrK, this.K));
				q.ExtraSelectElements.Add("SpottingsTotal", "count(distinct UsrPhotoMe.UsrK)");
				q.Columns = new ColumnSet();
				UsrSet us = new UsrSet(q);
				if (this.SpottingsTotal != (int)us[0].ExtraSelectElements["SpottingsTotal"])
				{
					this.SpottingsTotal = (int)us[0].ExtraSelectElements["SpottingsTotal"];
					this.Update();
				}
			}
		}
		#endregion

		#region DsiUsr
		public static int DsiUsrK { get { return 8; } }
		private static Usr dsiUsr;
		public static Usr DsiUsr
		{
			get
			{
				if (dsiUsr == null)
					dsiUsr = new Usr(DsiUsrK);
				return dsiUsr;
			}
		}
		#endregion

		#region Links to Bobs
		#region BannedByUsr
		public Usr BannedByUsr
		{
			get
			{
				if (bannedByUsr == null && BannedByUsrK > 0)
					bannedByUsr = new Usr(BannedByUsrK);
				return bannedByUsr;
			}
			set
			{
				bannedByUsr = value;
			}
		}
		private Usr bannedByUsr;
		#endregion
		#region Country
		/// <summary>
		/// This is the Country linked to by CounrtyK
		/// </summary>
		public Country AddressCountry
		{
			get
			{
				if (addressCountry == null && AddressCountryK > 0)
				{
					try
					{
						addressCountry = new Country(AddressCountryK);
					}
					catch { }
				}
				return addressCountry;
			}
		}
		private Country addressCountry;
		#endregion
		//		#region GuestClient
		//		public GuestClient GuestClient
		//		{
		//			get
		//			{
		//				if (guestClient==null && GuestClientK>0)
		//					guestClient = new GuestClient(GuestClientK);
		//				return guestClient;
		//			}
		//			set
		//			{
		//				guestClient = value;
		//			}
		//		}
		//		private GuestClient guestClient;
		//		#endregion
		#region AddedByUsr
		public Usr AddedByUsr
		{
			get
			{
				if (addedByUsr == null)
					addedByUsr = new Usr(AddedByUsrK);
				return addedByUsr;
			}
			set
			{
				addedByUsr = value;
			}
		}
		private Usr addedByUsr;
		#endregion
		#region Home
		public Place Home
		{
			get
			{
				if (home == null && HomePlaceK > 0)
				{
					home = new Place(HomePlaceK);
				}
				return home;
			}
			set
			{
				home = value;
			}
		}
		private Place home;
		#endregion
		#region FavouriteMusicType
		public MusicType FavouriteMusicType
		{
			get
			{
				if (favouriteMusicType == null && FavouriteMusicTypeK > 0)
					favouriteMusicType = new MusicType(FavouriteMusicTypeK);
				return favouriteMusicType;
			}
			set
			{
				favouriteMusicType = value;
			}
		}
		private MusicType favouriteMusicType;
		#endregion

		#region AddedByGroup
		public Group AddedByGroup
		{
			get
			{
				if (addedByGroup == null && AddedByGroupK > 0)
					addedByGroup = new Group(AddedByGroupK, this, Usr.Columns.AddedByGroupK);
				return addedByGroup;
			}
			set
			{
				addedByGroup = value;
			}
		}
		private Group addedByGroup;
		#endregion
		#region SavedCards
		public TransferSet SavedCards
		{
			get
			{
				if (savedCards == null)
				{
					Query savedTransfers = new Query();
					savedTransfers.QueryCondition = new And(new Q(Transfer.Columns.CardSaved, true),
															new Q(Transfer.Columns.UsrK, this.K),
															new Q(Transfer.Columns.Method, Transfer.Methods.Card),
															new Q(Transfer.Columns.CardExpires, QueryOperator.GreaterThanOrEqualTo, Utilities.GetStartOfMonth(DateTime.Now)));
					savedTransfers.OrderBy = new OrderBy(Transfer.Columns.DateTimeCreated, OrderBy.OrderDirection.Descending);
					savedTransfers.Columns = new ColumnSet(Transfer.Columns.K, Transfer.Columns.CardNumberEnd, Transfer.Columns.CardDigits);

					savedCards = new TransferSet(savedTransfers);
				}
				return savedCards;
			}
		}
		private TransferSet savedCards;
		public ListItem[] SavedCardsToListItemArray
		{
			get
			{
				List<ListItem> savedCardsListItemArray = new List<ListItem>();
				if (SavedCards != null)
				{
					foreach (Transfer savedCard in SavedCards)
					{

						savedCardsListItemArray.Add(new ListItem(savedCard.CardNumber, savedCard.K.ToString()));
					}
				}
				return savedCardsListItemArray.ToArray();
			}
		}
		#endregion

		#region JoinedPromoterUsr
		public PromoterUsr JoinedPromoterUsr
		{
			get
			{
				if (joinedPromoterUsr == null)
				{
					joinedPromoterUsr = new PromoterUsr(this, Usr.Columns.K);
				}
				return joinedPromoterUsr;
			}
			set
			{
				joinedPromoterUsr = value;
			}
		}
		private PromoterUsr joinedPromoterUsr;
		#endregion
		#endregion

		#region Links to BobSets

		#region PlacesVisitKOnly
		public PlaceSet PlacesVisitKOnly
		{
			get
			{
				if (placesVisitKOnly == null)
				{
					Query q = new Query();
					q.Columns = new ColumnSet(Place.Columns.K);
					q.TableElement = Place.UsrVisitJoin;
					q.OrderBy = new OrderBy(Place.Columns.Population, OrderBy.OrderDirection.Descending);
					q.QueryCondition = new Q(Usr.Columns.K, this.K);
					placesVisitKOnly = new PlaceSet(q);
				}
				return placesVisitKOnly;
			}
			set { placesVisitKOnly = value; }
		}
		PlaceSet placesVisitKOnly;
		#endregion

		#region PlacesVisit
		public PlaceSet PlacesVisit(ColumnSet cs, int topRecords)
		{
			if (placesVisit == null)
			{
				Query q = new Query();
				q.TableElement = Place.UsrVisitJoin;
				if (cs != null)
					q.Columns = cs;
				if (topRecords > 0)
					q.TopRecords = topRecords;
				q.OrderBy = new OrderBy(Place.Columns.Population, OrderBy.OrderDirection.Descending);
				q.QueryCondition = new Q(Usr.Columns.K, this.K);
				placesVisit = new PlaceSet(q);
			}
			return placesVisit;
		}
		PlaceSet placesVisit;
		#endregion
		#region EventsAttended
		public EventSet EventsAttended(ColumnSet cs, int topRecords)
		{
			if (eventsAttended == null)
			{
				Query q = new Query();
				q.QueryCondition = new Q(Usr.Columns.K, this.K);
				q.TableElement = Event.UsrAttendedJoin;
				if (cs != null)
					q.Columns = cs;
				if (topRecords > 0)
					q.TopRecords = topRecords;
				eventsAttended = new EventSet(q);
			}
			return eventsAttended;
		}
		EventSet eventsAttended;
		#endregion
		#region PhotosMe
		public PhotoSet PhotosMe(ColumnSet cs, int topRecords)
		{
			if (photosMe == null)
			{
				Query q = new Query();
				q.QueryCondition = new Q(Usr.Columns.K, this.K);
				q.TableElement = Photo.UsrMeJoin;
				q.OrderBy = Photo.DateTimeOrder(OrderBy.OrderDirection.Descending);
				if (cs != null)
					q.Columns = cs;
				if (topRecords > 0)
					q.TopRecords = topRecords;
				photosMe = new PhotoSet(q);
			}
			return photosMe;
		}
		PhotoSet photosMe;
		#endregion
		#region MusicTypesFavourite
		public MusicTypeSet MusicTypesFavourite
		{
			get
			{
				if (musicTypesFavourite == null)
				{
					Query q = new Query();
					q.TableElement = MusicType.UsrFavouriteJoin;
					q.QueryCondition = new Q(Usr.Columns.K, K);
					q.OrderBy = MusicType.OrderBy;
					musicTypesFavourite = new MusicTypeSet(q);
				}
				return musicTypesFavourite;
			}
			set { musicTypesFavourite = value; }
		}
		MusicTypeSet musicTypesFavourite;
		#endregion

		public bool HasPrivateThreads
		{
			get
			{
				Query q = new Query();
				q.NoLock = true;
				q.QueryCondition = new Q(ThreadUsr.Columns.UsrK, this.K);
				q.TopRecords = 1;
				ThreadUsrSet tus = new ThreadUsrSet(q);
				if (tus.Count > 0)
					return true;

				Query q1 = new Query();
				q1.NoLock = true;
				q1.QueryCondition = new And(new Q(Thread.Columns.UsrK, this.K), new Q(Thread.Columns.Private, true));
				q1.TopRecords = 1;
				ThreadSet ts = new ThreadSet(q1);
				if (ts.Count > 0)
					return true;

				return false;

			}
		}
		#region Promoters
		public void PromotersClear()
		{
			promoters = null;
			promotersColumns = null;
		}
		public PromoterSet Promoters(ColumnSet cs)
		{
			if (promoters == null || !promotersColumns.Equals(cs))
			{
				Query q = new Query();
				q.NoLock = true;
				q.TableElement = Usr.PromoterJoin;
				q.QueryCondition = new Q(PromoterUsr.Columns.UsrK, this.K);
				q.OrderBy = new OrderBy(Promoter.Columns.Name);
				q.Columns = cs;
				promoters = new PromoterSet(q);
				promotersColumns = cs;
			}
			return promoters;
		}
		ColumnSet promotersColumns;
		PromoterSet promoters;
		#endregion
		#endregion

		#region BeforeUpdate
		partial void BeforeUpdate(Transaction t)
		{
			if (HttpContext.Current != null && HttpContext.Current.User.Identity.IsAuthenticated && Prefs.Current.HasChanged())
				this.PrefsText = Prefs.Current.Serialise();

			if ((this.IsDirty(Usr.Columns.Email) || 
				this.IsDirty(Usr.Columns.PasswordHash) || 
				this.IsDirty(Usr.Columns.FacebookUID)) && Usr.Current != null && Usr.Current.K == this.K)
				this.SetAuthCookieFixedNew();
		}
		#endregion

		#region IName Members

		public string Name
		{
			get
			{
				return NickName;
			}
			set
			{
				// TODO:  Add Usr.Name setter implementation
			}
		}

		public string FriendlyName
		{
			get
			{
				return NickName;
			}
		}

		#endregion

		#region IBobType Members

		public string TypeName
		{
			get
			{
				return "User";
			}
		}

		public Model.Entities.ObjectType ObjectType
		{
			get
			{
				return Model.Entities.ObjectType.Usr;
			}
		}

		#endregion

		#region PicMisc and PicPhoto
		#region PicMisc
		public Misc PicMisc
		{
			get
			{
				if (picMisc == null && PicMiscK > 0)
					picMisc = new Misc(PicMiscK);
				return picMisc;
			}
			set
			{
				picMisc = value;
			}
		}
		private Misc picMisc;
		#endregion
		#region PicPhoto
		public Photo PicPhoto
		{
			get
			{
				if (picPhoto == null && PicPhotoK > 0)
					picPhoto = new Photo(PicPhotoK);
				return picPhoto;
			}
			set
			{
				picPhoto = value;
			}
		}
		private Photo picPhoto;
		#endregion
		#endregion

		#region AbuseReportsOverturnedFraction
		public double AbuseReportsOverturnedFraction
		{
			get
			{
				return (double)AbuseReportsOverturned / (double)AbuseReportsUseful;
			}
		}
		#endregion

		#region GetSalesUsrs
		public static UsrSet GetCurrentAndPreviousSalesUsrsNameAndK()
		{
			Q salesUsrsQueryCondition = new Q(Usr.Columns.SalesTeam, QueryOperator.GreaterThan, 0);

			Query salesPersonsQuery = new Query(new Or(salesUsrsQueryCondition,
														new Q(Invoice.Columns.SalesUsrK, QueryOperator.GreaterThan, 0)));
			salesPersonsQuery.TableElement = new Join(Usr.Columns.K, Invoice.Columns.SalesUsrK, QueryJoinType.Left);
			salesPersonsQuery.Distinct = true;
			salesPersonsQuery.DistinctColumn = Usr.Columns.K;
			salesPersonsQuery.Columns = new ColumnSet(Usr.Columns.K, Usr.Columns.NickName, Usr.Columns.FirstName, Usr.Columns.LastName, Usr.Columns.SalesTeam);
			salesPersonsQuery.OrderBy = new OrderBy(Usr.Columns.FirstName, OrderBy.OrderDirection.Ascending);
			return new UsrSet(salesPersonsQuery);
		}

		public static UsrSet GetCurrentSalesUsrsNameAndK()
		{
			return GetCurrentSalesUsrsNameAndK(new int[] { });
		}

		public static UsrSet GetCurrentSalesUsrsNameAndK(int salesTeam)
		{
			return GetCurrentSalesUsrsNameAndK(new int[] { salesTeam });
		}

		public static UsrSet GetCurrentSalesUsrsNameAndK(int[] salesTeams)
		{
			Q salesUsrsQueryCondition = new Q(true);

			if (salesTeams.Length > 0)
			{
				salesUsrsQueryCondition = new Q(Usr.Columns.SalesTeam, -1);

				foreach (int salesTeam in salesTeams)
					salesUsrsQueryCondition = new Or(salesUsrsQueryCondition, new Q(Usr.Columns.SalesTeam, salesTeam));
			}
			Query salesPersonsQuery = new Query(new And(salesUsrsQueryCondition,
														new Q(Usr.Columns.SalesTeam, QueryOperator.GreaterThan, 0)));
			salesPersonsQuery.Columns = new ColumnSet(Usr.Columns.K, Usr.Columns.NickName, Usr.Columns.FirstName, Usr.Columns.LastName, Usr.Columns.SalesTeam);
			salesPersonsQuery.OrderBy = new OrderBy(Usr.Columns.FirstName, OrderBy.OrderDirection.Ascending);
			return new UsrSet(salesPersonsQuery);
		}

		public static UsrSet GetCurrentSalesUsrsNameAndKBySalesTeam()
		{
			Query salesPersonsQuery = new Query(new Q(Usr.Columns.SalesTeam, QueryOperator.GreaterThan, 0));
			salesPersonsQuery.Columns = new ColumnSet(Usr.Columns.K, Usr.Columns.NickName, Usr.Columns.FirstName, Usr.Columns.LastName, Usr.Columns.SalesTeam);
			salesPersonsQuery.OrderBy = new OrderBy(new OrderBy(Usr.Columns.SalesTeam, OrderBy.OrderDirection.Ascending), new OrderBy(Usr.Columns.FirstName, OrderBy.OrderDirection.Ascending));
			return new UsrSet(salesPersonsQuery);
		}

		public static UsrSet GetNewPromoterSalesUsrsNameAndK()
		{
			return GetCurrentSalesUsrsNameAndK(new int[] { Convert.ToInt32(SalesTeams.PromoterSalesTeam) });
		}

		public static UsrSet GetCorporateSalesUsrsNameAndK()
		{
			return GetCurrentSalesUsrsNameAndK(new int[] { Convert.ToInt32(SalesTeams.CorporateSalesTeam) });
		}
		#endregion

		#region Tickets
		public TicketSet Tickets()
		{
			return Tickets(Utilities.DateRange.All, 0, 0);
		}

		public TicketSet Tickets(Utilities.DateRange dateRange)
		{
			return Tickets(dateRange, 0, 0);
		}

		public TicketSet Tickets(Utilities.DateRange dateRange, int pageNumber, int recordsPerPage)
		{
			// Date range for all
			DateTime startDateRange = new DateTime(2006, 7, 1);
			DateTime endDateRange = DateTime.MaxValue;

			if (dateRange.Equals(Utilities.DateRange.Current))
				startDateRange = DateTime.Today.AddMonths(-1);
			else if (dateRange.Equals(Utilities.DateRange.Old))
				endDateRange = DateTime.Today.AddMonths(-1);

			Query ticketQuery = new Query(new And(new Q(Event.Columns.DateTime, QueryOperator.GreaterThanOrEqualTo, startDateRange),
												  new Q(Event.Columns.DateTime, QueryOperator.LessThan, endDateRange),
												  new Q(Ticket.Columns.BuyerUsrK, this.K),
												  new Q(Ticket.Columns.Enabled, true)));
			ticketQuery.Columns = new ColumnSet(Ticket.Columns.K, Ticket.Columns.Cancelled, Ticket.Columns.CardNumberEnd, Ticket.Columns.Quantity, Ticket.Columns.EventK, Ticket.Columns.Feedback, Ticket.Columns.FeedbackNote);
			//ticketQuery.Distinct = true;
			//ticketQuery.DistinctColumn = Ticket.Columns.K;
			if (pageNumber > 0 && recordsPerPage > 0)
			{
				ticketQuery.Paging.RecordsPerPage = recordsPerPage;
				ticketQuery.Paging.RequestedPage = pageNumber;
				ticketQuery.TopRecords = (pageNumber * recordsPerPage) + 1;
			}
			ticketQuery.ExtraSelectElements.Add("TicketRunName", "[TicketRun].[Name]");
			ticketQuery.ExtraSelectElements.Add("TicketRunDescription", "[TicketRun].[Description]");
			ticketQuery.ExtraSelectElements.Add("TicketRunPrice", "[TicketRun].[Price]");
			ticketQuery.ExtraSelectElements.Add("TicketRunK", "[TicketRun].[K]");

			ticketQuery.OrderBy = new OrderBy(new OrderBy(Event.Columns.DateTime, OrderBy.OrderDirection.Descending), new OrderBy(TicketRun.Columns.Price));

			ticketQuery.TableElement = new Join(new Join(Ticket.Columns.EventK, Event.Columns.K), TicketRun.Columns.K, Ticket.Columns.TicketRunK);


			return new TicketSet(ticketQuery);
		}

		public TicketSet Tickets(int eventK)
		{
			Query ticketQuery = new Query(new And(new Q(TicketRun.Columns.EventK, eventK),
												  new Q(Ticket.Columns.BuyerUsrK, this.K),
												  new Q(Ticket.Columns.Enabled, true)));
			ticketQuery.Columns = new ColumnSet(Ticket.Columns.K, Ticket.Columns.Cancelled, Ticket.Columns.CardNumberEnd, Ticket.Columns.Quantity, Ticket.Columns.Feedback, Ticket.Columns.FeedbackNote, Ticket.Columns.Code, Ticket.Columns.AddressStreet, Ticket.Columns.AddressArea, Ticket.Columns.AddressTown, Ticket.Columns.AddressCounty, Ticket.Columns.AddressCountryK, Ticket.Columns.AddressPostcode, Ticket.Columns.AddressName);
			
			
			ticketQuery.ExtraSelectElements.Add("TicketRunName", "[TicketRun].[Name]");
			ticketQuery.ExtraSelectElements.Add("TicketRunDescription", "[TicketRun].[Description]");
			ticketQuery.ExtraSelectElements.Add("TicketRunPrice", "[TicketRun].[Price]");
			ticketQuery.ExtraSelectElements.Add("ContactEmail", "[TicketPromoterEvent].[ContactEmail]");
			ticketQuery.ExtraSelectElements.Add("TicketRunK", "[TicketRun].[K]");
			ticketQuery.ExtraSelectElements.Add("DeliveryDate", "[TicketRun].[DeliveryDate]");
			ticketQuery.ExtraSelectElements.Add("DeliveryMethod", "[TicketRun].[DeliveryMethod]");
			

			
			
			ticketQuery.OrderBy = new OrderBy(new OrderBy(TicketRun.Columns.ListOrder), new OrderBy(TicketRun.Columns.Price));
			ticketQuery.TableElement = new Join(new Join(TicketRun.Columns.K, Ticket.Columns.TicketRunK), new TableElement(TablesEnum.TicketPromoterEvent), QueryJoinType.Inner,
												new And(new Q(TicketRun.Columns.PromoterK, TicketPromoterEvent.Columns.PromoterK, true), new Q(TicketRun.Columns.EventK, TicketPromoterEvent.Columns.EventK, true)));

			return new TicketSet(ticketQuery);
		}

		//public EventSet EventsWithTickets()
		//{
		//    return EventsWithTickets(Utilities.DateRange.All, 0, 0);
		//}

		//public EventSet EventsWithTickets(Utilities.DateRange dateRange)
		//{
		//    return EventsWithTickets(dateRange, 0, 0);
		//}
		
		public EventSet EventsWithTickets(Utilities.DateRange dateRange, int pageNumber, int recordsPerPage)
		{
			return new EventSet(EventsWithTicketsQuery(this, dateRange, pageNumber, recordsPerPage));
		}

		public EventSet EventsWithTickets(IStyledEventHolder styledObject, Utilities.DateRange dateRange, int pageNumber, int recordsPerPage)
		{
			Query query = EventsWithTicketsQuery(this, dateRange, pageNumber, recordsPerPage);
			if (styledObject is Brand)
			{
				query.TableElement = new Join(query.TableElement, new TableElement(TablesEnum.EventBrand), QueryJoinType.Inner, Event.Columns.K, EventBrand.Columns.EventK);
				query.QueryCondition = new And(query.QueryCondition,
											   new Q(EventBrand.Columns.BrandK, styledObject.K));
			}
			else if (styledObject is Venue)
			{
				query.QueryCondition = new And(query.QueryCondition,
											   new Q(Event.Columns.VenueK, styledObject.K));
			}
			return new EventSet(query);
		}

		private static Query EventsWithTicketsQuery(Usr usr, Utilities.DateRange dateRange, int pageNumber, int recordsPerPage)
		{
			// Date range for all
			DateTime startDateRange = new DateTime(2006, 7, 1);
			DateTime endDateRange = DateTime.MaxValue;

			if (dateRange.Equals(Utilities.DateRange.Current))
				startDateRange = DateTime.Today.AddMonths(-1);
			else if (dateRange.Equals(Utilities.DateRange.Old))
				endDateRange = DateTime.Today.AddMonths(-1);

			Query eventWithTicketsQuery = new Query(new And(new Q(Event.Columns.DateTime, QueryOperator.GreaterThanOrEqualTo, startDateRange),
															new Q(Event.Columns.DateTime, QueryOperator.LessThan, endDateRange),
															new Q(Ticket.Columns.BuyerUsrK, usr.K),
															new Q(Ticket.Columns.Quantity, QueryOperator.GreaterThan, 0),
															new Q(Ticket.Columns.Enabled, true)));
			eventWithTicketsQuery.Columns = new ColumnSet(Event.Columns.K,
															Event.Columns.SpotterRequest,
															Event.Columns.IsTicketsAvailable,
															Event.Columns.UrlFragment,
															Event.Columns.VenueK,
															Event.Columns.DateTime,
															Event.Columns.Pic,
															Event.Columns.StartTime,
															Event.Columns.LivePhotos,
															Event.Columns.Name,
															Event.Columns.MusicTypesString,
															Event.Columns.VenueK
															);
			eventWithTicketsQuery.Distinct = true;
			eventWithTicketsQuery.DistinctColumn = Event.Columns.K;
			
			eventWithTicketsQuery.TableElement = Event.EventTicketJoin;

			if (pageNumber > 0 && recordsPerPage > 0)
			{
				eventWithTicketsQuery.Paging.RecordsPerPage = recordsPerPage;
				eventWithTicketsQuery.Paging.RequestedPage = pageNumber;
				eventWithTicketsQuery.TopRecords = (pageNumber * recordsPerPage) + 1;
			}

			eventWithTicketsQuery.OrderBy = new OrderBy(new OrderBy(Event.Columns.DateTime, OrderBy.OrderDirection.Descending), new OrderBy(Event.Columns.Donated), new OrderBy(Event.Columns.Name));

			return eventWithTicketsQuery;
		}

		#endregion

		#region DeleteProfilePic
		public void DeleteProfilePic()
		{
			Guid oldPic = this.Pic;

			this.PicPhotoK = 0;
			this.PicState = "";
			this.Pic = Guid.Empty;
			this.PicOriginal = Guid.Empty;
			this.Update();

			if (oldPic != Guid.Empty)
				Storage.RemoveFromStore(Storage.Stores.Pix, oldPic, "jpg");
		}
		#endregion

		#region Unsubscribe
		public void Unsubscribe()
		{
			this.DeleteProfilePic();

			Mailer sm = new Mailer();
			sm.UsrRecipient = this;
			sm.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
			sm.Body = @"<p>You have unsubscribed from DontStayIn</p>
						<p>The following has been done to your account:</p>
						<ul><li>DontStayIn will stop sending you emails.</li>
							<li>Your profile pic has been deleted.</li>
							<li>On your user profile page it shows: ""This user has unsubscribed from DontStayIn.""</li></ul>
						<p>To subscribe again to DontStayIn click link below.</p>";
			sm.Subject = "You have unsubscribed from DontStayIn";
			sm.RedirectUrl = "/pages/unsubscribe";
			sm.Send();

			this.EmailHold = true;
			this.Update();
		}
		#endregion

		#region IBuyer Methods + Properties
		#region GetBalance
		/// <summary>
		/// This calculates the total balance of the user's account based on (Invoices + Credit) totals - successful Transfer amounts.
		/// Note: Pending refunds are considered the same as successful refunds
		/// </summary>
		/// <returns></returns>
		public decimal GetBalance()
		{
			Query qInvoiceBalance = new Query();
			qInvoiceBalance.Columns = new ColumnSet();
			qInvoiceBalance.ExtraSelectElements.Add("sum", "SUM([Invoice].[Total])");
			qInvoiceBalance.QueryCondition = new And(new Q(Invoice.Columns.UsrK, this.K),
													 new Q(Invoice.Columns.PromoterK, 0));
			InvoiceSet isInvoiceBalance = new InvoiceSet(qInvoiceBalance);

			Query qTransferBalance = new Query();
			qTransferBalance.Columns = new ColumnSet();
			qTransferBalance.ExtraSelectElements.Add("sum", "SUM(ISNULL([Transfer].[Amount], 0))");
			qTransferBalance.QueryCondition = new And(new Or(new Q(Transfer.Columns.Status, Transfer.StatusEnum.Success),
															 new And(new Q(Transfer.Columns.Status, Transfer.StatusEnum.Pending),
																	 new Q(Transfer.Columns.Type, Transfer.TransferTypes.Refund))),
													 new Q(Transfer.Columns.UsrK, this.K),
													 new Q(Transfer.Columns.PromoterK, 0));
			TransferSet tsTransferBalance = new TransferSet(qTransferBalance);

			decimal invoiceBalance = 0;
			decimal transferBalance = 0;

			if (isInvoiceBalance[0].ExtraSelectElements["sum"] != DBNull.Value)
				invoiceBalance = Convert.ToDecimal(isInvoiceBalance[0].ExtraSelectElements["sum"]);

			if (tsTransferBalance[0].ExtraSelectElements["sum"] != DBNull.Value)
				transferBalance = Convert.ToDecimal(tsTransferBalance[0].ExtraSelectElements["sum"]);

			return Math.Round(transferBalance - invoiceBalance, 2);
		}
		#endregion

		#region CreditLimit
		public decimal CreditLimit
		{
			get
			{
				return 0;
			}
		}
		#endregion

		#region InvoiceDueDaysEffective
		public int InvoiceDueDaysEffective
		{
			get
			{
				return 0;
			}
		}
		#endregion

		#region CampaignCredits
		public int CampaignCredits { get { return 0; } }
		#endregion
		#endregion

		#region IBuyable Methods + Properties
		/// <summary>
		/// Queries database to retrieve the latest BuyableLockDateTime. Only returns if there is a lock within the Vars.IBUYABLE_LOCK_SECONDS
		/// </summary>
		//public bool IsLocked
		//{
		//    get
		//    {
		//        Query iBuyableLockDateTimeQuery = new Query(new And(new Q(Usr.Columns.K, this.K),
		//                                                            new Q(Usr.Columns.BuyableLockDateTime, QueryOperator.GreaterThanOrEqualTo, DateTime.Now.AddSeconds(-1 * Vars.IBUYABLE_LOCK_SECONDS))));
		//        iBuyableLockDateTimeQuery.Columns = new ColumnSet(Usr.Columns.BuyableLockDateTime);

		//        UsrSet lockedBuyableSet = new UsrSet(iBuyableLockDateTimeQuery);
		//        if (lockedBuyableSet.Count > 0)
		//        {
		//            this.BuyableLockDateTime = lockedBuyableSet[0].BuyableLockDateTime;
		//            return true;
		//        }
		//        else
		//            return false;
		//    }
		//}

		///// <summary>
		///// Checks the price entered against the calculated price.  This checks if the figures have been adjusted during the payment processing.
		///// </summary>
		///// <param name="invoiceItemType">InvoiceItem.Type</param>
		///// <param name="price">InvoiceItem.Price</param>
		///// <returns></returns>
		//public bool VerifyPrice(InvoiceItem.Types invoiceItemType, double price, double total)
		//{
		//    if (invoiceItemType.Equals(InvoiceItem.Types.UsrDonate))
		//        return Math.Round(price, 2) >= 4;
		//    else if (invoiceItemType.Equals(InvoiceItem.Types.CharityDonation))
		//        return Math.Round(price, 2) >= 5;
		//    else
		//        throw new Exception("invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));
		//}

		///// <summary>
		///// Checks if the IBuyable Bob is ready to be processed. This is used as a pre-purchasing check.
		///// </summary>
		///// <param name="invoiceItemType">InvoiceItem.Type</param>
		///// <param name="price">InvoiceItem.Price</param>
		///// <returns></returns>
		//public bool IsReadyForProcessing(InvoiceItem.Types invoiceItemType, double price, double total)
		//{
		//    if (VerifyPrice(invoiceItemType, price, total))
		//    {
		//        if (invoiceItemType.Equals(InvoiceItem.Types.UsrDonate))
		//            return true;
		//        else if (invoiceItemType.Equals(InvoiceItem.Types.CharityDonation))
		//            return true;
		//        else
		//            throw new Exception("invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));
		//    }
		//    else
		//        throw new DsiUserFriendlyException("price wrong!");

		//    //return false;
		//}

		///// <summary>
		///// Processes the IBuyable Bob. For usrs, it verifies that the usr donation IsReadyForProcessing. If yes, then it sets the appropriate usr donation icon and expiry date, and updates the usr.
		///// </summary>
		///// <param name="invoiceItemType">InvoiceItem.Type</param>
		///// <param name="price">InvoiceItem.Price</param>
		///// <returns></returns>
		//public bool Process(InvoiceItem.Types invoiceItemType, double price, double total)
		//{
		//    if (VerifyPrice(invoiceItemType, price, total))
		//    {
		//        if (invoiceItemType.Equals(InvoiceItem.Types.UsrDonate))
		//        {
		//            throw new NotImplementedException();
		//            //this.ProcessDonation();
		//        }
		//        else if (invoiceItemType.Equals(InvoiceItem.Types.CharityDonation))
		//        {
		//            throw new NotImplementedException();
		//            //this.ProcessDonation();
		//        }
		//        else
		//            throw new Exception("invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));
		//    }
		//    else
		//    {
		//        throw new Exception("price wrong!");
		//    }

		//    return IsProcessed(invoiceItemType);
		//}

		///// <summary>
		///// Unprocesses the IBuyable Bob. For usrs, it sets the appropriate usr donation icon off, and updates the usr.
		///// </summary>
		///// <param name="invoiceItemType">InvoiceItem.Type</param>
		///// <returns></returns>
		//public bool Unprocess(InvoiceItem.Types invoiceItemType)
		//{
		//    if (invoiceItemType.Equals(InvoiceItem.Types.UsrDonate))
		//    {
		//        throw new NotImplementedException();
		//        //this.UnprocessDonation();
		//    }
		//    else if (invoiceItemType.Equals(InvoiceItem.Types.CharityDonation))
		//    {
		//        throw new NotImplementedException();
		//        //this.UnprocessDonation();
		//    }
		//    else
		//        throw new Exception("invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));

		//    return true;
		//}

		///// <summary>
		///// Verifies if the IBuyable Bob has already been processed successfully.
		///// </summary>
		///// <param name="invoiceItemType">InvoiceItem.Type</param>
		///// <returns></returns>
		//public bool IsProcessed(InvoiceItem.Types invoiceItemType)
		//{
		//    if (invoiceItemType.Equals(InvoiceItem.Types.UsrDonate))
		//        throw new NotImplementedException();
		//        //return this.IsDonationProcessed;
		//    else if (invoiceItemType.Equals(InvoiceItem.Types.CharityDonation))
		//        throw new NotImplementedException();
		//        //return this.IsDonationProcessed;
		//    else
		//        throw new Exception("invalid invoice item type: " + Utilities.CamelCaseToString(invoiceItemType.ToString()));
		//}

		///// <summary>
		///// Sets the IBuyable Bob field BuyableLockDateTime to DateTime.Now and updates the Bob
		///// </summary>
		//public void Lock()
		//{
		//    this.BuyableLockDateTime = DateTime.Now;
		//    this.Update();
		//}

		///// <summary>
		///// Sets the IBuyable Bob field BuyableLockDateTime to DateTime.MinValue and updates the Bob
		///// </summary>
		//public void Unlock()
		//{
		//    this.BuyableLockDateTime = DateTime.MinValue;
		//    this.Update();
		//}

		#endregion

		#region Reminder to leave ticket feedback
		private const char DELIMITER1 = (char)1;
		private const char DELIMITER2 = (char)2;
		private const string PREFS_DATE_FORMAT = "dd-MM-yy";


		public void SetPrefsNextTicketFeedbackDate()
		{
			// Run query if tickets from last 2 weeks need feedback.
			Prefs.Current[Prefs.NEEDS_TICKET_FEEDBACK_LINKS_KEY] = "";

			try
			{
				EventSet eventsNeedingFeedback = Event.GetEventsForTicketsNeedingFeedback(Usr.Current);

				if (eventsNeedingFeedback.Count > 0)
				{
					Prefs.Current[Prefs.NEEDS_TICKET_FEEDBACK_NEXT_DATE_KEY] = eventsNeedingFeedback[0].DateTime.ToString(PREFS_DATE_FORMAT);

					string output = eventsNeedingFeedback[0].DateTime.ToString(PREFS_DATE_FORMAT) + DELIMITER2.ToString() + Utilities.Link(eventsNeedingFeedback[0].UrlTicketFeedback(), eventsNeedingFeedback[0].FriendlyNameNoDate + ", " + eventsNeedingFeedback[0].DateTime.ToString("ddd dd MMM"));

					for (int i = 1; i < eventsNeedingFeedback.Count; i++)
					{
						output += DELIMITER1.ToString() + eventsNeedingFeedback[i].DateTime.ToString(PREFS_DATE_FORMAT) + DELIMITER2.ToString() + Utilities.Link(eventsNeedingFeedback[i].UrlTicketFeedback(), eventsNeedingFeedback[i].FriendlyNameNoDate + ", " + eventsNeedingFeedback[i].DateTime.ToString("ddd dd MMM"));
					}

					Prefs.Current[Prefs.NEEDS_TICKET_FEEDBACK_LINKS_KEY] = output;
				}
				else
				{
					throw new DsiUserFriendlyException("No events needing ticket feedback.");
				}
			}
			catch
			{
				// Set Next Date to Max DateTime so it is not null, but never attained
				Prefs.Current[Prefs.NEEDS_TICKET_FEEDBACK_NEXT_DATE_KEY] = DateTime.MaxValue.ToString(PREFS_DATE_FORMAT);
				if (!Prefs.Current[Prefs.NEEDS_TICKET_FEEDBACK_LINKS_KEY].IsNull)
					Prefs.Current.Remove(Prefs.NEEDS_TICKET_FEEDBACK_LINKS_KEY);
			}
		}

		public string GetPrefsTicketFeedbackLinks()
		{
			string output = "";
			try
			{
				if (!Prefs.Current[Prefs.NEEDS_TICKET_FEEDBACK_LINKS_KEY].IsNull && Prefs.Current[Prefs.NEEDS_TICKET_FEEDBACK_LINKS_KEY].Value.Length > 0)
				{
					string[] eventLinks = Prefs.Current[Prefs.NEEDS_TICKET_FEEDBACK_LINKS_KEY].Value.Split(DELIMITER1);

					foreach (string eventLink in eventLinks)
					{
						string[] dateAndLink = eventLink.Split(DELIMITER2);
						DateTime dt = Convert.ToDateTime(dateAndLink[0]);
						if (dt >= DateTime.Today.AddDays(Vars.DAYS_TO_SHOW_TICKET_FEEDBACK_ALERT) && dt < DateTime.Today)
							output += "<br>" + dateAndLink[1];
					}
				}
			}
			catch (Exception)
			{
				output = "";
				Prefs.Current[Prefs.NEEDS_TICKET_FEEDBACK_LINKS_KEY] = "";
			}
			return output;
		}
		#endregion

		#region Inbox / Watching
		public void UpdateThreadUsrs(ThreadUsr.StatusEnum changeStatus, ThreadUsr.StatusEnum threadStatusToChange)
		{
			UpdateThreadUsrs(changeStatus, new List<ThreadUsr.StatusEnum>() { threadStatusToChange }, null);
		}
		public void UpdateThreadUsrs(ThreadUsr.StatusEnum changeStatus, List<ThreadUsr.StatusEnum> threadStatusToChange)
		{
			UpdateThreadUsrs(changeStatus, threadStatusToChange, null);
		}
		public void UpdateThreadUsrs(ThreadUsr.StatusEnum changeStatus, ThreadUsr.StatusEnum threadStatusToChange, IBobType invitingBob)
		{
			UpdateThreadUsrs(changeStatus, new List<ThreadUsr.StatusEnum>() { threadStatusToChange }, invitingBob);
		}
		public void UpdateThreadUsrs(ThreadUsr.StatusEnum changeStatus, List<ThreadUsr.StatusEnum> threadStatusesToChange, IBobType invitingBob)
		{
			if (invitingBob != null)
				UpdateThreadUsrs(changeStatus, threadStatusesToChange, invitingBob.ObjectType, invitingBob.K);
			else
				UpdateThreadUsrs(changeStatus, threadStatusesToChange, null, null);
		}
		public void UpdateThreadUsrs(ThreadUsr.StatusEnum changeStatus, ThreadUsr.StatusEnum threadStatusToChange, Model.Entities.ObjectType? statusChangeObjectType)
		{
			UpdateThreadUsrs(changeStatus, threadStatusToChange, statusChangeObjectType, null);
		}
		public void UpdateThreadUsrs(ThreadUsr.StatusEnum changeStatus, ThreadUsr.StatusEnum threadStatusToChange, Model.Entities.ObjectType? statusChangeObjectType, int? statusChangeObjectK)
		{
			UpdateThreadUsrs(changeStatus, new List<ThreadUsr.StatusEnum>() { threadStatusToChange }, statusChangeObjectType, statusChangeObjectK);
		}
		public void UpdateThreadUsrs(ThreadUsr.StatusEnum changeStatus, List<ThreadUsr.StatusEnum> threadStatusesToChange, Model.Entities.ObjectType? statusChangeObjectType, int? statusChangeObjectK)
		{
			string memcachedKey = new Caching.CacheKey(Caching.CacheKeyPrefix.UpdateThreadUsrJobStatus, "UsrK", this.K.ToString(), "StatusChangeObjectType", statusChangeObjectType.ToString(), "StatusChangeObjectK", statusChangeObjectK.ToString()).ToString();
			UpdateThreadUsrJob utuj = new UpdateThreadUsrJob(this.K, changeStatus, threadStatusesToChange, statusChangeObjectType, statusChangeObjectK, memcachedKey);
			utuj.ExecuteAsynchronously();

			//Update uThreadUsr = new Update();
			//uThreadUsr.Table = TablesEnum.ThreadUsr;
			//uThreadUsr.Changes.Add(new Assign(ThreadUsr.Columns.Status, changeStatus));
			//uThreadUsr.Changes.Add(new Assign(ThreadUsr.Columns.StatusChangeDateTime, Time.Now));
			//uThreadUsr.Where = new Q(ThreadUsr.Columns.UsrK, this.K);

			//if (threadStatusesToChange != null && threadStatusesToChange.Count > 0)
			//{
			//    Or statusOr = new Or();
			//    foreach (ThreadUsr.StatusEnum statusEnum in threadStatusesToChange)
			//    {
			//        statusOr = new Or(statusOr,
			//                        new Q(ThreadUsr.Columns.Status, statusEnum));
			//    }
			//    uThreadUsr.Where = new And(uThreadUsr.Where,
			//                               statusOr);
			//}
			//else
			//    throw new Exception("Usr.UpdateThreadUsrs(): Invalid list of ThreadUsr.StatusEnum to change.");


			//if (statusChangeObjectType != null)
			//{
			//    if (statusChangeObjectType.Value == Model.Entities.ObjectType.Usr)
			//    {
			//        // do nothing here
			//    }
			//    else
			//    {
			//        uThreadUsr.Where = new And(uThreadUsr.Where,
			//                                   new Q(ThreadUsr.Columns.StatusChangeObjectType, statusChangeObjectType.Value));
			//    }

			//    if (statusChangeObjectK != null)
			//    {
			//        if (statusChangeObjectType.Value == Model.Entities.ObjectType.Usr)
			//        {
			//            uThreadUsr.Where = new And(uThreadUsr.Where,
			//                                       new Q(ThreadUsr.Columns.InvitingUsrK, statusChangeObjectK.Value));
			//        }
			//        else
			//        {
			//            uThreadUsr.Where = new And(uThreadUsr.Where,
			//                                       new Q(ThreadUsr.Columns.StatusChangeObjectK, statusChangeObjectK.Value));
			//        }
			//    }
			//}
			//uThreadUsr.CommandTimeout = 90;
			//uThreadUsr.Run();
		}
		public void SmartDelete(ThreadUsr.StatusEnum threadStatusToChange, IBobType invitingBob)
		{
			if (invitingBob != null)
				SmartDelete(threadStatusToChange, invitingBob.ObjectType, invitingBob.K);
			else
				SmartDelete(threadStatusToChange, null, null);
		}
		public void SmartDelete(ThreadUsr.StatusEnum threadStatusToChange, Model.Entities.ObjectType? statusChangeObjectType, int? statusChangeObjectK)
		{
			SmartDelete(new List<ThreadUsr.StatusEnum>() { threadStatusToChange }, statusChangeObjectType, statusChangeObjectK);
		}
		public void SmartDelete(List<ThreadUsr.StatusEnum> threadStatusesToChange, Model.Entities.ObjectType? statusChangeObjectType, int? statusChangeObjectK)
		{
			string memcachedKey = new Caching.CacheKey(Caching.CacheKeyPrefix.UpdateThreadUsrJobStatus, "UsrK", this.K.ToString(), "StatusChangeObjectType", statusChangeObjectType.ToString(), "StatusChangeObjectK", statusChangeObjectK.ToString()).ToString();
			SmartDeleteThreadUsrJob sdtuj = new SmartDeleteThreadUsrJob(this.K, threadStatusesToChange, statusChangeObjectType, statusChangeObjectK, memcachedKey);
			sdtuj.ExecuteAsynchronously();
		}
		public void UpdateInboxThreads(ThreadUsr.StatusEnum changeStatus)
		{
			UpdateInboxThreads(changeStatus, null);
		}
		public void UpdateInboxThreads(ThreadUsr.StatusEnum changeStatus, IBobType invitingBob)
		{
			UpdateThreadUsrs(changeStatus, ThreadUsr.InboxStatuses, invitingBob);
		}
		public void UpdateWatchingThreads(ThreadUsr.StatusEnum changeStatus)
		{
			UpdateWatchingThreads(changeStatus, null);
		}
		public void UpdateWatchingThreads(ThreadUsr.StatusEnum changeStatus, IBobType invitingBob)
		{
			UpdateThreadUsrs(changeStatus, ThreadUsr.WatchingStatuses, invitingBob);
		}
		#endregion

		#region IHasIcon Members
		public string IconSrc
		{
			get { return "/gfx/icon-me-up.png"; }
		}
		#endregion
		
		#region IReadableReference Members

		public string ReadableReference
		{
			get { return Name; }
		}

		#endregion

		#region ExDirectory options
		#region AllowLinkToProfile
		public bool AllowLinkToProfile()
		{
			return AllowLinkToProfile(null);
		}
		public bool AllowLinkToProfile(Buddy.BuddyFindingMethod? buddyFindingMethod)
		{
			return (buddyFindingMethod.HasValue && buddyFindingMethod.Value == Buddy.BuddyFindingMethod.SpotterCode) ||
				this.ExDirectory == false ||
				(BuddyRelationshipWithCurrentLoggedInUsr != null &&
					(BuddyRelationshipWithCurrentLoggedInUsr.FullBuddy == true ||
					BuddyRelationshipWithCurrentLoggedInUsr.BuddyFoundByMethod == Buddy.BuddyFindingMethod.Nickname ||
					BuddyRelationshipWithCurrentLoggedInUsr.BuddyFoundByMethod == Buddy.BuddyFindingMethod.SpotterCode));
		}
		#endregion
		#region DisplayName
		private string displayName;
		public string DisplayName()
		{
			if (displayName == null)
			{
				displayName = DisplayName(null);
			}
			return displayName;
		}
		public string DisplayName(Buddy.BuddyFindingMethod? foundByMethod)
		{
			if (!this.ExDirectory)
			{
				return this.NickNameSafe;
			}
			if (!foundByMethod.HasValue)
			{
				if (BuddyRelationshipWithCurrentLoggedInUsr == null)
				{
					throw new ArgumentNullException("foundByMethod", "must have a value if current Buddy relationship is null");
				}
				else
				{
					foundByMethod = BuddyRelationshipWithCurrentLoggedInUsr.BuddyFoundByMethod;
				}
			}

			switch (foundByMethod.Value)
			{
				case Bobs.Buddy.BuddyFindingMethod.Nickname: return this.NickNameSafe;
				case Bobs.Buddy.BuddyFindingMethod.EmailAddress: return this.Email;
				case Bobs.Buddy.BuddyFindingMethod.RealName: return this.FullName;
				case Bobs.Buddy.BuddyFindingMethod.SpotterCode: return this.NickNameSafe; // this is fine, spotter numbers are public anyway
				default: throw new NotImplementedException();
			}
		}
		#endregion
		#endregion

		#region BuddyRelationshipWithCurrentLoggedInUsr
		private bool buddyRelationshipWithCurrentLoggedInUsrChecked = false;
		private Buddy buddyRelationshipWithCurrentLoggedInUsr;
		/// <summary>
		/// this is the relationship from how the currently logged in usr has added this usr
		/// </summary>
		public Buddy BuddyRelationshipWithCurrentLoggedInUsr
		{
			get
			{
				if (!buddyRelationshipWithCurrentLoggedInUsrChecked)
				{
					if (Current != null)
					{
						try
						{
							buddyRelationshipWithCurrentLoggedInUsr = new Buddy(Current.K, this.K);
						}
						catch (BobNotFound) { }
					}
					buddyRelationshipWithCurrentLoggedInUsrChecked = true;
				}
				return buddyRelationshipWithCurrentLoggedInUsr;
			}
		}
		#endregion

		#region Donation Generics
		//private DateTime ActiveDonateExpire
		//{
		//    get
		//    {
		//        switch (Donate.CurrentIcon)
		//        {
		//            case Donate.Icon.Icon1: return this.Donate1Expire;
		//            case Donate.Icon.Icon2: return this.Donate2Expire;
		//            default: throw new NotImplementedException();
		//        }
		//    }
		//    set
		//    {
		//        switch (Donate.CurrentIcon)
		//        {
		//            case Donate.Icon.Icon1: this.Donate1Expire = value; break;
		//            case Donate.Icon.Icon2: this.Donate2Expire = value; break;
		//            default: throw new NotImplementedException();
		//        }
		//    }
		//}
		//private bool ActiveDonateIconEnabled
		//{
		//    get
		//    {
		//        switch (Donate.CurrentIcon)
		//        {
		//            case Donate.Icon.Icon1: return this.Donate1Icon;
		//            case Donate.Icon.Icon2: return this.Donate2Icon;
		//            default: throw new NotImplementedException();
		//        }
		//    }
		//    set
		//    {
		//        switch (Donate.CurrentIcon)
		//        {
		//            case Donate.Icon.Icon1: this.Donate1Icon = value; break;
		//            case Donate.Icon.Icon2: this.Donate2Icon = value; break;
		//            default: throw new NotImplementedException();
		//        }
		//    }
		//}
		#endregion

		#region Process Donation methods
		//private bool IsDonationProcessed
		//{
		//    get { return this.ActiveDonateIconEnabled && this.ActiveDonateExpire > DateTime.Now; }
		//}
		//private void ProcessDonation()
		//{
		//    if ((!this.ActiveDonateIconEnabled || this.ActiveDonateExpire < DateTime.Now))
		//    {
		//        this.ActiveDonateIconEnabled = true;
		//        this.ActiveDonateExpire = DateTime.Now.AddMonths(1);
		//        this.Update();
		//    }
		//}
		//private void UnprocessDonation()
		//{
		//    this.ActiveDonateIconEnabled = false;
		//    this.Update();
		//}
		#endregion

		#region SetSpottedInPhoto
		/// <summary>
		/// 
		/// </summary>
		/// <param name="photoK"></param>
		/// <param name="spottedByUsrK"></param>
		/// <param name="usrIsInPhoto"></param>
		/// <returns>true if any change occurred</returns>
		public void SetSpottedInPhoto(Photo photo, Usr spottedByUsr, bool usrIsInPhoto)
		{
			if (usrIsInPhoto)
			{
				if (!this.IsSpottedInPhoto(photo))
				{
					this.PhotoMe(photo, true, null);
				}

				if (photo.ThreadK > 0 && photo.Thread != null)
				{
					CommentAlert.Enable(this, photo.ThreadK.Value, Model.Entities.ObjectType.Thread);
				}
				else
				{
					CommentAlert.Enable(this, photo.K, Model.Entities.ObjectType.Photo);
				}

				if (this.K != spottedByUsr.K)
				{
					SendEmailToNotifyThePersonWhoWasSpotted(photo, spottedByUsr);
				}
			}
			else
			{
				this.PhotoMe(photo, false, null);
			}
		}
		private bool IsSpottedInPhoto(Photo photo)
		{
			UsrPhotoMeSet upms = new UsrPhotoMeSet(
				new Query(
					new And(
						new Q(UsrPhotoMe.Columns.UsrK, this.K),
						new Q(UsrPhotoMe.Columns.PhotoK, photo.K)
					)
				)
			);
			return upms.Count > 0;
		}
		private void SendEmailToNotifyThePersonWhoWasSpotted(Photo photo, Usr currentUsr)
		{
			Mailer sm = new Mailer();
			sm.RedirectUrl = photo.Url();
			sm.Subject = "You've been spotted!";
			string intro = "A friend of yours";
			if (currentUsr != null)
				intro = "Your buddy, <a href=\"[LOGIN(" + currentUsr.Url() + ")]\">" + currentUsr.NickNameSafe + "</a>";

			sm.Body = "<p>" + intro + " has spotted you in a photo, shown below:</p>" +
				"<p align=\"center\"><a href=\"[LOGIN(" + photo.Url() + ")]\"><img src=\"" + photo.WebPath + "\" height=\"" + photo.WebHeight + "\" width=\"" + photo.WebWidth + "\" class=\"BorderBlack All\" border=\"0\"></a></p>" +
				"<p>If you're not in this photo, please click the link below to log in, and click the <b>Remove me from this photo</b> " +
				"button under the photo.</p>";

			sm.TemplateType = Mailer.TemplateTypes.AnotherSiteUser;
			sm.UsrRecipient = this;
			sm.To = this.Email;
			sm.Send();
		}
		#endregion

		#region SystemUsr
		private static Usr systemUsr;
		public static Usr SystemUsr
		{
			get { return systemUsr ?? (systemUsr = new Usr(Common.Properties.SystemUsrK)); }
		}
		#endregion
	}
	#endregion
	#region UsrSet
	[Serializable]
	public partial class UsrSet
	{
		public void WriteUsrLinks(PlaceHolder ph)
		{
			//ph.Controls.Clear();
			for (int i = 0; i < this.Count; i++)
			{
				if (i > 0)
				{
					if (i == this.Count - 1)
						ph.Controls.Add(new LiteralControl(" and "));
					else
						ph.Controls.Add(new LiteralControl(", "));
				}
				ph.Controls.Add(new LiteralControl("<a href=\"" + this[i].Url() + "\" " + this[i].Rollover + ">" + this[i].NickName + "</a>"));
			}
		}
	}
	#endregion

	#region MatchMaker
	public class MatchMaker
	{

		#region SelectedMusicTypes
		public SortedList SelectedMusicTypes
		{
			get
			{
				if (selectedMusicTypes == null)
					selectedMusicTypes = new SortedList();
				return selectedMusicTypes;
			}
			set
			{
				selectedMusicTypes = value;
			}
		}
		private SortedList selectedMusicTypes;
		#endregion
		#region GenericMusicTypes
		public SortedList GenericMusicTypes
		{
			get
			{
				if (genericMusicTypes == null)
					genericMusicTypes = new SortedList();
				return genericMusicTypes;
			}
			set
			{
				genericMusicTypes = value;
			}
		}
		private SortedList genericMusicTypes;
		#endregion
		#region AllMusicTypes
		public SortedList AllMusicTypes
		{
			get
			{
				if (allMusicTypes == null)
					allMusicTypes = new SortedList();
				return allMusicTypes;
			}
			set
			{
				allMusicTypes = value;
			}
		}
		private SortedList allMusicTypes;
		#endregion
		#region SelectedPlaces
		public SortedList SelectedPlaces
		{
			get
			{
				if (selectedPlaces == null)
					selectedPlaces = new SortedList();
				return selectedPlaces;
			}
			set
			{
				selectedPlaces = value;
			}
		}
		private SortedList selectedPlaces;
		#endregion
		public Q MusicQ { get; set; }
		public Q PlaceQ { get; set; }
		#region ColumnSet
		ColumnSet ColumnSet
		{
			get
			{
				if (columnSet == null)
				{
					columnSet = new ColumnSet(
						Event.Columns.K,
						Event.Columns.StartTime,
						Event.Columns.Pic,
						Event.Columns.LivePhotos,
						Event.Columns.DateTime,
						Event.Columns.Name,
						Event.Columns.HasHilight,
						Event.Columns.SpotterRequest,
						Event.Columns.IsTicketsAvailable,
						Event.Columns.MusicTypesString,
						Place.Columns.RegionAbbreviation,
						Event.Columns.ShortDetailsHtml,
						Country.Columns.UrlName,
						Place.Columns.UrlName,
						Place.Columns.Name,
						Place.Columns.CountryK,
						Country.Columns.FriendlyName,
						Venue.Columns.UrlName,
						Venue.Columns.Name
					);
				}
				return columnSet;
			}
		}
		ColumnSet columnSet;
		#endregion
		#region OrderBy
		public OrderBy OrderBy
		{
			get
			{
				if (orderBy == null)
				{
					orderBy = new OrderBy(
						new OrderBy(Event.Columns.DateTime),
						new OrderBy(Event.Columns.StartTime),
						new OrderBy(Event.Columns.HasHilight, OrderBy.OrderDirection.Descending),
						new OrderBy(Event.Columns.SpotterRequest, OrderBy.OrderDirection.Descending),
						new OrderBy(Event.Columns.UsrAttendCount, OrderBy.OrderDirection.Descending),
						new OrderBy(Event.Columns.Capacity, OrderBy.OrderDirection.Descending),
						new OrderBy(Event.Columns.K)
					);
				}
				return orderBy;
			}
		}
		private OrderBy orderBy;
		#endregion
		#region TableElement
		public TableElement TableElement
		{
			get
			{
				if (tableElement == null)
					tableElement = new Join(Event.PlaceAndMusicTypeLeftJoin, Country.Columns.K, Place.Columns.CountryK);
				return tableElement;
			}
		}
		TableElement tableElement;
		#endregion
		#region DistinctTableElement
		public TableElement DistinctTableElement
		{
			get
			{
				if (distinctTableElement == null)
					distinctTableElement = Event.PlaceAndMusicTypeLeftJoin;
				return distinctTableElement;
			}
		}
		TableElement distinctTableElement;
		#endregion
		#region DataTableElement
		public TableElement DataTableElement
		{
			get
			{
				if (dataTableElement == null)
					dataTableElement = new Join(Event.PlaceJoin, Country.Columns.K, Place.Columns.CountryK);
				return dataTableElement;
			}
		}
		TableElement dataTableElement;
		#endregion
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public bool NyeSpecial { get; set; }

		public MatchMaker() { }

		public class Return
		{
			public Return() { }
			public int Events { get; set; }
			public bool HasFreeGuestlist { get; set; }
			public bool HasTickets { get; set; }
		}

		#region Init(Usr u)
		public void Init(Usr u)
		{
			AllMusicTypes = null;
			SelectedMusicTypes = null;
			GenericMusicTypes = null;
			SelectedPlaces = null;
			MusicQ = null;
			PlaceQ = null;
			#region Populate MusicQ and MusicTypes
			if (u.FavouriteMusicTypeK == 0 && u.MusicTypesFavouriteCount == 0)
			{
				MusicQ = new Q(true);
			}
			else
			{
				List<int> musicTypesK = new List<int>();

				#region Add MusicTypes
				if (u.MusicTypesFavouriteCount > 0)
				{
					foreach (MusicType mt in u.MusicTypesFavourite)
					{
						if (!musicTypesK.Contains(mt.K))
						{
							musicTypesK.Add(mt.K);
							AllMusicTypes.Add(mt.Order, mt);
							SelectedMusicTypes.Add(mt.Order, mt);
							AddMusicTypeChildren(mt, ref musicTypesK);
							#region Add the parent
							if (u.UpdateSendGenericMusic && mt.ParentK > 1 && !musicTypesK.Contains(mt.ParentK))
							{
								musicTypesK.Add(mt.ParentK);
								AllMusicTypes.Add(mt.Parent.Order, mt.Parent);
								GenericMusicTypes.Add(mt.Parent.Order, mt.Parent);
							}
							#endregion
						}
					}
				}
				else
				{
					if (u.FavouriteMusicTypeK != 0)
					{
						musicTypesK.Add(u.FavouriteMusicTypeK);
						AllMusicTypes.Add(u.FavouriteMusicType.Order, u.FavouriteMusicType);
						SelectedMusicTypes.Add(u.FavouriteMusicType.Order, u.FavouriteMusicType);
						AddMusicTypeChildren(u.FavouriteMusicType, ref musicTypesK);
					}
				}
				if (u.UpdateSendGenericMusic && !musicTypesK.Contains(1))
				{
					musicTypesK.Add(1);
					MusicType mtAllMusic = new MusicType(1);
					AllMusicTypes.Add(mtAllMusic.Order, mtAllMusic);
					GenericMusicTypes.Add(mtAllMusic.Order, mtAllMusic);
				}
				#endregion
				musicTypesK.Sort();
				MusicQ = new Or(musicTypesK.ConvertAll<Q>(mtk => new Q(EventMusicType.Columns.MusicTypeK, mtk)).ToArray());
			}
			#endregion
			#region Populate PlaceQ and SelectedPlaces
			List<int> placesK = new List<int>();
			if (u.HomePlaceK > 0)
			{
				placesK.Add(u.HomePlaceK);
				SelectedPlaces.Add(u.Home.Name, u.Home);
			}

			foreach (Place p in u.PlacesVisit(null, 0))
			{
				if (!placesK.Contains(p.K))
				{
					placesK.Add(p.K);
					SelectedPlaces.Add(p.Name, p);
				}
			}

			if (placesK.Count > 0)
			{
				placesK.Sort();
				PlaceQ = new Or(placesK.ConvertAll<Q>(pk => new Q(Venue.Columns.PlaceK, pk)).ToArray());
			}
			else
			{
				PlaceQ = new Q(false);
			}
			#endregion
		}
		#endregion
		#region GetMatchingEvents()
		/// <summary>
		/// this method uses result sets cached for a couple of days
		/// </summary>
		/// <returns></returns>
		public EventSet GetMatchingEvents()
		{
			Query eventsQ = new Query();
			eventsQ.NoLock = true;
			eventsQ.QueryCondition =
				new And(
					MusicQ,
					PlaceQ,
					new Q(Event.Columns.DateTime, QueryOperator.GreaterThanOrEqualTo, StartDate),
					new Q(Event.Columns.DateTime, QueryOperator.LessThan, EndDate.AddDays(1)
				)
			);
			eventsQ.Columns = ColumnSet;
			//	eventsQ.TableElement = TableElement;

			//Distinct - removed - seems to be faster to select all music types and skip duplicate events!
			eventsQ.Distinct = true;
			eventsQ.DistinctColumn = Event.Columns.K;
			eventsQ.TableElement = DistinctTableElement;
			eventsQ.DataTableElement = DataTableElement;

			eventsQ.OrderBy = OrderBy;
			eventsQ.CacheDuration = new TimeSpan(2, 0, 0, 0);
			return new EventSet(eventsQ);
		}
		#endregion

		#region BuildString
		public Return BuildString(StringBuilder sb)
		{
			int count = 0;
			EventSet allEvents = this.GetMatchingEvents();
			StringWriter s = new StringWriter(sb);
			HtmlTextWriter w = new HtmlTextWriter(s);

			int displayMode = 1;

			bool hasFreeGuestlist = false;
			bool hasTickets = false;

			//	int displayMode = 0;
			//	if (AllEvents.Count < 60)
			//		displayMode = 1;
			//	else if (AllEvents.Count < 120)
			//		displayMode = 2;
			//	else
			//		displayMode = 3;


			for (DateTime d = StartDate; d <= EndDate; d = d.AddDays(1))
			{
				int eventsDoneToday = 0;
				Dictionary<Event.StartTimes, int> eventsDoneTimeOfDay = new Dictionary<Event.StartTimes, int>();
				eventsDoneTimeOfDay[Event.StartTimes.Morning] = 0;
				eventsDoneTimeOfDay[Event.StartTimes.Daytime] = 0;
				eventsDoneTimeOfDay[Event.StartTimes.Evening] = 0;
				//	int eventsDoneMorning = 0;
				//	int eventsDoneDaytime = 0;
				//	int eventsDoneEvening = 0;
				while (count < allEvents.Count && allEvents[count].DateTime.Equals(d))
				{
					Event ev = allEvents[count];

					

					if (eventsDoneToday == 0)
					{
						w.Write("[h1]" + d.DayOfWeek.ToString() + " " + d.ToString("d MMMM") + "[/h1]\n");
						//w.Write(@"<div style=""width:500px;padding:5px 0px 5px 0px; background-color:FECA26;"" class=""BorderBlack All"">");
						w.Write("[div]");
					}
					eventsDoneToday++;

					if (eventsDoneTimeOfDay[ev.StartTime] == 0 && (!ev.StartTime.Equals(Event.StartTimes.Evening) || eventsDoneTimeOfDay[Event.StartTimes.Morning] > 0 || eventsDoneTimeOfDay[Event.StartTimes.Daytime] > 0))
					{
						w.AddStyleAttribute("padding", "4px 10px 4px 10px");
						w.AddStyleAttribute("font-family", "Verdana, sans-serif");
						w.AddStyleAttribute("font-size", "14px");
						w.AddStyleAttribute("line-height", "130%");
						w.AddStyleAttribute("font-weight", "bold");
						w.RenderBeginTag("div");
						w.Write(ev.StartTime.ToString());
						w.Write(" events");
						w.RenderEndTag();//div
					}
					eventsDoneTimeOfDay[ev.StartTime]++;
					if (eventsDoneTimeOfDay[ev.StartTime] > GetMaxEvents(d.DayOfWeek, ev.StartTime, NyeSpecial))
					{
						if (eventsDoneTimeOfDay[ev.StartTime] == GetMaxEvents(d.DayOfWeek, ev.StartTime, NyeSpecial) + 1)
						{

							w.AddAttribute("href", "[LOGIN(" + UrlInfo.PageUrl("mycalendar", "d", d.Day, "m", d.Month, "y", d.Year) + ")]");
							w.RenderBeginTag("a");

							//<div align="right"><small><a href="/2007/jul/archive/articles" id="Content_Latest_LatestContentUc_ArticlesArchiveAnchor">Articles archive<img src="/gfx/icon-calendar.png" border="0" align="absmiddle" style="margin-left:3px;" width="26" height="21"></a></small></div>
							w.AddStyleAttribute("padding", "4px 10px 4px 10px");
							w.AddStyleAttribute("font-size", "10px");
							w.AddStyleAttribute("font-family", "Verdana, sans-serif");
							//	w.AddStyleAttribute("font-size", "14px");
							//	w.AddStyleAttribute("line-height", "130%");
							w.AddStyleAttribute("font-weight", "bold");
							w.RenderBeginTag("div");

							//<img src="/gfx/icon-calendar.png" border="0" align="absmiddle" style="margin-left:3px;" width="26" height="21">
							w.AddAttribute("src", "[WEB-ROOT]gfx/icon-calendar.png");
							w.AddAttribute("border", "0");
							w.AddAttribute("align", "absmiddle");
							w.AddAttribute("width", "26");
							w.AddAttribute("height", "21");
							w.AddStyleAttribute("margin-right", "3px");
							w.RenderBeginTag("img");
							w.RenderEndTag();//img

							w.Write("We're not showing all ");
							w.Write(d.DayOfWeek.ToString());
							w.Write(" ");
							w.Write(ev.StartTime.ToString().ToLower());
							w.Write(" events - click here for the calendar.");

							w.RenderEndTag();//div

							w.RenderEndTag();//a

						}
						count++;
						continue;
					}

					bool showPic = displayMode == 1 || ev.HasHilight;
					bool showDescription = displayMode == 1 || ev.HasHilight;
					bool showMusic = displayMode == 1 || displayMode == 2 || ev.HasHilight;

					#region get strings
					int currentEventK = ev.K;

					//string titleNote = "";
					//if (ev.StartTime.Equals(Event.StartTimes.Daytime))
					//    titleNote = " <b>(daytime)</b>";
					//else if (ev.StartTime.Equals(Event.StartTimes.Morning))
					//    titleNote = " <b>(morning)</b>";

					string regionUrlPart = "";
					if (ev.ExtraSelectElements["Place_RegionAbbreviation"].ToString().Length > 0)
						regionUrlPart = "/" + ev.ExtraSelectElements["Place_RegionAbbreviation"].ToString();

					string placeUrl = "/" + ev.ExtraSelectElements["Country_UrlName"].ToString() + regionUrlPart + "/" + ev.ExtraSelectElements["Place_UrlName"].ToString();
					string placeName = ev.ExtraSelectElements["Place_Name"].ToString();

					string countryK = ev.ExtraSelectElements["Place_CountryK"].ToString();

					string venueUrl = placeUrl + "/" + ev.ExtraSelectElements["Venue_UrlName"].ToString();
					string venueName = ev.ExtraSelectElements["Venue_Name"].ToString();

					string eventUrl = venueUrl + "/" + ev.DateTime.ToString("yyyy") + "/" + ev.DateTime.ToString("MMM").ToLower() + "/" + ev.DateTime.ToString("dd") + "/" + "event-" + ev.K.ToString();
					string eventName = ev.Name.ToString();
					#endregion

					w.AddStyleAttribute("font-family", "Arial, sans-serif");
					//w.AddStyleAttribute("line-height", "130%");
					w.AddStyleAttribute("font-size", "11px");

					if (ev.HasHilight)
					{
						w.AddStyleAttribute("padding", "4px 5px 4px 5px");
						w.RenderBeginTag("div");

						w.AddStyleAttribute("background-color", "#fff9e5");
						w.AddStyleAttribute("padding", "4px 4px 6px 4px");
						w.AddStyleAttribute("border", "1px solid #a28322");
						w.RenderBeginTag("div");
					}
					else
					{
						w.AddStyleAttribute("padding", "4px 10px 4px 10px");
						w.RenderBeginTag("div");
					}

					if (ev.HasPic && showPic)
					{

						w.AddAttribute("href", "[LOGIN(" + eventUrl + ")]");
						w.RenderBeginTag("a");
						w.AddAttribute("class", "BorderBlack All");
						w.AddStyleAttribute("margin", "3px 3px 3px 3px");
						//w.AddStyleAttribute("margin", "3px 6px 3px 0px");
						w.AddAttribute("src", ev.PicPath);
						w.AddAttribute("align", "right");
						//w.AddAttribute("align", "left");
						w.AddAttribute("height", "36");
						w.AddAttribute("width", "36");
						w.AddAttribute("border", "0");
						w.RenderBeginTag("img");
						w.RenderEndTag();//img
						w.RenderEndTag();//a
					}

					w.RenderBeginTag("b");
					w.AddAttribute("href", "[LOGIN(" + eventUrl + ")]");
					w.RenderBeginTag("a");

					if (ev.IsTicketsAvailable)
					{
						hasTickets = true;
						//Tickets icon
						w.AddAttribute("src", "[WEB-ROOT]gfx/icon-tickets-small.png");
						w.AddAttribute("width", "20");
						w.AddAttribute("height", "16");
						w.AddAttribute("align", "left");
						w.AddAttribute("border", "0");
						w.AddStyleAttribute("margin", "2px 3px 0px 0px");
						w.RenderBeginTag("img");
						w.RenderEndTag();//img
					}

					if (ev.SpotterRequest.HasValue && ev.SpotterRequest.Value)
					{
						hasFreeGuestlist = true;
						//Free guestlist icon
						w.AddAttribute("src", "[WEB-ROOT]gfx/icon-freeguestlist-small.png");
						w.AddAttribute("width", "20");
						w.AddAttribute("height", "16");
						w.AddAttribute("align", "left");
						w.AddAttribute("border", "0");
						w.AddStyleAttribute("margin", "2px 3px 0px 0px");
						w.RenderBeginTag("img");
						w.RenderEndTag();//img
					}

					w.Write(Cambro.Misc.Utility.Snip(eventName, 40));
					w.RenderEndTag();//a
					w.RenderEndTag();//b
					w.Write(" @ ");
					w.AddAttribute("href", "[LOGIN(" + venueUrl + ")]");
					w.RenderBeginTag("a");
					w.Write(venueName);
					w.RenderEndTag();//a
					w.Write(" in ");
					w.AddAttribute("href", "[LOGIN(" + placeUrl + ")]");
					w.RenderBeginTag("a");
					w.Write(placeName);
					w.RenderEndTag();//a
					//w.Write(titleNote);

					if (ev.ShortDetailsHtml.Length > 0 && showDescription)
					{
						w.Write("<br>");
						w.Write(Cambro.Misc.Utility.Snip(Cambro.Web.Helpers.Strip(ev.ShortDetailsHtml), 80));
					}

					if (ev.MusicTypesString.Length > 0 && showMusic)
					{
						w.Write("<br>(");
						w.Write(Cambro.Misc.Utility.Snip(ev.MusicTypesString, 80));
						w.Write(")");
					}
					if (ev.HasHilight)
						w.RenderEndTag();//div
					w.RenderEndTag();//div for margin
					count++;
				}
				if (eventsDoneToday > 0)
					w.Write("[/div]");
			}
			w.Flush();

			Return r = new Return();
			r.Events = allEvents.Count;
			r.HasFreeGuestlist = hasFreeGuestlist;
			r.HasTickets = hasTickets;

			return r;
		}
		#endregion

		static int GetMaxEvents(DayOfWeek d, Event.StartTimes s, bool nyeSpecial)
		{
			if (nyeSpecial)
			{
				int[] i = new int[3];
				if (d.Equals(DayOfWeek.Monday))
					i = new int[] { 5, 10, 40 };
				else if (d.Equals(DayOfWeek.Tuesday))
					i = new int[] { 20, 10, 10 };

				if (s.Equals(Event.StartTimes.Morning))
					return i[0];
				else if (s.Equals(Event.StartTimes.Daytime))
					return i[1];
				else
					return i[2];
			}
			else
			{
				int[] i = new int[3];
				if (d.Equals(DayOfWeek.Sunday))
					i = new int[] { 5, 5, 10 };
				else if (d.Equals(DayOfWeek.Monday))
					i = new int[] { 2, 2, 5 };
				else if (d.Equals(DayOfWeek.Tuesday))
					i = new int[] { 2, 2, 5 };
				else if (d.Equals(DayOfWeek.Wednesday))
					i = new int[] { 2, 2, 5 };
				else if (d.Equals(DayOfWeek.Thursday))
					i = new int[] { 2, 2, 10 };
				else if (d.Equals(DayOfWeek.Friday))
					i = new int[] { 2, 2, 20 };
				else if (d.Equals(DayOfWeek.Saturday))
					i = new int[] { 5, 5, 20 };

				if (s.Equals(Event.StartTimes.Morning))
					return i[0];
				else if (s.Equals(Event.StartTimes.Daytime))
					return i[1];
				else
					return i[2];
			}
		}

		static void AddMusicTypeChildren(MusicType mt, ref List<int> musicTypes)
		{
			if (mt.Children.Count > 0)
			{
				foreach (MusicType mtChild in mt.Children)
				{
					if (!musicTypes.Contains(mtChild.K))
					{
						musicTypes.Add(mtChild.K);
					}
					if (mtChild.ParentK == 1)
					{
						AddMusicTypeChildren(mtChild, ref musicTypes);
					}
				}
			}
		}
	}
	#endregion

	#region UsrPlaceVisit

	#endregion

	#region UsrEventAttended
	/// <summary>
	/// Links a user to many events (I went to this event)
	/// </summary>
	[Serializable]
	public partial class UsrEventAttended
	{

		#region simple members
		/// <summary>
		/// Link to Usr table
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[UsrEventAttended.Columns.UsrK]; }
			set { usr = null; this[UsrEventAttended.Columns.UsrK] = value; }
		}
		/// <summary>
		/// Link to the Event table - an event that the user attended
		/// </summary>
		public override int EventK
		{
			get { return (int)this[UsrEventAttended.Columns.EventK]; }
			set { _event = null; this[UsrEventAttended.Columns.EventK] = value; }
		}
		/// <summary>
		/// Do we send emails about this event?
		/// </summary>
		public override bool SendUpdate
		{
			get { return (bool)this[UsrEventAttended.Columns.SendUpdate]; }
			set { this[UsrEventAttended.Columns.SendUpdate] = value; }
		}
		/// <summary>
		/// Is this user taking pictures?
		/// </summary>
		public override bool Spotter
		{
			get { return (bool)this[UsrEventAttended.Columns.Spotter]; }
			set { this[UsrEventAttended.Columns.Spotter] = value; }
		}
		#endregion

		#region Links to Bobs
		#region Usr
		public Usr Usr
		{
			get
			{
				if (usr == null)
					usr = new Usr(UsrK);
				return usr;
			}
		}
		Usr usr;
		#endregion
		#region Event
		public Event Event
		{
			get
			{
				if (_event == null)
					_event = new Event(EventK);
				return _event;
			}
		}
		Event _event;
		#endregion
		#endregion

	}
	#endregion

	#region UsrEventGuestlist
	/// <summary>
	/// Links a user to many events (I went to this event)
	/// </summary>
	[Serializable]
	public partial class UsrEventGuestlist
	{

		#region simple members
		/// <summary>
		/// Link to Usr table
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[UsrEventGuestlist.Columns.UsrK]; }
			set { usr = null; this[UsrEventGuestlist.Columns.UsrK] = value; }
		}
		/// <summary>
		/// Link to the Event table - an event
		/// </summary>
		public override int EventK
		{
			get { return (int)this[UsrEventGuestlist.Columns.EventK]; }
			set { _event = null; this[UsrEventGuestlist.Columns.EventK] = value; }
		}
		/// <summary>
		/// DateTime when the usr signed up
		/// </summary>
		public override DateTime DateTime
		{
			get { return (DateTime)this[UsrEventGuestlist.Columns.DateTime]; }
			set { this[UsrEventGuestlist.Columns.DateTime] = value; }
		}
		#endregion

		#region Links to Bobs
		#region Usr
		public Usr Usr
		{
			get
			{
				if (usr == null)
					usr = new Usr(UsrK);
				return usr;
			}
		}
		Usr usr;
		#endregion
		#region Event
		public Event Event
		{
			get
			{
				if (_event == null)
					_event = new Event(EventK);
				return _event;
			}
		}
		Event _event;
		#endregion
		#endregion

	}
	#endregion

	#region UsrPhotoFavourite
	/// <summary>
	/// Links a user to many photos (my favorite photos)
	/// </summary>
	[Serializable]
	public partial class UsrPhotoFavourite
	{

		#region simple members
		/// <summary>
		/// Primary K - not clustered index
		/// </summary>
		public override int K
		{
			get { return this[UsrPhotoFavourite.Columns.K] as int? ?? 0; }
			set { this[UsrPhotoFavourite.Columns.K] = value; }
		}
		/// <summary>
		/// Link to Usr table
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[UsrPhotoFavourite.Columns.UsrK]; }
			set { usr = null; this[UsrPhotoFavourite.Columns.UsrK] = value; }
		}
		/// <summary>
		/// Link to the Photo table
		/// </summary>
		public override int PhotoK
		{
			get { return (int)this[UsrPhotoFavourite.Columns.PhotoK]; }
			set { photo = null; this[UsrPhotoFavourite.Columns.PhotoK] = value; }
		}
		/// <summary>
		/// When was this favourite added?
		/// </summary>
		public override DateTime? DateTimeAdded
		{
			get { return (DateTime?)this[UsrPhotoFavourite.Columns.DateTimeAdded]; }
			set { this[UsrPhotoFavourite.Columns.DateTimeAdded] = value; }
		}
		#endregion

		#region Links to Bobs
		#region Usr
		public Usr Usr
		{
			get
			{
				if (usr == null)
					usr = new Usr(UsrK);
				return usr;
			}
		}
		Usr usr;
		#endregion
		#region Photo
		public Photo Photo
		{
			get
			{
				if (photo == null)
					photo = new Photo(PhotoK);
				return photo;
			}
		}
		Photo photo;
		#endregion
		#endregion

		readonly static string fetchSql = "SELECT TOP 1 * FROM [" + Tables.GetTableName(TablesEnum.UsrPhotoFavourite) + "] WITH (NOLOCK) WHERE [" + Tables.GetColumnName(UsrPhotoFavourite.Columns.UsrK) + "] = @UsrK AND [" + Tables.GetColumnName(UsrPhotoFavourite.Columns.PhotoK) + "] = @PhotoK";
		public UsrPhotoFavourite(int usrK, int photoK)
			: this()
		{

			using (SqlConnection conn = new SqlConnection(Common.Properties.ConnectionString))
			{
				using (SqlCommand cmd = new SqlCommand(fetchSql, conn))
				{

					cmd.Parameters.AddWithValue("@UsrK", usrK);
					cmd.Parameters.AddWithValue("@PhotoK", photoK);
					using (var adapter = new SqlDataAdapter(cmd))
					{
						conn.Open();
						DataTable dt = new DataTable();
						adapter.Fill(dt);
						if (dt.Rows.Count == 0)
						{
							throw new BobNotFound();
						}
						else
						{
							Initialise(dt.Rows[0]);
						}
					}
				}
			}
		}
	}
	#endregion

	#region UsrPhotoMe
	/// <summary>
	/// Links a user to many photos (photos of me)
	/// </summary>
	[Serializable]
	public partial class UsrPhotoMe 
	{

		#region simple members
		public override int K
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}
		/// <summary>
		/// Link to Usr table
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[UsrPhotoMe.Columns.UsrK]; }
			set { usr = null; this[UsrPhotoMe.Columns.UsrK] = value; }
		}
		/// <summary>
		/// Link to the Photo table
		/// </summary>
		public override int PhotoK
		{
			get { return (int)this[UsrPhotoMe.Columns.PhotoK]; }
			set { photo = null; this[UsrPhotoMe.Columns.PhotoK] = value; }
		}
		#endregion

		#region Links to Bobs
		#region Usr
		public Usr Usr
		{
			get
			{
				if (usr == null)
					usr = new Usr(UsrK);
				return usr;
			}
		}
		Usr usr;
		#endregion
		#region Photo
		public Photo Photo
		{
			get
			{
				if (photo == null)
					photo = new Photo(PhotoK);
				return photo;
			}
		}
		Photo photo;
		#endregion
		#endregion

		readonly static string fetchSql = "SELECT TOP 1 * FROM [" + Tables.GetTableName(TablesEnum.UsrPhotoMe) + "] WITH (NOLOCK) WHERE [" + Tables.GetColumnName(UsrPhotoMe.Columns.UsrK) + "] = @UsrK AND [" + Tables.GetColumnName(UsrPhotoMe.Columns.PhotoK) + "] = @PhotoK";
		public UsrPhotoMe(int usrK, int photoK)
			: this()
		{

			using (SqlConnection conn = new SqlConnection(Common.Properties.ConnectionString)){
				using (SqlCommand cmd = new SqlCommand(fetchSql, conn))
				{
						
					cmd.Parameters.AddWithValue("@UsrK", usrK);
					cmd.Parameters.AddWithValue("@PhotoK", photoK);
					using (var adapter = new SqlDataAdapter(cmd))
					{
						conn.Open();
						DataTable dt = new DataTable();
						adapter.Fill(dt);
						if (dt.Rows.Count == 0)
						{
							throw new BobNotFound();
						}
						else
						{
							Initialise(dt.Rows[0]);
						}
					}
				}
			}
		}
	}
	#endregion

	#region UsrMusicTypeFavourite
	/// <summary>
	/// Links a user to many music types (music I listen to)
	/// </summary>
	[Serializable]
	public partial class UsrMusicTypeFavourite
	{

		#region simple members
		/// <summary>
		/// Link to Usr table
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[UsrMusicTypeFavourite.Columns.UsrK]; }
			set { usr = null; this[UsrMusicTypeFavourite.Columns.UsrK] = value; }
		}
		/// <summary>
		/// Link to the MusicType table - a music type that the user listens to
		/// </summary>
		public override int MusicTypeK
		{
			get { return (int)this[UsrMusicTypeFavourite.Columns.MusicTypeK]; }
			set { musicType = null; this[UsrMusicTypeFavourite.Columns.MusicTypeK] = value; }
		}
		#endregion

		#region Links to Bobs
		#region Usr
		public Usr Usr
		{
			get
			{
				if (usr == null)
					usr = new Usr(UsrK);
				return usr;
			}
		}
		Usr usr;
		#endregion
		#region MusicType
		public MusicType MusicType
		{
			get
			{
				if (musicType == null)
					musicType = new MusicType(MusicTypeK, this, UsrMusicTypeFavourite.Columns.MusicTypeK);
				return musicType;
			}
		}
		MusicType musicType;
		#endregion
		#endregion

	}
	#endregion

	#region UsrDate

	#endregion

	#region UsrDataHolder
	[Serializable]
	public class UsrDataHolder : BobDataHolder
	{
		#region Variables
		private int k = 0;
		private int salesTeam = 0;
		private string firstName = "";
		private string lastName = "";
		private string nickName = "";
		#endregion

		#region Properties
		/// <summary>
		/// The primary key
		/// </summary>
		public int K
		{
			get { return this.k; }
			set
			{
				if (value == 0)
					this.State = DataHolderState.Added;
				else
					this.State = DataHolderState.Modified;
				this.k = value;
			}
		}
		/// <summary>
		/// First Name
		/// </summary>
		public string FirstName
		{
			get { return this.firstName; }
			set
			{
				if (this.K == 0)
					this.State = DataHolderState.Added;
				else
					this.State = DataHolderState.Modified;
				this.firstName = value;
			}
		}
		/// <summary>
		/// Last Name
		/// </summary>
		public string LastName
		{
			get { return this.lastName; }
			set
			{
				if (this.K == 0)
					this.State = DataHolderState.Added;
				else
					this.State = DataHolderState.Modified;
				this.lastName = value;
			}
		}
		/// <summary>
		/// NickName
		/// </summary>
		public string NickName
		{
			get { return this.nickName; }
			set
			{
				if (this.K == 0)
					this.State = DataHolderState.Added;
				else
					this.State = DataHolderState.Modified;
				this.nickName = value;
			}
		}
		/// <summary>
		/// Sales Team
		/// </summary>
		public int SalesTeam
		{
			get { return this.salesTeam; }
			set
			{
				if (this.K == 0)
					this.State = DataHolderState.Added;
				else
					this.State = DataHolderState.Modified;
				this.salesTeam = value;
			}
		}

		#endregion

		#region Constructors
		public UsrDataHolder()
		{
			this.State = DataHolderState.Added;
		}

		public UsrDataHolder(Usr usr)
		{
			this.State = DataHolderState.Unchanged;
			this.k = usr.K;
			if (usr.FirstName != null)
				this.firstName = usr.FirstName;
			if (usr.LastName != null)
				this.lastName = usr.LastName;
			if (usr.NickName != null)
				this.nickName = usr.NickName;
			this.salesTeam = usr.SalesTeam;
		}
		#endregion
	}


	#endregion
}
