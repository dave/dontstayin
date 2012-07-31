using System;
namespace SpottedScript
{
	public static class SpottedScriptExtensions
	{
		public static string ToLowerCase(this string s)
		{
			return s.ToLower();
		}
		public static void AppendAttribute(this System.Text.StringBuilder sb, string name, string value)
		{
			sb.Append(" ");
			sb.Append(name);
			sb.Append("=\"");
			sb.Append(value.Replace("\"", "&#34;"));
			sb.Append("\"");
		}
	}
}
