using System;

namespace ServiceLocator
{
	public class DelegateKernel : IKernel
	{
		private readonly Func<Type, object> getter;

		public DelegateKernel(Func<Type, object> getter)
		{
			this.getter = getter;
		}
		public T Get<T>()
		{
			return (T)this.getter(typeof(T));
		}

	}
}
