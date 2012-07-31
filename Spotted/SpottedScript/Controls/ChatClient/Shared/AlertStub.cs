namespace SpottedScript.Controls.ChatClient.Shared
{
	public class AlertStub : ItemStub
	{
		public string nickName;
		public string stmuParams;
		public int usrK;
		public string pic;

		public AlertStub(
			string guid,
			ItemType type,
			string dateTime,
			string roomGuid,
			string nickName,
			string stmuParams,
			int usrK,
			string pic)
			: base(guid, type, dateTime, roomGuid)
		{
			this.nickName = nickName;
			this.stmuParams = stmuParams;
			this.usrK = usrK;
			this.pic = pic;
		}
	}
}
