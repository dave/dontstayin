using System.DHTML;
using System;
using Sys.UI;
using Spotted.System.Text;
using Sys;
using Sys.Net;
using System.XML;
using SpottedScript.Controls.ChatClient.Items;
using SpottedScript.Controls.ChatClient.Shared;
using ChatClientController = SpottedScript.Controls.ChatClient.Controller;
using HtmlItem = SpottedScript.Controls.ChatClient.Items.Html;
using Login = SpottedScript.Controls.Navigation.Login.PageImplementation;
using JQ;
using ScriptSharpLibrary;

namespace SpottedScript.Controls.ChatClient
{
	public delegate void SortableUpdateDelegate(object e, JQueryObject ui);
	public delegate void ResizeDelegate(object e, JQueryObject ui);
	public class Controller
	{

		#region Private members
		int SessionID;
		string LastActionTicks;
		ServerClass Server;
		Dictionary Rooms;
		Array RoomsListOrder;
		StateStub[] State;
		int lastKeyDown;
		string SystemMessagesRoomGuid;
		string InboxUpdatesRoomGuid;
		string BuddyStreamRoomGuid;
		string PublicStreamRoomGuid;
		string PrivateChatRequestsRoomGuid;
		bool chatClientIsPaused;
		View View;
		PopupArea Popups;
		long serverTicksAtPageLoad;
		long clientTicksAtPageLoad;
		#endregion

		#region Public members
		public int UsrK;
		public bool LoggedIn;
		public string ClientID;
		public static Controller Instance;
		public bool HasFocus;
		public DOMElement StreamList;
		#endregion

		#region getClientTicksSincePageLoad()
		long getClientTicksSincePageLoad()
		{
			long clientTicksNow = getRelevantDigits(new DateTime().GetTime().ToString());
			long clientTicksSincePageLoad = clientTicksNow - clientTicksAtPageLoad;
			return clientTicksSincePageLoad > 0 ? clientTicksSincePageLoad : 0;
		}
		public long GetMessageAge(string serverTicksMessageString)
		{
			long serverTicksMessage = getRelevantDigits(serverTicksMessageString);
			long serverTicksNow = serverTicksAtPageLoad + (getClientTicksSincePageLoad() * 10000);
			long messageAge = (serverTicksNow - serverTicksMessage) / 10000;
			return messageAge > 0 ? messageAge : 0;
		}
		long getRelevantDigits(string stringIn)
		{
			return long.ParseInvariant(stringIn);
		//	if (stringIn.Length > 10)
		//		return int.ParseInvariant(stringIn.Substr(stringIn.Length - 10, 6));
		//	else
		//		return int.ParseInvariant(stringIn.Substr(0, stringIn.Length - 4));
		}
		#endregion

