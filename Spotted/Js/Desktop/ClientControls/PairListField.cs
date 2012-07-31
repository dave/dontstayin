using System;
using System.Html;
using System.Collections;

namespace Js.ClientControls
{
	public class PairListField 
	{
		InputElement hiddenField;
		public PairListField(InputElement hiddenField)
		{
			this.hiddenField = hiddenField;
		}
		private void Add(string key, string value)
		{
			if (hiddenField.Value.Length > 0)
			{
				hiddenField.Value += ";";
			}
			hiddenField.Value += key.Escape() + ":" + value.Escape();
		}
		public void Set(string key, object value)
		{
			this.RemoveByKey(key);
			if (value != null)
			{
				if (hiddenField.Value.Length > 0)
				{
					hiddenField.Value += ";";
				}
				if (value is Date)
				{
					hiddenField.Value += key.Escape() + ":" + ((Date)value).ToDateString().Escape();
				}
				else
				{
					hiddenField.Value += key.Escape() + ":" + value.ToString().Escape();
				}
			}
		}
		public void RemoveAt(int index)
		{
			Array values = ToArray();
			Array newValues = new Array();
			for (int i = 0; i < values.Length; i++)
			{
				if (i == index)
				{
					continue;
				}
				else
				{
					newValues[newValues.Length] = values[i];
				}
			}
			WriteValuesToHiddenInput(newValues);
		}
		public int Count
		{
			get
			{
				if (hiddenField.Value.Length > 0)
				{
					return hiddenField.Value.Split(';').Length;
				}
				else
				{
					return 0;
				}
			}
		}
 
		private void RemoveByKey(string key)
		{
			RemoveAt(IndexOf(key));
		}
		public bool ContainsKey(string key)
		{
			return IndexOf(key) > -1;
		}
		public int IndexOf(string key)
		{
			Array values = ToArray();
			for (int i = 0; i < values.Length; i++)
			{
				if (((string[])values[i])[0] == key)
				{
					return i;
				}
			}
			return -1;
		}
		private void WriteValuesToHiddenInput(Array array)
		{
			this.hiddenField.Value = "";
			Array tempArray = new Array();
			for (int i = 0; i < array.Length; i++)
			{
				string[] pair = (string[]) array[i];
				tempArray[i] = pair[0].Escape() + ":" + pair[1].Escape();
			}
			this.hiddenField.Value = tempArray.Join(";");
		}
		public Array ToArray()
		{
			Array array = new Array();
			if (hiddenField.Value.Length > 0)
			{
				string[] pairs = hiddenField.Value.Split(';');
				for (int i = 0; i < pairs.Length; i++)
				{
					string[] pair = pairs[i].Split(':');
					pair[0] = pair[0].Unescape();
					pair[1] = pair[1].Unescape();
					array[array.Length] = pair;
				}
			}
			return array;
		}

		internal void Clear()
		{
			hiddenField.Value = "";
		}

		internal Dictionary ToDictionary()
		{
			Array values = ToArray();
			Dictionary dictionary = new Dictionary();
			for (int i = 0; i < values.Length; i++)
			{
				string[] pair = (string[]) values[i];
				dictionary[pair[0]] = pair[1];
			}
			return dictionary;
		}
	}
}
