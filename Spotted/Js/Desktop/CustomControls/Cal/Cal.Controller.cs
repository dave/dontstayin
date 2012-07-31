using System;
using System.Html;
using Js.Library;
using jQueryApi;

namespace Js.CustomControls.Cal
{
	public partial class View
	{
		public InputElement TextBox { get { if (_TextBox == null) { _TextBox = (InputElement)Document.GetElementById(clientId + "_inner"); }; return _TextBox; } } private InputElement _TextBox;
		public jQueryObject TextBoxJ { get { if (_TextBoxJ == null) { _TextBoxJ = jQuery.Select("#" + clientId + "_inner"); }; return _TextBoxJ; } } private jQueryObject _TextBoxJ;
		
	}
	public delegate void DateDelegate(Date dateTime);
	public class Controller
	{
		private readonly View view;
		public DateDelegate OnDateChanged;
		public Controller(View view)
		{
			this.view = view;
			view.TextBoxJ.Keydown(OnKeyDown);
			view.TextBoxJ.Blur(OnBlur);

		}

		private void OnBlur(jQueryEvent e)
		{
			if (GetDate() == null)
			{
				this.view.TextBox.Value = "";
			}
		}

		private void OnKeyDown(jQueryEvent e)
		{
			if ("ABCDEFGHIJKLMNOPQRSTUVWXYZ,.;#[]".IndexOf(String.FromCharCode(e.Which)) > -1)
			{
				e.PreventDefault();
			}
		}

		public void SetDate(Date dateTime)
		{
			this.view.TextBox.Value = dateTime.Format("dd/MM/yyyy");
		}

		public Date GetDate()
		{
			try
			{
				string[] parts = this.view.TextBox.Value.Split('/');
				return Date.Parse(parts[1] + '/' + parts[0] + '/' + parts[2]);
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
