namespace Js.Controls.CommentsDisplay
{
	public class CommentStub
	{
		public int k;
		public string usrUrl;
		public int usrK;
		public string usrRollover;
		public string usrPicSrc;
		public string usrName;
		public bool isNew;
		public string html;
		public string script;
		public string lolHtml;
		public bool haveAlreadyLold;
		public string friendlyTimeNoCaps;
		public bool editLinkVisible;
		public string editedHtml;
		public bool deleteLinkVisible;
		public string deleteLinkOnClickConfirmText;
		public int threadK;
	}

	public class CommentResult
	{
		public CommentStub initialComment;
		public CommentStub[] comments;
		public int lastPage;
		public int currentPage;
		public int firstUnreadPage;
		public int viewComments;
		public int totalComments;
	}
}
