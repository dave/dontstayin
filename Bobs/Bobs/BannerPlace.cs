using System;

namespace Bobs
{
	/// <summary>
	/// Links a user to many music types (music I listen to)
	/// </summary>
	[Serializable]
	public partial class BannerPlace
	{

		#region simple members
		/// <summary>
		/// Link to Banner table
		/// </summary>
		public override int BannerK
		{
			get { return (int)this[BannerPlace.Columns.BannerK]; }
			set { this.banner = null; this[BannerPlace.Columns.BannerK] = value; }
		}
		/// <summary>
		/// Link to the Place table
		/// </summary>
		public override int PlaceK
		{
			get { return (int)this[BannerPlace.Columns.PlaceK]; }
			set { this.place = null; this[BannerPlace.Columns.PlaceK] = value; }
		}
		#endregion

		#region Links to Bobs
		#region Banner
		public Banner Banner
		{
			get
			{
				if (this.banner == null)
					this.banner = new Banner(this.BannerK);
				return this.banner;
			}
		}
		Banner banner;
		#endregion
		#region Place
		public Place Place
		{
			get
			{
				if (this.place == null)
					this.place = new Place(this.PlaceK);
				return this.place;
			}
		}
		Place place;
		#endregion
		#endregion

	}
}
