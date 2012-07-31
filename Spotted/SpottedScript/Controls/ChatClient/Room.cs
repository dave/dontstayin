using System.DHTML;
using SpottedScript.Controls.ChatClient.Shared;
using Sys;
using SpottedScript.Controls.ChatClient.Items;
using System;
using HtmlItem = SpottedScript.Controls.ChatClient.Items.Html;
using Sys.UI;


namespace SpottedScript.Controls.ChatClient
{
	public class Room
	{
		#region Members
		Controller Parent;
		DOMElement OuterElement;
		DOMElement RoomElement;
		DOMElement LinkElement;
		//DOMElement ArrowElement;
		DOMElement CrossElement;
		DOMElement PresenceElement;
		DOMElement TotalElement;
		DOMElement StatsSeperatorElement;
		DOMElement UnreadElement;
		public string Guid;
		public string Name;
		public string Url;
		public bool Pinable;
		public bool Starrable;
		public bool ReadOnly;
		RoomStub stub;
		StateStub state;
		RoomHtml html;
		bool elementsInitialised;
		Item[] Items;
		DOMElement MesssagesElementHolder;
		DOMElement MesssagesElement;
		DOMElement MoreInfoElement = null;
		bool needsNewStatusUpdate;
		bool doneFullUpdateOfNewStatus;
		public EventHandler RoomPinAction = null;
		public EventHandler RoomStarAction = null;
		public EventHandler GuestRoomPinAction = null;
		public EventHandler GetMoreInfoHtml = null;
		public EventHandler GetArchiveItems = null;
		public int RequestIndex;
		public bool IsPhotoChatRoom;
		public bool IsPrivateChatRoom;
		public bool IsNewPhotoAlertsRoom;
		public PresenceState Presence;
		public string Icon;
		string tokenDateTimeTicks;
		string token;
		bool LoggedIn;
		bool onlyRenderItemsWhenSelected;
		int onlyRenderItemsWhenSelectedMaxItems;
		
		bool hasTopPhoto;
		TopPhoto topPhoto;
		DOMElement topPhotoHolder;
		ImageElement topPhotoImage;
		AnchorElement topPhotoAnchor;

		bool hasChatPic;
		string chatPic;
		string chatPicStmuParams;
		string chatPicUrl;
		DOMElement chatPicHolder;
		ImageElement chatPicImage;
		AnchorElement chatPicAnchor;
		HtmlItem latestHtmlItem;

		bool haveCheckedArchive;
		bool hasArchive;
		bool hasArchiveItems;
		bool hiddenFromRoomList;
		bool isStreamRoom;
		
		#region Selected
		public bool Selected
		{
			get
			{
				return state.selected;
			}
			set
			{
				if (state.selected != value)
				{
					state.selected = value;

					if (state.selected)
					{
						resetItemsOnSelect();
						if (!hasArchive || (haveCheckedArchive && !hasArchiveItems))
							showMoreInfoAfterDelayIfNoMessages();
						else
							getArchiveAfterDelayIfLessThan20Messages();
						

						if (onlyRenderItemsWhenSelected)
							onlyRenderItemsWhenSelectedRenderItemsNow();
					}
					else
					{
						moreInfoVisible = false;
					}

					updateUI();

				}
			}
		}
		void resetItemsOnSelect()
		{
			LatestItemSeen = LatestItem;
			NewMessages = 0;
		}
		#endregion
		#region Pinned
		public bool Pinned
		{
			get
			{
				if (Pinable)
					return pinned;
				else
					return true;

			}
			set
			{
				if (Pinable)
				{
					if (pinned != value)
					{
						pinned = value;

						if (RoomPinAction != null)
							RoomPinAction(this, new PinActionEventArgs(Guid, pinned));

						if (Guest)
						{
							if (GuestRoomPinAction != null)
								GuestRoomPinAction(this, new PinActionEventArgs(Guid, pinned));
						}

						if (pinned)
						{
							RequestIndex = 0;
						}
						else
						{
							starred = isStarredByDefault;
						}

						if (!pinned && !Guest)
							ClearItems();

						updateUI();
					}
				}
			}
		}
		bool pinned;
		#endregion
		#region Starred
		public bool Starred
		{
			get
			{
				return starred;
			}
			set
			{
				if (starred != value)
				{
					starred = value;

					if (RoomStarAction != null)
						RoomStarAction(this, new StarActionEventArgs(Guid, starred));
					
					updateUI();
				}
			}
		}
		bool starred;
		bool isStarredByDefault;
		#endregion
		#region Guest
		public bool Guest
		{
			get
			{
				return state.guest;
			}
			set
			{
				state.guest = value;
			}
		}
		#endregion
		#region NewMessages
		int NewMessages
		{
			get
			{
				return state.newMessages;
			}
			set
			{
				state.newMessages = value;
			}
		}
		#endregion
		#region TotalMessages
		int TotalMessages
		{
			get
			{
				return state.totalMessages;
			}
			set
			{
				state.totalMessages = value;
			}
		}
		#endregion
		#region LatestItem
		string LatestItem
		{
			get
			{
				return state.latestItem;
			}
			set
			{
				state.latestItem = value;
			}
		}
		#endregion
		#region LatestItemSeen
		string LatestItemSeen
		{
			get
			{
				return state.latestItemSeen;
			}
			set
			{
				state.latestItemSeen = value;
			}
		}
		#endregion
		#region LatestItemOld
		string LatestItemOld
		{
			get
			{
				return state.latestItemOld;
			}
			set
			{
				state.latestItemOld = value;
			}
		}
		#endregion
		#region ItemCount
		public int ItemCount
		{
			get
			{
				return Items == null ? 0 : Items.Length;
			}
		}
		#endregion
		#endregion