		#region public Controller(View view)
		public Controller(View view)
		{
			View = view;

			if (Misc.BrowserIsIE)
				JQueryAPI.JQuery(Document.Body).ready(new Action(initialise));
			else
				initialise();
		}
		void initialise()
		{
			if (View.InitGo.Value == "0")
				return;

			serverTicksAtPageLoad = getRelevantDigits(View.InitLastActionTicks.Value);
			clientTicksAtPageLoad = getRelevantDigits(new DateTime().GetTime().ToString());

			StreamList = View.StreamList;
			Instance = this;
			UsrK = int.ParseInvariant(View.InitUsrK.Value);
			LoggedIn = UsrK > 0;
			ClientID = View.InitClientID.Value;
			LastActionTicks = View.InitLastActionTicks.Value;
			SessionID = Math.Round(Math.Random() * 10000);
			SystemMessagesRoomGuid = View.InitSystemMessagesRoomGuid.Value;
			InboxUpdatesRoomGuid = View.InitInboxUpdatesRoomGuid.Value;
			BuddyStreamRoomGuid = View.InitBuddyStreamRoomGuid.Value;
			PublicStreamRoomGuid = "ARAFAAAAAAUAAAAANdfH9w";
			PrivateChatRequestsRoomGuid = "ARIFAAAAAAUAAAAAO3ZY0A";
			lastKeyDown = -1;
			chatClientIsPaused = false;
			State = new StateStub[0];
			RoomsListOrder = new Array();
			Popups = new PopupArea(View);

			#region Add dom events
			DomEvent.AddHandler(View.TextBox, "focus", new DomEventHandler(textBoxFocus));
			DomEvent.AddHandler(View.TextBox, "blur", new DomEventHandler(textBoxBlur));
			DomEvent.AddHandler(View.TextBox, "keypress", new DomEventHandler(textBoxKeyPress));
			DomEvent.AddHandler(View.TextBox, "keydown", new DomEventHandler(textBoxKeyDown));
			DomEvent.AddHandler(View.OuterMain, "keydown", new DomEventHandler(outerKeyDown));
			DomEvent.AddHandler(View.RoomsMain, "click", new DomEventHandler(roomListClick));
			DomEvent.AddHandler(View.RoomsMain, "mousedown", new DomEventHandler(roomListMouseDown));
			if (LoggedIn)
			{
				DomEvent.AddHandler(View.PrivateChatDrop, "change", new DomEventHandler(privateChatDropChange));
			}
			DomEvent.AddHandler(View.WrongSessionResumeLink, "click", new DomEventHandler(resumeLinkClick));
			DomEvent.AddHandler(View.TimeoutResumeLink, "click", new DomEventHandler(resumeLinkClick));
			DomEvent.AddHandler(View.DeleteArchiveAnchor, "click", new DomEventHandler(deleteArchive));
			#endregion

			#region Create rooms
			Rooms = new Dictionary();
			selectedRoomGuid = "";
			for (int i = 0; i < View.RoomList.ChildNodes.Length + View.RoomPrivateList.ChildNodes.Length + View.RoomGuestList.ChildNodes.Length; i++)
			{
				DOMElement child = null;
				if (i < View.RoomList.ChildNodes.Length)
					child = View.RoomList.ChildNodes[i];
				else if (i < View.RoomList.ChildNodes.Length + View.RoomPrivateList.ChildNodes.Length)
					child = View.RoomPrivateList.ChildNodes[i - View.RoomList.ChildNodes.Length];
				else
					child = View.RoomGuestList.ChildNodes[i - View.RoomList.ChildNodes.Length - View.RoomPrivateList.ChildNodes.Length];

				if (child.NodeType == DOMElementType.Element && child.ClassName.StartsWith("ChatClientRoomHolder"))
				{
					Room r = new Room(this, View);
					r.InitialiseFromElement(child, State);
					initialiseRoomEvents(r);

					if (r.Selected)
					{
						if (selectedRoomGuid.Length > 0)
							r.Selected = false;
						else
							selectedRoomGuid = r.Guid;
					}

					Rooms[r.Guid] = r;
					RoomsListOrder[RoomsListOrder.Length] = r.Guid;
					r.SetListOrder(RoomsListOrder.Length - 1);
				}
			}
			#endregion

			#region LatestTopPhoto
			if (View.InitTopPhoto.Value.Length > 0 && Rooms["AQEFAAAAAAUAAAAAvVaVmQ"] != null)
			{
				string[] topPhotoParts = View.InitTopPhoto.Value.Split(',');
				TopPhoto p = new TopPhoto(
					new TopPhotoStub(
						"", 
						ItemType.TopPhoto, 
						"", 
						"AQEFAAAAAAUAAAAAvVaVmQ",
						int.ParseInvariant(topPhotoParts[0]),
						topPhotoParts[1],
						topPhotoParts[2],
						topPhotoParts[3],
						int.ParseInvariant(topPhotoParts[4]),
						int.ParseInvariant(topPhotoParts[5]),
						topPhotoParts[6],
						int.ParseInvariant(topPhotoParts[7]),
						int.ParseInvariant(topPhotoParts[8])),
					this);
				((Room)Rooms["AQEFAAAAAAUAAAAAvVaVmQ"]).AddItem(p, null);
			}
			#endregion

			updateDraggable();

			#region Initialise server
			Server = new ServerClass(this, SessionID, LastActionTicks, State);
			Server.GotItems = new EventHandler(gotItems);
			Server.GotNoItems = new EventHandler(gotNoItems);
			Server.GotWrongSessionException = new EventHandler(gotWrongSessionException);
			Server.GotTimeoutException = new EventHandler(gotTimeoutException);
			Server.GotGenericException = new EventHandler(gotGenericException);
			Server.GotRoom = new GotRoomHandler(gotRoom);
			Server.GotNewPhotoRoom = new GotRoomHandler(gotNewPhotoRoom);
			Server.GotRoomState = new EventHandler(gotRoomState);
			Server.ShowLoadingIcon = new EventHandler(showLoadingIcon);
			Server.HideLoadingIcon = new EventHandler(hideLoadingIcon);
			Server.GotMoreInfo = new EventHandler(gotRoomMoreInfoHtml);
			Server.GotArchiveItems = new EventHandler(gotRoomArchiveItems);
			Server.DebugPrint = new EventHandler(debugEventHandler);
			Server.DoneDeleteArchive = new EventHandler(doneDeleteArchive);
			Server.Start();
			#endregion

			if (selectedRoomGuid.Length == 0 && RoomsListOrder.Length > 0)
				SelectedRoom = (Room)Rooms[(string)RoomsListOrder[0]];

			debug("Controller started successfully.");

			
		}
		#endregion

		#region initialiseRoomEvents
		void initialiseRoomEvents(Room r)
		{
			r.RoomPinAction = new EventHandler(roomPinAction);
			r.RoomStarAction = new EventHandler(roomStarAction);
			r.GetMoreInfoHtml = new EventHandler(getRoomMoreInfoHtml);
			r.GetArchiveItems = new EventHandler(getRoomArchiveItems);
			if (r.Guest)
				r.GuestRoomPinAction = new EventHandler(guestRoomPinAction);
		}
		#endregion

