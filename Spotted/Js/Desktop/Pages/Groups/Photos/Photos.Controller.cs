using Js.Controls.PhotoBrowser;
using PhotoControlController = Js.Controls.PhotoControl.Controller;
using PhotoBrowserController = Js.Controls.PhotoBrowser.Controller;
using ThreadControlController = Js.Controls.ThreadControl.Controller;
using PhotoBrowserPhotoProvider = Js.Controls.PhotoBrowser.PhotoProvider;
using LatestChatController = Js.Controls.LatestChat.Controller;

namespace Js.Pages.Groups.Photos
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
			get { return photoProvider ?? (photoProvider = new GroupPhotoProvider(int.Parse(view.uiGroupK.Value))); }
		}

		protected override LatestChatController LatestChatController
		{
			get { return view.uiLatestChat; }
		}
	}
}