		#region Initialisation
		#region public Room(Controller parent, View view)
		public Room(Controller parent, View view)
		{
			Parent = parent;
			Items = (Item[])new Array();

			LoggedIn = parent.LoggedIn;

			elementsInitialised = false;
			needsNewStatusUpdate = false;
			doneFullUpdateOfNewStatus = false;
			
			MesssagesElementHolder = Document.CreateElement("div");
			MesssagesElementHolder.Style.Display = "none";
			view.MessageList.AppendChild(MesssagesElementHolder);

		//	if (this.Guid == parent.PublicStreamRoomGuid)
		//	{
		//		Script.Alert("foo");
		//		MesssagesElement = view.StreamList;
		//	}
		//	else
		//	{
				MesssagesElement = Document.CreateElement("div");
				MesssagesElementHolder.AppendChild(MesssagesElement);
		//	}

			MoreInfoElement = Document.CreateElement("div");
			MoreInfoElement.ClassName = "ChatClientMessageListHiddenHolder";
			view.MessageList.AppendChild(MoreInfoElement);

			RequestIndex = 0;

		}
		#endregion
		#region public void InitialiseFromElement(DOMElement e)
		public void InitialiseFromElement(DOMElement e, StateStub[] controllerStateStore)
		{
			Guid = e.Attributes.GetNamedItem("roomGuid").Value;
			Name = e.Attributes.GetNamedItem("roomName").Value;
			Url = e.Attributes.GetNamedItem("roomUrl").Value;
			Pinable = bool.Parse(e.Attributes.GetNamedItem("roomPinable").Value);
			pinned = bool.Parse(e.Attributes.GetNamedItem("roomPinned").Value);
			starred = bool.Parse(e.Attributes.GetNamedItem("roomStarred").Value);
			Starrable = bool.Parse(e.Attributes.GetNamedItem("roomStarrable").Value);
			isStarredByDefault = bool.Parse(e.Attributes.GetNamedItem("roomIsStarredByDefault").Value);
			ReadOnly = bool.Parse(e.Attributes.GetNamedItem("roomReadOnly").Value);
			IsPhotoChatRoom = bool.Parse(e.Attributes.GetNamedItem("roomIsPhotoChatRoom").Value);
			IsPrivateChatRoom = bool.Parse(e.Attributes.GetNamedItem("roomIsPrivateChatRoom").Value);
			IsNewPhotoAlertsRoom = bool.Parse(e.Attributes.GetNamedItem("roomIsNewPhotoAlertsRoom").Value);
			Presence = (PresenceState)int.ParseInvariant(e.Attributes.GetNamedItem("roomPresence").Value);

			Icon = e.Attributes.GetNamedItem("roomIcon").Value;
			tokenDateTimeTicks = e.Attributes.GetNamedItem("roomTokenDateTimeTicks").Value;
			token = e.Attributes.GetNamedItem("roomToken").Value;
			hasArchive = bool.Parse(e.Attributes.GetNamedItem("roomHasArchive").Value);

			hiddenFromRoomList = bool.Parse(e.Attributes.GetNamedItem("roomHiddenFromRoomList").Value);
			isStreamRoom = bool.Parse(e.Attributes.GetNamedItem("roomisStreamRoom").Value);

			state = new StateStub();
			state.Initialise(
				Guid,
				bool.Parse(e.Attributes.GetNamedItem("roomSelected").Value),
				bool.Parse(e.Attributes.GetNamedItem("roomGuest").Value), 
				int.ParseInvariant(e.Attributes.GetNamedItem("roomNewMessages").Value), 
				int.ParseInvariant(e.Attributes.GetNamedItem("roomTotalMessages").Value), 
				e.Attributes.GetNamedItem("roomLatestItem").Value, 
				e.Attributes.GetNamedItem("roomLatestItemSeen").Value,
				e.Attributes.GetNamedItem("roomLatestItemOld").Value,
				int.ParseInvariant(e.Attributes.GetNamedItem("roomListOrder").Value),
				tokenDateTimeTicks,
				token);

			addToStateStoreIfNotAlreadyThere(controllerStateStore, state);

			stub = new RoomStub(
				Parent.ClientID, 
				Guid, 
				Name, 
				Url,
				Pinned,
				Starred,
				isStarredByDefault,
				Pinable,
				Starrable,
				Selected,
				Guest, 
				NewMessages, 
				TotalMessages, 
				LatestItem, 
				LatestItemSeen, 
				LatestItemOld,
				ReadOnly,
				state.listOrder,
				IsPhotoChatRoom,
				IsPrivateChatRoom,
				IsNewPhotoAlertsRoom,
				Presence,
				Icon,
				tokenDateTimeTicks,
				token,
				hasArchive,
				hiddenFromRoomList,
				isStreamRoom);
			
			html = new RoomHtml(stub, LoggedIn);

			InitialiseElements("");

			genericInitialise();
		}
		#endregion
		#region public void InitialiseFromStub(RoomStub r)
		public void InitialiseFromStub(RoomStub roomStub, DOMElement roomList, StateStub[] controllerStateStore)
		{
			stub = roomStub;

			Guid = stub.guid;
			Name = stub.name;
			Url = stub.url;
			Pinable = stub.pinable;
			pinned = stub.pinned;
			starred = stub.starred;
			Starrable = stub.starrable;
			isStarredByDefault = stub.isStarredByDefault;
			ReadOnly = stub.readOnly;
			IsPhotoChatRoom = stub.isPhotoChatRoom;
			IsPrivateChatRoom = stub.isPrivateChatRoom;
			IsNewPhotoAlertsRoom = stub.isNewPhotoAlertsRoom;
			Presence = stub.presence;
			tokenDateTimeTicks = stub.tokenDateTimeTicks;
			token = stub.token;
			hasArchive = stub.hasArchive;


			state = new StateStub();
			state.Initialise(
				stub.guid, 
				stub.selected,
				stub.guest,
				stub.newMessages,
				stub.totalMessages,
				stub.latestItem,
				stub.latestItemSeen,
				stub.latestItemOld,
				stub.listOrder,
				stub.tokenDateTimeTicks,
				stub.token);

			addToStateStoreIfNotAlreadyThere(controllerStateStore, state);

			html = new RoomHtml(stub, LoggedIn);

			string outerClientID = roomStub.parentClientID + "_Room_" + Guid + "_Outer";
			DOMElement newNode = Document.CreateElement("span");
			newNode.ID = outerClientID;
			newNode.InnerHTML = html.ToHtml();
			roomList.AppendChild(newNode.FirstChild);

			InitialiseElements(outerClientID);

			genericInitialise();
		}
		#endregion
		#region genericInitialise
		void genericInitialise()
		{
			if (IsNewPhotoAlertsRoom)
			{
				onlyRenderItemsWhenSelected = true;
				onlyRenderItemsWhenSelectedMaxItems = 10;
			}
			else
			{
				onlyRenderItemsWhenSelected = false;
				onlyRenderItemsWhenSelectedMaxItems = 0;
			}
		}
		#endregion
		#region replaceOrAddToStateStore
		void addToStateStoreIfNotAlreadyThere(StateStub[] stateStore, StateStub state)
		{
			for (int k = 0; k < stateStore.Length; k++)
			{
				StateStub ss = (StateStub)stateStore[k];
				if (ss.guid == state.guid)
					return;
			}
			stateStore[stateStore.Length] = state;
		}
		#endregion
		#endregion
		