		#region updateDraggable()
		void updateDraggable()
		{
			//removed
			//JQueryObject roomListJq = (JQueryObject)JQueryAPI.JQuery(".ChatClientRoomPrivateList");
			//Dictionary sortableOptions = new Dictionary();
			//sortableOptions["handle"] = ".ChatClientRoomArrow";
			//sortableOptions["axis"] = "y";
			//sortableOptions["containment"] = "parent";
			//sortableOptions["update"] = new SortableUpdateDelegate(chatClientUpdateRoomOrder);
			//roomListJq.Sortable(sortableOptions, null);
		}
		#endregion

		#region chatClientUpdateRoomOrder
		public void chatClientUpdateRoomOrder(object e, JQueryObject ui)
		{
			Script.Literal("htm();");
			try
			{
				DOMElement draggedElement = (DOMElement)ui.Item[0];
				Room r = getRoomFromID(draggedElement.ID);
				if (r.Guest && !r.Pinned)
					r.Pinned = true;

				JQueryObject roomListJq = (JQueryObject)JQueryAPI.JQuery(".ChatClientRoomPrivateList");
				Dictionary seraliseOptions = new Dictionary();

				string order = (string)roomListJq.Sortable("serialize", seraliseOptions);

				string[] pairs = order.Split('&');
				for (int i = 0; i < pairs.Length; i++)
				{
					string guid = pairs[i].Split('=')[1];
					RoomsListOrder[i] = guid;
					((Room)Rooms[guid]).SetListOrder(i);
				}
				Server.StoreUpdatedRoomListOrder();

			}
			catch
			{
				debug("Serailise failed.");
			}
		}
		#endregion

		#region SelectedRoom
		Room SelectedRoom
		{
			get
			{
				if (selectedRoomGuid.Length == 0)
					return null;
				else
					return (Room)Rooms[selectedRoomGuid];
			}
			set
			{
				setSelectedRoom(value, true);
			}
		}
		string selectedRoomGuid;
		string previouslySelectedRoomGuid;
		void setSelectedRoom(Room room, bool focus)
		{
			if (chatClientIsPaused)
			{
				previouslySelectedRoomGuid = room == null ? "" : room.Guid;
				return;
			}

			selectedRoomGuid = room == null ? "" : room.Guid;

			foreach (DictionaryEntry entry in Rooms)
			{
				Room r = (Room)entry.Value;
				if (room != null && r.Guid == room.Guid)
				{
					if (!r.Selected)
					{
						r.Selected = true;
						selectedRoomGuid = r.Guid;
					}
				}
				else if (r.Selected)
				{
					r.Selected = false;
				}
			}
			View.TextBox.Style.Display = (room == null || room.ReadOnly) ? "none" : "";
			View.DeleteArchiveHolder.Style.Display = (room == null || !room.IsPrivateChatRoom) ? "none" : "";
			View.DeleteArchiveDoneLabel.Style.Display = "none";
			//Script.Literal("RepositionChatBox()");
			if (room != null && focus)
			{
				focusNow(room);
			}
		}
		#endregion

		#region focusNow
		void focusNow(Room r)
		{
			if (!r.ReadOnly && UsrK > 0)
				View.TextBox.Focus();
			else
				View.TabsChatLink.Focus();
		}
		#endregion

