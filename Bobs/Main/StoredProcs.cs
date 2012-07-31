using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlTypes;

namespace Bobs
{
	#region stored procedures
	public static partial class StoredProcedures
	{
		public static partial class BannerServer
		{
			public static partial class Banner
			{
				public static partial class RecalculateTrafficLevelRelativeToMinuteOfDay
				{
					public static void Execute(DateTime? useWeekStartingFromDate)
					{
						ExecuteNonQuery(useWeekStartingFromDate);
					}
				 
				}
			}
		}
		public static partial class Bobs
		{
			public static partial class BannerStat
			{
				public static partial class Log
				{
					public static void Execute(int bannerK, DateTime? date, int hits, int uniqueVisitors, int clicks)
					{
						ExecuteNonQuery(bannerK, date, hits, uniqueVisitors, clicks);
					}
				 
				}
			}
			public static partial class Flyer
			{
				public static partial class LogView
				{
					public static void Execute(int flyerK)
					{
						ExecuteNonQuery(flyerK);
					}

				}
				public static partial class LogClick
				{
					public static void Execute(int flyerK)
					{
						ExecuteNonQuery(flyerK);
					}

				}
				public static partial class LogSend
				{
					public static void Execute(int flyerK)
					{
						ExecuteNonQuery(flyerK);
					}

				}
				public static partial class LogUnsubscribe
				{
					public static void Execute(int flyerK)
					{
						ExecuteNonQuery(flyerK);
					}

				}
			}
		}
		public static partial class Spatial
		{
			public static partial class Search
			{
				#region Generic methods
				private static T[] ExecuteSearch<T>(TablesEnum tableEnum, string whereClause, string customSql, OrderBy orderBy, int firstRowIndex, int lastRowIndex, double north, double east, double south, double west, Func<DataRow, T> func)
				{
					DataTable dt = ExecuteDataTable(Tables.GetTableName(tableEnum),
						whereClause, customSql ?? "",
						orderBy != null ? orderBy.ToString(true) : null,
						firstRowIndex, lastRowIndex, north, east, south, west);
					if (dt.Rows.Count == 0)
					{
						return new T[0];
					}

					T[] t = new T[dt.Rows.Count];
					for (int i = 0; i < t.Length; i++)
					{
						t[i] = func(dt.Rows[i]);
					}
					return t;
				}
				static T InitBob<T>(DataRow dr) where T : IBob, new()
				{
					T t = new T();
					t.Bob.Initialise(dr);
					return t;
				}
				#endregion

				public static Place[] PlacesByPopulationDesc(int firstRowIndex, int lastRowIndex, double north, double east, double south, double west)
				{
					return ExecuteSearch(TablesEnum.Place, null, null, new OrderBy(Place.Columns.Population, OrderBy.OrderDirection.Descending), firstRowIndex, lastRowIndex, north, east, south, west, dr => InitBob<Place>(dr));
				}
				public static Place[] PlacesByNameAsc(int firstRowIndex, int lastRowIndex, double north, double east, double south, double west)
				{
					return ExecuteSearch(TablesEnum.Place, null, null, new OrderBy(Place.Columns.Name, OrderBy.OrderDirection.Ascending), firstRowIndex, lastRowIndex, north, east, south, west, dr => InitBob<Place>(dr));
				}

				public static Thread[] RecentThreadsByLastPostDesc(int firstRowIndex, int lastRowIndex, double north, double east, double south, double west)
				{
					return ExecuteSearch(TablesEnum.Thread,
						Tables.GetColumnName(Thread.Columns.LastPost) + " > DATEADD(day, - 180, GETDATE())", null, 
						new OrderBy(Thread.Columns.LastPost, OrderBy.OrderDirection.Descending),
						firstRowIndex, lastRowIndex, north, east, south, west, dr => InitBob<Thread>(dr));
				}
				public static Event[] EventsByDateThenName(int firstRowIndex, int lastRowIndex, double north, double east, double south, double west, DateTime? startDate, DateTime? endDate, int musicTypeK, bool orderDesc)
				{
					return ExecuteSearch(TablesEnum.Event,
						(startDate == null ? " 1 = 1 " : (Tables.GetColumnName(Event.Columns.DateTime) + " >= @StartDate ")) 
						+ " AND " + 
						(endDate == null ? " 1 = 1 " : (Tables.GetColumnName(Event.Columns.DateTime) + " < @EndDate "))
						,
						String.Format("DECLARE @StartDate DATETIME SET @StartDate = '{0}' DECLARE @EndDate DATETIME SET @EndDate = '{1}'", (startDate ?? SqlDateTime.MinValue.Value).ToString("dd MMM yyyy"), (endDate ?? SqlDateTime.MaxValue.Value).ToString("dd MMM yyyy")),
						new OrderBy(new OrderBy(Event.Columns.DateTime, orderDesc ? OrderBy.OrderDirection.Descending : OrderBy.OrderDirection.Ascending), new OrderBy(Event.Columns.Name, OrderBy.OrderDirection.Ascending)),
						firstRowIndex, lastRowIndex, north, east, south, west, dr => InitBob<Event>(dr));
				}
				public static Venue[] VenuesByName(int firstRowIndex, int lastRowIndex, double north, double east, double south, double west)
				{
					return ExecuteSearch(TablesEnum.Venue, null, null, new OrderBy(Venue.Columns.Name, OrderBy.OrderDirection.Ascending), firstRowIndex, lastRowIndex, north, east, south, west, dr => InitBob<Venue>(dr));
				}
				public static Venue[] VenuesByCapacityDesc(int firstRowIndex, int lastRowIndex, double north, double east, double south, double west)
				{
					return ExecuteSearch(TablesEnum.Venue, null, null, new OrderBy(Venue.Columns.Capacity, OrderBy.OrderDirection.Descending), firstRowIndex, lastRowIndex, north, east, south, west, dr => InitBob<Venue>(dr));
				}
				public static Venue[] VenuesByTotalEventsDesc(int firstRowIndex, int lastRowIndex, double north, double east, double south, double west)
				{
					return ExecuteSearch(TablesEnum.Venue, null, null, new OrderBy(Venue.Columns.TotalEvents, OrderBy.OrderDirection.Descending), firstRowIndex, lastRowIndex, north, east, south, west, dr => InitBob<Venue>(dr));
				}

				public static Gallery[] GallerysByLastLiveDateTime(int firstRowIndex, int lastRowIndex, double north, double east, double south, double west)
				{
					return ExecuteSearch(TablesEnum.Gallery, 
						Tables.GetColumnName(Gallery.Columns.EventK) + " > 0 ", null,
						new OrderBy(Gallery.Columns.LastLiveDateTime, OrderBy.OrderDirection.Descending),
						firstRowIndex, lastRowIndex, north, east, south, west, dr => InitBob<Gallery>(dr));
				}
				public static Article[] ArticlesByAddedDateTimeDesc(int firstRowIndex, int lastRowIndex, double north, double east, double south, double west)
				{
					return ExecuteSearch(TablesEnum.Article, null, null, new OrderBy(Article.Columns.AddedDateTime, OrderBy.OrderDirection.Descending), firstRowIndex, lastRowIndex, north, east, south, west, dr => InitBob<Article>(dr));
				}
			}
		}
	}
	
	#endregion
 
}
