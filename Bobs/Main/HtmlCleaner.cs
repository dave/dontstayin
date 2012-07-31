// Copyright © 2005 by Omar Al Zabir. All rights are reserved.
// 
// If you like this code then feel free to go ahead and use it.
// The only thing I ask is that you don't remove or alter my copyright notice.
//
// Your use of this software is entirely at your own risk. I make no claims or
// warrantees about the reliability or fitness of this code for any particular purpose.
// If you make changes or additions to this code please mark your code as being yours.
// 
// website http://www.oazabir.com, email OmarAlZabir@gmail.com, msn oazabir@hotmail.com

using System;
using System.Globalization;
using System.IO;
using Sgml;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using Bobs;

namespace HtmlCleaner
{

	#region HtmlReader
	/// <summary>
	/// This class skips all nodes which has some kind of prefix. This trick does the job 
	/// to clean up MS Word/Outlook HTML markups.
	/// </summary>
	public class HtmlReader : Sgml.SgmlReader
	{
		public HtmlReader(TextReader reader)
			: base()
		{
			base.InputStream = reader;
			base.DocType = "HTML";
		}
		public HtmlReader(string content)
			: base()
		{
			base.InputStream = new StringReader(content);
			base.DocType = "HTML";
		}
		public override bool Read()
		{
			try
			{
				bool status = base.Read();
				if (status)
				{
					if (base.NodeType == XmlNodeType.Element)
					{
						// Got a node with prefix. This must be one of those "<o:p>" or something else.
						// Skip this node entirely. We want prefix less nodes so that the resultant XML 
						// requires not namespace.
						if (base.Name.IndexOf(':') > 0 && !base.Name.StartsWith("dsi:"))
							base.Skip();
					}
				}
				return status;
			}
			catch
			{
				return false;
			}
		}
	}
	#endregion

	#region HtmlWriter
	/// <summary>
	/// Extends XmlTextWriter to provide Html writing feature which is not as strict as Xml
	/// writing. For example, Xml Writer encodes content passed to WriteString which encodes special markups like
	/// &nbsp to &amp;bsp. So, WriteString is bypassed by calling WriteRaw.
	/// </summary>
	public class HtmlWriter : XmlTextWriter
	{

		public HtmlWriter(TextWriter writer) : base(writer) { }
		public HtmlWriter(StringBuilder builder) : base(new StringWriter(builder)) { }
		public HtmlWriter(Stream stream, Encoding enc) : base(stream, enc) { }

		#region Tag definitions and rules

		#region AllowedTags
		/// <summary>
		/// Set the tag names in lower case which are allowed to go to output
		/// </summary>
		//public string[] AllowedTags = new string[] { 
		//    "dsi:video", "dsi:audio", "dsi:flash", "dsi:object", "dsi:link", "dsi:quote",
		//    "a", "address", "area", 
		//    "b", "big", "blockquote", "br",
		//    "caption", "center", "cite", "code", "col", "colgroup", 
		//    "dd", "dfn", "div", "dl", "dt", 
		//    "em", "font", 
		//    "h1", "h2", "h3", "h4", "h5", "h6", "hr", 
		//    "i", "img", "ins", 
		//    "kbd", 
		//    "label", "li", 
		//    "map", 
		//    "ol", 
		//    "p", "pre", 
		//    "q", 
		//    "s", "samp", "small", "spacer", "span", "strike", "strong", "sub", "sup", 
		//    "table", "tbody", "td", "tfoot", "th", "thead", "tr", "tt", 
		//    "u", "ul", 
		//    "var" };
		public string[] AllowedTags = new string[] { 
			"dsi:video", "dsi:audio", "dsi:flash", "dsi:object", "dsi:link", "dsi:quote",
			"a", "area", 
			"b", "big", "blockquote", "br",
			"center", "col", "colgroup", 
			"div",
			"em", "font", 
			"h1", "h2", "h3", "h4", "h5", "h6", "hr", 
			"i", "img", 
			"li", 
			"map", 
			"ol", 
			"p", "pre", 
			"s", "small", "spacer", "span", "strike", "strong", "sub", "sup", 
			"table", "tbody", "td", "tfoot", "th", "thead", "tr", 
			"u", "ul"};
		#endregion
		#region IsAlowedTag
		public bool IsAlowedTag(string tagName)
		{
			try
			{
				string tagNameLower = tagName.ToLower();
				foreach (string name in this.AllowedTags)
				{
					if (name.Equals(tagNameLower))
						return true;
				}
				return false;
			}
			catch
			{
				return false;
			}
		}
		#endregion

		#region EmptyTags
		public string[] EmptyTags = new string[] { "br", "hr", "img", "area", "dsi:video", "dsi:audio", "dsi:flash", "dsi:object" };
		#endregion
		#region IsEmptyTag
		public bool IsEmptyTag(string tagName)
		{
			try
			{
				string tagNameLower = tagName.ToLower();
				foreach (string name in this.EmptyTags)
				{
					if (name.Equals(tagNameLower))
						return true;
				}
				return false;
			}
			catch
			{
				return false;
			}
		}
		#endregion

		#region ContainerRestriction
		//only inside <map>: <area>
		//only inside <table>: <caption>, <col>, <colgroup>, <tr>, <thead>, <tfoot>, <tbody>
		//only inside <tr>: <td>, <th>
		//only inside <dl>: <dd>, <dt>
		//only inside <ul> or <ol>: <li>
		Dictionary<string, string[]> ContainerRestriction
		{
			get
			{
				if (containerRestriction == null)
				{
					containerRestriction = new Dictionary<string, string[]>();
					containerRestriction.Add("area", new string[] { "map" });
					containerRestriction.Add("caption", new string[] { "table" });
					containerRestriction.Add("col", new string[] { "table" });
					containerRestriction.Add("colgroup", new string[] { "table" });
					containerRestriction.Add("tr", new string[] { "table" });
					containerRestriction.Add("thead", new string[] { "table" });
					containerRestriction.Add("tfoot", new string[] { "table" });
					containerRestriction.Add("tbody", new string[] { "table" });
					containerRestriction.Add("td", new string[] { "tr" });
					containerRestriction.Add("th", new string[] { "tr" });
					containerRestriction.Add("dd", new string[] { "dl" });
					containerRestriction.Add("dt", new string[] { "dl" });
					containerRestriction.Add("li", new string[] { "ol", "ul" });
				}
				return containerRestriction;
			}
		}
		Dictionary<string, string[]> containerRestriction;
		#endregion
		#region IsTagContainerOk

		#region IsTagContainerOk(string tagName)
		bool IsTagContainerOk(string tagName)
		{
			try
			{
				string tagNameLower = tagName.ToLower();
				if (ContainerRestriction.ContainsKey(tagNameLower))
				{
					foreach (string allowedContainer in ContainerRestriction[tagNameLower])
					{
						if (OpenTags.ContainsKey(allowedContainer) && OpenTags[allowedContainer] > 0)
							return true;
					}
					return false;
				}
				return true;
			}
			catch
			{
				return false;
			}
		}
		#endregion

		#region OpenTags
		public Dictionary<string, int> OpenTags
		{
			get
			{
				if (openTags == null)
					openTags = new Dictionary<string, int>();
				return openTags;
			}
			set
			{
				openTags = value;
			}
		}
		Dictionary<string, int> openTags;
		#endregion
		#region LogStartTag
		public void LogStartTag(string tagName)
		{
			string tagNameLower = tagName.ToLower();
			if (OpenTags.ContainsKey(tagNameLower))
				OpenTags[tagNameLower]++;
			else
				OpenTags[tagNameLower] = 1;
		}
		#endregion
		#region LogEndTag
		public void LogEndTag(string tagName)
		{
			string tagNameLower = tagName.ToLower();
			if (OpenTags.ContainsKey(tagNameLower) && OpenTags[tagNameLower] > 0)
				OpenTags[tagNameLower]--;
		}
		#endregion

		#endregion

		#endregion

		#region Parse html attribute