		#region Adding items
		#region gotItems
		void gotItems(object o, EventArgs e)
		{
			GotItemsEventArgs args = (GotItemsEventArgs)e;

			Dictionary itemTracker = new Dictionary();

			for (int i = 0; i < args.Items.Length; i++) //Newest last
			{
				Item item = args.Items[i];

				if (Rooms[item.RoomGuid] != null && (((Room)Rooms[item.RoomGuid]).Pinned || ((Room)Rooms[item.RoomGuid]).Guest))
				{
					Room r = (Room)Rooms[item.RoomGuid];

					if (item.Type == ItemType.CommentChatMessage && (item.RoomGuid == PublicStreamRoomGuid || item.RoomGuid == BuddyStreamRoomGuid))
					{
						CommentMessage c = (CommentMessage)item;
						c.ShowSubHead = true;
					}
					else if (item.Type == ItemType.LaughAlert)
					{
						Laugh l = (Laugh)item;
						l.ShowSubHead = true;
					}
					
					r.AddItem(item, itemTracker);
				}
				else if (item.Type == ItemType.PrivateChatMessage)
				{
					Private p = (Private)args.Items[i];
					if (p.UsrK != UsrK)
					{
						if (Rooms[PrivateChatRequestsRoomGuid] != null)
						{
							p.ShowChatButton = true;

							Room r = (Room)Rooms[PrivateChatRequestsRoomGuid];
							r.AddItem(item, itemTracker);
						}
					}
				}
				else if (item.Type == ItemType.CommentChatMessage && item.RoomGuid != PublicStreamRoomGuid && item.RoomGuid != BuddyStreamRoomGuid) //Don't put stuff from stream in the inbox updates room!
				{
					CommentMessage c = (CommentMessage)args.Items[i];
					if (Rooms[InboxUpdatesRoomGuid] != null && c.UsrK != UsrK)
					{
						c.ShowSubHead = true;

						Room r = (Room)Rooms[InboxUpdatesRoomGuid];
						r.AddItem(item, itemTracker);
					}
				}
				else
				{
					//just for testing... any messages received for rooms that we don't have pinned, we're gonna put in the orphans room.
					if (Rooms["AQ0FAAAAAAUAAAAA5wHGJw"] != null) //Orphans
					{
						Room r = (Room)Rooms["AQ0FAAAAAAUAAAAA5wHGJw"];
						r.AddItem(args.Items[i], itemTracker);
					}
				}
			}


			foreach (DictionaryEntry de in Rooms)
			{
				Room r = (Room)Rooms[de.Key];

				if (args.ServerRequestIndex == 0 || r.RequestIndex == 0 || itemTracker[r.Guid] != null)
				{
					r.FinaliseRequest(args.ServerRequestIndex);

					if (args.ServerRequestIndex > 0 && r.RequestIndex > 0 && itemTracker[r.Guid] != null && r.Starred && (!HasFocus || !r.Selected))
					{

						Popup p = new Popup(this, r.Name, r, (HtmlItem[])itemTracker[r.Guid]);
						if (p.HasRelevantItems)
						{
							p.ClickAction = new EventHandler(popupClickAction);
							Popups.Add(p);
						}
					}
				}
			}
		}
		#endregion
		#region gotNoItems
		void gotNoItems(object o, EventArgs e)
		{
			GotNoItemsEventArgs args = (GotNoItemsEventArgs)e;

			foreach (DictionaryEntry de in Rooms)
			{
				Room r = (Room)Rooms[de.Key];
				if (args.ServerRequestIndex == 0 || r.RequestIndex == 0)
				{
					//update stats request on all rooms...
					r.FinaliseRequest(args.ServerRequestIndex);
				}
			}
		}
		#endregion
		#region gotWrongSessionException
		void gotWrongSessionException(object o, EventArgs e)
		{
			GotExceptionEventArgs a = (GotExceptionEventArgs)e;
			previouslySelectedRoomGuid = SelectedRoom == null ? "" : SelectedRoom.Guid;
			SelectedRoom = null;
			chatClientIsPaused = true;
			View.WrongSessionHolder.Style.Display = "";
			View.TimeoutHolder.Style.Display = "none";
		}
		#endregion
		#region gotTimeoutException
		void gotTimeoutException(object o, EventArgs e)
		{
			GotExceptionEventArgs a = (GotExceptionEventArgs)e;
			previouslySelectedRoomGuid = SelectedRoom == null ? "" : SelectedRoom.Guid;
			SelectedRoom = null;
			chatClientIsPaused = true;
			View.WrongSessionHolder.Style.Display = "none";
			View.TimeoutHolder.Style.Display = "";
		}
		#endregion
		#region gotGenericException
		void gotGenericException(object o, EventArgs e)
		{
			GotExceptionEventArgs a = (GotExceptionEventArgs)e;

		//	if (a.Error.ExceptionType.EndsWith("+LoginPermissionException"))
		//		Script.Alert("Login error from the chat server... Are you logged in?");

			if (a.Error.ExceptionType.EndsWith("+SelfPrivateChatRoomException"))
				Script.Alert("Trying to have a private chat with yourself? Are you MAD?");

			if (a.Error.ExceptionType.EndsWith("+WritePermissionException"))
				Script.Alert("Error from the chat server while sending a message... Do you have permission to post into this room? If it's a group chat room, you need to be a member to chat.");

		//	if (a.Error.ExceptionType.EndsWith("+ReadPermissionException"))
		//		Script.Alert("Error from the chat server while retreving... Do you have permission to see this room?");

			if (Rooms[SystemMessagesRoomGuid] != null)
			{
				Error err = new Error(a.Error, a.Method, this, SystemMessagesRoomGuid, 0);
				((Room)Rooms[SystemMessagesRoomGuid]).AddItem(err, null);
				((Room)Rooms[SystemMessagesRoomGuid]).FinaliseRequest(1);
			}
		}
		#endregion
		#endregion

		#region popupReadButtonAction
		void popupClickAction(object o, EventArgs e)
		{
			View.TabsChatLink.Focus();

			string roomGuid = (string)o;
			PinNewRoom(roomGuid);
			
		}
		#endregion
		#region windowIsScrolledTooFarDownToSeeChat - removed
		//public bool windowIsScrolledTooFarDownToSeeChat
		//{
		//    get
		//    {
		//        int scrollOffset = Document.Body.ScrollTop;
		//        JQueryObject j = JQueryAPI.JQuery(View.MessageList);

		//        return scrollOffset > j.Offset().Top;
		//    }
		//}
		#endregion

