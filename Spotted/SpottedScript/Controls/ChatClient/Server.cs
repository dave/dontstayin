using Sys.Net;
using Sys;
using System;
using System.XML;
using System.DHTML;
using SpottedScript.Controls.ChatClient.Shared;
using SpottedScript.Controls.ChatClient.Items;
using Spotted.WebServices;


namespace SpottedScript.Controls.ChatClient
{
	public class ServerClass
	{

		#region Private members
		Controller Controller;
		int SessionID;
		string LastActionTicks;
		string LastItemGuid;
		int RequestIndex;
		/// <summary>
		/// Each call to a web service has a uniquer call id.
		/// </summary>
		int webRequestIndex;
		StateStub[] roomState;
		Queue criticalRequestQueue;
		bool criticalRequestInProgress;
		bool periodicBackgroundRefreshInProgress;
		bool cancelCurrentPeriodicBackgroundRefresh;
		bool periodicBackgroundRefreshIsPaused;
		#endregion

		#region Event handlers
		public EventHandler GotItems = null;
		public EventHandler GotNoItems = null;
		public EventHandler GotWrongSessionException = null;
		public EventHandler GotTimeoutException = null;
		public EventHandler GotGenericException = null;
		public GotRoomHandler GotRoom = null;
		public GotRoomHandler GotNewPhotoRoom = null;
		public EventHandler GotMoreInfo = null;
		public EventHandler GotArchiveItems = null;
		public EventHandler GotRoomState = null;
		public EventHandler DebugPrint = null;
		public EventHandler ShowLoadingIcon = null;
		public EventHandler HideLoadingIcon  = null;
		public EventHandler DoneDeleteArchive = null;
		#endregion

		#region public Server(Controller controller, int sessionID, string lastActionTicks)
		public ServerClass(Controller controller, int sessionID, string lastActionTicks, StateStub[] controllerRoomState)
		{
			SessionID = sessionID;
			LastActionTicks = lastActionTicks;
			LastItemGuid = "";
			RequestIndex = 0;
			Controller = controller;
			criticalRequestQueue = new Queue();
			criticalRequestInProgress = false;
			periodicBackgroundRefreshInProgress = false;
			cancelCurrentPeriodicBackgroundRefresh = false;
			periodicBackgroundRefreshIsPaused = false;
			roomState = controllerRoomState;
		}
		#endregion

		#region Start()
		public void Start()
		{
			StartChatRefreshIfIdle();
			Window.SetInterval(StartChatRefreshIfIdle, 1000);
		}
		#endregion

		#region sendOrQueue
		void sendOrQueue(CriticalRequest criticalRequest)
		{
			if (ShowLoadingIcon != null)
				ShowLoadingIcon(this, null);

			if (criticalRequestQueue.Length == 0 && !criticalRequestInProgress)
			{
				if (periodicBackgroundRefreshInProgress)
					cancelCurrentPeriodicBackgroundRefresh = true;
				criticalRequestInProgress = true;
				criticalRequest.SendNow();
				debug("Processing request immediately");
			}
			else
			{
				Queue.Enqueue(criticalRequestQueue, criticalRequest);
				debug("Queueing request...");
			}
		}
		#endregion
		
		#region continueProcessingCriticalRequestQueue
		void continueProcessingCriticalRequestQueue()
		{
			if (criticalRequestQueue.Length > 0)
			{
				if (ShowLoadingIcon != null)
					ShowLoadingIcon(this, null);

				CriticalRequest criticalRequest = (CriticalRequest)Queue.Dequeue(criticalRequestQueue);
				criticalRequest.SendNow();

				debug("Processing queued request (" + criticalRequestQueue.Length + " in the queue)");
			}
			else
			{
				if (HideLoadingIcon != null)
					HideLoadingIcon(this, null);

				criticalRequestInProgress = false;
			}
		}
		#endregion

