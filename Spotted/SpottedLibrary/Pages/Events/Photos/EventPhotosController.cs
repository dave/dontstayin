using System;
using System.Collections.Generic;
using System.Text;
using Bobs;
using Common;
using SpottedLibrary.Controls.PhotoBrowserControl;
using SpottedLibrary.Controls.PhotoPage;
using Pair = System.Collections.Generic.KeyValuePair<object, Bobs.OrderBy.OrderDirection>;
namespace SpottedLibrary.Pages.Events.Photos
{
	public class EventPhotosController : PhotoPageController
	{
		new IEventPhotosView view;
		public EventPhotosController(IEventPhotosView view)
			:base(view)
		{
			this.view = view;
			this.view.SelectedGalleryChanged += new EventHandler<EventArgs<int>>(view_SelectedGalleryChanged);
		}

		void view_SelectedGalleryChanged(object sender, EventArgs<int> e)
		{
			if (e.Value == -1)
			{
				view.Redirect(view.EventFromUrl.UrlApp("photos"));
			}
			else
			{
				Gallery g = new Gallery(e.Value);
				view.Redirect(g.Url());
			}
		}
 

	 

		void ThreadControl_CommentAdded(object sender, EventArgs<Comment> e)
		{
			((Photo) this.view.ThreadControl.ParentObject).UpdateSingleThread();
		}


		protected override IPagedDataService<Photo> GetPagedDataService()
		{
			IPagedDataService<Photo> photoPagedDataService;
			
			if (view.GalleryFromUrl != null)
			{
				photoPagedDataService = view.GalleryFromUrl.ChildPhotos(Photo.EnabledQueryCondition, Photo.DefaultOrder);
			}
			else
			{
				photoPagedDataService = view.EventFromUrl.ChildPhotos(Photo.EnabledQueryCondition, Photo.DefaultOrder);
			}
			
			if (!view.IsPostBack)
			{
				this.view.GalleryNamesAndKs = view.EventFromUrl.GalleriesWithJoinedGalleryUsr(new OrderBy(Bobs.Gallery.Columns.Name), Usr.Current == null ? 0 : Usr.Current.K).ToList().ConvertAll(g => new KeyValuePair<string, int>(g.NameWithNewInfo, g.K));
				
				if (this.view.GalleryFromUrl != null)
					this.view.SelectedGalleryK = this.view.GalleryFromUrl.K;
				
			}
			return photoPagedDataService;
		}
		 
			
		protected override string GetTitle()
		{
			return view.EventFromUrl.Name;
		}
	}
}