		#region Room panel actions
		#region RoomListMouseDown
		void roomListMouseDown(DomEvent e)
		{
			Window.SetTimeout(textBoxStopWatermark, 20);
		}
		#endregion
		#region RoomListClick
		void roomListClick(DomEvent e)
		{
			if (e.Target.ID.EndsWith("PrivateChatDrop"))
				return;
			
			if (e.Target.ID.EndsWith("_Link") || e.Target.ID.EndsWith("_Icon"))
			{
				Room r = getRoomFromChildID(e.Target.ID);
				SelectedRoom = r;
				unPauseChatClient(true);
			}
			else if (e.Target.ID.EndsWith("_Cross"))
			{
				Room r = getRoomFromChildID(e.Target.ID);
				if (r.Pinned)
				{
					unPauseChatClient(false);
					SelectedRoom = null;
					r.Pinned = false;
					//r.RemoveFromRoomsList();
				}
				else
					unPauseChatClient(true);
			}
			else if (e.Target.ID.EndsWith("_Star"))
			{
				Room r = getRoomFromChildID(e.Target.ID);
				
				if (r.Starrable)
					r.Starred = !r.Starred;
				else if (r.Starred)
					Script.Alert("This room can't be un-starred");
				else
					Script.Alert("This room can't be starred");
				
				unPauseChatClient(true);
			}

			if (SelectedRoom != null)
			{
				focusNow(SelectedRoom);
			}

			e.PreventDefault();
		}
		#endregion
		#region privateChatDropChange
		void privateChatDropChange(DomEvent e)
		{
			if (View.PrivateChatDrop.Value != "0")
			{
				PinNewRoom(View.PrivateChatDrop.Value);
				View.PrivateChatDrop.SelectedIndex = 0;
			}
		}
		#endregion
		#region getRoomFromChildID
		Room getRoomFromChildID(string ID)
		{
			string[] a = ID.Split("_");
			string guid = a[a.Length - 2];
			return (Room)Rooms[guid];
		}
		#endregion
		#region getRoomFromID
		Room getRoomFromID(string ID)
		{
			string[] a = ID.Split("_");
			string guid = a[a.Length - 1];
			return (Room)Rooms[guid];
		}
		#endregion
		#region getSelectedRoomListIndex
		int getSelectedRoomListIndex()
		{
			for (int i = 0; i < RoomsListOrder.Length; i++)
			{
				if (((Room)Rooms[RoomsListOrder[i].ToString()]).Selected)
					return i;
			}
			return 0;
		}
		#endregion
		#endregion

		#region getRoomArchiveItems
		void getRoomArchiveItems(object o, EventArgs e)
		{
			if (e != null)
			{
				RoomGuidEventArgs a = (RoomGuidEventArgs)e;
				Server.GetArchiveItems(a.RoomGuid);
			}
		}
		#endregion
		#region gotRoomArchiveItems
		void gotRoomArchiveItems(object o, EventArgs e)
		{
			if (e != null)
			{
				GotArchiveItemsEventArgs a = (GotArchiveItemsEventArgs)e;
				unPauseChatClient(true);
				if (Rooms[a.RoomGuid] != null)
				{
					Room r = (Room)Rooms[a.RoomGuid];
					if (!r.Selected)
						return;

					r.ShowArchiveItems(a.ArchiveItems);
				}
			}
		}
		#endregion

		#region deleteArchive
		void deleteArchive(DomEvent e)
		{
			unPauseChatClient(true);
			Server.DeleteArchive(SelectedRoom.Guid);
			e.PreventDefault();
		}
		#endregion
		#region doneDeleteArchive
		void doneDeleteArchive(object o, EventArgs e)
		{
			if (e != null)
			{
				RoomGuidEventArgs a = (RoomGuidEventArgs)e;
				unPauseChatClient(true);
				if (Rooms[a.RoomGuid] != null)
				{
					Room r = (Room)Rooms[a.RoomGuid];
					r.ClearItems();
					View.DeleteArchiveDoneLabel.Style.Display = "";
					Window.SetTimeout(delegate { View.DeleteArchiveDoneLabel.Style.Display = "none"; }, 2000);
					
				}
			}
		}
		#endregion

		#region MoreInfoClick
		void getRoomMoreInfoHtml(object o, EventArgs e)
		{
			if (e != null)
			{
				RoomGuidEventArgs a = (RoomGuidEventArgs)e;
				Server.GetMoreInfo(a.RoomGuid);
			}
		}
		void gotRoomMoreInfoHtml(object o, EventArgs e)
		{
			if (e != null)
			{
				GotMoreInfoEventArgs a = (GotMoreInfoEventArgs)e;
				unPauseChatClient(true);
				if (Rooms[a.RoomGuid] != null)
				{
					Room r = (Room)Rooms[a.RoomGuid];
					if (!r.Selected)
						return;

					r.StoreMoreInfoHtmlAndShowMoreInfo(a.MoreInfoHtml);
				}
			}
		}
		void moreInfoClick(DomEvent e)
		{
			unPauseChatClient(true);

			if (SelectedRoom != null && !SelectedRoom.MoreInfoVisible)
				SelectedRoom.ShowMoreInfo(false);

			e.PreventDefault();
		}
		//void messagesHeaderClick(DomEvent e)
		//{
		//    unPauseChatClient(true);

		//    if (SelectedRoom != null && SelectedRoom.MoreInfoVisible)
		//        SelectedRoom.HideMoreInfo(true);

		//    e.PreventDefault();
		//}
		
		#endregion

