using System.XML;
using System;
using Spotted.System.Text;
using SpottedScript.Controls.ChatClient.Shared;
using System.DHTML;

namespace SpottedScript.Controls.ChatClient.Items
{
	public class Logout : Alert
	{
		public Logout(AlertStub alertStub, Controller parent, int serverRequestIndex)
			: base(alertStub, parent, serverRequestIndex)
		{
		}
		protected override string getHtml()
		{
			return "logged out";
		}
	}
}
