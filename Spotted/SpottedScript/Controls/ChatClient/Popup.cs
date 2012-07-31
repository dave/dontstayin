using System.DHTML;
using SpottedScript.Controls.ChatClient.Shared;
using Sys;
using SpottedScript.Controls.ChatClient.Items;
using System;
using Sys.UI;
using JQ;
using ScriptSharpLibrary;
using HtmlItem = SpottedScript.Controls.ChatClient.Items.Html;


namespace SpottedScript.Controls.ChatClient
{
	public class PopupArea
	{
		public Popup[] Popups;
		DOMElement areaHolder;
		public static int PopupWidth = 250;
		public static int PopupHeight = 170;
		public bool Animate;
		
		#region PopupArea
		public PopupArea(View view)
		{
			Popups = (Popup[])new Array();
			Animate = bool.Parse(view.InitAnimatePopups.Value);

			
			areaHolder = Document.GetElementById("ChatClientPopupAreaHolder");
			areaHolder.Style.Width = PopupWidth.ToString() + "px";
			
		}
		#endregion

		#region Add
		public void Add(Popup popup)
		{
			popup.Area = this;

			int positionIndex = Popups.Length;
			Popups[positionIndex] = popup;
			
			areaHolder.AppendChild(popup.Holder);
			
			DomEvent.AddHandler(popup.Holder, "mouseover", onMouseOver);
			DomEvent.AddHandler(popup.Holder, "mouseout", onMouseOut);
			
			popup.RepositionImmediate(positionIndex);

			popup.Show();
			
			popup.SetRemoveTimeout(10000);
		}
		#endregion
		#region Remove
		bool mouseIsOverPopup;
		bool cancelMouseOut;
		public void Remove(Popup popup, bool force, bool forceNoAnimation)
		{
			if (!mouseIsOverPopup || force)
				RemoveNow(popup, forceNoAnimation);
		}
		void RemoveNow(Popup popup, bool forceNoAnimation)
		{
			if (!popup.Removed)
			{
				popup.Hide(forceNoAnimation);
				popup.Removed = true;

				Popup[] popupsNew = (Popup[])new Array();

				for (int i = 0; i < Popups.Length; i++)
				{
					if (Popups[i].Id != popup.Id)
						popupsNew[popupsNew.Length] = Popups[i];
				}

				Popups = popupsNew;

				repositionPopups();
			}
		}
		void RemoveAllThatNeedRemoval()
		{
			Popup[] popupsNew = (Popup[])new Array();

			for (int i = 0; i < Popups.Length; i++)
			{
				if (Popups[i].NeedsRemoval)
					Popups[i].Hide(false);
				else
					popupsNew[popupsNew.Length] = Popups[i];
			}

			Popups = popupsNew;

			repositionPopups();
		}
		void onMouseOver(DomEvent e)
		{
			mouseIsOverPopup = true;
			cancelMouseOut = true;
		}
		void onMouseOut(DomEvent e)
		{
			cancelMouseOut = false;
			Window.SetTimeout(onMouseOutAfterDelay, 100);
		}
		void onMouseOutAfterDelay()
		{
			if (!cancelMouseOut)
			{
				mouseIsOverPopup = false;
				mouseOut();
			}
		}
		void mouseOut()
		{
			RemoveAllThatNeedRemoval();
		}
		#endregion

		void repositionPopups()
		{
			for (int i = 0; i < Popups.Length; i++)
			{
				Popups[i].RepositionSlide(i);
			}
		}
	}
	public class Popup
	{
		public string Title;
		public DivElement Holder;
		public DivElement ItemsList;
		public EventHandler ClickAction = null;
		public string RoomGuid;
		public PopupArea Area;
		public string Id;
		JQueryObject jHolder;
		public bool NeedsRemoval;
		public bool Removed;
		public string RoomGuidForClickAction;
		public bool HasRelevantItems;