		#region RemoveFromRoomsList()
		public void RemoveFromRoomsList()
		{
			if (OuterElement != null)
				OuterElement.ParentNode.RemoveChild(OuterElement);
			else
				RoomElement.ParentNode.RemoveChild(RoomElement);
		}
		#endregion
		#region AddToRoomsList
		public void AddToRoomsList(DOMElement parent)
		{
			parent.AppendChild(RoomElement);
		}
		#endregion
		#region PrepareForRemoval()
		public void PrepareForRemoval()
		{
			RemoveFromRoomsList();
			MesssagesElementHolder.ParentNode.RemoveChild(MesssagesElementHolder);
			MoreInfoElement.ParentNode.RemoveChild(MoreInfoElement);
		}
		#endregion
		#region SetListOrder
		public void SetListOrder(int listOrder)
		{
			state.listOrder = listOrder;
		}
		#endregion
		#region UpdateUIAfterGuestPinAction
		public void UpdateUIAfterGuestPinAction()
		{
			updateUI();
		}
		#endregion

		#region Adding items
		#region public void AddItem(Item item)
		public bool AddItem(Item item, Dictionary itemTracker)
		{
			if (Guest && !item.FromGuestRefresh)
				return false;

			if (!Guest && !Pinned)
				return false;

			LatestItem = item.Guid;

			if (item is HtmlItem)
			{
				latestHtmlItem = (HtmlItem)item;

				if (!isStreamRoom)
				{
					if (item is Newable)
						needsNewStatusUpdate = true;

					if (item.ServerRequestIndex > 0)
					{
						if (item is Newable)
							((Newable)item).IsInNewSection = true;
					}
				}
				else
				{
					if (item is Newable)
						((Newable)item).IsInNewSection = false;
				}


				if (!onlyRenderItemsWhenSelected)
				{
					renderItemToHtmlNow((HtmlItem)item, true);
				}

				if (itemTracker != null)
				{
					if (itemTracker[Guid] == null)
						itemTracker[Guid] = new Array();

					HtmlItem[] roomTracker = (HtmlItem[])itemTracker[Guid];
					roomTracker[roomTracker.Length] = (HtmlItem)item;
				}

				if (hideMoreInfoOnNextReceivedItem && MoreInfoVisible)
					HideMoreInfo(false);
			}
			else if (item is TopPhoto)
			{
				hasTopPhoto = true;
				topPhoto = (TopPhoto)item;
				updateUI();
			}

			Items[Items.Length] = item;

			return true;
		}
		#endregion
		#region public void FinaliseRequest
		public void FinaliseRequest(int serverRequestIndex)
		{
			if (onlyRenderItemsWhenSelected && Selected)
				onlyRenderItemsWhenSelectedRenderItemsNow();

			#region //update LatestItem, LatestItemSeen, LatestItemOld
			//LatestItem - this is the latest item added to this group. It's always kept up-to-date by the AddItem function.
			//LatestItemSeen - this is the latest item that's been seen by the user. It's updated in this function, but only when the item list is visible.
			//LatestItemOld - this is the latest item that should be un-hilighted in the list.

			//if (serverRequestIndex == 0 || RequestIndex == 0) removed this to fix bug where initial message posted in photo room leaves the room with 1 new message. Will probably break other stuff!
			//if (serverRequestIndex == 0) put back in in conjuction with always firing this on the 1st request of the room...
			if (serverRequestIndex == 0 || RequestIndex == 0)
			{
				if (LatestItemSeen != LatestItem)
				{
					LatestItemOld = LatestItemSeen;

					if (SelectedAndMessagesVisible)
					{
						LatestItemSeen = LatestItem;
					}
				}
			}
			else
			{
				if (SelectedAndMessagesVisible)
				{
					LatestItemOld = LatestItemSeen;
					LatestItemSeen = LatestItem;
				}
				else
				{
					if (LatestItemOld != LatestItemSeen)
					{
						LatestItemOld = LatestItemSeen;
					}
				}
			}
			#endregion

			#region removed
			//if (RequestIndex == 0 && SelectedAndMessagesVisible) // If we're on the 1st request of this room, and we only have new messages, we show them all as read.
			//{
			//    bool foundOldMessage = false;
			//    for (int i = Items.Length - 1; i >= 0; i--)
			//    {
			//        Newable n = (Newable)Items[i];
			//        if (LatestItemOld == n.Guid)
			//            foundOldMessage = true;
			//    }
			//    if (!foundOldMessage)
			//    {
			//        LatestItemOld = LatestItemSeen;
			//    }
			//}
			#endregion

			if (serverRequestIndex == 0 && Selected)
			{
				if (!hasArchive || (haveCheckedArchive && !hasArchiveItems))
					showMoreInfoAfterDelayIfNoMessages();
				else
					getArchiveAfterDelayIfLessThan20Messages();
			}

			if (latestHtmlItem != null)
			{
				if (latestHtmlItem is Message)
				{
					Message m = (Message)latestHtmlItem;

					hasChatPic = true;
					chatPic = m.AnyChatPic;
					chatPicStmuParams = m.StmuParams;
					chatPicUrl = "/members/" + m.NickName.ToLowerCase();

				}
				else
					hasChatPic = false;
			}
			else
				hasChatPic = false;

			updateNewStatus();

			updateUI();

			RequestIndex++;
		}
		#endregion
		#region renderItemToHtmlNow
		void renderItemToHtmlNow(HtmlItem item, bool insertAtTop)
		{
			DOMElement newNode = Document.CreateElement("span");
			newNode.InnerHTML = ((HtmlItem)item).ToHtml();

			DOMElement messageList = isStreamRoom ? Parent.StreamList : MesssagesElement;

			if (messageList.HasChildNodes() && insertAtTop)
				messageList.InsertBefore(newNode, messageList.ChildNodes[0]);
			else
				messageList.AppendChild(newNode);

			((HtmlItem)item).InitialiseElements();
		}
		#endregion
		#region onlyRenderItemsWhenSelectedRenderItemsNow
		string onlyRenderItemsWhenSelectedLatestItemWhenLastRefreshed;
		void onlyRenderItemsWhenSelectedRenderItemsNow()
		{
			if (onlyRenderItemsWhenSelectedLatestItemWhenLastRefreshed != LatestItem)
			{
				onlyRenderItemsWhenSelectedLatestItemWhenLastRefreshed = LatestItem;

				//removeAllChildren(MesssagesElement);
				MesssagesElement.InnerHTML = "";

				int htmlItemCount = 0;
				for (int i = Items.Length - 1; i >= 0; i--)
				{
					Item item = Items[i];
					if (item is HtmlItem)
					{
						htmlItemCount++;

						renderItemToHtmlNow((HtmlItem)item, false);

						if (onlyRenderItemsWhenSelectedMaxItems > 0 && htmlItemCount >= onlyRenderItemsWhenSelectedMaxItems)
							break;
					}
				}
			}
		}
		#endregion
		#endregion

