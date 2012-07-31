using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Configuration;
using Bobs;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Xml;
using System.Web.Security;
using System.Security.Principal;
using System.Runtime.InteropServices;
using System.CodeDom.Compiler;
using System.Text;
using System.Security.Cryptography;
using System.Xml.Serialization;
using System.Globalization;

namespace Cambro
{

	#region Web Request

	/*
	#region Get Image full from web request
				HttpWebRequest loHttp =
					(HttpWebRequest) WebRequest.Create(ImageUrl);
				loHttp.Timeout = 5000;
				loHttp.UserAgent = "Cropper";
				HttpWebResponse loWebResponse = (HttpWebResponse) loHttp.GetResponse();
				Encoding enc = Encoding.GetEncoding(1252);
				//	StreamReader loResponseStream =
				//		new StreamReader(loWebResponse.GetResponseStream(),enc);
				//	string lcHtml = loResponseStream.ReadToEnd();
				fullImage = System.Drawing.Image.FromStream(loWebResponse.GetResponseStream());
				loWebResponse.Close();
				//	loResponseStream.Close();
	#endregion
				*/
	#endregion

	namespace Misc
	{

		#region Utility
		/// <summary>
		/// Util provides a bunch of commonly used functions, most of them accessible as static methods.
		/// </summary>
		public class Utility
		{
			#region Encrypt
			public static string Encrypt(string content, DateTime expiration)
			{
				FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
					1, HttpContext.Current.Request.UserHostAddress, DateTime.Now,
					expiration, false, content);
				return FormsAuthentication.Encrypt(ticket);
			}
			#endregion

			#region Decrypt
			public static string Decrypt(string encryptedContent)
			{
				try
				{
					FormsAuthenticationTicket ticket =
						FormsAuthentication.Decrypt(encryptedContent);
					if (!ticket.Expired) return ticket.UserData;
				}
				catch (ArgumentException) { }
				return null;
			}
			#endregion

			#region GetDayNumberSuffix
			public static string GetDayNumberSuffix(int Number)
			{
				//Adapted from VB.NET version posted at http://www.developerfusion.com/show/3915/ 
				//The Ordinal Number Function 
				//Karl Moore - http://www.karlmoore.com 

				string suffix = String.Empty;
				// Accepts an integer, returns the ordinal suffix 
				// Handles special case three digit numbers ending 
				// with 11, 12 or 13 - ie, 111th, 112th, 113th, 211th, et al 
				if (Number.ToString().Length > 2)
				{
					int intEndNum = Convert.ToInt32(Number.ToString().Substring(Number.ToString().Length - 2, 2));
					if (intEndNum >= 11 && intEndNum <= 13)
						switch (intEndNum)
						{
							case 11:
							case 12:
							case 13:
								suffix = "th";
								break;
						}
				}

				if (Number >= 21)
				{
					//Handles 21st, 22nd, 23rd, et al 
					int Number21 = Convert.ToInt32(Number.ToString().Substring(Number.ToString().Length - 1, 1));
					switch (Number21)
					{
						case 1:
							suffix = "st";
							break;
						case 2:
							suffix = "nd";
							break;
						case 3:
							suffix = "rd";
							break;
						case 0:
							suffix = "th";
							break;
						default:
							for (int i = 4; i <= 9; i++)
							{
								if (Number21 == i)
								{
									suffix = "th";
									break;
								}
								else
									suffix = String.Empty;
							}
							break;
					}
				}
				else
				{
					switch (Number)
					{
						case 1:
							suffix = "st";
							break;
						case 2:
							suffix = "nd";
							break;
						case 3:
							suffix = "rd";
							break;
						default:
							for (int i = 4; i <= 21; i++)
							{
								if (Number == i)
								{
									suffix = "th";
									break;
								}
								else
									suffix = String.Empty;
							}
							break;
					}
				}

				return suffix;
			}
			#endregion

			#region Hash
			public static Guid Hash(string inString)
			{
				return Hash(inString, Guid.Empty);
			}
			public static Guid Hash(string inString, Guid saltGuid)
			{
				Guid systemSaltGuid = new Guid("bd33c53b-cf57-4e17-addb-9f0b9fa9ce63");
				inString += Convert.ToBase64String(systemSaltGuid.ToByteArray());

				if (!saltGuid.Equals(Guid.Empty))
					inString += Convert.ToBase64String(saltGuid.ToByteArray());

				byte[] bytesToHash = UnicodeEncoding.UTF8.GetBytes(inString);

				MD5CryptoServiceProvider myMD5 = new MD5CryptoServiceProvider();
				byte[] hashBytes = myMD5.ComputeHash(bytesToHash);
				return new Guid(hashBytes);

			}
			#endregion

			#region Snip(string txt, int length)
			public static string Snip(string txt, int length)
			{
				if (txt.Length > length && length > 0)
					return txt.Substring(0, length - 3) + "...";
				else
					return txt;
			}
			#endregion

			#region JsStringEncode
			/// <summary>
			/// Use following js to decode:
			/// function UrlDecode(psEncodeString) 
			/// {
			///		var lsRegExp = /\+/g;
			///		return unescape(String(psEncodeString).replace(lsRegExp," "));
			/// }
			/// </summary>
			public static string JsStringEncode(string s)
			{
				return HttpUtility.UrlEncodeUnicode(s).Replace("'", "\\'");
			}
			#endregion
			#region XmlAttributeEncode
			public static string XmlAttributeEncode(string s)
			{
				return s.Replace("&", "&amp;").Replace("<", "&lt;").Replace("\"", "&quot;").Replace("\011", "&#x9;").Replace("\012", "&#xA;").Replace("\015", "&#xD;");
			}
			#endregion
			#region XmlTextEncode
			public static string XmlTextEncode(string s)
			{
				return s.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\015", "&#xD;");
			}
			#endregion

			#region AddXmlAttribute
			public static void AddXmlAttribute(XmlNode node, XmlDocument xmlDoc, string key, string val)
			{
				XmlAttribute att = xmlDoc.CreateAttribute(key);
				att.Value = val;
				node.Attributes.Append(att);
			}
			#endregion
			#region ReturnXmlAttribute
			static XmlAttribute ReturnXmlAttribute(XmlNode node, XmlDocument xmlDoc, string key, string val)
			{
				XmlAttribute att = xmlDoc.CreateAttribute(key);
				att.Value = val;
				node.Attributes.Append(att);
				return att;
			}
			#endregion

			#region SecondsSince2003
			public static long SecondsSince2003(DateTime d)
			{
				double secs = ((TimeSpan)(d - (new DateTime(2003, 1, 1)))).TotalSeconds;
				return (long)secs;
			}
			#endregion
			#region SecondsSince2003Reverse
			public static DateTime SecondsSince2003Reverse(long secs)
			{
				return (new DateTime(2003, 1, 1)).AddSeconds(secs);
			}
			#endregion
			#region GenRandomText
			public static string GenRandomText(int length)
			{
				System.Random rnd = new System.Random();
				return GenRandomText(length, rnd);
			}
			public static string GenRandomText(int length, System.Random randomOb)
			{
				string tmpStr = "";
				for (int i = 0; i < length; i++)
				{
					int randomNumber = randomOb.Next(62);
					randomNumber = randomNumber + 48;
					if (randomNumber > 57)
						randomNumber = randomNumber + 7;
					if (randomNumber > 90)
						randomNumber = randomNumber + 6;
					tmpStr += ((char)randomNumber).ToString();
				}
				return tmpStr;
			}
			public static string GenRandomChars(int length)
			{
				System.Random rnd = new System.Random();
				return GenRandomChars(length, rnd);
			}
			public static string GenRandomChars(int length, System.Random randomOb)
			{
				string tmpStr = "";
				for (int i = 0; i < length; i++)
				{
					int randomNumber = randomOb.Next(52);
					randomNumber = randomNumber + 65;
				//	if (randomNumber > 57)
				//		randomNumber = randomNumber + 7;
					if (randomNumber > 90)
						randomNumber = randomNumber + 6;
					tmpStr += ((char)randomNumber).ToString();
				}
				return tmpStr;
			}
			#endregion
			#region isInitialized
			/// <summary>
			///  Used by isInitialized -- do not instantiate.
			/// </summary>
			protected static DateTime dtNull;
			/// <summary>
			/// Used by isInitialized -- do not instantiate
			/// </summary>
			protected static int iNull;
			/// <summary>
			/// isInitialized returns true if the variable has been set.
			/// </summary>
			/// <param name="iTest"></param>
			/// <returns>True if the value of the variable has been set, false otherwise.</returns>
			public static bool isInitialized(int iTest)
			{
				if (Int32.Equals(iTest, iNull))
					return false;
				return true;
			}
			/// <summary>
			/// isInitialized checks to see if a given DateTime value has been initialized or not.
			/// </summary>
			/// <param name="dtTest"></param>
			/// <returns>True if the value of the variable has been set, false otherwise.</returns>
			public static bool isInitialized(DateTime dtTest)
			{
				// Check for un-initialized date
				if (DateTime.Equals(dtTest, dtNull))
					return false;
				return true;
			}
			#endregion
			#region ColourFromHex
			public static System.Drawing.Color ColourFromHex(string hexStr)
			{
				int redInt = IntFromHex(hexStr.Substring(0, 2));
				int greenInt = IntFromHex(hexStr.Substring(2, 2));
				int blueInt = IntFromHex(hexStr.Substring(4, 2));
				return System.Drawing.Color.FromArgb(redInt, greenInt, blueInt);
			}
			public static int IntFromHex(string hexStr)
			{
				int total = 0;
				for (int i = hexStr.Length - 1; i >= 0; i--)
				{
					int digitnumber = hexStr.Length - i;
					int power = ((digitnumber - 1) * 4);
					int multiplier = (int)Math.Pow(2, (digitnumber - 1) * 4);
					switch (hexStr.Substring(i, 1).ToLower())
					{
						case "0":
							break;
						case "a":
							total += multiplier * 10;
							break;
						case "b":
							total += multiplier * 11;
							break;
						case "c":
							total += multiplier * 12;
							break;
						case "d":
							total += multiplier * 13;
							break;
						case "e":
							total += multiplier * 14;
							break;
						case "f":
							total += multiplier * 15;
							break;
						default:
							total += multiplier * Int32.Parse(hexStr.Substring(i, 1));
							break;
					}
				}
				return total;
			}
			#endregion
			#region ColourToHex
			public static string ColourToHex(Color color)
			{
				int argb = color.ToArgb();
				argb = argb & 0x00FFFFFF;
				return argb.ToString("X6");
			}
			#endregion

			#region RandomName
			/// <summary>
			/// Returns a random name. Uses normal distribution to return common names more frequently. Requires 
			/// the db_names database to be accessible using the Vars.GenericDatabaseConnection function.
			/// </summary>
			/// <param name="rnd">A Random object.</param>
			/// <param name="type">Use "Male" for a male christian name, "Female" for a female christian name, and "Last" for a last name.</param>
			/// <returns>The name</returns>
			public static string RandomName(Random rnd, string type)
			{
				int maxRank = (int)(Db.Dv("select max(" + type + "Rank) from " + type, Vars.GenericDatabaseConnection("db_names"))[0][0]);
				string name = "";
				int i = 0;
				do
				{
					i++;
					int j = rnd.Next(100000);
					double area = ((double)j) / 200000.0;
					double normalRandUpTo4 = ApproxInverseNorm(area);
					double normalRand = normalRandUpTo4 * (maxRank / 4.41);
					int rand = int.Parse(Math.Floor(normalRand).ToString());
					if (rand > maxRank)
						rand = maxRank;
					if (rand < 1)
						rand = 1;
					//Page.Trace.Warn(rand.ToString());
					//DataView dv = Db.Dv("select TOP 1 "+type+"Name from "+type+" Order By ABS("+rand+"-"+type+"Rank)",namesConn);
					DataView dv = Db.Dv("select " + type + "Name from " + type + " where " + type + "Rank = " + rand, Vars.GenericDatabaseConnection("db_names"));
					try
					{
						name = dv[0][0].ToString();
					}
					catch { }
					dv.Dispose();
					if (i == 100)
						throw new Exception("Error finding name.");
				}
				while (name == "");
				return name;
			}
			/// <summary>
			/// Beasley Springer approx. to inverse norm, Applied Stats. 26, 118-121.
			/// See: J.H.Maindonald "Statistical Computation" p.295.
			/// Returns z given a half-middle tail type p.
			/// </summary>
			/// <param name="p">half-middle tail type</param>
			/// <returns>z</returns>
			public static double ApproxInverseNorm(double p)
			{
				double a0 = 2.5066282, a1 = -18.6150006, a2 = 41.3911977, a3 = -25.4410605,
					b1 = -8.4735109, b2 = 23.0833674, b3 = -21.0622410, b4 = 3.1308291,
					c0 = -2.7871893, c1 = -2.2979648, c2 = 4.8501413, c3 = 2.3212128,
					d1 = 3.5438892, d2 = 1.6370678, r, z;

				if (p > 0.42)
				{
					r = Math.Sqrt(-Math.Log(0.5 - p));
					z = (((c3 * r + c2) * r + c1) * r + c0) / ((d2 * r + d1) * r + 1);
				}
				else
				{
					r = p * p;
					z = p * (((a3 * r + a2) * r + a1) * r + a0) / ((((b4 * r + b3) * r + b2) * r + b1) * r + 1);
				}
				return z;
			}
			#endregion

			#region ExponentiallyDistributedDouble
			public static double ExponentiallyDistributedDouble(double mean)
			{
				Random r = new Random();
				return ExponentiallyDistributedDouble(mean, r);
			}
			public static double ExponentiallyDistributedDouble(double mean, Random r)
			{
				double Random_Number = 0;
				while (Random_Number == 0)
				{
					Random_Number = r.NextDouble();
				}
				double Exp_Random_Number = new Double();
				Exp_Random_Number = -1 * Math.Log(1 - Random_Number) * mean;
				return Exp_Random_Number;
			}
			#endregion

			#region SerialiseHashtable, DeSerialiseHashtable
			public static string SerialiseHashtable(Hashtable h)
			{
				if (h.Count == 0)
					return "";
				XmlDocument d = new XmlDocument();
				d.AppendChild(d.CreateElement("prefs"));
				XmlNode prefs = d.GetElementsByTagName("prefs")[0];
				foreach (object o in h.Keys)
				{
					prefs.AppendChild(CreateXmlNode(d, "pref", System.Web.HttpUtility.UrlEncode(o.ToString()), System.Web.HttpUtility.UrlEncode(h[o].ToString())));
				}
				return d.InnerXml;
			}
			public static Hashtable DeSerialiseHashtable(string s)
			{
				Hashtable h = new Hashtable();
				if (s.Length == 0)
					return h;
				XmlDocument d = new XmlDocument();
				d.LoadXml(s);
				foreach (XmlNode n in d.GetElementsByTagName("pref"))
				{
					h[System.Web.HttpUtility.UrlDecode(n.Attributes["k"].Value)] = System.Web.HttpUtility.UrlDecode(n.InnerText);
				}
				return h;
			}
			public static bool HashtableStringComparison(Hashtable h1, Hashtable h2)
			{
				try
				{
					if (h1.Count != h2.Count)
						return false;
					foreach (object o in h1.Keys)
					{
						string k = o.ToString();
						if (!h1[k].ToString().Equals(h2[k].ToString()))
							return false;
					}
					return true;
				}
				catch
				{
					return false;
				}
			}




			




