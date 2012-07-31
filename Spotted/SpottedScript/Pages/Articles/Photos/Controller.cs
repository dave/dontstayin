using PhotoControlController = SpottedScript.Controls.PhotoControl.Controller;
using PhotoBrowserController = SpottedScript.Controls.PhotoBrowser.Controller;
using ThreadControlController = SpottedScript.Controls.ThreadControl.Controller;
using LatestChatController = SpottedScript.Controls.LatestChat.Controller;
using PhotoBrowserPhotoProvider = SpottedScript.Controls.PhotoBrowser.PhotoProvider;
using SpottedScript.Controls.PhotoBrowser;

namespace SpottedScript.Pages.Articles.Photos
{
	public class Controller : PhotosController
	{
		View view;
		public Controller(View view)
		{
			this.view = view;
			setupController();
		}

		protected override PhotoControlController PhotoControl
		{
			get { return view.uiPhotoControl; }
		}

		protected override PhotoBrowserController PhotoBrowser
		{
			get { return view.uiPhotoBrowser; }
		}

		protected override ThreadControlController ThreadControl
		{
			get { return view.uiThreadControl; }
		}

		PhotoBrowserPhotoProvider photoProvider;
		protected override PhotoBrowserPhotoProvider PhotoProvider
		{
			get
			{
				if (photoProvider == null)
				{
					photoProvider = new ArticlePhotoProvider(int.ParseInvariant(view.uiArticleK.Value));
				}
				return photoProvider;
			}
		}

		protected override LatestChatController LatestChatController
		{
			get { return view.uiLatestChat; }
		}
	}
}


