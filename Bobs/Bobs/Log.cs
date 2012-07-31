using System;
using System.Collections.Generic;
using System.Text;

namespace Bobs
{
	#region Log
	/// <summary>
	/// Logs simple counts by day
	/// </summary>
	[Serializable] 
	public partial class Log
	{

		#region Simple members
		/// <summary>
		/// The item - Log.Items
		/// </summary>
		public override Items ItemType
		{
			get { return (Items)this[Log.Columns.Item]; }
			set { this[Log.Columns.Item] = value; }
		}
		/// <summary>
		/// The date
		/// </summary>
		public override DateTime Date
		{
			get { return (DateTime)this[Log.Columns.Date]; }
			set { this[Log.Columns.Date] = value; }
		}
		/// <summary>
		/// The number of times this count happened
		/// </summary>
		public override int Count
		{
			get { return (int)this[Log.Columns.Count]; }
			set { this[Log.Columns.Count] = value; }
		}
		#endregion

		

		public Log(Items Item, DateTime Date)
			: this()
		{
			Bob.GetBobFromPrimaryKeyArray(new Q[] { new Q(Log.Columns.Item, Item), new Q(Log.Columns.Date, Date) });
		}

		 
			

		public static void Increment(Items item)
		{
			Log.Increment(item, 1, DateTime.Today);
		}

		public static void Increment(Items item, int count)
		{
			Log.Increment(item, count, DateTime.Today);
		}

		public static void Increment(Items item, int count, DateTime date)
		{
			bool exists = false;
			try
			{
				Log l = new Log(item, date);
				exists = true;
			}
			catch (BobNotFound)
			{
				try
				{
					Log l = new Log();
					l.ItemType = item;
					l.Date = date;
					l.Count = count;
					l.Update();
				}
				catch
				{
					exists = true;
				}
			}

			if (exists)
			{
				try
				{
					Update u = new Update();
					u.Table = TablesEnum.Log;
					u.Where = new And(
						new Q(Log.Columns.Item, item),
						new Q(Log.Columns.Date, date));

					if (count == 1)
						u.Changes.Add(new Assign.Increment(Log.Columns.Count));
					else
						u.Changes.Add(new Assign.Addition(Log.Columns.Count, count));

					u.Run();
				}
				catch
				{ }
			}
		}

		public static int GetCount(Items item, DateTime date)
		{
			return GetSum(item, date.Date, date.Date.AddDays(1));
		}

		public static int GetSum(Items item, DateTime dateTimeGreaterThanOrEqualTo, DateTime dateTimeLessThan)
		{
			Query q = new Query();
			q.Columns = new ColumnSet();
			q.ExtraSelectElements.Add("sum", "sum(Count)");
			q.QueryCondition = new And(
				new Q(Log.Columns.Date, QueryOperator.GreaterThanOrEqualTo, dateTimeGreaterThanOrEqualTo),
				new Q(Log.Columns.Date, QueryOperator.LessThan, dateTimeLessThan),
				new Q(Log.Columns.Item, item));
			LogSet ls = new LogSet(q);
			if (ls[0].ExtraSelectElements["sum"].Equals(System.DBNull.Value))
				return 0;
			else
				return (int)ls[0].ExtraSelectElements["sum"];
		}

	}
	#endregion
}