			public static XmlElement CreateXmlNode(XmlDocument xmlDoc, string tagName, string valueInt, string data)
			{
				XmlElement xmlEle = xmlDoc.CreateElement(tagName);
				XmlAttribute iAttribute = xmlDoc.CreateAttribute("k");
				iAttribute.Value = valueInt.ToString();
				xmlEle.Attributes.Append(iAttribute);
				xmlEle.InnerText = data;
				return xmlEle;
			}
			#endregion
			#region FriendlyDate
			public static string FriendlyDate(DateTime? nullableDate, bool Capital, bool BracketedDate)
			{
				if (nullableDate == null) return "null";
				var d = nullableDate.Value;
				DateTime now = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
				DateTime dateD = new DateTime(d.Year, d.Month, d.Day);

				TimeSpan ts = now - dateD;
				if (ts.Days > 7 || ts.Days < -7)
				{
					if (d.Year != DateTime.Today.Year)
						return d.ToString("ddd dd MMM yyyy");
					else
						return d.ToString("ddd dd MMM");
				}
				else if (ts.Days == 0)
					return (Capital ? "T" : "t") + "oday" + (BracketedDate ? " (" + d.ToString("ddd") + ")" : "");
				else if (ts.Days == 1)
					return (Capital ? "Y" : "y") + "esterday" + (BracketedDate ? " (" + d.ToString("ddd") + ")" : "");
				else if (ts.Days > 1)
					return (Capital ? "L" : "l") + "ast " + d.ToString("ddd") + (BracketedDate ? " (" + d.ToString("dd MMM") + ")" : "");
				else if (ts.Days == -1)
					return (Capital ? "T" : "t") + "omorrow" + (BracketedDate ? " (" + d.ToString("ddd") + ")" : "");
				else if (ts.Days < -1)
				{
					if (FriendlyDayOfWeek(d.DayOfWeek) > FriendlyDayOfWeek(DateTime.Now.DayOfWeek))
						return (Capital ? "T" : "t") + "his " + d.ToString("ddd") + (BracketedDate ? " (" + d.ToString("dd MMM") + ")" : "");
					else
						return (Capital ? "N" : "n") + "ext " + d.ToString("ddd") + (BracketedDate ? " (" + d.ToString("dd MMM") + ")" : "");
				}
				else
					return "Err";
			}
			public static string FriendlyDate(DateTime? d, bool Capital)
			{
				return FriendlyDate(d, Capital, true);
			}
			public static string FriendlyDate(DateTime? d)
			{
				return FriendlyDate(d, true, true);
			}
			#endregion
			#region FriendlyDayOfWeek
			private static int FriendlyDayOfWeek(System.DayOfWeek d)
			{
				switch (d)
				{
					case DayOfWeek.Monday:
						return 0;
					case DayOfWeek.Tuesday:
						return 1;
					case DayOfWeek.Wednesday:
						return 2;
					case DayOfWeek.Thursday:
						return 3;
					case DayOfWeek.Friday:
						return 4;
					case DayOfWeek.Saturday:
						return 5;
					case DayOfWeek.Sunday:
						return 6;
					default:
						return 0;
				}

			}
			#endregion
			#region FriendlyTime


 
			public static string FriendlyTime(DateTime? d, bool Capital)
			{
				return FriendlyTime(d, Capital, false);
			}
 


			public static string FriendlyTime(DateTime? d, bool Capital, bool Exact)
			{
				if (d == null) return "null";
				TimeSpan ts = DateTime.Now - d.Value;
				if (ts.Days > 0)
					return FriendlyDate(d.Value, Capital, false) + (Exact ? " at " + d.Value.ToShortTimeString() : "");
				else
				{
					if ((int)ts.TotalMinutes == 0)
						return ((int)ts.TotalSeconds).ToString() + " sec" + (ts.Seconds == 1 ? "" : "s") + " ago";
					else if ((int)ts.TotalHours == 0)
						return ((int)ts.TotalMinutes).ToString() + " min" + (ts.Minutes == 1 ? "" : "s") + " ago";
					else
						return ((int)ts.TotalHours).ToString() + " hour" + (ts.Hours == 1 ? "" : "s") + " ago";
				}

			}
			public static string FriendlyTime(DateTime? d)
			{
				return FriendlyTime(d, true);
			}
 
			#endregion
			#region FriendlyTimeSpan
			public static string FriendlyTimeSpan(TimeSpan ts)
			{
				if (ts.TotalDays >= 365.0)
				{
					int years = (int)Math.Floor(ts.TotalDays / 365.0);
					return years.ToString("#,##0") + " year" + (years == 1 ? "" : "s");
				}
				else if (ts.TotalDays >= 30.0)
				{
					int months = (int)Math.Floor(ts.TotalDays / 30.0);
					return months.ToString() + " month" + (months == 1 ? "" : "s");
				}
				else if (ts.TotalDays >= 1.0)
				{
					int days = (int)Math.Floor(ts.TotalDays);
					return days.ToString() + " day" + (days == 1 ? "" : "s");
				}
				else if (ts.TotalHours >= 1.0)
				{
					int hours = (int)Math.Floor(ts.TotalHours);
					return hours.ToString() + " hour" + (hours == 1 ? "" : "s");
				}
				else if (ts.TotalMinutes >= 1.0)
				{
					int mins = (int)Math.Floor(ts.TotalMinutes);
					return mins.ToString() + " minute" + (mins == 1 ? "" : "s");
				}
				else
				{
					int sec = (int)Math.Floor(ts.TotalSeconds);
					return sec.ToString() + " second" + (sec == 1 ? "" : "s");
				}
			}
			#endregion
			#region JoinStringArrays
			public static string[] JoinStringArrays(string[] a1, string[] a2)
			{
				string[] combArray = new string[a1.Length + a2.Length];
				a1.CopyTo(combArray, 0);
				a2.CopyTo(combArray, a1.Length);
				return combArray;

				//				ArrayList a = new ArrayList();
				//
				//				foreach (string s1 in a1)
				//					a.Add(s1);
				//
				//				foreach (string s2 in a2)
				//					a.Add(s2);
				//
				//				return (string[])a.ToArray(typeof(string));
			}
			#endregion
			#region JoinArrays
			public static object[] JoinArrays(object[] a1, object[] a2)
			{
				object[] combArray = new object[a1.Length + a2.Length];
				a1.CopyTo(combArray, 0);
				a2.CopyTo(combArray, a1.Length);
				return combArray;
			}
			#endregion
			#region Join
			public static object[] Join(object[] a1, params object[] a2)
			{
				object[] combArray = new object[a1.Length + a2.Length];
				a1.CopyTo(combArray, 0);
				a2.CopyTo(combArray, a1.Length);
				return combArray;
			}
			#endregion

			#region JSON
			/// <summary>
			/// This class encodes and decodes JSON strings.
			/// Spec. details, see http://www.json.org/
			/// 
			/// JSON uses Arrays and Objects. These correspond here to the datatypes ArrayList and Hashtable.
			/// All numbers are parsed to doubles.
			/// </summary>
			public class JSON
			{
				public const int TOKEN_NONE = 0;
				public const int TOKEN_CURLY_OPEN = 1;
				public const int TOKEN_CURLY_CLOSE = 2;
				public const int TOKEN_SQUARED_OPEN = 3;
				public const int TOKEN_SQUARED_CLOSE = 4;
				public const int TOKEN_COLON = 5;
				public const int TOKEN_COMMA = 6;
				public const int TOKEN_STRING = 7;
				public const int TOKEN_NUMBER = 8;
				public const int TOKEN_TRUE = 9;
				public const int TOKEN_FALSE = 10;
				public const int TOKEN_NULL = 11;

				private const int BUILDER_CAPACITY = 2000;

				protected static JSON instance = new JSON();

				/// <summary>
				/// On decoding, this value holds the position at which the parse failed (-1 = no error).
				/// </summary>
				protected int lastErrorIndex = -1;
				protected string lastDecode = "";

				/// <summary>
				/// Parses the string json into a value
				/// </summary>
				/// <param name="json">A JSON string.</param>
				/// <returns>An ArrayList, a Hashtable, a double, a string, null, true, or false</returns>
				public static object JsonDecode(string json)
				{
					// save the string for debug information
					JSON.instance.lastDecode = json;

					if (json != null)
					{
						char[] charArray = json.ToCharArray();
						int index = 0;
						bool success = true;
						object value = JSON.instance.ParseValue(charArray, ref index, ref success);
						if (success)
						{
							JSON.instance.lastErrorIndex = -1;
						}
						else
						{
							JSON.instance.lastErrorIndex = index;
						}
						return value;
					}
					else
					{
						return null;
					}
				}

				/// <summary>
				/// Converts a Hashtable / ArrayList object into a JSON string
				/// </summary>
				/// <param name="json">A Hashtable / ArrayList</param>
				/// <returns>A JSON encoded string, or null if object 'json' is not serializable</returns>
				public static string JsonEncode(object json)
				{
					StringBuilder builder = new StringBuilder(BUILDER_CAPACITY);
					bool success = JSON.instance.SerializeValue(json, builder);
					return (success ? builder.ToString() : null);
				}

				/// <summary>
				/// On decoding, this function returns the position at which the parse failed (-1 = no error).
				/// </summary>
				/// <returns></returns>
				public static bool LastDecodeSuccessful()
				{
					return (JSON.instance.lastErrorIndex == -1);
				}

				/// <summary>
				/// On decoding, this function returns the position at which the parse failed (-1 = no error).
				/// </summary>
				/// <returns></returns>
				public static int GetLastErrorIndex()
				{
					return JSON.instance.lastErrorIndex;
				}

				/// <summary>
				/// If a decoding error occurred, this function returns a piece of the JSON string 
				/// at which the error took place. To ease debugging.
				/// </summary>
				/// <returns></returns>
				public static string GetLastErrorSnippet()
				{
					if (JSON.instance.lastErrorIndex == -1)
					{
						return "";
					}
					else
					{
						int startIndex = JSON.instance.lastErrorIndex - 5;
						int endIndex = JSON.instance.lastErrorIndex + 15;
						if (startIndex < 0)
						{
							startIndex = 0;
						}
						if (endIndex >= JSON.instance.lastDecode.Length)
						{
							endIndex = JSON.instance.lastDecode.Length - 1;
						}

						return JSON.instance.lastDecode.Substring(startIndex, endIndex - startIndex + 1);
					}
				}

				protected Hashtable ParseObject(char[] json, ref int index)
				{
					Hashtable table = new Hashtable();
					int token;

					// {
					NextToken(json, ref index);

					bool done = false;
					while (!done)
					{
						token = LookAhead(json, index);
						if (token == JSON.TOKEN_NONE)
						{
							return null;
						}
						else if (token == JSON.TOKEN_COMMA)
						{
							NextToken(json, ref index);
						}
						else if (token == JSON.TOKEN_CURLY_CLOSE)
						{
							NextToken(json, ref index);
							return table;
						}
						else
						{

							// name
							string name = ParseString(json, ref index);
							if (name == null)
							{
								return null;
							}

							// :
							token = NextToken(json, ref index);
							if (token != JSON.TOKEN_COLON)
							{
								return null;
							}

							// value
							bool success = true;
							object value = ParseValue(json, ref index, ref success);
							if (!success)
							{
								return null;
							}

							table[name] = value;
						}
					}

					return table;
				}

				protected ArrayList ParseArray(char[] json, ref int index)
				{
					ArrayList array = new ArrayList();

					// [
					NextToken(json, ref index);

					bool done = false;
					while (!done)
					{
						int token = LookAhead(json, index);
						if (token == JSON.TOKEN_NONE)
						{
							return null;
						}
						else if (token == JSON.TOKEN_COMMA)
						{
							NextToken(json, ref index);
						}
						else if (token == JSON.TOKEN_SQUARED_CLOSE)
						{
							NextToken(json, ref index);
							break;
						}
						else
						{
							bool success = true;
							object value = ParseValue(json, ref index, ref success);
							if (!success)
							{
								return null;
							}

							array.Add(value);
						}
					}

					return array;
				}

				protected object ParseValue(char[] json, ref int index, ref bool success)
				{
					switch (LookAhead(json, index))
					{
						case JSON.TOKEN_STRING:
							return ParseString(json, ref index);
						case JSON.TOKEN_NUMBER:
							return ParseNumber(json, ref index);
						case JSON.TOKEN_CURLY_OPEN:
							return ParseObject(json, ref index);
						case JSON.TOKEN_SQUARED_OPEN:
							return ParseArray(json, ref index);
						case JSON.TOKEN_TRUE:
							NextToken(json, ref index);
							return Boolean.Parse("TRUE");
						case JSON.TOKEN_FALSE:
							NextToken(json, ref index);
							return Boolean.Parse("FALSE");
						case JSON.TOKEN_NULL:
							NextToken(json, ref index);
							return null;
						case JSON.TOKEN_NONE:
							break;
					}

					success = false;
					return null;
				}

				protected string ParseString(char[] json, ref int index)
				{
					string s = "";
					char c;

					EatWhitespace(json, ref index);

					// "
					c = json[index++];

					bool complete = false;
					while (!complete)
					{

						if (index == json.Length)
						{
							break;
						}

						c = json[index++];
						if (c == '"')
						{
							complete = true;
							break;
						}
						else if (c == '\\')
						{

							if (index == json.Length)
							{
								break;
							}
							c = json[index++];
							if (c == '"')
							{
								s += '"';
							}
							else if (c == '\\')
							{
								s += '\\';
							}
							else if (c == '/')
							{
								s += '/';
							}
							else if (c == 'b')
							{
								s += '\b';
							}
							else if (c == 'f')
							{
								s += '\f';
							}
							else if (c == 'n')
							{
								s += '\n';
							}
							else if (c == 'r')
							{
								s += '\r';
							}
							else if (c == 't')
							{
								s += '\t';
							}
							else if (c == 'u')
							{
								int remainingLength = json.Length - index;
								if (remainingLength >= 4)
								{
									// fetch the next 4 chars
									char[] unicodeCharArray = new char[4];
									Array.Copy(json, index, unicodeCharArray, 0, 4);
									// parse the 32 bit hex into an integer codepoint
									uint codePoint = UInt32.Parse(new string(unicodeCharArray), NumberStyles.HexNumber);
									// convert the integer codepoint to a unicode char and add to string
									s += Char.ConvertFromUtf32((int)codePoint);
									// skip 4 chars
									index += 4;
								}
								else
								{
									break;
								}
							}

						}
						else
						{
							s += c;
						}

					}

					if (!complete)
					{
						return null;
					}

					return s;
				}

				protected double ParseNumber(char[] json, ref int index)
				{
					EatWhitespace(json, ref index);

					int lastIndex = GetLastIndexOfNumber(json, index);
					int charLength = (lastIndex - index) + 1;
					char[] numberCharArray = new char[charLength];

					Array.Copy(json, index, numberCharArray, 0, charLength);
					index = lastIndex + 1;
					return Double.Parse(new string(numberCharArray), CultureInfo.InvariantCulture);
				}

				protected int GetLastIndexOfNumber(char[] json, int index)
				{
					int lastIndex;
					for (lastIndex = index; lastIndex < json.Length; lastIndex++)
					{
						if ("0123456789+-.eE".IndexOf(json[lastIndex]) == -1)
						{
							break;
						}
					}
					return lastIndex - 1;
				}

