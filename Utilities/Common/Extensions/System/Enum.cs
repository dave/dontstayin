using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Common;
using System.Reflection;

namespace System
{
	public static class EnumExtensions
	{
		
		public static T EnumParse<T>(this string value)
		{
			return EnumExtensions.EnumParse<T>(value, false);
		}
		public static T EnumParse<T>(this string value, bool ignoreCase)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			value = value.Trim();
			if (value.Length == 0)
			{
				throw new ArgumentException("Must specify valid information for parsing in the string.", "value");
			}
			Type t = typeof(T); if (!t.IsEnum)
			{
				throw new ArgumentException("Type provided must be an Enum.", "T");
			}
			T enumType = (T)Enum.Parse(t, value, ignoreCase);
			return enumType;
		}
	
		/// <summary>
		/// Gets the <see cref="DescriptionAttribute"/> of an <see cref="E
		/// </summary>
		/// <param name="value">The <see cref="Enum"/> type value.</param>
		/// <returns>A string containing the text of the 
		public static string GetDescription(Enum value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}

			string description = value.ToString();
			FieldInfo fieldInfo = value.GetType().GetField(description);
			EnumDescriptionAttribute[] attributes = (EnumDescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(EnumDescriptionAttribute), false);

			if (attributes != null && attributes.Length > 0)
			{
				description = attributes[0].Description;
			}
			return description;
		}
		
		
	}

}
