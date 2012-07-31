using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Common.General;
namespace CommonTests.General
{
	[TestFixture]
	public class LazyLoadTests
	{
		[Test]
		public void TestLazyLoad(){
			int i = 1;
			LazyLoad<int> t = new LazyLoad<int>(() => i);
			i = 2;
			Assert.AreEqual(2, t);
			i = 3;
			Assert.AreEqual(2, t);
			t.Reset();
			Assert.AreEqual(3, t);

		}
	}
}
