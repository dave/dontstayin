using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace System.Tests
{
	[TestFixture]
	public class StringTests
	{
		[Test]
		public void Test1()
		{
			List<string> listA = new List<string>() { "a", "b", "c", "c", "one" };
			List<string> listB = new List<string>() { "a", "b", "c", "c", "one", "" };

			Assert.IsFalse(listA.HasSameContents(listB));
		}
		[Test]
		public void Test2()
		{
			List<string> listA = new List<string>() { "a", "b", "c", "c", "one", "" };
			List<string> listB = new List<string>() { "a", "b", "c", "c", "one", "" };

			Assert.IsTrue(listA.HasSameContents(listB));
		}
		[Test]
		public void Test3()
		{
			List<string> listA = new List<string>() { "a", "b", "c", "c", "one" };
			List<string> listB = new List<string>() { "a", "b", "c", "one", "" };

			Assert.IsFalse(listA.HasSameContents(listB));
		}
		[Test]
		public void Test4()
		{
			List<string> listA = new List<string>() { "a", "b", "c", "c", "one", "" };
			List<string> listB = new List<string>() { "a", "b", "b", "c", "one", "" };

			Assert.IsFalse(listA.HasSameContents(listB));
		}
		[Test]
		public void Test5()
		{
			List<string> listA = new List<string>() { "a", "b", "one", "c", "c", "" };
			List<string> listB = new List<string>() { "b", "", "c", "a", "c", "one" };

			Assert.IsTrue(listA.HasSameContents(listB));
		}
		[Test]
		public void Test6()
		{
			List<string> listA = new List<string>() { "a", "b", "c", "c", "one", "" };
			List<string> listB = new List<string>() { "A", "B", "c", "c", "one", "" };

			Assert.IsFalse(listA.HasSameContents(listB));
		}

		[Test]
		public void TestRemoveExtraSpaces()
		{
			Assert.AreEqual("hello", "hello".RemoveExtraSpaces());
			Assert.AreEqual("hello", "    hello".RemoveExtraSpaces());
			Assert.AreEqual("hello", "hello    ".RemoveExtraSpaces());
			Assert.AreEqual("hello", "      hello    ".RemoveExtraSpaces());
			Assert.AreEqual("hello world", "hello world".RemoveExtraSpaces());
			Assert.AreEqual("hello world", "    hello world".RemoveExtraSpaces());
			Assert.AreEqual("hello world", "hello       world".RemoveExtraSpaces());
			Assert.AreEqual("hello world", "hello world    ".RemoveExtraSpaces());
			Assert.AreEqual("hello world", "   hello     world".RemoveExtraSpaces());
			Assert.AreEqual("hello world", "    hello world    ".RemoveExtraSpaces());
			Assert.AreEqual("hello world", "     hello    world    ".RemoveExtraSpaces());
		}
	}
}
