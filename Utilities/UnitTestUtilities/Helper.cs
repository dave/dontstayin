using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
namespace UnitTestUtilities
{
	public class Helper
	{
		public static object RunStaticMethod(System.Type t, string strMethod, params object[] aobjParams)
		{
			BindingFlags eFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			return RunMethod(t, strMethod, null, eFlags, aobjParams);
		}
		public static object RunStaticMethod(System.Type t, string strMethod, System.Type[] paramTypes, params object[] aobjParams)
		{
			BindingFlags eFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			return RunMethod(t, strMethod, null, eFlags, paramTypes, aobjParams);
		}

		public static object RunInstanceMethod(System.Type t, string strMethod, object objInstance, params object[] aobjParams)
		{
			BindingFlags eFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			return RunMethod(t, strMethod, objInstance, eFlags, aobjParams);
		}

		private static object RunMethod(System.Type t, string methodName, object objInstance, BindingFlags eFlags, System.Type[] parameterDefinitions, object[] parameterValues)
		{
			foreach (MethodInfo m in t.GetMethods(eFlags))
			{
				if (m.Name == methodName && MethodMatchesParameterDefinitions(m, parameterDefinitions))
				{
					if (parameterDefinitions.Length == parameterValues.Length + 1)
					{
						throw new NotImplementedException("The case in which no args are passed to params parameter.");
					}
						// if only parameter is params arg, compiler collapses it. this re-expands it: 
					else if (parameterDefinitions[parameterDefinitions.Length - 1] != parameterValues[parameterValues.Length - 1].GetType())
					{
						Array unknownTypeArray = Array.CreateInstance(parameterValues[0].GetType(), parameterValues.Length);
						parameterValues.CopyTo(unknownTypeArray, 0);

						return m.Invoke(objInstance, new object[] { unknownTypeArray });
					}
					else
					{
						return m.Invoke(objInstance, parameterValues);
					}
					
				}
			}
			throw new ArgumentException("There is no method '" + methodName + "' for type '" + t.ToString() + "' which matched the parameter type list.");
		}

		private static bool MethodMatchesParameterDefinitions(MethodInfo m, System.Type[] parameterDefinitions)
		{
			ParameterInfo[] currentMethodParameters = m.GetParameters();

			if (parameterDefinitions.Length != currentMethodParameters.Length)
			{
				return false;
			}else{
				for (int i = 0; i < parameterDefinitions.Length; i++)
				{
					if (parameterDefinitions[i] != currentMethodParameters[i].ParameterType)
					{
						return false;
					}
				}
			}
			return true;
		}
		private static object RunMethod(System.Type t, string strMethod, object objInstance, BindingFlags eFlags, object[] aobjParams)
		{
			MethodInfo m = t.GetMethod(strMethod, eFlags);
			if (m == null)
			{
				throw new ArgumentException("There is no method '" + strMethod + "' for type '" + t.ToString() + "'.");
			}
			return m.Invoke(objInstance, aobjParams);
		}
		public static object GetStaticProperty(System.Type t, string propertyName)
		{
			object objInstance = null;
			return GetProperty(t, propertyName, objInstance);
		}
		public static object GetProperty(System.Type t, string propertyName, object objInstance)
		{
			BindingFlags eFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			PropertyInfo p = t.GetProperty(propertyName, eFlags);
			if (p == null)
			{
				throw new ArgumentException("There is no property'" + propertyName + "' for type '" + t.ToString() + "'.");
			}
			return p.GetValue(objInstance, null);
		}

	}
}
