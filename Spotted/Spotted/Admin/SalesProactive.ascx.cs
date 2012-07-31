using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Bobs;

namespace Spotted.Admin
{
	public partial class SalesProactive : AdminUserControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				BindPromoters();
				BindExpired();
				SetupFilterDropDownLists();
			}
		}
		void BindPromoters()
		{
			List<Q> queryConditionList = new List<Q>();
			queryConditionList.Add(Promoter.EnabledQ);
			queryConditionList.Add(new Or(new Q(Promoter.Columns.SalesHold, false),
										  new Q(Promoter.Columns.SalesHold, QueryOperator.IsNull, null)));
			queryConditionList.Add(new Q(Promoter.Columns.SalesStatus, Promoter.SalesStatusEnum.Proactive));
			queryConditionList.Add(new Q(Promoter.Columns.SalesStatusExpires, QueryOperator.GreaterThan, DateTime.Now));
			queryConditionList.Add(new Q(Promoter.Columns.SalesUsrK, Usr.Current.K));

			if(this.SectorFilterDropDownList.SelectedValue != "")
				queryConditionList.Add(new Q(Promoter.Columns.ClientSector, Convert.ToInt32(SectorFilterDropDownList.SelectedValue)));
			if(this.SalesEstimateFilterDropDownList.SelectedValue != "")
				queryConditionList.Add(new Q(Promoter.Columns.SalesEstimate, Convert.ToInt32(SalesEstimateFilterDropDownList.SelectedValue)));

			Query q = new Query(new And(queryConditionList.ToArray()));

			q.OrderBy = new OrderBy(
				new OrderBy(Promoter.Columns.SalesNextCall),
				new OrderBy(Promoter.Columns.SalesEstimate, OrderBy.OrderDirection.Descending),
				new OrderBy(Promoter.Columns.DateTimeSignUp, OrderBy.OrderDirection.Descending));
			PromoterSet ps = new PromoterSet(q);

			PromoterDataGrid.AllowPaging = (ps.Count > PromoterDataGrid.PageSize);
			PromoterDataGrid.DataSource = ps;
			PromoterDataGrid.DataBind();
		}
		void BindExpired()
		{
			List<Q> queryConditionList = new List<Q>();
			queryConditionList.Add(Promoter.EnabledQ);
			queryConditionList.Add(new Or(new Q(Promoter.Columns.SalesHold, false),
										  new Q(Promoter.Columns.SalesHold, QueryOperator.IsNull, null)));
			queryConditionList.Add(new Q(Promoter.Columns.SalesStatusExpires, QueryOperator.LessThan, DateTime.Now));
			queryConditionList.Add(new Or(new Q(Promoter.Columns.SalesNextCall, QueryOperator.LessThan, DateTime.Now), 
										  new Q(Promoter.Columns.SalesNextCall, QueryOperator.IsNull, null)));
			queryConditionList.Add(new Q(Promoter.Columns.SalesUsrK, Usr.Current.K));
			
			if(this.SectorFilterDropDownList.SelectedValue != "")
				queryConditionList.Add(new Q(Promoter.Columns.ClientSector, Convert.ToInt32(SectorFilterDropDownList.SelectedValue)));
			if(this.SalesEstimateFilterDropDownList.SelectedValue != "")
				queryConditionList.Add(new Q(Promoter.Columns.SalesEstimate, Convert.ToInt32(SalesEstimateFilterDropDownList.SelectedValue)));

			Query q = new Query(new And(queryConditionList.ToArray()));

			q.OrderBy = new OrderBy(
				new OrderBy(Promoter.Columns.SalesNextCall, OrderBy.OrderDirection.Descending),
				new OrderBy(Promoter.Columns.SalesEstimate, OrderBy.OrderDirection.Descending),
				new OrderBy(Promoter.Columns.DateTimeSignUp, OrderBy.OrderDirection.Descending));
			PromoterSet ps = new PromoterSet(q);

			ExpiredDataGrid.AllowPaging = (ps.Count > ExpiredDataGrid.PageSize);
			ExpiredDataGrid.DataSource = ps;
			ExpiredDataGrid.DataBind();
		}

		private void SetupFilterDropDownLists()
		{
			this.SalesEstimateFilterDropDownList.Items.Clear();
			this.SalesEstimateFilterDropDownList.Items.Add(new ListItem("ALL", ""));
			this.SalesEstimateFilterDropDownList.Items.AddRange(Promoter.SalesEstimatesAsListItemArray());

			this.SectorFilterDropDownList.Items.Clear();
			this.SectorFilterDropDownList.Items.Add(new ListItem("ALL", ""));
            Utilities.AddEnumValuesToDropDownList(this.SectorFilterDropDownList, typeof(Promoter.ClientSectorEnum));
		}

		public void DataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			PromoterDataGrid.CurrentPageIndex = e.NewPageIndex;
			BindPromoters();
		}
		public void ExpiredDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			ExpiredDataGrid.CurrentPageIndex = e.NewPageIndex;
			BindExpired();
		}
		#region DataGridCommand
		protected void DataGridCommand(object sender, DataGridCommandEventArgs eventArgs)
		{
			RemovePromoter(eventArgs);
			BindPromoters();
		}
		#endregion
		#region ExpiredDataGridCommand
		protected void ExpiredDataGridCommand(object sender, DataGridCommandEventArgs eventArgs)
		{
			RemovePromoter(eventArgs);
			BindExpired();
		}
		#endregion
		public void RemovePromoter(DataGridCommandEventArgs eventArgs)
		{
			if (eventArgs.CommandName.Equals("Remove"))
			{
				Promoter p = new Promoter(int.Parse(eventArgs.CommandArgument.ToString()));
				p.SalesUsrK = 0;
				p.SalesStatusExpires = null;
				p.SalesStatus = Promoter.SalesStatusEnum.Idle;
				p.Update();
				p.FixQuestionsThreadUsrs();
			}
		}

		protected void SalesEstimateFilterDropDownList_SelectedIndexChanged(object sender, EventArgs e)
		{
			PromoterDataGrid.CurrentPageIndex = 0;
			ExpiredDataGrid.CurrentPageIndex = 0;
			BindPromoters();
			BindExpired();
		}

		protected void SectorFilterDropDownList_SelectedIndexChanged(object sender, EventArgs e)
		{
			PromoterDataGrid.CurrentPageIndex = 0;
			ExpiredDataGrid.CurrentPageIndex = 0;
			BindPromoters();
			BindExpired();
		}
	}
}
