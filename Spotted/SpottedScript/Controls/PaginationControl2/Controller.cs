using Sys;
using Sys.UI;
using System;
using System.DHTML;

namespace SpottedScript.Controls.PaginationControl2
{
	public class Controller
	{
		View view;

		#region LastPage
		int lastPage;
		internal int LastPage
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
		internal int CurrentPage
		{
			get { return currentPage; }
			set
			{
				this.currentPage = value;
				view.uiCurrentPage.InnerHTML = this.currentPage.ToString();
			}
		}
		#endregion
		internal EventHandler OnPageChanged;

		public Controller(View view)
		{
			this.view = view;

			view.uiPrevPage.SetAttribute("onclick", "");
			DomEvent.AddHandler(view.uiPrevPage, "click", new DomEventHandler(prevClick));
			view.uiNextPage.SetAttribute("onclick", "");
			DomEvent.AddHandler(view.uiNextPage, "click", new DomEventHandler(nextClick));

			this.LastPage = int.ParseInvariant(view.uiLastPage.InnerHTML);
			this.CurrentPage = int.ParseInvariant(view.uiCurrentPage.InnerHTML);
		}

		void prevClick(DomEvent e)
		{
			e.PreventDefault();
			MoveToPreviousPage();
		}
		void nextClick(DomEvent e)
		{
			e.PreventDefault();
			MoveToNextPage();
		}

		internal void MoveToNextPage()
		{
			moveToPage((CurrentPage == LastPage) ? 1 : (CurrentPage + 1));
		}

		internal void MoveToPreviousPage()
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