		#region Resume after pause
		void resumeLinkClick(DomEvent e)
		{
			unPauseChatClient(true);
			e.PreventDefault();
		}
		void unPauseChatClient(bool selectPreviousRoom)
		{
			if (chatClientIsPaused)
			{
				chatClientIsPaused = false;

				View.WrongSessionHolder.Style.Display = "none";
				View.TimeoutHolder.Style.Display = "none";

				if (selectPreviousRoom && previouslySelectedRoomGuid.Length > 0)
					SelectedRoom = (Room)Rooms[previouslySelectedRoomGuid];

				Server.ResumeAfterPause();
			}
		}
		#endregion
		#region gotRoomState
		void gotRoomState(object o, EventArgs e)
		{
			GotRoomStateEventArgs a = (GotRoomStateEventArgs)e;
			
			if (a.RoomState == null)
				return;

			for (int i = 0; i < a.RoomState.Length; i++)
			{
				StateStub ss = (StateStub)a.RoomState[i];

				object ob = Rooms[ss.guid];
				if (ob != null)
				{
					Room r = (Room)ob;
					r.UpdateStatsAfterUnPause(ss);
				}

			}
		}
		#endregion

		#region ChangePhoto
		public void ChangePhoto(string newRoomGuid)
		{
			unPauseChatClient(false);
			object ob = Rooms[newRoomGuid];
			if (ob == null)
			{
				Server.SwitchPhotoRoom(newRoomGuid);
			}
			else
			{
				bool newRoomShouldBeSelected = removeAllUnPinnedGuestPhotoRoomsExceptSpecified(newRoomGuid);

				Room r = (Room)ob;
				if (newRoomShouldBeSelected)
					setSelectedRoom(r, false);
			}
		}
		void removeRoom(string roomGuid)
		{
			Room r = (Room)Rooms[roomGuid];
			r.PrepareForRemoval();
			Rooms.Remove(roomGuid);
			Array newRoomsListOrder = new Array();
			for (int i = 0; i < RoomsListOrder.Length; i++)
			{
				if (roomGuid != (string)RoomsListOrder[i])
					newRoomsListOrder[newRoomsListOrder.Length] = RoomsListOrder[i];
			}
			RoomsListOrder = newRoomsListOrder;
		}
		bool removeAllUnPinnedGuestPhotoRoomsExceptSpecified(string exceptThisRoomGuid)
		{
			bool oneIsSelected = false;
			//remove all guest photo rooms...
			foreach (DictionaryEntry entry in Rooms)
			{
				Room r = (Room)entry.Value;
				if (r.IsPhotoChatRoom && r.Guest && r.Guid != exceptThisRoomGuid && !r.Pinned)
				{
					if (r.Selected)
						oneIsSelected = true;
					removeRoom(r.Guid);
				}
			}
			return oneIsSelected;
		}
		bool gotNewPhotoRoom(object o, EventArgs e)
		{
			GotRoomEventArgs a = (GotRoomEventArgs)e;

			bool newRoomShouldBeSelected = removeAllUnPinnedGuestPhotoRoomsExceptSpecified(a.RoomStub.guid);
			
			object ob = Rooms[a.RoomStub.guid];
			if (ob == null)
			{
				Room r = new Room(this, View);
				r.InitialiseFromStub(a.RoomStub, a.RoomStub.isPrivateChatRoom ? View.RoomPrivateList : a.RoomStub.guest ? View.RoomGuestList : View.RoomList, State);
				initialiseRoomEvents(r);

				Rooms[r.Guid] = r;
				RoomsListOrder[RoomsListOrder.Length] = r.Guid;
				r.SetListOrder(RoomsListOrder.Length - 1);

				if (newRoomShouldBeSelected)
					setSelectedRoom(r, false);

				updateDraggable();
				updateRoomUI();

				return false;
			}
			else
			{
				Room r = (Room)ob;
				
				if (newRoomShouldBeSelected)
					setSelectedRoom(r, false);
				
				return true;
			}
		}
		#endregion

