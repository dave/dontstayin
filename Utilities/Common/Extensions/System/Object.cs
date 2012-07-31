using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
	public static class ObjectExtensions
	{
		public static void SwapElements<T>(this List<T> list, int index0, int index1){
			T temp = list[index0];
			list[index0] = list[index1];
			list[index1] = temp;
		}
		public static void SwitchValueWith<T>(this T a, ref T b)
		{
			T temp = a;
			a = b;
			b = temp;
		}
		
	}
}
