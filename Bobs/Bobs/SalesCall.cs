using System;
using System.Collections.Generic;
using System.Text;

namespace Bobs
{
	#region SalesCall
	/// <summary>
	/// Sales phone call made / received by a sales person
	/// </summary>
	[Serializable] 
	public partial class SalesCall
	{

		#region Simple members
		/// <summary>
		/// Key
		/// </summary>
		public override int K
		{
			get { return this[SalesCall.Columns.K] as int? ?? 0; }
			set { this[SalesCall.Columns.K] = value; }
		}
		/// <summary>
		/// Duplicate guid
		/// </summary>
		public override Guid DuplicateGuid
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[SalesCall.Columns.DuplicateGuid]); }
			set { this[SalesCall.Columns.DuplicateGuid] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// The sales person
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[SalesCall.Columns.UsrK]; }
			set { usr = null; this[SalesCall.Columns.UsrK] = value; }
		}
		/// <summary>
		/// The promoter
		/// </summary>
		public override int PromoterK
		{
			get { return (int)this[SalesCall.Columns.PromoterK]; }
			set { promoter = null; this[SalesCall.Columns.PromoterK] = value; }
		}
		/// <summary>
		/// Start of the call
		/// </summary>
		public override DateTime DateTimeStart
		{
			get { return (DateTime)this[SalesCall.Columns.DateTimeStart]; }
			set { this[SalesCall.Columns.DateTimeStart] = value; }
		}
		/// <summary>
		/// End of the call
		/// </summary>
		public override DateTime DateTimeEnd
		{
			get { return (DateTime)this[SalesCall.Columns.DateTimeEnd]; }
			set { this[SalesCall.Columns.DateTimeEnd] = value; }
		}
		/// <summary>
		/// Duration in minutes
		/// </summary>
		public override double Duration
		{
			get { return (double)this[SalesCall.Columns.Duration]; }
			set { this[SalesCall.Columns.Duration] = value; }
		}
		/// <summary>
		/// Is the call still in progress?
		/// </summary>
		public override bool InProgress
		{
			get { return (bool)this[SalesCall.Columns.InProgress]; }
			set { this[SalesCall.Columns.InProgress] = value; }
		}
		/// <summary>
		/// 1 = Outgoing, 2 = Incoming
		/// </summary>
		public override Directions Direction
		{
			get { return (Directions)this[SalesCall.Columns.Direction]; }
			set { this[SalesCall.Columns.Direction] = value; }
		}
		/// <summary>
		/// 1 = Cold, 2 = ProactiveFollowUp, 3 = Active
		/// </summary>
		public override Types Type
		{
			get { return (Types)this[SalesCall.Columns.Type]; }
			set { this[SalesCall.Columns.Type] = value; }
		}
		/// <summary>
		/// Has the call got through to the right person?
		/// </summary>
		public override bool Effective
		{
			get { return (bool)this[SalesCall.Columns.Effective]; }
			set { this[SalesCall.Columns.Effective] = value; }
		}
		/// <summary>
		/// Is this a call or just a note?
		/// </summary>
		public override bool IsCall
		{
			get { return (bool)this[SalesCall.Columns.IsCall]; }
			set { this[SalesCall.Columns.IsCall] = value; }
		}
		/// <summary>
		/// Text note added by the sales person
		/// </summary>
		public override string Note
		{
			get { return (string)this[SalesCall.Columns.Note]; }
			set { this[SalesCall.Columns.Note] = value; }
		}
		/// <summary>
		/// Has the "hang up" button been clicked?
		/// </summary>
		public override bool Dismissed
		{
			get { return (bool)this[SalesCall.Columns.Dismissed]; }
			set { this[SalesCall.Columns.Dismissed] = value; }
		}
		/// <summary>
		/// Flag for important notes
		/// </summary>
		public override bool IsImportant
		{
			get { return (bool)this[SalesCall.Columns.IsImportant]; }
			set { this[SalesCall.Columns.IsImportant] = value; }
		}
		/// <summary>
		/// Is this a call to a promoter that was recently added to the site by the sales user?
		/// </summary>
		public override bool IsCallToNewLead
		{
			get { return (bool)this[SalesCall.Columns.IsCallToNewLead]; }
			set { this[SalesCall.Columns.IsCallToNewLead] = value; }
		}
		#endregion

		#region Usr
		public Usr Usr
		{
			get
			{
				if (usr == null && UsrK > 0)
					usr = new Usr(UsrK, this, SalesCall.Columns.UsrK);
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
					promoter = new Promoter(PromoterK, this, SalesCall.Columns.PromoterK);
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

		#region Directions
		#endregion

		public void EffectiveAction()
		{
			if (Promoter.EffectiveSalesStatus.Equals(Promoter.SalesStatusEnum.Idle) || Promoter.EffectiveSalesStatus.Equals(Promoter.SalesStatusEnum.New))
			{
				if (Promoter.EffectiveSalesStatus.Equals(Promoter.SalesStatusEnum.New))
					Promoter.ActivateRefresh();

//				Promoter.SalesUsrK = Usr.Current.K;
				Promoter.SalesStatus = Promoter.SalesStatusEnum.Proactive;
				Promoter.SalesStatusExpires = DateTime.Now.AddDays(30);
				Promoter.AssignSalesUsrAndUpdate(Usr.Current.K);
//				Promoter.Update();

				//SalesStatusChange ssc = new SalesStatusChange();
				//ssc.DuplicateGuid = Guid.NewGuid();
				//ssc.UsrK = Usr.Current.K;
				//ssc.PromoterK = PromoterK;
				//ssc.DateTime = DateTime.Now;
				//ssc.Type = SalesStatusChange.Types.NewProactiveClient;
				//ssc.Update();

			}
			else if (Promoter.EffectiveSalesStatus.Equals(Promoter.SalesStatusEnum.Proactive) || Promoter.EffectiveSalesStatus.Equals(Promoter.SalesStatusEnum.Active))
			{
				DateTime newDate = DateTime.Now.AddDays(30);
				if (newDate > Promoter.SalesStatusExpires)
				{
					Promoter.SalesStatusExpires = newDate;
					Promoter.Update();
				}
			}
		}

	}
	#endregion
}
