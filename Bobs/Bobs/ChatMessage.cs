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
using System.Xml;
using SpottedScript.Controls.ChatClient.Shared;

namespace Bobs
{

	#region ChatMessage
	[Serializable] 
	public partial class ChatMessage
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[ChatMessage.Columns.K] as int? ?? 0; }
			set { this[ChatMessage.Columns.K] = value; }
		}
		/// <summary>
		/// Text of the message
		/// </summary>
		public override string Text
		{
			get { return (string)this[ChatMessage.Columns.Text]; }
			set { this[ChatMessage.Columns.Text] = value; }
		}
		/// <summary>
		/// Date/time that the message was sent
		/// </summary>
		public override DateTime DateTime
		{
			get { return (DateTime)this[ChatMessage.Columns.DateTime]; }
			set { this[ChatMessage.Columns.DateTime] = value; }
		}
		/// <summary>
		/// UsrK of the posting user
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[ChatMessage.Columns.UsrK]; }
			set { this[ChatMessage.Columns.UsrK] = value; }
		}
		/// <summary>
		/// Room posted to
		/// </summary>
		public override Guid RoomGuid
		{
			get { return Cambro.Misc.Db.GuidConvertor(this[ChatMessage.Columns.RoomGuid]); }
			set { this[ChatMessage.Columns.RoomGuid] = new System.Data.SqlTypes.SqlGuid(value); }
		}
		/// <summary>
		/// The guid of the main chat item (for the archive)
		/// </summary>
		public override Guid? ChatItemGuid
		{
			get { return (Guid?)this[ChatMessage.Columns.ChatItemGuid]; }
			set { this[ChatMessage.Columns.ChatItemGuid] = value; }
		}
		/// <summary>
		/// Has this item been deleted from the archive?
		/// </summary>
		public override bool? Deleted
		{
			get { return (bool?)this[ChatMessage.Columns.Deleted]; }
			set { this[ChatMessage.Columns.Deleted] = value; }
		}
		#endregion

		#region Links to Bobs
		public Usr FromUsr
		{
			get
			{
				if (fromUsr == null)
				{
					fromUsr = new Usr(UsrK);
				}
				return fromUsr;
			}
		}
		Usr fromUsr;
		#endregion

	}
	#endregion

}
