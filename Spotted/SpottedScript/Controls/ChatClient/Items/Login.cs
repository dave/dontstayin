using System.XML;
using System;
using Spotted.System.Text;
using SpottedScript.Controls.ChatClient.Shared;
using System.DHTML;

namespace SpottedScript.Controls.ChatClient.Items
{
	public class Login : Alert
	{
		public Login(AlertStub alertStub, Controller parent, int serverRequestIndex)
			: base(alertStub, parent, serverRequestIndex)
		{
		}
		protected override string getHtml()
		{
			return "logged in";
		}
	}
}
