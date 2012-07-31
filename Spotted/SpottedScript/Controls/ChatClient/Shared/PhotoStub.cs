namespace SpottedScript.Controls.ChatClient.Shared
{
	public class PhotoStub : ItemStub
	{
		public int width;
		public int height;
		public string url;
		public string web;
		public string icon;
		public string thumb;
		public int thumbWidth;
		public int thumbHeight;
		public bool buddyAlert;

		public PhotoStub(
			string guid,
			ItemType type,
			string dateTime,
			string roomGuid,
			int width,
			int height,
			string url,
			string web,
			string icon,
			string thumb,
			int thumbWidth,
			int thumbHeight,
			bool buddyAlert)
			: base(guid, type, dateTime, roomGuid)
		{
			this.width = width;
			this.height = height;
			this.url = url;
			this.web = web;
			this.icon = icon;
			this.thumb = thumb;
			this.thumbWidth = thumbWidth;
			this.thumbHeight = thumbHeight;
			this.buddyAlert = buddyAlert;
		}
	}
}
