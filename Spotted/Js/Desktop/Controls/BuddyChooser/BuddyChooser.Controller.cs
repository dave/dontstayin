using System;
using Js.Controls.MultiBuddyChooser;

namespace Js.Controls.BuddyChooser
{
	public class Controller
	{
		View view;
		CreateUserFromEmailController behaviour;
		public Controller(View view)
		{
			this.view = view;
			behaviour = new CreateUserFromEmailController(view.uiHtmlAutoComplete);
		}

		public string Value
		{
			get { return view.uiHtmlAutoComplete.Value; }
			set { view.uiHtmlAutoComplete.Value = value; }
		}
		public string Text
		{
			get { return view.uiHtmlAutoComplete.Text; }
			set { view.uiHtmlAutoComplete.Text = value; }
		}
	}
}
