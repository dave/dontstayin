using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using SpottedScript.Controls.ChatClient.Shared;
using System.Web.Script.Services;
using System.Text;
using Bobs;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;
using System.Collections.Generic;
using Bobs.JobProcessor;

namespace Spotted.WebServices.Controls.ChatClient
{
	/// <summary>
	/// Summary description for ChatClient
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[ToolboxItem(false)]
	[ScriptService]
	public class ChatClient : System.Web.Services.WebService
	{
		public void WaitIfDevEnv()
		{
			//if (Vars.DevEnv)
			//	System.Threading.Thread.Sleep(1500);
		}

		#region storeRoomState
		void storeRoomState(StateStub[] roomState, int usrK)
		{
			Guid dsiGuid = usrK > 0 ? Guid.Empty : Chat.GetDsiGuidWithoutTouchingDatabase();

			Chat.StoreStateInCache(roomState, usrK, dsiGuid);
		}
		#endregion
		
		#region Send
		[WebMethod]
		[ScriptMethod]
		public RefreshStub Send(string message, string roomGuidString, string lastItemGuidString, int sessionID, string pageUrl, StateStub[] roomState)
		{
			WaitIfDevEnv();

			if (Usr.Current == null || Usr.Current.IsSkeleton || Usr.Current.Banned)
				throw new LoginPermissionException();

			Guid guid = roomGuidString.UnPackGuid();
			Chat.RoomSpec spec = Chat.RoomSpec.FromGuid(guid);
			Guid chatItemGuid = Guid.NewGuid();

			if (spec == null)
				throw new InvalidRoomException();

			if (!spec.CheckPermission(Usr.Current, true))
				throw new WritePermissionException();

			storeRoomState(roomState, Usr.Current.K);

			string txt = Chat.GetMessageFromChatBox(message);

			#region Create a ChatMessage database entry
			if (!spec.IsThreadRoom)
			{
				ChatMessage c = new ChatMessage();
				c.ChatItemGuid = chatItemGuid;
				c.DateTime = DateTime.Now;
				c.UsrK = Usr.Current.K;
				c.Text = txt;
				c.RoomGuid = spec.Guid;
				c.Update();
			}
			#endregion

			Usr.Current.ChatMessageCount++;
			Usr.Current.DateTimeLastChatMessage = DateTime.Now;
			string lastActionTicks = resetLastActionAndSessionID(sessionID); //will also fire Usr.Current.Update()

			MessageStub m;
			if (spec.IsPrivateChatRoom)
			{
				#region Private chat message
				int recipientUsrK = Usr.Current.K == spec.ObjectK ? spec.SecondObjectK : spec.ObjectK;
				bool isBuddy;
				try
				{
					Buddy b = new Buddy(recipientUsrK, Usr.Current.K);
					isBuddy = true;
				}
				catch
				{
					isBuddy = false;
				}
				
				m = new PrivateStub(
					chatItemGuid.Pack(),
					ItemType.PrivateChatMessage,
					lastActionTicks,
					roomGuidString.UnPackGuid().Pack(),
					Usr.Current.NickName,
					Usr.Current.StmuParams,
					Usr.Current.K,
					Usr.Current.HasPic ? Usr.Current.PicOrFacebookUID : "0",
					Usr.Current.HasChatPic ? Usr.Current.ChatPic.Value.ToString() : "0",
					txt,
					"",
					isBuddy);

				Chat.SendJsonChatItem(m, new int[]{Usr.Current.K, recipientUsrK});
				#endregion
			}
			else if (spec.IsThreadRoom || spec.IsHasPrimaryThreadObject)
			{
				#region Thread rooms
				Thread t = null;
				bool alreadyCreatedComment = false;
				bool createNewThread = false;


				if (spec.IsHasPrimaryThreadObject)
				{
					//if the object doesn't have a primary thread...
					IHasPrimaryThread primaryThreadObject = (IHasPrimaryThread)spec.ObjectBob;

					if (primaryThreadObject.ThreadK.IsNullOrZero())
					{
						createNewThread = true;
					}
					else
					{
						try
						{
							t = new Thread(primaryThreadObject.ThreadK.Value);
						}
						catch (BobNotFound)
						{
							createNewThread = true;
						}
					}

					if (createNewThread)
					{
						Thread.MakerReturn threadMakerReturn = Thread.CreatePublicThread((IDiscussable)spec.ObjectBob, Guid.NewGuid(), txt, false, null, true);

						alreadyCreatedComment = true;

						if (threadMakerReturn.Success || threadMakerReturn.Duplicate)
							t = threadMakerReturn.Thread;
						else
							throw new Exception("Couldn't create new thread!");
					}
				}
				else
				{
					t = new Thread(spec.ObjectK);
				}

				string subject = t.Subject.TruncateWithDots(30);
				int threadK = t.K;
				bool isPublic = !t.Private && !t.PrivateGroup && !t.GroupPrivate;
				string url = "";
				if (createNewThread)
				{
					url = t.UrlRefresher() + "#Comments";
				}
				else
				{
					if (t.LastPage == 1)
						url = t.UrlRefresher() + "#Comments";
					else
						url = t.UrlRefresher("c", t.LastPage.ToString()) + "#Comments";

					try
					{
						if (t.TotalComments % 20 == 0)
							url = t.UrlRefresher("c", (t.LastPage + 1).ToString()) + "#Comments";
						else if (t.LastComment != null)
							url = t.LastComment.UrlRefresherAnchorAfter();
					}
					catch { }
				}

				m = new CommentMessageStub(
					chatItemGuid.Pack(),
					ItemType.CommentChatMessage,
					lastActionTicks,
					roomGuidString.UnPackGuid().Pack(),
					Usr.Current.NickName,
					Usr.Current.StmuParams,
					Usr.Current.K,
					Usr.Current.HasPic ? Usr.Current.PicOrFacebookUID : "0",
					Usr.Current.HasChatPic ? Usr.Current.ChatPic.Value.ToString() : "0",
					txt,
					"",
					url,
					subject);

				List<int> usrKs = Thread.GetAllLoggedInParticipants(t).ToList().ConvertAll(u => u.K);
				try
				{
					usrKs.Add(Usr.Current.K);
				}
				catch { }
				
				Chat.SendJsonChatItem(m, usrKs.ToArray());

				if (isPublic && threadK > 0)
				{
					m.guid = Guid.NewGuid().Pack();
					m.roomGuid = new Chat.RoomSpec(RoomType.RandomChat).Guid.Pack();
					m.pinRoomGuid = t.GetRoomSpec().Guid.Pack();
					Chat.SendJsonChatItem(m);
				}

				if (!alreadyCreatedComment)
				{
					AddCommentJob job = new AddCommentJob(t.K, txt, Usr.Current.K, chatItemGuid);
					job.ExecuteAsynchronously();
				}
				#endregion
			}
			else
			{
				#region Public chat rooms
				m = new MessageStub(
					chatItemGuid.Pack(),
					ItemType.PublicChatMessage,
					lastActionTicks,
					roomGuidString.UnPackGuid().Pack(),
					Usr.Current.NickName,
					Usr.Current.StmuParams,
					Usr.Current.K,
					Usr.Current.HasPic ? Usr.Current.PicOrFacebookUID : "0",
					Usr.Current.HasChatPic ? Usr.Current.ChatPic.ToString() : "0",
					txt,
					"");

				Chat.SendJsonChatItem(m, Usr.Current.K);
				#endregion
			}

			SendStub s = new SendStub();
			s.itemGuid = chatItemGuid.Pack();

			RefreshStub r = refreshPrivate(false, lastItemGuidString, sessionID, lastActionTicks, pageUrl, Usr.Current.K, roomState);
			s.guestRefreshStubs = r.guestRefreshStubs;
			s.itemsJson = r.itemsJson;
			s.lastActionTicks = r.lastActionTicks;
			s.lastItemGuidReturned = r.lastItemGuidReturned;
			return s;
		}
		#region AddCommentJob
		public class AddCommentJob : Job
		{
			JobDataMapItemProperty<int> ThreadK { get { return new JobDataMapItemProperty<int>("ThreadK", JobDataMap); } }
			JobDataMapItemProperty<string> Body { get { return new JobDataMapItemProperty<string>("Body", JobDataMap); } }
			JobDataMapItemProperty<int> PostingUsrK { get { return new JobDataMapItemProperty<int>("PostingUsrK", JobDataMap); } }
			JobDataMapItemProperty<Guid> ChatItemGuid { get { return new JobDataMapItemProperty<Guid>("ChatItemGuid", JobDataMap); } }

