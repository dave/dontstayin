
using System.DHTML;
using Sys.UI;
using System;
using Sys;
using JQ;
using Utils;
namespace ScriptSharpLibrary
{
	public delegate void ItemClickDelegate(object value);
	public delegate void ItemHighlightedDelegate(object value);
	public class PopupMenu
	{
		public ItemClickDelegate ItemClick = null;
		public Action ItemHighlighted = null;
		DivElement container;
		string highlightedItemCssClass;
		public PopupMenu(string cssClass, string highlightedItemCssClass)
		{
			container = (DivElement) Document.CreateElement("DIV");
			container.ClassName = cssClass == null ? "PopupMenu" : cssClass;
			this.highlightedItemCssClass = highlightedItemCssClass == null ? "PopupMenuHighlightedItem" : highlightedItemCssClass;
			container.Style.Display = "none";
			Document.Body.AppendChild(container);
		}
		public void AddItem(KeyValuePair pair)
		{

			DivElement div = (DivElement)Document.CreateElement("DIV");
			Dictionary divAsArray = (Dictionary)(object)div;
			divAsArray["value"] = pair.Value;
			if (pair.Key.IndexOf("<") > -1)
				div.InnerHTML = pair.Key;
			else
				div.InnerHTML = "<div class=\"DefaultPopupMenuItem\">" + pair.Key + "</div>";
			container.AppendChild(div);
			DomEvent.AddHandler(div, "click", ItemClickHandler);
			DomEvent.AddHandler(div, "mouseover", ItemMouseEnterHandler);
			
		}
		private void ItemClickHandler(DomEvent e)
		{
			if (ItemClick != null) ItemClick(CurrentlyHighlightedItem);
		}
		private void ItemMouseEnterHandler(DomEvent e)
		{
			DOMElement el = e.Target;
			while (el.ParentNode != container)
			{
				el = el.ParentNode;
			}
			for (int i=0;i<container.ChildNodes.Length;i++)
			{
				if (container.ChildNodes[i] == el)
				{
					IndexOfCurrentlyHighlightedItem = i;
					return;
				}
			}
		}
		public void Clear()
		{
			while(container.ChildNodes.Length > 0)
			{
				DomEvent.ClearHandlers(container.ChildNodes[0]);
				container.RemoveChild(container.ChildNodes[0]);
			}
			indexOfCurrentlyHighlightedItem = -1;
			container.Style.Display = "none";
		}
		public void Show(DOMElement anchor, int minWidth, int maxWidth, int topOffset, int leftOffset, bool rightAlign)
		{
			container.Style.Display = container.ChildNodes.Length > 0 ? "" : "none";

			if (container.ChildNodes.Length > 0)
			{
				JQueryObject jq = JQueryAPI.JQuery(anchor);

				int anchorOffsetHeight = anchor == null ? 0 : anchor.OffsetHeight;
				int anchorOffsetWidth = anchor == null ? 0 : anchor.OffsetWidth;

				if (minWidth == 0 && maxWidth == 0)
				{
					minWidth = anchorOffsetWidth;
					maxWidth = anchorOffsetWidth;
				}
				
				int nudgeLeft = Browser.Agent == Browser.InternetExplorer ? -2 : 0;
				int nudgeTop = Browser.Agent == Browser.InternetExplorer ? -2 : 0;
				int nudgeWidth = Browser.Agent == Browser.InternetExplorer ? 0 : -2;
				
				container.Style.Left = jq.Offset().Left + nudgeLeft + leftOffset + "px";
				
				container.Style.Top = jq.Offset().Top + anchorOffsetHeight + nudgeTop - 1 + topOffset + "px";


				container.Style.Width = "auto";

				if (container.OffsetWidth < minWidth)
					container.Style.Width = minWidth + nudgeWidth + "px";
				else if (container.OffsetWidth > maxWidth)
					container.Style.Width = maxWidth + nudgeWidth + "px";
				if (rightAlign)
				{
					Trace.Write("" + jq.Offset().Left + ", " + nudgeLeft + ", " + anchor.ClientWidth + ", " + container.ClientWidth);
					container.Style.Left = (jq.Offset().Left + nudgeLeft + anchor.ClientWidth - container.ClientWidth) + "px";
				}
				
				
			}
		}
		public void Hide()
		{
			Script.Eval("try{htm();}catch(e){}");
			container.Style.Display = "none";
		}
		int indexOfCurrentlyHighlightedItem = -1;
		public int IndexOfCurrentlyHighlightedItem
		{
			get { return indexOfCurrentlyHighlightedItem < container.ChildNodes.Length ? indexOfCurrentlyHighlightedItem : -1; }
			set
			{
				
				if (CurrentlyHighlightedElement != null) { CurrentlyHighlightedElement.ClassName = ""; }
				indexOfCurrentlyHighlightedItem = (value + container.ChildNodes.Length) % container.ChildNodes.Length;
				if (CurrentlyHighlightedElement != null) { 
					CurrentlyHighlightedElement.ClassName = highlightedItemCssClass;
					if (ItemHighlighted != null) { ItemHighlighted(); }
				}
			}
		}
		public object CurrentlyHighlightedItem
		{
			get
			{
				if (CurrentlyHighlightedElement == null) { return null; }
				else { return ((Dictionary)(object)CurrentlyHighlightedElement)["value"]; }
			}
		}
		DivElement CurrentlyHighlightedElement
		{
			get
			{
				if (IndexOfCurrentlyHighlightedItem == -1) { return null; }
				else { return (DivElement)this.container.ChildNodes[IndexOfCurrentlyHighlightedItem]; }
			}
		}
		int findPosLeft(DOMElement el)
		{
			int curleft = 0;
			if (el.OffsetParent != null)
			{
				do
				{
					curleft += el.OffsetLeft;
					el = el.OffsetParent;
				} while (el != null);
			}
			return curleft;
		}
		int findPosTop(DOMElement el)
		{
			int curtop = 0;
			if (el.OffsetParent != null)
			{
				do
				{
					curtop += el.OffsetTop;
					el = el.OffsetParent;
				} while (el != null);
			}
			return curtop;
		}

		
	}
}
