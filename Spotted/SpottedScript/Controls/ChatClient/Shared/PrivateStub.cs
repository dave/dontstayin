namespace SpottedScript.Controls.ChatClient.Shared
{
	public class PrivateStub : MessageStub
	{
		public bool buddy;

		public PrivateStub(
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
			string pinRoomGuid,
			bool buddy)
			: base(guid, type, dateTime, roomGuid, nickName, stmuParams, usrK, pic, chatPic, text, pinRoomGuid)
		{
			this.buddy = buddy;
		}
	}
}
