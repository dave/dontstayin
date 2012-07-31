using System;
using System.Collections.Generic;
using System.Text;

namespace Bobs
{
	#region ClubDetails
	/// <summary>
	/// Club details harvested from Yellow pages
	/// </summary>
	[Serializable] 
	public partial class ClubDetails
	{
		#region Simple members
		/// <summary>
		/// Key
		/// </summary>
		public override int K
		{
			get { return this[ClubDetails.Columns.K] as int? ?? 0; }
			set { this[ClubDetails.Columns.K] = value; }
		}
		/// <summary>
		/// Company
		/// </summary>
		public override string Company
		{
			get { return (string)this[ClubDetails.Columns.Company]; }
			set { this[ClubDetails.Columns.Company] = value; }
		}
		/// <summary>
		/// WebLink
		/// </summary>
		public override string WebLink
		{
			get { return (string)this[ClubDetails.Columns.WebLink]; }
			set { this[ClubDetails.Columns.WebLink] = value; }
		}
		/// <summary>
		/// Telephone
		/// </summary>
		public override string Telephone
		{
			get { return (string)this[ClubDetails.Columns.Telephone]; }
			set { this[ClubDetails.Columns.Telephone] = value; }
		}
		/// <summary>
		/// Address
		/// </summary>
		public override string Address
		{
			get { return (string)this[ClubDetails.Columns.Address]; }
			set { this[ClubDetails.Columns.Address] = value; }
		}
		/// <summary>
		/// PostCode
		/// </summary>
		public override string PostCode
		{
			get { return (string)this[ClubDetails.Columns.PostCode]; }
			set { this[ClubDetails.Columns.PostCode] = value; }
		}
		/// <summary>
		/// ExtraInfo
		/// </summary>
		public override string ExtraInfo
		{
			get { return (string)this[ClubDetails.Columns.ExtraInfo]; }
			set { this[ClubDetails.Columns.ExtraInfo] = value; }
		}
		/// <summary>
		/// PromoterK
		/// </summary>
		public override int PromoterK
		{
			get { return (int)this[ClubDetails.Columns.PromoterK]; }
			set { this[ClubDetails.Columns.PromoterK] = value; }
		}
		/// <summary>
		/// VenueK
		/// </summary>
		public override int VenueK
		{
			get { return (int)this[ClubDetails.Columns.VenueK]; }
			set { this[ClubDetails.Columns.VenueK] = value; }
		}
		/// <summary>
		/// DoneDate
		/// </summary>
		public override DateTime DoneDate
		{
			get { return (DateTime)this[ClubDetails.Columns.DoneDate]; }
			set { this[ClubDetails.Columns.DoneDate] = value; }
		}
		/// <summary>
		/// Dead
		/// </summary>
		public override int Dead
		{
			get { return (int)this[ClubDetails.Columns.Dead]; }
			set { this[ClubDetails.Columns.Dead] = value; }
		}
		#endregion
	}
	#endregion
}