			public AddCommentJob()
			{ }

			public AddCommentJob(int threadK, string body, int postingUsrK, Guid chatItemGuid)
			{
				ThreadK.Value = threadK;
				Body.Value = body;
				PostingUsrK.Value = postingUsrK;
				ChatItemGuid.Value = chatItemGuid;
			}

			protected override void Execute()
			{
				Thread t = new Thread(ThreadK);
				Usr u = new Usr(PostingUsrK);
				Comment.Maker commentMaker = new Comment.Maker();
				commentMaker.ChatItemGuid = ChatItemGuid;
				commentMaker.Body = Body;
				commentMaker.ParentThread = t;
				commentMaker.NewThread = false;
				commentMaker.DuplicateGuid = Guid.NewGuid();
				commentMaker.PostingUsr = u;
				commentMaker.CurrentThreadUsr = t.GetThreadUsr(u);
				if (t.GroupK > 0)
					commentMaker.CurrentGroupUsr = t.Group.GetGroupUsr(u);
				commentMaker.RunAsync = false;
				commentMaker.DisableLiveChatMessage = true;
				commentMaker.Post(null);
			}
		}
		#endregion
		#endregion

		#region ResetSessionAndGetState
		[WebMethod]
		[ScriptMethod]
		public GetStateStub ResetSessionAndGetState(bool isFirstRequest, string lastItemGuidString, int sessionID, string lastActionTicks, string pageUrl, StateStub[] roomState)
		{
			WaitIfDevEnv();

			int usrK = Chat.GetUsrKWithoutTouchingDatabase();
			Guid dsiGuid = usrK == 0 ? Guid.Empty : Chat.GetDsiGuidWithoutTouchingDatabase();


			RoomPinSet rps = null;

			string newLastActionTicks = "";
			if (usrK > 0 && Usr.Current != null)
			{
				newLastActionTicks = resetLastActionAndSessionID(sessionID);

				Query q = new Query();
				q.QueryCondition = new Q(RoomPin.Columns.UsrK, usrK);
				q.Columns = new ColumnSet(RoomPin.Columns.RoomGuid, RoomPin.Columns.StateStub);
				rps = new RoomPinSet(q);
			}
			else
				newLastActionTicks = DateTime.Now.Ticks.ToString();

			GetStateStub gs = new GetStateStub();
			gs.roomState = Chat.GetStateFromCacheOrDatabase(roomState.ConvertAll(s => s.guid.UnPackGuid()).ToArray<Guid>(), usrK, dsiGuid, rps);

			RefreshStub r = refreshPrivate(isFirstRequest, lastItemGuidString, sessionID, newLastActionTicks, pageUrl, usrK, null);
			gs.guestRefreshStubs = r.guestRefreshStubs;
			gs.itemsJson = r.itemsJson;
			gs.lastActionTicks = r.lastActionTicks;
			gs.lastItemGuidReturned = r.lastItemGuidReturned;
			return gs;
			
		}
		#endregion

