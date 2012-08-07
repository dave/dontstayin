using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using NUnit.Framework;

namespace UnitTestUtilities.Sql.Tests
{
	[TestFixture]
	public class SqlHelper_Tests
	{
		[Test]
		public void ResetSqlServerForTesting_RunsCleanly()
		{
			SqlHelper.ResetSqlServerForTesting(Common.Properties.ConnectionString);
		}
	}
}
