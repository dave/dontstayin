using System.XML;
using System;
using Spotted.System.Text;
using SpottedScript.Controls.ChatClient.Shared;
using System.DHTML;

namespace SpottedScript.Controls.ChatClient.Items
{
	public class Message : Newable, IHasPostingUsr
	{
		public int UsrK;
		public string NickName;
		public string StmuParams;
		string Pic;
		string ChatPic;
		string Text;
		string PinRoomGuid;
		string RoomGuid;
		string AnyPic;
		public string AnyChatPic;
		string PicID;
		string PinID;
		string MessageBodyID;
		DOMElement PicElement;
		DOMElement MessageBodyElement;
		public bool ShowChatButton;
		public bool ShowReadButton;
		public bool ShowSubHead;

		public Message(MessageStub messageStub, Controller parent, int serverRequestIndex)
			: base(messageStub, parent, serverRequestIndex)
		{
			NickName = messageStub.nickName;
			StmuParams = messageStub.stmuParams;
			UsrK = messageStub.usrK;
			Pic = messageStub.pic;
			ChatPic = messageStub.chatPic;
			Text = messageStub.text;
			PinRoomGuid = messageStub.pinRoomGuid;
			RoomGuid = messageStub.roomGuid;
			AnyPic = Pic == "0" ? "00000000-0000-0000-b916-000000000001" : Pic;
			AnyChatPic = ChatPic == "0" ? "00000000-0000-0000-b916-000000000005" : ChatPic;
			
			IsInNewSection = false;
			IsTopOfSection = false;
			IsBottomOfSection = false;

			PicID = ClientID + "_Pic";
			PinID = ClientID + "_RoomPin";
			MessageBodyID = ClientID + "_MessageBody";
		}

		public override void InitialiseElements()
		{
			initialiseElementsInternal(true);
		}
		protected override void initialiseElementsInternal(bool setElementsInitialisedFlagOnFinish)
		{
			base.initialiseElementsInternal(false);
			PicElement = Document.GetElementById(PicID);
			MessageBodyElement = Document.GetElementById(MessageBodyID);

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

		protected virtual string getReadButtonUrl()
		{
			return "";
		}
		protected virtual string getHtmlAfterBody()
		{
			return "";
		}
		protected virtual string getHtmlAfterName()
		{
			return "";
		}
		protected virtual string getSubhead()
		{
			return "";
		}

		public override string GetRoomGuidForChatClickAction()
		{
			return PinRoomGuid.Length == 0 ? RoomGuid : PinRoomGuid;
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

			if (ShowChatButton || ShowReadButton)
			{
				sb.Append("<div class=\"ChatClientMessageChatButtonHolder\" align=\"right\">");
				{
					if (ShowChatButton)
					{
						sb.Append("<button");
						sb.AppendAttribute("class", "ChatClientMessageChatButton");
						sb.AppendAttribute("onclick", "chatClientPinRoom('" + GetRoomGuidForChatClickAction() + "', null, false);return false;");
						sb.Append(">chat</button>");
					}
					
					if (ShowChatButton && ShowReadButton)
						sb.Append("<br />");
					
					if (ShowReadButton)
					{
						sb.Append("<button");
						sb.AppendAttribute("class", "ChatClientMessageChatButton");
						sb.AppendAttribute("onclick", "event.cancelBubble = true; if (event.stopPropagation) { event.stopPropagation(); } document.location = \"" + getReadButtonUrl() + "\";return false;");
						sb.Append(">read</button>");
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
			sb.Append(@"</a>");

			string htmlAfterName = getHtmlAfterName();


			if (RoomGuid != "AQEFAAAAAAUAAAAAvVaVmQ")
			{
				int age = GetAgeInMinutes();
				if (age >= 5)
				{
					sb.Append(" (");
					if (age > 525600)
					{
						int years = (int)Math.Floor(age / 525600);
						sb.Append(years.ToString());
						sb.Append(" yr");
						sb.Append(years == 1 ? "" : "s");
					}
					else if (age > 43200)
					{
						int months = (int)Math.Floor(age / 43200);
						sb.Append(months.ToString());
						sb.Append(" month");
						sb.Append(months == 1 ? "" : "s");
					}
					else if (age > 10080)
					{
						int weeks = (int)Math.Floor(age / 10080);
						sb.Append(weeks.ToString());
						sb.Append(" wk");
						sb.Append(weeks == 1 ? "" : "s");
					}
					else if (age > 1440)
					{
						int days = (int)Math.Floor(age / 1440);
						sb.Append(days.ToString());
						sb.Append(" day");
						sb.Append(days == 1 ? "" : "s");
					}
					else if (age > 60)
					{
						int hrs = (int)Math.Floor(age / 60);
						sb.Append(hrs.ToString());
						sb.Append(" hr");
						sb.Append(hrs == 1 ? "" : "s");
					}
					else
					{
						sb.Append(age.ToString());
						sb.Append(" min");
						sb.Append(age == 1 ? "" : "s");
					}
					sb.Append(" ago) ");
				}
			}

			if (htmlAfterName.Length > 0)
			{
				sb.Append(htmlAfterName);	
			}

			sb.Append(@"</div>");//MessageHeader

			if (ShowSubHead && getSubhead().Length > 0)
			{
				sb.Append(@"<div");
				sb.Append(@" class=""ChatClientMessageSubHead"">");
				sb.Append(getSubhead());
				sb.Append(@"</div>");//MessageBody
			}

			sb.Append(@"<div");
			sb.AppendAttribute("id", MessageBodyID);
			sb.Append(@" class=""ChatClientMessageBody"">");
			sb.Append(Text);
			sb.Append(getHtmlAfterBody());
			sb.Append(@"</div>");//MessageBody

			sb.Append(@"</div>");//MessageHolder

		}


		#region IHasPostingUsr Members

		int IHasPostingUsr.PostingUsrK
		{
			get { return UsrK; }
		}

		#endregion
	}
	public enum MessagePinLocation
	{
		AfterName = 1,
		AfterSubhead = 2,
		AfterBody = 3
	}
	
}
