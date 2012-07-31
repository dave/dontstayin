using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bobs
{
	#region TagBuilder
	public class TagBuilder
	{
		StringBuilder sb;
		Dictionary<string, string> attributes;
		string tagName;
		HtmlCleaner.HtmlWriter w;

		public TagBuilder(string tagName, StringBuilder sb) : this(new HtmlCleaner.HtmlWriter(new StringBuilder()), tagName, sb) { }
		public TagBuilder(HtmlCleaner.HtmlWriter w, string tagName, StringBuilder sb)
		{
			this.w = w;
			this.sb = sb;
			this.tagName = tagName;
			this.attributes = new Dictionary<string, string>();
		}

		public void AddAttribute(string name, string value)
		{
			if (attributes.ContainsKey(name))
				attributes[name] = value;
			else
				attributes.Add(name, value);
		}
		void renderTag(bool leaveOpen)
		{
			if (w.IsAlowedTag(tagName))
			{
				sb.Append("<");
				sb.Append(tagName);
				foreach (string k in attributes.Keys)
				{
					HtmlCleaner.HtmlWriter.AttributeTypes t = w.GetAttributeType(k);
					if (t != HtmlCleaner.HtmlWriter.AttributeTypes.Remove)
					{
						string val = w.ParseAttribute(attributes[k], t);
						if (val.Length > 0)
						{
							sb.Append(" ");
							sb.Append(k);
							sb.Append("=\"");
							sb.Append(val);
							sb.Append("\"");
						}
					}
				}
				if (leaveOpen)
					sb.Append(">");
				else
					sb.Append(" />");
			}
		}
		public void RenderStartTag()
		{
			renderTag(true);
		}
		public void RenderEmptyTag()
		{
			renderTag(false);
		}
		public void RenderEndTag()
		{
			if (w.IsAlowedTag(tagName))
			{
				sb.Append("</");
				sb.Append(tagName);
				sb.Append(">");
			}
		}
		public void RenderContentAndEndTag(string content)
		{
			if (w.IsAlowedTag(tagName))
			{
				if (content.EndsWith(" "))
				{
					sb.Append(content.Substring(0, content.Length - 1));
					sb.Append("</");
					sb.Append(tagName);
					sb.Append("> ");
				}
				else
				{
					sb.Append(content);
					sb.Append("</");
					sb.Append(tagName);
					sb.Append(">");
				}
			}
		}

	}
	#endregion
}