		#region ClearItems
		public void ClearItems()
		{
			Items = (Item[])new Array();
			MesssagesElement.InnerHTML = "";
			hasChatPic = false;
			updateUI();
		}
		#endregion

		#region SelectedAndMessagesVisible
		public bool SelectedAndMessagesVisible
		{
			get
			{
				return Selected && !MoreInfoVisible;
			}
		}
		#endregion

		#region MoreInfoVisible
		public bool MoreInfoVisible
		{
			get
			{
				return moreInfoVisible;
			}
		}
		bool moreInfoVisible;
		#endregion
		#region ShowMoreInfo
		public void ShowMoreInfo(bool hideMoreInfoOnNextReceivedItem)
		{
			this.hideMoreInfoOnNextReceivedItem = hideMoreInfoOnNextReceivedItem;
			if (MoreInfoElement.InnerHTML.Length > 0)
			{
				moreInfoVisible = true;
				updateUI();
			}
			else
			{
				if (GetMoreInfoHtml != null)
					GetMoreInfoHtml(this, new RoomGuidEventArgs(this.Guid));
			}
		}
		bool hideMoreInfoOnNextReceivedItem;
		#endregion
		#region StoreMoreInfoHtmlAndShowMoreInfo
		public void StoreMoreInfoHtmlAndShowMoreInfo(string moreInfoHtml)
		{
			if (moreInfoHtml.Length > 0)
			{
				MoreInfoElement.InnerHTML = moreInfoHtml;
				moreInfoVisible = true;
				updateUI();
			}
		}
		#endregion
		#region HideMoreInfo
		public void HideMoreInfo(bool resetItems)
		{
			moreInfoVisible = false;

			if (Selected && resetItems)
				resetItemsOnSelect();

			updateUI();
		}
		#endregion
		#region showMoreInfoIfNoMessages
		int showMoreInfoIfNoMessagesLastRandom;
		void showMoreInfoIfNoMessages(int showMoreInfoIfNoMessagesRandom)
		{
			if (Selected && ItemCount == 0 && !MoreInfoVisible && showMoreInfoIfNoMessagesLastRandom == showMoreInfoIfNoMessagesRandom)
			{
				ShowMoreInfo(true);
			}
		}
		#endregion
		#region showMoreInfoAfterDelayIfNoMessages
		void showMoreInfoAfterDelayIfNoMessages()
		{
			showMoreInfoIfNoMessagesLastRandom = (int)Math.Floor(Math.Random() * 10000);
			Window.SetTimeout(delegate { showMoreInfoIfNoMessages(showMoreInfoIfNoMessagesLastRandom); }, 500);
		}
		#endregion

