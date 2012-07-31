
using System;
using System.DHTML;
using Sys.UI;

 
namespace ScriptSharpLibrary
{
	
	public class HelloWorld
	{
		DOMElement te;
		public HelloWorld(DOMElement te)
		{
			
			this.te = te;
			te.InnerHTML = "hello";
			DomEventHandler handler = new DomEventHandler(DoSomething);
			DomEvent.AddHandler(te, "click", handler);
			
		}

		public void DoSomething(Sys.UI.DomEvent ev)
		{
			te.InnerHTML = "goodbye at " + (new DateTime()).GetTime();
		}

	 
		
	}
}
