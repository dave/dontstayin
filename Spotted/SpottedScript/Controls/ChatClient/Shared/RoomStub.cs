namespace SpottedScript.Controls.ChatClient.Shared
{
	public class RoomStub
	{
		public string parentClientID;
		public string guid;
		public string name;
		public string url;
		public bool pinned;
		public bool pinable;
		public bool starred;
		public bool starrable;
		public bool isStarredByDefault;
		public bool selected;
		public bool guest;
		public int newMessages;
		public int totalMessages;
		public string latestItem;
		public string latestItemSeen;
		public string latestItemOld;
		public bool readOnly;
		public int listOrder;
		public bool isPhotoChatRoom;
		public bool isPrivateChatRoom;
		public bool isNewPhotoAlertsRoom;
		public PresenceState presence;
		public string icon;
		public string tokenDateTimeTicks;
		public string token;
		public bool hasArchive;
		public bool hiddenFromRoomList;
		public bool isStreamRoom;

		public RoomStub(
			string parentClientID, 
			string guid,
			string name,
			string url, 
			bool pinned,
			bool starred,
			bool isStarredByDefault,
			bool pinable,
			bool starrable,
			bool selected, 
			bool guest, 
			int newMessages, 
			int totalMessages, 
			string latestItem, 
			string latestItemSeen, 
			string latestItemOld, 
			bool readOnly, 
			int listOrder,
			bool isPhotoChatRoom,
			bool isPrivateChatRoom,
			bool isNewPhotoAlertsRoom,
			PresenceState presence,
			string icon,
			string tokenDateTimeTicks,
			string token,
			bool hasArchive,
			bool hiddenFromRoomList,
			bool isStreamRoom)
		{
			this.parentClientID = parentClientID;
			this.guid = guid;
			this.name = name;
			this.url = url;
			this.pinned = pinned;
			this.starred = starred;
			this.isStarredByDefault = isStarredByDefault;
			this.pinable = pinable;
			this.starrable = starrable;
			this.selected = selected;
			this.guest = guest;
			this.newMessages = newMessages;
			this.totalMessages = totalMessages;
			this.latestItem = latestItem;
			this.latestItemSeen = latestItemSeen;
			this.latestItemOld = latestItemOld;
			this.readOnly = readOnly;
			this.listOrder = listOrder;
			this.isPhotoChatRoom = isPhotoChatRoom;
			this.isPrivateChatRoom = isPrivateChatRoom;
			this.isNewPhotoAlertsRoom = isNewPhotoAlertsRoom;
			this.presence = presence;
			this.icon = icon;
			this.tokenDateTimeTicks = tokenDateTimeTicks;
			this.token = token;
			this.hasArchive = hasArchive;
			this.hiddenFromRoomList = hiddenFromRoomList;
			this.isStreamRoom = isStreamRoom;

		}
	}
	public enum PresenceState
	{
		None = 0,
		Offline = 1,
		Online = 2,
		Chatting = 3
	}
}
