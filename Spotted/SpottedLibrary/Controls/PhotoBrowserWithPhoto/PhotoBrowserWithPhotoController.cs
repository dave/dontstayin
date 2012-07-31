using System;
using System.Collections.Generic;
using System.Text;
using Bobs;
using SpottedLibrary.Controls.PhotoPage;

namespace SpottedLibrary.Controls.PhotoBrowserWithPhoto
{
	public class PhotoBrowserWithPhotoController
	{
		IPhotoBrowserWithPhotoView view;
		public PhotoBrowserWithPhotoController(IPhotoBrowserWithPhotoView view)
		{
			this.view = view;
			this.view.Init += new EventHandler(view_Init);
		}

		public bool GallerySelected
		{
			set
			{
				view.PhotoControl.GallerySelected = value;
			}
		}

		int IndexOfCurrentPhoto
		{
			get
			{
				return view.ViewState["IndexOfCurrentPhoto"] as int? ?? 0;
			}
			set
			{
				if (this.view.PhotoBrowser.CurrentPageItems.Length > this.IndexOfCurrentPhoto)
				{ 
					this.view.PhotoBrowser.CurrentPageItems[this.IndexOfCurrentPhoto].Highlight = false; 
				}
				view.ViewState["IndexOfCurrentPhoto"] = value;
				this.view.PhotoControl.Photo = new Photo(this.view.PhotoBrowser.CurrentPageItems[this.IndexOfCurrentPhoto].K);
				this.view.PhotoBrowser.CurrentPageItems[this.IndexOfCurrentPhoto].Highlight = true;

				this.view.DataBind();
			}
		}

		void view_Init(object sender, EventArgs e)
		{
			//this.view.PhotoControl.PhotoClick += (o, ev) => MoveToPhoto(this.IndexOfCurrentPhoto + 1);
			//this.view.PhotoControl.NextButtonClick += (o, ev) => MoveToPhoto(this.IndexOfCurrentPhoto + 1);
			//this.view.PhotoControl.PrevButtonClick += (o, ev) => MoveToPhoto(this.IndexOfCurrentPhoto - 1);
			//this.view.PhotoBrowser.ItemClicked += (o, ev) => MoveToPhoto(ev.Value);
			this.view.PhotoBrowser.PagedDataServiceChanged += new EventHandler<EventArgs<Common.IPagedDataService<Photo>>>(PhotoBrowser_PagedDataServiceChanged);
			this.view.PhotoBrowser.PaginationControl.PageChanged += new EventHandler<EventArgs<int>>(PaginationControl_PageChanged);
		}

		void PaginationControl_PageChanged(object sender, EventArgs<int> e)
		{
			if (!view.IsPostBack)
			{
				//if (view.CutDownUrl["photo"] == "last")
				//{
				//	view.Redirect(view.CutDownUrl.CurrentUrl("photo", this.view.PhotoBrowser.CurrentPageItems[this.view.PhotoBrowser.CurrentPageItems.Length - 1].K));
				//}
				//else if (!view.CutDownUrl["photo"].Exists)
				//{
				//	view.Redirect(view.CutDownUrl.CurrentUrl("photo", this.view.PhotoBrowser.CurrentPageItems[0].K));
				//}
				Photo photo = null;

				if (view.CutDownUrl["photo"] == "last")
					photo = view.PhotoBrowser.CurrentPageItems[view.PhotoBrowser.CurrentPageItems.Length - 1];
				else if (view.PhotoFromUrl != null)
					photo = view.PhotoFromUrl;
				else
					photo = view.PhotoBrowser.CurrentPageItems[0];

				int indexOfCurrentPhoto = view.PhotoBrowser.CurrentPageItems.FindFirstIndex(p => p.K == photo.K) ?? -1;
				if (indexOfCurrentPhoto == -1)
				{
					//view.Redirect(photo.Url()); //this causes a redirect loop is the photo is not enabled!
					photo = view.PhotoBrowser.CurrentPageItems[0];
					indexOfCurrentPhoto = 0;
				}

				//if (indexOfCurrentPhoto == -1)
				//{
				//    view.Redirect(view.CutDownUrl.CurrentUrl("photo", this.view.PhotoBrowser.CurrentPageItems[0].K));
				//}
				//else
				//{
				photo.AddRelevant(view.RelevanceHolder);
				if (view.Title == "" && photo.Event != null)
				{
					view.Title = photo.Event.Name + " @ " + photo.Event.Venue.Name;
				}
				IndexOfCurrentPhoto = indexOfCurrentPhoto;
				//}
			}
			else
			{
				IndexOfCurrentPhoto = 0;
			}
		}

		void PhotoBrowser_PagedDataServiceChanged(object sender, EventArgs<Common.IPagedDataService<Photo>> e)
		{
			if (!this.view.IsPostBack)
			{
				if (view.PhotoBrowser.PagedDataService == null || view.PhotoBrowser.PagedDataService.Count == 0)
				{
					if (this.view.CutDownUrl["photo"].Exists)
					{
						view.Redirect(view.CutDownUrl.CurrentUrl("photo", null));
					}
					this.view.PhotoControl.Photo = null;
				}
				else
				{
					view.PhotoBrowser.PaginationControl.CurrentPage = view.CutDownUrl["photopage"].Exists ? view.CutDownUrl["photopage"].ValueInt : initialPage;
				}

				try
				{
					this.view.DataBind();
				}
				catch { }
			}
		}

		int initialPage
		{
			get
			{
				if (view.PhotoFromUrl != null)
					return 1 + (view.PhotoFromUrl.GalleryTimeOrder / Common.Properties.PhotoBrowser.IconsPerPage);
				else
					return 1;
			}
		}

		//private void MoveToPhoto(int index)
		//{
		//    //this.view.PhotoBrowser.CurrentPageItems[this.IndexOfCurrentPhoto].Highlight = false;
		//    if (index < 0)
		//    {
		//        if (this.view.PhotoBrowser.PaginationControl.CurrentPage > 1)
		//        {
		//            this.view.PhotoBrowser.PaginationControl.CurrentPage -= 1;
		//        }
		//        else
		//        {
		//            this.view.PhotoBrowser.PaginationControl.CurrentPage = this.view.PhotoBrowser.PaginationControl.LastPage;
		//        }
		//        IndexOfCurrentPhoto = this.view.PhotoBrowser.CurrentPageItems.Length - 1;
		//    }
		//    else if (index >= view.PhotoBrowser.CurrentPageItems.Length)
		//    {
		//        if (this.view.PhotoBrowser.PaginationControl.CurrentPage < this.view.PhotoBrowser.PaginationControl.LastPage)
		//        {
		//            this.view.PhotoBrowser.PaginationControl.CurrentPage += 1;
		//        }
		//        else
		//        {
		//            this.view.PhotoBrowser.PaginationControl.CurrentPage = 1;
		//        }
		//        IndexOfCurrentPhoto = 0;
		//    }
		//    else
		//    {
		//        IndexOfCurrentPhoto = index;
		//    }
		//}

	}
}
