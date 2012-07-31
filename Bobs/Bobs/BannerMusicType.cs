using System;

namespace Bobs
{
	/// <summary>
	/// Links a user to many music types (music I listen to)
	/// </summary>
	[Serializable]
	public partial class BannerMusicType 
	{

		#region simple members
		/// <summary>
		/// Link to Banner table
		/// </summary>
		public override int BannerK
		{
			get { return (int)this[BannerMusicType.Columns.BannerK]; }
			set { this.banner = null; this[BannerMusicType.Columns.BannerK] = value; }
		}
		/// <summary>
		/// Link to the MusicType table
		/// </summary>
		public override int MusicTypeK
		{
			get { return (int)this[BannerMusicType.Columns.MusicTypeK]; }
			set { this.musicType = null; this[BannerMusicType.Columns.MusicTypeK] = value; }
		}
		/// <summary>
		/// If the musicType was actually chosen by the promoter
		/// </summary>
		public override bool Chosen
		{
			get { return (bool)this[BannerMusicType.Columns.Chosen]; }
			set { this[BannerMusicType.Columns.Chosen] = value; }
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
		#region MusicType
		public MusicType MusicType
		{
			get
			{
				if (this.musicType == null)
					this.musicType = new MusicType(this.MusicTypeK);
				return this.musicType;
			}
		}
		MusicType musicType;
		#endregion
		#endregion

	}
}
