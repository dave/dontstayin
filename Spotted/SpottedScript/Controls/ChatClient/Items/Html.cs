using System.XML;
using System;
using Spotted.System.Text;
using SpottedScript.Controls.ChatClient.Shared;
using System.DHTML;

namespace SpottedScript.Controls.ChatClient.Items
{
	public class Html : Item
	{
		public int Instance;
		protected DOMElement ItemElement;
		protected bool elementsInitialised;

		protected string ClientID
		{
			get
			{
				return Parent.ClientID + "_ServerRequestIndex_" + ServerRequestIndex + "_Item_" + Guid + "_Instance_" + Instance;
			}
		}

		public Html(ItemStub itemStub, Controller parent, int serverRequestIndex)
			: base(itemStub, parent)
		{
			elementsInitialised = false;
			ServerRequestIndex = serverRequestIndex;
		}
		public virtual string GetRoomGuidForChatClickAction()
		{
			return RoomGuid;
		}
		public bool IsElementsInitialised
		{
			get
			{
				return elementsInitialised;
			}
		}
		public virtual void InitialiseElements()
		{
			initialiseElementsInternal(true);
		}
		protected virtual void initialiseElementsInternal(bool setElementsInitialisedFlagOnFinish)
		{
			ItemElement = Document.GetElementById(ClientID);

			if (setElementsInitialisedFlagOnFinish)
				elementsInitialised = true;
		}

		public string ToHtml()
		{
			StringBuilder sb = new StringBuilder();
			AppendHtml(sb);
			return sb.ToString();
		}
		public virtual void AppendHtml(StringBuilder sb)
		{
			sb.Append("<p>");
			sb.Append(Type.ToString());
			sb.Append(" - ");
			sb.Append(Guid);
			sb.Append("</p>");
		}
		
	}
}
