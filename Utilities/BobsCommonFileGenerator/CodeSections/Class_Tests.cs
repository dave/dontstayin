using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace BobsCommonFileGenerator.CodeSections.Tests
{
	[TestFixture]
	public class Class_Tests
	{

		[Test]
		public void GetClass_ClassIsAddedAndRetrievedByName_SameClassIsReturned()
		{
			Class c1 = new Class("Class1");
			Class c2 = new Class("Class2");
			Assert.IsNull(c1.GetClass("Class2"));
			c1.Add(c2);
			Assert.AreSame(c2, c1.GetClass("Class2"));
		}
	}
}
