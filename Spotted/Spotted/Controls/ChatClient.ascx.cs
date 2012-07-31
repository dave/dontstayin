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
using Bobs;
using SpottedScript.Controls.ChatClient.Shared;
using System.Collections.Generic;


namespace Spotted.Controls
{
	[ClientScript]
	public partial class ChatClient : EnhancedUserControl
	{
		public ChatClient()
		{
		}
		protected void Page_PreRender(object sender, EventArgs e)
		{

			//<div class="ChatClientBottomBar" onmouseover="stt('Drag this bar to resize the chat box')" onmouseout="htm();"><img src="/gfx/pin4.gif" width="20" height="7"/></div>

			
			JQuery.Include(Page, "ui.core");
			JQuery.Include(Page, "ui.mouse");
			JQuery.Include(Page, "ui.draggable");
			JQuery.Include(Page, "ui.sortable");

			JQuery.Include(Page, "effects.core");
			JQuery.Include(Page, "effects.drop");
			JQuery.Include(Page, "effects.transfer");

			if (this.Visible)
			{
				if (!ScriptManager.GetCurrent(Page).SupportsPartialRendering)
				{
					reconfigureVisibility(VisibilityModes.Downlevel);
					InitGo.Value = "0";
				}
				else
				{
					reconfigureVisibility(VisibilityModes.Normal);
					InitGo.Value = "1";
					InitUsrK.Value = Usr.Current == null ? "0" : Usr.Current.K.ToString();
					InitClientID.Value = this.ClientID;
					InitLastActionTicks.Value = DateTime.Now.Ticks.ToString();
					InitSystemMessagesRoomGuid.Value = new Chat.RoomSpec(RoomType.SystemMessages, Usr.Current == null ? Model.Entities.ObjectType.None : Model.Entities.ObjectType.Usr, Usr.Current == null ? 0 : Usr.Current.K).Guid.Pack();
					InitInboxUpdatesRoomGuid.Value = new Chat.RoomSpec(RoomType.InboxUpdates, Usr.Current == null ? Model.Entities.ObjectType.None : Model.Entities.ObjectType.Usr, Usr.Current == null ? 0 : Usr.Current.K).Guid.Pack();
					InitBuddyAlertsRoomGuid.Value = new Chat.RoomSpec(RoomType.BuddyAlerts, Usr.Current == null ? Model.Entities.ObjectType.None : Model.Entities.ObjectType.Usr, Usr.Current == null ? 0 : Usr.Current.K).Guid.Pack();
					InitBuddyStreamRoomGuid.Value = new Chat.RoomSpec(RoomType.BuddyStream, Usr.Current == null ? Model.Entities.ObjectType.None : Model.Entities.ObjectType.Usr, Usr.Current == null ? 0 : Usr.Current.K).Guid.Pack();
					InitAnimatePopups.Value = Prefs.Current["ChatClientAnimatePopups"].Exists && Prefs.Current["ChatClientAnimatePopups"] == 0 ? "false" : "true";

					#region LatestTopPhoto (removed)
					//Photo p = Photo.GetLatestTopPhoto(true, true);
					//if (p != null)
					//{
					//    InitTopPhoto.Value = String.Format(
					//        "{0},{1},{2},{3},{4},{5},{6},{7},{8}",
					//        p.K,
					//        p.Url(),
					//        p.Icon,
					//        p.Web,
					//        p.WebWidth,
					//        p.WebHeight,
					//        p.Thumb,
					//        p.ThumbWidth,
					//        p.ThumbHeight);
					//}
					#endregion

					if (Vars.IE)
						TextBox.Style["margin-bottom"] = "-1px";
					TextBox.Style["width"] = Vars.IE ? "292px" : "292px";

					//if (Usr.Current == null || Usr.Current.IsSkeleton)
					//{
					//    if (Usr.Current == null)
					//    {
					//        TextBox.Value = "You must log in to chat!";
					//        TextBox.Attributes["onclick"] = "event.cancelBubble = true; if (event.stopPropagation) { event.stopPropagation(); } DsiPageShowLoginNew(); return false;";
					//    }
					//    else if (Usr.Current.IsSkeleton)
					//    {
					//        TextBox.Value = "You must complete sign-up to chat!";
					//        TextBox.Attributes["onclick"] = "event.cancelBubble = true; if (event.stopPropagation) { event.stopPropagation(); } document.location = \"/popup/welcome?Url=" + HttpUtility.UrlEncode(Request.QueryString["Url"] != null && Request.QueryString["Url"].Length > 0 ? Request.QueryString["Url"] : Page.Request.Url.PathAndQuery) + "\";return false;";
					//    }
					//    TextBox.Attributes["readonly"] = "true";
					//    TextBox.Style["cursor"] = "pointer";
						
					//}

					if (Usr.Current != null)
					{
						//PrivateChatDrop

						//BuddySet bs = Usr.Current.ChildBuddys(Usr.LoggedInChatQ, new KeyValuePair<object, OrderBy.OrderDirection>(Usr.Columns.K, OrderBy.OrderDirection.Ascending)).AllItems();
						UsrSet us;

						if (Vars.DevEnv)
						{
							{
								Query q = new Query();
								q.QueryCondition = new And(new Q(Usr.Columns.K, QueryOperator.NotEqualTo, Usr.Current.K), Usr.LoggedInChatQ);
								q.OrderBy = new OrderBy(Usr.Columns.NickName);
								q.TopRecords = 50;
								us = new UsrSet(q);
							}

							if (us.Count == 0)
							{
								Query q = new Query();
								q.TableElement = Usr.BuddyJoin;
								q.QueryCondition = new Q(Buddy.Columns.UsrK, Usr.Current.K);
								q.OrderBy = new OrderBy(Usr.Columns.DateTimeLastAccess, OrderBy.OrderDirection.Descending);
								q.TopRecords = 20;
								us = new UsrSet(q);
							}
						}
						else
						{
							Query q = new Query();
							q.TableElement = Usr.BuddyJoin;
							q.QueryCondition = new And(
								new Q(Buddy.Columns.UsrK, Usr.Current.K),
								Usr.LoggedInChatQ);
							q.OrderBy = new OrderBy(Usr.Columns.NickName);
							us = new UsrSet(q);
						}
						if (us.Count == 0)
						{
							PrivateChatDrop.Items.Add(new ListItem("None of your buddies are online", "0"));
						}
						foreach (Usr u in us)
						{
							PrivateChatDrop.Items.Add(new ListItem(u.NickName, new Bobs.Chat.RoomSpec(Usr.Current.K, u.K).Guid.Pack()));
						}

					}
					else
					{
						RoomsMain.Style["display"] = "none";
						PrivateChatDropMain.Style["display"] = "none";
					}
					
					Dictionary<Guid, RoomStub> rooms = new Dictionary<Guid, RoomStub>(); 
					List<Guid> roomsOrder = new List<Guid>();
					RoomPinSet roomPinSet = getPinnedRooms(rooms, roomsOrder);
					addDefaultRooms(rooms, roomsOrder);
					
					ensureAllRoomsAreInDatabase(rooms, roomsOrder, roomPinSet);

					List<Guid> guestRooms = getGuestRooms(rooms);

					Dictionary<Guid, StateStub> stateStubs = getState(rooms, guestRooms, roomPinSet);

					//addGuestRooms(guestRooms, rooms, roomsOrder);

					#region //remove disabled rooms
					Dictionary<Guid, RoomStub> newRooms = new Dictionary<Guid, RoomStub>();
					foreach (RoomStub roomStub in rooms.Values)
					{
						Guid g = roomStub.guid.UnPackGuid();
						Chat.RoomSpec r = Chat.RoomSpec.FromGuid(g);
						if (r == null || r.Disabled)
							roomsOrder.Remove(g);
						else
							newRooms.Add(r.Guid, roomStub);
					}
					rooms = newRooms;
					#endregion

					mergeStateIntoRoomsList(rooms, stateStubs);

					RoomStub selectedRoom = ensureOnlyOneRoomIsSelectedAndGetSelectedRoom(rooms);

					ensureOnlyGuestRoomsAreUnPinned(rooms, roomsOrder);

					bool hasGuestRooms = false;
					bool hasPrivateRooms = false;
					addContorls(rooms, roomsOrder, ref hasGuestRooms, ref hasPrivateRooms);

					sendJoinRoomsCommandToChatServerForEmittedRooms(rooms);

					updateUI(selectedRoom, roomsOrder, hasGuestRooms, hasPrivateRooms);
				}
			}
		}
		
