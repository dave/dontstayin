using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Bobs;

namespace Spotted.Admin
{
	public partial class AddBulkSkeletonPromoters : AdminUserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				this.Sector.Items.Clear();
				Utilities.AddEnumValuesToDropDownList(this.Sector, typeof(Promoter.ClientSectorEnum));
				Sector.Items.Insert(0, new ListItem("(none)", "0"));

				SalesCampaignDropDown.Items.Clear();
				SalesCampaignSet scs = new SalesCampaignSet(new Query() { OrderBy = new OrderBy(SalesCampaign.Columns.DateStart, OrderBy.OrderDirection.Descending) });
				SalesCampaignDropDown.DataSource = scs;
				SalesCampaignDropDown.DataTextField = "Name";
				SalesCampaignDropDown.DataValueField = "K";
				SalesCampaignDropDown.DataBind();
				SalesCampaignDropDown.Items.Insert(0, new ListItem("(none)", "0"));
			}
		}
		#region Add
		protected void Add(object sender, EventArgs eventArgs)
		{
			int errors = 0;
			string[] promoters = Csv.Text.Split('\n');
			foreach (string s in promoters)
			{
				try
				{
					string[] parts = s.Split(',');
					string name = parts[0].Trim();
					string number = parts[1].Trim();
					string notes = "";
					if (parts.Length > 2)
					{
						for (int i = 2; i < parts.Length; i++)
							notes += (notes.Length > 0 ? "\n" : "") + parts[i].Trim();
					}
					Bobs.Promoter CurrentPromoter = new Bobs.Promoter();
					CurrentPromoter.DateTimeSignUp = DateTime.Now;
					CurrentPromoter.AddedByUsrK = Usr.Current.K;
					CurrentPromoter.Status = Promoter.StatusEnum.Enabled;
					CurrentPromoter.PricingMultiplier = 1.0;
					CurrentPromoter.AddedMethod = Promoter.AddedMedhods.SalesUser;

					CurrentPromoter.Name = name;
					CurrentPromoter.PhoneNumber = number;
					CurrentPromoter.ClientSector = (Promoter.ClientSectorEnum)Convert.ToInt32(Sector.SelectedValue);
					CurrentPromoter.SalesCampaignK = int.Parse(SalesCampaignDropDown.SelectedValue);
					CurrentPromoter.IsSkeleton = true;
					CurrentPromoter.SalesStatus = Promoter.SalesStatusEnum.New;
					CurrentPromoter.SalesUsrK = Usr.Current.K;
					CurrentPromoter.SalesStatusExpires = DateTime.Today.AddMonths(3);
					CurrentPromoter.SalesNextCall = DateTime.Now;

					CurrentPromoter.CreateUniqueUrlName();

					CurrentPromoter.Update();

					if (notes.Length > 0)
					{
						CurrentPromoter.AddNote(notes, Guid.NewGuid(), Usr.Current);
						CurrentPromoter.SalesCallCount = 0;
						CurrentPromoter.Update();
					}
				}
				catch
				{
					errors++;
					Error.InnerHtml += "FAILED on \"" + s + "\" <br/>";
				}
			}
			if (errors == 0)
				Response.Redirect("/admin/salesnew");
		}
		#endregion
	}
}