		#region DeleteArchive
		[WebMethod]
		[ScriptMethod]
		public DeleteArchiveStub DeleteArchive(string roomGuid, string lastItemGuidString, int sessionID, string lastActionTicks, string pageUrl, StateStub[] roomState)
		{
			WaitIfDevEnv();

			Guid guid = roomGuid.UnPackGuid();
			Chat.RoomSpec spec = Chat.RoomSpec.FromGuid(guid);

			if (Usr.Current == null || Usr.Current.IsSkeleton || Usr.Current.Banned)
				throw new LoginPermissionException();

			if (spec == null)
				throw new InvalidRoomException();

			if (!spec.IsPrivateChatRoom)
				throw new InvalidRoomException();

			if (!spec.CheckPermission(Usr.Current, true))
				throw new WritePermissionException();

			storeRoomState(roomState, Usr.Current.K);

			lastActionTicks = resetLastActionAndSessionID(sessionID);

			//delete archive...
			spec.DeleteArchive();

			DeleteArchiveStub das = new DeleteArchiveStub();
			das.roomGuid = roomGuid;

			RefreshStub r = refreshPrivate(false, lastItemGuidString, sessionID, lastActionTicks, pageUrl, Usr.Current.K, roomState);
			das.guestRefreshStubs = r.guestRefreshStubs;
			das.itemsJson = r.itemsJson;
			das.lastActionTicks = r.lastActionTicks;
			das.lastItemGuidReturned = r.lastItemGuidReturned;
			return das;
		}
		#endregion

		#region GetArchive
		[WebMethod]
		[ScriptMethod]
		public ArchiveStub GetArchive(string roomGuid, string lastItemGuidString, int sessionID, string lastActionTicks, string pageUrl, StateStub[] roomState)
		{
			WaitIfDevEnv();

			Guid guid = roomGuid.UnPackGuid();
			Chat.RoomSpec spec = Chat.RoomSpec.FromGuid(guid);
			int usrK = Usr.Current == null ? 0 : Usr.Current.K;

			if (spec == null)
				throw new InvalidRoomException();

			if (!spec.CheckPermission(Usr.Current, false))
				throw new ReadPermissionException();

			storeRoomState(roomState, usrK);

			lastActionTicks = resetLastActionAndSessionID(sessionID);

			ArchiveStub ars = new ArchiveStub();
			ars.roomGuid = roomGuid;
			ars.archiveItems = spec.GetArchiveItems(20);

			RefreshStub r = refreshPrivate(false, lastItemGuidString, sessionID, lastActionTicks, pageUrl, usrK, roomState);
			ars.guestRefreshStubs = r.guestRefreshStubs;
			ars.itemsJson = r.itemsJson;
			ars.lastActionTicks = r.lastActionTicks;
			ars.lastItemGuidReturned = r.lastItemGuidReturned;
			return ars;
		}
		#endregion

