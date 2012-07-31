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
using System.Collections.Generic;

namespace Bobs
{

	#region Abuse
	/// <summary>
	/// Reports of abuse
	/// </summary>
	[Serializable] 
	public partial class Abuse
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[Abuse.Columns.K] as int? ?? 0; }
			set { this[Abuse.Columns.K] = value; }
		}
		/// <summary>
		/// The usr reporting the abuse
		/// </summary>
		public override int ReportUsrK
		{
			get { return (int)this[Abuse.Columns.ReportUsrK]; }
			set { reportUsr = null; this[Abuse.Columns.ReportUsrK] = value; }
		}
		/// <summary>
		/// The user doing the abuse
		/// </summary>
		public override int AbuseUsrK
		{
			get { return (int)this[Abuse.Columns.AbuseUsrK]; }
			set { abuseUsr = null; this[Abuse.Columns.AbuseUsrK] = value; }
		}
		/// <summary>
		/// The type of the target object
		/// </summary>
		public override Model.Entities.ObjectType ObjectType
		{
			get { return (Model.Entities.ObjectType)this[Abuse.Columns.ObjectType]; }
			set { triedObject = false; _object = null; this[Abuse.Columns.ObjectType] = value; }
		}
		/// <summary>
		/// The K of the target object
		/// </summary>
		public override int ObjectK
		{
			get { return (int)this[Abuse.Columns.ObjectK]; }
			set { triedObject = false; _object = null; this[Abuse.Columns.ObjectK] = value; }
		}
		/// <summary>
		/// String reprasentation of the taget object - for use after the target has been deleted.
		/// </summary>
		public override string ObjectString
		{
			get { return (string)this[Abuse.Columns.ObjectString]; }
			set { this[Abuse.Columns.ObjectString] = value; }
		}
		/// <summary>
		/// The text of the abuse
		/// </summary>
		public override string ReportDescription
		{
			get { return (string)this[Abuse.Columns.ReportDescription]; }
			set { this[Abuse.Columns.ReportDescription] = value; }
		}
		/// <summary>
		/// DateTime that the report was made
		/// </summary>
		public override DateTime ReportDateTime
		{
			get { return (DateTime)this[Abuse.Columns.ReportDateTime]; }
			set { this[Abuse.Columns.ReportDateTime] = value; }
		}
		/// <summary>
		/// Photo align type - 1=Left, 2=Right, 3=Center
		/// </summary>
		public override StatusEnum Status
		{
			get { return (StatusEnum)this[Abuse.Columns.Status]; }
			set { this[Abuse.Columns.Status] = value; }
		}
		/// <summary>
		/// When was this report resolved?
		/// </summary>
		public override DateTime ResolveDateTime
		{
			get { return (DateTime)this[Abuse.Columns.ResolveDateTime]; }
			set { this[Abuse.Columns.ResolveDateTime] = value; }
		}
		/// <summary>
		/// What was the decision of the moderator?
		/// </summary>
		public override ResolveStatusEnum ResolveStatus
		{
			get { return (ResolveStatusEnum)this[Abuse.Columns.ResolveStatus]; }
			set { this[Abuse.Columns.ResolveStatus] = value; }
		}
		/// <summary>
		/// Description of the resolution (if needed)
		/// </summary>
		public override string ResolveDescription
		{
			get { return (string)this[Abuse.Columns.ResolveDescription]; }
			set { this[Abuse.Columns.ResolveDescription] = value; }
		}
		/// <summary>
		/// The moderator that resolved the report
		/// </summary>
		public override int ResolveUsrK
		{
			get { return (int)this[Abuse.Columns.ResolveUsrK]; }
			set { resolveUsr = null; this[Abuse.Columns.ResolveUsrK] = value; }
		}
		#endregion

		#region StatusEnum
		#endregion
		#region ResolveStatusEnum
		#endregion

		#region ReportUsr
		public Usr ReportUsr
		{
			get
			{
				if (reportUsr == null && ReportUsrK > 0)
				{
					reportUsr = new Usr(ReportUsrK, this, Abuse.Columns.ReportUsrK);
				}
				return reportUsr;
			}
			set
			{
				reportUsr = value;
			}
		}
		private Usr reportUsr;
		#endregion

		#region AbuseUsr
		public Usr AbuseUsr
		{
			get
			{
				if (abuseUsr == null && AbuseUsrK > 0)
				{
					abuseUsr = new Usr(AbuseUsrK, this, Abuse.Columns.AbuseUsrK);
				}
				return abuseUsr;
			}
			set
			{
				abuseUsr = value;
			}
		}
		private Usr abuseUsr;
		#endregion

		#region ResolveUsr
		public Usr ResolveUsr
		{
			get
			{
				if (resolveUsr == null && ResolveUsrK > 0)
				{
					resolveUsr = new Usr(ResolveUsrK, this, Abuse.Columns.ResolveUsrK);
				}
				return resolveUsr;
			}
			set
			{
				resolveUsr = value;
			}
		}
		private Usr resolveUsr;
		#endregion

		#region Object
		public IBob Object
		{
			get
			{
				if (_object == null && ObjectK > 0 && !triedObject)
				{
					triedObject = true;
					_object = Bob.Get(ObjectType, ObjectK);
				}
				return _object;
			}
			set
			{
				triedObject = false;
				_object = value;
			}
		}
		private bool triedObject = false;
		private IBob _object;
		#endregion

	}
	#endregion

}
