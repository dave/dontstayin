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
using Bobs.DataHolders;

namespace Spotted.Pages.Promoters
{
	public partial class BannersPending : PromoterUserControl
	{
		protected string BannerCheckBoxClientIDsAsList { get; set; }

		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			if (ViewState["BannerCheckBoxClientIDsAsList"] != null) BannerCheckBoxClientIDsAsList = (string)ViewState["BannerCheckBoxClientIDsAsList"];
		}
		protected override object SaveViewState()
		{
			ViewState["BannerCheckBoxClientIDsAsList"] = BannerCheckBoxClientIDsAsList;
			return base.SaveViewState();
		}

		#region Banner selection
		protected void Page_Load(object sender, EventArgs e)
		{
			ContainerPage.SslPage = true;
            Response.CacheControl = "no-cache";
            Response.AddHeader("Pragma", "no-cache");
            Response.Expires = -1;

			ContainerPage.SetPageTitle("Pending banners");

			if (!Page.IsPostBack)
			{
				Query q = new Query(
							  new And(
								  new Q(Banner.Columns.PromoterK, this.CurrentPromoter.K),
								  new Q(Banner.Columns.StatusBooked, false),
								  new Q(Banner.Columns.LastDay, QueryOperator.GreaterThanOrEqualTo, Common.Time.Now.Date)));
                //q.Columns = new ColumnSet(
                //                Banner.Columns.K, 
                //                Banner.Columns.Name, 
                //                Banner.Columns.TotalRequiredImpressions, 
                //                Banner.Columns.Position,
                //                Banner.Columns.LastDay,
                //                Banner.Columns.FirstDay,
                //                Banner.Columns.EventK,
                //                Banner.Columns.VenueK,
                //                Banner.Columns.PromoterK,
                //                Banner.Columns.BrandK,
                //                Banner.Columns.LinkTarget,
                //                Banner.Columns.LinkUrl,
                //                Banner.Columns.StatusBooked,
                //                Banner.Columns.IsPriceFixed,
                //                Banner.Columns.PriceCreditsStored,
                //                Banner.Columns.DesignType);

				BannerSet bs = new BannerSet(q);
				if (bs.Count == 0)
				{
					NoPendingBannersLabel.Visible = true;
					BookBannersPanel.Visible = false;
				}
				else
				{
					NoPendingBannersLabel.Visible = false;
					BannerGrid.DataSource = bs;
					BannerGrid.DataBind();
					BookBannersPanel.Visible = true;
				}

				PaymentPanel.Visible = false;
				ConfirmedPanel.Visible = false;
			}
		}


		public void BookBannersButton_Click(object o, EventArgs e)
		{
			if (!Page.IsValid)
			{
				return;
			}

			Payment.Reset();

            List<int> bannerKs = new List<int>();
			foreach (GridViewRow row in this.BannerGrid.Rows)
			{
                try
                {
				    if (((CheckBox)row.FindControl("CheckBox")).Checked)
				    {
					    bannerKs.Add(Convert.ToInt32((((Label)row.FindControl("BannerK")).Text)));
                    }
                }
                catch
                {}
            }

            if(bannerKs.Count > 0)
            {
                Query bannerQuery = new Query(new Q(Banner.Columns.K, bannerKs.ToArray()));
                //bannerQuery.Columns = new ColumnSet(Banner.Columns.K, Banner.Columns.TotalRequiredImpressions, Banner.Columns.Position, Banner.Columns.Name, Banner.Columns.PriceStored, Banner.Columns.PromoterK,
                //                                    Banner.Columns.FirstDay, Banner.Columns.LastDay, Banner.Columns.StatusBooked, Banner.Columns.IsPriceFixed, Banner.Columns.PriceCreditsStored);
                BannerSet banners = new BannerSet(bannerQuery);

                foreach (Banner banner in banners)
                {
					Payment.CampaignCredits.AddRange(banner.ToCampaignCredits(Usr.Current, CurrentPromoter.K, true));
                }
			}
            

			Payment.PromoterK = CurrentPromoter.K;
			Payment.Initialize();
			Payment.LoadBuyerDetailsToScreen();

			ShowPaymentOptions();
		}

		private void ShowPaymentOptions()
		{
			BookBannersPanel.Visible = false;
			PaymentPanel.Visible = true;
		}
		#endregion

		#region PaymentReceived()
		public void PaymentReceived(object o, Controls.Payment.PaymentDoneEventArgs e)
		{
			BookBanners(e.CampaignCredits);
			ShowConfirmedPanel();
		}
		#endregion

		private void BookBanners(List<CampaignCredit> campaignCredits)
		{
            List<int> bookedBannerKs = new List<int>();
			foreach (CampaignCredit cc in campaignCredits)
			{
                if (cc.BuyableObjectType == Model.Entities.ObjectType.Banner)
                    bookedBannerKs.Add(cc.BuyableObjectK);
			}

            BannerSet bookedBanners = new BannerSet(new Query(new Q(Banner.Columns.K, bookedBannerKs.ToArray())));
            this.BookedBannersGridView.DataSource = bookedBanners;
            this.BookedBannersGridView.DataBind();

            H1Title.InnerHtml = "Booked Banners";
		}

		private void ShowConfirmedPanel()
		{
			PaymentPanel.Visible = false;
			ConfirmedPanel.Visible = true;
		}

		protected void CancelButton_Click(object o, EventArgs e)
		{
			Payment.Reset();
			PaymentPanel.Visible = false;
			BookBannersPanel.Visible = true;
		}

		protected void BannerGrid_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				if (BannerCheckBoxClientIDsAsList == null)
				{
					BannerCheckBoxClientIDsAsList = "\"" + ((CheckBox)e.Row.FindControl("CheckBox")).ClientID + "\"";
				}
				else
				{
					BannerCheckBoxClientIDsAsList += ",\"" + ((CheckBox)e.Row.FindControl("CheckBox")).ClientID + "\"";
				}
			}
		}

		protected void EnsureBannersSelected(object sender, ServerValidateEventArgs args)
		{
			foreach (GridViewRow row in this.BannerGrid.Rows)
			{
				if (((CheckBox)row.FindControl("CheckBox")).Checked)
				{
					args.IsValid = true;
					return;
				}
			}
			args.IsValid = false;
		}

	}
}
