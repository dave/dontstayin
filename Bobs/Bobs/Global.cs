
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


	#region Global
	[Serializable] 
	public partial class Global 
	{

		#region simple members
		/// <summary>
		/// The primary key
		/// </summary>
		public override int K
		{
			get { return this[Global.Columns.K] as int? ?? 0; }
			set { this[Global.Columns.K] = value; }
		}
		/// <summary>
		/// Name
		/// </summary>
		public override string Name
		{
			get { return (string)this[Global.Columns.Name]; }
			set { this[Global.Columns.Name] = value; }
		}
		/// <summary>
		/// Description
		/// </summary>
		public override string Description
		{
			get { return (string)this[Global.Columns.Description]; }
			set { this[Global.Columns.Description] = value; }
		}
		/// <summary>
		/// Value (200 chars)
		/// </summary>
		public override string ValueString
		{
			get { return (string)this[Global.Columns.ValueString]; }
			set { this[Global.Columns.ValueString] = value; }
		}
		/// <summary>
		/// Value
		/// </summary>
		public override int ValueInt
		{
			get { return (int)this[Global.Columns.ValueInt]; }
			set { this[Global.Columns.ValueInt] = value; }
		}
		/// <summary>
		/// Value
		/// </summary>
		public override double ValueDouble
		{
			get { return (double)this[Global.Columns.ValueDouble]; }
			set { this[Global.Columns.ValueDouble] = value; }
		}
		/// <summary>
		/// Value
		/// </summary>
		public override DateTime ValueDateTime
		{
			get { return (DateTime)this[Global.Columns.ValueDateTime]; }
			set { this[Global.Columns.ValueDateTime] = value; }
		}
		/// <summary>
		/// Unlimited length string value
		/// </summary>
		public override string ValueText
		{
			get { return (string)this[Global.Columns.ValueText]; }
			set { this[Global.Columns.ValueText] = value; }
		}
		#endregion

		public Global(Global.Records Record)
			: this()
		{
			Bob.GetBobFromPrimaryKey((int)Record);
		}

		/// <summary>
		/// Use http://kruithof.xs4all.nl/uuid/uuidgen to generate a guid for the codeId
		/// </summary>
		/// <param name="codeId"></param>
		/// <param name="ex"></param>
		public static void Log(string codeId, Exception ex)
		{
		//	StackFrame CallStack = new StackFrame(1, true);

		//	string filename = CallStack.GetFileName().Substring(CallStack.GetFileName().LastIndexOf("\\")+1);
			
			//Mailer sm = new Mailer();
			//sm.Subject="Exception "+ex.Message;
			//sm.Body+="<h2>Exception detected</h2>";
			//sm.Body+="<p>Exception:</p>";
			//sm.Body+="<p>"+ex.ToString().Replace("\n","<br>")+"</p>";
			//sm.Body+="<p>Code ID:</p>";
			//sm.Body+="<p>"+codeId+"</p>";
			//sm.TemplateType=Mailer.TemplateTypes.AdminNote;
			//sm.To="dave@dont-stay-in.com";
			//sm.Send();

		}

		#region LogSqlQuery
		public static void LogSqlQuery(QueryTypes QueryType) { LogSqlQuery(QueryType, 1); }
		public static void LogSqlQuery(QueryTypes QueryType, int Count)
		{
			if (HttpContext.Current != null)
			{
				string item = "";
				if (QueryType.Equals(QueryTypes.Select))
					item = "DbQueriesSelect";
				else if (QueryType.Equals(QueryTypes.Update))
					item = "DbQueriesUpdate";
				else if (QueryType.Equals(QueryTypes.Insert))
					item = "DbQueriesInsert";
				else if (QueryType.Equals(QueryTypes.Delete))
					item = "DbQueriesDelete";
				else
					item = "DbQueries";


				if (HttpContext.Current.Items[item] == null)
					HttpContext.Current.Items[item] = 0;
				else
					HttpContext.Current.Items[item] = ((int)HttpContext.Current.Items[item]) + Count;
			}
		}
		#endregion

		public static void IncrementRequestCounter(RequestCounter pc) { IncrementRequestCounter(pc, 1); }
		public static void IncrementRequestCounter(RequestCounter pc, int count)
		{
			if (HttpContext.Current != null)
			{
				string key = "RequestCounter." + pc.ToString();
				if (HttpContext.Current.Items[key] == null)
				{
					HttpContext.Current.Items[key] = count;
				}
				else
				{
					HttpContext.Current.Items[key] = ((int)HttpContext.Current.Items[key]) + count;
				}
			}
		}
		
	
		public static void UpdatePhotoAbuseReports()
		{
			Query q = new Query();
			q.ReturnCountOnly=true;
			q.QueryCondition=new And(
				new Q(Abuse.Columns.ObjectType, Model.Entities.ObjectType.Photo),
				new Q(Abuse.Columns.Status,Abuse.StatusEnum.New)
				);
			AbuseSet abs = new AbuseSet(q);
			Global g = new Global(Global.Records.PendingPhotoAbuseReports);
			g.ValueInt=abs.Count;
			g.Update();
		}
		

		public static void SetMaxUsers(int number)
		{
			if (number > 500)
			{
				Global g = new Global(Global.Records.MaxUsers5Min);
				if (number > g.ValueInt)
				{
					g.ValueInt = number;
					g.ValueDateTime = DateTime.Now;
					g.Update();

					Query q30min = new Query();
					q30min.QueryCondition = Usr.LoggedIn30MinQ;
					q30min.NoLock = true;
					q30min.ReturnCountOnly = true;
					UsrSet us30min = new UsrSet(q30min);

					Global g1 = new Global(Global.Records.MaxUsers30Min);
					if (us30min.Count > g1.ValueInt)
					{
						g1.ValueInt = us30min.Count;
						g1.ValueDateTime = DateTime.Now;
						g1.Update();
					}
				}
			}
		}


	}
	#endregion

}