				protected void EatWhitespace(char[] json, ref int index)
				{
					for (; index < json.Length; index++)
					{
						if (" \t\n\r".IndexOf(json[index]) == -1)
						{
							break;
						}
					}
				}

				protected int LookAhead(char[] json, int index)
				{
					int saveIndex = index;
					return NextToken(json, ref saveIndex);
				}

				protected int NextToken(char[] json, ref int index)
				{
					EatWhitespace(json, ref index);

					if (index == json.Length)
					{
						return JSON.TOKEN_NONE;
					}

					char c = json[index];
					index++;
					switch (c)
					{
						case '{':
							return JSON.TOKEN_CURLY_OPEN;
						case '}':
							return JSON.TOKEN_CURLY_CLOSE;
						case '[':
							return JSON.TOKEN_SQUARED_OPEN;
						case ']':
							return JSON.TOKEN_SQUARED_CLOSE;
						case ',':
							return JSON.TOKEN_COMMA;
						case '"':
							return JSON.TOKEN_STRING;
						case '0':
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case '7':
						case '8':
						case '9':
						case '-':
							return JSON.TOKEN_NUMBER;
						case ':':
							return JSON.TOKEN_COLON;
					}
					index--;

					int remainingLength = json.Length - index;

					// false
					if (remainingLength >= 5)
					{
						if (json[index] == 'f' &&
							json[index + 1] == 'a' &&
							json[index + 2] == 'l' &&
							json[index + 3] == 's' &&
							json[index + 4] == 'e')
						{
							index += 5;
							return JSON.TOKEN_FALSE;
						}
					}

					// true
					if (remainingLength >= 4)
					{
						if (json[index] == 't' &&
							json[index + 1] == 'r' &&
							json[index + 2] == 'u' &&
							json[index + 3] == 'e')
						{
							index += 4;
							return JSON.TOKEN_TRUE;
						}
					}

					// null
					if (remainingLength >= 4)
					{
						if (json[index] == 'n' &&
							json[index + 1] == 'u' &&
							json[index + 2] == 'l' &&
							json[index + 3] == 'l')
						{
							index += 4;
							return JSON.TOKEN_NULL;
						}
					}

					return JSON.TOKEN_NONE;
				}

				protected bool SerializeObjectOrArray(object objectOrArray, StringBuilder builder)
				{
					if (objectOrArray is Hashtable)
					{
						return SerializeObject((Hashtable)objectOrArray, builder);
					}
					else if (objectOrArray is ArrayList)
					{
						return SerializeArray((ArrayList)objectOrArray, builder);
					}
					else
					{
						return false;
					}
				}

				protected bool SerializeObject(Hashtable anObject, StringBuilder builder)
				{
					builder.Append("{");

					IDictionaryEnumerator e = anObject.GetEnumerator();
					bool first = true;
					while (e.MoveNext())
					{
						string key = e.Key.ToString();
						object value = e.Value;

						if (!first)
						{
							builder.Append(", ");
						}

						SerializeString(key, builder);
						builder.Append(":");
						if (!SerializeValue(value, builder))
						{
							return false;
						}

						first = false;
					}

					builder.Append("}");
					return true;
				}

				protected bool SerializeArray(ArrayList anArray, StringBuilder builder)
				{
					builder.Append("[");

					bool first = true;
					for (int i = 0; i < anArray.Count; i++)
					{
						object value = anArray[i];

						if (!first)
						{
							builder.Append(", ");
						}

						if (!SerializeValue(value, builder))
						{
							return false;
						}

						first = false;
					}

					builder.Append("]");
					return true;
				}

				protected bool SerializeValue(object value, StringBuilder builder)
				{
					if (value is string)
					{
						SerializeString((string)value, builder);
					}
					else if (value is Hashtable)
					{
						SerializeObject((Hashtable)value, builder);
					}
					else if (value is ArrayList)
					{
						SerializeArray((ArrayList)value, builder);
					}
					else if (IsNumeric(value))
					{
						SerializeNumber(Convert.ToDouble(value), builder);
					}
					else if ((value is Boolean) && ((Boolean)value == true))
					{
						builder.Append("true");
					}
					else if ((value is Boolean) && ((Boolean)value == false))
					{
						builder.Append("false");
					}
					else if (value == null)
					{
						builder.Append("null");
					}
					else
					{
						return false;
					}
					return true;
				}

				protected void SerializeString(string aString, StringBuilder builder)
				{
					SerializeStringStatic(aString, builder);
				}

				public static void SerializeStringStatic(string aString, StringBuilder builder)
				{
					builder.Append("\"");

					char[] charArray = aString.ToCharArray();
					for (int i = 0; i < charArray.Length; i++)
					{
						char c = charArray[i];
						if (c == '"')
						{
							builder.Append("\\\"");
						}
						else if (c == '\\')
						{
							builder.Append("\\\\");
						}
						else if (c == '\b')
						{
							builder.Append("\\b");
						}
						else if (c == '\f')
						{
							builder.Append("\\f");
						}
						else if (c == '\n')
						{
							builder.Append("\\n");
						}
						else if (c == '\r')
						{
							builder.Append("\\r");
						}
						else if (c == '\t')
						{
							builder.Append("\\t");
						}
						else
						{
							int codepoint = Convert.ToInt32(c);
							if ((codepoint >= 32) && (codepoint <= 126))
							{
								builder.Append(c);
							}
							else
							{
								builder.Append("\\u" + Convert.ToString(codepoint, 16).PadLeft(4, '0'));
							}
						}
					}

					builder.Append("\"");
				}

				protected void SerializeNumber(double number, StringBuilder builder)
				{
					builder.Append(Convert.ToString(number, CultureInfo.InvariantCulture));
				}

				/// <summary>
				/// Determines if a given object is numeric in any way
				/// (can be integer, double, etc). C# has no pretty way to do this.
				/// </summary>
				protected bool IsNumeric(object o)
				{
					try
					{
						Double.Parse(o.ToString());
					}
					catch (Exception)
					{
						return false;
					}
					return true;
				}
			}
			#endregion

		}
		#endregion
		#region CommandExecution
		public class CommandExecution
		{
			public CommandExecution() { }

			public const int LOGON32_LOGON_INTERACTIVE = 2;
			public const int LOGON32_PROVIDER_DEFAULT = 0;

			WindowsImpersonationContext impersonationContext;

			[DllImport("advapi32.dll", CharSet = CharSet.Auto)]
			public static extern int LogonUser(String lpszUserName,
				String lpszDomain,
				String lpszPassword,
				int dwLogonType,
				int dwLogonProvider,
				ref IntPtr phToken);
			[DllImport("advapi32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto,
				 SetLastError = true)]
			public extern static int DuplicateToken(IntPtr hToken,
				int impersonationLevel,
				ref IntPtr hNewToken);



			public bool ImpersonateValidUser(String userName, String domain, String password)
			{
				WindowsIdentity tempWindowsIdentity;
				IntPtr token = IntPtr.Zero;
				IntPtr tokenDuplicate = IntPtr.Zero;

				if (LogonUser(userName, domain, password, LOGON32_LOGON_INTERACTIVE,
					LOGON32_PROVIDER_DEFAULT, ref token) != 0)
				{
					if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
					{
						tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
						impersonationContext = tempWindowsIdentity.Impersonate();
						if (impersonationContext != null)
							return true;
						else
							return false;
					}
					else
						return false;
				}
				else
					return false;
			}
			public void UndoImpersonation()
			{
				impersonationContext.Undo();
			}


			public string ExecuteCmdLineApp(string strCmd)
			{
				string output = "";
				string error = "";

				TempFileCollection tf = new TempFileCollection();
				Executor.ExecWaitWithCapture(strCmd, tf, ref output, ref error);

				StreamReader sr = File.OpenText(output);
				StringBuilder strBuilder = new StringBuilder();
				string strLine = null;

				while (null != (strLine = sr.ReadLine()))
				{
					if ("" != strLine)
					{
						strBuilder.Append(strLine);
						strBuilder.Append("\r\n");
					}
				}
				sr.Close();


				StreamReader srE = File.OpenText(error);
				//StringBuilder strBuilderE = new StringBuilder();
				string strLineE = null;

				while (null != (strLineE = srE.ReadLine()))
				{
					if ("" != strLineE)
					{
						strBuilder.Append(strLineE);
						strBuilder.Append("\r\n");
					}
				}
				srE.Close();

				File.Delete(output);
				File.Delete(error);

				return strBuilder.ToString();
			}
		}
		#endregion
		#region RegEx
		/// <summary>
		/// Regex class - this is a collection of commonly used regular expression strings
		/// </summary>
		public class RegEx
		{
			#region IsEmail
			public static bool IsEmail(string email)
			{
				Regex r = new Regex(RegEx.Email);
				return r.IsMatch(email);
			}
			#endregion