		#region Refresh
		[WebMethod]
		[ScriptMethod]
		public RefreshStub Refresh(bool isFirstRequest, string lastItemGuidString, int sessionID, string lastActionTicks, string pageUrl, StateStub[] roomState)
		{
			WaitIfDevEnv();

			int usrK = Chat.GetUsrKWithoutTouchingDatabase();

			storeRoomState(roomState, usrK);

			return refreshPrivate(isFirstRequest, lastItemGuidString, sessionID, lastActionTicks, pageUrl, usrK, roomState);
		}
		#region refreshPrivate
		RefreshStub refreshPrivate(bool isFirstRequest, string lastItemGuidString, int sessionID, string lastActionTicks, string pageUrl, int usrK, StateStub[] roomState)
		{
			Guid lastItemGuidReturned = Guid.Empty;
			Guid lastItemGuid = lastItemGuidString.Length == 0 ? Guid.Empty : lastItemGuidString.UnPackGuid();
			if (usrK == 0)
				sessionID = 0;

			ChatLibrary.ChatServerInterface cs = (ChatLibrary.ChatServerInterface)Activator.GetObject(typeof(ChatLibrary.ChatServerInterface), Bobs.Vars.ChatServerAddress);
			string chatItems = cs.GetLatest(usrK, sessionID, isFirstRequest, lastItemGuid, ref lastItemGuidReturned);

			if (chatItems == "wrong session")
				throw new WrongSessionException();

			List<GuestRefreshStub> guestStubs = new List<GuestRefreshStub>();
			if (roomState != null)
			{
				foreach (StateStub ss in roomState)
				{
					if (ss.guest)
					{
						Guid g = ss.guid.UnPackGuid();
						Chat.RoomSpec spec = Chat.RoomSpec.FromGuid(g);
						if (!spec.MightFailReadPermissionCheck || Chat.Authenticate(ss.token, ss.tokenDateTimeTicks, g))
						{
							Guid guestLastItemGuidReturned = Guid.Empty;
							Guid guestLastItemGuid = ss.latestItem.Length == 0 ? Guid.Empty : ss.latestItem.UnPackGuid();
							string guestChatItems = cs.GetLatestGuest(usrK, isFirstRequest, guestLastItemGuid, ref guestLastItemGuidReturned, ss.guid.UnPackGuid());
							if (guestChatItems.Length > 0)
							{
								GuestRefreshStub guestStub = new GuestRefreshStub();
								guestStub.roomGuid = ss.guid;
								guestStub.itemsJson = guestChatItems;
								guestStub.lastItemGuidReturned = guestLastItemGuidReturned.Pack();
								guestStubs.Add(guestStub);
							}
						}
					}
				}
			}

			DateTime lastAction = new DateTime(long.Parse(lastActionTicks));
			if (lastAction < DateTime.Now.AddMinutes(-5))
				throw new TimeoutException();
			
			RefreshStub r = new RefreshStub();
			r.guestRefreshStubs = guestStubs.Count == 0 ? null : guestStubs.ToArray();
			r.itemsJson = chatItems;
			r.lastActionTicks = lastActionTicks;
			r.lastItemGuidReturned = lastItemGuidReturned.Pack();
			return r;
		}
		#endregion
		public class WrongSessionException : Exception { }
		public class TimeoutException : Exception { }
		public class ReadPermissionException : Exception { }
		public class WritePermissionException : Exception { }
		public class LoginPermissionException : Exception { }
		#endregion

		#region MoreInfo
		[WebMethod]
		[ScriptMethod]
		public MoreInfoStub MoreInfo(string roomGuid, string lastItemGuidString, int sessionID, string lastActionTicks, string pageUrl, StateStub[] roomState)
		{
			WaitIfDevEnv();

			Guid guid = roomGuid.UnPackGuid();
			Chat.RoomSpec spec = Chat.RoomSpec.FromGuid(guid);
			int usrK = Usr.Current == null ? 0 : Usr.Current.K;

			if (spec == null)
				throw new InvalidRoomException();

			if (!spec.CheckPermission(Usr.Current, false))
				throw new ReadPermissionException();

			storeRoomState(roomState, usrK);

			lastActionTicks = resetLastActionAndSessionID(sessionID);

			MoreInfoStub ms = new MoreInfoStub();
			ms.roomGuid = roomGuid;
			ms.moreInfoHtml = spec.GetMoreInfoHtml();

			RefreshStub r = refreshPrivate(false, lastItemGuidString, sessionID, lastActionTicks, pageUrl, usrK, roomState);
			ms.guestRefreshStubs = r.guestRefreshStubs;
			ms.itemsJson = r.itemsJson;
			ms.lastActionTicks = r.lastActionTicks;
			ms.lastItemGuidReturned = r.lastItemGuidReturned;
			return ms;
		}
		#endregion

