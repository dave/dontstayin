using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Security;

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
using Bobs.Jobs;


namespace Bobs
{

	#region SalesCampaign
	/// <summary>
	/// Sales efforts in a particular demographic
	/// </summary>
	[Serializable]
	public partial class SalesCampaign
	{

		#region Simple members
		/// <summary>
		/// K
		/// </summary>
		public override int K
		{
			get { return this[SalesCampaign.Columns.K] as int? ?? 0; }
			set { this[SalesCampaign.Columns.K] = value; }
		}
		/// <summary>
		/// User that added the sales campaign
		/// </summary>
		public override int UsrK
		{
			get { return (int)this[SalesCampaign.Columns.UsrK]; }
			set { this[SalesCampaign.Columns.UsrK] = value; }
		}
		/// <summary>
		/// Name to identify this sales campaign
		/// </summary>
		public override string Name
		{
			get { return (string)this[SalesCampaign.Columns.Name]; }
			set { this[SalesCampaign.Columns.Name] = value; }
		}
		/// <summary>
		/// Description
		/// </summary>
		public override string Description
		{
			get { return (string)this[SalesCampaign.Columns.Description]; }
			set { this[SalesCampaign.Columns.Description] = value; }
		}
		/// <summary>
		/// Approximate start date - used for ordering and relative duration
		/// </summary>
		public override DateTime DateStart
		{
			get { return (DateTime)this[SalesCampaign.Columns.DateStart]; }
			set { this[SalesCampaign.Columns.DateStart] = value; }
		}
		/// <summary>
		/// Approximate end date - used for ordering and relative duration
		/// </summary>
		public override DateTime DateEnd
		{
			get { return (DateTime)this[SalesCampaign.Columns.DateEnd]; }
			set { this[SalesCampaign.Columns.DateEnd] = value; }
		}
		#endregion

		public double TotalRevenue
		{
			get
			{
				Query q = new Query();
				q.QueryCondition = new Q(Promoter.Columns.SalesCampaignK, this.K);
				q.TableElement = new Join(Invoice.Columns.K, Promoter.Columns.K);
				q.ExtraSelectElements.Add("sum", "SUM([Invoice].[Price])");
				q.Columns = new ColumnSet();
				InvoiceSet iss = new InvoiceSet(q);
				try
				{
					return (double)iss[0].ExtraSelectElements["sum"];
				}
				catch
				{
					return 0.0;
				}
			}
		}
		public int NumberOfPromoters
		{
			get
			{
				Query q = new Query();
				q.QueryCondition = new Q(Promoter.Columns.SalesCampaignK, this.K);
				q.ReturnCountOnly = true;
				PromoterSet ps = new PromoterSet(q);
				return ps.Count;
			}
		}

	}
	#endregion
}