		#region Attribute types
		public AttributeTypes GetAttributeType(string attribute)
		{
			switch (attribute)
			{
				case "ref": return AttributeTypes.Text;		 //for dsi media tags
				case "nsfw": return AttributeTypes.Text;	 //for dsi media tags
				case "play": return AttributeTypes.Text;	 //for dsi media tags
				case "loop": return AttributeTypes.Text;	 //for dsi media tags
				case "menu": return AttributeTypes.Text;	 //for dsi media tags
				case "quality": return AttributeTypes.Text;	 //for dsi media tags
				case "scale": return AttributeTypes.Text;	 //for dsi media tags
				case "salign": return AttributeTypes.Text;	 //for dsi media tags
				case "wmode": return AttributeTypes.Text;	 //for dsi media tags
				case "base": return AttributeTypes.Src;		 //for dsi media tags
				case "flashvars": return AttributeTypes.List;//for dsi media tags
				case "draw": return AttributeTypes.Text;	 //for dsi media tags
				case "app": return AttributeTypes.Text;		 //for dsi media tags
				case "jump": return AttributeTypes.Text;	 //for dsi media tags
				case "par": return AttributeTypes.List;		 //for dsi media tags
				case "date": return AttributeTypes.Text;	 //for dsi media tags

				case "abbr": return AttributeTypes.Text;
				case "align": return AttributeTypes.Text;
				case "alt": return AttributeTypes.Text;
				case "bgcolor": return AttributeTypes.Colour;
				case "border": return AttributeTypes.Number;
				case "cellpadding": return AttributeTypes.Number;
				case "cellspacing": return AttributeTypes.Number;
				case "class": return AttributeTypes.Text;
				case "clear": return AttributeTypes.Text;
				case "colspan": return AttributeTypes.Number;
				case "color": return AttributeTypes.Colour;
				case "coords": return AttributeTypes.Text;
				case "dir": return AttributeTypes.Text;
				case "face": return AttributeTypes.Text;
				case "for": return AttributeTypes.Text;
				case "frame": return AttributeTypes.Text;
				case "headers": return AttributeTypes.Text;
				case "height": return AttributeTypes.Number;
				case "href": return AttributeTypes.Href;
				case "hreflang": return AttributeTypes.Text;
				case "hspace": return AttributeTypes.Number;
				//case "id": return AttributeTypes.Text;
				case "ismap": return AttributeTypes.Text;
				case "lang": return AttributeTypes.Text;
				case "longdesc": return AttributeTypes.Text;
				case "name": return AttributeTypes.Text;
				case "nohref": return AttributeTypes.Text;
				case "noshade": return AttributeTypes.Text;
				case "nowrap": return AttributeTypes.Text;
				case "rel": return AttributeTypes.Text;
				case "rev": return AttributeTypes.Text;
				case "rowspan": return AttributeTypes.Number;
				case "rules": return AttributeTypes.Text;
				case "scope": return AttributeTypes.Text;
				case "shape": return AttributeTypes.Text;
				case "size": return AttributeTypes.Number;
				case "span": return AttributeTypes.Number;
				case "src": return AttributeTypes.Src;
				case "start": return AttributeTypes.Number;
				case "style": return AttributeTypes.Style;
				case "summary": return AttributeTypes.Text;
				case "tabindex": return AttributeTypes.Number;
				case "target": return AttributeTypes.Text;
				case "title": return AttributeTypes.Text;
				case "type": return AttributeTypes.Text;
				case "usemap": return AttributeTypes.UseMap;
				case "valign": return AttributeTypes.Text;
				case "value": return AttributeTypes.Number;
				case "vspace": return AttributeTypes.Number;
				case "width": return AttributeTypes.Number;
				default: return AttributeTypes.Remove;
			}
		}

		public enum AttributeTypes
		{
			Text,
			List,
			Src,
			Href,
			Number,
			Style,
			Colour,
			UseMap,
			Remove
		}
		#endregion

		#region ParseAttribute
		public string ParseAttribute(string valueIn, AttributeTypes type)
		{
			try
			{
				switch (type)
				{
					case AttributeTypes.Text: return ParseAttributeText(valueIn);
					case AttributeTypes.List: return ParseAttributeList(valueIn);
					case AttributeTypes.Src: return ParseAttributeUrl(valueIn, false);
					case AttributeTypes.Href: return ParseAttributeUrl(valueIn, true);
					case AttributeTypes.Number: return ParseAttributeNumber(valueIn);
					case AttributeTypes.Style: return ParseAttributeStyle(valueIn);
					case AttributeTypes.Colour: return ParseAttributeColour(valueIn);
					case AttributeTypes.UseMap: return ParseAttributeUseMap(valueIn);
					case AttributeTypes.Remove: return "";
					default: return "";
				}
			}
			catch
			{
				return "";
			}
		}
		#endregion

		#region urlDecodeEncode
		string urlDecodeEncode(string inString)
		{
			return System.Web.HttpUtility.UrlEncode(System.Web.HttpUtility.UrlDecode(inString)).Replace("+", "%20");
		}
		#endregion

		#region Text
		string ParseAttributeText(string valueIn)
		{
			return AttributeTextRegex.Replace(valueIn, String.Empty);
		}
		Regex AttributeTextRegex
		{
			get
			{
				if (attributeTextRegex == null)
					attributeTextRegex = new Regex("[^a-zA-Z0-9.,/\\-_ ]");
				return attributeTextRegex;
			}
		}
		Regex attributeTextRegex;
		#endregion

