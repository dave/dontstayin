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
using System.Text;

namespace Spotted.Pages.Promoters
{
	public partial class Home : PromoterUserControl
	{

		#region Page_Init
		protected override void Page_Init(object sender, System.EventArgs e)
		{
			base.Page_Init(sender, e);
		}
		#endregion

		#region Page_Load
		protected Panel PanelNotEnabled, PanelEnabled;
		private void Page_Load(object sender, System.EventArgs e)
		{
			PanelNotEnabled.Visible = !CurrentPromoter.IsEnabled;
			PanelEnabled.Visible = CurrentPromoter.IsEnabled;
			CreateCampaignCreditsResponseLabel.Visible = false;

			if (!Page.IsPostBack)
			{
				this.ViewState["SalesCallDuplicateGuid"] = Guid.NewGuid();
				SetupSalesStatusDropDownList();
				SetupSalesPersonsDropDownList();
				SetupSalesEstimateDropDownList();
				CreateCampaignCreditsButton.Attributes["onclick"] = "if(confirm('Are you sure you want to use ticket funds to buy campaign credits?')){__doPostBack('" + CreateCampaignCreditsButton.UniqueID + "','');return false;}else{return false;};";

//				uiDomains.PromoterK = CurrentPromoter.K;
			}
			SetupNextCallTimeJavascript();
			
			if (ContainerPage.Url["clearrecentlytransferred"].Exists)
			{
				CurrentPromoter.RecentlyTransferred = false;
				CurrentPromoter.Update();
			}
			

			uiConfirmCardDetailsLink.Visible = CurrentPromoter.WillCheckCardsForPurchasedTickets;
		}

		#endregion

		#region Page_PreRender
		protected void Page_PreRender(object sender, EventArgs eventArgs)
		{
			SalesHoldLabel.Text = CurrentPromoter.SalesHold ? "[ON HOLD]" : "";

			if (Prefs.Current["HideAdmin"] == 1)
			{
				AdminPanel.Visible = false;
			}
			if (Usr.Current != null && Usr.Current.IsAdmin && ContainerPage.Url["callnow"].Equals("true"))
			{
				MakeSalesCall(null, null);
			}
		}
		#endregion

		#region PromoterIntro
		protected Spotted.CustomControls.PromoterIntro PromoterIntro;
		protected Panel UsersPanel;
		protected Repeater UsersRepeater;
		private void PromoterIntro_Load(object sender, System.EventArgs e)
		{
			if (CurrentPromoter.IsEnabled)
			{
				PromoterIntro.Header = CurrentPromoter.Name;
				UsersPanel.Visible = CurrentPromoter.AdminUsrs.Count > 0;
				if (CurrentPromoter.AdminUsrs.Count > 0)
				{
					UsersRepeater.DataSource = CurrentPromoter.AdminUsrs;
					UsersRepeater.DataBind();
				}

				// Commented out by request of John Brophy on 11/1/06
				//DateTime expire = CurrentPromoter.DateTimeSignUp.AddDays(8);
				//if (CurrentPromoter.OfferType.Equals(Promoter.OfferTypes.DoubleSlots) && CurrentPromoter.OfferExpireDateTime>expire)
				//    expire = CurrentPromoter.OfferExpireDateTime;

				//if (expire>DateTime.Now)
				//{
				//    IntroOfferPanel.Visible = true;
				//    OfferExpireTimeSpan.Text=Cambro.Misc.Utility.FriendlyTimeSpan(expire - DateTime.Now);
				//}
				//else
					IntroOfferPanel.Visible = false;
			}
		}
		#endregion

		#region VenuesPanel
		protected Panel VenuesPanel, NoVenuesPanel;
		protected DataGrid VenueDataGrid;
		private void VenuesPanel_Load(object sender, System.EventArgs e)
		{
			if (CurrentPromoter.IsEnabled)
			{
				VenuesPanel.Visible = CurrentPromoter.AllVenues.Count > 0;
				NoVenuesPanel.Visible = CurrentPromoter.AllVenues.Count == 0;
				if (CurrentPromoter.AllVenues.Count > 0)
					BindVenues();

			}
		}
		void BindVenues()
		{
			VenueDataGrid.Columns[VenueDataGrid.Columns.Count - 1].Visible = Usr.Current.IsSuperAdmin;
			CurrentPromoter.AllVenues = null;
			VenueDataGrid.AllowPaging = false;// (CurrentPromoter.AllVenues.Count > VenueDataGrid.PageSize);
			VenueDataGrid.DataSource = CurrentPromoter.AllVenues;
			VenueDataGrid.DataBind();
		}
		public void VenueDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			VenueDataGrid.CurrentPageIndex = e.NewPageIndex;
			BindVenues();
		}
		#endregion

