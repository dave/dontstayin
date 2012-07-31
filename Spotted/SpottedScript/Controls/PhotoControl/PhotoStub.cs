namespace SpottedScript.Controls.PhotoControl
{
	public class PhotoStub
	{
		public int k;
		public string url;
		public string iconPath;
		public string webPath;
		public string thumbPath;
		public int width;
		public int height;
		public int thumbWidth;
		public int thumbHeight;

		public string takenByDetailsHtml;

		public string usrLink;
		public string photoVideoLabel;
		public bool isPhoto;
		public bool isVideo;
		public int videoMedWidth;
		public int videoMedHeight;
		public string videoMedPath;

		public string usrsInPhotoHtml;
		public bool usrIsInPhoto;
		public bool isFavourite;
		public bool isInCompetitionGroup;
		public bool canEnterCompetition;

		public string quickBrowserUrl;
		public string downloadPhotoLinkHtml;
		public string linkToPhotoUrl;
		public string embedThisPhotoHtml;

		//public string photoOfWeekCaption;
		//public bool photoOfWeek;

		public string photoUsageAdminString;

		public int threadK;
		public int commentsCount;

		public string chatRoomGuid;

		// in photo browser only
		public string rolloverMouseOverText;
	}

	public class PhotoResult
	{
		public PhotoStub[] photos;
		public int lastPage;
	}
}
