using System;
using DomainNameRegistrar.com.wildwestdomains.api;
using NUnit.Framework;

namespace DomainNameRegistrar.Test
{
	[TestFixture]
	public class LiveTest
	{
		[Test]
		public void TestAvailability()
		{
			WAPI wapi = new WAPI();
			Credential credentials = new Credential()
			{
				Account = "dontstayin",
				Password = "Blind78bat"
			};

			string cltrid = Guid.NewGuid().ToString();
			string response = wapi.CheckAvailability(cltrid, credentials, new string[] { "google.com" }, null, null);
		}
	}
}
