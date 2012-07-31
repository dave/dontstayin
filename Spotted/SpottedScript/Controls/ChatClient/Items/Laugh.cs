using System.XML;
using System;
using Spotted.System.Text;
using SpottedScript.Controls.ChatClient.Shared;
using System.DHTML;

namespace SpottedScript.Controls.ChatClient.Items
{
	public class Laugh : CommentMessage
	{
		public Laugh(LaughStub laughStub, Controller parent, int serverRequestIndex)
			: base(laughStub, parent, serverRequestIndex)
		{
		}
		protected override string getHtmlAfterName()
		{
			return " laughed at:";
		}
		public override string GetRoomGuidForChatClickAction()
		{
			return RoomGuid;
		}
	}
}