		#region getPinnedRooms
		RoomPinSet getPinnedRooms(Dictionary<Guid, RoomStub> rooms, List<Guid> roomsOrder)
		{
			if (Usr.Current != null)
			{
				#region Get pinned rooms from the RoomPin table
				Query q = new Query();
				q.QueryCondition = new Q(RoomPin.Columns.UsrK, Usr.Current.K);
				q.OrderBy = new OrderBy(RoomPin.Columns.ListOrder);
				RoomPinSet rps = new RoomPinSet(q);
				foreach (RoomPin rp in rps)
				{
					Chat.RoomSpec spec = Chat.RoomSpec.FromGuid(rp.RoomGuid);
					if (spec != null)
					{
						try
						{
							rooms.Add(rp.RoomGuid, getRoomStub(rp.RoomGuid, spec.GetName(Usr.Current), spec.GetUrl(Usr.Current), spec.IsPinnable, false, spec.IsReadOnly, rp.Pinned, spec.IsPhotoChatRoom, spec.IsPrivateChatRoom, spec.IsNewPhotoAlertsRoom, spec.GetPrivateChatRoomPresence(Usr.Current), spec.Icon, rp.Starred.GetValueOrDefault(spec.IsStarredByDefault), rp.ListOrder, spec.IsStarredByDefault, spec.IsStarrable, spec.HasArchive, spec.HiddenFromRoomList, spec.IsStreamRoom));
							roomsOrder.Add(rp.RoomGuid);
						}
						catch { }
					}
				}
				return rps;
				#endregion
			}
			else
				return null;
		}
		#endregion
		#region addDefaultRooms
		public static int MaximumDefaultRoomListOrder = 3;
		void addDefaultRooms(Dictionary<Guid, RoomStub> rooms, List<Guid> roomsOrder)
		{
			if (Usr.Current != null)
			{
				#region Add default rooms

				insertRoom(
					rooms,
					roomsOrder,
					0,
					new Chat.RoomSpec(RoomType.Normal),
					false);

				insertRoom(
					rooms,
					roomsOrder,
					1,
					new Chat.RoomSpec(RoomType.PrivateChatRequests),
					false);

				insertRoom(
					rooms,
					roomsOrder,
					2,
					new Chat.RoomSpec(RoomType.InboxUpdates, Model.Entities.ObjectType.Usr, Usr.Current.K),
					false);

				//insertRoom(
				//    rooms,
				//    roomsOrder,
				//    2,
				//    new Chat.RoomSpec(RoomType.BuddyAlerts),
				//    false);

				//insertRoom(
				//    rooms,
				//    roomsOrder,
				//    3,
				//    new Chat.RoomSpec(RoomType.PrivateChatRequestsBuddies),
				//    false);

				//insertRoom(
				//    rooms,
				//    roomsOrder,
				//    4,
				//    new Chat.RoomSpec(RoomType.PrivateChatRequestsStrangers),
				//    false);

				//insertRoom(
				//    rooms,
				//    roomsOrder,
				//    5,
				//    new Chat.RoomSpec(RoomType.RandomChat),
				//    false);

				//insertRoom(
				//    rooms,
				//    roomsOrder,
				//    6,
				//    new Chat.RoomSpec(RoomType.Laughs),
				//    false);

				//insertRoom(
				//    rooms,
				//    roomsOrder,
				//    7,
				//    new Chat.RoomSpec(RoomType.NewPhotosProSpotters),
				//    false);

				//insertRoom(
				//    rooms,
				//    roomsOrder,
				//    8,
				//    new Chat.RoomSpec(RoomType.NewPhotosSpotters),
				//    false);

				insertRoom(
					rooms,
					roomsOrder,
					3,
					new Chat.RoomSpec(RoomType.PublicStream),
					false);

				//REMEMBER TO UPDATE MaximumDefaultRoomListOrder!!!

				#endregion
			}
			else
			{
				#region //Add default rooms (logged off)

				insertRoom(
					rooms,
					roomsOrder,
					0,
					new Chat.RoomSpec(RoomType.Normal),
					false);

				//insertRoom(
				//    rooms,
				//    roomsOrder,
				//    1,
				//    new Chat.RoomSpec(RoomType.RandomChat),
				//    false);

				//insertRoom(
				//    rooms,
				//    roomsOrder,
				//    2,
				//    new Chat.RoomSpec(RoomType.Laughs),
				//    false);

				//insertRoom(
				//    rooms,
				//    roomsOrder,
				//    3,
				//    new Chat.RoomSpec(RoomType.NewPhotosProSpotters),
				//    false);

				//insertRoom(
				//    rooms,
				//    roomsOrder,
				//    4,
				//    new Chat.RoomSpec(RoomType.NewPhotosSpotters),
				//    false);

				//insertRoom(
				//    rooms,
				//    roomsOrder,
				//    1,
				//    new Chat.RoomSpec(RoomType.Normal),
				//    false);

				//if (Vars.DevEnv)
				//{
				//    insertRoom(
				//        rooms,
				//        roomsOrder,
				//        2,
				//        new Chat.RoomSpec(RoomType.SystemMessages),
				//        false);
				//}

				
				
				#endregion
			}
		}
		#endregion
		void ensureAllRoomsAreInDatabase(Dictionary<Guid, RoomStub> rooms, List<Guid> roomsOrder, RoomPinSet roomPinSet)
		{
			if (Usr.Current != null)
			{
				Dictionary<Guid, RoomPin> roomPins = new Dictionary<Guid, RoomPin>();
				if (roomPinSet != null) roomPinSet.ToList().ForEach((rp) => { if (!roomPins.ContainsKey(rp.RoomGuid)) roomPins.Add(rp.RoomGuid, rp); });

				foreach (Guid g in roomsOrder)
				{
					if (!roomPins.ContainsKey(g))
					{
						try
						{
							Chat.RoomSpec spec = Chat.RoomSpec.FromGuid(g);
							RoomPin rp = new RoomPin();
							rp.DateTime = DateTime.Now;
							rp.ListOrder = rooms[g].listOrder;
							rp.Pinned = rooms[g].pinned;
							rp.RoomGuid = g;
							rp.Starred = rooms[g].starred;
							rp.UsrK = Usr.Current.K;
							rp.Update();
						}
						catch { }
					}
				}
			}
		}
		#region getGuestRooms
		List<Guid> getGuestRooms(Dictionary<Guid, RoomStub> rooms)
		{
			List<Guid> guestRooms = new List<Guid>();
			Thread t = null;
			if (ContainerPage.Url.HasObjectFilter || (ContainerPage.Url.CurrentApplication == "chat" && ContainerPage.Url["k"].IsInt))
			{
				IBob guestChatObject = null;

				#region Get from page
				if (ContainerPage.Url.CurrentApplication == "chat" && ContainerPage.Url["k"].IsInt)
				{
					try
					{
						t = new Thread(ContainerPage.Url["k"]);
						IBob b = t.ParentObject;
						while (b != null && b is Photo && b is IHasParent)
							b = ((IHasParent)b).ParentObject;
						guestChatObject = (IBob)b;
					}
					catch { }
				}
				else if (ContainerPage.Url.HasArticleObjectFilter)
					guestChatObject = ContainerPage.Url.ObjectFilterArticle;
				else if (ContainerPage.Url.HasGroupLogicalFilter)
					guestChatObject = ContainerPage.Url.LogicalFilterGroup;
				else if (ContainerPage.Url.HasEventObjectFilter)
					guestChatObject = ContainerPage.Url.ObjectFilterEvent;
				else if (ContainerPage.Url.HasVenueObjectFilter)
					guestChatObject = ContainerPage.Url.ObjectFilterVenue;
				else if (ContainerPage.Url.HasPlaceObjectFilter)
					guestChatObject = ContainerPage.Url.ObjectFilterPlace;
				else if (ContainerPage.Url.HasCountryObjectFilter)
					guestChatObject = ContainerPage.Url.ObjectFilterCountry;
				else if (ContainerPage.Url.HasUsrObjectFilter && Usr.Current != null && ContainerPage.Url.ObjectFilterUsr.K != Usr.Current.K)
					guestChatObject = ContainerPage.Url.ObjectFilterUsr;
				#endregion

				if (guestChatObject != null)
				{
					#region Create spec
					Chat.RoomSpec s = null;
					if (guestChatObject is Usr)
					{
						if (Usr.Current != null)
						{
							Usr usrWeAreVisiting = (Usr)guestChatObject;
							if (usrWeAreVisiting.K != Usr.Current.K)
							{
								s = new Chat.RoomSpec(Usr.Current.K, usrWeAreVisiting.K);
							}
						}
					}
					else
					{
						s = new Chat.RoomSpec(RoomType.Normal, ((IBobType)guestChatObject).ObjectType, ((IHasSinglePrimaryKey)guestChatObject).K);
					}
					#endregion

					if (s != null && !s.Disabled && s.CheckPermission(Usr.Current, false))
					{
						Guid g = s.Guid;
						guestRooms.Add(g);
					}
				}
			}
			#region Did we find a thread?
			if (t != null)
			{
				try
				{
					Chat.RoomSpec s = t.GetRoomSpec();
					if (s != null && !s.Disabled && s.CheckPermission(Usr.Current, false))
						guestRooms.Add(s.Guid);
				}
				catch { }
			}
			#endregion

			#region Photo guest
			Photo p = null;
			if ((ContainerPage.Url.CurrentApplication.StartsWith("tags/") || ContainerPage.Url.CurrentApplication == "photos") && ContainerPage.Url["photo"].IsInt)
			{
				//photo room...
				try
				{
					p = new Photo(ContainerPage.Url["photo"]);
				}
				catch { }
			}
			else if (ContainerPage.Url.HasPhotoObjectFilter)
			{
				try
				{
					p = ContainerPage.Url.ObjectFilterPhoto;
				}
				catch { }
			}
			else if (ContainerPage.Url.HasGalleryObjectFilter && ContainerPage.Url.ObjectFilterGallery.LivePhotos > 0)
			{
				try
				{
					p = ContainerPage.Url.ObjectFilterGallery.ChildPhotos(Photo.EnabledQueryCondition, Photo.DefaultOrder)[0];
				}
				catch { }
			}

			if (p != null)
			{
				try
				{
					Chat.RoomSpec s = new Chat.RoomSpec(RoomType.Normal, Model.Entities.ObjectType.Photo, p.K);
					if (s != null && !s.Disabled && s.CheckPermission(Usr.Current, false))
						guestRooms.Add(s.Guid);
				}
				catch { }
			}
			#endregion

			return guestRooms;
		}
		#endregion
		#region getStateFromMemcached
		Dictionary<Guid, StateStub> getState(Dictionary<Guid, RoomStub> rooms, List<Guid> guestRooms, RoomPinSet roomPinSet)
		{
			List<Guid> allRooms = new List<Guid>();
			foreach (Guid g in rooms.Keys)
			{
				if (rooms[g].pinned)
					allRooms.Add(g);
			}
			foreach (Guid guest in guestRooms)
			{
				if (!allRooms.Contains(guest))
					allRooms.Add(guest);
			}
			//remove un-pinned non-guests...

			int usrK = Usr.Current == null ? 0 : Usr.Current.K;
			//Guid dsiGuid = usrK == 0 ? Guid.Empty : Chat.GetDsiGuidWithoutTouchingDatabase();
			Guid dsiGuid = usrK > 0 ? Guid.Empty : Chat.GetDsiGuidWithoutTouchingDatabase();

			StateStub[] stateFromCache = Chat.GetStateFromCacheOrDatabase(allRooms.ToArray(), usrK, dsiGuid, roomPinSet);
			Dictionary<Guid, StateStub> stateStubs = new Dictionary<Guid, StateStub>();
			if (stateFromCache != null)
			{
				foreach (StateStub stateStub in stateFromCache)
				{
					Guid g = stateStub.guid.UnPackGuid();
					if (!stateStubs.ContainsKey(g))
						stateStubs.Add(g, stateStub);
				}
			}
			return stateStubs;
		}
		#endregion
		#region searchRoomsForUnPinnedRooms (Removed)
		//List<Guid> searchRoomsForUnPinnedRooms(Dictionary<Guid, RoomStub> rooms, List<Guid> roomsOrder, Dictionary<Guid, StateStub> stateStubs)
		//{
		//    List<Guid> roomsToExitAndUnPin = new List<Guid>();
		//    foreach (RoomStub room in rooms.Values)
		//    {
		//        Guid g = room.guid.UnPackGuid();
		//        if (stateStubs.ContainsKey(g))
		//        {
		//            StateStub state = stateStubs[g];
		//            if (!state.pinned)
		//                roomsToExitAndUnPin.Add(g);
		//        }
		//    }
		//    return roomsToExitAndUnPin;
		//}
		#endregion
		#region exitAndUnPin (Removed)
		//void exitAndUnPin(List<Guid> roomsToExitAndUnPin, Dictionary<Guid, RoomStub> rooms, List<Guid> roomsOrder)
		//{
		//    foreach (Guid g in roomsToExitAndUnPin)
		//    {
		//        Chat.RoomSpec spec = Chat.RoomSpec.FromGuid(g);

