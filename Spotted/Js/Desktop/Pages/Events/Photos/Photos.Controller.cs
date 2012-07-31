using Js.Controls.PhotoBrowser;
using PhotoControlController = Js.Controls.PhotoControl.Controller;
using PhotoBrowserController = Js.Controls.PhotoBrowser.Controller;
using ThreadControlController = Js.Controls.ThreadControl.Controller;
using LatestChatController = Js.Controls.LatestChat.Controller;
using PhotoBrowserPhotoProvider = Js.Controls.PhotoBrowser.PhotoProvider;
using System;
using System.Html;
using jQueryApi;

namespace Js.Pages.Events.Photos
{
	public class Controller : PhotosController
	{
		View view;
		public Controller(View view)
		{
			this.view = view;
			view.uiCurrentGalleryJ.Change(galleryChanged);
			setupController();
		}

		void galleryChanged(jQueryEvent e)
		{
			for (int i = 0; i < view.uiCurrentGallery.ChildNodes.Length; i++)
			{
				if (((OptionElement)view.uiCurrentGallery.ChildNodes[i]).Selected)
				{
					int galleryK = int.Parse(((OptionElement)view.uiCurrentGallery.ChildNodes[i]).Value);
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
						int.Parse(view.uiGalleryK.Value),
						int.Parse(view.uiEventK.Value));
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