		#region Url
		#region ParseAttributeUrl
		public string ParseAttributeUrl(string valueIn, bool allowMailTo)
		{
			valueIn = valueIn.Replace("&amp;", "&").Trim();

			string protocol = "";
			string domain = "";
			string port = "";
			string path = "";
			string querystring = "";
			string bookmark = "";


			string pathAnyQuery = "";
			if (valueIn.StartsWith("#"))
			{
				bookmark = valueIn.Substring(1);
			}
			else
			{
				if (valueIn.StartsWith("/"))
				{
					#region /xyz -> http://www.dontstayin.com/xyz
					protocol = "http://";
					domain = "www.dontstayin.com";
					port = "";
					pathAnyQuery = valueIn;
					#endregion
				}
				else
				{
					#region Find protocol
					string domainRight = "";
					if (valueIn.ToLower().StartsWith("http://"))
					{
						protocol = "http://";
						domainRight = valueIn.Substring(7);
					}
					else if (valueIn.ToLower().StartsWith("https://"))
					{
						protocol = "https://";
						domainRight = valueIn.Substring(8);
					}
					else if (allowMailTo && valueIn.ToLower().StartsWith("mailto:"))
					{
						//	protocol = "mailto:";
						//	domainRight = valueIn.Substring(7);
						return "mailto:" + AttributeEmailRegex.Replace(valueIn.Substring(7), String.Empty);
					}
					else
					{
						protocol = "http://";
						domainRight = valueIn;
					}
					#endregion

					#region Find domain and port
					string domainAndPort = "";
					if (domainRight.Contains("/"))
					{
						domainAndPort = domainRight.Substring(0, domainRight.IndexOf("/"));
						pathAnyQuery = domainRight.Substring(domainRight.IndexOf("/"));
					}
					else if (domainRight.Contains("?"))
					{
						domainAndPort = domainRight.Substring(0, domainRight.IndexOf("?"));
						pathAnyQuery = domainRight.Substring(domainRight.IndexOf("?"));
					}
					else
					{
						domainAndPort = domainRight;
						pathAnyQuery = "";
					}

					if (domainAndPort.Contains(":"))
					{
						domain = domainAndPort.Substring(0, domainAndPort.IndexOf(":"));
						port = int.Parse(domainAndPort.Substring(domainAndPort.IndexOf(":") + 1)).ToString();
					}
					else
					{
						domain = domainAndPort;
						port = "";
					}
					#endregion
				}

				#region Split path, querystring and bookmark
				if (pathAnyQuery.Contains("#"))
				{
					bookmark = pathAnyQuery.Substring(pathAnyQuery.LastIndexOf('#') + 1);
					pathAnyQuery = pathAnyQuery.Substring(0, pathAnyQuery.LastIndexOf('#'));
				}
				else
					bookmark = "";

				string pathTmp = "";

				if (pathAnyQuery.Contains("?"))
				{
					string querystringTmp = pathAnyQuery.Substring(pathAnyQuery.LastIndexOf('?') + 1);
					pathTmp = pathAnyQuery.Substring(0, pathAnyQuery.LastIndexOf('?'));
					foreach (string s in querystringTmp.Split('&'))
					{
						querystring += querystring.Length == 0 ? "" : "&";
						if (s.Contains("="))
						{
							string[] sA = s.Split('=');
							for (int i = 0; i < sA.Length; i++)
							{
								querystring += (i == 0 ? "" : "=") + urlDecodeEncode(sA[i]);
							}
							//querystring += urlDecodeEncode(s.Split('=')[0]);
							//querystring += "=";
							//querystring += urlDecodeEncode(s.Split('=')[1]);
						}
						else
						{
							querystring += urlDecodeEncode(s);
						}
					}

				}
				else
				{
					querystring = "";
					pathTmp = pathAnyQuery;
				}

				if (pathTmp.Length > 0)
				{
					if (pathTmp == "/")
						path = "/";
					else
					{
						string[] pathAry = pathTmp.Split('/');
						for (int i = 1; i < pathAry.Length; i++)
						{
							path += "/" + urlDecodeEncode(pathAry[i]);
						}
					}
				}

				#endregion

				#region remove dsi login portion
				if (domain.EndsWith("dontstayin.com") && path.Length >= 15)
				{
					string pathWithoutInitialSlash = path.Substring(1);
					int firstSlashLocation = pathWithoutInitialSlash.IndexOf('/');

					string initialPathPart = pathWithoutInitialSlash;
					if (firstSlashLocation > -1)
						initialPathPart = pathWithoutInitialSlash.Substring(0, firstSlashLocation);

					if (LoginPathPartRegex.IsMatch(initialPathPart))
					{
						path = path.Substring(initialPathPart.Length + 1);
					}
				}
				#endregion

			}

			

			string parsed = protocol;
			if (domain.Length > 0)
				parsed += AttributeUrlDomainRegex.Replace(domain, String.Empty);
			if (port.Length > 0)
				parsed += ":" + port;
			if (path.Length > 0)
				parsed += path; // AttributeUrlPathRegex.Replace(path, String.Empty);
			if (querystring.Length > 0)
				parsed += "?" + querystring; // AttributeUrlQuerystringRegex.Replace(querystring, String.Empty);
			if (bookmark.Length > 0)
				parsed += "#" + urlDecodeEncode(bookmark); //AttributeUrlBookmarkRegex.Replace(bookmark, String.Empty);

			return parsed;


		}
		#endregion

		#region LoginPathPartRegex
		public Regex LoginPathPartRegex
		{
			get
			{
				if (loginPathPartRegex == null)
					loginPathPartRegex = new Regex("^login-[0-9]{1,}-[a-zA-Z]{6}$");
				return loginPathPartRegex;
			}
			set
			{
				loginPathPartRegex = value;
			}
		}
		Regex loginPathPartRegex;
		#endregion

		#region AttributeUrlDomainRegex
		Regex AttributeUrlDomainRegex
		{
			get
			{
				if (attributeUrlDomainRegex == null)
					attributeUrlDomainRegex = new Regex(@"[^a-zA-Z0-9.\-_]");
				return attributeUrlDomainRegex;
			}
		}
		Regex attributeUrlDomainRegex;
		#endregion

		#region AttributeEmailRegex
		Regex AttributeEmailRegex
		{
			get
			{
				if (attributeEmailRegex == null)
					attributeEmailRegex = new Regex(@"[^a-zA-Z0-9.\-_@]");
				return attributeEmailRegex;
			}
		}
		Regex attributeEmailRegex;
		#endregion
		#endregion

		#region Number
		string ParseAttributeNumber(string valueIn)
		{
			try
			{
				if (valueIn.EndsWith("%"))
				{
					return AttributeNumberRegex.Replace(valueIn.Substring(0, valueIn.Length - 1), String.Empty) + "%";
				}
				else if (valueIn.EndsWith("em"))
				{
					return AttributeNumberRegex.Replace(valueIn.Substring(0, valueIn.Length - 2), String.Empty) + "em";
				}
				else if (valueIn.EndsWith("px"))
				{
					return AttributeNumberRegex.Replace(valueIn.Substring(0, valueIn.Length - 2), String.Empty) + "px";
				}
				else if (valueIn.StartsWith("+"))
				{
					return "+" + AttributeNumberRegex.Replace(valueIn.Substring(1), String.Empty);
				}
				else if (valueIn.StartsWith("-"))
				{
					return "-" + AttributeNumberRegex.Replace(valueIn.Substring(1), String.Empty);
				}
				else
				{
					return AttributeNumberRegex.Replace(valueIn, String.Empty);
				}
			}
			catch
			{
				return "0";
			}
		}
		Regex AttributeNumberRegex
		{
			get
			{
				if (attributeNumberRegex == null)
					attributeNumberRegex = new Regex("[^0-9]");
				return attributeNumberRegex;
			}
		}
		Regex attributeNumberRegex;
		#endregion

		#region Colour
		string ParseAttributeColour(string valueIn)
		{
			valueIn = valueIn.ToLower();
			if (valueIn.Length == 7 && AttributeColourRegex.IsMatch(valueIn))
				return valueIn;
			else
				return AttributeTextRegex.Replace(valueIn, String.Empty);
		}
		Regex AttributeColourRegex
		{
			get
			{
				if (attributeColourRegex == null)
					attributeColourRegex = new Regex("^#[0-9a-f]{6}$");
				return attributeColourRegex;
			}
		}
		Regex attributeColourRegex;
		#endregion

		#region UseMap
		string ParseAttributeUseMap(string valueIn)
		{
			if (valueIn.StartsWith("#"))
				return "#" + ParseAttributeText(valueIn.Substring(1));
			else
				return ParseAttributeText(valueIn);
		}
		#endregion

		#region List
		string ParseAttributeList(string valueIn)
		{
			string valueOut = "";
			foreach (string s in valueIn.Replace("&amp;", "\n[ampersand-replacement]\n").Replace("&amp%3b", "\n[ampersand-replacement-encoded]\n").Split('&'))
			{
				valueOut += valueOut.Length == 0 ? "" : "&";
				if (s.Contains("="))
				{
					valueOut += urlDecodeEncode(s.Substring(0, s.IndexOf('=')));
					valueOut += "=";
					valueOut += urlDecodeEncode(s.Substring(s.IndexOf('=') + 1).Replace("\n[ampersand-replacement]\n", "&amp;").Replace("\n[ampersand-replacement-encoded]\n", "&amp%3b"));
				}
				else
				{
					valueOut += urlDecodeEncode(s);
				}
			}
			return valueOut;
		}
		#endregion

		#endregion

		#region Parse style attrbite

		#region ParseAttributeStyle
		string ParseAttributeStyle(string valueIn)
		{
			string valueOut = "";
			string[] styleElementAry = valueIn.Split(';');
			foreach (string styleElement in styleElementAry)
			{
				try
				{
					if (styleElement.IndexOf(":") > 0)
					{
						string elementName = styleElement.Substring(0, styleElement.IndexOf(":")).Trim().ToLower();
						elementName = StyleNameRegex.Replace(elementName, String.Empty);

						string elementValue = styleElement.Substring(styleElement.IndexOf(":") + 1).Trim();

						string parsedValue = StyleElement.Parse(this, elementName, elementValue, this);

						if (parsedValue.Length > 0)
							valueOut += elementName + ": " + parsedValue + "; ";
					}
				}
				catch { }
			}
			return valueOut;
		}
		#endregion

		#region class StyleElement
		class StyleElement
		{
			HtmlWriter Parent;
			string[] ValidTextValues;
			int MaxSubElements;
			Types ValidElementTypes;

