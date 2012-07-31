using SpottedScript.Controls.PhotoBrowser;
using PhotoControlController = SpottedScript.Controls.PhotoControl.Controller;
using PhotoBrowserController = SpottedScript.Controls.PhotoBrowser.Controller;
using ThreadControlController = SpottedScript.Controls.ThreadControl.Controller;
using LatestChatController = SpottedScript.Controls.LatestChat.Controller;
using PhotoBrowserPhotoProvider = SpottedScript.Controls.PhotoBrowser.PhotoProvider;
using Sys.UI;
using System;
using System.DHTML;

namespace SpottedScript.Pages.Videos
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
			get { return view.uiVideoControl; }
		}

		protected override PhotoBrowserController PhotoBrowser
		{
			get { return view.uiVideoBrowser; }
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
					photoProvider = new VideoPhotoProvider();
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
