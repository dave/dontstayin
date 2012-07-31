
using System.Html;
using System.Collections.Generic;
using System;
using Js.Library;
using jQueryApi;

namespace Js.ClientControls
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
			Dictionary<object, object> divAsArray = (Dictionary<object, object>)(object)div;
			divAsArray["value"] = pair.Value;
			if (pair.Key.IndexOf("<") > -1)
				div.InnerHTML = pair.Key;
			else
				div.InnerHTML = "<div class=\"DefaultPopupMenuItem\">" + pair.Key + "</div>";
			container.AppendChild(div);
			jQuery.FromElement(div).Click(ItemClickHandler);
			jQuery.FromElement(div).MouseOver(ItemMouseEnterHandler);
			
		}
		private void ItemClickHandler(jQueryEvent e)
		{
			if (ItemClick != null) ItemClick(CurrentlyHighlightedItem);
		}
		private void ItemMouseEnterHandler(jQueryEvent e)
		{
			Element el = e.Target;
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
				jQuery.FromElement(container.ChildNodes[0]).Unbind("click").Unbind("mouseover");
				//DomEvent.ClearHandlers(container.ChildNodes[0]);
				container.RemoveChild(container.ChildNodes[0]);
			}
			indexOfCurrentlyHighlightedItem = -1;
			container.Style.Display = "none";
		}
		public void Show(Element anchor, int minWidth, int maxWidth, int topOffset, int leftOffset, bool rightAlign)
		{
			container.Style.Display = container.ChildNodes.Length > 0 ? "" : "none";

			if (container.ChildNodes.Length > 0)
			{
				
				jQueryObject jq = jQuery.FromElement(anchor);

				int anchorOffsetHeight = anchor == null ? 0 : anchor.OffsetHeight;
				int anchorOffsetWidth = anchor == null ? 0 : anchor.OffsetWidth;

				if (minWidth == 0 && maxWidth == 0)
				{
					minWidth = anchorOffsetWidth;
					maxWidth = anchorOffsetWidth;
				}
				
				int nudgeLeft = jQuery.Browser.MSIE ? -2 : 0;
				int nudgeTop = jQuery.Browser.MSIE ? -2 : 0;
				int nudgeWidth = jQuery.Browser.MSIE ? 0 : -2;
				
				container.Style.Left = jq.GetOffset().Left + nudgeLeft + leftOffset + "px";
				
				container.Style.Top = jq.GetOffset().Top + anchorOffsetHeight + nudgeTop - 1 + topOffset + "px";


				container.Style.Width = "auto";

				if (container.OffsetWidth < minWidth)
					container.Style.Width = minWidth + nudgeWidth + "px";
				else if (container.OffsetWidth > maxWidth)
					container.Style.Width = maxWidth + nudgeWidth + "px";
				if (rightAlign)
				{
					Trace.Write("" + jq.GetOffset().Left + ", " + nudgeLeft + ", " + anchor.ClientWidth + ", " + container.ClientWidth);
					container.Style.Left = (jq.GetOffset().Left + nudgeLeft + anchor.ClientWidth - container.ClientWidth) + "px";
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
				else { return ((Dictionary<object, object>)(object)CurrentlyHighlightedElement)["value"]; }
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
		int findPosLeft(Element el)
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
		int findPosTop(Element el)
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
