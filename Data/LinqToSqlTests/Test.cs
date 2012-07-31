using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Transactions;
using Autofac.Builder;
using DataInterface;
using Model;
using LinqToSql.Classes;
using NUnit.Framework;


namespace LinqToSqlTests
{
	[TestFixture]
	public class Test
	{
	    #region SetUp / TearDown
	    [SetUp]
	    public void SetUp()
	    {
	        this.ts = new TransactionScope(TransactionScopeOption.RequiresNew);
	        this.dc = new DbSpottedDataContext("server=anoth;database=db_spotted_test;trusted_connection=true");
	    }

	    [TearDown]
	    public void TearDown()
	    {
	        this.dc.Dispose();
	        this.ts.Dispose();
	    }
	    #endregion

		private DbSpottedDataContext dc;
	    private TransactionScope ts;

		[Test]
		public void PrefetchAttempt()
		{
 
			//var themes = (from t in dc.Themes
			//              orderby t.Order
			//              select new
			//                        {
			//                            Name = t.Name,
			//                            Description = t.Description,
			//                            Groups = (t.Groups).Take(5)
			//                        });
			//var x = themes.ToArray();
 
		}

		//    [Test]
	//    public void CheckThatJoinIsDoneInSqlNotInMemory()
	//    {
	//        var q = from v in dc.Venues
	//                join e in dc.Events on v.K equals e.VenueK
	//                select new { VenueName = v.Name, EventName = e.Name };
	//        Assert.AreEqual("SELECT [t0].[Name] AS [VenueName], [t1].[Name] AS [EventName]\r\nFROM [dbo].[Venue] AS [t0]\r\nINNER JOIN [dbo].[Event] AS [t1] ON [t0].[K] = [t1].[VenueK]\r\n", q.ToString());
	//    }
	//    [Test]
	//    public void TestThatDeletesAreOK()
	//    {
	//        Assert.IsTrue(dc.Venues.Any());
	//        this.DeleteAllVenues(dc);
	//        Assert.IsFalse(dc.Venues.Any());
	//    }

	//    private void DeleteAllVenues(IDsiDataContext dc)
	//    {
	//        foreach (var venue in dc.Venues)
	//        {
	//            dc.Venues.DeleteOnSubmit(venue);
	//        }
	//        dc.Submit();
	//    }

	//    [Test]
	//    public void CheckThatInsertsAreOK()
	//    {
	//        DeleteAllVenues(dc);
	//        Assert.IsFalse(dc.Venues.Any());
	//        IVenue venue = new Venue();
	//        dc.Venues.InsertOnSubmit(venue);
	//        dc.Submit();
	//        Assert.IsTrue(dc.Venues.Any());
	//    }
	//    [Test]
	//    public void CheckThatFunctionsCanBeCalledInline()
	//    {
	//        var q = from v in dc.Venues
	//                from pair in dc.FHtmCoverCircleLatLon(1, 1, 1, 1)
	//                where v.HtmId >= pair.HtmIDStart && v.HtmId <= pair.HtmIDEnd
	//                select new {v.K};
	//        Assert.AreEqual(
	//            "SELECT [t0].[K]\r\nFROM [dbo].[Venue] AS [t0]\r\nCROSS JOIN [dbo].[fHtmCoverCircleLatLon](@p0, @p1, @p2) AS [t1]\r\nWHERE ([t0].[HtmId] >= [t1].[HtmIDStart]) AND ([t0].[HtmId] <= [t1].[HtmIDEnd])\r\n",
	//            q.ToString());
	//    }

	//    [Test]
	//    public void CallScalarFunctionInline()
	//    {
	//        var q = from v in dc.Venues
	//                where dc.FDistanceLatLon(v.Lat, v.Lon, v.Lat, v.Lon) == v.Lon
	//                select v.K;

	//        foreach (var v in q)
	//        {
	//            // balh
	//        }
	//        Assert.AreEqual(
	//            "SELECT [t0].[K]\r\nFROM [dbo].[Venue] AS [t0]\r\nCROSS JOIN [dbo].[fHtmCoverCircleLatLon](@p0, @p1, @p2) AS [t1]\r\nWHERE (([t0].[HtmId]) >= [t1].[HtmIDStart]) AND (([t0].[HtmId]) <= [t1].[HtmIDEnd])\r\n",
	//            q.ToString());
	//    }

	//    [Test]
	//    public void CheckThatScalarValueFunctionsCanBeCalledInline()
	//    {
	//        var q = from v in dc.Venues
	//                //from radius in 
	//                from pair in dc.FHtmCoverCircleLatLon(v.Lat, v.Lon, v.Lat, v.Lon)
	//                where v.HtmId >= pair.HtmIDStart && v.HtmId <= pair.HtmIDEnd
	//                select new { v.K };

	//        q.ToArray();
	//        Assert.AreEqual("SELECT [t0].[K]\r\nFROM [dbo].[Venue] AS [t0]\r\nCROSS JOIN [dbo].[fHtmCoverCircleLatLon](@p0, @p1, @p2) AS [t1]\r\nWHERE (([t0].[HtmId]) >= [t1].[HtmIDStart]) AND (([t0].[HtmId]) <= [t1].[HtmIDEnd])\r\n",
	//            q.ToString());
	//    }


	//    [Test]
	//    public void Stuff()
	//    {
	//        IDsiDataContext db = new DbSpottedDataContext(Common.Properties.ConnectionString);
	//        var q = from v in db.Venues
	//                where v.Lat == db.FDistanceLatLon(v.Lat, v.Lat, v.Lat, v.Lat)
	//                select v.K;
	//        q.ToArray();

	    
	}

}