		#region Popup
		public Popup(Controller controller, string title, Room room, HtmlItem[] items)
		{
			Title = title;
			RoomGuid = room.Guid;
			Removed = false;
			RoomGuidForClickAction = room.Guid;
			//RoomGuidForClickAction = item.GetRoomGuidForChatClickAction();


			#region //Holder
			Holder = (DivElement)Document.CreateElement("div");
			Holder.ClassName = "ChatClientPopup";
			Holder.Style.Width = PopupArea.PopupWidth.ToString() + "px";
			Holder.Style.Height = PopupArea.PopupHeight.ToString() + "px";
			
			#region //HeaderDiv
			DOMElement header = (DOMElement)Document.CreateElement("div");
			header.ClassName = "ChatClientPopupHeader";
			header.Style.Overflow = "hidden";

			{
				#region //Close button
				DivElement div = (DivElement)Document.CreateElement("div");
				div.ClassName = "ChatClientPopupCloseLinkHolder";

				{
					AnchorElement link = (AnchorElement)Document.CreateElement("a");
					link.InnerHTML = "Close";
					link.Href = "";
					DomEvent.AddHandler(link, "click", new DomEventHandler(closeButtonClick));
					div.AppendChild(link);
				}
				header.AppendChild(div);
				#endregion
			}

			{
				#region //TitleDiv
				DivElement div = (DivElement)Document.CreateElement("div");
				div.ClassName = "ChatClientPopupTitle";
				div.InnerHTML = Title;
				header.AppendChild(div);
				#endregion
			}

			Holder.AppendChild(header);
			
			#endregion

			#region //ItemsDiv
			{
				if (items != null)
				{
					ItemsList = (DivElement)Document.CreateElement("div");
					ItemsList.ClassName = "ChatClientPopupItemsList";

					DomEvent.AddHandler(ItemsList, "click", new DomEventHandler(holderClick));
					DomEvent.AddHandler(ItemsList, "mouseover", new DomEventHandler(holderMouseOver));
					DomEvent.AddHandler(ItemsList, "mouseout", new DomEventHandler(holderMouseOut));

					bool hasMultipleRelevantItems = false;
					for (int i = 0; i < items.Length; i++)
					{
						HtmlItem item = (HtmlItem)items[i];

						#region // if the item is posted by the current user, we never want to show a popup
						if (item is IHasPostingUsr)
						{
							if (((IHasPostingUsr)item).PostingUsrK == controller.UsrK)
								continue;
						}
						#endregion

						if (HasRelevantItems)
						{
							hasMultipleRelevantItems = true;
							break;
						}

						HasRelevantItems = true;
					}
					if (HasRelevantItems)
					{
						for (int i = 0; i < items.Length; i++)
						{
							HtmlItem item = (HtmlItem)items[i];

							#region // if the item is posted by the current user, we never want to show a popup
							if (item is IHasPostingUsr)
							{
								if (((IHasPostingUsr)item).PostingUsrK == controller.UsrK)
									continue;
							}
							#endregion

							if (!hasMultipleRelevantItems)
								RoomGuidForClickAction = item.GetRoomGuidForChatClickAction();

							#region //bodge it so it displays correctly - remember to save all things we change
							int previousInstance = item.Instance;
							bool previousNewStatus = false;
							if (item is Newable)
							{
								previousNewStatus = ((Newable)item).IsInNewSection;
								((Newable)item).IsInNewSection = false;
							}
							item.Instance = 2;

							bool previousShowChatButton = false;
							if (!hasMultipleRelevantItems && item is Message) // we only hide the chat button if there's only one item shown. for multi-item popups we want to leave the chat buttons as they were.
							{
								previousShowChatButton = ((Message)item).ShowChatButton;
								((Message)item).ShowChatButton = false;
							}
							#endregion

							DOMElement itemNode = Document.CreateElement("span");
							itemNode.InnerHTML = item.ToHtml();

							if (ItemsList.HasChildNodes())
								ItemsList.InsertBefore(itemNode, ItemsList.ChildNodes[0]);
							else
								ItemsList.AppendChild(itemNode);

							#region //restore all the things we bodged

							item.Instance = previousInstance;

							if (item is Newable)
								((Newable)item).IsInNewSection = previousNewStatus;
							
							if (!hasMultipleRelevantItems && item is Message) // we only hide the chat button if there's only one item shown. for multi-item popups we want to leave the chat buttons as they were.
								((Message)item).ShowChatButton = previousShowChatButton;

							#endregion

						}
						Holder.AppendChild(ItemsList);
					}
				}
			}
			#endregion

			#endregion

			Id = Math.Random().ToString();
			jHolder = JQueryAPI.JQuery(Holder);
		}
		#endregion