		//        //For thread chat and PersistantAlertsRooms, we don't want to exit the room on the chat server.
		//        if (spec.RoomType == RoomType.Normal && (spec.ObjectType == Model.Entities.ObjectType.Thread || (spec.ObjectBob != null && spec.ObjectBob is IHasPrimaryThread && ((IHasPrimaryThread)spec.ObjectBob).ThreadK.IsNotNullOrZero())))
		//        {
		//            //don't exit the room if we're watching the topic
		//            bool exitRoom = true;
		//            int threadK = 0;
		//            if (spec.ObjectType == Model.Entities.ObjectType.Thread)
		//                threadK = spec.ObjectK;
		//            else
		//                threadK = ((IHasPrimaryThread)spec.ObjectBob).ThreadK.Value;

		//            try
		//            {
		//                Thread t = new Thread(threadK);
		//                ThreadUsr tu = t.GetThreadUsr(Usr.Current);
		//                if (tu != null && tu.IsWatching)
		//                    exitRoom = false;
		//            }
		//            catch { }
		//            if (exitRoom)
		//                Chat.ExitRoom(g, Usr.Current.K);
		//        }
		//        else if (!spec.IsPersistantAlertsRoom)
		//        {
		//            Chat.ExitRoom(g, Usr.Current.K);
		//        }


