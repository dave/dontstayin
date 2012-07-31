using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLocator
{

	public static class SL
	{
		private static IKernel _kernel;
		public static void Initialize(IKernel kernel)
		{
			_kernel = kernel;
		}
		public static T Get<T>()
		{
			return _kernel.Get<T>();
		}
		 
	}
}
