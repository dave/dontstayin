using System;
using System.DHTML;

namespace SpottedScript.Controls.ClientSideRepeater.Template
{
	public class Controller
	{
		private View view;
		public Controller(View view)
		{
			this.view = view;
		}
		public string Render(Dictionary data)
		{
			Dictionary transformed = TransformData(data);
			string itemTemplate = Document.GetElementById(view.clientId).InnerHTML;
			foreach (DictionaryEntry de in transformed)
			{
				RegularExpression regex = new RegularExpression("{" + de.Key + @"}", "g");
				itemTemplate = itemTemplate.Unescape().Replace(regex, de.Value.ToString());
			}
			return itemTemplate;
		}

		protected virtual Dictionary TransformData(Dictionary data)
		{
			return data;
		}
	}
}
