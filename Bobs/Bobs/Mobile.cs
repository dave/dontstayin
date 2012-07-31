using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Collections;
using Cambro;
using Cambro.Web;
using Cambro.Misc;

using System.Net;
using System.IO;
using System.Text;
using System.Net.Sockets;

using System.Configuration;
using System.Diagnostics;
using System.ComponentModel;

namespace Bobs
{

	#region Mobile
	[Serializable] 
	public partial class Mobile
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[Mobile.Columns.K] as int? ?? 0; }
			set { this[Mobile.Columns.K] = value; }
		}
		/// <summary>
		/// Link to the Usr table if this mobile number has been linked to a user.
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[Mobile.Columns.UsrK]; }
			set { usr = null; this[Mobile.Columns.UsrK] = value; }
		}
		/// <summary>
		/// The mobile number, incluting the 2 digit country code, and excluding the leading zero.
		/// </summary>
		public override string Number
		{
			get { return (string)this[Mobile.Columns.Number]; }
			set { this[Mobile.Columns.Number] = value; }
		}
		/// <summary>
		/// The 2-digit int network id - 10=O2, 15=Vodaphone, 30=T-Mobile/Virgin, 33=Orange
		/// </summary>
		public override int Network
		{
			get { return (int)this[Mobile.Columns.Network]; }
			set { this[Mobile.Columns.Network] = value; }
		}
		/// <summary>
		/// Total number of incoming messages
		/// </summary>
		public override int TotalIncoming
		{
			get { return (int)this[Mobile.Columns.TotalIncoming]; }
			set { this[Mobile.Columns.TotalIncoming] = value; }
		}
		/// <summary>
		/// Total number of outgoing messages
		/// </summary>
		public override int TotalOutgoing
		{
			get { return (int)this[Mobile.Columns.TotalOutgoing]; }
			set { this[Mobile.Columns.TotalOutgoing] = value; }
		}
		/// <summary>
		/// DateTime that the forst incoming was received, and the account was created.
		/// </summary>
		public override DateTime DateTimeCreated
		{
			get { return (DateTime)this[Mobile.Columns.DateTimeCreated]; }
			set { this[Mobile.Columns.DateTimeCreated] = value; }
		}
		/// <summary>
		/// DateTime that the last message arrived
		/// </summary>
		public override DateTime DateTimeLastIncoming
		{
			get { return (DateTime)this[Mobile.Columns.DateTimeLastIncoming]; }
			set { this[Mobile.Columns.DateTimeLastIncoming] = value; }
		}
		#endregion

		public static Mobile GetByNumber(string number, int network)
		{
			Query q = new Query();
			q.QueryCondition = new Q(Mobile.Columns.Number, number);
			q.OrderBy = new OrderBy(Mobile.Columns.K);
			MobileSet ms = new MobileSet(q);
			if (ms.Count == 0)
			{
				Mobile m = new Mobile();
				m.Number = number;
				m.Network = network;
				m.Update();
				return m;
			}
			else
			{
				Mobile m = ms[0];
				if (m.Network != network && network != 0)
				{
					m.Network = network;
					m.Update();
				}
				return m;
			}
		}

		#region Bobs
//		#region Client
//		public GuestClient Client
//		{
//			get
//			{
//				if (client==null && GuestClientK>0)
//					client = new GuestClient(GuestClientK);
//				return client;
//			}
//			set
//			{
//				client = value;
//			}
//		}
//		private GuestClient client;
//		#endregion

		#endregion

		#region Usr
		public Usr Usr
		{
			get
			{
				if (usr==null && UsrK>0)
					usr = new Usr(UsrK);
				return usr;
			}
			set
			{
				usr = value;
			}
		}
		private Usr usr;
		#endregion

	}
	#endregion

}





