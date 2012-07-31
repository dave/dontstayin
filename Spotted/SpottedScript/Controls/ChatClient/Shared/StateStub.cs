namespace SpottedScript.Controls.ChatClient.Shared
{

#if !SCRIPT
	[System.Serializable]
#endif
	public class StateStub
	{
		public string guid;
		public bool selected;
		public bool guest;
		public int newMessages;
		public int totalMessages;
		public string latestItem;
		public string latestItemOld;
		public string latestItemSeen;
		public int listOrder;
		public string tokenDateTimeTicks;
		public string token;

		public StateStub() { }
		public void Initialise(string guid, bool selected, bool guest, int newMessages, int totalMessages, string latestItem, string latestItemSeen, string latestItemOld, int listOrder, string tokenDateTimeTicks, string token)
		{
			this.guid = guid;
			this.selected = selected;
			this.guest = guest;
			this.newMessages = newMessages;
			this.totalMessages = totalMessages;
			this.latestItem = latestItem;
			this.latestItemOld = latestItemOld;
			this.latestItemSeen = latestItemSeen;
			this.listOrder = listOrder;
			this.tokenDateTimeTicks = tokenDateTimeTicks;
			this.token = token;
		}
	}
}