		#region Pin
		#region Pin
		[WebMethod]
		[ScriptMethod]
		public PinStub Pin(string clientID, string roomGuid, string lastItemGuidString, int sessionID, string lastActionTicks, string pageUrl, StateStub[] roomState)
		{
			WaitIfDevEnv();

			Guid guid = roomGuid.UnPackGuid();
			Chat.RoomSpec spec = Chat.RoomSpec.FromGuid(guid);

			if (Usr.Current == null || Usr.Current.IsSkeleton || Usr.Current.Banned)
				throw new LoginPermissionException();

			if (spec == null)
				throw new InvalidRoomException();

			if (!spec.CheckPermission(Usr.Current, false))
				throw new ReadPermissionException();

			storeRoomState(roomState, Usr.Current.K);

			RoomStub r;

			if (spec.RoomType == RoomType.BuddyAlerts && spec.ObjectK > 0)
			{
				if (spec.ObjectK == Usr.Current.K)
					throw new SelfPrivateChatRoomException();

				spec = new Chat.RoomSpec(spec.ObjectK, Usr.Current.K);
				guid = spec.Guid;
			}

			if (spec.RoomType == RoomType.PrivateChat && spec.ObjectK > 0 && spec.SecondObjectK == 0)
			{
				if (spec.ObjectK == Usr.Current.K)
					throw new SelfPrivateChatRoomException();

				spec = new Chat.RoomSpec(spec.ObjectK, Usr.Current.K);
				guid = spec.Guid;
			}

			if (!spec.CheckPermission(Usr.Current, false))
				throw new ReadPermissionException();


			lastActionTicks = resetLastActionAndSessionID(sessionID);
			RoomPin p = getOrCreateRoomPin(guid, null, Usr.Current.K);

			r = new RoomStub(
				clientID, 
				guid.Pack(), 
				spec.GetName(Usr.Current), 
				spec.GetUrl(Usr.Current),
				true,
				spec.IsStarredByDefault,
				spec.IsStarredByDefault,
				true,
				spec.IsStarrable,
				false, 
				false, 
				0, 
				0, 
				"", 
				"", 
				"", 
				spec.IsReadOnly, 
				p.ListOrder, 
				spec.IsPhotoChatRoom,
				spec.IsPrivateChatRoom,
				spec.IsNewPhotoAlertsRoom,
				spec.GetPrivateChatRoomPresence(Usr.Current),
				spec.Icon,
				"",
				"",
				spec.HasArchive,
				spec.HiddenFromRoomList,
				spec.IsStreamRoom);

			Guid lastItemGuidReturned = Guid.Empty;
			Guid lastItemGuid = lastItemGuidString.Length == 0 ? Guid.Empty : lastItemGuidString.UnPackGuid();

			ChatLibrary.ChatServerInterface cs = (ChatLibrary.ChatServerInterface)Activator.GetObject(typeof(ChatLibrary.ChatServerInterface), Bobs.Vars.ChatServerAddress);
			string chatItems = cs.PinRoom(Usr.Current.K, sessionID, lastItemGuid, ref lastItemGuidReturned, guid);

			PinStub ps = new PinStub();
			ps.roomStub = r;
			ps.itemsJson = chatItems;
			ps.lastActionTicks = lastActionTicks;
			ps.lastItemGuidReturned = lastItemGuidReturned.Pack();
			return ps;
		}
		#endregion
		#region SwitchPhotoRoom
		[WebMethod]
		[ScriptMethod]
		public PinStub SwitchPhotoRoom(string clientID, string roomGuid, string lastItemGuidString, int sessionID, string lastActionTicks, string pageUrl, StateStub[] roomState)
		{
			WaitIfDevEnv();

			Guid guid = roomGuid.UnPackGuid();
			Chat.RoomSpec spec = Chat.RoomSpec.FromGuid(guid);
			int usrK = Usr.Current == null ? 0 : Usr.Current.K;

			if (spec == null)
				throw new InvalidRoomException();

			if (!spec.CheckPermission(Usr.Current, false))
				throw new ReadPermissionException();

			storeRoomState(roomState, usrK);

			RoomStub r;

			string tokenDateTime = DateTime.Now.Ticks.ToString();
			string token = Chat.GetToken(guid, tokenDateTime);

			//we should add the new guest room to the state stubs...
			StateStub ss = new StateStub();
			ss.guid = roomGuid;
			ss.guest = true;
			ss.listOrder = -1;
			ss.tokenDateTimeTicks = tokenDateTime;
			ss.token = token;
			List<StateStub> roomStateList = roomState.ToList();
			roomStateList.Add(ss);
			roomState = roomStateList.ToArray();

			lastActionTicks = resetLastActionAndSessionID(sessionID);

			r = new RoomStub(
				clientID,
				guid.Pack(),
				spec.GetName(Usr.Current),
				spec.GetUrl(Usr.Current),
				false,
				spec.IsStarredByDefault,
				spec.IsStarredByDefault,
				true,
				spec.IsStarrable,
				false,
				true,
				0,
				0,
				"",
				"",
				"",
				spec.IsReadOnly,
				-1,
				spec.IsPhotoChatRoom,
				spec.IsPrivateChatRoom,
				spec.IsNewPhotoAlertsRoom,
				spec.GetPrivateChatRoomPresence(Usr.Current),
				spec.Icon,
				tokenDateTime,
				token,
				spec.HasArchive,
				spec.HiddenFromRoomList,
				spec.IsStreamRoom);

			Guid lastItemGuidReturned = Guid.Empty;
			Guid lastItemGuid = lastItemGuidString.Length == 0 ? Guid.Empty : lastItemGuidString.UnPackGuid();

			ChatLibrary.ChatServerInterface cs = (ChatLibrary.ChatServerInterface)Activator.GetObject(typeof(ChatLibrary.ChatServerInterface), Bobs.Vars.ChatServerAddress);
			string chatItems = cs.GetLatest(usrK, sessionID, false, lastItemGuid, ref lastItemGuidReturned);

			PinStub ps = new PinStub();
			ps.roomStub = r;
			ps.itemsJson = chatItems;
			ps.lastActionTicks = lastActionTicks;
			ps.lastItemGuidReturned = lastItemGuidReturned.Pack();
			return ps;
		}
		#endregion
		#region RePin
		[WebMethod]
		[ScriptMethod]
		public RefreshStub RePin(string clientID, string roomGuid, string lastItemGuidString, int sessionID, string lastActionTicks, string pageUrl, StateStub[] roomState)
		{
			WaitIfDevEnv();

			Guid guid = roomGuid.UnPackGuid();
			Chat.RoomSpec spec = Chat.RoomSpec.FromGuid(guid);

			if (Usr.Current == null || Usr.Current.IsSkeleton || Usr.Current.Banned)
				throw new LoginPermissionException();

			if (spec == null)
				throw new InvalidRoomException();

			if (!spec.CheckPermission(Usr.Current, false))
				throw new ReadPermissionException();

			StateStub state = null;
			foreach (StateStub ss in roomState)
			{
				if (ss.guid == roomGuid)
				{
					state = ss;
					break;
				}
			}

			storeRoomState(roomState, Usr.Current.K);

			lastActionTicks = resetLastActionAndSessionID(sessionID);
			RoomPin p = getOrCreateRoomPin(guid, state != null ? (int?)state.listOrder : null, Usr.Current.K);

			Guid lastItemGuidReturned = Guid.Empty;
			Guid lastItemGuid = lastItemGuidString.Length == 0 ? Guid.Empty : lastItemGuidString.UnPackGuid();

			ChatLibrary.ChatServerInterface cs = (ChatLibrary.ChatServerInterface)Activator.GetObject(typeof(ChatLibrary.ChatServerInterface), Bobs.Vars.ChatServerAddress);
			string chatItems = cs.PinRoom(Usr.Current.K, sessionID, lastItemGuid, ref lastItemGuidReturned, guid);

			RefreshStub rs = new RefreshStub();
			rs.itemsJson = chatItems;
			rs.lastActionTicks = lastActionTicks;
			rs.lastItemGuidReturned = lastItemGuidReturned.Pack();
			return rs;
		}
		#endregion
		#region UnPin
		[WebMethod]
		[ScriptMethod]
		public UnPinStub UnPin(string clientID, string roomGuid, string lastItemGuidString, int sessionID, string lastActionTicks, string pageUrl, StateStub[] roomState)
		{
			WaitIfDevEnv();

			Guid guid = roomGuid.UnPackGuid();
			Chat.RoomSpec spec = Chat.RoomSpec.FromGuid(guid);

			if (Usr.Current == null || Usr.Current.IsSkeleton || Usr.Current.Banned)
				throw new LoginPermissionException();

			if (spec == null)
				throw new InvalidRoomException();

			StateStub state = null;
			foreach (StateStub ss in roomState)
			{
				if (ss.guid == roomGuid)
				{
					state = ss;
					break;
				}
			}

			#region Exit room on chat server only if we need to
			//For thread chat and PersistantAlertsRooms, we don't want to exit the room on the chat server.
			if (spec.RoomType == RoomType.Normal && (spec.ObjectType == Model.Entities.ObjectType.Thread || (spec.ObjectBob != null && spec.ObjectBob is IHasPrimaryThread && ((IHasPrimaryThread)spec.ObjectBob).ThreadK.IsNotNullOrZero())))
			{
				//don't exit the room if we're watching the topic
				bool exitRoom = true;
				int threadK = spec.ObjectType == Model.Entities.ObjectType.Thread ? spec.ObjectK : ((IHasPrimaryThread)spec.ObjectBob).ThreadK.Value;

				try
				{
					Thread t = new Thread(threadK);
					ThreadUsr tu = t.GetThreadUsr(Usr.Current);
					if (tu != null && tu.IsWatching)
						exitRoom = false;
				}
				catch { }
				if (exitRoom)
					Chat.ExitRoom(guid, Usr.Current.K);
			}
			else if (!spec.IsPersistantAlertsRoom)
			{
				Chat.ExitRoom(guid, Usr.Current.K);
			}
			#endregion

			#region Store the un-pinned room in the database, or delete the pin record.
			try
			{
				RoomPin rp = new RoomPin(Usr.Current.K, guid);
				if (spec.IsDefaultRoom)
				{
					rp.Pinned = false;
					rp.Update();
				}
				else
				{
					rp.Delete();
				}
			}
			catch (BobNotFound)
			{
				if (spec.IsDefaultRoom)
				{
					RoomPin rp = new RoomPin();
					rp.UsrK = Usr.Current.K;
					rp.RoomGuid = guid;
					rp.ListOrder = state != null ? state.listOrder : 0;
					rp.Pinned = false;
					rp.Starred = spec.IsStarredByDefault;
					rp.DateTime = DateTime.Now;
					rp.Update();
				}
			}
			#endregion

			storeRoomState(roomState, Usr.Current.K);

			lastActionTicks = resetLastActionAndSessionID(sessionID);

			Guid lastItemGuidReturned = Guid.Empty;
			Guid lastItemGuid = lastItemGuidString.Length == 0 ? Guid.Empty : lastItemGuidString.UnPackGuid();

			ChatLibrary.ChatServerInterface cs = (ChatLibrary.ChatServerInterface)Activator.GetObject(typeof(ChatLibrary.ChatServerInterface), Bobs.Vars.ChatServerAddress);
			string chatItems = cs.GetLatest(Usr.Current.K, sessionID, false, lastItemGuid, ref lastItemGuidReturned);

			UnPinStub ups = new UnPinStub();
			ups.roomGuid = roomGuid;
			ups.itemsJson = chatItems;
			ups.lastActionTicks = lastActionTicks;
			ups.lastItemGuidReturned = lastItemGuidReturned.Pack();
			return ups;
		}
		#endregion
		#region Star
		[WebMethod]
		[ScriptMethod]
		public RefreshStub Star(string clientID, string roomGuid, bool starred, string lastItemGuidString, int sessionID, string lastActionTicks, string pageUrl, StateStub[] roomState)
		{
			WaitIfDevEnv();

			Guid guid = roomGuid.UnPackGuid();
			Chat.RoomSpec spec = Chat.RoomSpec.FromGuid(guid);

			if (Usr.Current == null || Usr.Current.IsSkeleton || Usr.Current.Banned)
				throw new LoginPermissionException();

			if (spec == null)
				throw new InvalidRoomException();

			if (!spec.IsStarrable)
				throw new Exception("This room is not starrable");

			StateStub state = null;
			foreach (StateStub ss in roomState)
			{
				if (ss.guid == roomGuid)
				{
					state = ss;
					break;
				}
			}

			#region Store the star room in the database, or delete the pin record.
			try
			{
				RoomPin rp = new RoomPin(Usr.Current.K, guid);
				rp.Starred = starred;
				rp.Update();
			}
			catch (BobNotFound)
			{
				if (!spec.IsDefaultRoom || spec.IsStarredByDefault != starred)
				{
					RoomPin rp = new RoomPin();
					rp.UsrK = Usr.Current.K;
					rp.RoomGuid = guid;
					rp.ListOrder = state != null ? state.listOrder : 0;
					rp.Pinned = true;
					rp.Starred = starred;
					rp.DateTime = DateTime.Now;
					rp.Update();
				}
			}
			#endregion

			storeRoomState(roomState, Usr.Current.K);

			lastActionTicks = resetLastActionAndSessionID(sessionID);

			Guid lastItemGuidReturned = Guid.Empty;
			Guid lastItemGuid = lastItemGuidString.Length == 0 ? Guid.Empty : lastItemGuidString.UnPackGuid();

			ChatLibrary.ChatServerInterface cs = (ChatLibrary.ChatServerInterface)Activator.GetObject(typeof(ChatLibrary.ChatServerInterface), Bobs.Vars.ChatServerAddress);
			string chatItems = cs.GetLatest(Usr.Current.K, sessionID, false, lastItemGuid, ref lastItemGuidReturned);

			RefreshStub rs = new RefreshStub();
			rs.itemsJson = chatItems;
			rs.lastActionTicks = lastActionTicks;
			rs.lastItemGuidReturned = lastItemGuidReturned.Pack();
			return rs;
		}
		#endregion
		#region getOrCreateRoomPin
		RoomPin getOrCreateRoomPin(Guid guid, int? listOrder, int usrK)
		{
			Chat.RoomSpec spec = Chat.RoomSpec.FromGuid(guid);
			RoomPin p;
			try
			{
				p = new RoomPin(usrK, guid);
			}
			catch
			{
				p = new RoomPin();
				p.UsrK = usrK;
				p.RoomGuid = guid;
				if (listOrder.HasValue)
					p.ListOrder = listOrder.Value;
				else
					p.ListOrder = GetMaximumRoomPinListOrder(Usr.Current) + 1;
			}
			p.DateTime = DateTime.Now;
			p.Pinned = true;
			p.Starred = spec.IsStarredByDefault;
			p.Expires = false;
			p.Update();
			return p;
		}
		#endregion
		#region GetMaximumRoomPinListOrder
		int GetMaximumRoomPinListOrder(Usr u)
		{
			Query q = new Query();
			q.TopRecords = 1;
			q.QueryCondition = new And(new Q(RoomPin.Columns.UsrK, u.K), new Q(RoomPin.Columns.Pinned, true));
			q.OrderBy = new OrderBy(RoomPin.Columns.ListOrder, OrderBy.OrderDirection.Descending);
			RoomPinSet rps = new RoomPinSet(q);
			if (rps.Count > 0)
				return rps[0].ListOrder > Spotted.Controls.ChatClient.MaximumDefaultRoomListOrder ? rps[0].ListOrder : Spotted.Controls.ChatClient.MaximumDefaultRoomListOrder;
			else
				return Spotted.Controls.ChatClient.MaximumDefaultRoomListOrder;
		}
		#endregion
		public class InvalidRoomException : Exception { }
		public class SelfPrivateChatRoomException : Exception { }