		#region StartChatRefreshIfIdle()
		void StartChatRefreshIfIdle()
		{
			if (periodicBackgroundRefreshIsPaused)
				return;

			if (criticalRequestQueue.Length == 0 && !criticalRequestInProgress && !periodicBackgroundRefreshInProgress)
			{
				periodicBackgroundRefreshInProgress = true;
				InvokeRefreshChat();
			}

			if (criticalRequestQueue.Length == 0 && !criticalRequestInProgress)
			{
				if (HideLoadingIcon != null)
					HideLoadingIcon(this, null);
			}
			else
			{
				if (ShowLoadingIcon != null)
					ShowLoadingIcon(this, null);
			}
		}
		#endregion

		#region SendMessage
		public void SendMessage(string message, string roomGuid)
		{
			periodicBackgroundRefreshIsPaused = false;
			SendMessageRequest r = new SendMessageRequest(this, message, roomGuid);
			sendOrQueue(r);
		}
		public void InvokeSendMessage(string message, string roomGuid)
		{
			ChatService.Send(
			    message,
			    roomGuid,
			    LastItemGuid,
			    SessionID,
			    "",
			    roomState,
				SendSuccessCallback,
			    SendFailureCallback,
				++webRequestIndex,
				2500);
		}
		#endregion
		#region SendSuccessCallback
		public void SendSuccessCallback(RefreshStub s, object userContext, string methodName)
		{
			if (s != null)
				processItems(s.itemsJson, s.lastActionTicks, s.lastItemGuidReturned, methodName, s.guestRefreshStubs, "", false);

			continueProcessingCriticalRequestQueue();
		}
		#endregion
		#region SendFailureCallback
		public void SendFailureCallback(WebServiceError error, object userContext, string methodName)
		{
			if (GotGenericException != null)
				GotGenericException(this, new GotExceptionEventArgs(error, methodName));

			continueProcessingCriticalRequestQueue();
		}
		#endregion

		#region DeleteArchive
		public void DeleteArchive(string roomGuid)
		{
			periodicBackgroundRefreshIsPaused = false;
			DeleteArchiveRequest r = new DeleteArchiveRequest(this, roomGuid);
			sendOrQueue(r);
		}
		public void InvokeDeleteArchive(string roomGuid)
		{
			ChatService.DeleteArchive(
				roomGuid,
				LastItemGuid,
				SessionID,
				LastActionTicks,
				"",
				roomState,
				DeleteArchiveSuccessCallback,
				DeleteArchiveFailureCallback,
				++webRequestIndex,
				5000);
		}
		#endregion
		#region DeleteArchiveSuccessCallback
		public void DeleteArchiveSuccessCallback(DeleteArchiveStub s, object userContext, string methodName)
		{
			if (s != null)
			{
				if (DoneDeleteArchive != null)
					DoneDeleteArchive(this, new RoomGuidEventArgs(s.roomGuid));

				processItems(s.itemsJson, s.lastActionTicks, s.lastItemGuidReturned, methodName, s.guestRefreshStubs, "", false);
			}

			continueProcessingCriticalRequestQueue();
		}
		#endregion
		#region DeleteArchiveFailureCallback
		public void DeleteArchiveFailureCallback(WebServiceError error, object userContext, string methodName)
		{
			if (GotGenericException != null)
				GotGenericException(this, new GotExceptionEventArgs(error, methodName));

			continueProcessingCriticalRequestQueue();
		}
		#endregion

		#region GetArchiveItems
		public void GetArchiveItems(string roomGuid)
		{
			periodicBackgroundRefreshIsPaused = false;
			ArchiveItemsRequest r = new ArchiveItemsRequest(this, roomGuid);
			sendOrQueue(r);
		}
		public void InvokeGetArchiveItems(string roomGuid)
		{
			ChatService.GetArchive(
			    roomGuid,
			    LastItemGuid,
			    SessionID,
				LastActionTicks,
			    "",
			    roomState,
				GetArchiveItemsSuccessCallback,
				GetArchiveItemsFailureCallback,
			    ++webRequestIndex,
			    5000);
		}
		#endregion
		#region GetArchiveItemsSuccessCallback
		public void GetArchiveItemsSuccessCallback(ArchiveStub s, object userContext, string methodName)
		{
			if (s != null)
			{
				processItems(s.itemsJson, s.lastActionTicks, s.lastItemGuidReturned, methodName, s.guestRefreshStubs, "", false);

				if (GotArchiveItems != null)
					GotArchiveItems(this, new GotArchiveItemsEventArgs(s.roomGuid, s.archiveItems));
			}

			continueProcessingCriticalRequestQueue();
		}
		#endregion
		#region GetArchiveItemsFailureCallback
		public void GetArchiveItemsFailureCallback(WebServiceError error, object userContext, string methodName)
		{
			if (GotGenericException != null)
				GotGenericException(this, new GotExceptionEventArgs(error, methodName));

			continueProcessingCriticalRequestQueue();
		}
		#endregion

