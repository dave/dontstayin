using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Autofac.Builder;
using Common;
using DataInterface;
using Model;
using Model.Entities;
using LinqToSql.Classes;
using NUnit.Framework;
using System.Threading;

namespace LinqToSqlTests
{
	[TestFixture]
	public class Test2
	{
		[Test]
		public void TestingNoLock()
		{
			var dc = new DbSpottedDataContext(Common.Properties.ConnectionString);
			
			var q = from v in dc.Venues
					join e in dc.Events on v.K equals e.VenueK
					select new { VenueName = v.Name, EventName = e.Name };

			q.ToArray();
		}
}


}
