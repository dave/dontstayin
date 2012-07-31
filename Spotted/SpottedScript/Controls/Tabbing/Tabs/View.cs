using System;
using System.DHTML;
using SpottedScript.Controls.Tabbing.Tab;
namespace SpottedScript.Controls.Tabbing.Tabs
{
	public partial class View
	{
		public ITabController[] Tabs
		{
			get
			{
				ITabController[] tabs = new ITabController[] { };
				string[] tabControllerNames = this.uiTabControllerNames.Value.Split("|");
				for (int i = 0; i < tabControllerNames.Length; i++)
				{
					tabs[tabs.Length] = (ITabController)Script.Eval(tabControllerNames[i]);
				}
				return tabs;
			}
		}
		public DOMElement[] TabTitles
		{
			get
			{
				DOMElement[] tabTitles = new DOMElement[] { };
				string[] tabTitleIds = this.uiTabTitleIds.Value.Split("|");
				for (int i = 0; i < tabTitleIds.Length; i++)
				{
					tabTitles[tabTitles.Length] = Document.GetElementById(tabTitleIds[i]);
				}
				return tabTitles;
			}
		}

	}
}