		#region GetMoreInfo
		public void GetMoreInfo(string roomGuid)
		{
			
			periodicBackgroundRefreshIsPaused = false;
			MoreInfoRequest r = new MoreInfoRequest(this, roomGuid);
			sendOrQueue(r);
		}
		public void InvokeGetMoreInfo(string roomGuid)
		{
			ChatService.MoreInfo(
			    roomGuid,
			    LastItemGuid,
			    SessionID,
				LastActionTicks,
			    "",
			    roomState,
				GetMoreInfoSuccessCallback,
				GetMoreInfoFailureCallback,
			    ++webRequestIndex,
			    2500);
		}
		#endregion
		#region GetMoreInfoSuccessCallback
		public void GetMoreInfoSuccessCallback(MoreInfoStub s, object userContext, string methodName)
		{
			if (s != null)
			{
				if (GotMoreInfo != null)
					GotMoreInfo(this, new GotMoreInfoEventArgs(s.roomGuid, s.moreInfoHtml));

				processItems(s.itemsJson, s.lastActionTicks, s.lastItemGuidReturned, methodName, s.guestRefreshStubs, "", false);
			}

			continueProcessingCriticalRequestQueue();
		}
		#endregion
		#region GetMoreInfoFailureCallback
		public void GetMoreInfoFailureCallback(WebServiceError error, object userContext, string methodName)
		{
			if (GotGenericException != null)
				GotGenericException(this, new GotExceptionEventArgs(error, methodName));

			continueProcessingCriticalRequestQueue();
		}
		#endregion

		#region SwitchPhotoRoom
		public void SwitchPhotoRoom(string newRoomGuid)
		{
			periodicBackgroundRefreshIsPaused = false;
			SwitchPhotoRoomRequest r = new SwitchPhotoRoomRequest(this, newRoomGuid);
			sendOrQueue(r);
		}
		public void InvokeSwitchPhotoRoom(string newRoomGuid)
		{
			ChatService.SwitchPhotoRoom(
				Controller.ClientID,
				newRoomGuid,
				LastItemGuid,
				SessionID,
				LastActionTicks,
				"",
				roomState,
				SwitchPhotoRoomSuccessCallback,
				SwitchPhotoRoomFailureCallback,
				++webRequestIndex,
				2500);
		}
		#endregion
		#region SwitchPhotoRoomSuccessCallback
		public void SwitchPhotoRoomSuccessCallback(PinStub p, object userContext, string methodName)
		{
			if (p != null)
			{
				bool excludeItemsFromPinnedRoom = false;
				if (GotNewPhotoRoom != null)
					excludeItemsFromPinnedRoom = GotNewPhotoRoom(this, new GotRoomEventArgs(p.roomStub));

				processItems(p.itemsJson, p.lastActionTicks, p.lastItemGuidReturned, methodName, p.guestRefreshStubs, p.roomStub.guid, excludeItemsFromPinnedRoom);
			}
			continueProcessingCriticalRequestQueue();
		}
		#endregion
		#region SwitchPhotoRoomFailureCallback
		public void SwitchPhotoRoomFailureCallback(WebServiceError error, object userContext, string methodName)
		{
			if (GotGenericException != null)
				GotGenericException(this, new GotExceptionEventArgs(error, methodName));

			continueProcessingCriticalRequestQueue();
		}
		#endregion

