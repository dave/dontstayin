using System;

namespace Bobs
{
	/// <summary>
	/// Links to many places that the user may like to visit.
	/// </summary>
	[Serializable]
	public partial class UsrPlaceVisit
	{

		#region simple members
		/// <summary>
		/// Link to Usr table
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[UsrPlaceVisit.Columns.UsrK]; }
			set { this.usr = null; this[UsrPlaceVisit.Columns.UsrK] = value; }
		}
		/// <summary>
		/// Link to the Place table - a place that this user visits
		/// </summary>
		public override int PlaceK
		{
			get { return (int)this[UsrPlaceVisit.Columns.PlaceK]; }
			set { this.place = null; this[UsrPlaceVisit.Columns.PlaceK] = value; }
		}
		#endregion

		#region Links to Bobs
		#region Usr
		public Usr Usr
		{
			get
			{
				if (this.usr == null)
					this.usr = new Usr(this.UsrK);
				return this.usr;
			}
		}
		Usr usr;
		#endregion
		#region Place
		public Place Place
		{
			get
			{
				if (this.place == null)
					this.place = new Place(this.PlaceK, this, UsrPlaceVisit.Columns.PlaceK);
				return this.place;
			}
		}
		Place place;
		#endregion
		#endregion

	}
}