			#region public StyleElement(...)
			public StyleElement(
				HtmlWriter parent,
				params string[] validTextValues)
			{
				Parent = parent;
				ValidTextValues = validTextValues;
				MaxSubElements = 1;
				ValidElementTypes = Types.Text;
			}

			public StyleElement(
				HtmlWriter parent,
				int maxSubElements,
				params string[] validTextValues)
			{
				Parent = parent;
				ValidTextValues = validTextValues;
				MaxSubElements = maxSubElements;
				ValidElementTypes = Types.Text;
			}

			public StyleElement(
				HtmlWriter parent,
				Types validElementTypes)
			{
				Parent = parent;
				MaxSubElements = 1;
				ValidElementTypes = validElementTypes;
				ValidTextValues = new string[] { };

			}

			public StyleElement(
				HtmlWriter parent,
				int maxSubElements,
				Types validElementTypes)
			{
				Parent = parent;
				MaxSubElements = maxSubElements;
				ValidElementTypes = validElementTypes;
				ValidTextValues = new string[] { };
			}

			public StyleElement(
				HtmlWriter parent,
				int maxSubElements,
				Types validElementTypes,
				params string[] validTextValues)
			{
				Parent = parent;
				MaxSubElements = maxSubElements;
				ValidElementTypes = validElementTypes | Types.Text;
				ValidTextValues = validTextValues;
			}

			public StyleElement(
				HtmlWriter parent,
				Types validElementTypes,
				params string[] validTextValues)
			{
				Parent = parent;
				MaxSubElements = 1;
				ValidElementTypes = validElementTypes | Types.Text;
				ValidTextValues = validTextValues;
			}
			#endregion

			#region Types
			[Flags]
			public enum Types
			{
				None = 0,
				Text = 1,                 // lower case alpha, hyphen
				Url = 2,                  // url(...)
				Percentage = 4,           // int%
				Length = 8,               // int[.int][em/px/pt...]
				Colour = 16,              // #ffffff or text
				Shape = 32,               // rect(length length length length)
				Integer = 64,             // int
				FontFamily = 128,         // alpha seperated by [, ] or [,]
				FontSizeLineHeight = 256, // length[/length]
				Number = 512,             // float
			}
			#endregion

			#region IsValidTextValue
			public bool IsValidTextValue(string s)
			{
				foreach (string textValue in ValidTextValues)
				{
					if (textValue.Equals(s))
					{
						return true;
					}
				}
				return false;
			}
			#endregion

			#region GetStyleElement
			public static StyleElement GetStyleElement(HtmlWriter parent, string name, HtmlWriter writer)
			{
				#region Get from definition cache
				if (!writer.StyleElementCache.ContainsKey(name))
					writer.StyleElementCache[name] = GetStyleElementPrivate(parent, name);
				return writer.StyleElementCache[name];
				#endregion
			}
			static StyleElement GetStyleElementPrivate(HtmlWriter parent, string name)
			{
				#region Element definitions
				switch (name)
				{
					//Extra style elements for dsi tags
					case "content": return new StyleElement(parent, "text", "icon", "text-under-icon");    // for type=usr, event, venue, place, group, brand
					case "details": return new StyleElement(parent, "none", "venue", "place", "country");  // for type=event, venue, place
					case "date": return new StyleElement(parent, "false", "true");                         // for type=event
					case "snip": return new StyleElement(parent, Types.Number);                            // for type=event
					case "rollover": return new StyleElement(parent, "true", "false");                     // for type=usr, photo
					case "photo": return new StyleElement(parent, "icon", "thumb", "web");                 // for type=photo
					case "link": return new StyleElement(parent, "true", "false");

					case "font-family": return new StyleElement(parent, Types.FontFamily);
					case "font-style": return new StyleElement(parent, "normal", "italic");
					case "font-variant": return new StyleElement(parent, "normal", "small-caps");
					case "font-weight": return new StyleElement(parent, "normal", "bold");
					case "font-size": return new StyleElement(parent, Types.Percentage | Types.Length, "xx-large", "x-large", "large", "medium", "small", "x-small", "xx-small", "larger", "smaller");
					case "font": return new StyleElement(parent, 5, Types.Percentage | Types.Length | Types.FontSizeLineHeight | Types.FontFamily, "normal", "italic", "normal", "small-caps", "normal", "bold", "xx-large", "x-large", "large", "medium", "small", "x-small", "xx-small", "larger", "smaller");
					case "color": return new StyleElement(parent, Types.Colour);
					case "background-color": return new StyleElement(parent, Types.Colour, "transparent");
					case "background-image": return new StyleElement(parent, Types.Url, "none");
					case "background-repeat": return new StyleElement(parent, "repeat", "repeat-x", "repeat-y", "no-repeat");
					case "background-attachment": return new StyleElement(parent, "scroll", "fixed");
					case "background-position": return new StyleElement(parent, 2, Types.Length | Types.Percentage, "top", "center", "bottom", "left", "right");
					case "background": return new StyleElement(parent, 6, Types.Colour | Types.Url | Types.Length | Types.Percentage, "transparent", "none", "repeat", "repeat-x", "repeat-y", "no-repeat", "scroll", "fixed", "top", "center", "bottom", "left", "right");
					case "letter-spacing": return new StyleElement(parent, Types.Length, "normal");
					case "text-decoration": return new StyleElement(parent, "none", "underline", "overline", "line-through");
					case "vertical-align": return new StyleElement(parent, "sub", "super");
					case "text-transform": return new StyleElement(parent, "capitalize", "uppercase", "lowercase", "none");
					case "text-align": return new StyleElement(parent, "left", "right", "center", "justify");
					case "text-indent": return new StyleElement(parent, Types.Length | Types.Percentage);
					case "line-height": return new StyleElement(parent, Types.Number | Types.Length | Types.Percentage, "normal");
					case "margin-top": return new StyleElement(parent, Types.Length | Types.Percentage, "auto");
					case "margin-right": return new StyleElement(parent, Types.Length | Types.Percentage, "auto");
					case "margin-bottom": return new StyleElement(parent, Types.Length | Types.Percentage, "auto");
					case "margin-left": return new StyleElement(parent, Types.Length | Types.Percentage, "auto");
					case "margin": return new StyleElement(parent, 4, Types.Length | Types.Percentage, "auto");
					case "padding-top": return new StyleElement(parent, Types.Length | Types.Percentage);
					case "padding-right": return new StyleElement(parent, Types.Length | Types.Percentage);
					case "padding-bottom": return new StyleElement(parent, Types.Length | Types.Percentage);
					case "padding-left": return new StyleElement(parent, Types.Length | Types.Percentage);
					case "padding": return new StyleElement(parent, 4, Types.Length | Types.Percentage);
					case "border-top-width": return new StyleElement(parent, Types.Length, "thin", "medium", "thick");
					case "border-right-width": return new StyleElement(parent, Types.Length, "thin", "medium", "thick");
					case "border-bottom-width": return new StyleElement(parent, Types.Length, "thin", "medium", "thick");
					case "border-left-width": return new StyleElement(parent, Types.Length, "thin", "medium", "thick");
					case "border-width": return new StyleElement(parent, 4, Types.Length, "thin", "medium", "thick");
					case "border-top-color": return new StyleElement(parent, Types.Colour);
					case "border-right-color": return new StyleElement(parent, Types.Colour);
					case "border-bottom-color": return new StyleElement(parent, Types.Colour);
					case "border-left-color": return new StyleElement(parent, Types.Colour);
					case "border-color": return new StyleElement(parent, 4, Types.Colour);
					case "border-top-style": return new StyleElement(parent, "none", "solid", "double", "groove", "ridge", "inset", "outset");
					case "border-right-style": return new StyleElement(parent, "none", "solid", "double", "groove", "ridge", "inset", "outset");
					case "border-bottom-style": return new StyleElement(parent, "none", "solid", "double", "groove", "ridge", "inset", "outset");
					case "border-left-style": return new StyleElement(parent, "none", "solid", "double", "groove", "ridge", "inset", "outset");
					case "border-style": return new StyleElement(parent, "none", "solid", "double", "groove", "ridge", "inset", "outset");
					case "border-top": return new StyleElement(parent, 3, Types.Length | Types.Colour, "thin", "medium", "thick", "none", "solid", "double", "groove", "ridge", "inset", "outset");
					case "border-right": return new StyleElement(parent, 3, Types.Length | Types.Colour, "thin", "medium", "thick", "none", "solid", "double", "groove", "ridge", "inset", "outset");
					case "border-bottom": return new StyleElement(parent, 3, Types.Length | Types.Colour, "thin", "medium", "thick", "none", "solid", "double", "groove", "ridge", "inset", "outset");
					case "border-left": return new StyleElement(parent, 3, Types.Length | Types.Colour, "thin", "medium", "thick", "none", "solid", "double", "groove", "ridge", "inset", "outset");
					case "border": return new StyleElement(parent, 3, Types.Length | Types.Colour, "thin", "medium", "thick", "none", "solid", "double", "groove", "ridge", "inset", "outset");
					case "float": return new StyleElement(parent, "none", "left", "right");
					case "clear": return new StyleElement(parent, "none", "left", "right", "both");
					case "display": return new StyleElement(parent, "none", "block", "inline", "list-item");
					case "list-style-type": return new StyleElement(parent, "disk", "circle", "square", "decimal", "lower-roman", "upper-roman", "lower-alpha", "upper-alpha", "none");
					case "list-style-image": return new StyleElement(parent, Types.Url, "none");
					case "list-style-position": return new StyleElement(parent, "inside", "outside");
					case "list-style": return new StyleElement(parent, 3, Types.Url, "disk", "circle", "square", "decimal", "lower-roman", "upper-roman", "lower-alpha", "upper-alpha", "none", "inside", "outside");
					case "clip": return new StyleElement(parent, Types.Shape, "auto");
					case "height": return new StyleElement(parent, Types.Length, "auto");
					case "left": return null;// return new StyleElement(parent, Types.Length | Types.Percentage, "auto"); REMOVED
					case "overflow": return new StyleElement(parent, "visible", "hidden", "scroll", "auto");
					case "position": return null; // return new StyleElement(parent, "absolute", "relative", "static"); REMOVED
					case "top": return null;// return new StyleElement(parent, Types.Length | Types.Percentage, "auto"); REMOVED
					case "visibility": return new StyleElement(parent, "visible", "hidden", "inherit");
					case "width": return new StyleElement(parent, Types.Length | Types.Percentage, "auto");
					case "z-index": return new StyleElement(parent, Types.Integer, "auto");
					case "page-break-before": return new StyleElement(parent, 2, "auto", "always", "left", "right");
					case "page-break-after": return new StyleElement(parent, 2, "auto", "always", "left", "right");
					case "cursor": return new StyleElement(parent, "auto", "crosshair", "default", "hand", "move", "e-resize", "ne-resize", "nw-resize", "n-resize", "se-resize", "sw-resize", "s-resize", "w-resize", "text", "wait", "help");
					default: return null;
				}
				#endregion
			}
			#endregion