		#region PinRoom
		public void PinRoom(string newRoomGuid)
		{
			periodicBackgroundRefreshIsPaused = false;
			PinRoomRequest r = new PinRoomRequest(this, newRoomGuid);
			sendOrQueue(r);
		}
		public void InvokePinRoom(string newRoomGuid)
		{
			ChatService.Pin(
				Controller.ClientID,
				newRoomGuid,
				LastItemGuid,
				SessionID,
				LastActionTicks,
				"",
				roomState,
				PinSuccessCallback,
				PinFailureCallback,
				++webRequestIndex,
				2500);

		}
		#endregion
		#region PinSuccessCallback
		public void PinSuccessCallback(PinStub p, object userContext, string methodName)
		{
			if (p != null)
			{
				bool excludeItemsFromPinnedRoom = false;
				if (GotRoom != null)
					excludeItemsFromPinnedRoom = GotRoom(this, new GotRoomEventArgs(p.roomStub));

				processItems(p.itemsJson, p.lastActionTicks, p.lastItemGuidReturned, methodName, p.guestRefreshStubs, p.roomStub.guid, excludeItemsFromPinnedRoom);
			}
			continueProcessingCriticalRequestQueue();
		}
		#endregion
		#region PinFailureCallback
		public void PinFailureCallback(WebServiceError error, object userContext, string methodName)
		{
			if (GotGenericException != null)
				GotGenericException(this, new GotExceptionEventArgs(error, methodName));

			continueProcessingCriticalRequestQueue();
		}
		#endregion

		#region UnPinRoom
		public void UnPinRoom(string roomGuid)
		{
			periodicBackgroundRefreshIsPaused = false;
			UnPinRoomRequest r = new UnPinRoomRequest(this, roomGuid);
			sendOrQueue(r);
		}
		public void InvokeUnPinRoom(string roomGuid)
		{
			ChatService.UnPin(
				Controller.ClientID,
				roomGuid,
				LastItemGuid,
				SessionID,
				LastActionTicks,
				"",
				roomState,
				UnPinSuccessCallback,
				UnPinFailureCallback,
				++webRequestIndex,
				2500);

		}
		#endregion
		#region UnPinSuccessCallback
		public void UnPinSuccessCallback(UnPinStub u, object userContext, string methodName)
		{
			processItems(u.itemsJson, u.lastActionTicks, u.lastItemGuidReturned, methodName, u.guestRefreshStubs, u.roomGuid, true);
			
			continueProcessingCriticalRequestQueue();
		}
		#endregion
		#region UnPinFailureCallback
		public void UnPinFailureCallback(WebServiceError error, object userContext, string methodName)
		{
			if (GotGenericException != null)
				GotGenericException(this, new GotExceptionEventArgs(error, methodName));

			continueProcessingCriticalRequestQueue();
		}
		#endregion

		#region StarRoom
		public void StarRoom(string roomGuid, bool starred)
		{
			periodicBackgroundRefreshIsPaused = false;
			StarRoomRequest r = new StarRoomRequest(this, roomGuid, starred);
			sendOrQueue(r);
		}
		public void InvokeStarRoom(string roomGuid, bool starred)
		{
			ChatService.Star(
				Controller.ClientID,
				roomGuid,
				starred,
				LastItemGuid,
				SessionID,
				LastActionTicks,
				"",
				roomState,
				StarSuccessCallback,
				StarFailureCallback,
				++webRequestIndex,
				2500);

		}
		#endregion
		#region StarSuccessCallback
		public void StarSuccessCallback(RefreshStub r, object userContext, string methodName)
		{
			processItems(r.itemsJson, r.lastActionTicks, r.lastItemGuidReturned, methodName, r.guestRefreshStubs, "", false);

			continueProcessingCriticalRequestQueue();
		}
		#endregion
		#region StarFailureCallback
		public void StarFailureCallback(WebServiceError error, object userContext, string methodName)
		{
			if (GotGenericException != null)
				GotGenericException(this, new GotExceptionEventArgs(error, methodName));

			continueProcessingCriticalRequestQueue();
		}
		#endregion

