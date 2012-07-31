using System;
using System.Collections.Generic;
using System.Text;

namespace System.Xml
{
	public static class XmlNodeAttributeExtensions
	{
		public static void AddAttribute(this XmlNode node, string key, string val)
		{
			XmlAttribute att = node.OwnerDocument.CreateAttribute(key);
			att.Value = val;
			node.Attributes.Append(att);
		}
	}
}
