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

	#region Hit
	/// <summary>
	/// Stores information about a Hit - guid from the cookie etc.
	/// </summary>
	[Serializable] 
	public partial class Hit
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[Hit.Columns.K] as int? ?? 0; }
			set { this[Hit.Columns.K] = value; }
		}
		/// <summary>
		/// Which server did the request come in to
		/// </summary>
		public override int ServerId
		{
			get { return (int)this[Hit.Columns.ServerId]; }
			set { this[Hit.Columns.ServerId] = value; }
		}
		/// <summary>
		/// Date/time that the request was made
		/// </summary>
		public override DateTime StartTime
		{
			get { return (DateTime)this[Hit.Columns.StartTime]; }
			set { this[Hit.Columns.StartTime] = value; }
		}
		/// <summary>
		/// Has the request ended?
		/// </summary>
		public override bool HasEnded
		{
			get { return (bool)this[Hit.Columns.HasEnded]; }
			set { this[Hit.Columns.HasEnded] = value; }
		}
		/// <summary>
		/// Date/time that the request was completed (requests under 5s in length are deleted)
		/// </summary>
		public override DateTime EndTime
		{
			get { return (DateTime)this[Hit.Columns.EndTime]; }
			set { this[Hit.Columns.EndTime] = value; }
		}
		/// <summary>
		/// Get string
		/// </summary>
		public override string GetData
		{
			get { return (string)this[Hit.Columns.GetData]; }
			set { this[Hit.Columns.GetData] = value; }
		}
		/// <summary>
		/// Post data
		/// </summary>
		public override string PostData
		{
			get { return (string)this[Hit.Columns.PostData]; }
			set { this[Hit.Columns.PostData] = value; }
		}
		/// <summary>
		/// The usr that was logged in (if any)
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[Hit.Columns.UsrK]; }
			set { usr = null; this[Hit.Columns.UsrK] = value; }
		}
		/// <summary>
		/// Cookie data
		/// </summary>
		public override string CookieData
		{
			get { return (string)this[Hit.Columns.CookieData]; }
			set { this[Hit.Columns.CookieData] = value; }
		}
		#endregion

		#region Links to Bobs

		#region Usr
		public Usr Usr
		{
			get
			{
				if (usr==null && UsrK>0)
					usr = new Usr(UsrK, this, Hit.Columns.UsrK);
				return usr;
			}
		}
		Usr usr;
		#endregion
		
		#endregion

	}
	#endregion

}