		#region RePinRoom
		public void RePinRoom(string newRoomGuid)
		{
			periodicBackgroundRefreshIsPaused = false;
			RePinRoomRequest r = new RePinRoomRequest(this, newRoomGuid);
			sendOrQueue(r);
		}
		public void InvokeRePinRoom(string roomGuid)
		{
			ChatService.RePin(
				Controller.ClientID,
				roomGuid,
				LastItemGuid,
				SessionID,
				LastActionTicks,
				"",
				roomState,
				RePinSuccessCallback,
				RePinFailureCallback,
				++webRequestIndex,
				2500);
		}
		#endregion
		#region RePinSuccessCallback
		public void RePinSuccessCallback(RefreshStub r, object userContext, string methodName)
		{
			processItems(r.itemsJson, r.lastActionTicks, r.lastItemGuidReturned, methodName, r.guestRefreshStubs, "", false);

			continueProcessingCriticalRequestQueue();
		}
		#endregion
		#region RePinFailureCallback
		public void RePinFailureCallback(WebServiceError error, object userContext, string methodName)
		{
			if (GotGenericException != null)
				GotGenericException(this, new GotExceptionEventArgs(error, methodName));

			continueProcessingCriticalRequestQueue();
		}
		#endregion

		#region StoreUpdatedRoomListOrder
		public void StoreUpdatedRoomListOrder()
		{
			periodicBackgroundRefreshIsPaused = false;
			StoreUpdatedRoomListOrderRequest r = new StoreUpdatedRoomListOrderRequest(this);
			sendOrQueue(r);
		}
		public void InvokeStoreUpdatedRoomListOrder()
		{
			ChatService.StoreUpdatedRoomListOrder(
				LastItemGuid,
				SessionID,
				LastActionTicks,
				"",
				roomState,
				StoreUpdatedRoomListOrderSuccessCallback,
				StoreUpdatedRoomListOrderFailureCallback,
				++webRequestIndex,
				2500);
		}
		#endregion
		#region StoreUpdatedRoomListOrderSuccessCallback
		public void StoreUpdatedRoomListOrderSuccessCallback(RefreshStub s, object userContext, string methodName)
		{
			processItems(s.itemsJson, s.lastActionTicks, s.lastItemGuidReturned, methodName, s.guestRefreshStubs, "", false);

			continueProcessingCriticalRequestQueue();
		}
		#endregion
		#region StoreUpdatedRoomListOrderFailureCallback
		public void StoreUpdatedRoomListOrderFailureCallback(WebServiceError error, object userContext, string methodName)
		{
			if (GotGenericException != null)
				GotGenericException(this, new GotExceptionEventArgs(error, methodName));

			continueProcessingCriticalRequestQueue();
		}
		#endregion

		#region ResumeAfterPause()
		public void ResumeAfterPause()
		{
			periodicBackgroundRefreshIsPaused = false;
			ForceResetSessionAndGetState r = new ForceResetSessionAndGetState(this);
			sendOrQueue(r);
		}
		#endregion
		#region InvokeRefreshWithForceResetSession
		public void InvokeForceResetSessionAndGetState()
		{
			ChatService.ResetSessionAndGetState(
				RequestIndex == 0,
				LastItemGuid,
				SessionID,
				LastActionTicks,
				"",
				roomState,
				ForceResetSessionAndGetStateSuccessCallback,
				ForceResetSessionAndGetStateFailureCallback,
				++webRequestIndex,
				2500);

		}
		#endregion
		#region ForceResetSessionAndGetStateSuccessCallback
		public void ForceResetSessionAndGetStateSuccessCallback(object result, object userContext, string methodName)
		{
			GetStateStub r = (GetStateStub)result;

			if (GotRoomState != null)
				GotRoomState(this, new GotRoomStateEventArgs(r.roomState));

			processItems(r.itemsJson, r.lastActionTicks, r.lastItemGuidReturned, methodName, r.guestRefreshStubs, "", false);

			continueProcessingCriticalRequestQueue();
		}
		#endregion
		#region ForceResetSessionAndGetStateFailureCallback
		public void ForceResetSessionAndGetStateFailureCallback(WebServiceError error, object userContext, string methodName)
		{
			if (GotGenericException != null)
				GotGenericException(this, new GotExceptionEventArgs(error, methodName));

			continueProcessingCriticalRequestQueue();
		}
		#endregion

