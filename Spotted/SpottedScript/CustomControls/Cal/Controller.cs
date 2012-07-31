using System;
using Sys.UI;
using Utils;

namespace SpottedScript.CustomControls.Cal
{
	public delegate void DateDelegate(DateTime dateTime);
	public class Controller
	{
		private readonly View view;
		public DateDelegate OnDateChanged;
		public Controller(View view)
		{
			this.view = view;
			DomEvent.AddHandler(this.view.TextBox, "keydown", OnKeyDown);
			DomEvent.AddHandler(this.view.TextBox, "blur", OnBlur);

		}

		private void OnBlur(DomEvent e)
		{
			if (GetDate() == null)
			{
				this.view.TextBox.Value = "";
			}
		}

		private void OnKeyDown(DomEvent e)
		{
			if ("ABCDEFGHIJKLMNOPQRSTUVWXYZ,.;#[]".IndexOf(String.FromCharCode(e.KeyCode)) > -1)
			{
				e.PreventDefault();
			}
		}

		public void SetDate(DateTime dateTime)
		{
			this.view.TextBox.Value = dateTime.Format("dd/MM/yyyy");
		}

		public DateTime GetDate()
		{
			try
			{
				string[] parts = this.view.TextBox.Value.Split('/');
				return DateTime.ParseLocale(parts[1] + '/' + parts[0] + '/' + parts[2]);
			}
			catch(Exception ex)
			{
				return null;
			}
		}

		internal void Focus()
		{
			//this.view.TextBox.NextSibling.Click();
			this.view.TextBox.Focus();
		}
	}
}
