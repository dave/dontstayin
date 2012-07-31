using System.XML;
using System;
using Spotted.System.Text;
using SpottedScript.Controls.ChatClient.Shared;
using System.DHTML;

namespace SpottedScript.Controls.ChatClient.Items
{
	public class Private : Message
	{
		public bool Buddy;
		public Private(PrivateStub privateStub, Controller parent, int serverRequestIndex)
			: base(privateStub, parent, serverRequestIndex)
		{
			Buddy = privateStub.buddy;
		}
	}
}
