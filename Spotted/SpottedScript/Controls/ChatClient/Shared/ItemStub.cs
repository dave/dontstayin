namespace SpottedScript.Controls.ChatClient.Shared
{
#if SCRIPT
#else
	[System.Serializable]
#endif
	public class ItemStub
	{
		public string guid;
		public ItemType type;
		public string dateTime;
		public string roomGuid;

		public ItemStub(string guid, ItemType type, string dateTime, string roomGuid)
		{
			this.guid = guid;
			this.type = type;
			this.dateTime = dateTime;
			this.roomGuid = roomGuid;
		}
	}
}