		#region InvokeRefreshChat
		public void InvokeRefreshChat()
		{
			ChatService.Refresh(
				RequestIndex == 0,
				LastItemGuid,
				SessionID,
				LastActionTicks,
				"",
				roomState,
				RefreshSuccessCallback,
				RefreshFailureCallback,
				++webRequestIndex,
				2500);

		}
		#endregion
		#region RefreshSuccessCallback
		public void RefreshSuccessCallback(object result, object userContext, string methodName)
		{
			if (!cancelCurrentPeriodicBackgroundRefresh)
			{
				RefreshStub r = (RefreshStub)result;
				processItems(r.itemsJson, r.lastActionTicks, r.lastItemGuidReturned, methodName, r.guestRefreshStubs, "", false);
			}
			else
				debug("Cancelling periodic background refresh...");
			
			cancelCurrentPeriodicBackgroundRefresh = false;
			periodicBackgroundRefreshInProgress = false;
		}
		#endregion
		#region RefreshFailureCallback
		public void RefreshFailureCallback(WebServiceError error, object userContext, string methodName)
		{
			cancelCurrentPeriodicBackgroundRefresh = false;
			periodicBackgroundRefreshInProgress = false;

			if (error.ExceptionType.EndsWith("+WrongSessionException"))
			{
				periodicBackgroundRefreshIsPaused = true;
				if (GotWrongSessionException != null)
					GotWrongSessionException(this, new GotExceptionEventArgs(error, methodName));
			}
			else if (error.ExceptionType.EndsWith("+TimeoutException"))
			{
				periodicBackgroundRefreshIsPaused = true;
				if (GotTimeoutException != null)
					GotTimeoutException(this, new GotExceptionEventArgs(error, methodName));
			}
			else
			{
				if (GotGenericException != null)
					GotGenericException(this, new GotExceptionEventArgs(error, methodName));
			}
		}
		#endregion

		#region processItems
		void processItems(string itemsJson, string lastActionTicks, string lastItemGuidReturned, string methodName, GuestRefreshStub[] guestRefreshStubs, string pinResultRoomGuid, bool excludeItemsFromPinnedRoom)
		{

			ItemStub[] itemStubArray = (ItemStub[])Script.Eval(" [ " + itemsJson + " ] ");//Newest last

			Array itemsArray = new Array();

			for (int i = 0; i < itemStubArray.Length; i++) //Newest last
			{
				//If we pin a room, and the returned room is already pinned, we want to skip any items in the already pinned room
				if (!excludeItemsFromPinnedRoom || itemStubArray[i].roomGuid != pinResultRoomGuid)
				{
					Item item = Item.Create(itemStubArray[i], Controller, RequestIndex, false, 1);
					itemsArray[itemsArray.Length] = item;
				}
			}

			Item[] items = (Item[])itemsArray;//Newest last

			if (items.Length > 0 && lastItemGuidReturned.Length > 0)
				LastItemGuid = lastItemGuidReturned;

			if (lastActionTicks.Length > 0)
				LastActionTicks = lastActionTicks;

			if (guestRefreshStubs != null)
			{
				for (int i = 0; i < guestRefreshStubs.Length; i++)
				{
					GuestRefreshStub g = (GuestRefreshStub)guestRefreshStubs[i];
					if (g.itemsJson.Length > 0)
					{
						ItemStub[] guestItemStubArray = (ItemStub[])Script.Eval(" [ " + g.itemsJson + " ] ");
						for (int j = 0; j < guestItemStubArray.Length; j++)
						{
							Item item = Item.Create(guestItemStubArray[j], Controller, RequestIndex, true, 1);
							items[items.Length] = item;
						}
						//update state...
						if (g.lastItemGuidReturned.Length > 0)
						{
							for (int k = 0; k < roomState.Length; k++)
							{
								StateStub ss = (StateStub)roomState[k];
								if (ss.guid == g.roomGuid)
								{
									ss.latestItem = g.lastItemGuidReturned;
									break;
								}
							}
						}
					}
				}
			}

			if (items.Length > 0)
			{
				if (GotItems != null)
					GotItems(this, new GotItemsEventArgs(items, RequestIndex));
			}
			else
			{
				if (GotNoItems != null)
					GotNoItems(this, new GotNoItemsEventArgs(RequestIndex));
			}

			RequestIndex++;
		}
		#endregion