		//        try
		//        {
		//            RoomPin rp = new RoomPin(Usr.Current.K, g);
		//            if (spec != null && spec.IsDefaultRoom)
		//            {
		//                rp.Pinned = false;
		//                rp.Update();
		//            }
		//            else
		//            {
		//                rp.Delete();
		//            }
		//        }
		//        catch (BobNotFound)
		//        {
		//            if (spec != null && spec.IsDefaultRoom)
		//            {
		//                RoomPin rp = new RoomPin();
		//                rp.UsrK = Usr.Current.K;
		//                rp.RoomGuid = g;
		//                rp.Pinned = false;
		//                rp.DateTime = DateTime.Now;
		//                rp.Update();
		//            }
		//        }

		//        if (rooms.ContainsKey(g))
		//            rooms.Remove(g);

		//        if (roomsOrder.Contains(g))
		//            roomsOrder.Remove(g);
		//    }
		//}
		#endregion
		#region addGuestRooms
		void addGuestRooms(List<Guid> guestRooms, Dictionary<Guid, RoomStub> rooms, List<Guid> roomsOrder)
		{
			foreach (Guid g in guestRooms)
			{
				if (!rooms.ContainsKey(g))
				{
					Chat.RoomSpec spec = Chat.RoomSpec.FromGuid(g);
					rooms.Add(g, getRoomStub(g, spec.GetName(Usr.Current), spec.GetUrl(Usr.Current), true, true, false, false, spec.IsPhotoChatRoom, spec.IsPrivateChatRoom, spec.IsNewPhotoAlertsRoom, spec.GetPrivateChatRoomPresence(Usr.Current), spec.Icon, spec.IsStarredByDefault, -1, spec.IsStarredByDefault, spec.IsStarrable, spec.HasArchive, spec.HiddenFromRoomList, spec.IsStreamRoom));
					roomsOrder.Insert(0, g);
				}
			}
		}
		#endregion
		#region mergeStateIntoRoomsList
		void mergeStateIntoRoomsList(Dictionary<Guid, RoomStub> rooms, Dictionary<Guid, StateStub> stateStubs)
		{
			foreach (RoomStub room in rooms.Values)
			{
				Guid g = room.guid.UnPackGuid();
				if (stateStubs.ContainsKey(g))
				{
					StateStub state = stateStubs[g];

					room.selected = state.selected;
					room.newMessages = state.newMessages;
					room.totalMessages = state.totalMessages;
					room.latestItem = state.latestItem;
					room.latestItemSeen = state.latestItemSeen;
					room.latestItemOld = state.latestItemOld;

				}

			}
		}
		#endregion
		#region ensureOnlyOneRoomIsSelectedAndGetSelectedRoom
		RoomStub ensureOnlyOneRoomIsSelectedAndGetSelectedRoom(Dictionary<Guid, RoomStub> rooms)
		{
			RoomStub selectedRoom = null;
			foreach (RoomStub room in rooms.Values)
			{
				if (room.selected)
				{
					if (selectedRoom == null)
						selectedRoom = room;
					else
						room.selected = false;
				}
			}
			return selectedRoom;
		}
		#endregion
		#region ensureOnlyGuestRoomsAreUnPinned
		void ensureOnlyGuestRoomsAreUnPinned(Dictionary<Guid, RoomStub> rooms, List<Guid> roomsOrder)
		{
			Guid[] keys = new Guid[rooms.Keys.Count];
			rooms.Keys.CopyTo(keys, 0);
			foreach (Guid g in keys)
			{
				RoomStub r = rooms[g];
				if (!r.guest && !r.pinned && r.pinable)
				{
					rooms.Remove(g);
					roomsOrder.Remove(g);
				}
			}
		}
		#endregion
		#region addContorls
		void addContorls(Dictionary<Guid, RoomStub> rooms, List<Guid> roomsOrder, ref bool hasGuestRooms, ref bool hasPrivateRooms)
		{
			foreach (Guid g in roomsOrder)
			{
				RoomHtml roomHtml = new RoomHtml(rooms[g], Usr.Current != null);
				if (rooms[g].guest)
				{
					hasGuestRooms = true;
					RoomGuestList.Controls.Add(new LiteralControl(roomHtml.ToHtml()));
				}
				else if (rooms[g].isPrivateChatRoom)
				{
					hasPrivateRooms = true;
					RoomPrivateList.Controls.Add(new LiteralControl(roomHtml.ToHtml()));
				}
				else
				{
					RoomList.Controls.Add(new LiteralControl(roomHtml.ToHtml()));
				}
			}
		}
		#endregion
		#region sendJoinRoomsCommandToChatServerForEmittedRooms
		void sendJoinRoomsCommandToChatServerForEmittedRooms(Dictionary<Guid, RoomStub> rooms)
		{
				List<Guid> roomsList = new List<Guid>();
				foreach (RoomStub room in rooms.Values)
				{
					if (!room.guest)
						roomsList.Add(room.guid.UnPackGuid());
				}
				Chat.JoinRoom(roomsList.ToArray(), Usr.Current == null ? 0 : Usr.Current.K);
		}
		#endregion
		#region updateUI
		void updateUI(RoomStub selectedRoom, List<Guid> roomsOrder, bool hasGuestRooms, bool hasPrivateRooms)
		{
			bool readOnly = true;
			bool isPrivateRoom = false;
			if (selectedRoom != null)
			{
				Chat.RoomSpec selectedRoomSpec = Chat.RoomSpec.FromGuid(selectedRoom.guid.UnPackGuid());
				readOnly = selectedRoomSpec.IsReadOnly;
				isPrivateRoom = selectedRoomSpec.IsPrivateChatRoom;
			}
			RoomGuestListDivider.Style["display"] = hasGuestRooms ? "" : "none";
			RoomGuestList.Style["display"] = hasGuestRooms ? "" : "none";
			//RoomPrivateListDivider.Style["display"] = hasPrivateRooms ? "" : "none";
			RoomPrivateList.Style["display"] = hasPrivateRooms ? "" : "none";
			TextBox.Style["display"] = readOnly ? "none" : "";
			DeleteArchiveHolder.Style["display"] = isPrivateRoom ? "" : "none";
		}
		#endregion

