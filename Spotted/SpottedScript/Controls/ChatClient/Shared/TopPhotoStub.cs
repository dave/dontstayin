namespace SpottedScript.Controls.ChatClient.Shared
{
	public class TopPhotoStub : ItemStub
	{
		public int photoK;
		public string photoUrl;
		public string photoIcon;
		public string photoWeb;
		public int photoWebWidth;
		public int photoWebHeight;
		public string photoThumb;
		public int photoThumbWidth;
		public int photoThumbHeight;

		public TopPhotoStub(
			string guid,
			ItemType type,
			string dateTime,
			string roomGuid,
			int photoK,
			string photoUrl,
			string photoIcon,
			string photoWeb,
			int photoWebWidth,
			int photoWebHeight,
			string photoThumb,
			int photoThumbWidth,
			int photoThumbHeight)
			: base(guid, type, dateTime, roomGuid)
		{
			this.photoK = photoK;
			this.photoUrl = photoUrl;
			this.photoIcon = photoIcon;
			this.photoWeb = photoWeb;
			this.photoWebWidth = photoWebWidth;
			this.photoWebHeight = photoWebHeight;
			this.photoThumb = photoThumb;
			this.photoThumbWidth = photoThumbWidth;
			this.photoThumbHeight = photoThumbHeight;
		}
	}
}
