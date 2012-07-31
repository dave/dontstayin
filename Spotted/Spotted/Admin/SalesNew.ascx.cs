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

namespace Spotted.Admin
{
	public partial class SalesNew : AdminUserControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				BindNewPromoters();
				BindCallBacks();
			}


		}

		Q SalesUsrQ
		{
			get
			{
				if (ContainerPage.Url[0].Equals("all"))
					return new Q(Promoter.Columns.SalesUsrK, QueryOperator.GreaterThan, 0);
				else
					return new Q(Promoter.Columns.SalesUsrK, Usr.Current.K);
			}
		}

		void BindNewPromoters()
		{
			if (true)
			{
				Query q = new Query();
				q.QueryCondition = new And(
					(ContainerPage.Url[0].Equals("all") ? new Q(Promoter.Columns.ClientSector, Promoter.ClientSectorEnum.Promoter) : new Q(true)),
					Promoter.EnabledQ,
					new Q(Promoter.Columns.SalesStatus, Promoter.SalesStatusEnum.New),
					SalesUsrQ,
					new Q(Promoter.Columns.SalesCallCount, 0),
					new Or(
						new Q(Promoter.Columns.SalesHold, false),
						new Q(Promoter.Columns.SalesHold, QueryOperator.IsNull, null)
					)
				);

				q.OrderBy = new OrderBy(Promoter.Columns.DateTimeSignUp, OrderBy.OrderDirection.Ascending);

				PromoterSet ps = new PromoterSet(q);

				NewPromoterDataGrid.AllowPaging = (ps.Count > NewPromoterDataGrid.PageSize);
				NewPromoterDataGrid.DataSource = ps;
				NewPromoterDataGrid.DataBind();

			}
		}
		void BindCallBacks()
		{
			if (true)
			{
				Query q = new Query();
				q.QueryCondition = new And(
					(ContainerPage.Url[0].Equals("all") ? new Q(Promoter.Columns.ClientSector, Promoter.ClientSectorEnum.Promoter) : new Q(true)),
					Promoter.EnabledQ,
					new Q(Promoter.Columns.SalesStatus, Promoter.SalesStatusEnum.New),
					SalesUsrQ,
					new Or(
						new Q(Promoter.Columns.SalesHold, false),
						new Q(Promoter.Columns.SalesHold, QueryOperator.IsNull, null)
					),
					new Q(Promoter.Columns.SalesNextCall, QueryOperator.LessThan, DateTime.Today.AddDays(1)),
					new Q(Promoter.Columns.SalesCallCount, QueryOperator.NotEqualTo, 0)
				);

				q.OrderBy = Promoter.NewIdleOrder;

				PromoterSet ps = new PromoterSet(q);

				CallBacksDataGrid.AllowPaging = (ps.Count > CallBacksDataGrid.PageSize);
				CallBacksDataGrid.DataSource = ps;
				CallBacksDataGrid.DataBind();
			}
		}
		public void CallBacksChangePage(object o, DataGridPageChangedEventArgs e)
		{
			CallBacksDataGrid.CurrentPageIndex = e.NewPageIndex;
			BindCallBacks();

		}
		public void NewChangePage(object o, DataGridPageChangedEventArgs e)
		{
			NewPromoterDataGrid.CurrentPageIndex = e.NewPageIndex;
			BindNewPromoters();
		}
		#region NewDataGridCommand
		protected void NewDataGridCommand(object sender, DataGridCommandEventArgs eventArgs)
		{
			RemovePromoter(eventArgs);
			BindNewPromoters();
		}
		#endregion
		#region CallBacksDataGridCommand
		protected void CallBacksDataGridCommand(object sender, DataGridCommandEventArgs eventArgs)
		{
			RemovePromoter(eventArgs);
			BindCallBacks();
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
	}
}
