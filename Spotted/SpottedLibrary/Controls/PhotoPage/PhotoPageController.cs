using System;
using System.Collections.Generic;
using System.Text;
using Bobs;
using Common;
using SpottedLibrary.Controls.PhotoBrowserControl;

namespace SpottedLibrary.Controls.PhotoPage
{
	public abstract class PhotoPageController
	{
		protected IPhotoPageView view;
		public PhotoPageController(IPhotoPageView view)
		{
			this.view = view;
			this.view.Load += new EventHandler(view_Load);

		}


		void ThreadControl_CommentAdded(object sender, EventArgs<Comment> e)
		{
			((Photo)this.view.ThreadControl.ParentObject).UpdateSingleThread();
		}

		void view_Load(object sender, EventArgs e)
		{
			this.view.Title = GetTitle();
			this.view.PhotoBrowser.PagedDataService = GetPagedDataService();
		}

		protected abstract IPagedDataService<Photo> GetPagedDataService();
		protected abstract string GetTitle();
	}

}
