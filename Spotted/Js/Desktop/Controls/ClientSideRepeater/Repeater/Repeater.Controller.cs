using System;
using System.Collections.Generic;

namespace Js.Controls.ClientSideRepeater.Repeater
{
	public class Controller
	{
		public View view;
		public Controller(View view)
		{
			this.view = view;
		}

		private string GroupByField = "Date";
		public void DisplayData(object[] data)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(view.uiHeaderTemplateHolder.InnerHTML.Unescape());
			if (data != null)
			{
				string currentGroupByFieldValue = "";
				for (int i = 0; i < data.Length; i++)
				{
					Dictionary<object, object> dataItem = (Dictionary<object, object>)data[i];
					if (GroupByField != "")
					{
						if (currentGroupByFieldValue != (string) dataItem[GroupByField])
						{
							currentGroupByFieldValue = (string) dataItem[GroupByField];
							sb.Append("<div class='ClientSideRepeaterGroupHeader'>" + currentGroupByFieldValue + "</div>");
						}
					}
					sb.Append(this.view.uiItemTemplate.Render(dataItem));
					if (i + 1 < data.Length)
					{
						sb.Append(view.uiBetweenTemplateHolder.InnerHTML.Unescape());
					}
				}
			}
			sb.Append(view.uiFooterTemplateHolder.InnerHTML.Unescape());
			view.uiContent.InnerHTML = sb.ToString();
		}
	}
}
