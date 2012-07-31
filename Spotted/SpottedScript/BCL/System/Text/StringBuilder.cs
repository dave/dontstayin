using System;

namespace Spotted.System.Text
{
	public class StringBuilder
	{
		Array stringArray;
		public StringBuilder()
		{
			stringArray = new Array();
		}
		public void Append(string s)
		{
			stringArray[stringArray.Length] = s;
		}
		public override string ToString()
		{
			return stringArray.Join("");
		}
		public void AppendAttribute(string name, string value)
		{
			stringArray[stringArray.Length] = " ";
			stringArray[stringArray.Length] = name;
			stringArray[stringArray.Length] = "=\"";
			stringArray[stringArray.Length] = value.Replace(new RegularExpression("\"", "g"), "&#34;");
			stringArray[stringArray.Length] = "\"";
		}
	}

}
