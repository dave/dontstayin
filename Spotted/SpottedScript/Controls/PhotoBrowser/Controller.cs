using System;
using System.DHTML;
using Sys;
using Sys.UI;
using Sys.Net;
using PaginationControl2Controller = SpottedScript.Controls.PaginationControl2.Controller;

namespace SpottedScript.Controls.PhotoBrowser
{
	public class Controller : PhotoBrowsingUsingKeysControl
	{
		private View view;
		private TableCellElement[] cells;
		private int iconsPerPage;
		private int iconsPerRow;
		private int iconSize;
		private PhotoProvider photoProvider;
		internal PhotoProvider PhotoProvider { get { return photoProvider; } set { photoProvider = value; } }
		internal PaginationControl2Controller PaginationControl { get { return view.uiPaginationControl; } }
		private int selectedIndex;
		internal int SelectedIndex { get { return selectedIndex; } set { selectedIndex = value; } }

		internal EventHandler OnChangePhoto;
		internal EventHandler OnChangePhotoSet;

		public Controller(View view)
			: base(new DOMElement[] { view.uiPhotoRepeaterContainer })
		{
			this.view = view;
			this.iconsPerRow = int.ParseInvariant(view.uiIconsPerRow.Value);
			this.iconSize = int.ParseInvariant(view.uiIconSize.Value);
			this.iconsPerPage = int.ParseInvariant(view.uiIconsPerPage.Value);

			view.uiPaginationControl.OnPageChanged = new EventHandler(loadBrowserItems);

			cells = new TableCellElement[iconsPerPage];
			for (int i = 0; i < cells.Length; i++)
			{
				cells[i] = (TableCellElement)Document.GetElementById(view.uiTableCellsPrefix.Value + i);
			}

			for (int i = 0; i < cells.Length; i++)
			{
				// whole contents of the data cell
				DomEvent.AddHandler(cells[i].ChildNodes[0], "click", new DomEventHandler(photoClick));
			}

			this.SelectedIndex = highlightedCellIndex();

			this.OnPhotoNextClick = MoveToNextPhoto;
			this.OnPhotoPrevClick = MoveToPreviousPhoto;
			this.OnPhotoUpClick = MoveToPhotoAbove;
			this.OnPhotoDownClick = MoveToPhotoBelow;
			this.OnArrowKeyPress = arrowKeyPress;
		}

		private void arrowKeyPress(object source, EventArgs e)
		{
			// hides the blow up span on a photo
			//Script.Eval((string)cells[this.SelectedIndex].ChildNodes[0].ChildNodes[0].GetAttribute("onmouseout"));
		}

		private void photoClick(DomEvent e)
		{
			e.PreventDefault();

			for (int i = 0; i < cells.Length; i++)
			{
				//                                  Image  Div(?)     TableCell
				if (cells[i] == (TableCellElement)e.Target.ParentNode.ParentNode)
				{
					SelectedIndex = i;
					break;
				}
			}

			highlightCell();

			if (OnChangePhoto != null)
				OnChangePhoto(this, new IntEventArgs(SelectedIndex));

		}

		private int highlightedCellIndex()
		{
			// unhighlight the previously highlighted image - more precise way to do this?
			for (int i = 0; i < cells.Length; i++)
			{
				if (cells[i].ClassName == "PhotoBrowserCellHighlight")
					return i;
			}
			return 0;
		}

		private void highlightCell()
		{
			if (!selectedIndexValid())
				correctSelectedIndex();

			// unhighlight the previously highlighted image - more precise way to do this?
			for (int i = 0; i < cells.Length; i++)
			{
				cells[i].ClassName = "";
				ImageElement image = (ImageElement)cells[i].ChildNodes[0].ChildNodes[1];
				if (image.ClassName != "PhotoBrowserImage")
				{
					image.ClassName = "PhotoBrowserImage";
					image.Style.Width = iconSize.ToString() + "px";
					image.Style.Height = iconSize.ToString() + "px";
				}

				ImageElement imageBlowUp = (ImageElement)cells[i].ChildNodes[0].ChildNodes[2];
				if (imageBlowUp.ClassName != "PhotoBrowserImage")
				{
					imageBlowUp.ClassName = "PhotoBrowserImage";
					imageBlowUp.Style.MarginTop = "0px";
					imageBlowUp.Style.MarginLeft = "0px";
				}
				//((ImageElement)cells[i].ChildNodes[0].ChildNodes[1]).ClassName = "PhotoBrowserImage";
			}

			cells[SelectedIndex].ClassName = "PhotoBrowserCellHighlight";
			ImageElement imageHilight = (ImageElement)cells[SelectedIndex].ChildNodes[0].ChildNodes[1];
			imageHilight.ClassName = "PhotoBrowserImageHighlight";
			imageHilight.Style.Width = (iconSize - 2).ToString() + "px";
			imageHilight.Style.Height = (iconSize - 2).ToString() + "px";

			ImageElement imageHilightBlowUp = (ImageElement)cells[SelectedIndex].ChildNodes[0].ChildNodes[2];
			imageHilightBlowUp.ClassName = "PhotoBrowserImageHighlight";
			imageHilightBlowUp.Style.MarginTop = "-1px";
			imageHilightBlowUp.Style.MarginLeft = "-1px";
		}

		bool photoSetIsLoadingFromServer = false;
		bool firstLoad = true;
		public void DoPostLoadPhotoSetActions(object sender, EventArgs e)
		{
			photoSetIsLoadingFromServer = false;
			if (!firstLoad)
			{
				setBrowserPhotos();
				correctSelectedIndex();
				highlightCell();
			}
			firstLoad = false;

			view.uiPaginationControl.LastPage = PhotoProvider.CurrentPhotoSet.lastPage;

			if (OnChangePhotoSet != null)
				OnChangePhotoSet(this, new PhotoSetEventArgs(PhotoProvider.CurrentPhotoSet.photos, SelectedIndex));
		}
		public void PhotoSetIsLoadingFromServer(object sender, EventArgs e)
		{
			photoSetIsLoadingFromServer = true;
		}

