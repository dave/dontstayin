using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using LinqToSql.Classes;
using Model;

namespace MapBrowserSprocsLoadTester.MapBrowser.SqlViewportListeners
{
	class RectTableLinqToSqlLoggerViewportListener : LinqToSqlLoggerViewportListener
	{
		private readonly string tableName;

		public RectTableLinqToSqlLoggerViewportListener(string tableName,
			IResultLogger<Viewport> logger, string connectionString)
			: base(logger, connectionString)
		{
			this.tableName = tableName;
			this.dc = new DbSpottedDataContext(this.connectionString);
		}

		private IDsiDataContext dc;

		protected override int GetResults(Viewport viewport)
		{
			switch (this.tableName)
			{
				case "Venue":
					{
						var q = from v in dc.Venues
								from htmIds in dc.FHtmCoverRect(viewport.South, viewport.West, viewport.North, viewport.East)
								where v.HtmId >= htmIds.HtmIDStart && v.HtmId <= htmIds.HtmIDEnd &&
									v.Lat >= viewport.South && v.Lat <= viewport.North &&
									v.Lon >= viewport.West && v.Lon <= viewport.East
								orderby v.K
								select new { v.K };

						using (
							new TransactionScope(TransactionScopeOption.Required,
												 new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
						{
							return q.Take(200).ToArray().Length;
						}
					}
				case "Event":
					{
						List<int> allHardDanceMusicTypeKs = new List<int> { 10, 11, 12, 13, 14, 45, 59, 60 };
						var q = from v in dc.Events
								from htmIds in dc.FHtmCoverRect(viewport.South, viewport.West, viewport.North, viewport.East)
								join m in dc.EventMusicTypes on v.K equals m.EventK
								where v.HtmId >= htmIds.HtmIDStart && v.HtmId <= htmIds.HtmIDEnd &&
									v.Lat >= viewport.South && v.Lat <= viewport.North &&
									v.Lon >= viewport.West && v.Lon <= viewport.East
									&& allHardDanceMusicTypeKs.Contains(m.MusicTypeK)
								orderby v.K
								select new { v.K };

						using (
							new TransactionScope(TransactionScopeOption.Required,
												 new TransactionOptions { IsolationLevel = IsolationLevel.ReadUncommitted }))
						{
							return q.Take(200).ToArray().Length;
						}
					}
				default:
					throw new NotImplementedException(this.tableName);
			}
		}
	}
}

