using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Common.General;
namespace CommonTests.General
{
	[TestFixture]
	public class ConstructDisposeTests
	{
		[Test]
		public void TestConstructDispose()
		{
			bool b = false;
			using (new ConstructDispose( () => b = !b,  () => b = !b))
			{
				Assert.IsTrue(b);
			}
			Assert.IsFalse(b);
		}
		
	}
}