		private void setBrowserPhotos()
		{
			//Script.Literal("debugger");
			// first of all set all photos black
			for (int i = 0; i < cells.Length; i++)
			{
				if (i < PhotoProvider.CurrentPhotoSet.photos.Length)
				{
					((ImageElement)cells[i].ChildNodes[0].ChildNodes[1]).Src = "/gfx/1pix.gif";
				}
				else
					break;
			}

			for (int i = 0; i < cells.Length; i++)
			{
				if (i < PhotoProvider.CurrentPhotoSet.photos.Length)
				{
					// set the photo info
					cells[i].Style.Display = "";
					setRolloverMouseOverText(i);
					((ImageElement)cells[i].ChildNodes[0].ChildNodes[1]).Src = PhotoProvider.CurrentPhotoSet.photos[i].iconPath;

					ImageElement blowUp = ((ImageElement)cells[i].ChildNodes[0].ChildNodes[2]);
					blowUp.Src = PhotoProvider.CurrentPhotoSet.photos[i].thumbPath;
					blowUp.Style.Width = PhotoProvider.CurrentPhotoSet.photos[i].thumbWidth.ToString() + "px";
					blowUp.Style.Height = PhotoProvider.CurrentPhotoSet.photos[i].thumbHeight.ToString() + "px";
					blowUp.Style.Top = (-(PhotoProvider.CurrentPhotoSet.photos[i].thumbHeight - 75) / 2).ToString() + "px";
					blowUp.Style.Left = (-(PhotoProvider.CurrentPhotoSet.photos[i].thumbWidth - 75) / 2).ToString() + "px";
				}
				else
				{
					cells[i].Style.Display = "none";
				}
			}


			//for (int i = iconsPerRow; i < iconsPerPage; i += iconsPerRow)
			//{
			//    TableCellElement t = cells[i];
			//    if (cells[i].Style.Display == "none")
			//    {
			//        t.ParentNode.Style.Display = "none";
			//    }
			//    else
			//    {
			//        t.ParentNode.Style.Display = "";
			//    }
			//}

		}

		private void loadBrowserItems(object source, EventArgs e)
		{
			PhotoProvider.LoadPhotos(((IntEventArgs)e).value);
		}

		private void moveToSelectedIndex()
		{
			highlightCell();
			if (OnChangePhoto != null)
				OnChangePhoto(this, new IntEventArgs(SelectedIndex));
		}

		#region validate selectedIndex
		private bool selectedIndexValid()
		{
			return SelectedIndex >= 0 && SelectedIndex < PhotoProvider.CurrentPhotoSet.photos.Length;
		}
		private void correctSelectedIndex()
		{
			if (SelectedIndex < 0) SelectedIndex = 0;
			else if (SelectedIndex >= PhotoProvider.CurrentPhotoSet.photos.Length) SelectedIndex = PhotoProvider.CurrentPhotoSet.photos.Length - 1;
		}
		#endregion

		#region moveTo photo event handlers
		internal void MoveToNextPhoto(object source, EventArgs e)
		{
			if (!photoSetIsLoadingFromServer)
			{
				SelectedIndex++;
				if (SelectedIndex >= PhotoProvider.CurrentPhotoSet.photos.Length || PhotoProvider.CurrentPhotoSet.photos[SelectedIndex] == null)
				{
					SelectedIndex = 0;
					view.uiPaginationControl.MoveToNextPage();
				}
				else
				{
					moveToSelectedIndex();
				}
			}
		}
		internal void MoveToPreviousPhoto(object source, EventArgs e)
		{
			if (!photoSetIsLoadingFromServer)
			{
				SelectedIndex--;
				if (SelectedIndex < 0)
				{
					SelectedIndex += cells.Length;
					view.uiPaginationControl.MoveToPreviousPage();
				}
				else
				{
					moveToSelectedIndex();
				}
			}
		}
		internal void MoveToPhotoAbove(object source, EventArgs e)
		{
			SelectedIndex -= iconsPerRow;
			if (SelectedIndex < 0)
			{
				SelectedIndex += cells.Length;
				view.uiPaginationControl.MoveToPreviousPage();
			}
			else
			{
				moveToSelectedIndex();
			}
		}
		internal void MoveToPhotoBelow(object source, EventArgs e)
		{
			// if it's the last cell of an incomplete set of photos (i.e. last page)
			// or would cause index to be out of range of cells
			if (SelectedIndex == PhotoProvider.CurrentPhotoSet.photos.Length - 1 || SelectedIndex + iconsPerRow >= cells.Length)
			{
				setSelectedIndexToBeInFirstRow();
				view.uiPaginationControl.MoveToNextPage();
			}
			else
			{
				SelectedIndex += iconsPerRow;
				moveToSelectedIndex();
			}
		}
		#endregion

		void setSelectedIndexToBeInFirstRow()
		{
			while (SelectedIndex < 0) SelectedIndex += iconsPerRow;
			while (SelectedIndex >= iconsPerRow) SelectedIndex -= iconsPerRow;
		}

		internal void RolloverMouseOverTextChanged(object o, EventArgs e)
		{
			int index = ((IntEventArgs)e).value;
			setRolloverMouseOverText(index);
		}

		void setRolloverMouseOverText(int index)
		{
			cells[index].ChildNodes[0].ChildNodes[0].SetAttribute("rolloverMouseOverText", PhotoProvider.CurrentPhotoSet.photos[index].rolloverMouseOverText);
		}
	}
}
