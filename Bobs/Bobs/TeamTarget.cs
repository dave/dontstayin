using System;
using System.Collections.Generic;
using System.Text;

namespace Bobs
{
	#region TeamTarget
	/// <summary>
	/// Monthly team targets used to calculate team bonus
	/// </summary>
	[Serializable] 
	public partial class TeamTarget 
	{

		#region Simple members
		/// <summary>
		/// Year
		/// </summary>
		public override int Year
		{
			get { return (int)this[TeamTarget.Columns.Year]; }
			set { this[TeamTarget.Columns.Year] = value; }
		}
		/// <summary>
		/// Month
		/// </summary>
		public override int Month
		{
			get { return (int)this[TeamTarget.Columns.Month]; }
			set { this[TeamTarget.Columns.Month] = value; }
		}
		/// <summary>
		/// Target that the sales people are aiming for
		/// </summary>
		public override double Target
		{
			get { return (double)this[TeamTarget.Columns.Target]; }
			set { this[TeamTarget.Columns.Target] = value; }
		}
		/// <summary>
		/// Actual that the sales people got (null until the 15th)
		/// </summary>
		public override double Actual
		{
			get { return (double)this[TeamTarget.Columns.Actual]; }
			set { this[TeamTarget.Columns.Actual] = value; }
		}
		#endregion

	}
	#endregion
}