		#region Adding rooms
		public void PinNewRoom(string newRoomGuid)
		{
			unPauseChatClient(false);
			View.TabsChatLink.Focus();
			object ob = Rooms[newRoomGuid];
			if (ob == null)
			{
				Server.PinRoom(newRoomGuid);
			}
			else
			{
				Room r = (Room)ob;
				if (!r.Pinned)
					r.Pinned = true;

				SelectedRoom = r;
			}
		}
		void roomPinAction(object o, EventArgs e)
		{
			PinActionEventArgs a = (PinActionEventArgs)e;
			if (a.Pinned)
				Server.RePinRoom(a.RoomGuid);
			else
				Server.UnPinRoom(a.RoomGuid);
		}
		void roomStarAction(object o, EventArgs e)
		{
			StarActionEventArgs a = (StarActionEventArgs)e;
			Server.StarRoom(a.RoomGuid, a.Starred);
		}
		void guestRoomPinAction(object o, EventArgs e)
		{
			PinActionEventArgs a = (PinActionEventArgs)e;
			Room r = (Room)Rooms[a.RoomGuid];
			if (r != null && r.Guest)
			{
				r.RemoveFromRoomsList();
				r.AddToRoomsList(!r.Pinned ? View.RoomGuestList : r.IsPrivateChatRoom ? View.RoomPrivateList : View.RoomList);
				r.UpdateUIAfterGuestPinAction();
			}
			updateRoomUI();
		}
		void updateRoomUI()
		{
			bool hasGuestRooms = false;
			bool hasPrivateRooms = false;
			foreach (DictionaryEntry entry in Rooms)
			{
				Room r = (Room)entry.Value;

				if (r.Guest && !r.Pinned)
					hasGuestRooms = true;

				if (r.IsPrivateChatRoom && r.Pinned)
					hasPrivateRooms = true;

				if (hasGuestRooms && hasPrivateRooms)
					break;
			}

			updateRoomPrivateListVisibility(hasPrivateRooms);

			updateRoomGuestListVisibility(hasGuestRooms);
		}
		bool gotRoom(object o, EventArgs e)
		{
			GotRoomEventArgs a = (GotRoomEventArgs)e;

			object ob = Rooms[a.RoomStub.guid];
			if (ob == null)
			{
				Room r = new Room(this, View);
				r.InitialiseFromStub(a.RoomStub, a.RoomStub.guest ? View.RoomGuestList : a.RoomStub.isPrivateChatRoom ? View.RoomPrivateList : View.RoomList, State);
				initialiseRoomEvents(r);

				Rooms[r.Guid] = r;
				RoomsListOrder[RoomsListOrder.Length] = r.Guid;
				r.SetListOrder(RoomsListOrder.Length - 1);

				SelectedRoom = r;

				updateDraggable();

				if (a.RoomStub.guest)
					updateRoomGuestListVisibility(true);
				else if (a.RoomStub.isPrivateChatRoom)
					updateRoomPrivateListVisibility(true);

				return false;
			}
			else
			{
				Room r = (Room)ob;
				if (!r.Pinned)
					r.Pinned = true;
				SelectedRoom = r;
				return true;
			}

		}
		void updateRoomGuestListVisibility(bool hasGuestRooms)
		{
			View.RoomGuestListDivider.Style.Display = hasGuestRooms ? "" : "none";
			View.RoomGuestList.Style.Display = hasGuestRooms ? "" : "none";
		}
		void updateRoomPrivateListVisibility(bool hasPrivateRooms)
		{
			//View.RoomPrivateListDivider.Style.Display = hasPrivateRooms ? "" : "none";
			View.RoomPrivateList.Style.Display = hasPrivateRooms ? "" : "none";
		}
		#endregion

		#region GetTestPopup
		//public Popup GetTestPopup(int id)
		//{
		//    return new Popup(this, "Message " + id.ToString(), (Room)Rooms["AQEFAAAAAAUAAAAAvVaVmQ"], null);
		//}
		#endregion

		#region Key press event handlers
		#region TextBoxKeyDown
		void textBoxKeyDown(DomEvent e)
		{
			lastKeyDown = e.KeyCode;
			if (e.KeyCode == (int)Key.Esc)
			{
				unPauseChatClient(true);
				View.TextBox.Value = "";
			}
			else if (e.KeyCode == (int)Key.Enter)
			{
				if (SelectedRoom.Guest && !SelectedRoom.Pinned)
					SelectedRoom.Pinned = true; //This involves a round-trip to the server, so we only hold up the message post with it if we need to (guest rooms should be pinned before posting)

				//post message
				if (View.TextBox.Value.Trim().Length > 0)
				{
					unPauseChatClient(true);
					Login.WhenLoggedIn(
						new Action(
							delegate()
							{
								Server.SendMessage(View.TextBox.Value, selectedRoomGuid);
								View.TextBox.Value = "";
							}
						)
					);
				}
				else
					unPauseChatClient(true);

				if (!SelectedRoom.Pinned)
					SelectedRoom.Pinned = true;

				e.PreventDefault();
			}

		}
		#endregion
		#region OuterKeyDown
		void outerKeyDown(DomEvent e)
		{

			if (e.KeyCode == (int)Key.Up || e.KeyCode == (int)Key.Down)
			{
				int selectedRoomIndex = getSelectedRoomListIndex();

				Room newRoom = null;
				int count = 0;
				while ((count <= RoomsListOrder.Length + 1) && (newRoom == null || !newRoom.Pinned || newRoom.Guid == PublicStreamRoomGuid))
				{
					if (e.KeyCode == (int)Key.Up)
					{
						if (selectedRoomIndex == 0)
							selectedRoomIndex = RoomsListOrder.Length;

						selectedRoomIndex--;
					}
					else if (e.KeyCode == (int)Key.Down)
					{
						if (selectedRoomIndex == RoomsListOrder.Length - 1)
							selectedRoomIndex = -1;

						selectedRoomIndex++;
					}

					newRoom = (Room)Rooms[RoomsListOrder[selectedRoomIndex].ToString()];
					count++;
				}
				SelectedRoom = newRoom;

				unPauseChatClient(true);
				e.PreventDefault();

			}
		}
		#endregion
		#region TextBoxKeyPress
		void textBoxKeyPress(DomEvent e)
		{
			if (lastKeyDown == (int)Key.Enter)
			{
				e.PreventDefault(); // Needed to prevent Opera postback (DOESN'T WORK!!!)
			}
		}
		#endregion
		#endregion
		#region Watermark
		bool addWatermark = false;
		void textBoxFocus(DomEvent e)
		{
			if (!attributeExists(View.TextBox, "readonly"))
			{
				HasFocus = true;
				addWatermark = false;
				if (View.TextBox.Value == "Enter your message here...")
				{
					View.TextBox.Value = "";
				}
				View.TextBox.ClassName = "ChatClientTextBox";
			}
		}
		void textBoxBlur(DomEvent e)
		{
			if (!attributeExists(View.TextBox, "readonly"))
			{
				HasFocus = false;
				addWatermark = true;
				Window.SetTimeout(textBoxAddWatermark, 50);
			}
		}
		bool attributeExists(DOMElement el, string attributeName)
		{
			DOMAttribute d = null;

			try
			{
				d = el.Attributes.GetNamedItem(attributeName);
			}
			catch
			{
				return false;
			}

			if (d == null)
				return false;
			
			try
			{
				if (!d.Specified)
					return false;
			}
			catch
			{
				return false;
			}

			return true;
		}
		void textBoxAddWatermark()
		{
			if (View.TextBox.Value == "" && addWatermark)
			{
				View.TextBox.Value = "Enter your message here...";
				View.TextBox.ClassName = "ChatClientTextBoxWatermark";
			}
		}
		void textBoxStopWatermark()
		{
			addWatermark = false;
		}
		#endregion

