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
using Common.Clocks;
using Common;

namespace Spotted.Pages.Promoters
{
	public partial class EventHighlight : PromoterUserControl
	{
		protected HtmlTableCell RecommendedCell, RecommendedCellPrice;
		protected Controls.Payment Payment;

		private void Page_Load(object sender, System.EventArgs e)
		{
			ContainerPage.SslPage = true;

			if (CurrentEvent != null && Usr.Current != null)
			{
				if (CurrentPromoter.K > 1)
					Payment.PromoterK = CurrentPromoter.K;
                
                if (CurrentEvent.Donated)
                    EventDonated();
			}

            if (!this.IsPostBack)
                Bind();

			IntroBannerListLink.HRef = CurrentPromoter.UrlEventOptions(CurrentEvent);
            
		}

		public void Bind()
		{
            RecommendedCellPrice.InnerText = Vars.EventHighlightPriceCredits(CurrentEvent).ToString("N0") + " credits";

			RecommendedCell.InnerText = RecommendedCell.InnerText.Replace("?", CurrentEvent.Venue.Capacity.ToString());
		}
		protected Panel ChoicePanel, PayPanel, PayDonePanel;
		public void Donation_Click(object o, System.EventArgs e)
		{
            if (CurrentEvent.Donated)
                EventDonated();
            else
            {
                Usr.KickUserIfNotLoggedIn("You must be logged in to buy an event highlight!");
                
                Bobs.DataHolders.InvoiceDataHolder i = new Bobs.DataHolders.InvoiceDataHolder();
                Bobs.DataHolders.InvoiceItemDataHolder iidh = new Bobs.DataHolders.InvoiceItemDataHolder();

                CampaignCredit campaignCredit = new CampaignCredit()
                {
                    Description = "Event highlight " + CurrentEvent.K.ToString(),
                    BuyableObjectK = CurrentEvent.K,
                    BuyableObjectType = Model.Entities.ObjectType.Event,
                    Credits = -Vars.EventHighlightPriceCredits(CurrentEvent),
                    ActionDateTime = Time.Now,
                    PromoterK = CurrentPromoter.K,
                    InvoiceItemType = InvoiceItem.Types.EventDonate,
                    Enabled = false
                };
				campaignCredit.SetUsrAndActionUsr(Usr.Current);
                campaignCredit.Update();

                Payment.CampaignCredits.Clear();
                Payment.CampaignCredits.Add(campaignCredit);
                if (CurrentPromoter.K != 1)
                    Payment.PromoterK = CurrentPromoter.K;
                Payment.Initialize();

                ChoicePanel.Visible = false;
                PayPanel.Visible = true;
				
            }
		}

		public void PaymentReceived(object o, Controls.Payment.PaymentDoneEventArgs e)
		{
            EventDonated();
		}
		public void Pay_Cancel(object o, System.EventArgs e)
		{
			PayPanel.Visible = false;
			ChoicePanel.Visible = true;
		}

        private void EventDonated()
        {
            ChoicePanel.Visible = false;
            PayPanel.Visible = false;
            PayDonePanel.Visible = true;
            //List<Event> events = new List<Event>();
            //events.Add(CurrentEvent);
            //HighlightedEventDataList.ItemTemplate = this.LoadTemplate("/Templates/Events/UsrPageAttendedList.ascx");
            //HighlightedEventDataList.DataSource = events;
            //HighlightedEventDataList.DataBind();
        }

		#region CurrentEvent
		public Event CurrentEvent
		{
			get
			{
				if (currentEvent == null && EventK > 0)
					currentEvent = new Event(EventK);
				return currentEvent;
			}
			set
			{
				currentEvent = value;
			}
		}
		Event currentEvent;
		#endregion

		#region EventK
		public int EventK
		{
			get
			{
                return ContainerPage.Url["EventK"];
			}
		}
		#endregion

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