		#region ShowArchiveItems
		public void ShowArchiveItems(string itemsJson)
		{
			int htmlItemCount = 0;
			haveCheckedArchive = true;

			if (itemsJson.Length > 0)
			{

				ItemStub[] itemStubArray = (ItemStub[])Script.Eval(" [ " + itemsJson + " ] ");

				Array itemsArray = new Array();

				Newable firstNewable = null;
				Newable lastNewable = null;

				for (int i = 0; i < itemStubArray.Length; i++)
				{
					bool alreadyHaveThisItem = false;
					//check to see if we already have this guid...
					for (int j = 0; j < Items.Length; j++)
					{
						if (Items[j].Guid == itemStubArray[i].guid)
						{
							alreadyHaveThisItem = true;
							break;
						}
					}
					if (!alreadyHaveThisItem)
					{
						Item item = Item.Create(itemStubArray[i], Parent, RequestIndex, false, 1);

						if (item is HtmlItem)
						{
							if (htmlItemCount == 0)
							{
								DOMElement newNode = Document.CreateElement("div");
								newNode.ClassName = "ChatClientArchiveHeading";
								newNode.InnerHTML = "Archived messages:";
								MesssagesElement.AppendChild(newNode);
							}
							hasArchiveItems = true;
							htmlItemCount++;
							renderItemToHtmlNow((HtmlItem)item, false);

							if (item is Newable)
							{
								if (firstNewable == null)
									firstNewable = (Newable)item;

								lastNewable = (Newable)item;
							}
						}
					}
				}
				if (firstNewable != null)
					firstNewable.UpdateClassModifiersAllAtOnce(true, false, false);

				if (lastNewable != null)
					lastNewable.UpdateClassModifiersAllAtOnce(false, true, false);
			}

			if (htmlItemCount == 0 && Selected && ItemCount == 0 && !MoreInfoVisible)
				ShowMoreInfo(true);

		}
		#endregion
		#region GetArchiveItemsNow
		public void GetArchiveItemsNow()
		{
			if (haveCheckedArchive || !hasArchive)
				return;

			if (GetArchiveItems != null)
				GetArchiveItems(this, new RoomGuidEventArgs(this.Guid));
		}
		#endregion
		#region getArchiveIfLessThan20Messages
		int getArchiveLastRandom;
		void getArchiveIfLessThan20Messages(int getArchiveRandom)
		{
			if (haveCheckedArchive || !hasArchive)
				return;

			if (Selected && ItemCount < 20 && getArchiveLastRandom == getArchiveRandom)
				GetArchiveItemsNow();
		}
		#endregion
		#region getArchiveAfterDelayIfLessThan20Messages
		void getArchiveAfterDelayIfLessThan20Messages()
		{
			if (haveCheckedArchive || !hasArchive)
				return;

			getArchiveLastRandom = (int)Math.Floor(Math.Random() * 10000);
			Window.SetTimeout(delegate { getArchiveIfLessThan20Messages(getArchiveLastRandom); }, 500);
		}
		#endregion

