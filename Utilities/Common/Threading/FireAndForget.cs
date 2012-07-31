using System;
namespace Common.Threading
{
	public class FireAndForget
	{
		/// 	
		/// Delegate to wrap another delegate and its arguments
		/// 
		delegate void DelegateWrapper(Delegate d, object[] args);

		/// 
		/// An instance of DelegateWrapper which calls InvokeWrappedDelegate,
		/// which in turn calls the DynamicInvoke method of the wrapped
		/// delegate.
		/// 
		static DelegateWrapper wrapperInstance = new DelegateWrapper(InvokeWrappedDelegate);

		/// 
		/// Callback used to call EndInvoke on the asynchronously
		/// invoked DelegateWrapper.
		/// 
		static AsyncCallback callback = new AsyncCallback(EndWrapperInvoke);

		/// 
		/// Executes the specified delegate with the specified arguments
		/// asynchronously on a thread pool thread.
		/// 
		public static void Execute(Delegate d, params object[] args)
		{
			// Invoke the wrapper asynchronously, which will then
			// execute the wrapped delegate synchronously (in the
			// thread pool thread)
			wrapperInstance.BeginInvoke(d, args, callback, null);
		}

		/// 
		/// Invokes the wrapped delegate synchronously
		/// 
		static void InvokeWrappedDelegate(Delegate d, object[] args)
		{
			d.DynamicInvoke(args);
		}

		/// 
		/// Calls EndInvoke on the wrapper and Close on the resulting WaitHandle
		/// to prevent resource leaks.
		/// 
		static void EndWrapperInvoke(IAsyncResult ar)
		{
			wrapperInstance.EndInvoke(ar);
			ar.AsyncWaitHandle.Close();
		}
	}
}
