using System;

namespace Bobs
{
	/// <summary>
	/// Hits per day stats for the domain table
	/// </summary>
	[Serializable] 
	public partial class DomainStats
	{

		#region Simple members
		/// <summary>
		/// Link to the domain table
		/// </summary>
		public override int DomainK
		{
			get { return (int)this[DomainStats.Columns.DomainK]; }
			set { this[DomainStats.Columns.DomainK] = value; }
		}
		/// <summary>
		/// Date for the stats
		/// </summary>
		public override DateTime Date
		{
			get { return (DateTime)this[DomainStats.Columns.Date]; }
			set { this[DomainStats.Columns.Date] = value; }
		}
		/// <summary>
		/// Number of visitors in this day
		/// </summary>
		public override int Hits
		{
			get { return (int)this[DomainStats.Columns.Hits]; }
			set { this[DomainStats.Columns.Hits] = value; }
		}
		#endregion

	}
}