		#region debug
		void debug(string html)
		{
			if (DebugPrint != null)
				DebugPrint(this, new DebugPrintEventArgs(html));
		}
		#endregion

	}



	
	#region StoreUpdatedRoomListOrderRequest
	public class StoreUpdatedRoomListOrderRequest : CriticalRequest
	{
		ServerClass parent;
		public StoreUpdatedRoomListOrderRequest(ServerClass parent)
		{
			this.parent = parent;
		}
		public override void SendNow()
		{
			parent.InvokeStoreUpdatedRoomListOrder();
		}
	}
	#endregion
	#region SendMessageRequest
	public class SendMessageRequest : CriticalRequest
	{
		string message;
		string roomGuid;
		ServerClass parent;
		public SendMessageRequest(ServerClass parent, string message, string roomGuid)
		{
			this.parent = parent;
			this.message = message;
			this.roomGuid = roomGuid;
		}
		public override void SendNow()
		{
			parent.InvokeSendMessage(message, roomGuid);
		}
	}
	#endregion
	#region MoreInfoRequest
	public class MoreInfoRequest : CriticalRequest
	{
		string roomGuid;
		ServerClass parent;
		public MoreInfoRequest(ServerClass parent, string roomGuid)
		{
			this.parent = parent;
			this.roomGuid = roomGuid;
		}
		public override void SendNow()
		{
			parent.InvokeGetMoreInfo(roomGuid);
		}
	}
	#endregion
	#region ArchiveItemsRequest
	public class ArchiveItemsRequest : CriticalRequest
	{
		string roomGuid;
		ServerClass parent;
		public ArchiveItemsRequest(ServerClass parent, string roomGuid)
		{
			this.parent = parent;
			this.roomGuid = roomGuid;
		}
		public override void SendNow()
		{
			parent.InvokeGetArchiveItems(roomGuid);
		}
	}
	#endregion
	#region DeleteArchiveRequest
	public class DeleteArchiveRequest : CriticalRequest
	{
		string roomGuid;
		ServerClass parent;
		public DeleteArchiveRequest(ServerClass parent, string roomGuid)
		{
			this.parent = parent;
			this.roomGuid = roomGuid;
		}
		public override void SendNow()
		{
			parent.InvokeDeleteArchive(roomGuid);
		}
	}
	#endregion
	#region UnPinRoomRequest
	public class UnPinRoomRequest : CriticalRequest
	{
		string roomGuid;
		ServerClass parent;
		public UnPinRoomRequest(ServerClass parent, string roomGuid)
		{
			this.parent = parent;
			this.roomGuid = roomGuid;
		}
		public override void SendNow()
		{
			parent.InvokeUnPinRoom(roomGuid);
		}
	}
	#endregion
	#region PinRoomRequest
	public class PinRoomRequest : CriticalRequest
	{
		string newRoomGuid;
		ServerClass parent;
		public PinRoomRequest(ServerClass parent, string newRoomGuid)
		{
			this.parent = parent;
			this.newRoomGuid = newRoomGuid;
		}
		public override void SendNow()
		{
			parent.InvokePinRoom(newRoomGuid);
		}
	}
	#endregion
	#region StarRoomRequest
	public class StarRoomRequest : CriticalRequest
	{
		string roomGuid;
		bool starred;
		ServerClass parent;
		public StarRoomRequest(ServerClass parent, string roomGuid, bool starred)
		{
			this.parent = parent;
			this.roomGuid = roomGuid;
			this.starred = starred;
		}
		public override void SendNow()
		{
			parent.InvokeStarRoom(roomGuid, starred);
		}
	}
	#endregion
	#region SwitchPhotoRoomRequest
	public class SwitchPhotoRoomRequest : CriticalRequest
	{
		string newRoomGuid;
		ServerClass parent;
		public SwitchPhotoRoomRequest(ServerClass parent, string newRoomGuid)
		{
			this.parent = parent;
			this.newRoomGuid = newRoomGuid;
		}
		public override void SendNow()
		{
			parent.InvokeSwitchPhotoRoom(newRoomGuid);
		}
	}
	#endregion
	
