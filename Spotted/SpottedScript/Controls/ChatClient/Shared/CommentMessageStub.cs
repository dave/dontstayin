namespace SpottedScript.Controls.ChatClient.Shared
{
	public class CommentMessageStub : MessageStub
	{
		public string url;
		public string subject;

		public CommentMessageStub(
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
			string url,
			string subject)
			: base(guid, type, dateTime, roomGuid, nickName, stmuParams, usrK, pic, chatPic, text, pinRoomGuid)
		{
			this.url = url;
			this.subject = subject;
		}
	}
}
