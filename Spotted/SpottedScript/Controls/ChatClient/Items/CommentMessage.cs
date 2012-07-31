using System.XML;
using System;
using Spotted.System.Text;
using SpottedScript.Controls.ChatClient.Shared;
using System.DHTML;

namespace SpottedScript.Controls.ChatClient.Items
{
	public class CommentMessage : Message
	{
		public string Url;
		public string Subject;
		public CommentMessage(CommentMessageStub commentStub, Controller parent, int serverRequestIndex)
			: base(commentStub, parent, serverRequestIndex)
		{
			Url = commentStub.url;
			Subject = commentStub.subject;
			ShowReadButton = Url.Length > 0;
		}

		

		protected override string getReadButtonUrl()
		{
			return Url;
		}
		protected override string getHtmlAfterBody()
		{
		//	if (Url.Length > 0)
		//		return " <a href=\"" + Url + "\" class=\"ChatClientCommentMessageMoreLink\">(more)</a>";
		//	else
				return "";
		}
		protected override string getSubhead()
		{
			if (Subject.Length > 0 && Url.Length > 0)
				return "<a href=\"" + Url + "\" class=\"ChatClientCommentMessageSubhead\">" + Subject + "</a>";
			else
				return Subject;
		}

	}
}
