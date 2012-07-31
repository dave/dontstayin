using System;
using System.Collections.Generic;
using System.Text;
using SpottedLibrary.Controls.PaginationControl2;
using Common;

namespace SpottedLibrary.Controls.PagedRepeater
{
	public class PagedDataControlController<T>
	{
		IPagedDataControlView<T> view;
		public PagedDataControlController(IPagedDataControlView<T> view){
			this.view = view;
			this.view.PagedDataServiceChanged +=new EventHandler<EventArgs<IPagedDataService<T>>>(view_PagedDataServiceChanged);
			this.view.Init += new EventHandler(view_Init);
		}

		void view_Init(object sender, EventArgs e)
		{
			this.view.PaginationControl.PageChanged += new EventHandler<EventArgs<int>>(PaginationControl_PageChanged);
		}

		void PaginationControl_PageChanged(object sender, EventArgs<int> e)
		{
			this.view.CurrentPageItems = view.PagedDataService.Page(this.view.PaginationControl.CurrentPage, view.PageSize);
			this.view.DataBind();
		}
 

		void view_PagedDataServiceChanged(object sender, EventArgs<IPagedDataService<T>> e)
		{
			if (view.PagedDataService == null || view.PagedDataService.Count == 0)
			{
				view.Visible = false;
			}
			else
			{
				view.Visible = true;
				this.view.NumberOfItems = view.PagedDataService.Count;
				this.view.PaginationControl.LastPage = (int)Math.Ceiling((double)this.view.PagedDataService.Count / (double)view.PageSize);
				this.view.DataBind();
			}
		}


	}
}
