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
	public partial class PromoterWithTicketRunsVatStatus : AdminUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
				SendReminderEmailForUnknownVatStatusPromotersButton.Disabled = !Usr.Current.IsSuperAdmin;
				SendReminderEmailForUnknownVatStatusPromotersButton.Attributes["onclick"] = "if(confirm('Are you sure you want to send emails to all admins in all the promoter account whos VAT status is unknown?') && confirm('Are you really sure?')){this.disabled=true;__doPostBack('" + SendReminderEmailForUnknownVatStatusPromotersButton.UniqueID + "','');return false;}else{return false;};";
				SetupVatStatusDropDownList();
				LoadPromotersWithTicketRunsAndUnknownVatStatus();				
			}
		}

		private void LoadPromotersWithTicketRunsAndUnknownVatStatus()
		{
			Query q = new Query();

			if (this.VatStatusDropDownList.SelectedValue != "")
			{
				if (this.VatStatusDropDownList.SelectedValue == "0")
				{
					q.QueryCondition = new Or(new Q(Promoter.Columns.VatStatus, Promoter.VatStatusEnum.Unknown),
											  new Q(Promoter.Columns.VatStatus, QueryOperator.IsNull, null));
				}
				else
				{
					q.QueryCondition = new Q(Promoter.Columns.VatStatus, Convert.ToInt32(VatStatusDropDownList.SelectedValue));
				}
			}
			q.TableElement = new Join(Promoter.Columns.K, TicketRun.Columns.PromoterK);
			q.Distinct = true;
			q.DistinctColumn = Promoter.Columns.K;
			q.OrderBy = new OrderBy(Promoter.Columns.Name);

			PromoterSet promoters = new PromoterSet(q);

			UnknownPromoterVatStatusGridView.DataSource = promoters;
			UnknownPromoterVatStatusGridView.DataBind();
		}

		private void SetupVatStatusDropDownList()
		{
			this.VatStatusDropDownList.Items.Clear();
			this.VatStatusDropDownList.Items.Add(new ListItem("", ""));
			this.VatStatusDropDownList.Items.AddRange(Promoter.VatStatusEnumAsListItemArray());
		}

		protected void VatStatusDropDownList_SelectedIndexChanged(object sender, EventArgs e)
		{
			UnknownPromoterVatStatusGridView.PageIndex = 0;
			LoadPromotersWithTicketRunsAndUnknownVatStatus();
		}

		protected void UnknownPromoterVatStatusGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			UnknownPromoterVatStatusGridView.PageIndex = e.NewPageIndex;
			LoadPromotersWithTicketRunsAndUnknownVatStatus();
			if (UnknownPromoterVatStatusGridView.PageIndex > UnknownPromoterVatStatusGridView.PageCount)
				UnknownPromoterVatStatusGridView.PageIndex = 1;
		}

		protected void SendReminderEmailForUnknownVatStatusPromotersButton_Click(object sender, EventArgs e)
		{
			if (Usr.Current.IsSuperAdmin)
			{
				Utilities.EmailAllPromotersWithTicketsAndUnknownVatStatus();
				this.SendReminderEmailForUnknownVatStatusPromotersButton.Disabled = true;
			}
		}
	}
}