	#region RePinRoomRequest
	public class RePinRoomRequest : CriticalRequest
	{
		string roomGuid;
		ServerClass parent;
		public RePinRoomRequest(ServerClass parent, string roomGuid)
		{
			this.parent = parent;
			this.roomGuid = roomGuid;
		}
		public override void SendNow()
		{
			parent.InvokeRePinRoom(roomGuid);
		}
	}
	#endregion
	#region RefreshWithForceResetSession
	public class ForceResetSessionAndGetState : CriticalRequest
	{
		ServerClass parent;
		public ForceResetSessionAndGetState(ServerClass parent)
		{
			this.parent = parent;
		}
		public override void SendNow()
		{
			parent.InvokeForceResetSessionAndGetState();
		}
	}
	#endregion
	#region CriticalRequest
	public abstract class CriticalRequest
	{
		public abstract void SendNow();
	}
	#endregion
	
	#region GotRoomHandler
	public delegate bool GotRoomHandler(object o, EventArgs a);
	#endregion
	

	#region GotNoItemsEventArgs
	public class GotNoItemsEventArgs : EventArgs
	{
		public int ServerRequestIndex;
		public GotNoItemsEventArgs(int serverRequestIndex)
		{
			ServerRequestIndex = serverRequestIndex;
		}
	}
	#endregion
	#region GotMoreInfoEventArgs
	public class GotMoreInfoEventArgs : EventArgs
	{
		public string RoomGuid;
		public string MoreInfoHtml;
		public GotMoreInfoEventArgs(string roomGuid, string moreInfoHtml)
		{
			RoomGuid = roomGuid;
			MoreInfoHtml = moreInfoHtml;
		}
	}
	#endregion
	#region GotArchiveItemsEventArgs
	public class GotArchiveItemsEventArgs : EventArgs
	{
		public string RoomGuid;
		public string ArchiveItems;
		public GotArchiveItemsEventArgs(string roomGuid, string archiveItems)
		{
			RoomGuid = roomGuid;
			ArchiveItems = archiveItems;
		}
	}
	#endregion

	#region GotItemsEventArgs
	public class GotItemsEventArgs : EventArgs
	{
		public Item[] Items;
		public int ServerRequestIndex;
		public GotItemsEventArgs(Item[] items, int serverRequestIndex)
		{
			Items = items;
			ServerRequestIndex = serverRequestIndex;
		}
	}
	#endregion
	#region GotRoomEventArgs
	public class GotRoomEventArgs : EventArgs
	{
		public RoomStub RoomStub;
		public GotRoomEventArgs(RoomStub roomStub)
		{
			RoomStub = roomStub;
		}
	}
	#endregion
	#region GotRoomStateEventArgs
	public class GotRoomStateEventArgs : EventArgs
	{
		public StateStub[] RoomState;
		public GotRoomStateEventArgs(StateStub[] roomState)
		{
			RoomState = roomState;
		}
	}
	#endregion
	#region GotErrorEventArgs
	public class GotExceptionEventArgs : EventArgs
	{
		public WebServiceError Error;
		public string Method;
		public GotExceptionEventArgs(WebServiceError error, string method)
		{
			Error = error;
			Method = method;
		}
	}
	#endregion
	#region DebugPrintEventArgs
	public class DebugPrintEventArgs : EventArgs
	{
		public string Html;
		public DebugPrintEventArgs(string html)
		{
			Html = html;
		}
	}
	#endregion
}
