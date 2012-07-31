using System;
using Js.Library;
using LoginPageImplementation = Js.Controls.Login.PageImplementation;
using jQueryApi;

namespace Js.Controls.AddThread
{
	public class Controller
	{
		public Controller(View view)
		{
			view.AddThreadAdvancedCheckBoxJ.Click(
				delegate(jQueryEvent e)
				{
					LoginPageImplementation.WhenLoggedIn(
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
