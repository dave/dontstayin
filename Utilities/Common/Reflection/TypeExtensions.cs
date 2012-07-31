using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Common.Reflection
{
	public static class TypeExtensions
	{
		public static MethodInfo[] GetGetters(Type t)
		{
			return GetMethods(t, "get_").ToArray();
		}
		public static MethodInfo[] GetSetters(Type t)
		{
			return GetMethods(t, "set_").ToArray();
		}
		static List<MethodInfo> GetMethods(Type t, string methodPrefix)
		{
			List<MethodInfo> methods = new List<MethodInfo>();
			foreach (MethodInfo mi in t.GetMethods(BindingFlags.Static | BindingFlags.Public))
			{
				if (mi.Name.Substring(0, methodPrefix.Length) == methodPrefix)
				{
					methods.Add(mi);
				}
			}
			return methods;
		}
	}
}
