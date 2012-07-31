using System;
using System.Collections.Generic;
using System.Html;
using System.Runtime.CompilerServices;

namespace Js.Library
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
		public static Dictionary<object, object> d(params object[] parameters) { return null; }

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
			if (o is Date)
			{
				s += ((Date)o).ToDateString() + " " + ((Date)o).ToTimeString();
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
				string s1 = o.ToString().Replace("\n", "");
				s += s1.Length > 256 ? (s1.Substr(0, 256) + "(...)") : s1;
			}
			else if (o is Dictionary<object, object>)
			{
				Dictionary<object, object> d = (Dictionary<object, object>)o;
				s += "\n" + offset + "{\n";
				foreach (object key in d.Keys)
				{
					s += offset + tab + key + " : " + toStringWithOffset(d[key], offset + tab) + "\n";
				}
				s += offset + "}";
			}

			return s;
		}
		public static object get(Dictionary<object, object> d, string query)
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
						d = (Dictionary<object, object>)getFromDictionaryByQuery(d, queryArr[i]);
				}
				return null;
			}
			catch
			{
				return null;
			}
		}
		public static bool exists(Dictionary<object, object> d, string query)
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
						d = (Dictionary<object, object>)getFromDictionaryByQuery(d, queryArr[i]);
				}
				return true;
			}
			catch
			{
				return false;
			}
		}
		public static object getFromDictionaryByQuery(Dictionary<object, object> d, string query)
		{
			if (query == "" || query == "*")
				return getFromDictionaryByIndex(d, 0);
			else
				return d[query];
		}
		public static object getFromDictionaryByIndex(Dictionary<object, object> d, int index)
		{
			try
			{
				return d[d.Keys[index]];
			}
			catch { return null; }
			//int i = 0;
			//foreach (DictionaryEntry de in d)
			//{
			//	if (i == index)
			//		return de;
			//	i++;
			//}
			//return null;
		}
		public static bool isTrue(Dictionary<object, object> d, string query)
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
		public static bool hasValue(Dictionary<object, object> d, string query)
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
