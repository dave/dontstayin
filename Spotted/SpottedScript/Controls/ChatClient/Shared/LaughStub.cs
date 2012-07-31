namespace SpottedScript.Controls.ChatClient.Shared
{
	public class LaughStub : CommentMessageStub
	{
		public LaughStub(
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
			: base(guid, type, dateTime, roomGuid, nickName, stmuParams, usrK, pic, chatPic, text, pinRoomGuid, url, subject) { }
	}
}
