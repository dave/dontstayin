using System;

namespace Bobs
{
	/// <summary>
	/// Links a user to many usrs (usrs of me)
	/// </summary>
	[Serializable] 
	public partial class PromoterUsr
	{

		#region simple members
		/// <summary>
		/// Link to Promoter table
		/// </summary>
		public override int PromoterK
		{
			get { return (int)this[PromoterUsr.Columns.PromoterK]; }
			set { this.promoter = null; this[PromoterUsr.Columns.PromoterK] = value; }
		}
		/// <summary>
		/// Link to the Usr table
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[PromoterUsr.Columns.UsrK]; }
			set { this.usr = null; this[PromoterUsr.Columns.UsrK] = value; }
		}
		#endregion

		#region Links to Bobs
		#region Promoter
		public Promoter Promoter
		{
			get
			{
				if (this.promoter==null && this.PromoterK>0)
					this.promoter = new Promoter(this.PromoterK, this, PromoterUsr.Columns.PromoterK);
				return this.promoter;
			}
		}
		Promoter promoter;
		#endregion
		#region Usr
		public Usr Usr
		{
			get
			{
				if (this.usr==null && this.UsrK>0)
					this.usr = new Usr(this.UsrK);
				return this.usr;
			}
		}
		Usr usr;
		#endregion
		#endregion
		
	}
}
