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
using Bobs.DataHolders;

namespace Spotted.Pages.Promoters
{
	public partial class Guestlists : PromoterUserControl
	{
		#region Page_Init
		protected override void Page_Init(object sender, System.EventArgs e)
		{
			base.Page_Init(sender, e);
		}
		#endregion

		#region EnsureSecure()
		void EnsureSecure()
		{
			if (!ensureSecure)
			{
				if (!Usr.Current.IsAdmin)
				{
					if (!CurrentPromoter.HasGuestlist)
						throw new DsiUserFriendlyException("You must call our promoter hotline on 0207 835 5599 to set up your guestlist account.");

					if (CurrentEvent != null)
					{
						if (CurrentEvent.GuestlistPromoterK > 0 && CurrentEvent.GuestlistPromoterK != CurrentPromoter.K)
							throw new DsiUserFriendlyException("You don't have permission to access this guestlist!");
					}
				}
				ensureSecure = true;
			}
		}
		bool ensureSecure = false;
		#endregion

		protected Panel InfoPanel;

		private void Page_Load(object sender, System.EventArgs e)
		{
			EnsureSecure();
			ContainerPage.SetPageTitle("Guestlist administration");

			
			if (!Page.IsPostBack)
			{
				if (Mode.Equals(Modes.List))
					ChangePanel(PanelList);
				if (Mode.Equals(Modes.Close))
					ChangePanel(PanelClose);
			}
		}

		void RedirectDone()
		{
			if (CurrentEvent != null)
				Response.Redirect(CurrentPromoter.UrlEventOptions(CurrentEvent) + "#GuestlistPanel");
			else
				Response.Redirect(ContainerPage.Url.CurrentUrl("mode", null, "eventk", null));

		}

		#region PanelBuy
		protected Panel PanelBuy;
		protected TextBox BuyCredits;
		protected Panel PanelPayDone;

		public void Buy_Click(object o, System.EventArgs e)
		{
			EnsureSecure();
			if (Page.IsValid)
			{
				GuestlistCredit gc = new GuestlistCredit();
				gc.DateTimeCreated = DateTime.Now;
				gc.Credits = int.Parse(BuyCredits.Text);
				gc.PromoterK = CurrentPromoter.K;
				gc.TotalPrice = CurrentPromoter.GuestlistCharge * int.Parse(BuyCredits.Text);
				gc.Done = false;
				gc.Update();

				ChangePanel(PanelPay);

				this.ViewState["GuestlistCreditK"] = gc.K;

				InvoiceDataHolder i = new InvoiceDataHolder();
				InvoiceItemDataHolder iidh = new InvoiceItemDataHolder();
				iidh.VatCode = InvoiceItem.VATCodes.T1;
				iidh.PriceBeforeDiscount = gc.TotalPrice;
				iidh.Type = InvoiceItem.Types.GuestlistCredit;
				iidh.KeyData = gc.K;
				iidh.BuyableObjectK = gc.K;
				iidh.BuyableObjectType = Model.Entities.ObjectType.GuestlistCredit;
				iidh.RevenueStartDate = DateTime.Today;
				iidh.RevenueEndDate = DateTime.Today;
				iidh.Description = gc.Credits.ToString("#,###") + " guestlist credits";
				i.InvoiceItemDataHolderList.Add(iidh);
				
				i.PromoterK = CurrentPromoter.K;
				i.UsrK = Usr.Current.K;
				i.VatCode = Invoice.VATCodes.T1;
				
				Payment.Invoices.Clear();
				Payment.Invoices.Add(i);
				Payment.PromoterK = CurrentPromoter.K;
				Payment.Initialize();
				ContainerPage.SslPage = true;

				//Response.Redirect("https://www.paypal.com/xclick/business=paypal%40dontstayin.com&item_name=Payment+for+"+int.Parse(BuyCredits.Text).ToString()+"+guestlist+credits+code+guestlist-"+gc.K.ToString()+"&amount="+gc.TotalPrice.ToString("0.00")+"&no_note=1&currency_code=GBP");
			}
		}
		public void Buy_Cancel(object o, System.EventArgs e)
		{
			RedirectDone();
		}
		#endregion

