using System;
using System.DHTML;
using Sys.UI;
using Sys;
using ScriptSharpLibrary;

namespace ScriptSharpLibrary
{
	public delegate void Action();
	public delegate void ActionBool(bool parameter);
	public delegate void ActionBoolBool(bool parameter1, bool parameter2);
	public delegate void ActionBoolBoolBool(bool parameter1, bool parameter2, bool parameter3);
	public delegate void ActionString(string parameter);
	public delegate void ActionDictionary(Dictionary parameter);
	public delegate void ActionObjectObject(object o1, object o2);
	public delegate void Response(Dictionary parameter);
}

namespace ImportedUtilities
{
	/// <summary>
	/// You can find these in /misc/Helpers.js
	/// </summary>
	[IgnoreNamespace]
	[Imported]
	public class F
	{
		/// <summary>
		/// Kludge to allow us to use variable length parameter lists...
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns></returns>
		[PreserveCase]
		public static Dictionary d(params object[] parameters) { return null; }

	}
	public class U
	{
		public static string toString(object o)
		{
			return toStringWithOffset(o, "");
		}
		static string toStringWithOffset(object o, string offset)
		{

			string tab = "    ";

			string s = "";
			if (o is DateTime)
			{
				s += ((DateTime)o).ToDateString() + " " + ((DateTime)o).ToTimeString();
			}
			else if (o is Array)
			{
				Array a = (Array)o;

				s += "\n" + offset + "{\n";
				for (int i = 0; i < a.Length; i++)
				{
					s += offset + tab + "[" + i.ToString() + "] : " + toStringWithOffset(a[i], offset + tab) + "\n";
				}
				s += offset + "}";
			}
			else if (o is bool)
			{
				s += o.ToString();
			}
			else if (o is int)
			{
				s += o.ToString();
			}
			else if (o is string)
			{
				string s1 = o.ToString().Replace(new RegularExpression("[\n]", "g"), "");
				s += s1.Length > 256 ? (s1.Substr(0, 256) + "(...)") : s1;
			}
			else if (o is Dictionary)
			{
				s += "\n" + offset + "{\n";
				foreach (DictionaryEntry entry in (Dictionary)o)
				{
					s += offset + tab + entry.Key + " : " + toStringWithOffset(entry.Value, offset + tab) + "\n";
				}
				s += offset + "}";
			}
			
			return s;
		}
		public static object get(Dictionary d, string query)
		{
			try
			{
				string[] queryArr;
				if (query.IndexOf('/') > -1)
					queryArr = query.Split('/');
				else
					queryArr = new string[] { query };

				for (int i = 0; i < queryArr.Length; i++)
				{
					if (i == queryArr.Length - 1)
						return getFromDictionaryByQuery(d, queryArr[i]);
					else
						d = (Dictionary)getFromDictionaryByQuery(d, queryArr[i]);
				}
				return null;
			}
			catch
			{
				return null;
			}
		}
		public static bool exists(Dictionary d, string query)
		{
			try
			{
				string[] queryArr;
				if (query.IndexOf('/') > -1)
					queryArr = query.Split('/');
				else
					queryArr = new string[] { query };

				for (int i = 0; i < queryArr.Length; i++)
				{
					if (i == queryArr.Length - 1)
					{
						if (getFromDictionaryByQuery(d, queryArr[i]) == null)
							return false;
					}
					else
						d = (Dictionary)getFromDictionaryByQuery(d, queryArr[i]);
				}
				return true;
			}
			catch
			{
				return false;
			}
		}
		public static object getFromDictionaryByQuery(Dictionary d, string query)
		{
			if (query == "" || query == "*")
				return getFromDictionaryByIndex(d, 0);
			else
				return d[query];
		}
		public static DictionaryEntry getFromDictionaryByIndex(Dictionary d, int index)
		{
			int i = 0;
			foreach (DictionaryEntry de in d)
			{
				if (i == index)
					return de;
				i++;
			}
			return null;
		}
		public static bool isTrue(Dictionary d, string query)
		{
			try
			{
				if (U.exists(d, query))
				{
					object o = U.get(d, query);
					if (o is bool)
					{
						return (bool)o;
					}
				}
				return false;
			}
			catch
			{
				return false;
			}
		}
		public static bool hasValue(Dictionary d, string query)
		{
			try
			{
				if (U.exists(d, query))
				{
					return U.get(d, query) != null;
				}
				return false;
			}
			catch
			{
				return false;
			}
		}
	}
}
