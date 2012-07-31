using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using System.Transactions;
namespace UnitTestUtilities
{
	[TestFixture]
	public class DatabaseRollbackTestClass
	{
		protected TransactionScope scope;
		[SetUp]
		public void SetUp()
		{
			scope = new TransactionScope(TransactionScopeOption.RequiresNew, new TimeSpan(0, 10, 0));
		}
		[TearDown]
		public void TearDown()
		{
			scope.Dispose();
		}
	}
}