			#region Parse
			public static string Parse(HtmlWriter parent, string name, string value, HtmlWriter writer)
			{
				StringBuilder builder = new StringBuilder();
				StyleElement element = GetStyleElement(parent, name, writer);
				StringReader reader = new StringReader(value);
				System.Collections.Generic.List<string> subElements = new System.Collections.Generic.List<string>();
				System.Collections.Generic.List<string> parsedSubElements = new System.Collections.Generic.List<string>();

				#region Split elements
				bool skipSpaces = false;
				bool skipUntilApos = false;
				bool skipUntilCloseBracket = false;
				int thisInt;
				Char thisChar;
				while (true)
				{
					thisInt = reader.Read();
					if (thisInt == -1)
					{
						if (builder.Length > 0)
							subElements.Add(builder.ToString());
						break;
					}

					thisChar = Convert.ToChar(thisInt);

					if (skipSpaces && thisChar != ' ')
						skipSpaces = false;

					if (!skipSpaces && thisChar == ',')
						skipSpaces = true;

					if (skipUntilApos && thisChar == '\'')
						skipUntilApos = false;

					if (!skipUntilApos && thisChar == '\'')
						skipUntilApos = true;

					if (skipUntilCloseBracket && thisChar == ')')
						skipUntilCloseBracket = false;

					if (!skipUntilCloseBracket && thisChar == '(')
						skipUntilCloseBracket = true;

					if (thisChar == ' ' && !skipSpaces && !skipUntilApos && !skipUntilCloseBracket)
					{
						if (builder.Length > 0)
							subElements.Add(builder.ToString());
						builder.Length = 0;
						continue;
					}

					builder.Append(thisChar);
				}
				#endregion

				#region Parse elements
				string proposedElement;
				string proposedElementLower;
				for (int i = 0; i < subElements.Count && parsedSubElements.Count <= element.MaxSubElements; i++)
				{
					proposedElement = subElements[i].Trim();
					proposedElementLower = subElements[i].ToLower().Trim();

					#region Text
					try
					{
						if ((element.ValidElementTypes & Types.Text) != 0)
						{
							if (element.IsValidTextValue(proposedElement.ToLower()))
							{
								parsedSubElements.Add(proposedElement.ToLower());
								continue;
							}
						}
					}
					catch { }
					#endregion
					#region Url
					try
					{
						if ((element.ValidElementTypes & Types.Url) != 0)
						{
							if (proposedElementLower.StartsWith("url(") && proposedElementLower.EndsWith(")"))
							{
								string urlUnparsed = proposedElement.Substring(4, proposedElement.Length - 5).Trim();
								string surroundChar = "";

								if (urlUnparsed.StartsWith("'") && urlUnparsed.EndsWith("'"))
								{
									urlUnparsed = urlUnparsed.Substring(1, urlUnparsed.Length - 2);
									surroundChar = "'";
								}
								else if (urlUnparsed.StartsWith("\"") && urlUnparsed.EndsWith("\""))
								{
									urlUnparsed = urlUnparsed.Substring(1, urlUnparsed.Length - 2);
									surroundChar = "\"";
								}

								string url = element.Parent.ParseAttributeUrl(urlUnparsed, false);

								if (url.Length > 0)
									parsedSubElements.Add("url(" + surroundChar + url + surroundChar + ")");

								continue;

							}
						}
					}
					catch { }
					#endregion
					#region Percentage
					try
					{
						if ((element.ValidElementTypes & Types.Percentage) != 0)
						{
							if (writer.StylePercentageRegex.IsMatch(proposedElement))
							{
								parsedSubElements.Add(proposedElement);
								continue;
							}
						}
					}
					catch { }
					#endregion
					#region Length
					try
					{
						if ((element.ValidElementTypes & Types.Length) != 0)
						{
							if (writer.StyleLengthRegex.IsMatch(proposedElementLower))
							{
								parsedSubElements.Add(proposedElementLower);
								continue;
							}
						}
					}
					catch { }
					#endregion
					#region Colour
					try
					{
						if ((element.ValidElementTypes & Types.Colour) != 0)
						{
							if (writer.AttributeColourRegex.IsMatch(proposedElementLower) ||
								writer.StyleColourRgbRegex.IsMatch(proposedElementLower) ||
								writer.StyleColourNameRegex.IsMatch(proposedElementLower))
							{
								parsedSubElements.Add(proposedElementLower);
								continue;
							}
						}
					}
					catch { }
					#endregion
					#region Integer
					try
					{
						if ((element.ValidElementTypes & Types.Integer) != 0)
						{
							if (writer.StyleIntegerRegex.IsMatch(proposedElement))
							{
								parsedSubElements.Add(proposedElement);
								continue;
							}
						}
					}
					catch { }
					#endregion
					#region Number
					try
					{
						if ((element.ValidElementTypes & Types.Number) != 0)
						{
							if (writer.StyleNumberRegex.IsMatch(proposedElement))
							{
								parsedSubElements.Add(proposedElement);
								continue;
							}
						}
					}
					catch { }
					#endregion
					#region Shape
					try
					{
						if ((element.ValidElementTypes & Types.Shape) != 0)
						{
							if (proposedElementLower.StartsWith("rect(") && proposedElementLower.EndsWith(")"))
							{
								string coords = proposedElement.Substring(5, proposedElement.Length - 6).Trim();
								string[] coordsAry = coords.Split(',');
								if (writer.StyleLengthRegex.IsMatch(coordsAry[0].Trim()) &&
									writer.StyleLengthRegex.IsMatch(coordsAry[1].Trim()) &&
									writer.StyleLengthRegex.IsMatch(coordsAry[2].Trim()) &&
									writer.StyleLengthRegex.IsMatch(coordsAry[3].Trim()))
								{
									parsedSubElements.Add(proposedElementLower);
									continue;
								}
							}
						}
					}
					catch { }
					#endregion
					#region FontSizeLineHeight
					try
					{
						if ((element.ValidElementTypes & Types.FontSizeLineHeight) != 0)
						{
							if (proposedElementLower.IndexOf('/') > -1)
							{
								string fontSize = proposedElementLower.Substring(0, proposedElementLower.IndexOf('/'));
								string lineHeight = proposedElementLower.Substring(proposedElementLower.IndexOf('/') + 1);

								if ((fontSize.Equals("xx-small") || fontSize.Equals("x-small") || fontSize.Equals("small") || fontSize.Equals("medium") || fontSize.Equals("large") || fontSize.Equals("x-large") || fontSize.Equals("xx-large") || fontSize.Equals("smaller") || fontSize.Equals("larger") || writer.StyleLengthRegex.IsMatch(fontSize) || writer.StylePercentageRegex.IsMatch(fontSize)) &&
									(lineHeight.Equals("xx-small") || lineHeight.Equals("x-small") || lineHeight.Equals("small") || lineHeight.Equals("medium") || lineHeight.Equals("large") || lineHeight.Equals("x-large") || lineHeight.Equals("xx-large") || lineHeight.Equals("smaller") || lineHeight.Equals("larger") || writer.StyleNumberRegex.IsMatch(lineHeight) || writer.StyleLengthRegex.IsMatch(lineHeight) || writer.StylePercentageRegex.IsMatch(lineHeight)))
								{
									parsedSubElements.Add(proposedElementLower);
									continue;
								}
							}
						}
					}
					catch { }
					#endregion
					#region FontFamily
					try
					{
						if ((element.ValidElementTypes & Types.FontFamily) != 0)
						{
							string parsedFontFamily = writer.StyleFontFamilyRegex.Replace(proposedElement, String.Empty);
							if (parsedFontFamily.Length > 0)
							{
								parsedSubElements.Add(parsedFontFamily);
								continue;
							}
						}
					}
					catch { }
					#endregion
				}
				#endregion

				#region Build output
				builder.Length = 0;
				foreach (string parsedElement in parsedSubElements)
				{
					if (builder.Length > 0)
						builder.Append(" ");
					builder.Append(parsedElement);
				}
				return builder.ToString();
				#endregion

			}
			#endregion

		}
		#endregion

