using System;
using System.Collections.Generic;
using System.Text;
using Bobs;

namespace SpottedLibrary.Controls.SearchBoxControl
{
	partial class SearchQuery
	{
		const Char DoubleQuotes = '"';
		const Char Dash = '-';
		const Char Space = ' ';
		static List<char> AllowedCharacters = new List<char>("abcdefghijjklmnopqrstuvwxyz0123456789* -\"".ToCharArray());
		public static SearchQuery Parse(string searchString)
		{
			var tags = new List<string>();
			char[] ss = searchString.ToCharArray();
			StringBuilder sb = new StringBuilder();
			bool inQuotes = false;
			bool exclude = false;
			foreach (char c in ss)
			{
				if (c == Dash)
				{
					exclude = true;
				}
				else if (c == DoubleQuotes)
				{
					inQuotes = !inQuotes;
				}
				else if (c == Space && !inQuotes)
				{
					AddIfValidPart(tags, sb.ToString(), exclude);
					exclude = false;
					sb = new StringBuilder();
				}
				else
				{
					sb.Append(c);
				}
			}
			AddIfValidPart(tags, sb.ToString(), exclude);
			return new SearchQuery(tags);
			
		}
		private static void AddIfValidPart(List<string> tags, string part, bool exclude)
		{
			part = MakeLowerCaseAndRemoveDisallowedCharacters(part);
			if (part.Length != 0)
			{
				tags.Add(part);
			}
		}
		public static SearchQuery Parse(UrlInfo urlInfo)
		{
			List<string> tags = new List<string>();
			foreach (string tagText in urlInfo.TagFilter)
			{
				tags.Add(tagText);
			}
			return new SearchQuery(tags);
		}
		public static SearchQuery Parse(ICutDownUrlInfo urlInfo)
		{
			List<string> tags = new List<string>();
			foreach (string tagText in urlInfo.TagFilter)
			{
				tags.Add(tagText);
			}
			return new SearchQuery(tags);
		}
		
		private static string MakeLowerCaseAndRemoveDisallowedCharacters(string p)
		{
			List<char> output = new List<char>();
			foreach(char c in p.ToLower().ToCharArray())
			{
				if (AllowedCharacters.Contains(c))
				{
					output.Add(c);
				}
			}
			return new String(output.ToArray());
		}
	}
}
