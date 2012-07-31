using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
namespace Common.Reflection
{
	/// <summary>
	/// From http://www.codeproject.com/csharp/FastMethodInvoker.asp
	/// Contians method for calling a method quicker then using the <see cref="MethodInfo.Invoke(object, object[])"/>.
	/// </summary>
	public static class OptimizedMethodCall
	{
		/// <summary>
		/// The delegate that's used as a mask for the method call created.
		/// </summary>
		/// <param name="o">the object that is used to make the call, null for static
		/// cases.</param>
		/// <param name="parameters">The parameters of the call.</param>
		/// <returns>The result of the function.</returns>
		public delegate object MethodInvoke(object o, object[] parameters);
		/// <summary>
		/// Fast-Integer lookup.
		/// </summary>
		private static Dictionary<int, OpCode> valueIndex = new Dictionary<int, OpCode>();
		/// <summary>
		/// Initialize the lookup.
		/// </summary>
		static OptimizedMethodCall()
		{
			valueIndex.Add(-1, OpCodes.Ldc_I4_M1);
			valueIndex.Add(0, OpCodes.Ldc_I4_0);
			valueIndex.Add(1, OpCodes.Ldc_I4_1);
			valueIndex.Add(2, OpCodes.Ldc_I4_2);
			valueIndex.Add(3, OpCodes.Ldc_I4_3);
			valueIndex.Add(4, OpCodes.Ldc_I4_4);
			valueIndex.Add(5, OpCodes.Ldc_I4_5);
			valueIndex.Add(6, OpCodes.Ldc_I4_6);
			valueIndex.Add(7, OpCodes.Ldc_I4_7);
			valueIndex.Add(8, OpCodes.Ldc_I4_8);
		}
		/// <summary>
		/// Returns an optimized delegate method pointer which invokes the method described
		/// in <paramref name="method"/>.
		/// </summary>
		/// <param name="method">The method that is to be referenced during the build process.</param>
		/// <returns>A new instance of <see cref="MethodInvoke"/> which is responsible
		/// for invoking the method.</returns>
		public static MethodInvoke BuildOptimizedDelegate(MethodInfo method)
		{
			ParameterInfo[] methodParameters;
			ILGenerator interLangGenerator = null;
			DynamicMethod optimizedDelegate = new DynamicMethod(string.Format("{0}@{1}", method.Name, method.DeclaringType.Name), typeof(object), new Type[] { typeof(object), typeof(object[]) }, method.DeclaringType.Module);
			interLangGenerator = optimizedDelegate.GetILGenerator();
			methodParameters = method.GetParameters();
			List<LocalBuilder> paramLocals = new List<LocalBuilder>();
			for (int i = 0; i < methodParameters.Length; i++)
			{
				ParameterInfo param = methodParameters[i];
				/* *
				 * First is the declaration of local parameter-copies that are pushed
				 * from the source array (the second argument, first argument index) into
				 * the local values built off of the types related.
				 * */
				System.Type actType = null;
				LocalBuilder parameterLocal = null;
				if (param.ParameterType.IsByRef)
					actType = param.ParameterType.GetElementType();
				else
					actType = param.ParameterType;
				parameterLocal = interLangGenerator.DeclareLocal(actType);
				interLangGenerator.Emit(OpCodes.Ldarg_1);
				if (param.Position >= -1 && param.Position <= 8)
					interLangGenerator.Emit(valueIndex[param.Position]);
				else if (param.Position > -129 && param.Position < 128)
					interLangGenerator.Emit(OpCodes.Ldc_I4_S, (SByte)param.Position);
				else
					interLangGenerator.Emit(OpCodes.Ldc_I4, param.Position);
				//Indicate the location of the parameter.
				//Now, using what's on the stack, it's going to use the index and array
				//and load the value at the index on the array into the stack.
				interLangGenerator.Emit(OpCodes.Ldelem_Ref);
				/* *
				 * Next, we check if it's a value type, since everything was passed as an object
				 * The value types will need to be 'unboxed' or de-objectified; typically this
				 * boxing and unboxing is a speed hit, but it's neessary. Otherwise, 
				 * the non-value-types will be cast to their respective types.
				 * */
				if (actType.IsValueType)
					interLangGenerator.Emit(OpCodes.Unbox_Any, actType);
				else
					interLangGenerator.Emit(OpCodes.Castclass, actType);
				interLangGenerator.Emit(OpCodes.Stloc, parameterLocal);
				paramLocals.Add(parameterLocal);
			}
			/* *
			 * Now that the load phase is done, next step is to check if the method requests
			 * an instance to work properly, if it does, push it to the stack.  Then, tell it to
			 * push the properly cast or unboxed arguments onto the stack.
			 * */
			if (!method.IsStatic)
				interLangGenerator.Emit(OpCodes.Ldarg_0);

			for (int i = 0; i < methodParameters.Length; i++)
			{
				/* *
				 * Remember, by-reference arguments need to be passed by address,
				 * otherwise just the value is used.
				 * */
				if (methodParameters[i].ParameterType.IsByRef)
					interLangGenerator.Emit(OpCodes.Ldloca_S, paramLocals[i]);
				else
					interLangGenerator.Emit(OpCodes.Ldloc, paramLocals[i]);
			}

			/* *
			 * Once the stack is built, the next step is to push forth the method call itself.
			 * Remember, since everything's objects, the method's result needs boxed,
			 * so we emit a notice to box the result.
			 * */
			if (method.IsStatic)
				interLangGenerator.EmitCall(OpCodes.Call, method, null);
			else
				interLangGenerator.EmitCall(OpCodes.Callvirt, method, null);

			if (method.ReturnType == typeof(void))
				interLangGenerator.Emit(OpCodes.Ldnull);
			else if (method.ReturnType.IsValueType)
				interLangGenerator.Emit(OpCodes.Box, method.ReturnType);
			/* *
			 * Now, the method's been called, the next step is to update the 'byref' parameters
			 * by copying the [potentially] new values back to the array.
			 * */
			for (int i = 0; i < methodParameters.Length; i++)
			{
				ParameterInfo param = methodParameters[i];
				if (param.ParameterType.IsByRef)
				{
					interLangGenerator.Emit(OpCodes.Ldarg_1);
					/* *
					 * Use the lookup for the index of the parameter in the 'params' array
					 * where appropriate.
					 * */
					if (param.Position >= -1 && param.Position <= 8)
						interLangGenerator.Emit(valueIndex[param.Position]);
					else if (param.Position > -129 && param.Position < 128)
						interLangGenerator.Emit(OpCodes.Ldc_I4_S, (SByte)param.Position);
					else
						interLangGenerator.Emit(OpCodes.Ldc_I4, param.Position);
					//Load the local value onto the stack...
					interLangGenerator.Emit(OpCodes.Ldloc, paramLocals[i]);
					//If it's a value-type, re-box the value unboxed before.
					//It might've changed.
					if (paramLocals[i].LocalType.IsValueType)
						interLangGenerator.Emit(OpCodes.Box, paramLocals[i].LocalType);
					//Set the value.
					interLangGenerator.Emit(OpCodes.Stelem_Ref);
				}
			}
			/* *
			 * Denote that it should return now, it'll use the stack values to determine the
			 * result value.
			 * */
			interLangGenerator.Emit(OpCodes.Ret);
			//Return.
			return (MethodInvoke)optimizedDelegate.CreateDelegate(typeof(MethodInvoke));
		}
	}

}
