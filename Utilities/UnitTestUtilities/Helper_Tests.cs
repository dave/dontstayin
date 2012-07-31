using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace UnitTestUtilities.Tests
{
	[TestFixture]
	public class Helper_Tests
	{
		[Test]
		public void TestRunStaticMethod()
		{
			Assert.IsTrue((bool)Helper.RunStaticMethod(typeof(Helper_Tests), "PrivateStaticInvertBoolean", false));
			Assert.IsFalse((bool)Helper.RunStaticMethod(typeof(Helper_Tests), "PrivateStaticInvertBoolean", true));
		}

		private static bool PrivateStaticInvertBoolean(bool b)
		{
			return !b;
		}


		[Test]
		public void TestRunOverloadedMethod()
		{
			Assert.AreEqual("int", (string)Helper.RunStaticMethod(typeof(Helper_Tests), "OverloadedMethod", new System.Type[] { typeof(int) }, 3));
			Assert.AreEqual("int,string", (string)Helper.RunStaticMethod(typeof(Helper_Tests), "OverloadedMethod", new System.Type[] { typeof(int), typeof(string) }, 3, "blah"));
			Assert.AreEqual("params:string", (string)Helper.RunStaticMethod(typeof(Helper_Tests), "OverloadedMethod", new System.Type[] { typeof(string[]) }, new string[] { "blah" }));
			Assert.AreEqual("params:string", (string)Helper.RunStaticMethod(typeof(Helper_Tests), "OverloadedMethod", new System.Type[] { typeof(string[]) }, new string[] { "blah", "boo" }));

			Assert.AreEqual("int,params:string", (string)Helper.RunStaticMethod(typeof(Helper_Tests), "OverloadedMethod", new System.Type[] { typeof(int), typeof(string[]) }, 3, new string[] { "blah" }));
			Assert.AreEqual("int,params:string", (string)Helper.RunStaticMethod(typeof(Helper_Tests), "OverloadedMethod", new System.Type[] { typeof(int), typeof(string[]) }, 3, new string[] { "blah", "boo", "foo" }));
			
			// these are special cases yet to implement
			//Assert.AreEqual("int,params:string", (string)Helper.RunStaticMethod(typeof(HelperTests), "OverloadedMethod", new System.Type[] { typeof(int), typeof(string[])}, 3));
			//Assert.AreEqual("int,int,params:string", (string)Helper.RunStaticMethod(typeof(HelperTests), "OverloadedMethod", new System.Type[] { typeof(int), typeof(int), typeof(string[])}, 3, 4));

		}

		
		private static string OverloadedMethod(int i)
		{
			return "int";
		}
		private static string OverloadedMethod(int i, string s)
		{
			return "int,string";
		}
		private static string OverloadedMethod(params string[] s)
		{
			return "params:string";
		}
		private static string OverloadedMethod(int i, params string[] s)
		{
			return "int,params:string";
		}
		private static string OverloadedMethod(int i, int j, params string[] s)
		{
			return "int,int,params:string";
		}


	}
}