		#endregion

		#region StoreUpdatedRoomListOrder
		[WebMethod]
		[ScriptMethod]
		public RefreshStub StoreUpdatedRoomListOrder(string lastItemGuidString, int sessionID, string lastActionTicks, string pageUrl, StateStub[] roomState)
		{
			WaitIfDevEnv();

			if (Usr.Current == null || Usr.Current.IsSkeleton || Usr.Current.Banned)
				throw new LoginPermissionException();

			storeRoomState(roomState, Usr.Current.K);

			foreach (StateStub ss in roomState)
			{
				try
				{
					RoomPin rp = new RoomPin(Usr.Current.K, ss.guid.UnPackGuid());
					if (rp.ListOrder != ss.listOrder)
					{
						rp.ListOrder = ss.listOrder;
						rp.Update();
					}
				}
				catch(BobNotFound)
				{
					Chat.RoomSpec spec = Chat.RoomSpec.FromGuid(ss.guid.UnPackGuid());
					RoomPin rp = new RoomPin();
					rp.DateTime = DateTime.Now;
					rp.ListOrder = ss.listOrder;
					rp.Pinned = true;
					rp.RoomGuid = spec.Guid;
					rp.Starred = spec.IsStarredByDefault;
					rp.UsrK = Usr.Current.K;
					rp.Update();
				}
			}

			return refreshPrivate(false, lastItemGuidString, sessionID, lastActionTicks, pageUrl, Usr.Current.K, roomState);
		}
		#endregion

		#region StoreState
		[WebMethod]
		[ScriptMethod]
		public bool StoreState(StateStub[] roomState)
		{
			WaitIfDevEnv();

			int usrK = Chat.GetUsrKWithoutTouchingDatabase();

			storeRoomState(roomState, usrK);

			return true;
		}
		#endregion	

		#region resetLastActionAndSessionID
		string resetLastActionAndSessionID(int sessionID)
		{
			DateTime now = DateTime.Now;
			
			if (Usr.Current != null)
			{
				Chat.NewResetSessionId(Usr.Current.K, sessionID);
				Usr.Current.RegisterPageRequest(true, now);
			}

			return now.Ticks.ToString();
		}
		#endregion

		#region RandomWait
		[WebMethod]
		[ScriptMethod]
		public void RandomWait(int min, int max)
		{
			Random r = new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
			System.Threading.Thread.Sleep(r.Next(min, max));
		}
		#endregion





	}
}

