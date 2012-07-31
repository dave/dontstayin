using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using UnitTestUtilities;
namespace SpottedLibrary.Controls.SearchBoxControl.Tests
{
	[TestFixture]
	public class SearchQueryTests : DatabaseRollbackTestClass
	{
		[Test]
		public void VariousSearchQueryTests()
		{
			SearchQuery items = SearchQuery.Parse("Hello \"is it me you're looking\" -for?");
			var enumerator = items.TagsQueryParts.GetEnumerator();
			Assert.AreEqual(true, enumerator.MoveNext());
			Assert.AreEqual("hello", enumerator.Current);
			Assert.AreEqual(true, enumerator.MoveNext());
			Assert.AreEqual("is it me youre looking", enumerator.Current);
			Assert.AreEqual(true, enumerator.MoveNext());
			Assert.AreEqual("for", enumerator.Current);
			Assert.AreEqual(false,	enumerator.MoveNext());
						
		}
	
	}
}
