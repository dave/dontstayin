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
	public partial class SalesLetterFollowUp : AdminUserControl
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
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
				new Q(Promoter.Columns.LetterStatus, Promoter.LetterStatusEnum.Posted),
				new StringQueryCondition("(SELECT COUNT(*) FROM [SalesCall] WHERE [SalesCall].[PromoterK]=[Promoter].[K] AND [SalesCall].[IsCall]=1)=0")
			);

			q.OrderBy = new OrderBy(
				new OrderBy(Promoter.Columns.FutureEvents, OrderBy.OrderDirection.Descending),
				new OrderBy(Promoter.Columns.SalesNextCall, OrderBy.OrderDirection.Ascending),
				new OrderBy(Promoter.Columns.DateTimeSignUp, OrderBy.OrderDirection.Ascending));
			PromoterSet ps = new PromoterSet(q);

			PromoterDataGrid.AllowPaging = (ps.Count > PromoterDataGrid.PageSize);
			PromoterDataGrid.DataSource = ps;
			PromoterDataGrid.DataBind();

		}
		public void DataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			PromoterDataGrid.CurrentPageIndex = e.NewPageIndex;
			BindPromoters();
		}
	}
}