			/// <summary>
			/// Email - matches simple email addresses - [any normail char, except @]@[domainname or dot].[domainname or dot]
			/// </summary>
			public readonly static string Email = @"^[^\@\s]+\@[A-Za-z0-9\-]{1}[.A-Za-z0-9\-]+\.[.A-Za-z0-9\-]*[A-Za-z0-9]$";
			/// <summary>
			/// Matches a simple +ve number - [.][number]
			/// </summary>
			public readonly static string NumberPositive = @"^\d*\.?\d+$";
			/// <summary>
			/// Matches a simple number - [-]number[.][number]
			/// </summary>
			public readonly static string Number = @"^-?\d*\.?\d*$";
			/// <summary>
			/// Matches an +ve integer number
			/// </summary>
			public readonly static string IntPositive = @"^\d+$";
			/// <summary>
			/// Matches an integer number
			/// </summary>
			public readonly static string Int = @"^-?\d+$";
			/// <summary>
			/// Matches a full postcode - L[L]N[N][L][(space)]NLL (L=letter, N=number)
			/// </summary>
			public readonly static string Postcode = @"^ *[a-zA-z]{1,2}\d{1,2}[a-zA-Z]? ?\d[a-zA-z]{2} *$";
			/// <summary>
			/// Matches a full postcode or first part - L[L]N[N][L][(space)]NLL or L[L]N[N][L] (L=letter, N=number)
			/// </summary>
			public readonly static string PostcodeOrShortPostcode = @"^ *[a-zA-z]{1,2}\d{1,2}[a-zA-Z]? ?\d[a-zA-z]{2} *$|^ *[a-zA-z]{1,2}\d{1,2}[a-zA-Z]? *$";
			/// <summary>
			/// Matches only a short postcode (first part) - L[L]N[N][L] (L=letter, N=number)
			/// </summary>
			public readonly static string ShortPostcode = @"^ *[a-zA-z]{1,2}\d{1,2}[a-zA-Z]? *$";
			/// <summary>
			/// Telephone - checks for at least 6 digits, possible -, possible 'ext', etc. - NOT EXHAUSTIVE.
			/// </summary>
			public readonly static string Telephone = @"^ *\+?[\(\)\d-ext ]{6,} *$";
			/// <summary>
			/// Credit card number - checks for 16 digits, groups of 4 digits may be seperated by "-" or " ".
			/// </summary>
			public readonly static string CcNum = @"^\d{4}[ \-]?\d{4}[ \-]?\d{4}[ \-]?\d{4}$";
			/// <summary>
			/// Credit card expiry date - checks in the form D/DD , D/DDDD , DD/DD or DD/DDDD with D=[0-10] - doesn't check date makes sence
			/// </summary>
			public readonly static string CcExp = @"^\d{1,2}/(\d{2}|\d{4})$";
			/// <summary>
			/// Url
			/// </summary>
			public readonly static string Url = @"(^|[ \t\r\n])((ftp|http|https|gopher|mailto|news|nntp|telnet|wais|file|prospero|aim|webcal):(([A-Za-z0-9$_.+!*(),;/?:@&~=-])|%[A-Fa-f0-9]{2})+(#([a-zA-Z0-9][a-zA-Z0-9$_.+!*(),;/?:@&~=%-]*))?)";
			/// <summary>
			/// Matches url and start/end of string - use this in a validator
			/// </summary>
			public readonly static string UrlOnly = @"^((ftp|http|https|gopher|mailto|news|nntp|telnet|wais|file|prospero|aim|webcal):(([A-Za-z0-9$_.+!*(),;/?:@&~=-])|%[A-Fa-f0-9]{2})+(#([a-zA-Z0-9][a-zA-Z0-9$_.+!*(),;/?:@&~=%-]*))?)$";
		}
		#endregion
		#region Db
		/// <summary>
		/// Db - the Cambro Database Class
		/// </summary>
		public class Db
		{
			#region public class Table, public class Col - this is an old attempt at a generic database access schema... - outdated
			public class Table
			{
				private string dbTableName;
				public string DbTableName
				{
					get { return this.dbTableName; }
					set { this.dbTableName = value; }
				}
				private ArrayList cols;
				public ArrayList Cols
				{
					get { return this.cols; }
					set { this.cols = value; }
				}
				private Table()
				{
					cols = new ArrayList();
				}
				public Table(string dbTableName)
					: this()
				{
					this.dbTableName = dbTableName;
				}
				public string InsertSql(Cambro.Web.SiteFramework.TemplatePage pageRef)
				{
					string sql = "insert into t" + dbTableName + " (";
					bool doneOne = false;
					foreach (Db.Col current in cols)
					{
						//If the current row is not the Primary Key, or wants a default value on insert...
						if (!((current.ColProperties & Col.ColPropertiesEnum.PrimaryKey) > 0 || (current.ColProperties & Col.ColPropertiesEnum.DefaultValueOnInsert) > 0))
						{
							sql += (doneOne ? ", " : "") + current.TypeChar + dbTableName + current.DbName;
							doneOne = true;
						}
					}
					sql += ") values (";
					doneOne = false;
					foreach (Db.Col current in cols)
					{
						//If the current row is not the Primary Key, or wants a default value on insert...
						if (!((current.ColProperties & Col.ColPropertiesEnum.PrimaryKey) > 0 || (current.ColProperties & Col.ColPropertiesEnum.DefaultValueOnInsert) > 0))
						{
							sql += (doneOne ? ", " : "");
							switch (current.SqlDataType)
							{
								case Col.SqlDataTypeEnum.Int:
								case Col.SqlDataTypeEnum.Float:
								case Col.SqlDataTypeEnum.Numeric:
									sql += Db.PNum(((TextBox)pageRef.FindControl(fullColDbName(current))).Text);
									break;
								case Col.SqlDataTypeEnum.Varchar:
								case Col.SqlDataTypeEnum.teXt:
									sql += "'" + Db.PStr(((TextBox)pageRef.FindControl(fullColDbName(current))).Text) + "'";
									break;
								case Col.SqlDataTypeEnum.Datetime:
									if ((current.ColProperties & Col.ColPropertiesEnum.CurrentDateTimeOnInsert) > 0)
										sql += Db.Dt(System.DateTime.Now);
									else if ((current.ColProperties & Col.ColPropertiesEnum.CustomDateTimeOnInsert) > 0)
										sql += Db.Dt(((System.Web.UI.WebControls.Calendar)pageRef.FindControl(fullColDbName(current))).SelectedDate);
									else
										sql += Db.Dt(System.DateTime.Now);
									break;
								case Col.SqlDataTypeEnum.Bit:
									sql += ((CheckBox)pageRef.FindControl(fullColDbName(current))).Checked ? "1" : "0";
									break;
								default:
									sql += "";
									break;
							}
							doneOne = true;
						}
					}
					sql += ")";
					return sql;
				}
				public string UpdateSql(Cambro.Web.SiteFramework.TemplatePage pageRef)
				{
					string sql = "update t" + dbTableName + " set ";
					bool doneOne = false;
					foreach (Db.Col current in cols)
					{
						//If the current row is not the Primary Key, or wants a default value on insert...
						if (!((current.ColProperties & Col.ColPropertiesEnum.PrimaryKey) > 0 || (current.ColProperties & Col.ColPropertiesEnum.DefaultValueOnUpdate) > 0))
						{
							sql += (doneOne ? ", " : "") + current.TypeChar + dbTableName + current.DbName + "=";
							doneOne = true;
							switch (current.SqlDataType)
							{
								case Col.SqlDataTypeEnum.Int:
								case Col.SqlDataTypeEnum.Float:
								case Col.SqlDataTypeEnum.Numeric:
									sql += Db.PNum(((TextBox)pageRef.FindControl(fullColDbName(current))).Text);
									break;
								case Col.SqlDataTypeEnum.Varchar:
								case Col.SqlDataTypeEnum.teXt:
									sql += "'" + Db.PStr(((TextBox)pageRef.FindControl(fullColDbName(current))).Text) + "'";
									break;
								case Col.SqlDataTypeEnum.Datetime:
									if ((current.ColProperties & Col.ColPropertiesEnum.CurrentDateTimeOnUpdate) > 0)
										sql += Db.Dt(System.DateTime.Now);
									else if ((current.ColProperties & Col.ColPropertiesEnum.CustomDateTimeOnUpdate) > 0)
										sql += Db.Dt(((System.Web.UI.WebControls.Calendar)pageRef.FindControl(fullColDbName(current))).SelectedDate);
									else
										sql += Db.Dt(System.DateTime.Now);
									break;
								case Col.SqlDataTypeEnum.Bit:
									sql += ((CheckBox)pageRef.FindControl(fullColDbName(current))).Checked ? "1" : "0";
									break;
								default:
									sql += "";
									break;
							}
							doneOne = true;
						}
					}
					sql += " where ";
					foreach (Db.Col current in cols)
					{
						//If the current row is the Primary Key...
						if ((current.ColProperties & Col.ColPropertiesEnum.PrimaryKey) > 0)
						{
							sql += fullColDbName(current) + "=" + Db.PNum(pageRef.ViewStatePublic[fullColDbName(current)]);
						}
					}

					return sql;
				}
				private string fullColDbName(Col col)
				{
					return col.TypeChar + dbTableName + col.DbName;
				}
				public void PopulateForm(HtmlForm formRef, Page pageRef, int primaryKey)
				{
					SqlDataReader dr;
					string primaryKeyColName = "";
					foreach (Db.Col current in cols)
					{
						//If the current row is the Primary Key...
						if ((current.ColProperties & Col.ColPropertiesEnum.PrimaryKey) > 0)
						{
							primaryKeyColName = fullColDbName(current);
						}
					}
					dr = Db.Dr("select * from t" + dbTableName + " where " + primaryKeyColName + "=" + primaryKey);
					dr.Read();
					foreach (Db.Col current in cols)
					{
						if (!((current.ColProperties & Col.ColPropertiesEnum.PrimaryKey) > 0 || (current.ColProperties & Col.ColPropertiesEnum.DefaultValueOnUpdate) > 0) || (current.ColProperties & Col.ColPropertiesEnum.CurrentDateTimeOnUpdate) > 0)
						{
							if (dr[fullColDbName(current)] != System.DBNull.Value)
							{
								switch (current.SqlDataType)
								{
									case Col.SqlDataTypeEnum.Int:
									case Col.SqlDataTypeEnum.Float:
									case Col.SqlDataTypeEnum.Numeric:
									case Col.SqlDataTypeEnum.Varchar:
									case Col.SqlDataTypeEnum.teXt:
										((TextBox)pageRef.FindControl(fullColDbName(current))).Text = (string)dr[fullColDbName(current)];
										break;
									case Col.SqlDataTypeEnum.Datetime:
										if ((current.ColProperties & Col.ColPropertiesEnum.CustomDateTimeOnUpdate) > 0)
											((System.Web.UI.WebControls.Calendar)pageRef.FindControl(fullColDbName(current))).SelectedDate = (DateTime)dr[fullColDbName(current)];
										break;
									case Col.SqlDataTypeEnum.Bit:
										((CheckBox)pageRef.FindControl(fullColDbName(current))).Checked = (bool)dr[fullColDbName(current)];
										break;
									default:
										break;
								}
							}
						}
					}
				}
			}
			public class Col
			{
				private string dbName;
				public string DbName
				{
					get { return this.dbName; }
					set { this.dbName = value; }
				}
				private SqlDataTypeEnum sqlDataType;
				public SqlDataTypeEnum SqlDataType
				{
					get { return this.sqlDataType; }
					set { this.sqlDataType = value; }
				}
				private int length;
				public int Length
				{
					get { return this.length; }
					set { this.length = value; }
				}
				private string englishName;
				public string EnglishName
				{
					get { return this.englishName; }
					set { this.englishName = value; }
				}
				private ColPropertiesEnum colProperties;
				public ColPropertiesEnum ColProperties
				{
					get { return this.colProperties; }
					set { this.colProperties = value; }
				}
				public enum SqlDataTypeEnum
				{
					Int = 1,
					Varchar = 2,
					Float = 3,
					Datetime = 4,
					teXt = 5,
					Numeric = 6,
					Bit = 7
				}
				public string TypeChar
				{
					get
					{
						switch (sqlDataType)
						{
							case SqlDataTypeEnum.Int:
								return "i";
							case SqlDataTypeEnum.Varchar:
								return "v";
							case SqlDataTypeEnum.Float:
								return "f";
							case SqlDataTypeEnum.Datetime:
								return "d";
							case SqlDataTypeEnum.teXt:
								return "x";
							case SqlDataTypeEnum.Numeric:
								return "n";
							case SqlDataTypeEnum.Bit:
								return "b";
							default:
								return "v";
						}
					}
				}
				public enum ColPropertiesEnum
				{
					NullProperty = 0,
					PrimaryKey = 1,
					CurrentDateTimeOnInsert = 2,
					CurrentDateTimeOnUpdate = 4,
					CustomDateTimeOnInsert = 8,
					CustomDateTimeOnUpdate = 16,
					DefaultValueOnInsert = 32,
					DefaultValueOnUpdate = 64
				}
				public Col(string dbName, string englishName, int length, SqlDataTypeEnum sqlDataType, ColPropertiesEnum colProperties)
				{
					this.dbName = dbName;
					this.englishName = englishName;
					this.length = length;
					this.sqlDataType = sqlDataType;
					this.colProperties = colProperties;
				}
				public Col(string dbName, SqlDataTypeEnum sqlDataType, ColPropertiesEnum colProperties)
				{
					this.dbName = dbName;
					this.englishName = "";
					this.length = -1;
					this.sqlDataType = sqlDataType;
					this.colProperties = colProperties;
				}
				public Col(string dbName, SqlDataTypeEnum sqlDataType)
				{
					this.dbName = dbName;
					this.englishName = "";
					this.length = -1;
					this.sqlDataType = sqlDataType;
					this.colProperties = ColPropertiesEnum.NullProperty;
				}
			}
			#endregion

			#region public static string QJoinI(string tab1, string tab2) - shorthand for the following string: "tTab1 inner join tTab2 on iTab1_Tab2K = iTab2K"
			/// <summary>
			/// Query - Join (inner) - tTab1 inner join tTab2 on iTab1_Tab2K = iTab2K 
			/// </summary>
			/// <param name="tab1">Table 1</param>
			/// <param name="tab2">Table 2</param>
			/// <returns>an sql fragment reprasenting the joined table</returns>
			public static string QJoinI(string tab1, string tab2)
			{
				return " t" + tab1 + " inner join t" + tab2 + " on i" + tab1 + "_" + tab2 + "K = i" + tab2 + "K ";
			}
			#endregion

			#region Dv, Ds, Dr - returns various recordsets given a SQL string (uses Vars.DefaultConnectionString)
			/// <summary>
			/// Dv - execute an SQL command, bring back a dataview - for ?
			/// </summary>
			public static DataView Dv(string SQL)
			{
				return Dv(SQL, Vars.DefaultConnectionString);
			}
			/// <summary>
			/// Dv - execute an SQL command, bring back a dataview - for ?
			/// </summary>
			public static DataView Dv(string SQL, string ConnectionString)
			{
				Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
				DataSet dataset = new DataSet();
				DataView dv;
				SqlConnection conn = new SqlConnection(ConnectionString);
				try
				{
					SqlDataAdapter adapter = new SqlDataAdapter();
					adapter.SelectCommand = new SqlCommand(SQL, conn);
					adapter.Fill(dataset);
					dv = dataset.Tables[0].DefaultView;
				}
				finally
				{

					conn.Close();
					conn.Dispose();
				}
				return dv;
			}
			/// <summary>
			/// Ds - execute an SQL read (SELECT etc.) query, bring back a dataset - for databinding to controls etc.
			/// </summary>
			public static DataSet Ds(string SQL)
			{
				Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
				DataSet dataset = new DataSet();
				SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
				try
				{
					SqlDataAdapter adapter = new SqlDataAdapter();
					adapter.SelectCommand = new SqlCommand(SQL, conn);
					adapter.Fill(dataset);
				}
				finally
				{
					conn.Close();
					conn.Dispose();
				}
				return dataset;
			}
			/// <summary>
			/// Dr - execute an SQL read (SELECT etc.) query, bring back a reader for reading, or for databinding.
			/// </summary>
			/// <param name="SQL">SQL statement</param>
			public static SqlDataReader Dr(string SQL)
			{
				return Dr(SQL, Vars.DefaultConnectionString);
			}
			/// <summary>
			/// Dr - execute an SQL read (SELECT etc.) query, bring back a reader for reading, or for databinding.
			/// </summary>
			/// <param name="SQL">SQL statement</param>
			/// <param name="ConnectionString">ConnectionString to use</param>
			public static SqlDataReader Dr(string SQL, string ConnectionString)
			{
				Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
				SqlConnection conn = new SqlConnection(ConnectionString);
				SqlCommand myCommand = new SqlCommand(SQL, conn);
				myCommand.Connection.Open();
				SqlDataReader dr = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
				return dr;
			}
			#endregion

			#region Qu - execute an SQL non-read (UPDATE,INSERT etc.) query, bring back the number of records affected.
			/// <summary>
			/// Qu - execute an SQL non-read (UPDATE,INSERT etc.) query, bring back the number of records affected.
			/// </summary>
			public static int Qu(string SQL)
			{
				return Qu(SQL, Vars.DefaultConnectionString, 120);
			}
			/// <summary>
			/// Qu - execute an SQL non-read (UPDATE,INSERT etc.) query, bring back the number of records affected.
			/// </summary>
			public static int Qu(string SQL, int ConnectionTimeout)
			{
				return Qu(SQL, Vars.DefaultConnectionString, ConnectionTimeout);
			}
			/// <summary>
			/// Qu - execute an SQL non-read (UPDATE,INSERT etc.) query, bring back the number of records affected.
			/// </summary>
			public static int Qu(string SQL, string ConnectionString, int CommandTimeout)
			{
				if (SQL.ToUpper().StartsWith("SELECT"))
					Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
				else if (SQL.ToUpper().StartsWith("UPDATE"))
					Global.LogSqlQuery(Bobs.Global.QueryTypes.Update);
				else if (SQL.ToUpper().StartsWith("INSERT"))
					Global.LogSqlQuery(Bobs.Global.QueryTypes.Insert);
				else if (SQL.ToUpper().StartsWith("DELETE"))
					Global.LogSqlQuery(Bobs.Global.QueryTypes.Delete);

				int rows = 0;
				SqlConnection conn = new SqlConnection(ConnectionString);
				try
				{
					SqlCommand myCommand = new SqlCommand(SQL, conn);
					myCommand.CommandTimeout = CommandTimeout;
					myCommand.Connection.Open();
					rows = myCommand.ExecuteNonQuery();
				}
				finally
				{
					conn.Close();
					conn.Dispose();
				}
				return rows;
			}
			#endregion

			#region public static string Dt(System.DateTime dt) - This is used in a SQL string to reprasent a datetime.
			/// <summary>
			/// This is used in a SQL string to reprasent a datetime.
			/// </summary>
			/// <param name="dt">This must be of type DateTime</param>
			/// <returns>Returns an sql fragment. NOTE: Don't put the fragment in quotes, just treat it as a value.</returns>
			public static string Dt(System.DateTime dt)
			{
				return "convert(datetime,'" + String.Format("{0:0000}", dt.Year) + "-" + String.Format("{0:00}", dt.Month) + "-" + String.Format("{0:00}", dt.Day) + " " + String.Format("{0:00}", dt.Hour) + ":" + String.Format("{0:00}", dt.Minute) + ":" + String.Format("{0:00}", dt.Second) + "." + String.Format("{0:000}", dt.Millisecond) + "',121)";
			}
			#endregion

			#region ReqS, ReqN - parse numbers or strings from the request collection for insertion in a sql string. - largely outdated.
			/// <summary>
			/// Parses a string for SQL insertion from the request collection
			/// </summary>
			/// <param name="inObj">This string is the key in the request collection.</param>
			/// <returns>A string ready for inclusion in an sql statement - remember, surround in quotes.</returns>
			public static string ReqS(object inObj)
			{
				string inStr = inObj.ToString();
				return Db.PStr(HttpContext.Current.Request[inStr]);
			}
			/// <summary>
			/// Parses a numeric value for SQL insertion from the request collection
			/// </summary>
			/// <param name="inObj">This string is the key in the request collection.</param>
			/// <returns>A safe string reprasenting a number ready for inclusion in an sql statement - remember, don't surround in quotes.</returns>
			public static string ReqN(object inObj)
			{
				string inStr = inObj.ToString();
				return Db.PNum(HttpContext.Current.Request[inStr]);
			}
			#endregion