		#region StyleElementCache
		System.Collections.Generic.Dictionary<string, StyleElement> StyleElementCache
		{
			get
			{
				if (styleElementCache == null)
					styleElementCache = new System.Collections.Generic.Dictionary<string, StyleElement>();
				return styleElementCache;
			}
		}
		System.Collections.Generic.Dictionary<string, StyleElement> styleElementCache;
		#endregion

		#region Style Regex's
		#region StyleNameRegex
		Regex StyleNameRegex
		{
			get
			{
				if (styleNameRegex == null)
					styleNameRegex = new Regex("[^-a-z]");
				return styleNameRegex;
			}
		}
		Regex styleNameRegex;
		#endregion
		#region StyleNumberRegex
		Regex StyleNumberRegex
		{
			get
			{
				if (styleNumberRegex == null)
					styleNumberRegex = new Regex("^[-+]?[0-9]+[.]?[0-9]*$");
				return styleNumberRegex;
			}
		}
		Regex styleNumberRegex;
		#endregion
		#region StyleIntegerRegex
		Regex StyleIntegerRegex
		{
			get
			{
				if (styleIntegerRegex == null)
					styleIntegerRegex = new Regex("^[-+]{0,1}[0-9]{1,}$");
				return styleIntegerRegex;
			}
		}
		Regex styleIntegerRegex;
		#endregion
		#region StyleLengthRegex
		Regex StyleLengthRegex
		{
			get
			{
				if (styleLengthRegex == null)
					styleLengthRegex = new Regex("^[-+]{0,1}[0-9]{1,}[.]{0,1}[0-9]{0,}(px|em|ex|in|cm|mm|pi|pt)$");
				return styleLengthRegex;
			}
		}
		Regex styleLengthRegex;
		#endregion
		#region StylePercentageRegex
		Regex StylePercentageRegex
		{
			get
			{
				if (stylePercentageRegex == null)
					stylePercentageRegex = new Regex("^[-+]{0,1}[0-9]{1,}[.]{0,1}[0-9]{0,}%$");
				return stylePercentageRegex;
			}
		}
		Regex stylePercentageRegex;
		#endregion
		#region StyleColourRgbRegex
		Regex StyleColourRgbRegex
		{
			get
			{
				if (styleColourRgbRegex == null)
					styleColourRgbRegex = new Regex(@"^rgb\([ ]{0,1}[0-9]{1,3}[,]{1}[ ]{0,1}[0-9]{1,3}[,]{1}[ ]{0,1}[0-9]{1,3}[ ]{0,1}\)$");
				return styleColourRgbRegex;
			}
		}
		Regex styleColourRgbRegex;
		#endregion
		#region StyleColourNameRegex
		Regex StyleColourNameRegex
		{
			get
			{
				if (styleColourNameRegex == null)
					styleColourNameRegex = new Regex(@"^(aqua|black|blue|fuchsia|gray|green|lime|maroon|navy|olive|purple|red|silver|teal|white|yellow)$");
				return styleColourNameRegex;
			}
		}
		Regex styleColourNameRegex;
		#endregion
		#region StyleFontFamilyRegex
		Regex StyleFontFamilyRegex
		{
			get
			{
				if (styleFontFamilyRegex == null)
					styleFontFamilyRegex = new Regex(@"[^a-zA-Z0-9 ,']");
				return styleFontFamilyRegex;
			}
		}
		Regex styleFontFamilyRegex;
		#endregion
		#endregion

		#endregion