		#region PanelPay
		protected Panel PanelPay;
		protected Controls.Payment Payment;
		public void PaymentReceived(object o, Controls.Payment.PaymentDoneEventArgs e)
		{
			this.ContainerPage.Url.ResetObjectFilterObject();
			ChangePanel(PanelPayDone);
		}
		public void Pay_Cancel(object o, System.EventArgs e)
		{
			try
			{
				int GuestlistCreditK = (int)this.ViewState["GuestlistCreditK"];
				GuestlistCredit guestlistCredit = new GuestlistCredit(GuestlistCreditK);
				if (!guestlistCredit.Done)
				{
					//guestlistCredit.Delete();
					//guestlistCredit.Update();
					this.Payment.Reset();
				}
				this.ViewState["GuestlistCreditK"] = null;
			}
			catch { }
			ChangePanel(PanelBuy);
		}
		#endregion

		#region PanelEdit
		protected Panel PanelEdit, PanelAddError, PanelAddCreditsError;
		protected DropDownList EditEventDropDown;
		protected TextBox EditPriceTextBox, EditRegularPriceTextBox, EditDetails, EditLimit;
		protected HtmlTableRow EditEventTr, EditEventTr1;

		public void PanelEdit_Save(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				if (Mode.Equals(Modes.Add))
				{
					Event ev = null;

					if (CurrentEvent != null)
						ev = CurrentEvent;
					else
						ev = new Event(int.Parse(EditEventDropDown.SelectedValue));

					if (!ev.IsConfirmedPromoter(CurrentPromoter.K))
						throw new DsiUserFriendlyException("You can't add a guestlist to this event!");

					ev.HasGuestlist = true;
					ev.GuestlistPromotion = true;
					ev.GuestlistPromoterK = CurrentPromoter.K;
					ev.GuestlistDetails = Cambro.Web.Helpers.StripHtml(EditDetails.Text);
					ev.GuestlistPrice = double.Parse(EditPriceTextBox.Text);
					ev.GuestlistRegularPrice = double.Parse(EditRegularPriceTextBox.Text);
					ev.GuestlistLimit = int.Parse(EditLimit.Text);
					ev.GuestlistOpen = true;
					ev.Update();
				}
				else if (Mode.Equals(Modes.Edit))
				{
					if (CurrentPromoter.K != CurrentEvent.GuestlistPromoterK)
						throw new DsiUserFriendlyException("You can't edit this guestlist");

					CurrentEvent.GuestlistDetails = Cambro.Web.Helpers.StripHtml(EditDetails.Text);
					CurrentEvent.GuestlistPrice = double.Parse(EditPriceTextBox.Text);
					CurrentEvent.GuestlistRegularPrice = double.Parse(EditRegularPriceTextBox.Text);
					CurrentEvent.GuestlistLimit = int.Parse(EditLimit.Text);
					CurrentEvent.Update();
				}
				RedirectDone();
			}
		}
		public void PanelEdit_Load(object o, System.EventArgs e)
		{
			EnsureSecure();
			if (Mode.Equals(Modes.Add) || Mode.Equals(Modes.Edit))
			{
				if (Mode.Equals(Modes.Edit))
				{
					if (CurrentEvent == null)
						throw new DsiUserFriendlyException("Must select an event!");
					if (!CurrentEvent.HasGuestlist)
						throw new DsiUserFriendlyException("This event doesn't have a guestlist!");
					if (CurrentPromoter.K != CurrentEvent.GuestlistPromoterK)
						throw new DsiUserFriendlyException("You can't edit this guestlist");
				}
				else if (Mode.Equals(Modes.Add))
				{
					if (CurrentEvent != null)
					{
						EditEventTr.Visible = false;
						EditEventTr1.Visible = false;
					}
					if (!Page.IsPostBack)
					{
						if (CurrentPromoter.GuestlistCreditAvailable <= 0)
						{
							ChangePanel(PanelAddCreditsError);
							return;
						}
					}
				}
				ChangePanel(PanelEdit);

				#region Future events
				if (Mode.Equals(Modes.Edit))
					EditEventDropDown.Enabled = false;

				if (!Page.IsPostBack)
				{
					EventSet es = null;
					if (Mode.Equals(Modes.Add))
						es = CurrentPromoter.GetUpcomingEvents(true);
					else
						es = CurrentPromoter.GetUpcomingEvents(CurrentEvent.K, true);
					if (es.Count == 0)
					{
						ChangePanel(PanelAddError);
						return;
					}
					EditEventDropDown.DataSource = es;
					EditEventDropDown.DataTextField = "FriendlyName";
					EditEventDropDown.DataValueField = "K";
					EditEventDropDown.DataBind();
				}
				#endregion
				#region Set up the form for editing
				if (!Page.IsPostBack && Mode.Equals(Modes.Edit))
				{
					EditEventDropDown.SelectedValue = CurrentEvent.K.ToString();
					EditPriceTextBox.Text = CurrentEvent.GuestlistPrice.ToString("0.00");
					EditRegularPriceTextBox.Text = CurrentEvent.GuestlistRegularPrice.ToString("0.00");
					EditDetails.Text = CurrentEvent.GuestlistDetails;
					EditLimit.Text = CurrentEvent.GuestlistLimit.ToString();
				}
				#endregion


			}
		}
		public void EditLimit_CreditVal(object o, ServerValidateEventArgs e)
		{
			int thisEventK = 0;
			if (Mode.Equals(Modes.Edit))
				thisEventK = CurrentEvent.K;

			e.IsValid = CurrentPromoter.GuestlistTotalCreditAvailable - CurrentPromoter.GetTotalOpenGuestlistCredits(thisEventK) >= int.Parse(EditLimit.Text);
		}
		public void EditLimit_CountVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = Mode.Equals(Modes.Add) || CurrentEvent.GuestlistCount <= int.Parse(EditLimit.Text);
		}
		public void EditEvent_Val(object o, ServerValidateEventArgs e)
		{
			if (Mode.Equals(Modes.Add))
			{
				Event ev = new Event(int.Parse(EditEventDropDown.SelectedValue));
				e.IsValid = !ev.HasGuestlist;
			}
			else
				e.IsValid = true;
		}
		public void PanelEdit_Cancel(object o, System.EventArgs e)
		{
			RedirectDone();
		}

		#endregion

		#region Utility_Load
		public void Utility_Load(object o, System.EventArgs e)
		{
			if (Mode.Equals(Modes.Pause) || Mode.Equals(Modes.Open) || Mode.Equals(Modes.Delete))
			{
				EnsureSecure();
				if (CurrentEvent == null)
					throw new DsiUserFriendlyException("No event selected!");

				if (Mode.Equals(Modes.Pause))
				{
					if (CurrentEvent.GuestlistFinished)
						throw new DsiUserFriendlyException("Guestlist is closed - can't pause now.");

					if (CurrentEvent.GuestlistOpen)
					{
						CurrentEvent.GuestlistOpen = false;
						CurrentEvent.Update();
					}
				}
				else if (Mode.Equals(Modes.Open))
				{
					if (CurrentEvent.GuestlistFinished)
						throw new DsiUserFriendlyException("Guestlist is closed - can't open now.");

					if (!CurrentEvent.GuestlistOpen)
					{
						CurrentEvent.GuestlistOpen = true;
						CurrentEvent.Update();
					}
				}
				else if (Mode.Equals(Modes.Delete))
				{
					if (CurrentEvent.GuestlistCount > 0)
						throw new DsiUserFriendlyException("Guestlist has people on it - can't delete.");

					CurrentEvent.HasGuestlist = false;
					CurrentEvent.GuestlistPromoterK = 0;
					CurrentEvent.GuestlistDetails = "";
					CurrentEvent.GuestlistFinished = false;
					CurrentEvent.GuestlistOpen = false;
					CurrentEvent.GuestlistLimit = 0;
					CurrentEvent.GuestlistRegularPrice = 0.0;
					CurrentEvent.GuestlistPrice = 0.0;
					CurrentEvent.Update();
				}
				RedirectDone();
			}
		}
		#endregion

		#region PanelList
		protected Panel PanelList;
		protected DataGrid EventsDataGrid;
		protected Label NoGuestlistsLabel;
		public void PanelList_Load(object o, System.EventArgs e)
		{
			if (Mode.Equals(Modes.List))
			{
				BindEvents();
			}
		}
		#region BindEvents()
		void BindEvents()
		{
			Query q = new Query();
			q.QueryCondition = new And(new Q(Event.Columns.HasGuestlist, true), new Q(Event.Columns.GuestlistPromoterK, CurrentPromoter.K));
			q.NoLock = true;
			q.OrderBy = Event.PastEventOrder;
			EventSet es = new EventSet(q);
			if (es.Count > 0)
			{
				EventsDataGrid.AllowPaging = (es.Count > EventsDataGrid.PageSize);
				EventsDataGrid.DataSource = es;
				EventsDataGrid.DataBind();
			}
			else
			{
				NoGuestlistsLabel.Visible = true;
				EventsDataGrid.Visible = false;
			}
		}
		#endregion
		#region EventsDataGridChangePage
		public void EventsDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			EventsDataGrid.CurrentPageIndex = e.NewPageIndex;
			BindEvents();
		}
		#endregion
		#endregion

		#region PanelClose
		protected Panel PanelClose;
		protected Label PanelCloseEventLabel, PanelCloseCountLabel;
		public void PanelClose_Load(object o, System.EventArgs e)
		{
			EnsureSecure();
			if (Mode.Equals(Modes.Close))
			{
				PanelCloseEventLabel.Text = CurrentEvent.FriendlyName;
				if (CurrentEvent.GuestlistCount == 1)
					PanelCloseCountLabel.Text = "There is 1 name on the list.";
				else
					PanelCloseCountLabel.Text = "There are " + CurrentEvent.GuestlistCount + " names on the list.";
			}
		}
		public void PanelClose_Cancel(object o, System.EventArgs e)
		{
			RedirectDone();
		}
		public void PanelClose_Close(object o, System.EventArgs e)
		{
			EnsureSecure();
			if (Mode.Equals(Modes.Close))
			{
				if (CurrentEvent.GuestlistFinished)
					throw new DsiUserFriendlyException("This guestlist is already closed!");

				CurrentEvent.GuestlistFinished = true;
				CurrentEvent.GuestlistOpen = false;
				CurrentEvent.Update();

				CurrentPromoter.GuestlistCredit = CurrentPromoter.GuestlistCredit - CurrentEvent.GuestlistCount;
				CurrentPromoter.Update();

				RedirectDone();

			}
		}

		#endregion

		#region Mode
		Modes Mode
		{
			get
			{
				if (ContainerPage.Url["Mode"].Equals("List") || ContainerPage.Url["Mode"].IsNull)
					return Modes.List;
				if (ContainerPage.Url["Mode"].Equals("Buy"))
					return Modes.Buy;
				if (ContainerPage.Url["Mode"].Equals("Pause"))
					return Modes.Pause;
				if (ContainerPage.Url["Mode"].Equals("Close"))
					return Modes.Close;
				if (ContainerPage.Url["Mode"].Equals("Delete"))
					return Modes.Delete;
				if (ContainerPage.Url["Mode"].Equals("Edit"))
					return Modes.Edit;
				if (ContainerPage.Url["Mode"].Equals("Open"))
					return Modes.Open;
				else
					return Modes.None;
			}
		}
		public enum Modes
		{
			None,
			List,
			Buy,
			Pause,
			Close,
			Delete,
			Open,
			Add,
			Edit
		}
		#endregion

		#region CurrentEvent
		public Event CurrentEvent
		{
			get
			{
				if (currentEvent == null && ContainerPage.Url["EventK"].IsInt)
					currentEvent = new Event(ContainerPage.Url["EventK"]);
				return currentEvent;
			}
		}
		Event currentEvent;
		#endregion
		#region CurrentBrand
		public Brand CurrentBrand
		{
			get
			{
				if (currentBrand == null && ContainerPage.Url["BrandK"].IsInt)
					currentBrand = new Brand(ContainerPage.Url["BrandK"]);
				return currentBrand;
			}
		}
		Brand currentBrand;
		#endregion

		void ChangePanel(Panel p)
		{
			PanelPayDone.Visible = p.Equals(PanelPayDone);
			PanelPay.Visible = p.Equals(PanelPay);
			PanelList.Visible = p.Equals(PanelList);
			PanelClose.Visible = p.Equals(PanelClose);
			PanelEdit.Visible = p.Equals(PanelEdit);
			PanelAddError.Visible = p.Equals(PanelAddError);
			PanelAddCreditsError.Visible = p.Equals(PanelAddCreditsError);
			PanelBuy.Visible = p.Equals(PanelBuy);
		}

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
			this.Load += new System.EventHandler(this.PanelList_Load);
			this.Load += new System.EventHandler(this.Utility_Load);
			this.Load += new System.EventHandler(this.PanelClose_Load);
			this.Load += new System.EventHandler(this.PanelEdit_Load);
		}
		#endregion
	}
}