			#region Pstr / PNum - Parse a string/number for inclusion in an SQL statement. (with a couple of overloads)
			/// <summary>
			/// PStr - Parse a string for the inclusion in an SQL statement.
			/// </summary>
			/// <param name="inObj">the string to parse</param>
			/// <returns>A safe string ready for inclusion in an sql statement - remember, surround in quotes.</returns>
			public static string PStr(object inObj)
			{
				string inStr = inObj.ToString();
				if (inStr == null)
				{
					return "";
				}
				return inStr.Replace("'", "''");
			}
			/// <summary>
			/// PStr - Parse a string from a textbox for the inclusion in an SQL statement.
			/// </summary>
			/// <param name="inObj">the textbox containing the string to parse</param>
			/// <returns>A safe string ready for inclusion in an sql statement - remember, surround in quotes.</returns>
			public static string PStr(System.Web.UI.WebControls.TextBox inObj)
			{
				string inStr = inObj.Text;
				return Db.PStr(inStr);
			}
			/// <summary>
			/// Pstr - Parse a number for the inclusion in an SQL statement.
			/// </summary>
			/// <param name="inObj">the string reprasenting the number to parse</param>
			/// <returns>A safe string reprasenting a number ready for inclusion in an sql statement - remember, don't surround in quotes.</returns>
			public static string PNum(object inObj)
			{
				if (inObj == null)
				{
					return "0";
				}
				string inStr = inObj.ToString();
				if (inStr == null)
				{
					return "0";
				}
				else
				{
					if (inStr == "")
					{
						return "0";
					}
					else
					{
						string outStr = "";
						int foundDot = 0;
						inStr = inStr.Trim();
						for (int i = 0; i < inStr.Length; i++)
						{
							string subStr = inStr.Substring(i, 1);

							if (subStr == "0" || subStr == "1" || subStr == "2" || subStr == "3" || subStr == "4" || subStr == "5" || subStr == "6" || subStr == "7" || subStr == "8" || subStr == "9" || (subStr == "-" && i == 1))
							{
								outStr += subStr;

							}
							if (subStr == "." && foundDot == 0)
							{
								outStr += subStr;
								foundDot = 1;
							}
						}
						if (outStr == "-")
						{
							outStr = "0";
						}
						if (outStr == "-.")
						{
							outStr = "0";
						}
						if (outStr == ".")
						{
							outStr = "0";
						}
						if (outStr.EndsWith("."))
						{
							outStr = outStr.Substring(0, outStr.Length - 1);
						}
						if (outStr == "")
						{
							outStr = "0";
						}
						return outStr;
					}
				}
			}
			/// <summary>
			/// Pstr - Parse a number from a textbox for the inclusion in an SQL statement.
			/// </summary>
			/// <param name="inObj">the textbox containing the number to parse</param>
			/// <returns>A safe string reprasenting a number ready for inclusion in an sql statement - remember, don't surround in quotes.</returns>
			public static string PNum(System.Web.UI.WebControls.TextBox inObj)
			{
				string inStr = inObj.Text;
				return Db.PNum(inStr);
			}
			#endregion

			#region Get - loads a value from a recordset object. - Outdated.
			/// <summary>
			/// loads a value from a recordset object. - Outdated.
			/// </summary>
			/// <param name="obj"></param>
			/// <param name="type"></param>
			/// <returns></returns>
			public static object Get(object obj, System.Type type)
			{
				if (obj == System.DBNull.Value)
					return null;
				if (type == typeof(int))
					return (int)obj;
				else if (type == typeof(string))
					return (string)obj;
				else if (type == typeof(System.DateTime))
				{
					return (System.DateTime)obj;
				}
				else if (type == typeof(bool))
					return (bool)obj;
				else
					return null;
			}
			#endregion

			public static Guid GuidConvertor(object sqlGuidObject)
			{
				if (sqlGuidObject.ToString().Length == 0)
					return Guid.Empty;
				else
					return new Guid(sqlGuidObject.ToString());
			}
			//	public static System.Data.SqlTypes.SqlGuid Guid2SqlGuid(Guid guid)
			//	{
			//		if (guid.Equals(Guid.Empty))
			//			return System.Data.SqlTypes.SqlGuid.Null;
			//		else
			//			return (System.Data.SqlTypes.SqlGuid)guid;
			//	}
		}
		#endregion

		#region ColorHelp
		public class ColorHelp
		{
			static char[] hexDigits = {
			'0', '1', '2', '3', '4', '5', '6', '7',
			'8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};
			/// <summary>
			/// Convert a .NET Color to a hex string.
			/// </summary>
			/// <returns>ex: "FFFFFF", "AB12E9"</returns>
			public static string ColorToHexString(Color color)
			{
				byte[] bytes = new byte[3];
				bytes[0] = color.R;
				bytes[1] = color.G;
				bytes[2] = color.B;
				char[] chars = new char[bytes.Length * 2];
				for (int i = 0; i < bytes.Length; i++)
				{
					int b = bytes[i];
					chars[i * 2] = hexDigits[b >> 4];
					chars[i * 2 + 1] = hexDigits[b & 0xF];
				}
				return new string(chars);
			}
			/// <summary>
			/// Convert a hex string to a .NET Color object.
			/// </summary>
			/// <param name="hexColor">a hex string: "FFFFFF", "#000000"</param>
			public static Color HexStringToColor(string hexColor)
			{
				string hc = ExtractHexDigits(hexColor);
				if (hc.Length != 6)
				{
					// you can choose whether to throw an exception
					//throw new ArgumentException("hexColor is not exactly 6 digits.");
					return Color.Empty;
				}
				string r = hc.Substring(0, 2);
				string g = hc.Substring(2, 2);
				string b = hc.Substring(4, 2);
				Color color = Color.Empty;
				try
				{
					int ri
						= Int32.Parse(r, System.Globalization.NumberStyles.HexNumber);
					int gi
						= Int32.Parse(g, System.Globalization.NumberStyles.HexNumber);
					int bi
						= Int32.Parse(b, System.Globalization.NumberStyles.HexNumber);
					color = Color.FromArgb(ri, gi, bi);
				}
				catch
				{
					// you can choose whether to throw an exception
					//throw new ArgumentException("Conversion failed.");
					return Color.Empty;
				}
				return color;
			}
			/// <summary>
			/// Extract only the hex digits from a string.
			/// </summary>
			public static string ExtractHexDigits(string input)
			{
				// remove any characters that are not digits (like #)
				Regex isHexDigit
					= new Regex("[abcdefABCDEF\\d]+", RegexOptions.Compiled);
				string newnum = "";
				foreach (char c in input)
				{
					if (isHexDigit.IsMatch(c.ToString()))
						newnum += c.ToString();
				}
				return newnum;
			}
		}
		#endregion

		#region RGBHSL
		/* This tool is part of the xRay Toolkit and is provided free of charge by Bob Powell. 
		* This code is not guaranteed to be free from defects or fit for merchantability in any way. 
		* By using this tool in your own programs you agree to hold Robert W. Powell free from all 
		* damages direct or incidental that arise from such use. 
		* You may use this code free of charge in your own projects on condition that you place the 
		* following paragraph (enclosed in quotes below) in your applications help or about dialog. 
		* "Portions of this code provided by Bob Powell. http://www.bobpowell.net" 
		* If you found this code useful and would like to make a donation towards the upkeep of the 
		* GDI+ FAQ you can send a sum of your choice via PayPal to bob@bobpowell.net. 
		*/
		public class RGBHSL
		{

			public class HSL
			{
				public HSL()
				{
					_h = 0;
					_s = 0;
					_l = 0;
				}

				double _h;
				double _s;
				double _l;

				public double H
				{
					get { return _h; }
					set
					{
						_h = value;
						_h = _h > 1 ? 1 : _h < 0 ? 0 : _h;
					}
				}

				public double S
				{
					get { return _s; }
					set
					{
						_s = value;
						_s = _s > 1 ? 1 : _s < 0 ? 0 : _s;
					}
				}

				public double L
				{
					get { return _l; }
					set
					{
						_l = value;
						_l = _l > 1 ? 1 : _l < 0 ? 0 : _l;
					}
				}
			}

			public RGBHSL()
			{
			}

			/// <summary> 
			/// Sets the absolute brightness of a colour 
			/// </summary> 
			/// <param name="c">Original colour</param> 
			/// <param name="brightness">The luminance level to impose</param> 
			/// <returns>an adjusted colour</returns> 
			public static Color SetBrightness(Color c, double brightness)
			{
				HSL hsl = RGB_to_HSL(c);
				hsl.L = brightness;
				return HSL_to_RGB(hsl);
			}

			/// <summary> 
			/// Modifies an existing brightness level 
			/// </summary> 
			/// <remarks> 
			/// To reduce brightness use a number smaller than 1. To increase brightness use a number larger tnan 1 
			/// </remarks> 
			/// <param name="c">The original colour</param> 
			/// <param name="brightness">The luminance delta</param> 
			/// <returns>An adjusted colour</returns> 
			public static Color ModifyBrightness(Color c, double brightness)
			{
				HSL hsl = RGB_to_HSL(c);
				hsl.L *= brightness;
				return HSL_to_RGB(hsl);
			}

			/// <summary> 
			/// Sets the absolute saturation level 
			/// </summary> 
			/// <remarks>Accepted values 0-1</remarks> 
			/// <param name="c">An original colour</param> 
			/// <param name="Saturation">The saturation value to impose</param> 
			/// <returns>An adjusted colour</returns> 
			public static Color SetSaturation(Color c, double Saturation)
			{
				HSL hsl = RGB_to_HSL(c);
				hsl.S = Saturation;
				return HSL_to_RGB(hsl);
			}

			/// <summary> 
			/// Modifies an existing Saturation level 
			/// </summary> 
			/// <remarks> 
			/// To reduce Saturation use a number smaller than 1. To increase Saturation use a number larger tnan 1 
			/// </remarks> 
			/// <param name="c">The original colour</param> 
			/// <param name="Saturation">The saturation delta</param> 
			/// <returns>An adjusted colour</returns> 
			public static Color ModifySaturation(Color c, double Saturation)
			{
				HSL hsl = RGB_to_HSL(c);
				hsl.S *= Saturation;
				return HSL_to_RGB(hsl);
			}

			/// <summary> 
			/// Sets the absolute Hue level 
			/// </summary> 
			/// <remarks>Accepted values 0-1</remarks> 
			/// <param name="c">An original colour</param> 
			/// <param name="Hue">The Hue value to impose</param> 
			/// <returns>An adjusted colour</returns> 
			public static Color SetHue(Color c, double Hue)
			{
				HSL hsl = RGB_to_HSL(c);
				hsl.H = Hue;
				return HSL_to_RGB(hsl);
			}

			/// <summary> 
			/// Modifies an existing Hue level 
			/// </summary> 
			/// <remarks> 
			/// To reduce Hue use a number smaller than 1. To increase Hue use a number larger tnan 1 
			/// </remarks> 
			/// <param name="c">The original colour</param> 
			/// <param name="Hue">The Hue delta</param> 
			/// <returns>An adjusted colour</returns> 
			public static Color ModifyHue(Color c, double Hue)
			{
				HSL hsl = RGB_to_HSL(c);
				hsl.H *= Hue;
				return HSL_to_RGB(hsl);
			}

			/// <summary> 
			/// Converts a colour from HSL to RGB 
			/// </summary> 
			/// <remarks>Adapted from the algoritm in Foley and Van-Dam</remarks> 
			/// <param name="hsl">The HSL value</param> 
			/// <returns>A Color structure containing the equivalent RGB values</returns> 
			public static Color HSL_to_RGB(HSL hsl)
			{
				double r = 0, g = 0, b = 0;
				double temp1, temp2;

				if (hsl.L == 0)
				{
					r = g = b = 0;
				}
				else
				{
					if (hsl.S == 0)
					{
						r = g = b = hsl.L;
					}
					else
					{
						temp2 = ((hsl.L <= 0.5) ? hsl.L * (1.0 + hsl.S) : hsl.L + hsl.S - (hsl.L * hsl.S));
						temp1 = 2.0 * hsl.L - temp2;

						double[] t3 = new double[] { hsl.H + 1.0 / 3.0, hsl.H, hsl.H - 1.0 / 3.0 };
						double[] clr = new double[] { 0, 0, 0 };
						for (int i = 0; i < 3; i++)
						{
							if (t3[i] < 0)
								t3[i] += 1.0;
							if (t3[i] > 1)
								t3[i] -= 1.0;

							if (6.0 * t3[i] < 1.0)
								clr[i] = temp1 + (temp2 - temp1) * t3[i] * 6.0;
							else if (2.0 * t3[i] < 1.0)
								clr[i] = temp2;
							else if (3.0 * t3[i] < 2.0)
								clr[i] = (temp1 + (temp2 - temp1) * ((2.0 / 3.0) - t3[i]) * 6.0);
							else
								clr[i] = temp1;
						}
						r = clr[0];
						g = clr[1];
						b = clr[2];
					}
				}

				return Color.FromArgb((int)(255 * r), (int)(255 * g), (int)(255 * b));

			}


			// 
			/// <summary> 
			/// Converts RGB to HSL 
			/// </summary> 
			/// <remarks>Takes advantage of whats already built in to .NET by using the Color.GetHue, Color.GetSaturation and Color.GetBrightness methods</remarks> 
			/// <param name="c">A Color to convert</param> 
			/// <returns>An HSL value</returns> 
			public static HSL RGB_to_HSL(Color c)
			{
				HSL hsl = new HSL();

				hsl.H = c.GetHue() / 360.0; // we store hue as 0-1 as opposed to 0-360 
				hsl.L = c.GetBrightness();
				hsl.S = c.GetSaturation();

				return hsl;
			}
		}
		#endregion
		#region ExifMetadata
		public class ExifMetadata
		{
			public ExifMetadata()
			{
			}

			public struct MetadataDetail
			{
				public string Hex;
				public string RawValueAsString;
				public string DisplayValue;
			}

			public struct Metadata
			{
				public MetadataDetail EquipmentMake;
				public MetadataDetail CameraModel;
				public MetadataDetail ExposureTime;
				public MetadataDetail Fstop;
				public MetadataDetail DatePictureTaken;
				public MetadataDetail ShutterSpeed;
				public MetadataDetail ExposureCompensation;
				public MetadataDetail MeteringMode;
				public MetadataDetail Flash;
				public MetadataDetail XResolution;
				public MetadataDetail YResolution;
				public MetadataDetail ImageWidth;
				public MetadataDetail ImageHeight;
			}

			public string LookupExifValue(string
				Description, string Value)
			{
				string DescriptionValue = null;

				if (Description == "MeteringMode")
				{
					switch (Value)
					{
						case "0":

							DescriptionValue = "Unknown";
							break;
						case "1":

							DescriptionValue = "Average";
							break;
						case "2":

							DescriptionValue = "Center Weighted Average";
							break;
						case "3":

							DescriptionValue = "Spot";
							break;
						case "4":

							DescriptionValue = "Multi-spot";
							break;
						case "5":

							DescriptionValue = "Multi-segment";
							break;
						case "6":

							DescriptionValue = "Partial";
							break;
						case "255":

							DescriptionValue = "Other";
							break;
					}
				}

				if (Description
					== "ResolutionUnit")
				{
					switch (Value)
					{
						case "1":

							DescriptionValue = "No Units";
							break;
						case "2":

							DescriptionValue = "Inch";
							break;
						case "3":

							DescriptionValue = "Centimeter";
							break;
					}
				}

				if (Description == "Flash")
				{
					switch (Value)
					{
						case "0":
							DescriptionValue = "Flash did not fire";
							break;
						case "1":

							DescriptionValue = "Flash fired";
							break;
						case "5":
							DescriptionValue = "Flash fired but strobe return light not detected";
							break;
						case "7":
							DescriptionValue = "Flash fired and strobe return light detected";
							break;
					}
				}
				return DescriptionValue;
			}

