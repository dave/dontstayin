
using System.Collections.Generic;
namespace System.Web
{
	public static class DictionaryExtension
	{
		public static string ToAttributeList(this Dictionary<string, string> dictionary)
		{
			return String.Join(";", new List<KeyValuePair<string, string>>(dictionary).ConvertAll(pair => pair.Key + ":" + pair.Value).ToArray());
		}
	}
}
