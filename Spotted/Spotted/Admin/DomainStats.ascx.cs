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
	public partial class DomainStats : AdminUserControl
	{
		#region Properties - UI
		int PromoterK
		{
			get { return int.Parse(this.uiPromoterHtmlAutoComplete.Value); }
		}
		DomainSet Domains
		{
			set
			{
				uiDomainsList.DataSource = value;
				uiDomainsList.Visible = true;
				uiDomainsList.DataBind();

				if (value.Count == 1)
				{
					uiSelectDomainButton.Visible = false;
					DisplayStatsDomainK = value[0].K;
				}
				else
				{
					uiSelectDomainButton.Visible = true;
					DisplayStatsDomainK = 0;
				}
			}
		}
		int SelectedDomainK
		{
			get
			{
				return int.Parse(uiDomainsList.SelectedValue);
			}
		}
		int DisplayStatsDomainK
		{
			set
			{
				if (value > 0)
				{
					Query q = new Query(new Q(Bobs.DomainStats.Columns.DomainK, value));
					q.OrderBy = new OrderBy(Bobs.DomainStats.Columns.Date, OrderBy.OrderDirection.Descending);
					uiGridView.DataSource = new DomainStatsSet(q);
					uiGridView.DataBind();
					uiGridView.Visible = true;
				}
				else
				{
					uiGridView.Visible = false;
				}
			}
		}
		#endregion

		#region Actions
		protected void PromoterSelected(object o, EventArgs e)
		{
			Domains = new DomainSet(new Query(new Q(Domain.Columns.PromoterK, PromoterK)));
		}
		protected void DomainSelected(object o, EventArgs e)
		{
			DisplayStatsDomainK = SelectedDomainK;
		}
		#endregion
	}
}
