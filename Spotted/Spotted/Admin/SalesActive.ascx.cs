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
	public partial class SalesActive : AdminUserControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				BindPromoters();
				BindExpiredPromoters();
				SetupFilterDropDownLists();
			}
		}

		private void SetupFilterDropDownLists()
		{
			this.SalesEstimateFilterDropDownList.Items.Clear();
			this.SalesEstimateFilterDropDownList.Items.Add(new ListItem("ALL", ""));
            Utilities.AddEnumValuesToDropDownList(this.SalesEstimateFilterDropDownList, typeof(Promoter.SalesEstimateEnum), false, false);

			this.SectorFilterDropDownList.Items.Clear();
			this.SectorFilterDropDownList.Items.Add(new ListItem("ALL", ""));
            Utilities.AddEnumValuesToDropDownList(this.SectorFilterDropDownList, typeof(Promoter.ClientSectorEnum));
		}

		void BindPromoters()
		{
			List<Q> queryConditionList = new List<Q>();
			//queryConditionList.Add(Promoter.EnabledQ);
			queryConditionList.Add(new Or(new Q(Promoter.Columns.SalesHold, false),
										  new Q(Promoter.Columns.SalesHold, QueryOperator.IsNull, null)));
			queryConditionList.Add(new Q(Promoter.Columns.SalesStatus, Promoter.SalesStatusEnum.Active));
			queryConditionList.Add(new Q(Promoter.Columns.SalesStatusExpires, QueryOperator.GreaterThan, DateTime.Now));
			queryConditionList.Add(new Q(Promoter.Columns.SalesUsrK, Usr.Current.K));

			if (this.SectorFilterDropDownList.SelectedValue != "")
				queryConditionList.Add(new Q(Promoter.Columns.ClientSector, Convert.ToInt32(SectorFilterDropDownList.SelectedValue)));
			if (this.SalesEstimateFilterDropDownList.SelectedValue != "")
				queryConditionList.Add(new Q(Promoter.Columns.SalesEstimate, Convert.ToInt32(SalesEstimateFilterDropDownList.SelectedValue)));

			Query q = new Query(new And(queryConditionList.ToArray()));

			q.OrderBy = new OrderBy(Promoter.Columns.SalesNextCall, OrderBy.OrderDirection.Ascending);
			PromoterSet ps = new PromoterSet(q);

			PromoterDataGrid.AllowPaging = (ps.Count > PromoterDataGrid.PageSize);
			PromoterDataGrid.DataSource = ps;
			PromoterDataGrid.DataBind();
		}

		void BindExpiredPromoters()
		{
			List<Q> queryConditionList = new List<Q>();
			//queryConditionList.Add(Promoter.EnabledQ);
			queryConditionList.Add(new Or(new Q(Promoter.Columns.SalesHold, false),
										  new Q(Promoter.Columns.SalesHold, QueryOperator.IsNull, null)));
			queryConditionList.Add(new Q(Promoter.Columns.SalesStatus, Promoter.SalesStatusEnum.Active));
			queryConditionList.Add(new Q(Promoter.Columns.SalesStatusExpires, QueryOperator.LessThanOrEqualTo, DateTime.Now));
			queryConditionList.Add(new Q(Promoter.Columns.SalesUsrK, Usr.Current.K));

			if (this.SectorFilterDropDownList.SelectedValue != "")
				queryConditionList.Add(new Q(Promoter.Columns.ClientSector, Convert.ToInt32(SectorFilterDropDownList.SelectedValue)));
			if (this.SalesEstimateFilterDropDownList.SelectedValue != "")
				queryConditionList.Add(new Q(Promoter.Columns.SalesEstimate, Convert.ToInt32(SalesEstimateFilterDropDownList.SelectedValue)));

			Query q = new Query(new And(queryConditionList.ToArray()));

			q.OrderBy = new OrderBy(Promoter.Columns.SalesNextCall, OrderBy.OrderDirection.Ascending);
			PromoterSet ps = new PromoterSet(q);

			ExpiredDataGrid.AllowPaging = (ps.Count > PromoterDataGrid.PageSize);
			ExpiredDataGrid.DataSource = ps;
			ExpiredDataGrid.DataBind();
		}
		public void DataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			PromoterDataGrid.CurrentPageIndex = e.NewPageIndex;
			BindPromoters();
		}
		public void ExpiredDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			ExpiredDataGrid.CurrentPageIndex = e.NewPageIndex;
			BindExpiredPromoters();
		}

		protected void SalesEstimateFilterDropDownList_SelectedIndexChanged(object sender, EventArgs e)
		{
			BindPromoters();
		}

		protected void SectorFilterDropDownList_SelectedIndexChanged(object sender, EventArgs e)
		{
			PromoterDataGrid.CurrentPageIndex = 1;
			BindPromoters();
		}
	}
}
