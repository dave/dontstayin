using System;
using System.Collections.Generic;
using System.Text;

namespace Bobs
{
	#region TrafficExceptionDay
	/// <summary>
	/// Days which we do not expect to follow usual traffic patterns, and the day to use instead
	/// </summary>
	public partial class TrafficExceptionDay 
	{

		#region Simple members
		/// <summary>
		/// Date which is expected to not follow regular traffic patterns from week to week, e.g. days around a 
		/// </summary>
		public override DateTime ExceptionDate
		{
			get { return (DateTime)this[TrafficExceptionDay.Columns.ExceptionDate]; }
			set { this[TrafficExceptionDay.Columns.ExceptionDate] = value; }
		}
		/// <summary>
		/// The date of a day whose traffic levels should be used instead, e.g. the previous Sunday if we expect
		/// </summary>
		public override DateTime DateToUseInstead
		{
			get { return (DateTime)this[TrafficExceptionDay.Columns.DateToUseInstead]; }
			set { this[TrafficExceptionDay.Columns.DateToUseInstead] = value; }
		}
		#endregion

	}
	#endregion
}
