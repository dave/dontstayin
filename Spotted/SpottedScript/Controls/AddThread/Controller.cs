using System;
using Sys.UI;
using ScriptSharpLibrary;
using Login = SpottedScript.Controls.Navigation.Login.PageImplementation;

namespace SpottedScript.Controls.AddThread
{
	public class Controller
	{
		public Controller(View view)
		{
			DomEvent.AddHandler(
				view.AddThreadAdvancedCheckBox,
				"click",
				delegate(DomEvent e)
				{
					Login.WhenLoggedIn(
						new Action(
							delegate()
							{
								view.AddThreadAdvancedPanel.Style.Display = view.AddThreadAdvancedCheckBox.Checked ? "" : "none";
							}
						)
					);
				}
			);
		}
	}
}
