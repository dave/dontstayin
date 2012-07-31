using System.XML;
using System;
using Spotted.System.Text;
using SpottedScript.Controls.ChatClient.Shared;
using System.DHTML;

namespace SpottedScript.Controls.ChatClient.Items
{
	public class Alert : Newable, IHasPostingUsr
	{
		public int UsrK;
		string NickName;
		string StmuParams;
		string Pic;
		string AnyPic;
		string PicID;
		string PinID;
		DOMElement PicElement;
		public bool ShowChatButton;

		public Alert(AlertStub alertStub, Controller parent, int serverRequestIndex)
			: base(alertStub, parent, serverRequestIndex)
		{
			NickName = alertStub.nickName;
			StmuParams = alertStub.stmuParams;
			UsrK = alertStub.usrK;
			Pic = alertStub.pic;
			AnyPic = Pic == "0" ? "00000000-0000-0000-b916-000000000001" : Pic;

			IsInNewSection = false;
			IsTopOfSection = false;
			IsBottomOfSection = false;

			PicID = ClientID + "_Pic";
			PinID = ClientID + "_Pin";
		}

		public override void InitialiseElements()
		{
			initialiseElementsInternal(true);
		}
		protected override void initialiseElementsInternal(bool setElementsInitialisedFlagOnFinish)
		{
			base.initialiseElementsInternal(false);
			PicElement = showPic ? Document.GetElementById(PicID) : null;

			if (setElementsInitialisedFlagOnFinish)
				elementsInitialised = true;
		}
		override protected void updateUI()
		{
			updateMessageHolder();
		}
		void updateMessageHolder()
		{
			if (elementsInitialised)
			{
				string cssClass = "ChatClientMessageHolder";
				cssClass += IsInNewSection ? " ChatClientMessageHolderNew" : " ChatClientMessageHolderOld";
				cssClass += IsTopOfSection ? " ChatClientMessageHolderTop" : "";
				cssClass += IsBottomOfSection ? " ChatClientMessageHolderBot" : "";
				cssClass += " ClearAfter";
				ItemElement.ClassName = cssClass;
			}
		}

		protected virtual string getHtml()
		{
			return "";
		}
		protected virtual bool showPic
		{
			get
			{
				return true;
			}
		}

		public override void AppendHtml(StringBuilder sb)
		{
			int size = 33;// IsTopOfNewSection ? 100 : (IsInNewSection ? 66 : 33);
			
			sb.Append(@"<div");
			sb.AppendAttribute("id", ClientID);
			sb.Append(@" class=""ChatClientMessageHolder");
			sb.Append(IsInNewSection ? " ChatClientMessageHolderNew" : " ChatClientMessageHolderOld");
			sb.Append(IsTopOfSection ? " ChatClientMessageHolderTop" : "");
			sb.Append(IsBottomOfSection ? " ChatClientMessageHolderBot" : "");
			sb.Append(@" ClearAfter"">");

			if (showPic)
			{
				sb.Append(@"<a");
				sb.AppendAttribute("href", "/members/" + NickName.ToLowerCase());
				sb.AppendAttribute("onmouseover", "stmu('" + Pic + "'," + StmuParams + ");");
				sb.AppendAttribute("onmouseout", "htm();");
				sb.Append(@">");

				sb.Append(@"<img");
				sb.AppendAttribute("id", PicID);
				sb.AppendAttribute("src", Misc.GetPicUrlFromGuid(AnyPic));
				sb.AppendAttribute("width", size.ToString());
				sb.AppendAttribute("height", size.ToString());
				sb.AppendAttribute("hspace", "0");
				sb.AppendAttribute("class", "ChatClientMessagePic");
				sb.AppendAttribute("align", "left");
				sb.Append(@" />");

				sb.Append(@"</a>");
			}

			if (ShowChatButton)
			{
				sb.Append("<div class=\"ChatClientMessageChatButtonHolder\" align=\"right\">");
				{
					if (ShowChatButton)
					{
						sb.Append("<button");
						sb.AppendAttribute("class", "ChatClientMessageChatButton");
						sb.AppendAttribute("onclick", "chatClientPinRoom('" + RoomGuid + "', null, false);return false;");
						sb.Append(">chat</button>");
					}
				}
				sb.Append("</div>");
			}

			sb.Append(@"<div class=""ChatClientMessageHeader"">");

			sb.Append(@"<a");
			sb.AppendAttribute("href", "/members/" + NickName.ToLowerCase());
			sb.AppendAttribute("onmouseover", "stmu('" + Pic + "'," + StmuParams + ");");
			sb.AppendAttribute("onmouseout", "htm();");
			sb.Append(@">");
			sb.Append(NickName);
			sb.Append(@"</a> ");

			sb.Append(getHtml());

			sb.Append(@"</div>");//MessageHeader

			sb.Append(@"</div>");//MessageHolder

		}


		#region IHasPostingUsr Members

		int IHasPostingUsr.PostingUsrK
		{
			get { return UsrK; }
		}

		#endregion
	}
	
}
