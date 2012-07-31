using System;
using System.Collections.Generic;
using System.Html;

namespace Js.Controls.ClientSideRepeater.Template
{
	public class Controller
	{
		private View view;
		public Controller(View view)
		{
			this.view = view;
		}
		public string Render(Dictionary<object, object> data)
		{
			Dictionary<object, object> transformed = TransformData(data);
			string itemTemplate = Document.GetElementById(view.clientId).InnerHTML;
			foreach (object key in transformed)
			{
				RegularExpression regex = new RegularExpression("{" + key.ToString() + @"}", "g");
				itemTemplate = itemTemplate.Unescape().ReplaceRegex(regex, transformed[key].ToString());
			}
			return itemTemplate;
		}

		protected virtual Dictionary<object, object> TransformData(Dictionary<object, object> data)
		{
			return data;
		}
	}
}
