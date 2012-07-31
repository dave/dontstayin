namespace SpottedScript.Controls.ChatClient.Shared
{
	public class MessageStub : ItemStub
	{
		public string nickName;
		public string stmuParams;
		public int usrK;
		public string pic;
		public string chatPic;
		public string text;
		public string pinRoomGuid;

		public MessageStub(
			string guid,
			ItemType type,
			string dateTime,
			string roomGuid,
			string nickName,
			string stmuParams,
			int usrK,
			string pic,
			string chatPic,
			string text,
			string pinRoomGuid)
			: base(guid, type, dateTime, roomGuid)
		{
			this.nickName = nickName;
			this.stmuParams = stmuParams;
			this.usrK = usrK;
			this.pic = pic;
			this.chatPic = chatPic;
			this.text = text;
			this.pinRoomGuid = pinRoomGuid;
		}
	}
	
}
