using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using UnitTestUtilities.Web;
namespace SpottedTests.Pages
{
    [TestFixture]
    public class Home_Tests
    {
		const string TextThatShouldBeOnThisPage = "Welcome to DontStayIn";
		#region Tests
		[Test]
		public void CheckThatPageLoads()
		{
			Uri uri = SpottedTests.Site.Uri;
			int maxTries = 3;
			int thisTry = 0;
			bool success = false;
			try
			{
				while (thisTry < maxTries && !success)
				{
					WebRequest request = new WebRequest(uri, 120*1000);
					success = request.Response.Contains(TextThatShouldBeOnThisPage);
					thisTry++;
				}
				Assert.IsTrue(success, "Could not access " + uri.ToString());
			}
			catch (Exception ex)
			{
				Assert.Fail("uri: " + uri.ToString() + "\nMessage: " + ex.Message);
			}
		}
		#endregion
    }
}

