using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace System
{
	public static class StringExtensions
	{
		public static bool HasSameContents(this string[] listA, string[] listB)
		{
			return HasSameContents(new List<string>(listA), new List<string>(listB));
		}

		public static bool HasSameContents(this List<string> listA, List<string> listB)
		{
			if (listA.Count != listB.Count)
			{
				return false;
			}

			listA.Sort();
			listB.Sort();

			for (int i = 0; i < listA.Count; i++)
			{
				if (listA[i] != listB[i])
				{
					return false;
				}
			}
			return true;
		}

		public static string TruncateWithDots(this string str, int maxLength)
		{
			if (str == null) 
				return null;
			if (str.Length <= maxLength) 
				return str;
			else
			{
				string ellipsis = "...";
				return str.Substring(0, maxLength - ellipsis.Length) + ellipsis;
			}
		}

		public static string Truncate(this string str, int maxLength)
		{
			if (str == null)
				return null;
			if (str.Length <= maxLength)
				return str;
			else
				return str.Substring(0, maxLength);
		}

		public static string StripAllNonAlphaNumeric(this string str)
		{
			System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("[^A-Za-z0-9]");
			return r.Replace(str, string.Empty);
		}

		public static List<int> CommaSeparatedValuesToIntList(this string str)
		{
			string str0 = str.Trim();
			if (str0 == "") return new List<int>();
			return new List<int>(Array.ConvertAll(str0.Split(','), i => int.Parse(i)));
		}
	 

		/// <summary>
		/// Trims, and replaces multiple-spaces with a single-space
		/// </summary>
		/// <returns></returns>
		public static string RemoveExtraSpaces(this string str)
		{
			string trimmed = str.Trim();
			string output = "";
			for (int i = 0; i < trimmed.Length; i++)
			{
				if (trimmed[i] == ' ')
				{
					while (i+1 < trimmed.Length && trimmed[i+1] == ' ') i++;
				}
				output += trimmed[i];
			}
			return output;
		}
	}

}
