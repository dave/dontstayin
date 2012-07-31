using System;
using System.Collections.Generic;
using System.Text;

namespace Bobs
{
	#region SalesStatusChange
	/// <summary>
	/// Logs changes in sales status, required for reports
	/// </summary>
	[Serializable] 
	public partial class SalesStatusChange
	{

		#region Simple members
		/// <summary>
		/// Key
		/// </summary>
		public override int K
		{
			get { return this[SalesStatusChange.Columns.K] as int? ?? 0; }
			set { this[SalesStatusChange.Columns.K] = value; }
		}
		/// <summary>
		/// Duplicate guid
		/// </summary>
		public override Guid DuplicateGuid
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[SalesStatusChange.Columns.DuplicateGuid]); }
			set { this[SalesStatusChange.Columns.DuplicateGuid] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// The sales person
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[SalesStatusChange.Columns.UsrK]; }
			set { usr = null; this[SalesStatusChange.Columns.UsrK] = value; }
		}
		/// <summary>
		/// The promoter
		/// </summary>
		public override int PromoterK
		{
			get { return (int)this[SalesStatusChange.Columns.PromoterK]; }
			set { promoter = null; this[SalesStatusChange.Columns.PromoterK] = value; }
		}
		/// <summary>
		/// Date time of the activity
		/// </summary>
		public override DateTime DateTime
		{
			get { return (DateTime)this[SalesStatusChange.Columns.DateTime]; }
			set { this[SalesStatusChange.Columns.DateTime] = value; }
		}
		/// <summary>
		/// 1 = NewProactiveClient, 2 = NewActiveClient
		/// </summary>
		public override Types Type
		{
			get { return (Types)this[SalesStatusChange.Columns.Type]; }
			set { this[SalesStatusChange.Columns.Type] = value; }
		}
		#endregion

		#region Usr
		public Usr Usr
		{
			get
			{
				if (usr == null && UsrK > 0)
					usr = new Usr(UsrK, this, SalesStatusChange.Columns.UsrK);
				return usr;
			}
			set
			{
				usr = value;
			}
		}
		Usr usr;
		#endregion

		#region Promoter
		public Promoter Promoter
		{
			get
			{
				if (promoter == null && PromoterK > 0)
					promoter = new Promoter(PromoterK, this, SalesStatusChange.Columns.PromoterK);
				return promoter;
			}
			set
			{
				promoter = value;
			}
		}
		Promoter promoter;
		#endregion

		#region Types
		#endregion

	}
	#endregion
}
