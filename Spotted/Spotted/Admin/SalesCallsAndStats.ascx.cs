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

namespace Spotted.Admin
{
	public partial class SalesCallsAndStats : AdminUserControl
	{
		private List<UsrDataHolder> salesUsrList;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.IsPostBack)
			{
				// Setup sales persons drop down list
				SetupSalesPersonsDropDownList();

				// Default is today + 6 days previous = 7 days
				this.FromDateCal.Date = DateTime.Now.AddDays(-6);
				this.ToDateCal.Date = DateTime.Now;
			}

			this.DateRangeLabel.Visible = false;
			this.DateRangeValueLabel.Visible = false;
		}

		#region Properties
		public List<UsrDataHolder> SalesUsrs
		{
			get
			{
				if (salesUsrList == null || salesUsrList.Count == 0)
				{
					salesUsrList = new List<UsrDataHolder>();
					UsrSet salesUsrSet = Usr.GetCurrentSalesUsrsNameAndKBySalesTeam();
					foreach (Usr salesUsr in salesUsrSet)
						salesUsrList.Add(new UsrDataHolder(salesUsr));
				}
				return salesUsrList;
			}
			set
			{
				salesUsrList = value;
			}
		}
		#endregion

		#region SaveViewState()
		protected override object SaveViewState()
		{
			this.ViewState["SalesUsrs"] = SalesUsrs;
			return base.SaveViewState();
		}
		#endregion
		#region LoadViewState()
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			if (this.ViewState["SalesUsrs"] != null) SalesUsrs = (List<UsrDataHolder>)this.ViewState["SalesUsrs"];
		}
		#endregion

		#region Setup DropDownLists
		private void SetupSalesPersonsDropDownList()
		{
			this.SalesPersonsDropDownList.Items.Clear();
			this.SalesPersonsDropDownList.Items.Add(new ListItem("ALL", "0"));

			int salesTeam = 0;
			foreach (UsrDataHolder usr in SalesUsrs)
			{
				if (salesTeam != usr.SalesTeam)
				{
					salesTeam = usr.SalesTeam;
					this.SalesPersonsDropDownList.Items.Add(new ListItem("Sales Team " + salesTeam.ToString(), "team" + salesTeam.ToString()));
				}
				this.SalesPersonsDropDownList.Items.Add(new ListItem("-" + usr.FirstName, usr.K.ToString()));
			}
		}
		#endregion

		#region Set Selection
		private enum Selection{
			Daily = 1,
			Monthly = 2,
			Selection = 3
		}

		private void SetSelection(Selection mySelection)
		{
			
		}
		#endregion

		#region Page Events
		protected void MyTodayButton_Click(object sender, EventArgs e)
		{
			try
			{
				this.SalesPersonsDropDownList.SelectedValue = Usr.Current.K.ToString();
			}
			catch (Exception)
			{
				NotSalesPersonCustomValidator.IsValid = false;
			}
			if (NotSalesPersonCustomValidator.IsValid)
			{
				this.OverrideDateCheckBox.Checked = false;
				GenerateSalesCallsReport(DateTime.Now, DateTime.Now, DateGrouping.Daily, Usr.Current.K.ToString());
			}
		}

		protected void MyMonthButton_Click(object sender, EventArgs e)
		{
			try
			{
				this.SalesPersonsDropDownList.SelectedValue = Usr.Current.K.ToString();
			}
			catch (Exception)
			{
				NotSalesPersonCustomValidator.IsValid = false;
			}
			if (NotSalesPersonCustomValidator.IsValid)
			{
				this.OverrideDateCheckBox.Checked = false;
				GenerateSalesCallsReport(Utilities.GetStartOfMonth(DateTime.Now), Utilities.GetEndOfMonth(DateTime.Now), DateGrouping.Monthly, Usr.Current.K.ToString());
			}
		}

		protected void TeamTodayButton_Click(object sender, EventArgs e)
		{
			if (Usr.Current.SalesTeam <= 0)
			{
				NotSalesPersonCustomValidator.IsValid = false;
			}
			else
			{
				this.SalesPersonsDropDownList.SelectedValue = "team" + Usr.Current.SalesTeam.ToString();
				this.OverrideDateCheckBox.Checked = false;
				GenerateSalesCallsReport(DateTime.Now, DateTime.Now, DateGrouping.Daily, "team" + Usr.Current.SalesTeam.ToString());
			}
		}

		protected void TeamMonthButton_Click(object sender, EventArgs e)
		{
			if (Usr.Current.SalesTeam <= 0)
			{
				NotSalesPersonCustomValidator.IsValid = false;
			}
			else
			{
				this.SalesPersonsDropDownList.SelectedValue = "team" + Usr.Current.SalesTeam.ToString();
				this.OverrideDateCheckBox.Checked = false;
				GenerateSalesCallsReport(Utilities.GetStartOfMonth(DateTime.Now), Utilities.GetEndOfMonth(DateTime.Now), DateGrouping.Monthly, "team" + Usr.Current.SalesTeam.ToString());
			}
		}

		protected void SalesCallsDailyButton_Click(object sender, EventArgs e)
		{
			GetDatesAndRunSalesCallsReport(DateGrouping.Daily);
		}

		protected void SalesCallsWeeklyButton_Click(object sender, EventArgs e)
		{
			GetDatesAndRunSalesCallsReport(DateGrouping.Weekly);
		}

		protected void SalesCallsMonthlyButton_Click(object sender, EventArgs e)
		{
			GetDatesAndRunSalesCallsReport(DateGrouping.Monthly);
		}

		protected void SalesStatsButton_Click(object sender, EventArgs e)
		{
			DateTime fromDate = FromDateCal.Date;
			DateTime toDate = ToDateCal.Date;

			if (!this.OverrideDateCheckBox.Checked)
			{
				// Default is today + 6 days previous = 7 days
				fromDate = DateTime.Now.AddDays(-6);
				toDate = DateTime.Now;

				FromDateCal.Date = fromDate;
				ToDateCal.Date = toDate;
			}
			
			GenerateSalesStatsReport(fromDate, toDate, this.SalesPersonsDropDownList.SelectedValue);
		}
		#endregion

		#region Methods
		private void GetDatesAndRunSalesCallsReport(DateGrouping dateGrouping)
		{
			DateTime fromDate = FromDateCal.Date;
			DateTime toDate = ToDateCal.Date;

			// If dates arent overridden, use defaults
			if (!this.OverrideDateCheckBox.Checked)
			{
				if (dateGrouping.Equals(DateGrouping.Daily))
				{
					// Default is today + 6 days previous = 7 days
					fromDate = DateTime.Now.AddDays(-6);
					toDate = DateTime.Now;
				}
				else if (dateGrouping.Equals(DateGrouping.Weekly))
				{
					// Default is this week + 5 weeks previous = 6 weeks
					fromDate = Utilities.GetStartOfWeek(DateTime.Now).AddDays(-35);
					toDate = Utilities.GetEndOfWeek(DateTime.Now);
				}
				else if (dateGrouping.Equals(DateGrouping.Monthly))
				{
					// Default is this month + 5 months previous = 6 months
					fromDate = Utilities.GetStartOfMonth(DateTime.Now).AddMonths(-5);
					toDate = Utilities.GetEndOfMonth(DateTime.Now);	
				}

				FromDateCal.Date = fromDate;
				ToDateCal.Date = toDate;
			}
			if (fromDate > DateTime.MinValue && toDate > DateTime.MinValue)
			{
				GenerateSalesCallsReport(fromDate, toDate, dateGrouping, this.SalesPersonsDropDownList.SelectedValue);
			}
		}

		private void GenerateSalesCallsReport(DateTime fromDate, DateTime toDate, DateGrouping dateGrouping)
		{
			// salesUsrK of 0 means all sales usrs
			GenerateSalesCallsReport(fromDate, toDate, dateGrouping, "0");
		}

		private void GenerateSalesCallsReport(DateTime fromDate, DateTime toDate, DateGrouping dateGrouping, string salesUsrK)
		{
			Page.Validate("");
			if (Page.IsValid)
			{
				this.DateRangeValueLabel.Text = fromDate.ToString("dd/MM/yy") + " to " + toDate.ToString("dd/MM/yy");
				this.DateRangeLabel.Visible = true;
				this.DateRangeValueLabel.Visible = true;

				int startOfWeek = 1;	// Sunday = 0, Monday = 1
				fromDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day);
				toDate = new DateTime(toDate.Year, toDate.Month, toDate.Day);

				if (toDate >= fromDate)
				{
					int numberOfDateGroupings = 0;
					if (dateGrouping.Equals(DateGrouping.Daily))
					{
						toDate = new DateTime(toDate.Year, toDate.Month, toDate.Day).AddDays(1);
						numberOfDateGroupings = ((TimeSpan)(toDate - fromDate)).Days;
					}
					else if (dateGrouping.Equals(DateGrouping.Weekly))
					{
						fromDate = Utilities.GetStartOfWeek(fromDate);
						toDate = Utilities.GetEndOfWeek(toDate);
						toDate = new DateTime(toDate.Year, toDate.Month, toDate.Day).AddDays(1);
						numberOfDateGroupings = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(((TimeSpan)(toDate - fromDate)).Days) / 7d));
					}
					else if (dateGrouping.Equals(DateGrouping.Monthly))
					{
						fromDate = Utilities.GetStartOfMonth(fromDate);
						toDate = Utilities.GetEndOfMonth(toDate);
						toDate = new DateTime(toDate.Year, toDate.Month, toDate.Day).AddDays(1);
						numberOfDateGroupings = (toDate.AddDays(-1).Year - fromDate.Year) * 12 + (toDate.AddDays(-1).Month - fromDate.Month) + 1;
					}

					List<UsrDataHolder> selectedSalesUsrs = SalesUsrs;
					if (salesUsrK != "0")
					{
						selectedSalesUsrs = new List<UsrDataHolder>();
						if (salesUsrK.IndexOf("team") == 0)
						{
							UsrSet salesTeam = Usr.GetCurrentSalesUsrsNameAndK(Convert.ToInt32(salesUsrK.Replace("team", "")));
							foreach (Usr salesUsr in salesTeam)
							{
								selectedSalesUsrs.Add(new UsrDataHolder(salesUsr));
							}
						}
						else
						{
							selectedSalesUsrs.Add(SalesUsrs[GetSalesPersonIndexNumber(SalesUsrs, Convert.ToInt32(salesUsrK))]);
						}
					}

					if (selectedSalesUsrs.Count > 0)
					{
						Q salesUsrsQueryCondition = new Q(SalesCall.Columns.UsrK, selectedSalesUsrs[0].K);
						Q invoiceSalesUsrsQueryCondition = new Q(Invoice.Columns.SalesUsrK, selectedSalesUsrs[0].K);
						Q promoterAddedByQueryCondition = new Q(Promoter.Columns.AddedByUsrK, selectedSalesUsrs[0].K);

						for (int i = 1; i < selectedSalesUsrs.Count; i++)
						{
							promoterAddedByQueryCondition = new Or(promoterAddedByQueryCondition,
															 new Q(Promoter.Columns.AddedByUsrK, selectedSalesUsrs[i].K));

							salesUsrsQueryCondition = new Or(salesUsrsQueryCondition,
															 new Q(SalesCall.Columns.UsrK, selectedSalesUsrs[i].K));

							invoiceSalesUsrsQueryCondition = new Or(invoiceSalesUsrsQueryCondition,
																	new Q(Invoice.Columns.SalesUsrK, selectedSalesUsrs[i].K));
						}

						Q salesCallDateRangeQueryCondition = new And(new Q(SalesCall.Columns.IsCall, true),
																	 new Q(SalesCall.Columns.DateTimeStart, QueryOperator.GreaterThanOrEqualTo, fromDate),
																	 new Q(SalesCall.Columns.DateTimeStart, QueryOperator.LessThan, toDate));

						Q promoterDateRangeQueryCondition = new And(
																new Q(Promoter.Columns.AddedMethod, Promoter.AddedMedhods.SalesUser), 
																new Q(Promoter.Columns.DateTimeSignUp, QueryOperator.GreaterThanOrEqualTo, fromDate),
																new Q(Promoter.Columns.DateTimeSignUp, QueryOperator.LessThan, toDate));

						#region Sales Calls: Total Money
						Query totalSalesAmountQuery = new Query(new And(invoiceSalesUsrsQueryCondition,
																		new Q(Invoice.Columns.SalesUsrAmount, QueryOperator.NotEqualTo, 0),
																		new Or(new And(new Q(Invoice.Columns.Type, Invoice.Types.Invoice),
																					   new Q(Invoice.Columns.Paid, true),
																					   new Q(Invoice.Columns.PaidDateTime, QueryOperator.GreaterThanOrEqualTo, fromDate),
																					   new Q(Invoice.Columns.PaidDateTime, QueryOperator.LessThan, toDate),
                                                                                       new Q(Usr.Columns.SalesTeam, QueryOperator.NotEqualTo, Usr.SalesTeams.CorporateSalesTeam)),
                                                                               new And(new Q(Invoice.Columns.Type, Invoice.Types.Invoice),
                                                                                       new Q(Invoice.Columns.TaxDateTime, QueryOperator.GreaterThanOrEqualTo, fromDate),
																					   new Q(Invoice.Columns.TaxDateTime, QueryOperator.LessThan, toDate),
                                                                                       new Q(Usr.Columns.SalesTeam, Usr.SalesTeams.CorporateSalesTeam)),
																			   new And(new Q(Invoice.Columns.Type, Invoice.Types.Credit),
																					   new Q(Invoice.Columns.CreatedDateTime, QueryOperator.GreaterThanOrEqualTo, fromDate),
																					   new Q(Invoice.Columns.CreatedDateTime, QueryOperator.LessThan, toDate))))); 
						
						totalSalesAmountQuery.TableElement = new Join(Invoice.Columns.SalesUsrK, Usr.Columns.K);
						totalSalesAmountQuery.ExtraSelectElements.Add("TotalSales", "SUM(SalesUsrAmount)");
						totalSalesAmountQuery.Columns = new ColumnSet(Invoice.Columns.SalesUsrK);
						if (dateGrouping.Equals(DateGrouping.Daily))
						{
							totalSalesAmountQuery.ExtraSelectElements.Add("Date", "CONVERT(datetime,CONVERT(varchar(2),DAY(IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime))) + '/' + CONVERT(varchar(2),MONTH(IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime))) + '/' + CONVERT(varchar(4),Year(IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime))))");
							totalSalesAmountQuery.OrderBy = new OrderBy("[Usr].[FirstName], YEAR(IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)) desc, MONTH(IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)) desc, DAY(IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)) desc");
							totalSalesAmountQuery.GroupBy = new GroupBy("[Usr].[FirstName], DAY(IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)), MONTH(IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)), YEAR(IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)), [Invoice].[SalesUsrK]");
						}
						else if (dateGrouping.Equals(DateGrouping.Weekly))
						{
							totalSalesAmountQuery.ExtraSelectElements.Add("Date", "CONVERT(dateTime,(CONVERT(varchar(2),DAY(DateAdd(day, -1 * datepart(dw, IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)) + 1, IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)))) + '/' + CONVERT(varchar(2),MONTH(DateAdd(day, -1 * datepart(dw, IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)) + 1, IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)))) + '/' + CONVERT(varchar(4),YEAR(DateAdd(day, -1 * datepart(dw, IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)) + 1, IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime))))))");
							totalSalesAmountQuery.OrderBy = new OrderBy("[Usr].[FirstName], CONVERT(dateTime,(CONVERT(varchar(2),DAY(DateAdd(day, -1 * datepart(dw, IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)) + 1, IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)))) + '/' + CONVERT(varchar(2),MONTH(DateAdd(day, -1 * datepart(dw, IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)) + 1, IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)))) + '/' + CONVERT(varchar(4),YEAR(DateAdd(day, -1 * datepart(dw, IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)) + 1, IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)))))) desc");
							totalSalesAmountQuery.GroupBy = new GroupBy("[Usr].[FirstName], CONVERT(dateTime,(CONVERT(varchar(2),DAY(DateAdd(day, -1 * datepart(dw, IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)) + 1, IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)))) + '/' + CONVERT(varchar(2),MONTH(DateAdd(day, -1 * datepart(dw, IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)) + 1, IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)))) + '/' + CONVERT(varchar(4),YEAR(DateAdd(day, -1 * datepart(dw, IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)) + 1, IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)))))), [Invoice].[SalesUsrK]");
						}
						else if (dateGrouping.Equals(DateGrouping.Monthly))
						{
							totalSalesAmountQuery.ExtraSelectElements.Add("Date", "CONVERT(datetime,'1/' + CONVERT(varchar(2),MONTH(IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime))) + '/' + CONVERT(varchar(4),Year(IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime))))");
							totalSalesAmountQuery.OrderBy = new OrderBy("[Usr].[FirstName], YEAR(IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)) desc, MONTH(IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)) desc");
							totalSalesAmountQuery.GroupBy = new GroupBy("[Usr].[FirstName], MONTH(IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)), YEAR(IsNULL(CASE WHEN Usr.SalesTeam = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN TaxDateTime ELSE PaidDateTime END, CreatedDateTime)), [Invoice].[SalesUsrK]");
						}

						InvoiceSet salesInvoices = new InvoiceSet(totalSalesAmountQuery);

						#endregion

						#region Sales Calls: SalesUsrK, Minutes, and Total calls
						Query salesCallMinutesQuery = new Query(new And(salesCallDateRangeQueryCondition,
																		salesUsrsQueryCondition));
						salesCallMinutesQuery.ExtraSelectElements.Add("Minutes", "SUM([SalesCall].[Duration])");
						salesCallMinutesQuery = PopulateSalesCallQuery(salesCallMinutesQuery, dateGrouping, startOfWeek);

						SalesCallSet salesCallsMinutes = new SalesCallSet(salesCallMinutesQuery);
						#endregion

						#region Promoters: New leads
						Query promotersNewLeadsQuery = new Query(new And(promoterDateRangeQueryCondition,
																		  promoterAddedByQueryCondition));
						promotersNewLeadsQuery = PopulatePromotersQuery(promotersNewLeadsQuery, dateGrouping, startOfWeek);
						PromoterSet promotersNewLeads = new PromoterSet(promotersNewLeadsQuery);
						#endregion

						#region Sales Calls: New leads
						Query salesCallNewLeadsQuery = new Query(new And(salesCallDateRangeQueryCondition,
																		  salesUsrsQueryCondition,
																		  new Q(SalesCall.Columns.IsCallToNewLead, true)));
						salesCallNewLeadsQuery = PopulateSalesCallQuery(salesCallNewLeadsQuery, dateGrouping, startOfWeek);
						SalesCallSet salesCallsNewLeads = new SalesCallSet(salesCallNewLeadsQuery);
						#endregion

						#region Sales Calls: Effective
						Query salesCallEffectiveQuery = new Query(new And(salesCallDateRangeQueryCondition,
																		  salesUsrsQueryCondition,
																		  new Q(SalesCall.Columns.Effective, true)));
						salesCallEffectiveQuery = PopulateSalesCallQuery(salesCallEffectiveQuery, dateGrouping, startOfWeek);
						SalesCallSet salesCallsEffective = new SalesCallSet(salesCallEffectiveQuery);
						#endregion

						#region Sales Calls: Cold
						Query salesCallColdQuery = new Query(new And(salesCallDateRangeQueryCondition,
																			  salesUsrsQueryCondition,
																			  new Q(SalesCall.Columns.Type, SalesCall.Types.Cold)));
						salesCallColdQuery = PopulateSalesCallQuery(salesCallColdQuery, dateGrouping, startOfWeek);
						SalesCallSet salesCallsCold = new SalesCallSet(salesCallColdQuery);
						#endregion

						#region Sales Calls: Followup
						Query salesCallFollowupQuery = new Query(new And(salesCallDateRangeQueryCondition,
																		salesUsrsQueryCondition,
																		new Q(SalesCall.Columns.Type, SalesCall.Types.ProactiveFollowUp)));
						salesCallFollowupQuery = PopulateSalesCallQuery(salesCallFollowupQuery, dateGrouping, startOfWeek);
						SalesCallSet salesCallsFollowup = new SalesCallSet(salesCallFollowupQuery);
						#endregion

						#region Sales Calls: Active
						Query salesCallActiveQuery = new Query(new And(salesCallDateRangeQueryCondition,
																	  salesUsrsQueryCondition,
																	  new Q(SalesCall.Columns.Type, SalesCall.Types.Active)));
						salesCallActiveQuery = PopulateSalesCallQuery(salesCallActiveQuery, dateGrouping, startOfWeek);
						SalesCallSet salesCallsActive = new SalesCallSet(salesCallActiveQuery);
						#endregion

						#region Table
						this.SalesStatsResultsTable.Visible = false;
						this.SalesCallsResultsTable.Visible = true;
						HtmlTable salesCallsTable = this.SalesCallsResultsTable;
						salesCallsTable.Rows.Clear();
						#endregion

						#region Header Rows
						HtmlTableRow headerRow1 = new HtmlTableRow();

						headerRow1.Attributes.Add("class", "dataGrid1stHeader");
						salesCallsTable.Rows.Add(headerRow1);
						HtmlTableCell[] header1TableCells = new HtmlTableCell[6];
						header1TableCells[0] = new HtmlTableCell("td");
						header1TableCells[0].RowSpan = 2;
						header1TableCells[0].InnerHtml = "Date";

						for (int i = 1; i < header1TableCells.Length; i++)
						{
							header1TableCells[i] = new HtmlTableCell("th");
							header1TableCells[i].ColSpan = selectedSalesUsrs.Count;
							header1TableCells[i].Align = "center";
							header1TableCells[i].Attributes.Add("class", "dataGridColumnDivider");
						}

						header1TableCells[1].InnerHtml = "Total sales";
						
						header1TableCells[2].InnerHtml = "<b>Total calls</b>";
						header1TableCells[3].InnerHtml = "Minutes per call";
						
						header1TableCells[4].InnerHtml = "<b>New leads</b>";
						header1TableCells[5].InnerHtml = "Calls&nbsp;to new&nbsp;leads";

						//header1TableCells[4].InnerHtml = "<nobr>Effective calls</nobr>";
						//header1TableCells[7].InnerHtml = "<nobr>Cold (%)</nobr>";
						//header1TableCells[8].InnerHtml = "<nobr>Followup (%)</nobr>";
						//header1TableCells[9].InnerHtml = "<nobr>Active (%)</nobr>";

						foreach (HtmlTableCell tc in header1TableCells)
							headerRow1.Cells.Add(tc);

						HtmlTableRow headerRow2 = new HtmlTableRow();
						headerRow2.Attributes.Add("class", "dataGrid2ndHeader");
						salesCallsTable.Rows.Add(headerRow2);

						HtmlTableCell[] header2TableCells = new HtmlTableCell[selectedSalesUsrs.Count * (header1TableCells.Length - 1)];
						for (int i = 0; i < header2TableCells.Length; i++)
						{
							header2TableCells[i] = new HtmlTableCell("th");
							header2TableCells[i].InnerHtml = "<b>" + selectedSalesUsrs[i % selectedSalesUsrs.Count].FirstName + "</b>";
							header2TableCells[i].Align = "right";
							header2TableCells[i].Width = "10";


							if (i % selectedSalesUsrs.Count == 0)
							{
								header2TableCells[i].Attributes.Add("class", "dataGridColumnDivider");
							}

							headerRow2.Cells.Add(header2TableCells[i]);
						}
						#endregion

						#region Data Rows
						HtmlTableCell[,] dataTableCells = new HtmlTableCell[numberOfDateGroupings, selectedSalesUsrs.Count * (header1TableCells.Length - 1) + 1];
						for (int i = 0; i < numberOfDateGroupings; i++)
						{
							dataTableCells[i, 0] = new HtmlTableCell();
							dataTableCells[i, 0].InnerHtml = "<nobr>";
							if (dateGrouping.Equals(DateGrouping.Daily))
								dataTableCells[i, 0].InnerHtml += String.Format("{0:ddd'&nbsp;'d'&nbsp;'MMM}", toDate.AddDays(-1 * (i + 1)));
							else if (dateGrouping.Equals(DateGrouping.Weekly))
							{
								dataTableCells[i, 0].InnerHtml += toDate.AddDays(-7 * (i + 1)).ToString("dd/MM/yy") + "&nbsp;-&nbsp;" + toDate.AddDays(-7 * (i + 1) + 6).ToString("dd/MM/yy");
							}
							else if (dateGrouping.Equals(DateGrouping.Monthly))
							{
								dataTableCells[i, 0].InnerHtml += String.Format("{0:MMM'&nbsp;'yy}", toDate.AddMonths(-1 * (i + 1)));
							}
							dataTableCells[i, 0].InnerHtml += "</nobr>";
						}

						foreach (Invoice salesInvoice in salesInvoices)
						{
							try
							{
								int i = GetSalesPersonIndexNumber(selectedSalesUsrs, salesInvoice.SalesUsrK);

								int dateGroupingsFromTop = 0;
								if (dateGrouping.Equals(DateGrouping.Daily))
									dateGroupingsFromTop = ((TimeSpan)(toDate - ((DateTime)salesInvoice.ExtraSelectElements["Date"]))).Days;
								else if (dateGrouping.Equals(DateGrouping.Weekly))
									dateGroupingsFromTop = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(((TimeSpan)(toDate - ((DateTime)salesInvoice.ExtraSelectElements["Date"]))).Days) / 7d));
								else if (dateGrouping.Equals(DateGrouping.Monthly))
								{
									dateGroupingsFromTop = (toDate.AddDays(-1).Year - ((DateTime)salesInvoice.ExtraSelectElements["Date"]).Year) * 12 + (toDate.AddDays(-1).Month - ((DateTime)salesInvoice.ExtraSelectElements["Date"]).Month) + 1;
								}

								dataTableCells[dateGroupingsFromTop - 1, i + 1] = new HtmlTableCell();
								dataTableCells[dateGroupingsFromTop - 1, i + 1].InnerHtml = "<nobr>" + Convert.ToDouble(salesInvoice.ExtraSelectElements["TotalSales"]).ToString("£#,##0") + "</nobr>";
							}
							catch (Exception ex)
							{
								string x = ex.Message;
							}
						}

						foreach (SalesCall salesCall in salesCallsMinutes)
						{
							try
							{
								int i = GetSalesPersonIndexNumber(selectedSalesUsrs, salesCall.UsrK);
								
								int dateGroupingsFromTop = 0;
								if (dateGrouping.Equals(DateGrouping.Daily))
									dateGroupingsFromTop = ((TimeSpan)(toDate - ((DateTime)salesCall.ExtraSelectElements["Date"]))).Days;
								else if (dateGrouping.Equals(DateGrouping.Weekly))
									dateGroupingsFromTop = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(((TimeSpan)(toDate - ((DateTime)salesCall.ExtraSelectElements["Date"]))).Days) / 7d));
								else if (dateGrouping.Equals(DateGrouping.Monthly))
								{
									dateGroupingsFromTop = (toDate.AddDays(-1).Year - ((DateTime)salesCall.ExtraSelectElements["Date"]).Year) * 12 + (toDate.AddDays(-1).Month - ((DateTime)salesCall.ExtraSelectElements["Date"]).Month) + 1;
								}

								dataTableCells[dateGroupingsFromTop - 1, selectedSalesUsrs.Count + i + 1] = new HtmlTableCell();
								dataTableCells[dateGroupingsFromTop - 1, selectedSalesUsrs.Count + i + 1].Style["font-weight"] = "bold";
								dataTableCells[dateGroupingsFromTop - 1, selectedSalesUsrs.Count + i + 1].InnerHtml = Convert.ToInt32(salesCall.ExtraSelectElements["TotalCalls"]).ToString("#,##0");

								
								try
								{
									double minutesPerCall = Convert.ToDouble(salesCall.ExtraSelectElements["Minutes"]) / Convert.ToDouble(salesCall.ExtraSelectElements["TotalCalls"]);
									int minutes = (int)Math.Floor(minutesPerCall);
									double minuteFraction = minutesPerCall - minutes;
									double seconds = minuteFraction * 60;


									dataTableCells[dateGroupingsFromTop - 1, selectedSalesUsrs.Count * 2 + i + 1] = new HtmlTableCell();
									dataTableCells[dateGroupingsFromTop - 1, selectedSalesUsrs.Count * 2 + i + 1].InnerHtml = minutes.ToString("0") + ":" + seconds.ToString("00");
								}
								catch (Exception ex)
								{
									string x = ex.Message;
								}

								

								
							}
							catch (Exception ex)
							{
								string x = ex.Message;
							}
						}

						foreach (Promoter promoter in promotersNewLeads)
						{
							try
							{
								int i = GetSalesPersonIndexNumber(selectedSalesUsrs, promoter.AddedByUsrK);

								int dateGroupingsFromTop = 0;
								if (dateGrouping.Equals(DateGrouping.Daily))
									dateGroupingsFromTop = ((TimeSpan)(toDate - ((DateTime)promoter.ExtraSelectElements["Date"]))).Days;
								else if (dateGrouping.Equals(DateGrouping.Weekly))
									dateGroupingsFromTop = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(((TimeSpan)(toDate - ((DateTime)promoter.ExtraSelectElements["Date"]))).Days) / 7d));
								else if (dateGrouping.Equals(DateGrouping.Monthly))
								{
									dateGroupingsFromTop = (toDate.AddDays(-1).Year - ((DateTime)promoter.ExtraSelectElements["Date"]).Year) * 12 + (toDate.AddDays(-1).Month - ((DateTime)promoter.ExtraSelectElements["Date"]).Month) + 1;
								}

								dataTableCells[dateGroupingsFromTop - 1, selectedSalesUsrs.Count * 3 + i + 1] = new HtmlTableCell();
								dataTableCells[dateGroupingsFromTop - 1, selectedSalesUsrs.Count * 3 + i + 1].Style["font-weight"] = "bold";
								dataTableCells[dateGroupingsFromTop - 1, selectedSalesUsrs.Count * 3 + i + 1].InnerHtml = Convert.ToInt32(promoter.ExtraSelectElements["TotalPromoters"]).ToString("#,##0");
							}
							catch (Exception ex)
							{
								string x = ex.Message;
							}
						}

						foreach (SalesCall salesCall in salesCallsNewLeads)
						{
							try
							{
								int i = GetSalesPersonIndexNumber(selectedSalesUsrs, salesCall.UsrK);

								int dateGroupingsFromTop = 0;
								if (dateGrouping.Equals(DateGrouping.Daily))
									dateGroupingsFromTop = ((TimeSpan)(toDate - ((DateTime)salesCall.ExtraSelectElements["Date"]))).Days;
								else if (dateGrouping.Equals(DateGrouping.Weekly))
									dateGroupingsFromTop = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(((TimeSpan)(toDate - ((DateTime)salesCall.ExtraSelectElements["Date"]))).Days) / 7d));
								else if (dateGrouping.Equals(DateGrouping.Monthly))
								{
									dateGroupingsFromTop = (toDate.AddDays(-1).Year - ((DateTime)salesCall.ExtraSelectElements["Date"]).Year) * 12 + (toDate.AddDays(-1).Month - ((DateTime)salesCall.ExtraSelectElements["Date"]).Month) + 1;
								}


								dataTableCells[dateGroupingsFromTop - 1, selectedSalesUsrs.Count * 4 + i + 1] = new HtmlTableCell();
								dataTableCells[dateGroupingsFromTop - 1, selectedSalesUsrs.Count * 4 + i + 1].InnerHtml = Convert.ToInt32(salesCall.ExtraSelectElements["TotalCalls"]).ToString("#,##0");



							}
							catch (Exception ex)
							{
								string x = ex.Message;
							}
						}

						//foreach (SalesCall salesCall in salesCallsEffective)
						//{
						//    try
						//    {
						//        int i = GetSalesPersonIndexNumber(selectedSalesUsrs, salesCall.UsrK);

						//        int dateGroupingsFromTop = 0;
						//        if (dateGrouping.Equals(DateGrouping.Daily))
						//            dateGroupingsFromTop = ((TimeSpan)(toDate - ((DateTime)salesCall.ExtraSelectElements["Date"]))).Days;
						//        else if (dateGrouping.Equals(DateGrouping.Weekly))
						//            dateGroupingsFromTop = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(((TimeSpan)(toDate - ((DateTime)salesCall.ExtraSelectElements["Date"]))).Days) / 7d));
						//        else if (dateGrouping.Equals(DateGrouping.Monthly))
						//        {
						//            dateGroupingsFromTop = (toDate.AddDays(-1).Year - ((DateTime)salesCall.ExtraSelectElements["Date"]).Year) * 12 + (toDate.AddDays(-1).Month - ((DateTime)salesCall.ExtraSelectElements["Date"]).Month) + 1;
						//        }

						//        dataTableCells[dateGroupingsFromTop - 1, selectedSalesUsrs.Count * 5 + i + 1] = new HtmlTableCell();
						//        dataTableCells[dateGroupingsFromTop - 1, selectedSalesUsrs.Count * 5 + i + 1].InnerHtml = Convert.ToInt32(salesCall.ExtraSelectElements["TotalCalls"]).ToString("#,##0");
						//    }
						//    catch (Exception ex)
						//    {
						//        string x = ex.Message;
						//    }
						//}

						//LoadSalesCallPercentDataToTable(dataTableCells, selectedSalesUsrs, salesCallsCold, toDate, 6, dateGrouping);
						//LoadSalesCallPercentDataToTable(dataTableCells, selectedSalesUsrs, salesCallsFollowup, toDate, 7, dateGrouping);
						//LoadSalesCallPercentDataToTable(dataTableCells, selectedSalesUsrs, salesCallsActive, toDate, 8, dateGrouping);

						// Fill all empty table cells with zeros
						HtmlTableRow tr;
						for (int i = 0; i < numberOfDateGroupings; i++)
						{
							tr = new HtmlTableRow();
							tr.Attributes.Clear();
							for (int j = 0; j < selectedSalesUsrs.Count * (header1TableCells.Length - 1) + 1; j++)
							{
								if (dataTableCells[i, j] == null)
								{
									dataTableCells[i, j] = new HtmlTableCell();

									if (j >= 1 && j <= selectedSalesUsrs.Count)
										dataTableCells[i, j].InnerHtml = "£0";
									else if ((j > selectedSalesUsrs.Count && j <= selectedSalesUsrs.Count * 2) || (j > selectedSalesUsrs.Count * 3 && j <= selectedSalesUsrs.Count * 4))
										dataTableCells[i, j].InnerHtml = "<b>0</b>";
									else
										dataTableCells[i, j].InnerHtml = "0";

								}
								if ((selectedSalesUsrs.Count == 1 && j > 0) || j % selectedSalesUsrs.Count - 1 == 0)
								{
									dataTableCells[i, j].Attributes.Add("class", "dataGridColumnDivider");
								}
								dataTableCells[i, j].Align = "right";
								tr.Cells.Add(dataTableCells[i, j]);
							}
							if (i % 2 == 0)
								tr.Attributes.Add("class", "dataGridItem");
							else
								tr.Attributes.Add("class", "dataGridAltItem");

							salesCallsTable.Rows.Add(tr);
						}
						#endregion

						#region Footer Row
						HtmlTableRow footerRow = new HtmlTableRow();

						salesCallsTable.Rows.Add(footerRow);
						//footerRow.Attributes.Add("class", "dataGridFooter");
						HtmlTableCell footerTotalLabelTableCell = new HtmlTableCell();
						footerTotalLabelTableCell.Style.Add("border-top", "solid 1px #000000;");
						footerTotalLabelTableCell.InnerHtml = "<b>Total:</b>";
						footerTotalLabelTableCell.Align = "right";
						footerRow.Cells.Add(footerTotalLabelTableCell);

						HtmlTableCell[,] footerTotalValueTableCells = new HtmlTableCell[selectedSalesUsrs.Count, 5];
						for (int i = 0; i < 5; i++)
						{
							for (int j = 0; j < selectedSalesUsrs.Count; j++)
							{
								footerTotalValueTableCells[j, i] = new HtmlTableCell();
								footerTotalValueTableCells[j, i].Align = "right";
								if (i == 0)
								{
									footerTotalValueTableCells[j, i].Style.Add("border-top", "solid 1px #000000;");
									decimal total = 0;
									for (int k = 0; k < numberOfDateGroupings; k++)
									{
										total += Utilities.ConvertMoneyStringToDecimal(Cambro.Web.Helpers.StripHtml(dataTableCells[k, selectedSalesUsrs.Count * i + j + 1].InnerHtml));
									}
									if (i == 0)
										footerTotalValueTableCells[j, i].InnerHtml = "<b>" + total.ToString("£#,##0") + "</b>";
									else
										footerTotalValueTableCells[j, i].InnerHtml = "<b>" + total.ToString() + "</b>";
								}
								else
									footerTotalValueTableCells[j, i].InnerHtml = "&nbsp;";

								if (j % selectedSalesUsrs.Count == 0)
								{
									footerTotalValueTableCells[j, i].Attributes.Add("class", "dataGridColumnDivider");
								}

								footerRow.Cells.Add(footerTotalValueTableCells[j, i]);
							}
						}
						#endregion
					}
				}
			}
		}

		#region PopulatePromotersQuery
		private Query PopulatePromotersQuery(Query query, DateGrouping dateGrouping, int startOfWeek)
		{
			// startOfWeek: Sunday = 0, Monday = 1

			query.TableElement = new Join(Promoter.Columns.AddedByUsrK, Usr.Columns.K);
			query.Columns = new ColumnSet(Promoter.Columns.AddedByUsrK);

			//salesCallActiveQuery.OrderBy = new OrderBy("[Usr].[FirstName], YEAR([Promoter].[DateTimeSignUp]) desc, MONTH([Promoter].[DateTimeSignUp]) desc, DAY([Promoter].[DateTimeSignUp]) desc");
			//salesCallActiveQuery.GroupBy = new GroupBy("[Usr].[FirstName], DAY([Promoter].[DateTimeSignUp]), MONTH([Promoter].[DateTimeSignUp]), YEAR([Promoter].[DateTimeSignUp]), [Promoter].[AddedByUsrK], DATENAME(dw, [Promoter].[DateTimeSignUp])");
			query.ExtraSelectElements.Add("TotalPromoters", "COUNT(*)");
			if (dateGrouping.Equals(DateGrouping.Daily))
			{
				query.ExtraSelectElements.Add("Date", "CONVERT(datetime,CONVERT(varchar(2),DAY([Promoter].[DateTimeSignUp])) + '/' + CONVERT(varchar(2),MONTH([Promoter].[DateTimeSignUp])) + '/' + CONVERT(varchar(4),Year([Promoter].[DateTimeSignUp])))");
				query.OrderBy = new OrderBy("[Usr].[FirstName], YEAR([Promoter].[DateTimeSignUp]) desc, MONTH([Promoter].[DateTimeSignUp]) desc, DAY([Promoter].[DateTimeSignUp]) desc");
				query.GroupBy = new GroupBy("[Usr].[FirstName], DAY([Promoter].[DateTimeSignUp]), MONTH([Promoter].[DateTimeSignUp]), YEAR([Promoter].[DateTimeSignUp]), [Promoter].[AddedByUsrK]");
			}
			else if (dateGrouping.Equals(DateGrouping.Weekly))
			{
				query.ExtraSelectElements.Add("Date", "CONVERT(dateTime,(CONVERT(varchar(2),DAY(DateAdd(day, -1 * datepart(dw, [Promoter].[DateTimeSignUp]) + " + startOfWeek.ToString() + ", [Promoter].[DateTimeSignUp]))) + '/' + CONVERT(varchar(2),MONTH(DateAdd(day, -1 * datepart(dw, [Promoter].[DateTimeSignUp]) + " + startOfWeek.ToString() + ", [Promoter].[DateTimeSignUp]))) + '/' + CONVERT(varchar(4),YEAR(DateAdd(day, -1 * datepart(dw, [Promoter].[DateTimeSignUp]) + " + startOfWeek.ToString() + ", [Promoter].[DateTimeSignUp])))))");
				query.OrderBy = new OrderBy("[Usr].[FirstName], CONVERT(dateTime,(CONVERT(varchar(2),DAY(DateAdd(day, -1 * datepart(dw, [Promoter].[DateTimeSignUp]) + " + startOfWeek.ToString() + ", [Promoter].[DateTimeSignUp]))) + '/' + CONVERT(varchar(2),MONTH(DateAdd(day, -1 * datepart(dw, [Promoter].[DateTimeSignUp]) + " + startOfWeek.ToString() + ", [Promoter].[DateTimeSignUp]))) + '/' + CONVERT(varchar(4),YEAR(DateAdd(day, -1 * datepart(dw, [Promoter].[DateTimeSignUp]) + " + startOfWeek.ToString() + ", [Promoter].[DateTimeSignUp]))))) desc");
				query.GroupBy = new GroupBy("[Usr].[FirstName], CONVERT(dateTime,(CONVERT(varchar(2),DAY(DateAdd(day, -1 * datepart(dw, [Promoter].[DateTimeSignUp]) + " + startOfWeek.ToString() + ", [Promoter].[DateTimeSignUp]))) + '/' + CONVERT(varchar(2),MONTH(DateAdd(day, -1 * datepart(dw, [Promoter].[DateTimeSignUp]) + " + startOfWeek.ToString() + ", [Promoter].[DateTimeSignUp]))) + '/' + CONVERT(varchar(4),YEAR(DateAdd(day, -1 * datepart(dw, [Promoter].[DateTimeSignUp]) + " + startOfWeek.ToString() + ", [Promoter].[DateTimeSignUp]))))), [Promoter].[AddedByUsrK]");
			}
			else if (dateGrouping.Equals(DateGrouping.Monthly))
			{
				query.ExtraSelectElements.Add("Date", "CONVERT(datetime,'1/' + CONVERT(varchar(2),MONTH([Promoter].[DateTimeSignUp])) + '/' + CONVERT(varchar(4),Year([Promoter].[DateTimeSignUp])))");
				query.OrderBy = new OrderBy("[Usr].[FirstName], YEAR([Promoter].[DateTimeSignUp]) desc, MONTH([Promoter].[DateTimeSignUp]) desc");
				query.GroupBy = new GroupBy("[Usr].[FirstName], MONTH([Promoter].[DateTimeSignUp]), YEAR([Promoter].[DateTimeSignUp]), [Promoter].[AddedByUsrK]");
			}

			return query;
		}
		#endregion

		#region PopulateSalesCallQuery
		private Query PopulateSalesCallQuery(Query query, DateGrouping dateGrouping, int startOfWeek)
		{
			// startOfWeek: Sunday = 0, Monday = 1

			query.TableElement = new Join(SalesCall.Columns.UsrK, Usr.Columns.K);
			query.Columns = new ColumnSet(SalesCall.Columns.UsrK);

			//salesCallActiveQuery.OrderBy = new OrderBy("[Usr].[FirstName], YEAR([SalesCall].[DateTimeStart]) desc, MONTH([SalesCall].[DateTimeStart]) desc, DAY([SalesCall].[DateTimeStart]) desc");
			//salesCallActiveQuery.GroupBy = new GroupBy("[Usr].[FirstName], DAY([SalesCall].[DateTimeStart]), MONTH([SalesCall].[DateTimeStart]), YEAR([SalesCall].[DateTimeStart]), [SalesCall].[UsrK], DATENAME(dw, [SalesCall].[DateTimeStart])");
			query.ExtraSelectElements.Add("TotalCalls", "COUNT(*)");
			if (dateGrouping.Equals(DateGrouping.Daily))
			{
				query.ExtraSelectElements.Add("Date", "CONVERT(datetime,CONVERT(varchar(2),DAY([SalesCall].[DateTimeStart])) + '/' + CONVERT(varchar(2),MONTH([SalesCall].[DateTimeStart])) + '/' + CONVERT(varchar(4),Year([SalesCall].[DateTimeStart])))");
				query.OrderBy = new OrderBy("[Usr].[FirstName], YEAR([SalesCall].[DateTimeStart]) desc, MONTH([SalesCall].[DateTimeStart]) desc, DAY([SalesCall].[DateTimeStart]) desc");
				query.GroupBy = new GroupBy("[Usr].[FirstName], DAY([SalesCall].[DateTimeStart]), MONTH([SalesCall].[DateTimeStart]), YEAR([SalesCall].[DateTimeStart]), [SalesCall].[UsrK]");
			}
			else if (dateGrouping.Equals(DateGrouping.Weekly))
			{
				query.ExtraSelectElements.Add("Date", "CONVERT(dateTime,(CONVERT(varchar(2),DAY(DateAdd(day, -1 * datepart(dw, [SalesCall].[DateTimeStart]) + " + startOfWeek.ToString() + ", [SalesCall].[DateTimeStart]))) + '/' + CONVERT(varchar(2),MONTH(DateAdd(day, -1 * datepart(dw, [SalesCall].[DateTimeStart]) + " + startOfWeek.ToString() + ", [SalesCall].[DateTimeStart]))) + '/' + CONVERT(varchar(4),YEAR(DateAdd(day, -1 * datepart(dw, [SalesCall].[DateTimeStart]) + " + startOfWeek.ToString() + ", [SalesCall].[DateTimeStart])))))");
				query.OrderBy = new OrderBy("[Usr].[FirstName], CONVERT(dateTime,(CONVERT(varchar(2),DAY(DateAdd(day, -1 * datepart(dw, [SalesCall].[DateTimeStart]) + " + startOfWeek.ToString() + ", [SalesCall].[DateTimeStart]))) + '/' + CONVERT(varchar(2),MONTH(DateAdd(day, -1 * datepart(dw, [SalesCall].[DateTimeStart]) + " + startOfWeek.ToString() + ", [SalesCall].[DateTimeStart]))) + '/' + CONVERT(varchar(4),YEAR(DateAdd(day, -1 * datepart(dw, [SalesCall].[DateTimeStart]) + " + startOfWeek.ToString() + ", [SalesCall].[DateTimeStart]))))) desc");
				query.GroupBy = new GroupBy("[Usr].[FirstName], CONVERT(dateTime,(CONVERT(varchar(2),DAY(DateAdd(day, -1 * datepart(dw, [SalesCall].[DateTimeStart]) + " + startOfWeek.ToString() + ", [SalesCall].[DateTimeStart]))) + '/' + CONVERT(varchar(2),MONTH(DateAdd(day, -1 * datepart(dw, [SalesCall].[DateTimeStart]) + " + startOfWeek.ToString() + ", [SalesCall].[DateTimeStart]))) + '/' + CONVERT(varchar(4),YEAR(DateAdd(day, -1 * datepart(dw, [SalesCall].[DateTimeStart]) + " + startOfWeek.ToString() + ", [SalesCall].[DateTimeStart]))))), [SalesCall].[UsrK]");
			}
			else if (dateGrouping.Equals(DateGrouping.Monthly))
			{
				query.ExtraSelectElements.Add("Date", "CONVERT(datetime,'1/' + CONVERT(varchar(2),MONTH([SalesCall].[DateTimeStart])) + '/' + CONVERT(varchar(4),Year([SalesCall].[DateTimeStart])))");
				query.OrderBy = new OrderBy("[Usr].[FirstName], YEAR([SalesCall].[DateTimeStart]) desc, MONTH([SalesCall].[DateTimeStart]) desc");
				query.GroupBy = new GroupBy("[Usr].[FirstName], MONTH([SalesCall].[DateTimeStart]), YEAR([SalesCall].[DateTimeStart]), [SalesCall].[UsrK]");
			}

			return query;
		}
		#endregion

		private void GenerateSalesStatsReport(DateTime fromDate, DateTime toDate)
		{
			// salesUsrK of 0 means all sales usrs
			GenerateSalesStatsReport(fromDate, toDate, "0");
		}

		private void GenerateSalesStatsReport(DateTime fromDate, DateTime toDate, string salesUsrK)
		{
			Page.Validate("");
			if (Page.IsValid)
			{
				this.DateRangeValueLabel.Text = fromDate.ToString("dd/MM/yy") + " to " + toDate.ToString("dd/MM/yy");
				this.DateRangeLabel.Visible = true;
				this.DateRangeValueLabel.Visible = true;

				fromDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day);
				toDate = new DateTime(toDate.Year, toDate.Month, toDate.Day).AddDays(1);
				if (toDate > fromDate)
				{
					int numberOfDays = ((TimeSpan)(toDate - fromDate)).Days;

					//UsrSet salesUsrs = Usr.GetSalesUsrsNameAndK(salesUsrK);

					List<UsrDataHolder> selectedSalesUsrs = SalesUsrs;
					if (salesUsrK != "0")
					{
						selectedSalesUsrs = new List<UsrDataHolder>();
						if (salesUsrK.IndexOf("team") == 0)
						{
							UsrSet salesTeam = Usr.GetCurrentSalesUsrsNameAndK(Convert.ToInt32(salesUsrK.Replace("team", "")));
							foreach (Usr salesUsr in salesTeam)
							{
								selectedSalesUsrs.Add(new UsrDataHolder(salesUsr));
							}
						}
						else
						{
							selectedSalesUsrs.Add(SalesUsrs[GetSalesPersonIndexNumber(SalesUsrs, Convert.ToInt32(salesUsrK))]);
						}
					}

					if (selectedSalesUsrs.Count > 0)
					{
						Q salesUsrsQueryCondition = new Q(Invoice.Columns.SalesUsrK, selectedSalesUsrs[0].K);
						for (int i = 1; i < selectedSalesUsrs.Count; i++)
						{
							salesUsrsQueryCondition = new Or(salesUsrsQueryCondition,
															 new Q(Invoice.Columns.SalesUsrK, selectedSalesUsrs[i].K));
						}

						Q salesStatsDateRangeQueryCondition = new And(new Q(Invoice.Columns.SalesUsrAmount, QueryOperator.NotEqualTo, 0),
																	  new Or(new And(new Q(Invoice.Columns.Type, Invoice.Types.Invoice),
																					 new Q(Invoice.Columns.Paid, true),
																					 new Q(Invoice.Columns.PaidDateTime, QueryOperator.GreaterThanOrEqualTo, fromDate),
																					 new Q(Invoice.Columns.PaidDateTime, QueryOperator.LessThan, toDate),
                                                                                     new Q(Usr.Columns.SalesTeam, QueryOperator.NotEqualTo, Usr.SalesTeams.CorporateSalesTeam)),
                                                                             new And(new Q(Invoice.Columns.Type, Invoice.Types.Invoice),
                                                                                     new Q(Invoice.Columns.TaxDateTime, QueryOperator.GreaterThanOrEqualTo, fromDate),
																					 new Q(Invoice.Columns.TaxDateTime, QueryOperator.LessThan, toDate),
                                                                                     new Q(Usr.Columns.SalesTeam, Usr.SalesTeams.CorporateSalesTeam)),
																		     new And(new Q(Invoice.Columns.Type, Invoice.Types.Credit),
																					 new Q(Invoice.Columns.CreatedDateTime, QueryOperator.GreaterThanOrEqualTo, fromDate),
																					 new Q(Invoice.Columns.CreatedDateTime, QueryOperator.LessThan, toDate)))); 
						
                        Query salesStatsMinutesQuery = new Query(new And(salesStatsDateRangeQueryCondition,
																		 salesUsrsQueryCondition));
                        salesStatsMinutesQuery.TableElement = new Join(Invoice.Columns.SalesUsrK, Usr.Columns.K);

						salesStatsMinutesQuery.OrderBy = new OrderBy("CASE WHEN [Usr].[SalesTeam] = " + Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam).ToString() + " THEN [Invoice].[TaxDateTime] ELSE CASE WHEN [Invoice].[PaidDateTime] > [Invoice].[CreatedDateTime] THEN [Invoice].[PaidDateTime] ELSE [Invoice].[CreatedDateTime] END END DESC, [Invoice].[K]");
							
							//new OrderBy(Invoice.Columns.PaidDateTime, OrderBy.OrderDirection.Descending), new OrderBy(Invoice.Columns.K, OrderBy.OrderDirection.Ascending));

						InvoiceSet salesInvoices = new InvoiceSet(salesStatsMinutesQuery);

						#region Table
						this.SalesCallsResultsTable.Visible = false;
						this.SalesStatsResultsTable.Visible = true;
						HtmlTable salesStatsTable = this.SalesStatsResultsTable;
						salesStatsTable.Rows.Clear();
						#endregion

						#region Header Rows
						HtmlTableRow headerRow = new HtmlTableRow();

						headerRow.Attributes.Add("class", "dataGridHeader");
						salesStatsTable.Rows.Add(headerRow);
						HtmlTableCell[] headerTableCells = new HtmlTableCell[6];

						for (int i = 0; i < headerTableCells.Length; i++)
						{
							headerTableCells[i] = new HtmlTableCell("th");
							headerTableCells[i].Align = "left";
						}
						headerTableCells[0].InnerHtml = "Time";
						headerTableCells[1].InnerHtml = "Date";
						headerTableCells[2].InnerHtml = "<nobr>Link to invoice and items</nobr>";
						headerTableCells[2].Width = "250";
						headerTableCells[3].InnerHtml = "Promoter";
						headerTableCells[4].InnerHtml = "<nobr>Sales amount</nobr>";
						headerTableCells[5].InnerHtml = "<nobr>Sales person</nobr>";

						foreach (HtmlTableCell tc in headerTableCells)
							headerRow.Cells.Add(tc);

						#endregion

						#region Data Rows
						decimal totalSalesAmount = 0;
						for (int i = 0; i < salesInvoices.Count; i++)
						{
							HtmlTableRow tr = new HtmlTableRow();
							salesStatsTable.Rows.Add(tr);
							if (i % 2 == 0)
								tr.Attributes.Add("class", "dataGridItem");
							else
								tr.Attributes.Add("class", "dataGridAltItem");

							HtmlTableCell[] dataTableCells = new HtmlTableCell[6];
							for (int j = 0; j < dataTableCells.Length; j++)
							{
								dataTableCells[j] = new HtmlTableCell();
								tr.Cells.Add(dataTableCells[j]);
							}

							DateTime dt = salesInvoices[i].Type == Invoice.Types.Invoice && salesInvoices[i].SalesUsr.SalesTeam != Convert.ToInt32(Usr.SalesTeams.CorporateSalesTeam) ? salesInvoices[i].PaidDateTime : salesInvoices[i].CreatedDateTime;

                            if (dt > DateTime.MinValue)
                            {
                                dataTableCells[0].InnerHtml = dt.ToString("HH:mm");
                                dataTableCells[1].InnerHtml = Utilities.DateToString(dt);
                            }
                            else
                            {
                                dataTableCells[0].InnerHtml = "&nbsp;";
                                dataTableCells[1].InnerHtml = "&nbsp;";
                            }
							dataTableCells[2].InnerHtml = "<a href=\"" + salesInvoices[i].UrlAdmin() + "\" target=\"_blank\">Invoice #" + salesInvoices[i].K.ToString() + "</a>";
							HtmlTable itemsTable = new HtmlTable();
							itemsTable.ID = "InvoiceItems" + salesInvoices[i].K.ToString();
							itemsTable.Style["display"] = "none";
							itemsTable.CellPadding = 3;
							itemsTable.CellSpacing = 0;
							itemsTable.Width = "250";
							itemsTable.Border = 0;

							for (int k = 0; k < salesInvoices[i].Items.Count; k++)
							{
								HtmlTableRow itemTr = new HtmlTableRow();
								itemsTable.Rows.Add(itemTr);
								HtmlTableCell itemDescriptionTd = new HtmlTableCell();
								HtmlTableCell itemPriceTd = new HtmlTableCell();
								itemTr.Cells.Add(itemDescriptionTd);
								itemTr.Cells.Add(itemPriceTd);
								itemDescriptionTd.Attributes.Add("style", "background-color:transparent;padding-top:1px;padding-bottom:1px;border-left:0px;");
								itemPriceTd.Attributes.Add("style", "background-color:transparent;padding-top:1px;padding-bottom:1px;border-left:0px;");
								try
								{
									itemDescriptionTd.InnerHtml = "<small><a href=\"" + salesInvoices[i].Items[k].AdminUrl + "\" target=\"_blank\">" + salesInvoices[i].Items[k].Description + "</a></small>";
								}
								catch
								{
									itemDescriptionTd.InnerHtml = "<small>" + salesInvoices[i].Items[k].Description + "</small>";
								}
								itemPriceTd.InnerHtml = "<nobr><small>" + salesInvoices[i].Items[k].Price.ToString("c") + "</small></nobr>";
								itemPriceTd.Align = "right";
							}
							string invoiceHtml = "<a href=\"" + salesInvoices[i].UrlAdmin() + "\" target=\"_blank\">" + salesInvoices[i].Type.ToString() + " #" + salesInvoices[i].K.ToString() + "</a>";
							dataTableCells[2].InnerHtml = "<a href=\"#\" onclick=\"var elem = document.getElementById('Content_" + itemsTable.ClientID + "'); var img = document.getElementById('" + this.ClientID + "_SalesStatsPlusMinus" + salesInvoices[i].K + "'); img.src = elem.style.display == 'none' ? '/gfx/minus.gif' : '/gfx/plus.gif'; elem.style.display = elem.style.display == 'none' ? '' : 'none'; return false;\"><img id=\"" + this.ClientID + "_SalesStatsPlusMinus" + salesInvoices[i].K + "\" src=\"/gfx/plus.gif\" alt=\"Show items\" border=\"0\" align=\"absmiddle\" style=\"margin-right:4px;\" /></a>" + invoiceHtml;
							dataTableCells[2].Controls.Add(itemsTable);
							// Make Promoter Link 
							dataTableCells[3].InnerHtml = "<nobr>" + salesInvoices[i].Promoter.LinkNewWindow() + "</nobr>";
							dataTableCells[3].Width = "250";
							dataTableCells[4].InnerHtml = salesInvoices[i].SalesUsrAmount.ToString("c") + "&nbsp;";
							dataTableCells[4].Align = "right";
							dataTableCells[5].InnerHtml = "<nobr>" + salesInvoices[i].SalesUsr.LinkNewWindow() + "</nobr>";

							// Fixes issue with random assigning of ColSpan = 5. Because footer has colspan=5 it "remembers" that on the same row #, but this fix has resolved it.
							for (int j = 0; j < dataTableCells.Length; j++)
								dataTableCells[j].ColSpan = 1;

							totalSalesAmount += salesInvoices[i].SalesUsrAmount;
						}
						#endregion

						#region Footer Row
						HtmlTableRow footerRow = new HtmlTableRow();
						salesStatsTable.Rows.Add(footerRow);
						footerRow.Attributes.Add("class", "dataGridFooter");
						HtmlTableCell footerTableCell = new HtmlTableCell();
						footerRow.Cells.Add(footerTableCell);
						footerTableCell.ColSpan = 5;
						footerTableCell.Align = "right";
						footerTableCell.InnerHtml = "Total for period: <b>" + totalSalesAmount.ToString("c") + "</b>&nbsp;";
						#endregion
					}
				}
			}
		}

		private void LoadSalesCallPercentDataToTable(HtmlTableCell[,] dataTableCells, List<UsrDataHolder> salesUsrs, SalesCallSet salesCalls, DateTime toDate, int column, DateGrouping dateGrouping)
		{
			foreach (SalesCall salesCall in salesCalls)
			{
				try
				{
					int i = GetSalesPersonIndexNumber(salesUsrs, salesCall.UsrK);

					int dateGroupingsFromTop = 0;
					if (dateGrouping.Equals(DateGrouping.Daily))
						dateGroupingsFromTop = ((TimeSpan)(toDate - ((DateTime)salesCall.ExtraSelectElements["Date"]))).Days;
					else if (dateGrouping.Equals(DateGrouping.Weekly))
						dateGroupingsFromTop = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(((TimeSpan)(toDate - ((DateTime)salesCall.ExtraSelectElements["Date"]))).Days) / 7d));
					else if (dateGrouping.Equals(DateGrouping.Monthly))
					{
						dateGroupingsFromTop = (toDate.AddDays(-1).Year - ((DateTime)salesCall.ExtraSelectElements["Date"]).Year) * 12 + (toDate.AddDays(-1).Month - ((DateTime)salesCall.ExtraSelectElements["Date"]).Month) + 1;
					}
					if (dataTableCells[dateGroupingsFromTop - 1, salesUsrs.Count * 2 + i + 1] != null)
					{
						double totalCalls = Convert.ToDouble(dataTableCells[dateGroupingsFromTop - 1, salesUsrs.Count * 2 + i + 1].InnerHtml.ToString());

						dataTableCells[dateGroupingsFromTop - 1, salesUsrs.Count * column + i + 1] = new HtmlTableCell();
						dataTableCells[dateGroupingsFromTop - 1, salesUsrs.Count * column + i + 1].InnerHtml = Math.Round(Convert.ToDouble(salesCall.ExtraSelectElements["TotalCalls"]) * 100 / totalCalls, 0).ToString();
					}
				}
				catch (Exception)
				{ }
			}
		}

		private int GetSalesPersonIndexNumber(List<UsrDataHolder> salesUsrs, int salesUsrK)
		{
			int i = 0;
			for (; i < salesUsrs.Count; i++)
			{
				if (salesUsrs[i].K == salesUsrK)
					break;
			}
			if (i == salesUsrs.Count)
				throw new Exception("Sales user " + new Usr(salesUsrK).Name + " was not found in the sales users query. Please report this issue to an administrator.");

			return i;
		}

		private double TotalSalesAmountPaid(DateTime fromDate, DateTime toDate, UsrSet salesUsrs)
		{
			fromDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day);
			toDate = new DateTime(toDate.Year, toDate.Month, toDate.Day).AddDays(1);

			Q salesUsrsQueryCondition = new Q(Invoice.Columns.SalesUsrK, salesUsrs[0].K);
			for (int i = 1; i < salesUsrs.Count; i++)
			{
				salesUsrsQueryCondition = new Or(salesUsrsQueryCondition,
												 new Q(Invoice.Columns.SalesUsrK, salesUsrs[i].K));
			}

			Query totalSalesAmountQuery = new Query(new And(salesUsrsQueryCondition,
															new Q(Invoice.Columns.Paid, true),
															new Q(Invoice.Columns.SalesUsrAmount, QueryOperator.GreaterThan, 0),
															new Q(Invoice.Columns.PaidDateTime, QueryOperator.GreaterThanOrEqualTo, fromDate),
															new Q(Invoice.Columns.PaidDateTime, QueryOperator.LessThan, toDate)));
			totalSalesAmountQuery.ExtraSelectElements.Add("TotalPrice", "SUM(Price)");
			totalSalesAmountQuery.Columns = new ColumnSet();

			InvoiceSet invoices = new InvoiceSet(totalSalesAmountQuery);

			double result = 0;
			try
			{
				if (invoices.Count > 0)
					result = Convert.ToDouble(invoices[0].ExtraSelectElements["TotalPrice"]);
			}
			catch (Exception)
			{
			}

			return result;
		}


		#endregion

		#region Enums
		private enum DateGrouping
		{
			Daily = 1,
			Weekly = 2,
			Monthly = 3
		}

		#endregion

		#region Custom Validators
		public void DateRangeVal(object o, ServerValidateEventArgs e)
		{
			if (this.OverrideDateCheckBox.Checked && this.FromDateCal.Date > this.ToDateCal.Date)
				e.IsValid = false;
			else
				e.IsValid = true;
		}
		#endregion
	}
}
