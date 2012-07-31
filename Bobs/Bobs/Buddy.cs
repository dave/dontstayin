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

	#region Buddy
	/// <summary>
	/// A buddy
	/// </summary>
	[Serializable] 
	public partial class Buddy : IDeleteAll
	{
		readonly static string fetchSql = "SELECT TOP 1 * FROM [" + Tables.GetTableName(TablesEnum.Buddy) + "] WITH (NOLOCK) WHERE [" + Tables.GetColumnName(Buddy.Columns.UsrK) + "] = @UsrK AND [" + Tables.GetColumnName(Buddy.Columns.BuddyUsrK) + "] = @BuddyUsrK";
 
		public Buddy(int usrK, int buddyUsrK)
			: this()
		{

			using (SqlConnection conn = new SqlConnection(Common.Properties.ConnectionString)){
				using (SqlCommand cmd = new SqlCommand(fetchSql, conn))
				{
						
					cmd.Parameters.AddWithValue("@UsrK", usrK);
					cmd.Parameters.AddWithValue("@BuddyUsrK", buddyUsrK);
					using (var adapter = new SqlDataAdapter(cmd))
					{
						conn.Open();
						DataTable dt = new DataTable();
						adapter.Fill(dt);
						if (dt.Rows.Count == 0)
						{
							throw new BobNotFound();
						}
						else
						{
							Initialise(dt.Rows[0]);
						}
					}
				}
			}
		}
		public Buddy(IBob Parent, object Column)
			: this()
		{
			Bob.GetBobFromParentSimple(Parent, Column, TablesEnum.Buddy);
		}
		#region simple members
		public override int K
		{
			get { throw new NotImplementedException(); }
			set { throw new NotImplementedException(); }
		}
		/// <summary>
		/// The user that added the buddy
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[Buddy.Columns.UsrK]; }
			set { usr = null; this[Buddy.Columns.UsrK] = value; }
		}
		/// <summary>
		/// The buddy
		/// </summary>
		public override int BuddyUsrK
		{
			get { return (int)this[Buddy.Columns.BuddyUsrK]; }
			set { buddyUsr = null; this[Buddy.Columns.BuddyUsrK] = value; }
		}
		/// <summary>
		/// Has the buddy added this user to his buddy list?
		/// </summary>
		public override bool FullBuddy
		{
			get { return (bool)this[Buddy.Columns.FullBuddy]; }
			set { this[Buddy.Columns.FullBuddy] = value; }
		}
		/// <summary>
		/// Has the user asked not to be alerted by pop-up from this buddy? If so, this is set to the data/time that the request was made. For 15 mins pop-up alerts will not be sent.
		/// </summary>
		public override DateTime LastPopupHoldOff
		{
			get { return (DateTime)this[Buddy.Columns.LastPopupHoldOff]; }
			set { this[Buddy.Columns.LastPopupHoldOff] = value; }
		}
		/// <summary>
		/// Can Buddy invite BuddyUsr to threads?
		/// </summary>
		public override bool CanBuddyInvite
		{
			get { return (bool)this[Buddy.Columns.CanBuddyInvite]; }
			set { this[Buddy.Columns.CanBuddyInvite] = value; }
		}
		/// <summary>
		/// Can BuddyUsr invite Buddy to threads?
		/// </summary>
		public override bool CanInvite
		{
			get { return (bool)this[Buddy.Columns.CanInvite]; }
			set { this[Buddy.Columns.CanInvite] = value; }
		}
		/// <summary>
		/// Has this buddy request been denied?
		/// </summary>
		public override bool Denied
		{
			get { return (bool)this[Buddy.Columns.Denied]; }
			set { this[Buddy.Columns.Denied] = value; }
		}
		/// <summary>
		/// 0 = Nickname, 1 = Real Name, 2 = Email Address, 3 = Spotter Code
		/// </summary>
		public override BuddyFindingMethod BuddyFoundByMethod
		{
			get { return (BuddyFindingMethod)this[Buddy.Columns.BuddyFoundByMethod]; }
			set { this[Buddy.Columns.BuddyFoundByMethod] = value; }
		}

		/// <summary>
		/// If this is a buddy invite for a skeleton user, then a nickname is required
		/// </summary>
		public override string SkeletonName
		{
			get { return (string)this[Buddy.Columns.SkeletonName]; }
			set { this[Buddy.Columns.SkeletonName] = value; }
		}
		#endregion

		#region BuddyFindingMethod
		#endregion

		#region JoinedBuddy
		public Buddy JoinedBuddy
		{
			get
			{
				if (joinedBuddy==null)
				{
					joinedBuddy = new Buddy(this, Buddy.Columns.BuddyUsrK);
				}
				return joinedBuddy;
			}
			set
			{
				joinedBuddy = value;
			}
		}
		private Buddy joinedBuddy;
		#endregion

		public void DeleteAll(Transaction transaction)
		{
			if (!this.Bob.DbRecordExists)
				return;

			this.Delete(transaction);
			this.Usr.UpdateBuddyCount(transaction);
			this.BuddyUsr.UpdateBuddyCount(transaction);
		}
		
		

		#region Links to Bobs
		#region Usr
		public Usr Usr
		{
			get
			{
				if (usr==null)
				{
					usr = new Usr(UsrK, this, Buddy.Columns.UsrK);
				}
				return usr;
			}
			set
			{
				usr = value;
			}
		}
		Usr usr;
		#endregion
		#region BuddyUsr
		public Usr BuddyUsr
		{
			get {
				if (this.Bob.BobSet != null)
				{
					return buddyUsr ?? (buddyUsr = new Usr(BuddyUsrK, this, Buddy.Columns.BuddyUsrK));
				}
				else
				{
					return buddyUsr ?? (buddyUsr = new Usr(this.BuddyUsrK));
				}
			}
			set { buddyUsr = value; }
		}
		Usr buddyUsr;
		#endregion
		#endregion



	}
	#endregion

}
