using PhotoControlController = Js.Controls.PhotoControl.Controller;
using PhotoBrowserController = Js.Controls.PhotoBrowser.Controller;
using ThreadControlController = Js.Controls.ThreadControl.Controller;
using LatestChatController = Js.Controls.LatestChat.Controller;
using PhotoBrowserPhotoProvider = Js.Controls.PhotoBrowser.PhotoProvider;
using Js.Controls.PhotoBrowser;

namespace Js.Pages.Articles.Photos
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
					photoProvider = new ArticlePhotoProvider(int.Parse(view.uiArticleK.Value));
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


