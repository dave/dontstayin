using System;
using SpottedScript.Controls.MultiBuddyChooser;
using SpottedScript.Behaviours;
using CreateUserFromEmailController = SpottedScript.Behaviours.CreateUserFromEmail.Controller;
namespace SpottedScript.Controls.BuddyChooser
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

		internal string Value
		{
			get { return view.uiHtmlAutoComplete.Value; }
			set { view.uiHtmlAutoComplete.Value = value; }
		}
		internal string Text
		{
			get { return view.uiHtmlAutoComplete.Text; }
			set { view.uiHtmlAutoComplete.Text = value; }
		}
	}
}
