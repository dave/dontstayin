using System.DHTML;
using JQ;
using ScriptSharpLibrary;
using JQ;
using SpottedScript.Controls.Tabbing.Tab;
using Sys;
using System;
using Utils;
namespace SpottedScript.Controls.Tabbing.Tabs
{
	public delegate void TabFunc(ITabController TabController);
	public class Controller
	{
		private readonly View view;

		public Controller(View view)
		{
			this.view = view;
			for (int i = 0; i < view.Tabs.Length; i++)
			{
				int index = i;
				ITabController controller = view.Tabs[index];
				controller.SetHeader(view.TabTitles[i]);
				
				Script.Literal("{0} = null;", index);
				controller.Updated = Misc.CombineEventHandler(controller.Updated, OnTabUpdated);
			}
			SelectedTab = view.Tabs[0];
			JQueryAPI.JQuery(".ui-tabs-nav").bind("tabsselect",
			                                      delegate(object ev, TabsSelectEventArgs ui1, TabsSelectEventArgs ui2)
			                                      {
			                                      	// jQuery bug, they'll prob fix it one day. losers!
			                                      	if (ui1 == null)
			                                      	{
			                                      		ui1 = ui2;
			                                      	}
			                                      	OnTabSelect(ev, ui1);
			                                      });
		}
		private void OnTabUpdated(object sender, EventArgs ev)
		{

			if (TabDisplayUpdated != null)
			{
				TabDisplayUpdated(sender, ev);
			}
		}

		private void OnTabSelect(object ev, TabsSelectEventArgs ui)
		{
			int tabIndex = 0;
			for (; tabIndex < this.view.Tabs.Length; tabIndex++)
			{
				if (ui.panel == this.view.Tabs[tabIndex].uiPanel)
				{
					break;
				}
			}
			SelectedTab = view.Tabs[tabIndex];

			if (TabDisplayUpdated != null)
			{
				Trace.Write("Firing TabDisplayUpdated ");
				TabDisplayUpdated(SelectedTab, EventArgs.Empty);
			}
			else
			{
				Trace.Write("TabDisplayUpdated is null");
			}
		}

		public EventHandler TabDisplayUpdated;

		public ITabController SelectedTab;
 

		public bool IsSelected(ITabController tab)
		{
			return tab == SelectedTab;
		}

		public void ApplyToTabs(TabFunc func)
		{
			for (int i = 0; i < view.Tabs.Length; i++)
			{
				func(view.Tabs[i]);
			}
		}
	}
}
