using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using UnitTestUtilities;
using Bobs;
using Bobs.Tagging;
using SpottedLibrary.Controls.PagedRepeater;
using SpottedLibrary.Controls.PhotoBrowserControl;
using Common;
namespace SpottedLibrary.Controls.SearchBoxControl
{
	[TestFixture]
	public class PhotoPagedDataProviderTests : DatabaseRollbackTestClass
	{
		[Test]
		public void TestThatNoDataIsReturnedWhenThereIsNoData()
		{
			string tag = Guid.NewGuid().ToString();
			SearchQuery sq = SearchQuery.Parse(tag);
			Assert.AreEqual(0, ((IPagedDataService<Photo>)sq).Count);
		}
	 

	}
}
