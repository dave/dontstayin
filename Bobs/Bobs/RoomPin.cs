using System;

namespace Bobs
{
	#region RoomPin
	/// <summary>
	/// Chat rooms pinned by users
	/// </summary>
	[Serializable]
	public partial class RoomPin 
	{

		#region Simple members
		/// <summary>
		/// Usr
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[RoomPin.Columns.UsrK]; }
			set { this[RoomPin.Columns.UsrK] = value; }
		}
		/// <summary>
		/// Room
		/// </summary>
		public override Guid RoomGuid
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[RoomPin.Columns.RoomGuid]); }
			set { this[RoomPin.Columns.RoomGuid] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// Date/time the room was last pinned
		/// </summary>
		public override DateTime DateTime
		{
			get { return (DateTime)this[RoomPin.Columns.DateTime]; }
			set { this[RoomPin.Columns.DateTime] = value; }
		}
		/// <summary>
		/// Order in this users list
		/// </summary>
		public override int ListOrder
		{
			get { return (int)this[RoomPin.Columns.ListOrder]; }
			set { this[RoomPin.Columns.ListOrder] = value; }
		}
		/// <summary>
		/// Set to false if the room is un-pinned
		/// </summary>
		public override bool Pinned
		{
			get { return (bool)this[RoomPin.Columns.Pinned]; }
			set { this[RoomPin.Columns.Pinned] = value; }
		}
		/// <summary>
		/// True if the pinned room expires
		/// </summary>
		public override bool Expires
		{
			get { return (bool)this[RoomPin.Columns.Expires]; }
			set { this[RoomPin.Columns.Expires] = value; }
		}
		/// <summary>
		/// If the pinned room expires, this is the expiry time
		/// </summary>
		public override DateTime DateTimeExpires
		{
			get { return (DateTime)this[RoomPin.Columns.DateTimeExpires]; }
			set { this[RoomPin.Columns.DateTimeExpires] = value; }
		}
		/// <summary>
		/// Is the room starred?
		/// </summary>
		public override bool? Starred
		{
			get { return (bool?)this[RoomPin.Columns.Starred]; }
			set { this[RoomPin.Columns.Starred] = value; }
		}
		/// <summary>
		/// The persisted room state
		/// </summary>
		public override string StateStub
		{
			get { return (string)this[RoomPin.Columns.StateStub]; }
			set { this[RoomPin.Columns.StateStub] = value; }
		}
		#endregion

	}
	#endregion


}