		#region updateNewStatus()
		void updateNewStatus()
		{
			int unseenCount = 0;
			if (Items.Length > 0 && needsNewStatusUpdate)
			{
				int newableCount = 0;
				int itemIndexOfLastNewable = -1;
				int itemIndexOfLastNewNewable = -1;
				bool previousNewableWasInNewSection = false;
				bool foundFirstOldNewableThatWasPreviouslyOld = false;
				bool foundFirstOldItem = false;
				bool foundFirstSeenItem = false;
				
				for (int i = Items.Length - 1; i >= 0; i--)
				{
					if (Items[i].Guid == LatestItemOld)
						foundFirstOldItem = true;

					if (Items[i].Guid == LatestItemSeen)
						foundFirstSeenItem = true;

					if (Items[i] is Newable)
					{
						Newable n = (Newable)Items[i];

						bool isTopOfSection = false;
						bool isBottomOfSection = false;
						bool isInNewSection = false;

						if (newableCount == 0)
							isTopOfSection = true;

						itemIndexOfLastNewable = i;

						if (!foundFirstSeenItem)
							unseenCount++;

						if (!foundFirstOldItem) // is the message in the new section?
						{
							isInNewSection = true;
							itemIndexOfLastNewNewable = i;
							previousNewableWasInNewSection = true;
						}
						else
						{
							isInNewSection = false;

							if (previousNewableWasInNewSection)
								isTopOfSection = true;

							previousNewableWasInNewSection = false;

							if (!n.IsInNewSection)
								foundFirstOldNewableThatWasPreviouslyOld = true;
						}

						n.UpdateClassModifiersAllAtOnce(isTopOfSection, isBottomOfSection, isInNewSection);

						newableCount++;

						if (doneFullUpdateOfNewStatus && foundFirstOldNewableThatWasPreviouslyOld)
							break;
					}
				}
				if (!doneFullUpdateOfNewStatus && itemIndexOfLastNewable > -1)
				{
					((Newable)Items[itemIndexOfLastNewable]).IsBottomOfSection = true;
				}
				if (itemIndexOfLastNewNewable > -1)
				{
					((Newable)Items[itemIndexOfLastNewNewable]).IsBottomOfSection = true;
				}

				//if (!foundFirstOldItem && newableCount > 1)
				//{
				//    newableCount = 0;
				//    itemIndexOfLastNewable = -1;
				//    //if all items are new, none should be hilighted.
				//    for (int i = Items.Length - 1; i >= 0; i--)
				//    {
				//        if (Items[i] is Newable)
				//        {
				//            Newable n = (Newable)Items[i];

				//            n.UpdateClassModifiersAllAtOnce(newableCount == 0, false, false);

				//            itemIndexOfLastNewable = i;
				//            newableCount++;
				//        }
				//    }
				//    if (itemIndexOfLastNewable > -1)
				//    {
				//        ((Newable)Items[itemIndexOfLastNewable]).IsBottomOfSection = true;
				//    }
				//}
				
				doneFullUpdateOfNewStatus = true;
				needsNewStatusUpdate = false;
			}

			if (unseenCount != NewMessages || TotalMessages != Items.Length)
			{
				TotalMessages = Items.Length;
				NewMessages = unseenCount;
				updateUI();
			}
			
		}
		#endregion

		#region InitialiseElements()
		public void InitialiseElements(string outerClientID)
		{
			if (outerClientID.Length > 0)
				OuterElement = Document.GetElementById(outerClientID);
			RoomElement = Document.GetElementById(html.ClientID);
			LinkElement = Document.GetElementById(html.LinkID);
			//ArrowElement = Document.GetElementById(html.ArrowID);
			CrossElement = Document.GetElementById(html.CrossID);
			PresenceElement = Document.GetElementById(html.PresenceID);
			TotalElement = Document.GetElementById(html.TotalID);
			StatsSeperatorElement = Document.GetElementById(html.StatsSeperatorID);
			UnreadElement = Document.GetElementById(html.UnreadID);
			elementsInitialised = true;
			updateUI();
		}
		#endregion

