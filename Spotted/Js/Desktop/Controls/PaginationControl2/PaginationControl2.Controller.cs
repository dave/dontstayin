using System;
using System.Html;
using jQueryApi;
using Js.Library;

namespace Js.Controls.PaginationControl2
{
	public class Controller
	{
		View view;

		#region LastPage
		int lastPage;
		public int LastPage
		{
			get { return lastPage; }
			set
			{
				this.lastPage = value;
				if (lastPage == 1)
					view.uiContainer.Style.Display = "none";
				else if (lastPage < 0)
				{
					// we don't know how many pages
					view.uiLastPage.InnerHTML = "many";
				}
				else
				{
					view.uiContainer.Style.Display = "";
					view.uiLastPage.InnerHTML = lastPage.ToString();
				}
			}
		}
		#endregion
		#region CurrentPage
		int currentPage;
		public int CurrentPage
		{
			get { return currentPage; }
			set
			{
				this.currentPage = value;
				view.uiCurrentPage.InnerHTML = this.currentPage.ToString();
			}
		}
		#endregion
		public EventHandler OnPageChanged;

		public Controller(View view)
		{
			this.view = view;

			view.uiPrevPage.SetAttribute("onclick", "");
			view.uiPrevPageJ.Click(prevClick);
			view.uiNextPage.SetAttribute("onclick", "");
			view.uiNextPageJ.Click(nextClick);

			this.LastPage = int.Parse(view.uiLastPage.InnerHTML);
			this.CurrentPage = int.Parse(view.uiCurrentPage.InnerHTML);
		}

		void prevClick(jQueryEvent e)
		{
			e.PreventDefault();
			MoveToPreviousPage();
		}
		void nextClick(jQueryEvent e)
		{
			e.PreventDefault();
			MoveToNextPage();
		}

		public void MoveToNextPage()
		{
			moveToPage((CurrentPage == LastPage) ? 1 : (CurrentPage + 1));
		}

		public void MoveToPreviousPage()
		{
			// we may not know what the last page is, in which case prevent going backwards from 1
			if (CurrentPage > 1 || LastPage > 0)
			{
				moveToPage((CurrentPage == 1) ? LastPage : (CurrentPage - 1));
			}
		}

		void moveToPage(int page)
		{
			// if Last page is negative we don't know how many pages we got
			if ((page > LastPage && LastPage > 0) || page < 1) 
				page = 1;

			CurrentPage = page;

			if (OnPageChanged != null) OnPageChanged(this, new IntEventArgs(page));
		}
	}
}
