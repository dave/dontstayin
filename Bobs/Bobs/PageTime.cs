using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Collections;
using Cambro;
using Cambro.Web;
using Cambro.Misc;

using System.Net;
using System.IO;
using System.Text;
using System.Net.Sockets;

using System.Configuration;
using System.Diagnostics;
using System.ComponentModel;

namespace Bobs
{

	#region PageTime
	/// <summary>
	/// Rating of a photo
	/// </summary>
	[Serializable] 
	public partial class PageTime
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[PageTime.Columns.K] as int? ?? 0; }
			set { this[PageTime.Columns.K] = value; }
		}
		/// <summary>
		/// Link to the Cob table - the page in question
		/// </summary>
		public override int CobK
		{
			get { return (int)this[PageTime.Columns.CobK]; }
			set { this[PageTime.Columns.CobK] = value; }
		}
		/// <summary>
		/// Date when these hits took place
		/// </summary>
		public override DateTime Date
		{
			get { return (DateTime)this[PageTime.Columns.Date]; }
			set { this[PageTime.Columns.Date] = value; }
		}
		/// <summary>
		/// Total number of impressions that this page got during the day
		/// </summary>
		public override int Impressions
		{
			get { return (int)this[PageTime.Columns.Impressions]; }
			set { this[PageTime.Columns.Impressions] = value; }
		}
		/// <summary>
		/// Total time taken to serve this page (used to work out the average page generation time)
		/// </summary>
		public override int TotalTime
		{
			get { return (int)this[PageTime.Columns.TotalTime]; }
			set { this[PageTime.Columns.TotalTime] = value; }
		}
		/// <summary>
		/// The maximum time taken to generate a page during the day
		/// </summary>
		public override int MaxTime
		{
			get { return (int)this[PageTime.Columns.MaxTime]; }
			set { this[PageTime.Columns.MaxTime] = value; }
		}
		/// <summary>
		/// The minimum time taken to generate a page during the day
		/// </summary>
		public override int MinTime
		{
			get { return (int)this[PageTime.Columns.MinTime]; }
			set { this[PageTime.Columns.MinTime] = value; }
		}
		/// <summary>
		/// The full URL when the maximum time was recorded
		/// </summary>
		public override string MaxUrl
		{
			get { return (string)this[PageTime.Columns.MaxUrl]; }
			set { this[PageTime.Columns.MaxUrl] = value; }
		}
		/// <summary>
		/// The full URL when the minimum time was recorded
		/// </summary>
		public override string MinUrl
		{
			get { return (string)this[PageTime.Columns.MinUrl]; }
			set { this[PageTime.Columns.MinUrl] = value; }
		}
		/// <summary>
		/// Name of cust page
		/// </summary>
		public override string CustPage
		{
			get { return (string)this[PageTime.Columns.CustPage]; }
			set { this[PageTime.Columns.CustPage] = value; }
		}
		/// <summary>
		/// Log item
		/// </summary>
		public override LogItems LogItem
		{
			get { return (LogItems)this[PageTime.Columns.LogItem]; }
			set { this[PageTime.Columns.LogItem] = value; }
		}

		#endregion


		public string LogString
		{
			get
			{
				if (LogItem==0)
					return "";
				else
					return LogItem.ToString();
			}
		}



		public static void IncrementEvent(LogItems logItem)
		{
			IncrementEvent(logItem, 1);
		}
		public static void IncrementEvent(LogItems logItem, int count)
		{
			Query q = new Query();
			q.QueryCondition=new And(
				new Q(PageTime.Columns.LogItem,logItem),
				new Q(PageTime.Columns.Date,DateTime.Today)
			);
			q.Columns=new ColumnSet(PageTime.Columns.K,PageTime.Columns.Impressions);
			q.NoLock=true;
			Bobs.PageTimeSet pts = new PageTimeSet(q);
			if (pts.Count>0)
			{
				Cambro.Misc.Db.Qu("UPDATE PageTime SET Impressions=Impressions+" + count.ToString() + " WHERE K="+pts[0].K.ToString());
			}
			else
			{
				try
				{
					Bobs.PageTime pt = new Bobs.PageTime();
					pt.Date = DateTime.Now.Date;
					pt.LogItem = logItem;
					pt.Impressions = 1;
					pt.Update();
				}
				catch { }
			}
		}

	}
	#endregion

}