		#region updateUI()
		void updateUI()
		{
			updateTopPhotoUI();
			updateChatPicUI();
			updateRoomUI();
			//updateArrowUI();
			updatePresenceUI();
			updateLinkUI();
			updateStatsUI();
			updateMessagesUI();
		}
		#region updateTopPhotoUI()
		void updateTopPhotoUI()
		{
			if (elementsInitialised)
			{
				if (hasTopPhoto)
				{
					if (topPhotoHolder == null)
					{
						topPhotoHolder = Document.CreateElement("div");
						topPhotoHolder.Style.Position = "relative";
						topPhotoHolder.Style.Width = "280px";
						topPhotoHolder.Style.Height = "120px";
						MesssagesElementHolder.InsertBefore(topPhotoHolder, MesssagesElementHolder.ChildNodes[0]);

						topPhotoAnchor = (AnchorElement)Document.CreateElement("a");
						topPhotoAnchor.Style.Position = "absolute";
						topPhotoAnchor.Style.Top = "9px";
						topPhotoAnchor.Style.Left = "96px";
						
						topPhotoImage = (ImageElement)Document.CreateElement("img");
						topPhotoImage.ClassName = "BorderBlack All";
						topPhotoImage.Style.Width = "100px";
						topPhotoImage.Style.Height = "100px";

						topPhotoAnchor.AppendChild(topPhotoImage);
						topPhotoHolder.AppendChild(topPhotoAnchor);

						DOMElement txtSpan = Document.CreateElement("span");
						txtSpan.Style.TextAlign = "right";
						txtSpan.InnerHTML = "<small><a href=\"/pages/frontpagephotos\">Get&nbsp;yours<br />here!</a></small>";
						txtSpan.Style.Position = "absolute";
						txtSpan.Style.Top = "7px";
						txtSpan.Style.Left = "225px";
						txtSpan.ClassName = "CleanLinks";
						topPhotoHolder.AppendChild(txtSpan);

						DOMElement txtSpan1 = Document.CreateElement("span");
						txtSpan1.Style.TextAlign = "left";
						txtSpan1.InnerHTML = "<small>Chat&nbsp;about<br />this:</small>";
						txtSpan1.Style.Position = "absolute";
						txtSpan1.Style.Top = "7px";
						txtSpan1.Style.Left = "5px";
						topPhotoHolder.AppendChild(txtSpan1);
					}

					topPhotoImage.Src = Misc.GetPicUrlFromGuid(topPhoto.PhotoIcon);
					//topPhotoImage.Src = Misc.GetPicUrlFromGuid(topPhoto.PhotoThumb);
					//topPhotoImage.Style.Width = topPhoto.PhotoThumbWidth.ToString() + "px";
					//topPhotoImage.Style.Height = topPhoto.PhotoThumbHeight.ToString() + "px";

					DomEvent.ClearHandlers(topPhotoImage);
					DomEvent.AddHandler(topPhotoImage, "mouseover", delegate { Script.Eval("stm('<img src=" + Misc.GetPicUrlFromGuid(topPhoto.PhotoWeb) + " width=" + topPhoto.PhotoWebWidth.ToString() + " height=" + topPhoto.PhotoWebHeight.ToString() + " class=Block />');"); });
					DomEvent.AddHandler(topPhotoImage, "mouseout", delegate { Script.Literal("htm();"); });

					topPhotoAnchor.Href = topPhoto.PhotoUrl;



				}

			}
		}
		#endregion
		#region updateChatPicUI()
		void updateChatPicUI()
		{
			if (elementsInitialised)
			{
				if (hasTopPhoto)
				{
					if (chatPicHolder != null)
					{
						chatPicHolder.Style.Display = "none";
					}
					return;
				}
				if (hasChatPic)
				{
					if (chatPicHolder == null)
					{
						chatPicHolder = Document.CreateElement("div");
					//	chatPicHolder.ClassName = "ChatClientChatPicHolder";
					//	chatPicHolder.Style.Position = "relative";
					//	chatPicHolder.Style.Width = "300px";
					//	if (Misc.BrowserIsIE)
					//		chatPicHolder.Style.Height = "102px";
					//	else
					//		chatPicHolder.Style.Height = "101px";
					//	chatPicHolder.Style.BorderBottom = "1px solid #cba21e";
						MesssagesElementHolder.InsertBefore(chatPicHolder, MesssagesElementHolder.ChildNodes[0]);

						chatPicAnchor = (AnchorElement)Document.CreateElement("a");

						chatPicImage = (ImageElement)Document.CreateElement("img");
						chatPicImage.ClassName = "ChatClientChatPicImage";
					//	chatPicImage.Style.Position = "absolute";
					//	chatPicImage.Style.Top = "1px";
					//	chatPicImage.Style.Left = "0px";
					//	chatPicImage.Style.Width = "300px";
					//	chatPicImage.Style.Height = "100px";
						//chatPicImage.Style.BorderWidth = "0px";
						//chatPicImage.Style.BorderBottom = "1px solid #cba21e";

						chatPicAnchor.AppendChild(chatPicImage);
						chatPicHolder.AppendChild(chatPicAnchor);
					}

					chatPicImage.Src = Misc.GetPicUrlFromGuid(chatPic);

					DomEvent.ClearHandlers(chatPicImage);
					DomEvent.AddHandler(chatPicImage, "mouseover", delegate { Script.Eval("stmun(" + chatPicStmuParams + ");"); });
					DomEvent.AddHandler(chatPicImage, "mouseout", delegate { Script.Literal("htm();"); });

					chatPicAnchor.Href = chatPicUrl;
				}
				else
				{
					if (chatPicHolder != null)
						chatPicHolder.Style.Display = "none";
				}
			}
		}
		#endregion
		#region updateRoomUI()
		void updateRoomUI()
		{
			if (elementsInitialised)
			{
				RoomElement.ClassName = !Pinned ? "ChatClientRoomHolder ChatClientRoomUnpinned" : Selected ? "ChatClientRoomHolder ChatClientRoomSelected" : "ChatClientRoomHolder";
			}
		}
		#endregion
		#region updateArrowUI() - removed
		//void updateArrowUI()
		//{
		//    if (elementsInitialised)
		//    {
		//        ArrowElement.Style.Visibility = LoggedIn && Selected && !Guest && Pinned && IsPrivateChatRoom ? "" : "hidden";
		//    }
		//}
		#endregion
		#region updatePresenceUI()
		void updatePresenceUI()
		{
			if (elementsInitialised)
			{
				if (LoggedIn && IsPrivateChatRoom && (Presence == PresenceState.Chatting || Presence == PresenceState.Online))
				{
					PresenceElement.SetAttribute("src", Presence == PresenceState.Chatting ? "/gfx/chat-chatting.png" : "/gfx/chat-online.png");
					PresenceElement.Style.Width = Presence == PresenceState.Chatting ? "13" : "9";
				}
			}
		}
		#endregion
		#region updateLinkUI()
		void updateLinkUI()
		{
			if (elementsInitialised)
			{
				if (Pinned || Guest)
				{
					LinkElement.Style.TextDecoration = "";
					LinkElement.ClassName = Selected ? "ChatClientRoomLink ChatClientRoomLinkSelected" : (NewMessages == 0 ? "ChatClientRoomLink ChatClientRoomLinkNoUnread" : "ChatClientRoomLink");
				}
				else
				{
					LinkElement.Style.TextDecoration = "line-through";
					LinkElement.ClassName = "ChatClientRoomLink ChatClientRoomLinkNoUnread";
				}
			}
		}
		#endregion
		#region updateStatsUI()
		void updateStatsUI()
		{
			if (elementsInitialised)
			{
				if (Pinned || Guest)
				{
					TotalElement.InnerHTML = TotalMessages > 0 ? TotalMessages.ToString() : "&nbsp;";
					UnreadElement.InnerHTML = NewMessages > 0 ? NewMessages.ToString() : "&nbsp;";
					StatsSeperatorElement.InnerHTML = NewMessages > 0 ? "/" : "&nbsp;";
				}
				else
				{
					TotalElement.InnerHTML = "&nbsp;";
					UnreadElement.InnerHTML = "&nbsp;";
					StatsSeperatorElement.InnerHTML = "&nbsp;";
				}
			}
		}
		#endregion
		#region updateMessagesUI()
		void updateMessagesUI()
		{
			if (MesssagesElementHolder.Style.Display != (Selected && !MoreInfoVisible ? "" : "none"))
				MesssagesElementHolder.Style.Display = Selected && !MoreInfoVisible ? "" : "none";

			if (MoreInfoElement.Style.Display != (Selected && MoreInfoVisible ? "" : "none"))
				MoreInfoElement.Style.Display = Selected && MoreInfoVisible ? "" : "none";
		}
		#endregion
		#endregion

		#region UpdateStatsAfterUnPause
		public void UpdateStatsAfterUnPause(StateStub ss)
		{
			LatestItemOld = ss.latestItemOld;
			LatestItemSeen = ss.latestItemSeen;
			NewMessages = ss.newMessages;
			TotalMessages = ss.totalMessages;
			updateUI();
		}
		#endregion

	}
	#region StarActionEventArgs
	public class StarActionEventArgs : EventArgs
	{
		public string RoomGuid;
		public bool Starred;
		public StarActionEventArgs(string roomGuid, bool starred)
		{
			RoomGuid = roomGuid;
			Starred = starred;
		}
	}
	#endregion
	#region PinActionEventArgs
	public class PinActionEventArgs : EventArgs
	{
		public string RoomGuid;
		public bool Pinned;
		public PinActionEventArgs(string roomGuid, bool pinned)
		{
			RoomGuid = roomGuid;
			Pinned = pinned;
		}
	}
	#endregion
	#region RoomGuidEventArgs
	public class RoomGuidEventArgs : EventArgs
	{
		public string RoomGuid;
		public RoomGuidEventArgs(string roomGuid)
		{
			RoomGuid = roomGuid;
		}
	}
	#endregion
}