		#region WriteNode
		#region WriteNode
		public override void WriteNode(XmlReader reader, bool defattr)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			//Removed by DaveB 22/02/2008 to simplify automatic link generation process
			//bool canReadValueChunk = reader.CanReadValueChunk;
			int depth = (reader.NodeType == XmlNodeType.None) ? -1 : reader.Depth;
			do
			{
				try
				{
					if (reader.NodeType == XmlNodeType.Element && (reader.LocalName.ToLower() == "object" || reader.LocalName.ToLower() == "embed"))
					{
						processObjectTag(reader);
					}

					switch (reader.NodeType)
					{
						case XmlNodeType.Element:
							{
								bool isEmptyTag = IsEmptyTag(reader.LocalName);
								bool isAlowedTag = IsAlowedTag(reader.LocalName);
								if (isAlowedTag && // don't render illegal tags
									IsTagContainerOk(reader.LocalName) && // don't render anything in the wrong container - e.g. <tr> outside <table>
									!(reader.IsEmptyElement && !isEmptyTag)) // don't render empty tags (unless they are always empty - e.g. <br>'s)
								{
									string localName = reader.LocalName; //see below for why we do this...
									this.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
									this.WriteAttributes(reader, defattr); //I've found a bug where this line breaks the reader, so that the LogStartTag(reader.LocalName) doesn't work. Fixed by reading localName before the write the attributes.
									if (reader.IsEmptyElement || isEmptyTag)
									{
										this.WriteEndElement();
									}
									else
										LogStartTag(localName); //see above for why we do this...
								}
								else
								{
									int i = 1;
									//skipping end tag...
								}
								break;
							}
						case XmlNodeType.Text:
							{
								this.WriteString(reader.Value);
								break;

								//Removed by DaveB 22/02/2008 to simplify automatic link generation process
								//int num2;
								//if (!canReadValueChunk)
								//{
								//    this.WriteString(reader.Value);
								//    break;
								//}
								//if (this.WriteNodeBuffer == null)
								//{
								//    this.WriteNodeBuffer = new char[0x400];
								//}
								//while ((num2 = reader.ReadValueChunk(this.WriteNodeBuffer, 0, 0x400)) > 0)
								//{	
								//    this.WriteChars(this.WriteNodeBuffer, 0, num2);
								//}
								//break;
							}
						case XmlNodeType.Whitespace:
						case XmlNodeType.SignificantWhitespace:
							{
								this.WriteWhitespace(reader.Value);
								break;
							}
						case XmlNodeType.EndElement:
							{
								if (IsAlowedTag(reader.LocalName) && IsTagContainerOk(reader.LocalName) && !IsEmptyTag(reader.LocalName))
								{
									this.WriteFullEndElement();
									LogEndTag(reader.LocalName);
								}
								else
								{
									int i = 1;
									//skipping end tag...
								}
								break;
							}
					}
				}
				catch { }
			}
			while (reader.Read() && ((depth < reader.Depth) || ((depth == reader.Depth) && (reader.NodeType == XmlNodeType.EndElement))));
		}
		char[] WriteNodeBuffer;
		#endregion
		#region processObjectTag(XmlReader reader)
		void processObjectTag(XmlReader reader)
		{
			Dictionary<string, string> objectParams = new Dictionary<string, string>();

			int depth = reader.Depth;
			do
			{
				try
				{
					switch (reader.NodeType)
					{
						case XmlNodeType.Element:
							{
								if (reader.LocalName.ToLower() == "object" || reader.LocalName.ToLower() == "embed")
								{
									while (reader.MoveToNextAttribute())
									{
										if (!objectParams.ContainsKey(reader.Name.ToLower()))
											objectParams.Add(reader.Name.ToLower(), reader.Value);
										else
											objectParams[reader.Name.ToLower()] = reader.Value;
									}
								}
								else if (reader.LocalName.ToLower() == "param")
								{
									string name = reader["name"].ToLower();
									string value = reader["value"];
									if (!objectParams.ContainsKey(name))
										objectParams.Add(name, value);
									else
										objectParams[name] = value;

									while (reader.MoveToNextAttribute())
									{
										if (reader.Name.ToLower() != "name" && reader.Name.ToLower() != "value")
										{
											if (!objectParams.ContainsKey(reader.Name.ToLower()))
												objectParams.Add(reader.Name.ToLower(), reader.Value);
											else
												objectParams[reader.Name.ToLower()] = reader.Value;
										}
									}
								}
								break;
							}
					}
				}
				catch { }
			}
			while (reader.Read() && ((depth < reader.Depth) || ((depth == reader.Depth) && (reader.NodeType == XmlNodeType.EndElement))));

			string src = "";
			if (objectParams.ContainsKey("movie"))
				src = ParseAttribute(objectParams["movie"], GetAttributeType("src"));
			else if (objectParams.ContainsKey("src"))
				src = ParseAttribute(objectParams["src"], GetAttributeType("src"));

			if (src.Length > 0)
			{
				if (objectParams.ContainsKey("style"))
				{
					foreach (string styleAttributeValuePairString in objectParams["style"].Split(';'))
					{
						if (styleAttributeValuePairString.Contains(":"))
						{
							string[] styleAttributeValuePair = styleAttributeValuePairString.Trim().Split(':');

							if (!objectParams.ContainsKey(styleAttributeValuePair[0].Trim().ToLower()))
								objectParams.Add(styleAttributeValuePair[0].Trim().ToLower(), styleAttributeValuePair[1].Trim().ToLower());
							else
								objectParams[styleAttributeValuePair[0].Trim().ToLower()] = styleAttributeValuePair[1].Trim().ToLower();
						}
					}
				}

				this.WriteStartElement(reader.Prefix, "dsi:flash", reader.NamespaceURI);

				this.writeAttributeStringWithoutParsingForUrls("src", src);

				if (objectParams.ContainsKey("width"))
					ParseAndWriteAttribute("width", AttributeNumberRegex.Replace(objectParams["width"], String.Empty));
				else
					ParseAndWriteAttribute("width", "450");

				if (objectParams.ContainsKey("height"))
					ParseAndWriteAttribute("height", AttributeNumberRegex.Replace(objectParams["height"], String.Empty));
				else
					ParseAndWriteAttribute("height", "300");

				if (objectParams.ContainsKey("play") && objectParams["play"] != "true")
					ParseAndWriteAttribute("play", objectParams["play"]);

				if (objectParams.ContainsKey("loop"))
					ParseAndWriteAttribute("loop", objectParams["loop"]);

				if (objectParams.ContainsKey("menu") && objectParams["menu"] != "false")
					ParseAndWriteAttribute("menu", objectParams["menu"]);

				if (objectParams.ContainsKey("quality"))
					ParseAndWriteAttribute("quality", objectParams["quality"]);

				if (objectParams.ContainsKey("scale"))
					ParseAndWriteAttribute("scale", objectParams["scale"]);

				if (objectParams.ContainsKey("align"))
					ParseAndWriteAttribute("align", objectParams["align"]);

				if (objectParams.ContainsKey("salign"))
					ParseAndWriteAttribute("salign", objectParams["salign"]);

				if (objectParams.ContainsKey("wmode") && objectParams["wmode"] != "transparent")
					ParseAndWriteAttribute("wmode", objectParams["wmode"]);

				if (objectParams.ContainsKey("bgcolor"))
					ParseAndWriteAttribute("bgcolor", objectParams["bgcolor"]);

				if (objectParams.ContainsKey("base"))
					ParseAndWriteAttribute("base", objectParams["base"]);

				if (objectParams.ContainsKey("flashvars"))
					ParseAndWriteAttribute("flashvars", objectParams["flashvars"]);

				this.WriteEndElement();
			}
		}
		void ParseAndWriteAttribute(string name, string value)
		{
			string parsedValue = ParseAttribute(value, GetAttributeType(name));
			if (parsedValue.Length > 0)
				this.writeAttributeStringWithoutParsingForUrls(name, parsedValue);
		}
		#endregion
		#region writeAttributeStringWithoutParsingForUrls
		void writeAttributeStringWithoutParsingForUrls(string localName, string value)
		{
			this.WriteStartAttribute(null, localName, null);
			this.WriteRaw(value);
			this.WriteEndAttribute();
		}
		#endregion
		#endregion

		#region WriteAttributes
		/// <summary>
		/// This method is overriden to filter out attributes which are not allowed
		/// </summary>
		public override void WriteAttributes(XmlReader reader, bool defattr)
		{
			// The following code is copied from implementation of XmlWriter's
			// WriteAttributes method. 
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if ((reader.NodeType == XmlNodeType.Element) || (reader.NodeType == XmlNodeType.XmlDeclaration))
			{
				if (reader.MoveToFirstAttribute())
				{
					try
					{
						this.WriteAttributes(reader, defattr);
					}
					catch { }

					reader.MoveToElement();
				}
			}
			else
			{
				if (reader.NodeType != XmlNodeType.Attribute)
				{
					throw new XmlException("Xml_InvalidPosition");
				}
				do
				{
					try
					{
						if (defattr || !reader.IsDefault)
						{
							// Check if the attribute is allowed 
							string attributeLocalName = reader.LocalName.ToLower();
							AttributeTypes type = GetAttributeType(attributeLocalName);

							if (!type.Equals(AttributeTypes.Remove))
							{
								//parse the attribute
								string attribteValue = "";
								while (reader.ReadAttributeValue())
								{
									if (reader.NodeType.Equals(XmlNodeType.Text))
										attribteValue += reader.Value;
								}
								string parsedAttribteValue = ParseAttribute(attribteValue, type);

								//write the 
								if (parsedAttribteValue.Length > 0)
								{
									this.WriteStartAttribute(attributeLocalName);
									this.WriteRaw(parsedAttribteValue);
									//this.WriteString(parsedAttribteValue);
									this.WriteEndAttribute();
								}

							}
						}
					}
					catch { }
				}
				while (reader.MoveToNextAttribute());
			}
		}
		#endregion