		public void SetRemoveTimeout(int timeout)
		{
			Window.SetTimeout(removeAfterTimeout, timeout);
		}
		void removeAfterTimeout()
		{
			NeedsRemoval = true;
			Area.Remove(this, false, false);
		}

		int getTop(int positionIndex)
		{
			return 0 - ((positionIndex + 1) * PopupArea.PopupHeight);
		}
		public void RepositionImmediate(int positionIndex)
		{
			Holder.Style.Top = getTop(positionIndex).ToString() + "px";
		}
		public void RepositionSlide(int positionIndex)
		{
			if (Area.Animate)
			{
				Dictionary options = new Dictionary();
				options["top"] = getTop(positionIndex).ToString() + "px";
				jHolder.animate(options, 500, "easeOutQuint", null);
			}
			else
			{
				RepositionImmediate(positionIndex);
			}
		}

		#region Show
		public void Show()
		{
			if (Area.Animate)
			{
				Dictionary options = new Dictionary();
				options["direction"] = "down";
				options["easing"] = "easeOutQuint";
				jHolder.show("drop", options, 500, null);
			}
			else
				Holder.Style.Display = "block";
		}
		#endregion
		#region Hide
		public void Hide(bool forceNoAnimation)
		{
			if (Area.Animate && !forceNoAnimation)
			{
				Dictionary options = new Dictionary();
				options["direction"] = "down";
				options["easing"] = "easeOutQuint";
				jHolder.hide("drop", options, 500, null);
			}
			else
				Holder.Style.Display = "none";
		}
		#endregion

		#region holderClick
		void holderClick(DomEvent e)
		{
			if (Area.Animate)
			{
				try
				{
					Dictionary options = new Dictionary();
					options["to"] = "#ChatClient_MessagesMain";
					jHolder.effect("transfer", options, 500, null);
				}
				catch (Exception ex)
				{
					//JQueryAPI.JQuery(""
				}

			}

			if (ClickAction != null)
				ClickAction(RoomGuidForClickAction, null);

			Area.Remove(this, true, true);

			e.PreventDefault();
		}
		#endregion
		#region holderMouseOver
		void holderMouseOver(DomEvent e)
		{
			itemsListCancelMouseOut = true;
			if (ItemsList != null)
				ItemsList.ClassName = "ChatClientPopupItemsList ChatClientPopupItemsListMouseOver";
			e.PreventDefault();
		}
		#endregion
		#region holderMouseOut
		void holderMouseOut(DomEvent e)
		{
			itemsListCancelMouseOut = false;
			Window.SetTimeout(itemsListMouseOutAfterDelay, 0);
			e.PreventDefault();
		}
		bool itemsListCancelMouseOut = false;
		void itemsListMouseOutAfterDelay()
		{
			if (ItemsList != null && !itemsListCancelMouseOut)
				ItemsList.ClassName = "ChatClientPopupItemsList";
		}
		#endregion
		#region closeButtonClick
		void closeButtonClick(DomEvent e)
		{
			Area.Remove(this, true, false);

			e.PreventDefault();
		}
		#endregion
	}
}