			public Metadata GetExifMetadata(System.Drawing.Image MyImage)
			{
				// Create an instance of the image to gather metadata from
				//System.Drawing.Image MyImage = System.Drawing.Image.FromFile(PhotoName);
				Metadata MyMetadata = new Metadata();
				try
				{

					// Create an integer array to hold the property id list,
					// and populate it with the list from my image.
					/* Note: this only generates a 
														list of integers, one for for each PropertyID.  
															* We will populate the 
														PropertyItem values later. */
					int[] MyPropertyIdList = MyImage.PropertyIdList;

					// Create an array of PropertyItems, but don't populate it yet.
					/* Note: there is a bug in .net 
						framework v1.0 SP2 and also in 1.1 beta:
							* If any particular PropertyItem 
						has a length of 0, you will get an unhandled error
							* when you populate the array 
						directly from the image.
							* So, rather than create an 
						array of PropertyItems and then populate it directly
							* from the image, we will create 
						an empty one of the appropriate length, and then 
							* test each of the PropertyItems 
						ourselves, one at a time, and not add any that 
							* would cause an error. */
					PropertyItem[] MyPropertyItemList = new PropertyItem[MyPropertyIdList.Length];

					// Create an instance of Metadata and populate Hex codes (values populated later)

					MyMetadata.EquipmentMake.Hex = "10f";
					MyMetadata.CameraModel.Hex = "110";
					MyMetadata.DatePictureTaken.Hex = "9003";
					MyMetadata.ExposureTime.Hex = "829a";
					MyMetadata.Fstop.Hex = "829d";
					MyMetadata.ShutterSpeed.Hex = "9201";
					MyMetadata.ExposureCompensation.Hex = "9204";
					MyMetadata.MeteringMode.Hex = "9207";
					MyMetadata.Flash.Hex = "9209";

					// Declare an ASCIIEncoding to use for returning string values from bytes
					System.Text.ASCIIEncoding Value = new System.Text.ASCIIEncoding();

					// Populate MyPropertyItemList.  
					// For each propertyID... 
					int index = 0;
					foreach (int MyPropertyId in MyPropertyIdList)
					{
						// ... try to call GetPropertyItem (it crashes if PropertyItem has length 0, so use Try/Catch)
						try
						{
							// Assign the image's PropertyItem to the PropertyItem array
							MyPropertyItemList[index] = MyImage.GetPropertyItem(MyPropertyId);

							// Troublshooting
							/*

						textBox1.AppendText("\r\n\t" + 
						BitConverter.ToString(MyImage.GetPropertyItem
						(MyPropertyId).Value));

						textBox1.AppendText("\r\n\thex location: " + 
						MyImage.GetPropertyItem(MyPropertyId).Id.ToString("x"));
							*/

							// Assign each element of MyMetadata
							if (MyImage.GetPropertyItem(MyPropertyId).Id.ToString("x") == "10f") // EquipmentMake
							{
								MyMetadata.EquipmentMake.RawValueAsString = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value);
								MyMetadata.EquipmentMake.DisplayValue = Value.GetString(MyPropertyItemList[index].Value);
							}

							if (MyImage.GetPropertyItem(MyPropertyId).Id.ToString("x") == "110") // CameraModel
							{
								MyMetadata.CameraModel.RawValueAsString = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value);
								MyMetadata.CameraModel.DisplayValue = Value.GetString(MyPropertyItemList[index].Value);
							}

							if (MyImage.GetPropertyItem(MyPropertyId).Id.ToString("x") == "9003") // DatePictureTaken
							{
								MyMetadata.DatePictureTaken.RawValueAsString = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value);
								MyMetadata.DatePictureTaken.DisplayValue = Value.GetString(MyPropertyItemList[index].Value);
							}

							if (MyImage.GetPropertyItem(MyPropertyId).Id.ToString("x") == "9207") // MeteringMode
							{
								MyMetadata.MeteringMode.RawValueAsString = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value);
								MyMetadata.MeteringMode.DisplayValue = LookupExifValue("MeteringMode", BitConverter.ToInt16(MyImage.GetPropertyItem(MyPropertyId).Value, 0).ToString());
							}

							if (MyImage.GetPropertyItem(MyPropertyId).Id.ToString("x") == "9209") // Flash
							{
								MyMetadata.Flash.RawValueAsString = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value);
								MyMetadata.Flash.DisplayValue = LookupExifValue("Flash", BitConverter.ToInt16(MyImage.GetPropertyItem(MyPropertyId).Value, 0).ToString());
							}

							if (MyImage.GetPropertyItem(MyPropertyId).Id.ToString("x") == "829a") // ExposureTime
							{
								MyMetadata.ExposureTime.RawValueAsString = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value);

								string StringValue = "";
								for (int Offset = 0; Offset < MyImage.GetPropertyItem(MyPropertyId).Len; Offset = Offset + 4)
								{
									StringValue += BitConverter.ToInt32(MyImage.GetPropertyItem(MyPropertyId).Value, Offset).ToString() + "/";
								}

								MyMetadata.ExposureTime.DisplayValue = StringValue.Substring(0, StringValue.Length - 1);
							}

							if (MyImage.GetPropertyItem(MyPropertyId).Id.ToString("x") == "829d") // F-stop
							{

								MyMetadata.Fstop.RawValueAsString = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value);

								int int1;
								int int2;
								int1 = BitConverter.ToInt32(MyImage.GetPropertyItem(MyPropertyId).Value, 0);
								int2 = BitConverter.ToInt32(MyImage.GetPropertyItem(MyPropertyId).Value, 4);

