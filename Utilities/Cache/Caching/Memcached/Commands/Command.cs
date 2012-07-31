using System;
using System.Collections.Generic;
using System.Text;

namespace Caching.Memcached.Commands
{
	internal abstract class Command
	{
		internal enum StoredObjectType
		{
			SerializableObject,
			IntCounter,
			UIntCounter,
			LongCounter
		}


		public abstract void Execute(MemcachedSocket memcachedSocket);
		internal void CheckResponse(string actualResponse, params string[] expectedResponses)
		{
			foreach (string expectedResponse in expectedResponses)
			{
				if (actualResponse.StartsWith(expectedResponse)) return;
			}
			throw new UnexpectedResponseMatchException(actualResponse, expectedResponses);
		}
		protected static void CheckResponseStart(string actualResponse, params string[] expectedResponses)
		{
			foreach (string expectedResponse in expectedResponses)
			{
				if (actualResponse.StartsWith(expectedResponse)) return;
			}
			throw new UnexpectedResponseStartException(actualResponse, expectedResponses);
		}
		public abstract class UnexpectedResponseException : Exception
		{
			internal UnexpectedResponseException(string message) : base(message) { }
		}
		public class UnexpectedResponseStartException : UnexpectedResponseException
		{
			internal UnexpectedResponseStartException(string actualResponse, params string[] expectedResponses)
				: base(String.Format("Expected response start: '{0}'\r\nActual response: '{1}'", String.Join("' or '", expectedResponses), actualResponse))
			{
			}
		}
		public class UnexpectedResponseMatchException : UnexpectedResponseException
		{
			internal UnexpectedResponseMatchException(string actualResponse, params string[] expectedResponses)
				: base(String.Format("Expected response: '{0}'\r\nActual response: '{1}'", String.Join("' or '", expectedResponses), actualResponse))
			{
			}
		}
	}
}
