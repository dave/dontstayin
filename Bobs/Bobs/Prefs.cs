using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Web;
using System.Xml;

namespace Bobs
{
	#region Prefs
	/// <summary>
	/// Prefs settings for browser guid
	/// </summary>
	[Serializable]
	public partial class Prefs
	{

		#region Simple members
		/// <summary>
		/// Browser guid
		/// </summary>
		public override Guid Guid
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[Prefs.Columns.Guid]); }
			set { this[Prefs.Columns.Guid] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Prefs string
		/// </summary>
		public override string PrefsString
		{
			get { return (string)this[Prefs.Columns.PrefsString]; }
			set { this[Prefs.Columns.PrefsString] = value; }
		}
		#endregion

        #region Keys
        public const string NEEDS_TICKET_FEEDBACK_LINKS_KEY = "NeedsTicketFeedbackLinks";
        public const string NEEDS_TICKET_FEEDBACK_NEXT_DATE_KEY = "NeedsTicketFeedbackNextDate";
        #endregion

        #region Current
        public static PrefsHolder Current
		{
			get
			{
				if (HttpContext.Current == null) { return null; }
				if (HttpContext.Current.Items["Prefs"] == null)
				{
					if (Usr.Current == null)
					{
						try
						{
							Prefs p = new Prefs(Visit.Current.Guid);
							HttpContext.Current.Items["Prefs"] = new PrefsHolder(p.PrefsString, Prefs.Sources.BrowserGuid);
						}
						catch (BobNotFound)
						{
							HttpContext.Current.Items["Prefs"] = new PrefsHolder("", Prefs.Sources.BrowserGuid);
						}
					}
					else
					{
						Prefs.InitialiseFromUsr(Usr.Current);
					}
				}
				return (PrefsHolder)HttpContext.Current.Items["Prefs"];
			
			}
		}
		#endregion

		#region InitialiseFromUsr(Usr usr)
		public static void InitialiseFromUsr(Usr usr)
		{
			HttpContext.Current.Items["Prefs"] = new PrefsHolder(usr.PrefsText, Prefs.Sources.CurrentUsr);
		}
		#endregion

		#region UpdateCurrentIfExists()
		public static void UpdateCurrentIfExists()
		{
			if (HttpContext.Current.Items["Prefs"] != null)
				Current.Update();
		}
		#endregion

		public static bool HideBanners
		{
			get
			{
				return Prefs.Current["HideBanners"] == 1;// || Vars.ClientIsDevBox;
			}
		}

		public static bool HideChat
		{
			get
			{
				return Prefs.Current["HideChat1"] == 1;// || Vars.ClientIsDevBox;
			}
		}

		#region PrefsHolder
		/// <summary>
		/// A dictionary with keys of type string and values of type Pref
		/// </summary>
		public class PrefsHolder : System.Collections.DictionaryBase
		{

			Dictionary<string, string> previousPrefs;

			#region PrefsHolder()
			/// <summary>
			/// Initializes a new empty instance of the PrefsHolder class
			/// </summary>
			public PrefsHolder()
			{
				// empty
			}
			#endregion

			#region PrefsHolder(string prefsString, Sources source)
			public PrefsHolder(string prefsString, Sources source)
			{
				Source = source;
				previousPrefs = DeSerialise(prefsString);
				foreach (string key in previousPrefs.Keys)
					this[key] = new Pref(key, previousPrefs[key]);
			}
			#endregion

			#region this[string key]
			public virtual Pref this[string key]
			{
				get
				{
					if (this.Dictionary[key] == null)
						return new Pref(key, true);
					else
						return (Pref)this.Dictionary[key];
				}
				set
				{
					this.Dictionary[key] = value;
					((Pref)this.Dictionary[key]).Key = key;
				}
			}
			#endregion

			#region Sources
			#endregion

			#region Source
			public Sources Source
			{
				get
				{
					return source;
				}
				set
				{
					source = value;
				}
			}
			Sources source;
			#endregion

			#region HasChanged()
			public bool HasChanged()
			{
				if (previousPrefs.Count != this.Count)
					return true;

				foreach (string key in this.Keys)
				{
					if (!this[key].Value.Equals(previousPrefs[key]))
						return true;
				}

				return false;
			}
			#endregion

			#region Update()
			public void Update()
			{
				if (this.HasChanged())
				{
					if (Source.Equals(Sources.BrowserGuid))
					{
						Prefs p;
						try
						{
							p = new Prefs(Visit.Current.Guid);
						}
						catch (BobNotFound)
						{
							p = new Prefs();
							p.Guid = Visit.Current.Guid;
						}
						p.PrefsString = this.Serialise();

						try
						{
							p.Update();
						}
						catch (System.Data.SqlClient.SqlException)
						{
							try
							{
								p = new Prefs(Visit.Current.Guid);
								p.PrefsString = this.Serialise();
								p.Update();
							}
							catch { }
						}

					}
					else
					{
						Usr.Current.PrefsText = this.Serialise();
						Usr.Current.Update();
					}
				}
			}
			#endregion

			#region Add(string key, Pref value)
			/// <summary>
			/// Adds an element with the specified key and value to this PrefsHolder.
			/// </summary>
			/// <param name="key">
			/// The string key of the element to add.
			/// </param>
			/// <param name="value">
			/// The Pref value of the element to add.
			/// </param>
			public virtual void Add(string key, Pref value)
			{
				this.Dictionary.Add(key, value);
			}
			#endregion

			#region Contains(string key)
			/// <summary>
			/// Determines whether this PrefsHolder contains a specific key.
			/// </summary>
			/// <param name="key">
			/// The string key to locate in this PrefsHolder.
			/// </param>
			/// <returns>
			/// true if this PrefsHolder contains an element with the specified key;
			/// otherwise, false.
			/// </returns>
			public virtual bool Contains(string key)
			{
				return this.Dictionary.Contains(key);
			}
			#endregion

			#region ContainsKey(string key)
			/// <summary>
			/// Determines whether this PrefsHolder contains a specific key.
			/// </summary>
			/// <param name="key">
			/// The string key to locate in this PrefsHolder.
			/// </param>
			/// <returns>
			/// true if this PrefsHolder contains an element with the specified key;
			/// otherwise, false.
			/// </returns>
			public virtual bool ContainsKey(string key)
			{
				return this.Dictionary.Contains(key);
			}
			#endregion

			#region ContainsValue(Pref value)
			/// <summary>
			/// Determines whether this PrefsHolder contains a specific value.
			/// </summary>
			/// <param name="value">
			/// The Pref value to locate in this PrefsHolder.
			/// </param>
			/// <returns>
			/// true if this PrefsHolder contains an element with the specified value;
			/// otherwise, false.
			/// </returns>
			public virtual bool ContainsValue(Pref value)
			{
				foreach (Pref item in this.Dictionary.Values)
				{
					if (item == value)
						return true;
				}
				return false;
			}
			#endregion

			#region Remove(string key)
			/// <summary>
			/// Removes the element with the specified key from this PrefsHolder.
			/// </summary>
			/// <param name="key">
			/// The string key of the element to remove.
			/// </param>
			public virtual void Remove(string key)
			{
				this.Dictionary.Remove(key);
			}
			#endregion

			#region Keys
			/// <summary>
			/// Gets a collection containing the keys in this PrefsHolder.
			/// </summary>
			public virtual System.Collections.ICollection Keys
			{
				get
				{
					return this.Dictionary.Keys;
				}
			}
			#endregion

			#region Values
			/// <summary>
			/// Gets a collection containing the values in this PrefsHolder.
			/// </summary>
			public virtual System.Collections.ICollection Values
			{
				get
				{
					return this.Dictionary.Values;
				}
			}
			#endregion

			#region Serialise()
			public string Serialise()
			{
				if (this.Count == 0)
					return "";
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.AppendChild(xmlDoc.CreateElement("prefs"));
				XmlNode prefs = xmlDoc.GetElementsByTagName("prefs")[0];
				foreach (string key in this.Keys)
				{
					prefs.AppendChild(Cambro.Misc.Utility.CreateXmlNode(xmlDoc, "pref", System.Web.HttpUtility.UrlEncode(key), System.Web.HttpUtility.UrlEncode(this[key].Value)));
				}
				return xmlDoc.InnerXml;
			}
			#endregion

			#region DeSerialise
			public static Dictionary<string, string> DeSerialise(string stringIn)
			{
				Dictionary<string, string> dictionary = new Dictionary<string, string>();
				if (stringIn.Length == 0)
					return dictionary;
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(stringIn);
				foreach (XmlNode xmlNode in xmlDoc.GetElementsByTagName("pref"))
				{
					dictionary[System.Web.HttpUtility.UrlDecode(xmlNode.Attributes["k"].Value)] = System.Web.HttpUtility.UrlDecode(xmlNode.InnerText);
				}
				return dictionary;
			}
			#endregion

		}
		#endregion

		#region Pref
		public class Pref
		{
			#region public Pref(...)
			public Pref(string value)
			{
				this.Exists = true;
				this.Value = value;
			}
			public Pref(string key, string value)
			{
				this.Exists = true;
				this.Key = key;
				this.Value = value;
			}
			public Pref(string key, bool nullPref)
			{
				this.Exists = false;
				this.Key = key;
			}
			#endregion
			#region Exists
			public bool Exists { get; set; }
			#endregion
			#region IsNull
			public bool IsNull
			{
				get { return !Exists; }
			}
			#endregion
			#region Key
			public string Key { get; set; }
			#endregion
			#region KeyInt
			public int KeyInt
			{
				get
				{
					int value;
					if (int.TryParse(Key, out value))
						return value;
					else
						return 0;
				}
			}
			#endregion
			#region Value
			public string Value { get; set; }
			#endregion
			#region IsInt
			public bool IsInt
			{
				get
				{
					if (Exists)
					{
						int value;
						return int.TryParse(Value, out value);
					}
					else
						return false;
				}
			}
			#endregion
			#region Operators
			public override string ToString()
			{
				return Value;
			}
			public override bool Equals(object o)
			{
				if (o is Pref)
					return this.IsNull.Equals(((Pref)o).IsNull) && this.Key.Equals(((Pref)o).Key) && this.Value.Equals(((Pref)o).Value);
				else
					return this.Exists && this.Value.Equals(o.ToString());
			}
			public static bool operator ==(Pref pref, Pref p)
			{
				return pref.Equals(p);
			}
			public static bool operator !=(Pref pref, Pref p)
			{
				return !pref.Equals(p);
			}
			public static bool operator ==(Pref pref, string s)
			{
				return pref.Equals(s);
			}
			public static bool operator !=(Pref pref, string s)
			{
				return !pref.Equals(s);
			}
			public static bool operator ==(Pref pref, int i)
			{
				return pref.Equals(i);
			}
			public static bool operator !=(Pref pref, int i)
			{
				return !pref.Equals(i);
			}
			public static implicit operator string(Pref pref)
			{
				return pref.ToString();
			}
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}
			public static implicit operator int(Pref pref)
			{
				if (pref.IsInt)
					return int.Parse(pref.Value);
				else
					return 0;
			}
			public static implicit operator Pref(string value)
			{
				return new Pref(value);
			}
			public static implicit operator Pref(int value)
			{
				return new Pref(value.ToString());
			}
			#endregion
		}
		#endregion

	}
	#endregion
}