		#region insertRoom
		void insertRoom(Dictionary<Guid, RoomStub> rooms, List<Guid> roomsOrder, int listOrder, Chat.RoomSpec spec, bool guest)
		{
			Guid g = spec.Guid;
			string name = spec.GetName(Usr.Current);
			string url = spec.GetUrl(Usr.Current);

			if (!rooms.ContainsKey(g))
			{
				rooms.Add(g, getRoomStub(g, name, url, spec.IsPinnable, guest, spec.IsReadOnly, true, spec.IsPhotoChatRoom, spec.IsPrivateChatRoom, spec.IsNewPhotoAlertsRoom, spec.GetPrivateChatRoomPresence(Usr.Current), spec.Icon, spec.IsStarredByDefault, listOrder, spec.IsStarredByDefault, spec.IsStarrable, spec.HasArchive, spec.HiddenFromRoomList, spec.IsStreamRoom));
				//roomsOrder.Insert(0, g);

				for (int i = 0; i <= roomsOrder.Count; i++)
				{
					if (i == roomsOrder.Count)
					{
						roomsOrder.Add(g);
						break;
					}
					else if (rooms[roomsOrder[i]].listOrder > listOrder)
					{
						roomsOrder.Insert(i, g);
						break;
					}
				}
			}
		}
		#endregion
		#region getRoomStub
		RoomStub getRoomStub(Guid g, string name, string url, bool pinnable, bool guest, bool readOnly, bool pinned, bool isPhotoChatRoom, bool isPrivateChatRoom, bool isNewPhotoAlertsRoom, PresenceState presence, string icon, bool starred, int listOrder, bool isStarredByDefault, bool starrable, bool hasArchive, bool hiddenFromRoomList, bool isStreamRoom)
		{
			string tokenDateTimeTicks = guest ? DateTime.Now.Ticks.ToString() : "";
			string token = guest ? Chat.GetToken(g, tokenDateTimeTicks) : "";
			return new RoomStub(
				this.ClientID,
				g.Pack(),//.ToString("N"),
				name,
				url,
				pinned,
				starred,
				isStarredByDefault,
				pinnable,
				starrable,
				false,//Prefs.Current["ChatClientSelectedRoomGuid"].Exists && Prefs.Current["ChatClientSelectedRoomGuid"].Equals(g.ToString("N")), 
				guest,
				0,
				0,
				"",
				"",
				"",
				readOnly,
				listOrder,
				isPhotoChatRoom,
				isPrivateChatRoom,
				isNewPhotoAlertsRoom,
				presence,
				icon,
				tokenDateTimeTicks,
				token,
				hasArchive,
				hiddenFromRoomList,
				isStreamRoom);
		}
		#endregion

		#region VisibilityModes
		enum VisibilityModes
		{
			Downlevel,
			Normal
		}
		void reconfigureVisibility(VisibilityModes mode)
		{
			DownlevelMain.Visible = mode == VisibilityModes.Downlevel;
			RoomsMain.Visible = mode == VisibilityModes.Normal;
			MessagesMain.Visible = mode == VisibilityModes.Normal;
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


	}
}