		#region UrlRegex
		public Regex UrlRegex
		{
			get
			{
				if (urlRegex == null)
				{
					urlRegex = new Regex(@"(^|[^~]{1})((http|https)://([0-9a-zA-Z\._-]+)(.*?))(\s|$)", RegexOptions.IgnoreCase);
				}
				return urlRegex;
			}
			set
			{
				urlRegex = value;
			}
		}
		Regex urlRegex;
		#endregion

		#region UrlReplacementEval
		public MatchEvaluator UrlReplacementEval
		{
			get
			{
				if (urlReplacementEval == null)
					urlReplacementEval = new MatchEvaluator(urlReplacement);
				return urlReplacementEval;
			}
			set
			{
				urlReplacementEval = value;
			}
		}
		MatchEvaluator urlReplacementEval;
		#endregion

		#region WriteString
		/// <summary>
		/// The reason why we are overriding this method is, we do not want the output to be
		/// encoded for texts inside attribute and inside node elements. For example, all the &nbsp;
		/// gets converted to &amp;nbsp in output. But this does not 
		/// apply to HTML. In HTML, we need to have &nbsp; as it is.
		/// </summary>
		/// <param name="text"></param>
		public override void WriteString(string text)
		{
			if ((int)text[text.Length - 1] == 0x0000ffff)
				text = text.Substring(0, text.Length - 1);

			// Do some encoding of our own because we are going to use WriteRaw which won't
			// do any of the necessary encoding
			text = text.Replace("<", "&lt;");
			text = text.Replace(">", "&gt;");
			//	text = text.Replace("'", "&#39;");
			//	text = text.Replace("\"", "&#34;");

			text = UrlRegex.Replace(text, UrlReplacementEval);

			base.WriteRaw(text);
		}
		#endregion

		#region UrlReplacementGeneric
		public string UrlReplacementGeneric(Match m, string contents)
		{
			try
			{
				bool hasContents = contents != null && contents.Length > 0;

				if (m.Groups[4].Value.ToLower().EndsWith(".dontstayin.com") || m.Groups[4].Value.ToLower() == "localhost")
				{
					try
					{
						string urlWithoutBookmark = m.Groups[5].Value;
						string anchorBookmark = "";
						if (urlWithoutBookmark.Contains("#"))
						{
							anchorBookmark = urlWithoutBookmark.Substring(urlWithoutBookmark.IndexOf("#") + 1);
							urlWithoutBookmark = urlWithoutBookmark.Substring(0, urlWithoutBookmark.IndexOf("#"));
						}
						UrlInfo url = new UrlInfo(urlWithoutBookmark.ToLower(), null, true, false, false);

						if (url.HasCustomPage ||
							url.HasMusicFilter ||
							url.HasTagFilter ||
							url.HasThemeFilter)
							return getSimpleLink(m, contents);

						if (url.HasArticleObjectFilter ||
							url.HasBrandObjectFilter ||
							url.HasEventObjectFilter ||
							url.HasGroupObjectFilter ||
							url.HasPhotoObjectFilter ||
							url.HasPlaceObjectFilter ||
							url.HasUsrObjectFilter ||
							url.HasVenueObjectFilter)
						{
							//create dsi usr link...
							StringBuilder sb = new StringBuilder();
							TagBuilder t = new TagBuilder(hasContents ? "dsi:link" : "dsi:object", sb);

							if (url.HasEventObjectFilter && url.CurrentApplication == "photos" && url["photo"].IsInt)
							{
								Photo p = new Photo(url["photo"]);
								t.AddAttribute("type", "photo");
								t.AddAttribute("ref", p.K.ToString());
							}
							else
							{
								switch (url.ObjectFilterType)
								{
									case Model.Entities.ObjectType.Article: t.AddAttribute("type", "article"); break;
									case Model.Entities.ObjectType.Brand: t.AddAttribute("type", "brand"); break;
									case Model.Entities.ObjectType.Event: t.AddAttribute("type", "event"); break;
									case Model.Entities.ObjectType.Group: t.AddAttribute("type", "group"); break;
									case Model.Entities.ObjectType.Photo: t.AddAttribute("type", "photo"); break;
									case Model.Entities.ObjectType.Place: t.AddAttribute("type", "place"); break;
									case Model.Entities.ObjectType.Usr: t.AddAttribute("type", "usr"); break;
									case Model.Entities.ObjectType.Venue: t.AddAttribute("type", "venue"); break;
								}
								t.AddAttribute("ref", url.ObjectFilterK.ToString());


								if (url.CurrentApplication != null && url.CurrentApplication.Length > 0 && url.CurrentApplication != "home")
									t.AddAttribute("app", url.CurrentApplication);

								if (url.HasYearFilter && url.ObjectFilterType != Model.Entities.ObjectType.Event && url.ObjectFilterType != Model.Entities.ObjectType.Photo && url.ObjectFilterType != Model.Entities.ObjectType.Article)
								{
									string date = url.DateFilter.ToString("yyyy");
									if (url.HasMonthFilter)
										date += "-" + url.DateFilter.ToString("MMM").ToLower();
									if (url.HasDayFilter)
										date += "-" + url.DateFilter.ToString("dd");

									t.AddAttribute("date", date);
								}

								if (url.Count > 0)
								{
									string parStr = "";
									for (int i = 0; i < url.Count; i++)
									{
										if (url[i].HasKeyValuePair())
											parStr += (parStr.Length == 0 ? "" : "&") + System.Web.HttpUtility.UrlEncode(url[i].Key) + "=" + System.Web.HttpUtility.UrlEncode(url[i].Value);
										else
											parStr += (parStr.Length == 0 ? "" : "&") + System.Web.HttpUtility.UrlEncode(url[i].Key);

									}
									if (parStr.Length > 0)
										t.AddAttribute("par", parStr);
								}
							}

							if (anchorBookmark.Length > 0)
								t.AddAttribute("jump",  urlDecodeEncode(anchorBookmark));

							if (hasContents)
							{
								t.RenderStartTag();
								t.RenderContentAndEndTag(contents);
							}
							else
								t.RenderEmptyTag();

							return addLeadingAndTrailingWhitespace(m, sb.ToString());

						}
						else
							return getSimpleLink(m, contents);
					}
					catch
					{
						return getSimpleLink(m, contents);
					}
				}
				else
				{
					//just create a link
					return getSimpleLink(m, contents);
				}

			}
			catch
			{
				//if (Bobs.Vars.Devenv
				return "Error!";
			}
		}
		#endregion
		#region urlReplacement
		public string urlReplacement(Match m)
		{
			return UrlReplacementGeneric(m, null);
		}
		#endregion
		#region addLeadingAndTrailingWhitespace
		string addLeadingAndTrailingWhitespace(Match m, string innerContent)
		{
			return m.Groups[1] + innerContent + m.Groups[m.Groups.Count - 1];
		}
		#endregion
		#region getSimpleLink
		string getSimpleLink(Match m, string contents)
		{
			//return addLeadingAndTrailingWhitespace(m, "<a href=\"" + m.Groups[1] + "\">[" + m.Groups[3] + "]</a>");
			string parsedUrl = this.ParseAttribute(m.Groups[2].Value, AttributeTypes.Href);

			if (parsedUrl.Length > 0)
			{
				if (contents != null && contents.Length > 0)
					return addLeadingAndTrailingWhitespace(m, "<dsi:link type=\"url\" href=\"" + parsedUrl + "\">" + contents + "</dsi:link>");
				else
					return addLeadingAndTrailingWhitespace(m, "<dsi:object type=\"url\" href=\"" + parsedUrl + "\" />");
			}
			else
				return addLeadingAndTrailingWhitespace(m, contents ?? "");
		}
		#endregion


	}
	#endregion

	
}
