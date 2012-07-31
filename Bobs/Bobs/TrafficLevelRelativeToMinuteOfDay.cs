using System;
using System.Collections.Generic;
using System.Text;

namespace Bobs
{
	#region TrafficLevelRelativeToMinuteOfDay
	/// <summary>
	/// Store of current analysed data calculating traffic levels at each minute of the day
	/// </summary>
	public partial class TrafficLevelRelativeToMinuteOfDay 
	{

		#region Simple members
		/// <summary>
		/// Minute since midnight
		/// </summary>
		public override int Minute
		{
			get { return (int)this[TrafficLevelRelativeToMinuteOfDay.Columns.Minute]; }
			set { this[TrafficLevelRelativeToMinuteOfDay.Columns.Minute] = value; }
		}
		/// <summary>
		/// A representative level of traffic at this minute
		/// </summary>
		public override int TrafficLevel
		{
			get { return (int)this[TrafficLevelRelativeToMinuteOfDay.Columns.TrafficLevel]; }
			set { this[TrafficLevelRelativeToMinuteOfDay.Columns.TrafficLevel] = value; }
		}
		#endregion

	}
	#endregion
}
