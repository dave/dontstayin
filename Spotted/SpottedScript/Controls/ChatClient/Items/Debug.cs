using Spotted.System.Text;
using SpottedScript.Controls.ChatClient.Shared;
using System;

namespace SpottedScript.Controls.ChatClient.Items
{
	public class Note : Html
	{
		string Text;

		public Note(string text, Controller parent, string roomGuid, int serverRequestIndex)
			: base(new ItemStub(Math.Round(Math.Random() * 100000).ToString(), ItemType.Error, "0", roomGuid), parent, serverRequestIndex)
		{
			this.Text = text;
		}

		public override void AppendHtml(StringBuilder sb)
		{
			sb.Append("<p>NOTE:<br />");
			sb.Append(Text);
			sb.Append("</p>");
		}
	}
	
}
