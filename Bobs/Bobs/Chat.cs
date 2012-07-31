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
using System.Xml;
using SpottedScript.Controls.ChatClient.Shared;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Bobs.JobProcessor;



namespace Bobs
{

	#region Chat
	[Serializable] 
	public partial class Chat
	{

		#region simple members
		/// <summary>
		/// The usr
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[Chat.Columns.UsrK]; }
			set { this[Chat.Columns.UsrK] = value; }
		}
		/// <summary>
		/// Xml string containing chat items
		/// </summary>
		public override string ChatItems
		{
			get { return (string)this[Chat.Columns.ChatItems]; }
			set { this[Chat.Columns.ChatItems] = value; }
		}
		/// <summary>
		/// Long reprasenting the datetime of the last chat item in ticks
		/// </summary>
		public override long LastChatItem
		{
			get { return (long)this[Chat.Columns.LastChatItem]; }
			set { this[Chat.Columns.LastChatItem] = value; }
		}
		/// <summary>
		/// Session id used to stop multi-browser use
		/// </summary>
		public override int SessionId
		{
			get { return (int)this[Chat.Columns.SessionId]; }
			set { this[Chat.Columns.SessionId] = value; }
		}
		#endregion


		#region GetMessageFromCommentBody
		public static string GetMessageFromCommentBody(string text)
		{
			//Lets prepare the body of the message for chatting...
			//Remove quoted text...
			Regex rQuotes = new Regex(@"<dsi:quote[^>]*>.*?</dsi:quote>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			text = rQuotes.Replace(text, "(quote) ");

			Regex rLinks = new Regex(@"<dsi:object type=""url"" href=""([^""]*)"" [^/]*/>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			text = rLinks.Replace(text, @"$1");

			//Regex rRoom = new Regex(@"<dsi:object type=""room"" href=""([a-zA-Z0-9]+)"" />", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			//text = rRoom.Replace(text, @"[room://$1]");

			text = Cambro.Web.Helpers.Strip(text, true, true, true, true);

			return GetMessageFromChatBox(text);
		}
		#endregion
		#region GetMessageFromChatBox
		public static string GetMessageFromChatBox(string text)
		{
			string textBackup = text;
			Regex chatDsiHttpRegex = new Regex(@"([ ]?)(http\://www\.dontstayin\.com[^ ]+)([ ]?)");
			Regex chatHttpRegex = new Regex(@"([^""]|^)(http\://([^ /]+)[^ ]*)([ ]?)");

			text = Cambro.Web.Helpers.Strip(text, true, true, true, true);

			string textNoReplace = text;
			text = chatDsiHttpRegex.Replace(text, "$1<a href=\"$2\">link</a>$3");
			text = chatHttpRegex.Replace(text, "$1<a href=\"$2\" target=\"_blank\">$3</a>$4");

			//text = System.Web.HttpUtility.HtmlEncode(text);

			text = Cambro.Web.Helpers.CleanHtml(text);
		//	HtmlRenderer r = new HtmlRenderer();
		//	r.LoadHtml(text);
		//	text = r.GetHtmlForEditorControl();

			if (text.Length > 200)
			{
				text = System.Web.HttpUtility.HtmlEncode(System.Web.HttpUtility.HtmlDecode(textBackup.Substring(0, 190)));

				//if (textNoReplace.Length > 200)
				//    text = textNoReplace.Substring(0, 200);
				//else
				//    text = textNoReplace;
			}
			return text;
		}
		#endregion
		#region GetStateFromCacheOrDatabase
		public static StateStub[] GetStateFromCacheOrDatabase(Guid[] roomGuids, int usrK, Guid dsiGuid, RoomPinSet roomPinSet)
		{
			try
			{
				Dictionary<Guid, RoomPin> roomPins = new Dictionary<Guid, RoomPin>();
				if (roomPinSet != null) roomPinSet.ToList().ForEach((rp) => { if (!roomPins.ContainsKey(rp.RoomGuid)) roomPins.Add(rp.RoomGuid, rp); });

				string[] serializedStatesFromCache = null;
				if (usrK > 0)
					serializedStatesFromCache = Caching.Instances.Main.MultiGet<string>(roomGuids.ConvertAll(g => Chat.GetRoomStateCacheHolderKey(usrK, g).ToString()));
				else
					serializedStatesFromCache = Caching.Instances.Main.MultiGet<string>(roomGuids.ConvertAll(g => Chat.GetRoomStateCacheHolderKey(dsiGuid, g).ToString()));

				if (serializedStatesFromCache.Length == 0)
					return null;

				Dictionary<Guid, StateStub> states = new Dictionary<Guid, StateStub>();
				foreach (string serializedState in serializedStatesFromCache)
				{
					if (serializedState != null && serializedState.Length > 0)
					{
						try
						{
							StateStub s = deSerializeStateStub(serializedState);
							if (s != null && s.guid.Length > 0)
								states.Add(s.guid.UnPackGuid(), s);
						}
						catch { }
					}
				}

				if (roomPins.Count > 0)
				{
					foreach (Guid g in roomGuids)
					{
						if (!states.ContainsKey(g) && roomPins.ContainsKey(g) && roomPins[g].StateStub != null && roomPins[g].StateStub.Length > 0)
						{
							try
							{
								StateStub ss = Chat.deSerializeStateStub(roomPins[g].StateStub);
								if (ss != null && ss.guid.Length > 0)
									states.Add(g, ss);
							}
							catch { }
						}
					}
				}

				if (states.Count == 0)
					return null;
				else
				{
					StateStub[] ssArray = new StateStub[states.Count];
					states.Values.CopyTo(ssArray, 0);
					return ssArray;
				}
			}
			catch
			{
				return null;
			}
		}
		#endregion

		#region GetStateFromCache
		public static StateStub[] GetStateFromCache(Guid[] roomGuids, int usrK, Guid dsiGuid)
		{
			return GetStateFromCacheOrDatabase(roomGuids, usrK, dsiGuid, null);
		}
		#endregion

		#region StoreStateInCache
		public static void StoreStateInCache(StateStub[] roomState, int usrK, Guid dsiGuid)
		{
			StoreRoomStateAsync job = new StoreRoomStateAsync(roomState, usrK, dsiGuid);

			System.Threading.Thread thread = Utilities.GetSafeThread(() => job.Execute());
			thread.Start();
		}
		#endregion
		#region StoreRoomStateAsync
		public class StoreRoomStateAsync
		{
			int UsrK;
			Guid DsiGuid;
			SerializedStateStubHolder[] State;
			public StoreRoomStateAsync(StateStub[] state, int usrK, Guid dsiGuid)
			{
				List<SerializedStateStubHolder> goodStates = new List<SerializedStateStubHolder>();
				foreach (StateStub ss in state)
				{
					try
					{
						if (ss != null)
						{
							Guid g = ss.guid.UnPackGuid();
							Chat.RoomSpec spec = Chat.RoomSpec.FromGuid(g);
							if (spec != null)
								goodStates.Add(new SerializedStateStubHolder(g, SerializeStateStub(ss)));
						}
					}
					catch { }
				}

				State = goodStates.ToArray();
				UsrK = usrK;
				DsiGuid = dsiGuid;
			}
			public void Execute()
			{
				if (State.Length > 0)
				{
					if (UsrK > 0)
					{
						Caching.Instances.Main.MultiSet(State.ConvertAll(s => new KeyValuePair<string, string>(Chat.GetRoomStateCacheHolderKey(UsrK, s.Guid).ToString(), s.SerializedStateStub)), DateTime.MaxValue);
						Caching.Instances.Main.Set(Chat.GetStateDirtyKey(UsrK).ToString(), DateTime.Now.Ticks.ToString());
					}
					else
					{
						Caching.Instances.Main.MultiSet(State.ConvertAll(s => new KeyValuePair<string, string>(Chat.GetRoomStateCacheHolderKey(DsiGuid, s.Guid).ToString(), s.SerializedStateStub)), DateTime.MaxValue);
						Caching.Instances.Main.Set(Chat.GetStateDirtyKey(DsiGuid).ToString(), DateTime.Now.Ticks.ToString());
					}
				}
			}
			public class SerializedStateStubHolder
			{
				public Guid Guid;
				public string SerializedStateStub;
				public SerializedStateStubHolder(Guid guid, string serializedStateStub)
				{
					this.Guid = guid;
					this.SerializedStateStub = serializedStateStub;
				}
			}
		}
		public static string SerializeStateStub(StateStub state)
		{
			string XmlizedString = null;
			MemoryStream memoryStream = new MemoryStream();
			XmlSerializer xs = new XmlSerializer(typeof(StateStub));
			XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
			xs.Serialize(xmlTextWriter, state);
			memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
			
			UTF8Encoding encoding = new UTF8Encoding();
			XmlizedString = encoding.GetString(memoryStream.ToArray());

			return XmlizedString;
		}
		static StateStub deSerializeStateStub(string xml)
		{
			try
			{
				XmlSerializer xs = new XmlSerializer(typeof(StateStub));

				UTF8Encoding encoding = new UTF8Encoding();
				Byte[] byteArray = encoding.GetBytes(xml);

				MemoryStream memoryStream = new MemoryStream(byteArray);
				XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
				return (StateStub)xs.Deserialize(memoryStream);
			}
			catch
			{
				return null;
			}
		}
		#endregion

		#region GetDsiGuidWithoutTouchingDatabase()
		public static Guid GetDsiGuidWithoutTouchingDatabase()
		{
			Guid g = Guid.Empty;
			if (HttpContext.Current != null && HttpContext.Current.Request.Cookies["DsiGuid"] != null)
			{
				//We have a Guid from the client...
				try
				{
					g = new Guid(HttpContext.Current.Request.Cookies["DsiGuid"].Value);
				}
				catch { } //We might get a corrupt cookie from the client...
			}
			return g;
		}
		#endregion

		#region GetUsrKWithoutTouchingDatabase()
		public static int GetUsrKWithoutTouchingDatabase()
		{
			int usrK = 0;
			if (HttpContext.Current != null && HttpContext.Current.User.Identity.IsAuthenticated)
			{
				string usrStr = HttpContext.Current.User.Identity.Name;
				string[] split = usrStr.Split('&');
				usrK = int.Parse(split[0]);
			}
			return usrK;
		}
		#endregion

		[Serializable]
		public class RoomStateCacheHolder
		{
			public StateStub State;
			public RoomStateCacheHolder(StateStub state)
			{
				State = state;
			}
		}
		public static string KeyVersion
		{
			get
			{
				return "8";
			}
		}
		public static Caching.CacheKey GetRoomStateCacheHolderKey(int UsrK, Guid roomGuid)
		{
			return new Caching.CacheKey(Caching.CacheKeyPrefix.ChatClientRoomState, "UsrK", UsrK.ToString(), "RoomGuid", roomGuid.ToString("N"), "Version", Chat.KeyVersion);
		}
		public static Caching.CacheKey GetRoomStateCacheHolderKey(Guid DsiGuid, Guid roomGuid)
		{
			return new Caching.CacheKey(Caching.CacheKeyPrefix.ChatClientRoomState, "DsiGuid", DsiGuid.ToString("N"), "RoomGuid", roomGuid.ToString("N"), "Version", Chat.KeyVersion);
		}
		public static Caching.CacheKey GetStateDirtyKey(int UsrK)
		{
			return new Caching.CacheKey(Caching.CacheKeyPrefix.ChatClientStateDirty, "UsrK", UsrK.ToString(), "Version", Chat.KeyVersion);
		}
		public static Caching.CacheKey GetStateDirtyKey(Guid DsiGuid)
		{
			return new Caching.CacheKey(Caching.CacheKeyPrefix.ChatClientStateDirty, "DsiGuid", DsiGuid.ToString("N"), "Version", Chat.KeyVersion);
		}

		public class RoomSpec
		{

			#region Version
			public int Version
			{
				get
				{
					return version;
				}
				set
				{
					guid = Guid.Empty;
					version = value;
				}
			}
			public int version;
			#endregion
			#region RoomType
			public RoomType RoomType
			{
				get
				{
					return roomType;
				}
				set
				{
					guid = Guid.Empty;
					roomType = value;
				}
			}
			RoomType roomType;
			#endregion
			#region ObjectType
			public Model.Entities.ObjectType ObjectType
			{
				get
				{
					return objectType;
				}
				set
				{
					guid = Guid.Empty;
					objectBob = null;
					triedGetObject = false;
					objectType = value;
				}
			}
			Model.Entities.ObjectType objectType;
			#endregion
			#region ObjectK
			public int ObjectK
			{
				get
				{
					return objectK;
				}
				set
				{
					guid = Guid.Empty;
					objectBob = null;
					triedGetObject = false;
					objectK = value;
				}
			}
			int objectK;
			#endregion
			#region SecondObjectType
			public Model.Entities.ObjectType SecondObjectType
			{
				get
				{
					return secondObjectType;
				}
				set
				{
					guid = Guid.Empty;
					secondObjectBob = null;
					triedGetSecondObject = false;
					secondObjectType = value;
				}
			}
			Model.Entities.ObjectType secondObjectType;
			#endregion
			#region SecondObjectK
			public int SecondObjectK
			{
				get
				{
					return secondObjectK;
				}
				set
				{
					guid = Guid.Empty;
					secondObjectBob = null;
					triedGetSecondObject = false;
					secondObjectK = value;
				}
			}
			int secondObjectK;
			#endregion
			#region Disabled
			public bool Disabled
			{
				get
				{
					return !((RoomType == RoomType.Normal && ObjectType == Model.Entities.ObjectType.None) || (RoomType == RoomType.Normal && ObjectType == Model.Entities.ObjectType.Group) || RoomType == RoomType.PrivateChat || RoomType == RoomType.SystemMessages || RoomType == RoomType.PublicStream || RoomType == RoomType.PrivateChatRequests);
				}
			}
			#endregion

			#region Initialisers
			public RoomSpec() { }
			public RoomSpec(
				int version,
				RoomType roomType,
				Model.Entities.ObjectType objectType,
				int objectK,
				Model.Entities.ObjectType secondObjectType,
				int secondObjectK)
			{
				Version = version;
				RoomType = roomType;
				ObjectType = objectType;
				ObjectK = objectK;
				SecondObjectType = secondObjectType;
				SecondObjectK = secondObjectK;
			}

			public RoomSpec(
				RoomType roomType,
				Model.Entities.ObjectType objectType,
				int objectK,
				Model.Entities.ObjectType secondObjectType,
				int secondObjectK) :
				this(1, roomType, objectType, objectK, secondObjectType, secondObjectK) { }

			public RoomSpec(
				RoomType roomType,
				Model.Entities.ObjectType objectType,
				int objectK)
				: this(roomType, objectType, objectK, Model.Entities.ObjectType.None, 0) { }

			public RoomSpec(
				RoomType roomType)
				: this(roomType, Model.Entities.ObjectType.None, 0, Model.Entities.ObjectType.None, 0) { }

			public RoomSpec(
				int firstUsrK, int secondUsrK)
				: this(RoomType.PrivateChat, Model.Entities.ObjectType.Usr, firstUsrK < secondUsrK ? firstUsrK : secondUsrK, Model.Entities.ObjectType.Usr, firstUsrK < secondUsrK ? secondUsrK : firstUsrK) { }
			#endregion

			#region Guid, FromGuid
			#region Guid
			Guid guid;
			public Guid Guid
			{
				get
				{
					if (guid == Guid.Empty)
					{
						byte[] bytes = new byte[16];

						bytes[0] = System.BitConverter.GetBytes(Version)[0];
						bytes[1] = System.BitConverter.GetBytes((int)RoomType)[0];

						bytes[2] = System.BitConverter.GetBytes((int)ObjectType)[0];
						bytes[3] = System.BitConverter.GetBytes(ObjectK)[0];
						bytes[4] = System.BitConverter.GetBytes(ObjectK)[1];
						bytes[5] = System.BitConverter.GetBytes(ObjectK)[2];
						bytes[6] = System.BitConverter.GetBytes(ObjectK)[3];

						bytes[7] = System.BitConverter.GetBytes((int)SecondObjectType)[0];
						bytes[8] = System.BitConverter.GetBytes(SecondObjectK)[0];
						bytes[9] = System.BitConverter.GetBytes(SecondObjectK)[1];
						bytes[10] = System.BitConverter.GetBytes(SecondObjectK)[2];
						bytes[11] = System.BitConverter.GetBytes(SecondObjectK)[3];

						byte[] hash = getHash().ToByteArray();

						bytes[12] = hash[0];
						bytes[13] = hash[1];
						bytes[14] = hash[2];
						bytes[15] = hash[3];

						guid = new Guid(bytes);
					}
					return guid;
				}
			}
			#endregion
			#region FromGuid(Guid g)
			public static RoomSpec FromGuid(Guid g)
			{
				if (g == Guid.Empty)
					return null;

				try
				{
					RoomSpec r = new RoomSpec();
					byte[] bytes = g.ToByteArray();
					r.Version = (int)bytes[0];
					

					if (r.Version == 1)
					{
						r.RoomType = (RoomType)(int)bytes[1];
						r.ObjectType = (Model.Entities.ObjectType)(int)bytes[2];
						r.ObjectK = BitConverter.ToInt32(new byte[] { bytes[3], bytes[4], bytes[5], bytes[6] }, 0);
						r.SecondObjectType = (Model.Entities.ObjectType)(int)bytes[7];
						r.SecondObjectK = BitConverter.ToInt32(new byte[] { bytes[8], bytes[9], bytes[10], bytes[11] }, 0);

						byte[] hash = r.getHash().ToByteArray();

						if (bytes[12] != hash[0] ||
							bytes[13] != hash[1] ||
							bytes[14] != hash[2] ||
							bytes[15] != hash[3])
							return null;

						return r;
					}
					else
						return null;
				}
				catch
				{
					return null;
				}
			}
			#endregion
			#region getHash()
			Guid getHash()
			{
				string s = string.Format(
					"Version-{0}-RoomType-{1}-ObjectType-{2}-ObjectK-{3}-SecondObjectType-{4}-SecondObjectK-{5}-Salt-9d07ab20104b11ddbd0b0800200c9a66",
					Version, (int)RoomType, (int)ObjectType, ObjectK, (int)SecondObjectType, SecondObjectK);
				return Cambro.Misc.Utility.Hash(s);
			}
			#endregion
			#endregion

			#region CheckPermission
			public bool CheckPermission(Usr u, bool write)
			{
				try
				{
					if (Disabled)
						return false;

					if (write && IsReadOnly)
						return false;

					if (write && u == null)
						return false;

					if (RoomType == RoomType.Normal && ObjectType == Model.Entities.ObjectType.Thread)
					{
						Thread t = (Thread)ObjectBob;

						if (write && !t.CheckPermissionPost(u))
							return false;

						if (!write && !t.CheckPermissionRead(u))
							return false;
					}

					if (RoomType == RoomType.Normal && ObjectType == Model.Entities.ObjectType.Group)
					{
						Group g = (Group)ObjectBob;
						GroupUsr gu = g.GetGroupUsr(u);

						if (!g.CanViewHomePage(u, gu))
							return false;

						if (write && gu != null && gu.StatusPermissionLevel == -1)
							return false;

					}

					if (RoomType == RoomType.PrivateChat)
					{
						if (u.K != ObjectK && SecondObjectK > 0 && u.K != SecondObjectK)
							return false;
					}

					return true;
				}
				catch
				{
					return false;
				}
			}
			#endregion

			#region MightFailReadPermissionCheck
			/// <summary>
			/// This is a hint to tell the server whether to do an authentication check on the guest refresh...
			/// </summary>
			public bool MightFailReadPermissionCheck
			{
				get
				{
					return RoomType == RoomType.PrivateChat ||
						(RoomType == RoomType.Normal && ObjectType == Model.Entities.ObjectType.Thread) ||
						(RoomType == RoomType.Normal && ObjectType == Model.Entities.ObjectType.Group);

				}
			}
			#endregion

			#region GetMoreInfoHtml()
			public string GetMoreInfoHtml()
			{

				StringBuilder sb = new StringBuilder();
				if (RoomType == RoomType.Normal && ObjectType == Model.Entities.ObjectType.None)
				{
					sb.Append("<p>This is the general chat room.</p>");
				}
				else if (RoomType == RoomType.PrivateChat)
				{
					if (Usr.Current == null || (Usr.Current.K != this.ObjectK && Usr.Current.K != this.SecondObjectK))
						sb.Append("<p>This is a private chat room.</p>");
					else
					{
						Usr otherUsr = new Usr(Usr.Current.K == this.ObjectK ? this.SecondObjectK : this.ObjectK);
						sb.Append("<p>This is a private chat room between you and ");
						otherUsr.LinkAppend(sb, false);
						sb.Append("</p>");

						if (otherUsr.HasPic)
						{
							sb.Append("<p>");
							{
								sb.Append("<a");
								sb.AppendAttribute("href", otherUsr.Url());
								sb.Append(">");
								{
									sb.Append("<img");
									sb.AppendAttribute("src", Storage.Path(otherUsr.Pic));
									sb.AppendAttribute("class", "BorderBlack All");
									sb.AppendAttribute("width", "100");
									sb.AppendAttribute("height", "100");
									sb.Append(otherUsr.RolloverNoPic);
									sb.Append(">");
								}
								sb.Append("</a>");
							}
							sb.Append("</p>");
						}

					}

					

				}
				else if (RoomType == RoomType.Normal)
				{
					sb.Append("<p>");
					//sb.Append(string.Format("This is {0} chat room.", this.ObjectType.ToString().PrefixWithAOrAn(false)));
					string s = "";
					switch (ObjectType)
					{
						case Model.Entities.ObjectType.Country: { s = "This is a country chat room."; break; }
						case Model.Entities.ObjectType.Place: { s = "This is a place chat room."; break; }
						case Model.Entities.ObjectType.Venue: { s = "This is a venue chat room."; break; }
						case Model.Entities.ObjectType.Event: { s = "This is an event chat room."; break; }
						case Model.Entities.ObjectType.Article: { s = "This is an article chat room. All chat in this room will be posted as comments in the main article topic."; break; }
						case Model.Entities.ObjectType.Group: { s = "This is a group chat room."; break; }
						case Model.Entities.ObjectType.Photo: { s = "This is a photo chat room. All chat in this room will be posted as comments in the main photo topic."; break; }
						case Model.Entities.ObjectType.Thread: { s = "This is a topic chat room. All chat in this room will be posted as comments in the topic."; break; }
						default: { s = ""; break; }
					}
					sb.Append(s);
					sb.Append("</p>");

					if ((this.ObjectBob is IPic && ((IPic)ObjectBob).HasPic) || ObjectType == Model.Entities.ObjectType.Photo)
					{
						sb.Append("<p>");
						if (this.ObjectBob is IPage)
						{
							sb.Append("<a");
							sb.AppendAttribute("href", ((IPage)ObjectBob).Url());
							sb.Append(">");
						}
						{
							sb.Append("<img");
							if (ObjectType == Model.Entities.ObjectType.Photo)
								sb.AppendAttribute("src", Storage.Path(((Photo)ObjectBob).Icon));
							else
								sb.AppendAttribute("src", Storage.Path(((IPic)ObjectBob).Pic));
							sb.AppendAttribute("class", "BorderBlack All");
							sb.AppendAttribute("width", "100");
							sb.AppendAttribute("height", "100");
							sb.Append(">");
						}
						if (this.ObjectBob is IPage)
						{
							sb.Append("</a>");
						}
						sb.Append("</p>");
					}
					if (ObjectBob is IPage && ObjectBob is IReadableReference)
					{
						sb.Append("<p>");
						{
							sb.Append("<a");
							sb.AppendAttribute("href", ((IPage)ObjectBob).Url());
							sb.Append(">");
							sb.Append(((IReadableReference)ObjectBob).ReadableReference);
							sb.Append("</a>");
						}
						sb.Append("</p>");
					}

				}
				else
				{
					//sb.Append("<p>No info available. This is " + RoomType.ToString().PrefixWithAOrAn(false) + " room.</p>");

					sb.Append("<p>");
					string s = "";
					switch (RoomType)
					{
						case RoomType.BuddyAlerts: { s = "This is the buddy alerts room. Alerts from your buddies get posted here."; break; }
						case RoomType.InboxUpdates: { s = "This is the inbox updates room. New comments in topics you are watching get posted here."; break; }
						case RoomType.Laughs: { s = "This is the laughs room. You'll get an alert here whenever anyone clicks the laugh button."; break; }
						case RoomType.NewPhotosAll: { s = "This is a photos room. As new photos and videos get uploaded to the site, they appear in here."; break; }
						case RoomType.NewPhotosBuddies: { s = "This is a photos room. As your buddies upload photos and videos, they appear in here."; break; }
						case RoomType.NewPhotosProSpotters: { s = "This is a photos room. As pro spotters upload photos and videos, they appear in here."; break; }
						case RoomType.NewPhotosSpotters: { s = "This is a photos room. As spotters upload photos and videos, they appear in here."; break; }
						case RoomType.NewVideosAll: { s = "This is a videos room. As new videos get uploaded to the site, they appear in here."; break; }
						case RoomType.Orphans: { s = "This is the orphans room. Messages that we can't find rooms for get posted here. It's just for debugging."; break; }
						case RoomType.PrivateChatRequestsBuddies: { s = "This is a private chat requests room. When one of your buddies tries to private chat with you, an alert is sent here."; break; }
						case RoomType.PrivateChatRequestsStrangers: { s = "This is a private chat requests room. When someone not on you buddy list are tries to private chat with you, an alert is sent here."; break; }
						case RoomType.PrivateChatRequests: { s = "This is a private chat requests room. When someone tries to private chat with you, an alert is sent here."; break; }
						case RoomType.RandomChat: { s = "This is the random chat room. All comments posted in public topics create an alert in here ."; break; }
						case RoomType.SystemMessages: { s = "This is the system messages room. It's just for testing."; break; }
						default: { s = ""; break; }
					}
					sb.Append(s);
					sb.Append("</p>");

				}


				//if (!IsReadOnly && !IsDefaultRoom)
				//{
				//    Query q = new Query();
				//    q.TableElement = new Join(Usr.Columns.K, RoomPin.Columns.UsrK);
				//    q.QueryCondition = new And(
				//        new Q(RoomPin.Columns.RoomGuid, this.Guid),
				//        new Q(RoomPin.Columns.Pinned, true));
				//    q.OrderBy = new OrderBy(Usr.Columns.DateTimeLastPageRequest, OrderBy.OrderDirection.Descending);
				//    q.Columns = Usr.LinkColumns;
				//    UsrSet us = new UsrSet(q);
				//    if (us.Count > 0)
				//    {
				//        sb.Append(string.Format("<p>{0} {1} {2} this room pinned:</p>", us.Count.ToString("#,##0"), us.Count == 1 ? "person" : "people", us.Count == 1 ? "has" : "have"));
				//        sb.Append("<p class=\"CleanLinks\">");

				//        int count = 0;
				//        foreach (Usr u in us)
				//        {
				//            string shading = u.LoggedInNow ? "selected-onyellow" : "shaded40-onyellow";
				//            sb.Append(count == 0 ? "" : "<br />");
				//            sb.Append(u.LoggedInNow ? "" : "<small>");
				//            u.LinkAppend(sb, false);
				//            sb.Append(u.LoggedInNow ? "" : "</small>");

				//            u.PresenceIconAppend(sb, shading);

				//            if (Usr.Current != null && u.K != Usr.Current.K)
				//            {
				//                RoomSpec c = new RoomSpec(u.K, Usr.Current.K);
				//                c.PinHtmlAppend(sb, shading);
				//            }
				//            count++;

				//            if (count > 100)
				//            {
				//                break;
				//            }
				//        }
				//        sb.Append("</p>");
				//        if (count > 100)
				//            sb.Append("<p><small>(only showing 100 people)</small></p>");
				//    }
				//}
				//else if (!IsReadOnly && IsDefaultRoom)
				//{
				//    sb.Append("<p>This is a default room - everybody has it pinned.</p>");
				//}


				return sb.ToString();
			}

			public void PinHtmlAppend(StringBuilder sb)
			{
				PinHtmlAppend(sb, "selected-onyellow");
			}
			public void PinHtmlAppend(StringBuilder sb, string presenceIconShade)
			{
				string roomGuidString = this.Guid.Pack();
				sb.Append("<a href=\"#\" onclick=\"chatClientPinRoom('");
				sb.Append(roomGuidString);
				sb.Append("', this, true);");
				//appendTransferJs(sb);
				sb.Append("return false;\"><img id=\"RoomsPage");
				sb.Append(roomGuidString);
				sb.Append(String.Format("\" src=\"/gfx/chatclient-pinup-{0}.gif\" onmouseover=\"sttd({1})\" onmouseout=\"htm();\" style=\"margin-left:2px;\" border=\"0\" width=\"9\" height=\"8\" /></a>", presenceIconShade, this.RoomType == RoomType.PrivateChat ? "8" : "9"));
			}
			public void LinkHtmlAppend(StringBuilder sb)
			{
				LinkHtmlAppend(sb, "selected-onyellow");
			}
			public void LinkHtmlAppend(StringBuilder sb, string presenceIconShade)
			{
				LinkHtmlAppend(sb, presenceIconShade, "", "", "");
			}
			public void LinkHtmlAppend(StringBuilder sb, string presenceIconShade, string extraHtmlAttributes, string extraStyleAttribute, string extraClassAttribute)
			{
				string roomGuidString = LinkHtmlAppendJustStartOfAnchorTag(sb, presenceIconShade, extraHtmlAttributes, extraStyleAttribute, extraClassAttribute);
				sb.Append(this.GetName(Usr.Current));
				sb.Append("<img id=\"RoomsPage");
				sb.Append(roomGuidString);
				sb.Append(String.Format("\" src=\"/gfx/chatclient-pinup-{0}.gif\" onmouseover=\"sttd({1})\" onmouseout=\"htm();\" style=\"margin-left:2px;\" border=\"0\" width=\"9\" height=\"8\" /></a>", presenceIconShade, this.RoomType == RoomType.PrivateChat ? "8" : "9"));
			}
			public string LinkHtmlAppendJustStartOfAnchorTag(StringBuilder sb, string presenceIconShade, string extraHtmlAttributes, string extraStyleAttribute, string extraClassAttribute)
			{
				string roomGuidString = this.Guid.Pack();
				sb.Append("<a href=\"#\" onclick=\"chatClientPinRoom('");
				sb.Append(roomGuidString);
				sb.Append("', this, true);return false;\"");
				sb.Append(extraHtmlAttributes);
				sb.Append(extraStyleAttribute);
				sb.Append(extraClassAttribute);
				sb.Append(">");
				return roomGuidString;
			}
			public string PinTag(Guid roomGuid)
			{
				return string.Format("<dsi:object type=\"room\" ref=\"{0}\" />", roomGuid.Pack());
			}
			#endregion

			#region IsReadOnly
			public bool IsReadOnly
			{
				get
				{
					return RoomType != RoomType.Normal && RoomType != RoomType.PrivateChat;
				}
			}
			#endregion

			#region IsStarredByDefault
			public bool IsStarredByDefault
			{
				get
				{
					return RoomType == RoomType.PrivateChatRequests || RoomType == RoomType.PrivateChatRequestsBuddies || RoomType == RoomType.PrivateChatRequestsStrangers || RoomType == RoomType.PrivateChat || RoomType == RoomType.InboxUpdates;
				}
			}
			#endregion

			#region IsPinnable
			public bool IsPinnable
			{
				get
				{
					if (RoomType == RoomType.Normal && ObjectType == Model.Entities.ObjectType.None)
						return false;
					else if (RoomType == RoomType.PublicStream || RoomType == RoomType.InboxUpdates || RoomType == RoomType.PrivateChatRequests || RoomType == RoomType.PrivateChatRequestsBuddies || RoomType == RoomType.PrivateChatRequestsStrangers)
						return false;
					else
						return true;
				}
			}
			#endregion

			#region IsStarrable
			public bool IsStarrable
			{
				get
				{
					if (RoomType == RoomType.Normal && ObjectType == Model.Entities.ObjectType.None)
						return false;
					else if (RoomType == RoomType.RandomChat || RoomType == RoomType.PrivateChatRequestsBuddies || RoomType == RoomType.PrivateChatRequests)
						return false;
					else
						return true;
				}
			}
			#endregion

			#region IsPhotoChatRoom
			public bool IsPhotoChatRoom
			{
				get
				{
					return RoomType == RoomType.Normal && ObjectType == Model.Entities.ObjectType.Photo;
				}
			}
			#endregion

			#region IsNewPhotoAlertsRoom
			public bool IsNewPhotoAlertsRoom
			{
				get
				{
					return RoomType == RoomType.NewPhotosAll ||
						RoomType == RoomType.NewPhotosBuddies ||
						RoomType == RoomType.NewPhotosProSpotters ||
						RoomType == RoomType.NewPhotosSpotters ||
						RoomType == RoomType.NewVideosAll;
				}
			}
			#endregion

			#region IsPrivateChatRoom
			public bool IsPrivateChatRoom
			{
				get
				{
					return RoomType == RoomType.PrivateChat;
				}
			}
			#endregion

			#region GetPresence
			public PresenceState GetPrivateChatRoomPresence(Usr currentUsr)
			{
				if (RoomType == RoomType.PrivateChat)
				{
					Usr u = ObjectK == currentUsr.K ? (Usr)SecondObjectBob : (Usr)ObjectBob;
					if (u.ChattingNow)
						return PresenceState.Chatting;
					else if (u.LoggedInNow)
						return PresenceState.Online;
					else
						return PresenceState.Offline;
				}
				else
					return PresenceState.None;
			}
			#endregion

			#region IsDefaultRoom
			/// <summary>
			/// Is this room a default room? Default rooms are emitted by default, so if they're unpinned, we need to store a RoomPin with pinned=false in the database.
			/// </summary>
			public bool IsDefaultRoom
			{
				get
				{
					return (RoomType == RoomType.Normal && ObjectType == Model.Entities.ObjectType.None) || 
						(RoomType != RoomType.Normal && RoomType != RoomType.PrivateChat) || 
						RoomType == RoomType.Laughs ||
						RoomType == RoomType.PublicStream;
				}
			}
			#endregion

			#region IsPersistantAlertsRoom
			/// <summary>
			/// These rooms should never be exited, even if we un-pin the room.
			/// </summary>
			public bool IsPersistantAlertsRoom
			{
				get
				{
					return RoomType == RoomType.PrivateChat || RoomType == RoomType.BuddyAlerts || RoomType == RoomType.PublicStream;
				}
			}
			#endregion

			#region Icon
			public string Icon
			{
				get
				{
					if (IsPhotoChatRoom)
						return ((Photo)ObjectBob).Icon.ToString();
					else
						return "";
				}
			}
			#endregion

			#region ObjectBob
			public IBob ObjectBob
			{
				get
				{
					if (!triedGetObject && objectBob == null)
					{
						triedGetObject = true;
						try
						{
							objectBob = Bob.Get(this.ObjectType, this.ObjectK);
						}
						catch { }
					}
					return objectBob;
				}
			}
			IBob objectBob;
			bool triedGetObject = false;
			#endregion

			#region SecondObjectBob
			public IBob SecondObjectBob
			{
				get
				{
					if (!triedGetSecondObject && secondObjectBob == null)
					{
						triedGetSecondObject = true;
						try
						{
							secondObjectBob = Bob.Get(this.SecondObjectType, this.SecondObjectK);
						}
						catch { }
					}
					return secondObjectBob;
				}
			}
			IBob secondObjectBob;
			bool triedGetSecondObject = false;
			#endregion

			#region GetName()
			public string GetName()
			{
				return GetName(null);
			}
			public string GetName(Usr currentUsr)
			{
				try
				{
					if (RoomType == RoomType.PrivateChat)
					{
						if (SecondObjectType == Model.Entities.ObjectType.None && SecondObjectK == 0)
							return ((Usr)ObjectBob).NickName;
						else if (currentUsr != null && currentUsr.K == ObjectK)
							return ((Usr)SecondObjectBob).NickName;
						else if (currentUsr != null && currentUsr.K == SecondObjectK)
							return ((Usr)ObjectBob).NickName;
						else
							return "Private chat - " + ((Usr)ObjectBob).NickName + ", " + ((Usr)SecondObjectBob).NickName;
					}
					else if (RoomType == RoomType.InboxUpdates)
						return "Inbox updates";
					else if (RoomType == RoomType.Laughs)
						return "Laughs";
					else if (RoomType == RoomType.NewPhotosAll)
						return "New photos (all)";
					else if (RoomType == RoomType.NewPhotosBuddies)
						return "New photos (buddies)";
					else if (RoomType == RoomType.NewPhotosProSpotters)
						return "New photos (pro spotters)";
					else if (RoomType == RoomType.NewPhotosSpotters)
						return "New photos (spotters)";
					else if (RoomType == RoomType.NewVideosAll)
						return "New videos";
					else if (RoomType == RoomType.PrivateChatRequestsBuddies)
						return "Private chat (buddies)";
					else if (RoomType == RoomType.PrivateChatRequestsStrangers)
						return "Private chat (strangers)";
					else if (RoomType == RoomType.PrivateChatRequests)
						return "Private chat requests";
					else if (RoomType == RoomType.RandomChat)
						return "Random comments";
					else if (RoomType == RoomType.BuddyAlerts)
						return "Buddy alerts";
					else if (RoomType == RoomType.SystemMessages)
						return "System messages";
					else if (RoomType == RoomType.PublicStream)
						return "Stream";
					else if (RoomType == RoomType.Normal && ObjectK == 0 && ObjectType == Model.Entities.ObjectType.None)
						return "General chat";
					else if (RoomType == RoomType.Normal && ObjectType == Model.Entities.ObjectType.Photo)
					{
						Photo p = (Photo)ObjectBob;
						try
						{
							if (p.ThreadK > 0 && p.Thread != null)
								return p.Thread.Subject;
							else
								return "Photo chat";
						}
						catch
						{
							return "Photo chat";
						}
					}
					else
					{
						if (ObjectBob != null && ObjectBob is IName)
							return ((IName)ObjectBob).FriendlyName;
						else if (ObjectBob != null && ObjectBob is IReadableReference)
							return ((IReadableReference)ObjectBob).ReadableReference;
						else
							return RoomType.ToString();
					}
				}
				catch 
				{
					return RoomType.ToString();
				}
			}
			#endregion

			#region GetUrl()
			public string GetUrl()
			{
				return GetName(null);
			}
			public string GetUrl(Usr currentUsr)
			{
				if (RoomType == RoomType.PrivateChat)
				{
					if (currentUsr != null && currentUsr.K == ObjectK)
						return ((Usr)SecondObjectBob).Url();
					else if (currentUsr != null && currentUsr.K == SecondObjectK)
						return ((Usr)ObjectBob).Url();
					else
						return ((Usr)ObjectBob).Url();
				}
				else if (RoomType == RoomType.Normal && ObjectBob != null && ObjectBob is IPage)
				{
					if (ObjectBob is Thread)
					{
						Thread t = (Thread)ObjectBob;
						if (t.LastPage > 1)
							return t.Url("c", t.LastPage.ToString());
						else
							return t.Url();
					}
					else
						return ((IPage)ObjectBob).Url();
				}
				else
				{
					return "";
				}
			}
			#endregion

			#region HasArchive
			public bool HasArchive
			{
				get
				{
					return RoomType == RoomType.Normal || RoomType == RoomType.PrivateChat;
				}
			}
			#endregion

			#region HiddenFromRoomList
			public bool HiddenFromRoomList
			{
				get
				{
					//return false;
					return RoomType == RoomType.PublicStream;
				}
			}
			#endregion

			#region IsStreamRoom
			public bool IsStreamRoom
			{
				get
				{
					return RoomType == RoomType.PublicStream;
				}
			}
			#endregion

			#region GetArchiveCacheKey
			public static Caching.CacheKey GetArchiveCacheKey(Guid roomGuid)
			{
				return new Caching.CacheKey(Caching.CacheKeyPrefix.ChatClientRoomArchive, "Version", "1", roomGuid.ToString());
			}
			#endregion

			#region GetArchiveItems()
			public string GetArchiveItems(int numberOfItems)
			{
				//Get from cache...
				string s = (string)Caching.Instances.Main.Get(GetArchiveCacheKey(this.Guid));
				if (s != null && !Vars.DevEnv)
					return s;

				string roomGuidPacked = this.Guid.Pack();
				System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

				List<ItemStub> items = new List<ItemStub>();
				//Generate...
				if (IsThreadRoom || IsHasPrimaryThreadObject)
				{
					if (ThreadK > 0)
					{
						Query q = new Query();
						q.QueryCondition = new Q(Comment.Columns.ThreadK, ThreadK);
						q.OrderBy = new OrderBy(Comment.Columns.K, OrderBy.OrderDirection.Descending);
						q.TopRecords = numberOfItems;

						CommentSet cs = new CommentSet(q);

						for (int i = 0; i < cs.Count; i++)
						{
							Comment c = cs[i];

							if (!c.ChatItemGuid.HasValue)
							{
								c.ChatItemGuid = Guid.NewGuid();
								c.Update();
							}

							CommentMessageStub cms = c.GetCommentMessageStub();
							items.Add(cms);
						}
					}
				}
				else
				{
					//get from ChatMessage

					bool isBuddyIfRoomIsPrivate = IsPrivateChatRoom && ((Usr)ObjectBob).HasFullBuddy(SecondObjectK);

					Query q = new Query();
					q.QueryCondition = new Q(ChatMessage.Columns.RoomGuid, this.Guid);
					q.OrderBy = new OrderBy(ChatMessage.Columns.K, OrderBy.OrderDirection.Descending);
					q.TopRecords = numberOfItems;

					ChatMessageSet cs = new ChatMessageSet(q);

					for (int i = 0; i < cs.Count; i++)
					{
						ChatMessage cm = cs[i];

						if (cm.Deleted.HasValue && cm.Deleted.Value)
							break;

						if (!cm.ChatItemGuid.HasValue)
						{
							cm.ChatItemGuid = Guid.NewGuid();
							cm.Update();
						}

						if (IsPrivateChatRoom)
						{
							PrivateStub p = new PrivateStub(
								cm.ChatItemGuid.Value.Pack(),
								ItemType.PrivateChatMessage,
								cm.DateTime.Ticks.ToString(),
								roomGuidPacked,
								cm.FromUsr.NickName,
								cm.FromUsr.StmuParams,
								cm.FromUsr.K,
								cm.FromUsr.HasPicNotFacebook ? cm.FromUsr.Pic.ToString() : "0",
								cm.FromUsr.HasChatPic ? cm.FromUsr.ChatPic.Value.ToString() : "0",
								cm.Text,
								"",
								isBuddyIfRoomIsPrivate);
							items.Add(p);
						}
						else
						{
							MessageStub m = new MessageStub(
								cm.ChatItemGuid.Value.Pack(),
								ItemType.PrivateChatMessage,
								cm.DateTime.Ticks.ToString(),
								roomGuidPacked,
								cm.FromUsr.NickName,
								cm.FromUsr.StmuParams,
								cm.FromUsr.K,
								cm.FromUsr.HasPicNotFacebook ? cm.FromUsr.Pic.ToString() : "0",
								cm.FromUsr.HasChatPic ? cm.FromUsr.ChatPic.Value.ToString() : "0",
								cm.Text,
								"");
							items.Add(m);
						}
					}
				}

				string itemsString = "";

				if (items.Count > 0)
				{
					StringBuilder sb = new StringBuilder();
					foreach (ItemStub item in items)
					{
						if (sb.Length > 0)
							sb.Append(",");

						sb.Append(serializer.Serialize(item));
					}
					itemsString = sb.ToString();
				}

				Caching.Instances.Main.Set(GetArchiveCacheKey(this.Guid), itemsString);

				return itemsString;

			}
			#endregion

			#region DeleteArchive()
			public void DeleteArchive()
			{
				//delete from chat server
				ChatLibrary.ChatServerInterface cs = (ChatLibrary.ChatServerInterface)Activator.GetObject(typeof(ChatLibrary.ChatServerInterface), Bobs.Vars.ChatServerAddress);
				cs.ClearRoom(this.Guid);

				//reset archive cache
				Caching.Instances.Main.Delete(Chat.RoomSpec.GetArchiveCacheKey(this.Guid));

				//start thread to delete from database?
				DeleteArchiveJob job = new DeleteArchiveJob(this.Guid);
				job.ExecuteAsynchronously();
				

			}
			#endregion


			#region IsThreadRoom
			public bool IsThreadRoom
			{
				get
				{
					return RoomType == RoomType.Normal && ObjectType == Model.Entities.ObjectType.Thread;
				}
			}
			#endregion
			#region IsHasPrimaryThreadObject
			public bool IsHasPrimaryThreadObject
			{
				get
				{
					return RoomType == RoomType.Normal && ObjectBob != null && ObjectBob is IHasPrimaryThread;
				}
			}
			#endregion
			#region ThreadK
			public int ThreadK
			{
				get
				{
					if (IsThreadRoom)
						return ObjectK;

					if (IsHasPrimaryThreadObject)
						return ((IHasPrimaryThread)ObjectBob).ThreadK ?? 0;

					return 0;
				}
			}
			#endregion


			#region DeleteArchiveJob
			public class DeleteArchiveJob : Job
			{

				JobDataMapItemProperty<Guid> RoomGuid { get { return new JobDataMapItemProperty<Guid>("RoomGuid", JobDataMap); } }

				public DeleteArchiveJob() // this is required by Quatz.net
				{
				}
				internal DeleteArchiveJob(Guid roomGuid)
					: this()
				{
					RoomGuid.Value = roomGuid;
				}
				protected override void Execute()
				{
					Bobs.Update u = new Update();
					u.Changes.Add(new Assign(ChatMessage.Columns.Deleted, true));
					u.Table = TablesEnum.ChatMessage;
					u.Where = new And(
						new Q(ChatMessage.Columns.RoomGuid, RoomGuid.Value),
						new Or(
							new Q(ChatMessage.Columns.Deleted, QueryOperator.IsNull, null),
							new Q(ChatMessage.Columns.Deleted, false)
						)
					);
					u.CommandTimeout = 3600;
					u.Run();
				}
			}
			#endregion

		}

		#region GetToken
		public static string GetToken(Guid guid, string dateTimeString)
		{
			byte[] bytesToHash = UnicodeEncoding.UTF8.GetBytes("aff89a1e-b1fb-4b6e-ab72-bad200843caa|" + guid + "|" + dateTimeString);
			MD5CryptoServiceProvider myMD5 = new MD5CryptoServiceProvider();
			byte[] hashBytes = myMD5.ComputeHash(bytesToHash);
			return new Guid(hashBytes).Pack();
		}
		#endregion
		#region Authenticate
		public static bool Authenticate(string token, string dateTimeString, Guid guid)
		{
			try
			{
				if (new DateTime(long.Parse(dateTimeString)).AddHours(2) < DateTime.Now)
					return false;

				return GetToken(guid, dateTimeString).Equals(token);
			}
			catch
			{
				return false;
			}
		}
		#endregion

		#region SendChatItemToBuddies
		/// <summary>
		/// Sends an item to the buddy stream of all the users logged in buddies
		/// </summary>
		public static void SendChatItemToBuddies(ItemStub item, int usrK)
		{
			SendChatItemToBuddiesJob job = new SendChatItemToBuddiesJob(item, usrK);
			job.ExecuteAsynchronously();
		}

		#region SendChatItemToBuddiesJob
		public class SendChatItemToBuddiesJob : Job
		{

			JobDataMapItemProperty<ItemStub> Item { get { return new JobDataMapItemProperty<ItemStub>("Item", JobDataMap); } }
			JobDataMapItemProperty<int> UsrK { get { return new JobDataMapItemProperty<int>("UsrK", JobDataMap); } }

			public SendChatItemToBuddiesJob() // this is required by Quatz.net
			{
			}
			internal SendChatItemToBuddiesJob(ItemStub item, int usrK)
				: this()
			{
				Item.Value = item;
				UsrK.Value = usrK;

			}
			protected override void Execute()
			{
				Query q = new Query();
				q.TableElement = Usr.BuddyJoin;
				q.QueryCondition = new And(Usr.GetBuddiesFullQ(UsrK.Value), Usr.LoggedInChatQ);
				q.Columns = new ColumnSet(Usr.Columns.K);
				UsrSet loggedOnBuddies = new UsrSet(q);

				System.Web.Script.Serialization.JavaScriptSerializer d = new System.Web.Script.Serialization.JavaScriptSerializer();
				ChatLibrary.ChatServerInterface cs = (ChatLibrary.ChatServerInterface)Activator.GetObject(typeof(ChatLibrary.ChatServerInterface), Bobs.Vars.ChatServerAddress);

				foreach (Usr buddy in loggedOnBuddies)
				{

					RoomSpec roomSpec = new RoomSpec(RoomType.BuddyStream, Model.Entities.ObjectType.Usr, buddy.K);
					Guid roomGuid = roomSpec.Guid;
					Guid itemGuid = Guid.NewGuid();

					Item.Value.guid = itemGuid.Pack();
					Item.Value.roomGuid = roomGuid.Pack();


					cs.SendTo(roomGuid, d.Serialize(Item.Value), new int[] { buddy.K }, itemGuid);
				}
			}
		}
		#endregion
		#endregion

		#region SendJsonChatItem
		public static void SendJsonChatItem(ItemStub item)
		{
			SendJsonChatItem(item, new int[] { });
		}
		public static void SendJsonChatItem(ItemStub item, int addToRoomUsrK)
		{
			SendJsonChatItem(item, new int[] { addToRoomUsrK });
		}
		public static void SendJsonChatItem(ItemStub item, UsrSet us)
		{
			List<int> usrKs = new List<int>();
			foreach (Usr u in us)
			{
				try
				{
					usrKs.Add(u.K);
				}
				catch { }
			}
			SendJsonChatItem(item, usrKs.ToArray());
		}
		public static void SendJsonChatItem(ItemStub item, int[] addToRoomUsrKs)
		{
			try
			{
				Guid roomGuid = item.roomGuid.UnPackGuid();
				Chat.RoomSpec rs = Chat.RoomSpec.FromGuid(roomGuid);
				if (rs != null && rs.HasArchive)
					Caching.Instances.Main.Delete(Chat.RoomSpec.GetArchiveCacheKey(roomGuid));


				System.Web.Script.Serialization.JavaScriptSerializer d = new System.Web.Script.Serialization.JavaScriptSerializer();
				ChatLibrary.ChatServerInterface cs = (ChatLibrary.ChatServerInterface)Activator.GetObject(typeof(ChatLibrary.ChatServerInterface), Bobs.Vars.ChatServerAddress);
				cs.SendTo(roomGuid, d.Serialize(item), addToRoomUsrKs, item.guid.UnPackGuid());
			}
			catch (Exception ex)
			{

			}
		}
		#endregion

		#region JoinRoom
		public static void JoinRoom(Guid RoomGuid, UsrSet us)
		{
			List<int> usrKs = new List<int>();
			foreach (Usr u in us)
			{
				try
				{
					usrKs.Add(u.K);
				}
				catch { }
			}
			JoinRoom(RoomGuid, usrKs.ToArray());
		}

		public static void JoinRoom(Guid[] RoomGuids, int UsrK)
		{
			JoinRoom(RoomGuids, new int[] { UsrK });
		}
		public static void JoinRoom(Guid RoomGuid, int[] UsrKs)
		{
			JoinRoom(new Guid[] { RoomGuid }, UsrKs);
		}

		public static void JoinRoom(Guid RoomGuid, int UsrK)
		{
			JoinRoom(new Guid[] { RoomGuid }, new int[] { UsrK });
		}

		public static void JoinRoom(Guid[] RoomGuids, int[] UsrKs)
		{
			try
			{
				ChatLibrary.ChatServerInterface cs = (ChatLibrary.ChatServerInterface)Activator.GetObject(typeof(ChatLibrary.ChatServerInterface), Bobs.Vars.ChatServerAddress);
				cs.JoinRoom(RoomGuids, UsrKs);
			}
			catch { }
		}
		#endregion

		#region ExitRoom
		public static void ExitRoom(Guid RoomGuid, int UsrK)
		{
			try
			{
				ChatLibrary.ChatServerInterface cs = (ChatLibrary.ChatServerInterface)Activator.GetObject(typeof(ChatLibrary.ChatServerInterface), Bobs.Vars.ChatServerAddress);
				cs.ExitRoom(RoomGuid, UsrK);
			}
			catch { }
		}
		#endregion

		#region NewResetSessionId
		public static void NewResetSessionId(int UsrK, int SessionId)
		{
			try
			{
				ChatLibrary.ChatServerInterface cs = (ChatLibrary.ChatServerInterface)Activator.GetObject(typeof(ChatLibrary.ChatServerInterface), Bobs.Vars.ChatServerAddress);
				cs.ResetSessionID(UsrK, SessionId);
			}
			catch { }
		}
		#endregion

		#region GetChat (old)
		public static Chat GetChat(int UsrK)
		{
			Chat c = null;
			try
			{
				c = new Chat(UsrK);
			}
			catch
			{
				c = new Chat();
				c.UsrK = UsrK;
			}
			return c;
		}
		#endregion


	}
	#endregion

}
