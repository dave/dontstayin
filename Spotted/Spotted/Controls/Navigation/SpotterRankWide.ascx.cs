using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;

namespace Spotted.Controls.Navigation
{
	public partial class SpotterRankWide : System.Web.UI.UserControl
	{
		protected Repeater SpotterRankRepeater;
		private void Page_Load(object sender, System.EventArgs e)
		{
			Query q = new Query();
			q.Columns = new ColumnSet(
				Usr.Columns.K,
				Usr.Columns.SpottingsMonth,
				Usr.Columns.SpottingsMonthRank,
				Usr.LinkColumns
			);
			q.QueryCondition = new And(new Q(Usr.Columns.IsAdmin, false), new Q(Usr.Columns.K, QueryOperator.NotEqualTo, 2770));
			q.OrderBy = new OrderBy(new OrderBy(Usr.Columns.SpottingsMonth, OrderBy.OrderDirection.Descending), new OrderBy(Usr.Columns.K, OrderBy.OrderDirection.Ascending));
			q.TopRecords = 10;
			UsrSet us = new UsrSet(q);

			SpotterRankRepeater.ItemTemplate = this.LoadTemplate("/Templates/Usrs/SpotterRankWide.ascx");
			SpotterRankRepeater.DataSource = us;
			SpotterRankRepeater.DataBind();
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion
	}
}
