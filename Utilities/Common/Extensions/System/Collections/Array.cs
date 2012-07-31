using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
	public static class ArrayExtensions
	{
		public static T[] ConvertAll<T, U>(this U[] items, Converter<U, T> converter){
			T[] newArray = new T[items.Length];
			for (int i = 0; i < items.Length; i++)
			{
				newArray[i] = converter(items[i]);
			}
			return newArray;
		}

		public static long ToBitwiseLong(this bool[] values)
		{
			long value = 0;
			for (int i = 0; i < values.Length; i++)
			{
				if (values[i]) value += 1L << i;
			}
			return value;
		}
	}
}
