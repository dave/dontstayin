using System;

namespace Js.Library
{
	public class StringBuilderJs
	{
		System.Text.StringBuilder sb;

		public StringBuilderJs()
		{
			sb = new System.Text.StringBuilder();
		}
		public void Append(string value)
		{
			sb.Append(value);
		}
		public void AppendAttribute(string name, string value)
		{
			sb.Append(" ");
			sb.Append(name);
			sb.Append("=\"");
			sb.Append(value.Replace("\"", "&#34;"));
			sb.Append("\"");
		}
		public string ToString()
		{
			return sb.ToString();
		}
	}

	public static class Extensions
	{
		public static string ToLowerCase(this string s)
		{
			return s.ToLower();
		}
		
	}
}