								MyMetadata.Fstop.DisplayValue = "F/" + (int1 / int2);
							}

							if (MyImage.GetPropertyItem(MyPropertyId).Id.ToString("x") == "9201") // ShutterSpeed
							{
								MyMetadata.ShutterSpeed.RawValueAsString = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value);
								string StringValue = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value).Substring(0, 2);
								MyMetadata.ShutterSpeed.DisplayValue = "1/" + StringValue;
							}

							if (MyImage.GetPropertyItem(MyPropertyId).Id.ToString("x") == "9204") // ExposureCompensation
							{
								MyMetadata.ExposureCompensation.RawValueAsString = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value);
								string StringValue = BitConverter.ToString(MyImage.GetPropertyItem(MyPropertyId).Value).Substring(0, 1);
								MyMetadata.ExposureCompensation.DisplayValue = StringValue + " (Needs work to confirm accuracy)";
							}

						}
						catch (Exception exc)
						{
							// if it is the expected error, do nothing
							if (exc.GetType().ToString() != "System.ArgumentNullException")
							{
							}
						}
						finally
						{
							index++;
						}
					}

					MyMetadata.XResolution.DisplayValue = MyImage.HorizontalResolution.ToString();
					MyMetadata.YResolution.DisplayValue = MyImage.VerticalResolution.ToString();
					MyMetadata.ImageHeight.DisplayValue = MyImage.Height.ToString();
					MyMetadata.ImageWidth.DisplayValue = MyImage.Width.ToString();
				}
				catch { }
				return MyMetadata;
			}
		}
		#endregion

		#region SerializableDictionary
		[XmlRoot("dictionary")]
		public class SerializableDictionary<TKey, TValue> : System.Collections.Generic.Dictionary<TKey, TValue>, System.Xml.Serialization.IXmlSerializable
		{
			#region IXmlSerializable Members
			public System.Xml.Schema.XmlSchema GetSchema()
			{
				return null;
			}

			public void ReadXml(System.Xml.XmlReader reader)
			{
				XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
				XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

				bool wasEmpty = reader.IsEmptyElement;
				reader.Read();

				if (wasEmpty)
					return;

				while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
				{
					reader.ReadStartElement("item");
					reader.ReadStartElement("key");
					TKey key = (TKey)keySerializer.Deserialize(reader);
					reader.ReadEndElement();
					reader.ReadStartElement("value");
					TValue value = (TValue)valueSerializer.Deserialize(reader);
					reader.ReadEndElement();
					this.Add(key, value);
					reader.ReadEndElement();
					reader.MoveToContent();
				}
				reader.ReadEndElement();
			}

			public void WriteXml(System.Xml.XmlWriter writer)
			{
				XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
				XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

				foreach (TKey key in this.Keys)
				{
					writer.WriteStartElement("item");
					writer.WriteStartElement("key");
					keySerializer.Serialize(writer, key);
					writer.WriteEndElement();
					writer.WriteStartElement("value");
					TValue value = this[key];
					valueSerializer.Serialize(writer, value);
					writer.WriteEndElement();
					writer.WriteEndElement();
				}
			}
			#endregion
		}
		#endregion

	}
	namespace Web
	{
		#region WebControls
		namespace WebControls
		{
			public class HtmlCommentStart : LiteralControl
			{
				public HtmlCommentStart()
				{
					this.Text = "<!--";
				}
			}
			public class HtmlCommentEnd : LiteralControl
			{
				public HtmlCommentEnd()
				{
					this.Text = "-->";
				}
			}
			public class CamLink : System.Web.UI.WebControls.LinkButton
			{
				private Hashtable args;
				/// <summary>
				/// This is the Args hashtable that will be accessible to the MasterEvent after postback.
				/// </summary>
				public Hashtable Args
				{
					get
					{
						return this.args;
					}
					set
					{
						this.args = value;
					}
				}
				/// <summary>
				/// Creates a CamButton with args from the hashtable, that will execute the MasterEvent stored in the __EVENT
				/// </summary>
				/// <param name="__EVENT"></param>
				/// <param name="args"></param>
				public CamLink(string __EVENT, Hashtable args)
					: base()
				{
					if (__EVENT.Length > 0)
					{
						args.Add("__EVENT", __EVENT);
					}
					this.args = args;
					this.PreRender += new EventHandler(davesSaveViewState);
				}
				/// <summary>
				/// Creates a new CamButton with blank Args
				/// </summary>
				public CamLink() : this(new Hashtable()) { }
				/// <summary>
				/// Creates a new CamButton with Args from a HashTable
				/// </summary>
				/// <param name="args">This is a hashtable containing the Args</param>
				public CamLink(Hashtable args)
					: this("", args)
				{
					this.args = args;
				}
				/// <summary>
				/// Creates a CamButton that will execute the MasterEvent stored in the __EVENT, blank args.
				/// </summary>
				/// <param name="__EVENT"></param>
				public CamLink(string __EVENT)
					: this(__EVENT, new Hashtable())
				{
				}
				/// <summary>
				/// Hop up the control hierachy till we find a CamPage object...
				/// Remember, the reason that we have to use the CamPage object is that the ViewState property of the Page object is protected.
				/// </summary>
				/// <returns></returns>
				protected void davesSaveViewState(object o, EventArgs e)
				{
					Control thisControl = this;
					while (!(thisControl is Cambro.Web.SiteFramework.TemplatePage))
					{
						thisControl = thisControl.Parent;
					}
					((Cambro.Web.SiteFramework.TemplatePage)thisControl).ViewStatePublic.Add(this.UniqueID, args);
				}
			}
			public class BlessedDataGrid : System.Web.UI.WebControls.DataGrid
			{
				public override void DataBind()
				{
					base.DataBind();
					if (this.EditItemIndex != -1)
					{
						blessGrid(this);
					}
				}
				/// <summary>
				/// BlessGrid - ties a grid's textboxes up so that they will execute the first button in the edit row (usually the update button) when the enter key is pressed.
				/// IMPORTANT: Run this function on a DataGrid in the OnEditCommand function AFTER you call your DataBind.
				/// Note - this is not tested.
				/// </summary>
				/// <param name="grid">The DataGrid to bless.</param>
				private void blessGrid(System.Web.UI.WebControls.DataGrid grid)
				{
					string buttonToTieTo = "";
					string formName = grid.Parent.UniqueID;
					foreach (object ob in grid.Items[grid.EditItemIndex].Controls)
					{
						System.Web.UI.WebControls.TableCell tc = ob as System.Web.UI.WebControls.TableCell;
						if (tc != null)
						{
							try
							{
								System.Web.UI.WebControls.Button bu = tc.Controls[0] as System.Web.UI.WebControls.Button;
								if (bu != null)
								{
									if (buttonToTieTo == "")
									{
										buttonToTieTo = bu.UniqueID;
									}
								}
							}
							catch
							{
							}
						}
					}
					if (buttonToTieTo != "")
					{
						foreach (object ob in grid.Items[grid.EditItemIndex].Controls)
						{
							System.Web.UI.WebControls.TableCell tc = ob as System.Web.UI.WebControls.TableCell;
							if (tc != null)
							{
								try
								{
									System.Web.UI.WebControls.TextBox tb = tc.Controls[0] as System.Web.UI.WebControls.TextBox;
									if (tb != null)
									{
										tb.Attributes.Add("onkeydown", "if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {document." + formName + ".elements['" + buttonToTieTo + "'].click();return false;} else return true;");
									}
								}
								catch
								{
								}
							}
						}
					}
				}
			}

		}
		#endregion
		#region Validators
		namespace Validators
		{
			/// <summary>
			/// Use this in conjunction with a custom validator to validate a credit card expires end field.
			/// </summary>
			public class CcExpVal
			{
				public static string ClientFunction = @"
					<script>
					function valExp(objSource, objArgs)
					{
						var val=new String("""");
						val=objArgs.Value;
						var re=/^\d{1,2}[/]\d{4}$/;
						if (!re.test(val))
						{
							objArgs.IsValid=false;
						}
						else
						{
							var ary=val.split(/[/]/);
							var year=parseInt(ary[1]);
							var month=parseInt(ary[0]);
							if (month>12)
							{
								objArgs.IsValid=false;
								return;
							}
							else
							{
								if (month==12)
								{
									year=year+1;
									month=1;
								}
								else
								{
									month=month+1;
								}
								var dt=new Date(year,month,1);
								var dtnow=new Date();
								if (dtnow>=dt)
								{
									objArgs.IsValid=false;
								}
								else
								{
									objArgs.IsValid=true;
								}
							}
						}
					}
					</script>";
				public static void ServerValidation(object o, System.Web.UI.WebControls.ServerValidateEventArgs objArgs)
				{
					string val = objArgs.Value;
					Regex re = new Regex(@"^\d{1,2}[/]\d{4}$");
					if (!re.IsMatch(val))
					{
						objArgs.IsValid = false;
						return;
					}
					string[] ary = val.Split('/');
					int year = Int32.Parse(ary[1]);
					int month = Int32.Parse(ary[0]);
					if (month > 12)
					{
						objArgs.IsValid = false;
						return;
					}
					DateTime dt = new DateTime(year, month, 1);
					dt = dt.AddMonths(1);
					DateTime dtnow = DateTime.Now;
					if (dtnow >= dt)
					{
						objArgs.IsValid = false;
						return;
					}
					else
					{
						objArgs.IsValid = true;
						return;
					}
				}
			}
		}
		#endregion
		#region Helpers
		public static class Helpers
		{

			#region BuildCleanHtml
			public static void BuildCleanHtml(string Html, StringBuilder sb)
			{
				HtmlCleaner.HtmlReader htmlReader = new HtmlCleaner.HtmlReader("<html>" + Html);

				HtmlCleaner.HtmlWriter writer = new HtmlCleaner.HtmlWriter(sb);

				htmlReader.Read();
				while (!htmlReader.EOF)
				{
					writer.WriteNode(htmlReader, true);
				}

				//	if(sb.Length>=13)
				//		sb.Remove(0, 13);

				//	if (sb.Length > 0 && sb[sb.Length-1] == ' ')
				//		sb.Remove(sb.Length - 1, 1);
			}
			#endregion

			#region CleanHtml
			public static string CleanHtml(this string Html)
			{
				StringBuilder sb = new StringBuilder();
				BuildCleanHtml(Html, sb);
				return sb.ToString();

			}
			#endregion

			#region WriteAlert
			public static int WriteAlert(string txt, int level)
			{
				if (HttpContext.Current!=null && HttpContext.Current.Items["WriteAlertHeader"] != null)
				{
					if (HttpContext.Current.Items["WriteAlertLevelMax"] == null)
					{
						HttpContext.Current.Items["WriteAlertLevelMax"] = level;
					}
					else
					{
						int max = (int)HttpContext.Current.Items["WriteAlertLevelMax"];
						if (level > max)
							HttpContext.Current.Items["WriteAlertLevelMax"] = level;
					}
					HttpContext.Current.Response.Write("<script>TextAlert(\"" + HttpUtility.UrlEncodeUnicode(txt).Replace("\"", "\\\"") + "\"," + level.ToString() + ");</script>\n");
					return level;
				}
				else
					return 0;
			}
			public static int WriteAlert(string txt, bool next)
			{
				if (HttpContext.Current != null && HttpContext.Current.Items["WriteAlertHeader"] != null)
				{
					int level = 1;
					if (HttpContext.Current.Items["WriteAlertLevelMax"] == null)
					{
						HttpContext.Current.Items["WriteAlertLevelMax"] = 1;
					}
					else
					{
						int max = (int)HttpContext.Current.Items["WriteAlertLevelMax"];
						level = max + 1;
						HttpContext.Current.Items["WriteAlertLevelMax"] = level;
					}
					HttpContext.Current.Response.Write("<script>TextAlert(\"" + HttpUtility.UrlEncodeUnicode(txt).Replace("\"", "\\\"") + "\"," + level.ToString() + ");</script>\n");
					return level;
				}
				else
					return 0;
			}
			public static int WriteAlert(string txt)
			{
				if (HttpContext.Current != null && HttpContext.Current.Items["WriteAlertHeader"] != null)
				{
					int level = 1;
					if (HttpContext.Current.Items["WriteAlertLevelMax"] == null)
					{
						HttpContext.Current.Items["WriteAlertLevelMax"] = 1;
					}
					else
					{
						level = (int)HttpContext.Current.Items["WriteAlertLevelMax"];
					}
					HttpContext.Current.Response.Write("<script>TextAlert(\"" + HttpUtility.UrlEncodeUnicode(txt).Replace("\"", "\\\"") + "\"," + level.ToString() + ");</script>\n");
					return level;
				}
				else 
					return 0;
			}
			#endregion
			#region WriteAlertHeader
			public static void WriteAlertHeader()
			{
				if (HttpContext.Current.Items["WriteAlertHeader"] == null)
				{
					HttpContext.Current.Items["WriteAlertHeader"] = 1;
					HttpContext.Current.Response.Buffer = false;
					HttpContext.Current.Response.Write(@"
<form name=form1>
<h1>Daves nerdy batch processor</h1>
<p>
	If you're a nerd, you might understand some of the updates below. 
	If not, just wait until they're finished. When they're done, a link will appear below the box. If the updating stops for more than a couple of minutes, the script might have crashed. If so, click <a href=""/"">here</a> to cancel it.
</p>
<textarea name=Box cols=100 rows=20></textarea>
</form>
<script>
	var i = 0;
	var alerts = [];
	function UrlDecode(psEncodeString) 
	{
		var lsRegExp = /\+/g;
		return unescape(String(psEncodeString).replace(lsRegExp,"" ""));
	}
	function TextAlert(txt,level)
	{
		alerts[level]=txt;
		var box = document.forms[0].elements[""Box""];
		
		box.value = """";
		for (var text in alerts)
		{
			box.value += text+"") ""+UrlDecode(alerts[text])+""\n"";
		}
	}
</script>
");
				}
			}
			#endregion

			#region WriteAlertFooter
			public static void WriteAlertFooter()
			{
				WriteAlertFooter("/admin/utility?"+Cambro.Misc.Utility.GenRandomText(3));
			}
			public static void WriteAlertFooter(string url)
			{
				if (HttpContext.Current != null && HttpContext.Current.Items["WriteAlertHeader"] != null)
				{
					HttpContext.Current.Response.Write("<p>Finished executing batch. <a href=\"" + url + "\">Click here to continue</a></p>");
					HttpContext.Current.Response.End();
				}
			}
			#endregion

			#region ChangeState
			public static void ChangeState(HtmlControl span, WebControl control, bool state)
			{
				if (state)
				{
					span.Attributes["disabled"] = null;
					span.Attributes["class"] = null;
					if (!Vars.IE)
						control.Enabled = true;
				}
				else
				{
					span.Attributes["disabled"] = "true";
					span.Attributes["class"] = "Disabled";
					if (!Vars.IE)
						control.Enabled = false;
				}
			}
			#endregion

			#region Strip
			/// <summary>
			/// strips various things from a user-entered string
			/// </summary>
			public static string Strip(string inStr, bool removeHtml, bool removeDoubleSpaces, bool removeLineBreaks, bool trim)
			{
				string s = inStr;

				if (removeHtml)
				{
					s = Regex.Replace(s, @"<(.|\n)*?>", string.Empty);
					s = Regex.Replace(s, "<", "&lt;");
				}

				if (removeLineBreaks)
					s = Regex.Replace(s, @"[\f\n\r\v]", " ");

				if (removeDoubleSpaces)
					s = Regex.Replace(s, @"[ \t]{2,}", " ");

				if (trim)
					s = s.Trim();

				return s;
			}
			/// <summary>
			/// strips html tags, reduces multiple-spaces to single spaces, and removes line-feeds, and trims leading and trailing whitespace
			/// </summary>
			public static string Strip(this string inStr)
			{
				return Strip(inStr, true, true, true, true);
			}
			/// <summary>
			/// strips html tags
			/// </summary>
			public static string StripHtml(string inStr)
			{
				return Strip(inStr, true, false, false, false);
			}
			/// <summary>
			/// strips html tags, and reduces multiple-spaces to single spaces
			/// </summary>
			public static string StripHtmlDoubleSpaces(string inStr)
			{
				return Strip(inStr, true, true, false, false);
			}
			/// <summary>
			/// strips html tags, reduces multiple-spaces to single spaces, and removes line-feeds
			/// </summary>
			public static string StripHtmlDoubleSpacesLineFeeds(string inStr)
			{
				return Strip(inStr, true, true, true, false);
			}
			#endregion

			#region MakeHtml
			public static string MakeHtml(string inStr, bool addParagraphTags, bool addLineFeeds, int shortenTo)
			{
				string txt = " " + StripHtml(inStr) + " ";

				if (shortenTo > 0 && txt.Length > (shortenTo + 2))
				{
					txt = txt.Substring(0, shortenTo + 1) + "... ";
				}

				Regex dsiHttpRegex = new Regex(@"([ ]?)(http\://www\.dontstayin\.com[^ ]+)([ ]?)");
				Regex httpRegex = new Regex(@"([^""])(http\://[^ ]+)([ ]?)");
				Regex mmsRegex = new Regex(@"([^""])(mms\://[^ ]+)([ ]?)");

				txt = dsiHttpRegex.Replace(txt, "$1<a href=\"$2\">link</a>$3");
				txt = httpRegex.Replace(txt, "$1<a href=\"$2\" target=\"_blank\">link</a>$3");
				txt = mmsRegex.Replace(txt, "$1<a href=\"$2\">link</a>$3");

				txt = txt.Trim();

				if (addLineFeeds)
					txt = txt.Replace("\n", "<br>");

				if (addParagraphTags)
					txt = "<p>" + txt + "</p>";

				return txt;
			}
			public static string MakeHtml(string inStr, bool addParagraphTags)
			{
				return MakeHtml(inStr, addParagraphTags, true, 0);
			}
			public static string MakeHtml(string inStr)
			{
				return MakeHtml(inStr, true);
			}
			#endregion
			#region SearchControl
			public static Control SearchControl(Control c, string id)
			{
				Control initial = c.FindControl(id);
				if (initial != null)
					return initial;
				else
				{
					foreach (Control child in c.Controls)
					{
						if (child.HasControls())
						{
							Control result = SearchControl(child, id);
							if (result != null)
								return result;
						}
					}
					return null;
				}
			}
			#endregion
			#region SetCookie
			public static void SetCookie(string Key, string Value, bool Persist)
			{
				if (HttpContext.Current.Request.Cookies[Key] == null)
				{
					HttpCookie c = new HttpCookie(Key, Value);
					if (Persist)
						c.Expires = DateTime.Now.AddYears(1);
					HttpContext.Current.Response.Cookies.Add(c);
				}
				else
				{
					HttpCookie c = HttpContext.Current.Request.Cookies[Key];
					c.Value = Value;
					if (Persist)
						c.Expires = DateTime.Now.AddYears(1);
					HttpContext.Current.Response.Cookies.Set(c);
				}
			}
			#endregion

			#region DeleteCookie
			public static void DeleteCookie(string Key)
			{
				if (HttpContext.Current.Request.Cookies[Key] == null)
				{
					HttpCookie c = new HttpCookie(Key);
					c.Expires = DateTime.Now.AddYears(-30);
					HttpContext.Current.Response.Cookies.Add(c);
				}
				else
				{
					HttpCookie c = HttpContext.Current.Request.Cookies[Key];
					c.Expires = DateTime.Now.AddYears(-30);
					HttpContext.Current.Response.Cookies.Set(c);
				}
			}
			#endregion

			#region TieButton - This ties a textbox to a button
			/// <summary>
			///		This ties a textbox to a button. 
			/// </summary>
			/// <param name="TextBoxToTie">
			///		This is the textbox to tie to. It doesn't have to be a TextBox control, but must be derived from either HtmlControl or WebControl,
			///		and the html control should accept an 'onkeydown' attribute.
			/// </param>
			/// <param name="ButtonToTie">
			///		This is the button to tie to. All we need from this is it's ClientID. The Html tag it renders should support click()
			/// </param>
			public static void TieButton(Control TextBoxToTie, Control ButtonToTie)
			{
				string formName;
				try
				{
					int i = 0;
					Control c = ButtonToTie.Parent;
					// Step up the control heirachy until either:
					// 1) We find an HtmlForm control
					// 2) We find a Page control - not what we want, but we should stop searching because we a Page will be higher than the HtmlForm.
					// 3) We complete 500 itterations. Obviously we are in a loop, and should stop.
					while (!(c is System.Web.UI.HtmlControls.HtmlForm) & !(c is System.Web.UI.Page) && i < 500)
					{
						c = c.Parent;
						i++;
					}
					// If we have found an HtmlForm, we use it's ClientID for the formName.
					// If not, we use the first form on the page ("forms[0]").
					if (c is System.Web.UI.HtmlControls.HtmlForm)
						formName = c.ClientID;
					else
						formName = "forms[0]";
				}
				catch
				{
					//If we catch an exception, we should use the first form on the page ("forms[0]").
					formName = "forms[0]";
				}
				// Tie the button.
				TieButton(TextBoxToTie, ButtonToTie, formName);
			}
			/// <summary>
			///		This ties a textbox to a button. 
			/// </summary>
			/// <param name="TextBoxToTie">
			///		This is the textbox to tie to. It doesn't have to be a TextBox control, but must be derived from either HtmlControl or WebControl,
			///		and the html control should accept an 'onkeydown' attribute.
			/// </param>
			/// <param name="ButtonToTie">
			///		This is the button to tie to. All we need from this is it's ClientID. The Html tag it renders should support click()
			/// </param>
			/// <param name="formName">
			///		This is the ClientID of the form that the button resides in.
			/// </param>
			public static void TieButton(Control TextBoxToTie, Control ButtonToTie, string formName)
			{
				// This is our javascript - we fire the client-side click event of the button if the enter key is pressed.
				string jsString = "";
				if (ButtonToTie is HtmlButton)
					jsString = "if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {document.getElementById('" + ButtonToTie.ClientID + "').click();return false;} else return true; ";
				else
					jsString = "if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {document." + formName + ".elements['" + ButtonToTie.UniqueID + "'].click();return false;} else return true; ";

				// We attach this to the onkeydown attribute - we have to cater for HtmlControl or WebControl.
				if (TextBoxToTie is System.Web.UI.HtmlControls.HtmlControl)
					((System.Web.UI.HtmlControls.HtmlControl)TextBoxToTie).Attributes.Add("onkeydown", jsString);
				else if (TextBoxToTie is System.Web.UI.WebControls.WebControl)
					((System.Web.UI.WebControls.WebControl)TextBoxToTie).Attributes.Add("onkeydown", jsString);
				else
				{
					// We throw an exception if TextBoxToTie is not of type HtmlControl or WebControl.
					throw new ArgumentException("Control TextBoxToTie should be derived from either System.Web.UI.HtmlControls.HtmlControl or System.Web.UI.WebControls.WebControl", "TextBoxToTie");
				}
			}
			#endregion

			#region UrlSerialize and UrlDeSerialize - not used much - better is UrlTextSerialize.
			/// <summary>
			/// UrlSerialize - this serialises any object onto a valid, search-engine compiant url (using a-z, A-Z, 0-9, -, ~)
			/// This function doesn't produce very efficient results (it puts a ~25 char header on the front). - For unicode strings, use UrlTextSerialize
			/// </summary>
			/// <param name="o">object - must be serialisable</param>
			/// <returns>a string, safe for the filename part of a url.</returns>
			public static string UrlSerialize(object o)
			{
				try
				{
					if ((string)o == "")
						return "";
				}
				catch { }
				char[] chars = "abcdefghijklmnopqrstuvwxyz-~ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
				int[] bits = { 1, 2, 4, 8, 16, 32, 64, 128 };
				IFormatter formatter = new BinaryFormatter();
				Stream stream = new MemoryStream();
				formatter.Serialize(stream, o);
				int currentByte;
				stream.Position = 0;
				currentByte = stream.ReadByte();
				int bitNumber = 0;
				int this6yte = 0;
				string this6yteString = "";
				while (currentByte != -1)
				{
					//Helpers.Trace("ByteIn:"+currentByte.ToString());
					for (int i = 0; i < 8; i++)
					{
						if ((currentByte & bits[i]) > 0)
						{
							this6yte += bits[bitNumber % 6];
						}
						if (bitNumber % 6 == 5)
						{
							this6yteString += chars[this6yte].ToString();
							this6yte = 0;
						}
						bitNumber++;
					}
					currentByte = stream.ReadByte();
				}
				stream.Close();
				return this6yteString;
			}
			/// <summary>
			/// UrlDeserialize - this deserialises any object from a valid, search-engine compiant url (using a-z, A-Z, 0-9, -, ~)
			/// </summary>
			/// <param name="serializedString">a string, safe for the filename part of a url (created by UrlSerialize)</param>
			/// <returns>object.</returns>
			public static object UrlDeSerialize(string serializedString)
			{
				try
				{
					if (serializedString == "")
						return null;
				}
				catch { }
				string chars = "abcdefghijklmnopqrstuvwxyz-~ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
				int[] bits = { 1, 2, 4, 8, 16, 32, 64, 128 };
				IFormatter formatter = new BinaryFormatter();
				Stream stream = new MemoryStream();
				int thisByte = 0;
				int bitNumber = 0;
				for (int i = 0; i < serializedString.Length; i++)
				{
					int this6yte = chars.IndexOf(serializedString.Substring(i, 1));
					for (int j = 0; j < 6; j++)
					{
						if ((this6yte & bits[j]) > 0)
						{
							thisByte += bits[bitNumber % 8];
						}
						if (bitNumber % 8 == 7)
						{
							//Helpers.Trace("ByteOut:"+thisByte.ToString());
							stream.WriteByte((byte)thisByte);
							thisByte = 0;
						}
						bitNumber++;
					}
				}
				if (bitNumber % 8 != 7)
				{
					//Helpers.Trace("ByteOut:"+thisByte.ToString());
					stream.WriteByte((byte)thisByte);
					thisByte = 0;
				}
				stream.Position = 0;
				object obj = formatter.Deserialize(stream);
				stream.Close();
				return obj;

			}
			#endregion

			#region UrlTextSerialize and UrlTextDeserialize
			/// <summary>
			/// UrlTextSerialize - this serialises any Unicode string onto a valid, search-engine compiant url (using a-z, A-Z, 0-9, _, ~, - )
			/// It's quite efficient. All a-z, A-Z, 0-9 are left alone, space -> _, and others are converted into -xx or ~xxxx 
			/// depending on whether they are in the upper unicode range or not (must normal chars map to xx00).
			/// </summary>
			/// <param name="s">unicode string</param>
			/// <returns>url string</returns>
			public static string UrlTextSerialize(string s)
			{
				if (s == "")
					return "";
				string acceptableChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
				string outStr = "";
				for (int i = 0; i < s.Length; i++)
				{
					string sub = s.Substring(i, 1);
					if (acceptableChars.IndexOf(sub) != -1)
					{
						outStr += sub;
					}
					else if (sub == " ")
					{
						outStr += "_";
					}
					else
					{
						byte[] inBytes = System.Text.UnicodeEncoding.Unicode.GetBytes(sub);
						string byteStr = System.BitConverter.ToString(inBytes);
						if (byteStr.Substring(3, 2) == "00")
						{
							outStr += "=" + byteStr.Substring(0, 2);
						}
						else
						{
							outStr += "@" + byteStr.Substring(0, 2) + byteStr.Substring(3, 2);
						}
					}
				}
				return outStr;
			}
			/// <summary>
			/// UrlTextDeserialize - this deserialises a string created with UrlTextSerialize int a Unicode string.
			/// </summary>
			/// <param name="s">url string</param>
			/// <returns>unicode string</returns>
			public static string UrlTextDeSerialize(string s)
			{
				if (s == "")
					return "";
				string outStr = "";
				for (int i = 0; i < s.Length; i++)
				{
					string sub = s.Substring(i, 1);
					if (sub == "@")
					{
						byte[] outBytes = new Byte[2];
						outBytes[0] = (byte)int.Parse(s.Substring(i + 1, 2), System.Globalization.NumberStyles.HexNumber);
						outBytes[1] = (byte)int.Parse(s.Substring(i + 3, 2), System.Globalization.NumberStyles.HexNumber);
						string thisCharStr = System.Text.UnicodeEncoding.Unicode.GetString(outBytes);
						outStr += thisCharStr;
						i += 4;
					}
					if (sub == "=")
					{
						byte[] outBytes = new Byte[2];
						outBytes[0] = (byte)int.Parse(s.Substring(i + 1, 2), System.Globalization.NumberStyles.HexNumber);
						outBytes[1] = (byte)0;
						string thisCharStr = System.Text.UnicodeEncoding.Unicode.GetString(outBytes);
						outStr += thisCharStr;
						i += 2;
					}
					else if (sub == "_")
					{
						outStr += " ";
					}
					else
					{
						outStr += sub;
					}
				}
				return outStr;
			}
			#endregion

		}
		#endregion
		#region SiteFramework
		namespace SiteFramework
		{
			#region TemplateHandlerFactory
			public class TemplateHandlerFactory : IHttpHandlerFactory
			{
				public virtual IHttpHandler GetHandler(HttpContext context, String requestType, String url, String pathTranslated)
				{
					//String fname = url.Substring(url.LastIndexOf('/')+1);
					//String cname = fname.Substring(0, fname.IndexOf('.'));
					//String className = "template.usrDefault";

					string fname = context.Request.Path.Substring(context.Request.Path.LastIndexOf('/') + 1);
					string path = context.Request.Path.Substring(0, context.Request.Path.LastIndexOf('/') + 1);
					string cname = fname.Substring(0, fname.IndexOf('.'));


					//Object h = null;
					//TemplateUserControl uc = (TemplateUserControl)Activator.CreateInstance(Type.GetType(Vars.LocalNamespace+".uc"+cname,true,true));
					//template.usrDefault h = (template.usrDefault)Activator.CreateInstance(Type.GetType("template.usrDefault"));
					string pageName = "usrTemplate.aspx";
					try
					{
						Type userControlClass = Type.GetType(Vars.LocalNamespace + ".uc" + cname, true, true);
						Cambro.Web.SiteFramework.CustomTemplatePage CustomTemplatePageAttribute = (Cambro.Web.SiteFramework.CustomTemplatePage)Attribute.GetCustomAttribute(userControlClass, Type.GetType("Cambro.Web.SiteFramework.CustomTemplatePage"), false);
						pageName = CustomTemplatePageAttribute.PageName;
					}
					catch
					{
					}
					//Get reference to page (modify path to path to page as needed)
					//aspxPage = (System.Web.UI.Page) PageParser.GetCompiledPageInstance(context.Request.Path, context.Server.MapPath(context.Request.Path), context);
					//System.Web.UI.Page aspxPage = (System.Web.UI.Page) PageParser.GetCompiledPageInstance("/usrTemplate.aspx", context.Server.MapPath(Vars.WebRoot+"usrTemplate.aspx"), context);

					TemplatePage aspxPage = (TemplatePage)PageParser.GetCompiledPageInstance("/" + pageName, context.Server.MapPath("/" + pageName), context);

					Control pageContents;
					try
					{
						pageContents = aspxPage.LoadControl(path + "uc" + fname);
					}
					catch (Exception e)
					{
						if (e is System.IO.FileNotFoundException)
						{
							pageContents = aspxPage.LoadControl("/ucFileNotFound.ascx");
						}
						else
						{
							throw (e);
						}
					}

					TemplateUserControl pageContentsTemplateUserControl = (TemplateUserControl)pageContents;


					pageContentsTemplateUserControl.GenericContainerPage = aspxPage;
					//aspxPage.ContentUserControl = pageContentsTemplateUserControl;

					// Process requested page (if postback, the page's event handlers will be called)
					//aspxPage.ProcessRequest(context); //???

					return (IHttpHandler)aspxPage;
				}

				public virtual void ReleaseHandler(IHttpHandler handler) { }
			}
			#endregion
			#region TemplateUserControl
			public class TemplateUserControl : System.Web.UI.UserControl
			{
				public TemplatePage GenericContainerPage;
			}
			#endregion
			public class TemplatePage : System.Web.UI.Page
			{

				/// <summary>
				/// This MUST be the first button on the first form of the page.
				/// If there are multiple buttons on the page, and the .NET environment cannot determine which was clicked (e.g. someone pressed enter in a text box),
				/// this button will receive the click event.
				/// </summary>
				protected Button MasterButton;
				public Hashtable MasterEvents;
				public Hashtable MasterButtonEvents;
				public StateBag ViewStatePublic;
				public ArrayList Traces;
				public HtmlForm TemplateForm;
				public HtmlGenericControl Body;

				#region Code for SiteFramework
				public PlaceHolder Content;
				protected bool ContentUserControlSet = false;
				private TemplateUserControl contentUserControl;
				public TemplateUserControl ContentUserControl
				{
					get
					{
						return this.contentUserControl;
					}
					set
					{
						this.contentUserControl = value;
						ContentUserControlSet = true;
					}
				}
				public void TemplatePage_Init(object o, System.EventArgs e)
				{
					if (!ContentUserControlSet)
					{
						ContentUserControl = (TemplateUserControl)this.LoadControl("/ucFileNotFound.ascx");
					}
					else
					{
						ContentUserControl.InitializeAsUserControl(this);
						Content.Controls.Add(ContentUserControl);
					}
				}
				#endregion

				public TemplatePage()
				{
					MasterEvents = new Hashtable();
					MasterButtonEvents = new Hashtable();
					MasterButton = new Button();
					this.Load += new System.EventHandler(TemplatePage_Load);
					this.Init += new System.EventHandler(TemplatePage_Init);
					//this.SaveViewState += new System.EventHandler(TemplatePage_PreRender);
					ViewStatePublic = base.ViewState;
					Traces = new ArrayList();
				}





				protected delegate void MasterEvent(object o, MasterEventArgs e);

				protected override void LoadViewState(object o)
				{
					base.LoadViewState(o);
					if (this.ViewState["MasterButton"] != null)
					{
						MasterButton.Click += (System.EventHandler)MasterButtonEvents[(string)this.ViewState["MasterButton"]];
					}
				}

				protected void TemplatePage_Load(object o, EventArgs e)
				{
					Hashtable myHashTable;
					MasterEventArgs myMasterEventArgs;
					if (Request["__EVENTTARGET"] != null)
					{
						try
						{
							myHashTable = (Hashtable)this.ViewState[Request["__EVENTTARGET"]];
							myMasterEventArgs = new MasterEventArgs();
							myMasterEventArgs.Args = myHashTable;
							((MasterEvent)MasterEvents[(string)myHashTable["__EVENT"]])(this, myMasterEventArgs);
						}
						catch
						{
							HttpContext.Current.Trace.Warn("MasterEventHandeler code exception");
						}
					}
					foreach (object current in Traces)
					{
						HttpContext.Current.Trace.Warn(current.ToString());
					}
				}
				public class MasterEventArgs : EventArgs
				{
					private Hashtable args;
					public Hashtable Args { get { return this.args; } set { this.args = value; } }
				}
			}


			#region define our CustomTemplatePage attribute
			[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
			public class CustomTemplatePage : Attribute
			{
				public CustomTemplatePage(string pageName)
				{
					this.pageName = pageName;
				}
				private string pageName;
				public string PageName
				{
					get
					{
						return this.pageName;
					}
				}
			}
			#endregion
		}
		#endregion
	}

	#region DnsLib - http://www.csharphelp.com/archives/archive43.html REMOVED

	/*
	namespace DnsLib 
	{
		public class MXRecord 
		{

			public int preference = -1;
			public string exchange = null;

			public override string ToString() 
			{

				return "Preference : " + preference + " Exchange : " + exchange;
			}

		}

		public class DnsLite 
		{

			public static bool HasMX(string domain)
			{
				ArrayList dnsServers = new ArrayList();
				dnsServers.Add("ns1.clara.net");
				dnsServers.Add("ns1.cambro.net");

				DnsLib.DnsLite dl = new DnsLib.DnsLite();
				dl.setDnsServers(dnsServers);

				ArrayList results;

				results = dl.getMXRecords(domain);

				if (results.Count==0)
					return false;
				else
					return true;			
			}

			private byte[] data;
			private int position, id, length;
			private string name;
			private ArrayList dnsServers;

			private static int DNS_PORT = 53;

			Encoding ASCII = Encoding.ASCII;

			public DnsLite() 
			{

				id = DateTime.Now.Millisecond * 60;
				dnsServers = new ArrayList();

			}

			public void setDnsServers(ArrayList dnsServers) 
			{

				this.dnsServers = dnsServers;

			}
			public ArrayList getMXRecords(string host) 
			{

				ArrayList mxRecords = null;

				for(int i=0; i < dnsServers.Count; i++) 
				{

					try 
					{

						mxRecords = getMXRecords(host,(string)dnsServers[i]);
						if (mxRecords.Count>0)
							break;

					}
					catch(IOException) 
					{
						continue;
					}

				}

				return mxRecords;
			}

			private int getNewId() 
			{

				//return a new id
				return ++id;
			}

			public ArrayList getMXRecords(string host,string serverAddress) 
			{

				//opening the UDP socket at DNS server
				//use UDPClient, if you are still with Beta1
				UdpClient dnsClient = new UdpClient(serverAddress, DNS_PORT);

				//preparing the DNS query packet.
				makeQuery(getNewId(),host);

				//send the data packet
				dnsClient.Send(data,data.Length);

				IPEndPoint endpoint = null;
				//receive the data packet from DNS server
				data = dnsClient.Receive(ref endpoint);

				length = data.Length;

				//un pack the byte array & makes an array of MXRecord objects.
				return makeResponse();

			}

			//for packing the information to the format accepted by server
			public void makeQuery(int id,String name) 
			{

				data = new byte[512];

				for(int i = 0; i < 512; ++i) 
				{
					data[i] = 0;
				}

				data[0]	 = (byte) (id >> 8);
				data[1]  = (byte) (id & 0xFF );
				data[2]  = (byte) 1; data[3] = (byte) 0;
				data[4]  = (byte) 0; data[5] = (byte) 1;
				data[6]  = (byte) 0; data[7] = (byte) 0;
				data[8]  = (byte) 0; data[9] = (byte) 0;
				data[10] = (byte) 0; data[11] = (byte) 0;

				string[] tokens = name.Split(new char[] {'.'});
				string label;

				position = 12;

				for(int j=0; j<tokens.Length; j++) 
				{

					label = tokens[j];
					data[position++] = (byte) (label.Length & 0xFF);
					byte[] b = ASCII.GetBytes(label);

					for(int k=0; k < b.Length; k++) 
					{
						data[position++] = b[k];
					}

				}

				data[position++] = (byte) 0 ; data[position++] = (byte) 0;
				data[position++] = (byte) 15; data[position++] = (byte) 0 ;
				data[position++] = (byte) 1 ;

			}

			//for un packing the byte array
			public ArrayList makeResponse() 
			{

				ArrayList mxRecords = new ArrayList();
				MXRecord mxRecord;

				//NOTE: we are ignoring the unnecessary fields.
				//		and takes only the data required to build
				//		MX records.

				int qCount = ((data[4] & 0xFF) << 8) | (data[5] & 0xFF);
				if (qCount < 0) 
				{
					throw new IOException("invalid question count");
				}

				int aCount = ((data[6] & 0xFF) << 8) | (data[7] & 0xFF);
				if (aCount < 0) 
				{
					throw new IOException("invalid answer count");
				}

				position=12;

				for( int i=0;i<qCount; ++i) 
				{
					name = "";
					position = proc(position);
					position += 4;
				}

				for (int i = 0; i < aCount; ++i) 
				{

					name = "";
					position = proc(position);

					position+=10;

					int pref = (data[position++] << 8) | (data[position++] & 0xFF);

					name="";
					position = proc(position);

					mxRecord = new MXRecord();

					mxRecord.preference = pref;
					mxRecord.exchange = name;

					mxRecords.Add(mxRecord);

				}

				return mxRecords;
			}

			private int proc(int position) 
			{

				int len = (data[position++] & 0xFF);

				if(len == 0) 
				{
					return position;
				}

				int offset;

				do 
				{
					if ((len & 0xC0) == 0xC0) 
					{
						if (position >= length) 
						{
							return -1;
						}
						offset = ((len & 0x3F) << 8) | (data[position++] & 0xFF);
						proc(offset);
						return position;
					} 
					else 
					{
						if ((position + len) > length) 
						{
							return -1;
						}
						name += ASCII.GetString(data, position, len);
						position += len;
					}

					if (position > length) 
					{
						return -1;
					}

					len = data[position++] & 0xFF;

					if (len != 0) 
					{
						name += ".";
					}
				}while (len != 0);

				return position;
			}
		}
	}*/

	#endregion

}
