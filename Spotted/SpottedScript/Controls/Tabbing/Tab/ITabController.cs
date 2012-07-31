using System.DHTML;
using Sys;

namespace SpottedScript.Controls.Tabbing.Tab
{
	public delegate void ActionOnDOMElementDelegate(DOMElement el);
	public delegate void SendActionOnDOMElementDelegate(ActionOnDOMElementDelegate action);
		
	public interface ITabController
	{
		DOMElement uiPanel { get; }
		void SetHeader(DOMElement el);
		void DisplayInfoForTab();
		EventHandler Updated { get; set; }
	}
}