		#region DomainsPanel
		private void DomainsPanel_Load(object sender, System.EventArgs e)
		{
			if (CurrentPromoter.IsEnabled)
			{
				DomainsPanel.Visible = CurrentPromoter.Domains.Count > 0;
				NoDomainsPanel.Visible = CurrentPromoter.Domains.Count == 0;
				if (CurrentPromoter.Domains.Count > 0)
					BindDomains();

			}
		}
		void BindDomains()
		{
			CurrentPromoter.Domains = null;
			DomainsDataGrid.AllowPaging = false;
			DomainsDataGrid.DataSource = CurrentPromoter.Domains;
			DomainsDataGrid.DataBind();
		}
		public void DomainsDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			DomainsDataGrid.CurrentPageIndex = e.NewPageIndex;
			BindDomains();
		}
		protected string hitsTotal(Domain d)
		{
			try
			{
				return ((int)d.ExtraSelectElements["hitsTotal"]).ToString("#,##0");
			}
			catch
			{
				return "0";
			}
		}
		protected string hitsMonth(Domain d)
		{
			try
			{
				return ((int)d.ExtraSelectElements["hitsMonth"]).ToString("#,##0");
			}
			catch
			{
				return "0";
			}
		}
		#endregion

		#region BrandsPanel
		protected Panel BrandsPanel, NoBrandsPanel;
		protected DataGrid BrandDataGrid;
		private void BrandsPanel_Load(object sender, System.EventArgs e)
		{
			if (CurrentPromoter.IsEnabled)
			{
				BrandsPanel.Visible = CurrentPromoter.AllBrands.Count > 0;
				NoBrandsPanel.Visible = CurrentPromoter.AllBrands.Count == 0;
				if (CurrentPromoter.AllBrands.Count > 0)
					BindBrands();
			}
		}
		void BindBrands()
		{
			BrandDataGrid.Columns[BrandDataGrid.Columns.Count - 1].Visible = Usr.Current.IsSuperAdmin;
			CurrentPromoter.AllBrands = null;
			BrandDataGrid.AllowPaging = false;// (CurrentPromoter.AllBrands.Count > BrandDataGrid.PageSize);
			BrandDataGrid.DataSource = CurrentPromoter.AllBrands;
			BrandDataGrid.DataBind();
		}
		public string BrandAdminHtml(string action, Brand brand)
		{
			bool on = Usr.Current.CanEdit(brand);
			string attributes = "href=\"\" onclick=\"alert('You do not have permission to alter this.\\nPlease confirm your brand by following the instructions or calling us on 0207 835 5599.');return false;\"";
			if (action.Equals("Name"))
			{
				if (on)
					attributes = "href=\"" + brand.UrlApp("edit", "promoterk", CurrentPromoter.K.ToString()) + "\"";
				return "<div style=\"padding-top:5px;\"><a " + attributes + ">change</a></div>";
			}
			else if (action.Equals("Picture"))
			{
				if (on)
					attributes = "href=\"" + brand.UrlApp("edit","page","pic","promoterk",CurrentPromoter.K.ToString()) + "\"";
				return "<a " + attributes + "><img src=\"" + (brand.HasPic ? "/gfx/icon-tick.png" : "/gfx/icon-cross.png") + "\" border=\"0\" height=\"21\" width=\"26\" align=\"absmiddle\">change</a>";
			}
			else 
				return "";

			
		}
		public string GroupAdminHtml(string action, Brand brand, Group group)
		{
			try
			{
				GroupUsr gu = null;
				if (GroupUsrHashtable.ContainsKey(group.K))
					gu = (GroupUsr)GroupUsrHashtable[group.K];
				else
				{
					gu = Usr.Current.GetGroupUsr(group.K);
					GroupUsrHashtable[group.K] = gu;
				}

				bool on = Usr.Current.CanGroupOwner(gu);

				bool tick = false;
				switch (action)
				{
					case "Location":
						tick = group.CountryK > 0;
						break;
					case "MusicType":
						tick = group.MusicTypeK > 0;
						break;
					case "Details":
						tick = group.LongDescriptionHtml.Length > 0;
						break;
					default:
						break;
				}

				string attributes = "";
				if (on)
				{
					switch (action)
					{
						case "Location":
							attributes = "href=\"" + group.UrlApp("edit", "mode", "location", "promoterk", CurrentPromoter.K.ToString()) + "\"";
							break;
						case "MusicType":
							attributes = "href=\"" + group.UrlApp("edit", "mode", "musictype", "promoterk", CurrentPromoter.K.ToString()) + "\"";
							break;
						case "Details":
							attributes = "href=\"" + group.UrlApp("edit", "mode", "details", "promoterk", CurrentPromoter.K.ToString()) + "\"";
							break;
						default:
							break;
					}
				}
				else
				{
					attributes = "href=\"\" onclick=\"alert('You do not have permission to alter this.\\nPlease contact ";
					if (CurrentPromoter.PrimaryUsr != null)
						attributes += CurrentPromoter.PrimaryUsr.NickName + " and ask " + CurrentPromoter.PrimaryUsr.HimString(false);
					else
						attributes += CurrentPromoter.ContactName + " and ask them";

					attributes += " to make you an owner of the " + brand.UrlName + " regulars group.');return false;\"";
				}
				return "<a " + attributes + "><img src=\"" + (tick ? "/gfx/icon-tick.png" : "/gfx/icon-cross.png") + "\" border=\"0\" height=\"21\" width=\"26\" align=\"absmiddle\">change</a>";
			}
			catch
			{
				return "[Error]";
			}
		}
		public Hashtable GroupUsrHashtable = new Hashtable();
		public void BrandDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			BrandDataGrid.CurrentPageIndex = e.NewPageIndex;
			BindBrands();
		}
		#endregion

		#region GuestlistPanel
		protected Panel GuestlistPanel, NoGuestlistPanel;
		private void GuestlistPanel_Load(object sender, System.EventArgs e)
		{
			GuestlistPanel.Visible = CurrentPromoter.IsEnabled 
									 && CurrentPromoter.HasGuestlist 
									 && (CurrentPromoter.GuestlistCredit > 0 || CurrentPromoter.HasUpcomingEventsWithGuestlists);
		}
		#endregion

		#region EventsPanel
		protected Panel EventsPanel;
		protected GridView EventsGridView;
		protected Panel EventsPanelEventsNoEvents, EventsPanelEvents;
		public void EventsPanel_Load(object o, System.EventArgs e)
		{
			if (CurrentPromoter.IsEnabled && !this.IsPostBack)
			{
				BindEvents();
			}
		}
		void BindEvents()
		{
			if (CurrentPromoter.Status.Equals(Promoter.StatusEnum.Enabled) && !CurrentPromoter.HasConfirmedVenues() && !CurrentPromoter.HasConfirmedBrands())
			{
				NoEventsPanel.Visible = true;
				QuickViewPanel.Visible = false;
			}
			else
			{
				NoEventsPanel.Visible = false;
				QuickViewPanel.Visible = true;

				EventSet es = Event.GetUpcomingEvents(CurrentPromoter);
				EventsPanelEventsNoEvents.Visible = es.Count == 0;
				EventsPanelEvents.Visible = es.Count > 0;
				if (es.Count > 0)
				{
					EventsGridView.AllowPaging = (es.Count > EventsGridView.PageSize);
					EventsGridView.DataSource = es;
					EventsGridView.DataBind();
				}
			}
		}
		#region EventsGridView Event Handlers
		public void EventsGridViewChangePage(object sender, GridViewPageEventArgs e)
		{
			EventsGridView.PageIndex = e.NewPageIndex;
			BindEvents();
			ContainerPage.AnchorSkip("QuickViewPanel");
		}
		#endregion
		#endregion

		#region AdminPanel
		#region AdminPanel_Load
		public void AdminPanel_Load(object o, System.EventArgs e)
		{
			AdminPanel.Visible = Usr.Current.IsAdmin || Usr.Current.IsSuperAdmin;

			if (Usr.Current.IsAdmin)
			{
				Cambro.Web.Helpers.TieButton(NoteTextBox, SaveNoteButton);
				NoteTextBox.Attributes["onfocus"] = "if(this.value=='add a note here...')this.value='';";

				if (CurrentPromoter.AllBrands.Count > 0)
				{
					AdminBrandRepeater.DataSource = CurrentPromoter.AllBrands;
					AdminBrandRepeater.DataBind();
				}
				else
					NoBrandsLabel.Visible = true;

				if (CurrentPromoter.AllVenues.Count > 0)
				{
					AdminVenueRepeater.DataSource = CurrentPromoter.AllVenues;
					AdminVenueRepeater.DataBind();
				}
				else
					NoVenuesLabel.Visible = true;

				System.Text.RegularExpressions.Regex PhoneRegex = new System.Text.RegularExpressions.Regex("[^0-9]");

				if (!Page.IsPostBack)
				{
					AdminPhoneNumbersDropDown.Items.Clear();
					AdminPhoneNumbersDropDown.Items.Add(new ListItem("Main number " + CurrentPromoter.PhoneNumber, "0"));
					foreach (Usr u in CurrentPromoter.AdminUsrs)
					{
						if (u.MobileNumber.Length > 0)
							AdminPhoneNumbersDropDown.Items.Add(new ListItem(u.NickName + " (" + u.FullName + ") " + u.MobileDial, u.K.ToString()));
						else
							AdminPhoneNumbersDropDown.Items.Add(new ListItem(u.NickName + " (" + u.FullName + ") no number", u.K.ToString()));
					}
				}

				AdminUsrP.InnerHtml = "Main contact name: <b>" + CurrentPromoter.ContactName + "</b><br><table border='0' cellpadding='0' cellspacing='1'>";
				foreach (Usr u in CurrentPromoter.AdminUsrs)
				{
					AdminUsrP.InnerHtml +=
						string.Format("<tr><td><b>{0} - {1}</b>{2}</td><td>(<a href='mailto:{3}'>{3}</a>)</td></tr>",
						u.Link(), u.FullName, (u.K == this.CurrentPromoter.PrimaryUsrK ? " (primary)" : ""), u.Email);
				}
				AdminUsrP.InnerHtml += "</table>";
				
				BindAdminPanel(!Page.IsPostBack);
				GetSalesSummary();
                GetTicketSalesSummary();
            }			
		}

		private void SetupSalesStatusDropDownList()
		{
			if (Usr.Current.IsSuperAdmin || Usr.Current.IsAdmin || this.CurrentPromoter.SalesUsrK == Usr.Current.K)
			{
				this.SalesStatusDropDownList.Items.Clear();
                Utilities.AddEnumValuesToDropDownList(this.SalesStatusDropDownList, typeof(Promoter.SalesStatusEnum), false, false);
			}
		}

		private void SetupSalesEstimateDropDownList()
		{
			SalesEstimate.Items.Clear();
			SalesEstimate.Items.Add(new ListItem("", "0"));
			SalesEstimate.Items.AddRange(Promoter.SalesEstimatesAsListItemArray());
		}

		private void SetupSalesPersonsDropDownList()
		{
			if (Usr.Current.IsSuperAdmin || this.CurrentPromoter.SalesUsrK == Usr.Current.K)
			{
				UsrSet salesUsrs = Usr.GetCurrentSalesUsrsNameAndK();
				this.SalesPersonsDropDownList.Items.Clear();
				this.SalesPersonsDropDownList.Items.Add(new ListItem("NONE", "0"));
				foreach (Usr usr in salesUsrs)
				{
					this.SalesPersonsDropDownList.Items.Add(new ListItem(usr.NickName, usr.K.ToString()));
				}
			}
		}

		private void SetSalesAccountTableVisibility()
		{
			this.SalesAccountTable.Visible = Usr.Current.IsSuperAdmin || this.CurrentPromoter.SalesUsrK == Usr.Current.K;

			this.SalesUsrTD1.Visible = Usr.Current.IsSuperAdmin || this.CurrentPromoter.SalesUsrK == Usr.Current.K;
			this.SalesUsrTD2.Visible = Usr.Current.IsSuperAdmin || this.CurrentPromoter.SalesUsrK == Usr.Current.K;
			this.SalesStatusTD1.Visible = Usr.Current.IsSuperAdmin;// || Usr.Current.IsAdmin || this.CurrentPromoter.SalesUsrK == Usr.Current.K;
			this.SalesStatusTD2.Visible = Usr.Current.IsSuperAdmin;// || Usr.Current.IsAdmin || this.CurrentPromoter.SalesUsrK == Usr.Current.K;
			this.SalesStatusExpiresTD1.Visible = Usr.Current.IsSuperAdmin;
			this.SalesStatusExpiresTD2.Visible = Usr.Current.IsSuperAdmin;
		}

		private void SetEditPromoterAccessibility()
		{
			this.AdminEditPanel.Visible = Usr.Current.IsAdmin;
			//this.AdminEditPanel.Visible = Usr.Current.IsSuperAdmin || Usr.Current.IsAdmin;
			//if (!Usr.Current.IsSuperAdmin)
			//{
			//    Utilities.EnableDisableControls(AdminEditPanel, false);
			//    this.OverrideInvoiceDueDaysCheckBox.Checked = true;
			//    this.OverrideInvoiceDueDaysCheckBox.Style.Add("display", "none");
			//    this.EnableTicketsCheckBox.Enabled = false;
			//    this.DisableOverdueRedirectCheckBox.Enabled = false;
			//    this.uiEnableSuppressReminderEmailCheckBox.Enabled = false;
			//    this.OverrideAutoApplyTicketFundsToInvoicesCheckBox.Enabled = false;
			//    this.SaveEditPromoterButton.Visible = CurrentPromoter.SalesUsrK == Usr.Current.K;
			//    Utilities.EnableDisableControls(this.DiscountTextBox, CurrentPromoter.SalesUsrK == Usr.Current.K);
			//}
		}

		private void SetupNextCallTimeJavascript()
		{
			this.NextCallTime.HourTextBox.Attributes.Remove("onkeypress");
			this.NextCallTime.MinuteTextBox.Attributes.Remove("onkeypress");
			this.SnoozeDropDownList.Attributes.Remove("onchange");

			// Clear Snooze DropDownList when entering in a specific time
			this.NextCallTime.HourTextBox.Attributes.Add("onkeypress", "javascript:document.getElementById('" + this.SnoozeDropDownList.ClientID + "').selectedIndex = 0;");
			this.NextCallTime.MinuteTextBox.Attributes.Add("onkeypress", "javascript:document.getElementById('" + this.SnoozeDropDownList.ClientID + "').selectedIndex = 0;");
			// Clear NextCallTime when selecting a snooze time from the SnoozeDropDownList
			this.SnoozeDropDownList.Attributes.Add("onchange", "javascript:document.getElementById('" + NextCallTime.HourTextBox.ClientID + "').value = ''; document.getElementById('" + NextCallTime.MinuteTextBox.ClientID + "').value = '';");
		}
		#endregion
		#region BindAdminPanel
		public void BindAdminPanel(bool refresh)
		{
			CurrentPromoter = null;

			if (refresh)
			{
				InitSkeletonAccountPanel.Visible = false;
				if (CurrentPromoter.IsSkeleton || CurrentPromoter.PrimaryUsrK == 0)
				{
					InitSkeletonAccountPanel.Visible = true;
					try
					{
						InitSkeletonAccountCodeLabel.Text = CurrentPromoter.K.ToString("0000") + " - " + CurrentPromoter.AccessCodeRandom.Substring(0, 4) + " - " + CurrentPromoter.AccessCodeRandom.Substring(4, 4);
					}
					catch 
					{ }
				}
			}
			
			if (refresh)
			{
				if (CurrentPromoter.SalesNextCall > DateTime.Today)
				{
					SetNextCallOnScreen();
				}		
				SalesEstimate.SelectedValue = ((int)CurrentPromoter.SalesEstimate).ToString();
			}

			StatusP.InnerHtml = "Status: " + CurrentPromoter.EffectiveSalesStatus.ToString();
			if (CurrentPromoter.SalesUsrK > 0 && (CurrentPromoter.EffectiveSalesStatus.Equals(Promoter.SalesStatusEnum.Proactive) || CurrentPromoter.EffectiveSalesStatus.Equals(Promoter.SalesStatusEnum.Active)))
			{
				StatusP.InnerHtml += " - expires " + Cambro.Misc.Utility.FriendlyDate(CurrentPromoter.SalesStatusExpires, false, true) + ". Account manager: " + CurrentPromoter.SalesUsr.Link();
			}
			if (refresh)
				BindNotes();

			SetSalesAccountTableVisibility();
			SetEditPromoterAccessibility();

			if(refresh && (Usr.Current.IsSuperAdmin || Usr.Current.IsAdmin || this.CurrentPromoter.SalesUsrK == Usr.Current.K))
			{
				this.SalesStatusExpiresCal.Date = CurrentPromoter.SalesStatusExpires ?? DateTime.MinValue;
				this.SalesStatusDropDownList.SelectedValue = ((int)CurrentPromoter.SalesStatus).ToString();
				this.SalesPersonsDropDownList.SelectedValue = CurrentPromoter.SalesUsrK.ToString();
				// Do not have an alarm for promoters with no sales user, as per Dave 21/02/07
				this.AlarmCheckBox.Enabled = CurrentPromoter.SalesUsrK > 0;					
			}
			if (refresh && (Usr.Current.IsSuperAdmin || Usr.Current.IsAdmin || this.CurrentPromoter.SalesUsrK == Usr.Current.K))
			{
				if (CurrentPromoter.InvoiceDueDays == 0 && Usr.Current.IsAdmin)
					this.InvoiceDueDaysTextBox.Text = Vars.InvoiceDueDaysDefault.ToString();
				else
					this.InvoiceDueDaysTextBox.Text = CurrentPromoter.InvoiceDueDays.ToString();
				if (CurrentPromoter.InvoiceDueDays > 0)
					this.OverrideInvoiceDueDaysCheckBox.Checked = true;
				this.CreditLimitTextBox.Text = CurrentPromoter.CreditLimit.ToString("c");
				this.EnableTicketsCheckBox.Checked = CurrentPromoter.EnableTickets;
				this.uiEnableSuppressReminderEmailCheckBox.Checked = CurrentPromoter.SuspendReminderEmails;
				this.DisableOverdueRedirectCheckBox.Checked = CurrentPromoter.DisableOverdueRedirect;
				this.DiscountTextBox.Text = CurrentPromoter.Discount.ToString();
                this.OverrideAutoApplyTicketFundsToInvoicesCheckBox.Checked = CurrentPromoter.OverrideApplyTicketFundsToInvoices;
			}
		}
		#endregion

		#region SetNextCallOnScreen
		private void SetNextCallOnScreen()
		{
			NextCallCal.Date = CurrentPromoter.SalesNextCall;
			NextCallTime.Hour = CurrentPromoter.SalesNextCall.Hour;
			NextCallTime.Minute = CurrentPromoter.SalesNextCall.Minute;
			this.SnoozeDropDownList.SelectedIndex = 0;
		}

		#endregion

		#region BindNotes
		public void BindNotes()
		{
			#region Important Notes / Calls
			Query q = new Query(new And(new Q(SalesCall.Columns.PromoterK, CurrentPromoter.K),
										new Q(SalesCall.Columns.IsImportant, true)));
			q.OrderBy = new OrderBy(SalesCall.Columns.DateTimeStart, OrderBy.OrderDirection.Descending);
			SalesCallSet importantSalesCalls = new SalesCallSet(q);
			if (importantSalesCalls.Count == 0)
				ImportantCallsPanel.Visible = false;
			else
			{
				ImportantCallsPanel.Visible = true;
				this.ImportantCallsGridView.DataSource = importantSalesCalls;
				this.ImportantCallsGridView.DataBind();
			}
			#endregion

			#region Notes / Calls
			Query q2 = new Query(new And(new Q(SalesCall.Columns.PromoterK, CurrentPromoter.K),
										 new Or(new Q(SalesCall.Columns.IsImportant, 0),
												new Q(SalesCall.Columns.IsImportant, QueryOperator.IsNull, null))));
			q2.OrderBy = new OrderBy(SalesCall.Columns.DateTimeStart, OrderBy.OrderDirection.Descending);
			q2.TopRecords = 100;
			SalesCallSet scs = new SalesCallSet(q2);
			if (scs.Count == 0)
				CallsPanel.Visible = false;
			else
			{
				if (scs.Count > 10)
				{
					CallsDiv.Style["height"] = "200px";
					CallsDiv.Style["overflow"] = "auto";
				}
				else if (scs.Count > 5)
				{
					CallsDiv.Style["height"] = "100px";
					CallsDiv.Style["overflow"] = "auto";
				}
				CallsPanel.Visible = true;
				this.CallsGridView.DataSource = scs;
				this.CallsGridView.DataBind();

				//StringBuilder sb = new StringBuilder();
				//foreach (SalesCall sc in scs)
				//{
				//    sb.Append("<p>" + SalesCallToString(sc) + "</p>");
				//}
				//CallsPh.Controls.Clear();
				//CallsPh.Controls.Add(new LiteralControl(sb.ToString()));
			}
			#endregion
		}
		#endregion

		#region SalesSummaryPanel
		private void GetSalesSummary()
		{
			DateTime fromDate = Utilities.GetStartOfMonth(DateTime.Now.AddMonths(-6));
			DateTime toDate = Utilities.GetStartOfMonth(DateTime.Now.AddMonths(1));

			Query salesSummaryQuery = new Query(new And(new Q(Invoice.Columns.PromoterK, this.CurrentPromoter.K),
														new Q(Invoice.Columns.CreatedDateTime, QueryOperator.GreaterThanOrEqualTo, fromDate),
														new Q(Invoice.Columns.CreatedDateTime, QueryOperator.LessThan, toDate)));

			salesSummaryQuery.Columns = new ColumnSet(Invoice.Columns.PromoterK);
			salesSummaryQuery.ExtraSelectElements.Add("Amount", "SUM(Price)");
			salesSummaryQuery.ExtraSelectElements.Add("Date", "CONVERT(datetime,'1/' + CONVERT(varchar(2),MONTH(CreatedDateTime)) + '/' + CONVERT(varchar(4),Year(CreatedDateTime)))");
			salesSummaryQuery.OrderBy = new OrderBy("YEAR(CreatedDateTime) desc, MONTH(CreatedDateTime) desc");
			salesSummaryQuery.GroupBy = new GroupBy("MONTH(CreatedDateTime), YEAR(CreatedDateTime), PromoterK");

			InvoiceSet invoices = new InvoiceSet(salesSummaryQuery);

			//toDate = toDate.AddMonths(-1);

			#region Table
			SalesSummaryTable.Rows.Clear();
			#endregion
			
			#region Header Row
			HtmlTableRow headerRow = new HtmlTableRow();

			headerRow.Attributes.Add("class", "dataGridHeader");
			SalesSummaryTable.Rows.Add(headerRow);
			List<HtmlTableCell> headerTableCells = new List<HtmlTableCell>();
			headerTableCells.Add(new HtmlTableCell("th"));
			headerTableCells.Add(new HtmlTableCell("th"));

			headerTableCells[0].InnerHtml = "Month";
			headerTableCells[1].InnerHtml = "<nobr>Amount (ex VAT)</nobr>";

			foreach (HtmlTableCell tc in headerTableCells)
				headerRow.Cells.Add(tc);
			#endregion

			#region Data Rows
			int numberOfMonths = (toDate.Year - fromDate.Year) * 12 + toDate.Month - fromDate.Month;
			for (int i = 0; i < numberOfMonths; i++)
			{
				HtmlTableRow dataRow = new HtmlTableRow();
				SalesSummaryTable.Rows.Add(dataRow);
				if (i % 2 == 1)
					dataRow.Attributes.Add("class", "dataGridItem");
				else
					dataRow.Attributes.Add("class", "dataGridAltItem");

				List<HtmlTableCell> dataTableCells = new List<HtmlTableCell>();
				dataTableCells.Add(new HtmlTableCell("td"));
				dataTableCells.Add(new HtmlTableCell("td"));
				foreach (HtmlTableCell tc in dataTableCells)
					dataRow.Cells.Add(tc);

				dataTableCells[0].InnerHtml = "";
				dataTableCells[1].InnerHtml = "";

				if (Utilities.GetStartOfMonth(fromDate.AddMonths(i)) == Utilities.GetStartOfMonth(DateTime.Now))
				{
					dataTableCells[0].InnerHtml = "<small>";
					dataTableCells[1].InnerHtml = "<small>";
				}
				
				dataTableCells[0].InnerHtml += fromDate.AddMonths(i).ToString("MMM yyyy");

				dataTableCells[1].Align = "right";
				invoices.Reset();
				bool found = false;
				foreach (Invoice invoice in invoices)
				{
					if (Utilities.GetStartOfMonth((DateTime)invoice.ExtraSelectElements["Date"]) == Utilities.GetStartOfMonth(fromDate.AddMonths(i)))
					{
						dataTableCells[1].InnerHtml += Convert.ToDouble(invoice.ExtraSelectElements["Amount"]).ToString("c");
						found = true;
						break;
					}
				}

				if(!found)