		#region roomsMoreLinkFocus
		void tabsChatLinkFocus(DomEvent e)
		{
			HasFocus = true;
		}
		#endregion
		#region roomsMoreLinkBlur
		void tabsChatLinkBlur(DomEvent e)
		{
			HasFocus = false;
		}
		#endregion

		#region loadingIcon
		private ImageElement loadingIcon = null;
		void hideLoadingIcon(object o, EventArgs e)
		{
			if (loadingIcon != null)
			{
				loadingIcon.Style.Display = "none";
			}
		}
		void showLoadingIcon(object o, EventArgs e)
		{
			if (View.TextBox.Style.Display == "")
			{
				if (loadingIcon == null)
				{
					loadingIcon = (ImageElement)Document.CreateElement("img");
					loadingIcon.Src = "/gfx/autocomplete-loading.gif";
					loadingIcon.Style.Height = "16px";
					loadingIcon.Style.Width = "16px";
					loadingIcon.Style.Position = "absolute";
					loadingIcon.Style.ZIndex = 200;
					Document.Body.AppendChild(loadingIcon);
				}
				Offset offset = JQueryAPI.JQuery(View.TextBox).Offset();
				loadingIcon.Style.Left = (offset.Left + View.TextBox.ClientWidth - 20) + "px";
				loadingIcon.Style.Top = (offset.Top + 2) + "px";
				loadingIcon.Style.Display = "";
			}
		}
		#endregion

		#region debug
		void debug(string text)
		{
			if (Rooms[SystemMessagesRoomGuid] != null)
			{
				try
				{
					Note n = new Note(text.Replace("<", "&lt;"), this, SystemMessagesRoomGuid, 0);
					((Room)Rooms[SystemMessagesRoomGuid]).AddItem(n, null);
					((Room)Rooms[SystemMessagesRoomGuid]).FinaliseRequest(1);
				}
				catch
				{
					Note n = new Note("<small>NULL</small>", this, SystemMessagesRoomGuid, 0);
					((Room)Rooms[SystemMessagesRoomGuid]).AddItem(n, null);
					((Room)Rooms[SystemMessagesRoomGuid]).FinaliseRequest(1);
				}
			}

		}
		void debugEventHandler(object o, EventArgs e)
		{
			DebugPrintEventArgs a = (DebugPrintEventArgs)e;
			debug(a.Html);
		}
		
		void debugObject(object o)
		{
			Dictionary d = Dictionary.GetDictionary(0);
			foreach (DictionaryEntry de in d)
			{
				debug(de.Key + ": " + de.Value.ToString() + "<br /><br />"); 
			}
		}
		#endregion

	}

	
	[GlobalMethods]
	public static class PageImplementation
	{
		#region ChatClientPinRoom
		public static void ChatClientPinRoom(string roomGuid, object transferSelector, bool transfer)
		{
			ChatClientController.Instance.PinNewRoom(roomGuid);
			if (transfer)
			{
				try
				{
					Dictionary options = new Dictionary();
					options["to"] = "#ChatClient_MessagesMain";
					JQueryAPI.JQuery(transferSelector).effect("transfer", options, 500, null);
				}
				catch (Exception ex)
				{
				}
			}
		}
		#endregion
		#region ChatClientChangePhoto
		public static void ChatClientChangePhoto(string photoRoomGuid)
		{
			ChatClientController.Instance.ChangePhoto(photoRoomGuid);
		}
		#endregion
	}

}



