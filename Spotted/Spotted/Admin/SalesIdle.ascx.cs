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
	public partial class SalesIdle : AdminUserControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Prefs.Current["NewPromotersPage"].Exists && Prefs.Current["NewPromotersPage"] > 0)
					PromoterDataGrid.CurrentPageIndex = Prefs.Current["NewPromotersPage"];

				BindPromoters();
			}

			
			

		}
		void BindPromoters()
		{
			Query q = new Query();
			q.QueryCondition = new And(
				Promoter.EnabledQ,
				new Or(
					new Q(Promoter.Columns.SalesHold, false),
					new Q(Promoter.Columns.SalesHold, QueryOperator.IsNull, null)
				),
				new Or(
					new Q(Promoter.Columns.SalesNextCall, QueryOperator.LessThan, DateTime.Today.AddDays(1)),
					new Q(Promoter.Columns.SalesNextCall, QueryOperator.IsNull, null)
				),
				new Or(
					new Q(Promoter.Columns.SalesStatus, QueryOperator.IsNull, null),
					new Q(Promoter.Columns.SalesStatus, Promoter.SalesStatusEnum.New),
					new Q(Promoter.Columns.SalesStatus, Promoter.SalesStatusEnum.Idle),
					new Q(Promoter.Columns.SalesStatusExpires, QueryOperator.LessThan, DateTime.Now)
				),
				new NotQ(
					new And(
						new Q(Promoter.Columns.LetterType, Promoter.LetterTypes.AutoVenue),
						new Q(Promoter.Columns.LetterStatus, Promoter.LetterStatusEnum.New)
					)
				)
			);

			q.OrderBy = Promoter.NewIdleOrder;
			PromoterSet ps = new PromoterSet(q);

			PromoterDataGrid.AllowPaging = (ps.Count > PromoterDataGrid.PageSize);
			PromoterDataGrid.DataSource = ps;
			PromoterDataGrid.DataBind();

			PageNumberP.Visible = PromoterDataGrid.CurrentPageIndex > 0;
			PageNumberP.InnerText = "Page " + ((int)(PromoterDataGrid.CurrentPageIndex + 1)).ToString();

		}
		public void DataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			PromoterDataGrid.CurrentPageIndex = e.NewPageIndex;
			BindPromoters();
			Prefs.Current["NewPromotersPage"] = e.NewPageIndex;
		}
	}
}
