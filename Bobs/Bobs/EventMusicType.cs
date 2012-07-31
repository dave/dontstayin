using System;

namespace Bobs
{
	/// <summary>
	/// Links an event to many MusicTypes - the Music types that will be played
	/// </summary>
	[Serializable] 
	public partial class EventMusicType
	{

		#region simple members
		/// <summary>
		/// Link to Event table
		/// </summary>
		public override int EventK
		{
			get { return (int)this[EventMusicType.Columns.EventK]; }
			set { this._event = null; this[EventMusicType.Columns.EventK] = value; }
		}
		/// <summary>
		/// Link to the MusicType table
		/// </summary>
		public override int MusicTypeK
		{
			get { return (int)this[EventMusicType.Columns.MusicTypeK]; }
			set { this.musicType = null; this[EventMusicType.Columns.MusicTypeK] = value; }
		}
		#endregion

		#region Links to Bobs
		#region Event
		public Event Event
		{
			get
			{
				if (this._event==null)
					this._event = new Event(this.EventK);
				return this._event;
			}
		}
		Event _event;
		#endregion
		#region MusicType
		public MusicType MusicType
		{
			get
			{
				if (this.musicType==null)
					this.musicType = new MusicType(this.MusicTypeK);
				return this.musicType;
			}
		}
		MusicType musicType;
		#endregion
		#endregion
		
	}
}
