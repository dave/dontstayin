using SpottedScript.Controls.PhotoBrowser;
using PhotoControlController = SpottedScript.Controls.PhotoControl.Controller;
using PhotoBrowserController = SpottedScript.Controls.PhotoBrowser.Controller;
using ThreadControlController = SpottedScript.Controls.ThreadControl.Controller;
using LatestChatController = SpottedScript.Controls.LatestChat.Controller;
using PhotoBrowserPhotoProvider = SpottedScript.Controls.PhotoBrowser.PhotoProvider;
using Sys.UI;
using System;
using System.DHTML;

namespace SpottedScript.Pages.Events.Photos
{
	public class Controller : PhotosController
	{
		View view;
		public Controller(View view)
		{
			this.view = view;
			DomEvent.AddHandler(view.uiCurrentGallery, "change", new DomEventHandler(galleryChanged));
			setupController();
		}

		void galleryChanged(DomEvent e)
		{
			for (int i = 0; i < view.uiCurrentGallery.ChildNodes.Length; i++)
			{
				if (((OptionElement)view.uiCurrentGallery.ChildNodes[i]).Selected)
				{
					int galleryK = int.ParseInvariant(((OptionElement)view.uiCurrentGallery.ChildNodes[i]).Value);
					((EventPhotoProvider)PhotoProvider).setGallery(galleryK);
					view.uiPhotoControl.IsGallerySelectedChanged(galleryK > 0);
					view.uiPhotoBrowser.SelectedIndex = 0;
					PhotoBrowser.PaginationControl.CurrentPage = 1;
				}
			}
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
					photoProvider = new EventPhotoProvider(
						int.ParseInvariant(view.uiGalleryK.Value),
						int.ParseInvariant(view.uiEventK.Value));
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
