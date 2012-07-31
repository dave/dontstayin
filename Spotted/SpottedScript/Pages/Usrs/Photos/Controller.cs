using SpottedScript.Controls.PhotoBrowser;
using PhotoControlController = SpottedScript.Controls.PhotoControl.Controller;
using PhotoBrowserController = SpottedScript.Controls.PhotoBrowser.Controller;
using ThreadControlController = SpottedScript.Controls.ThreadControl.Controller;
using PhotoBrowserPhotoProvider = SpottedScript.Controls.PhotoBrowser.PhotoProvider;
using LatestChatController = SpottedScript.Controls.LatestChat.Controller;

namespace SpottedScript.Pages.Usrs.Photos
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
		protected override PhotoProvider PhotoProvider
		{
			get { return photoProvider ?? (photoProvider = new PhotosOfUsrProvider(int.ParseInvariant(view.uiUsrK.Value), int.ParseInvariant(view.uiSpottedByUsrK.Value))); }
		}

		protected override LatestChatController LatestChatController
		{
			get { return view.uiLatestChat; }
		}
	}
}
