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
using System.Text;
using Bobs;
using Common;
using SpottedScript.Controls.ChatClient.Shared;

namespace Spotted.Controls.Navigation
{
	public partial class Admin : System.Web.UI.UserControl
	{
		//protected Panel AdminPanel, SuperPanel, SeniorPanel;
		public PlaceHolder AdminPanelContents, AdminPanelOther, SuperAdmin, SeniorAdmin;
		public Panel PromoterPanel;
		public HtmlAnchor PromoterQuestionsAnchor;
		public Panel StuckPanel;

		//protected Panel NewsPanel;
		//protected PlaceHolder NewsPlaceHolder;

		public Master.DsiPage ContainerPage
		{
			get
			{
				return (Master.DsiPage)Page;
			}
		}
		private void Page_Load(object sender, System.EventArgs e)
		{

			AdminPanel.Visible = (Usr.Current != null && Usr.Current.IsAdmin);

			#region Sales People
			SalesPanel.Visible = (Usr.Current != null && (Usr.Current.IsSalesPerson || Usr.Current.IsSuperAdmin));

			if (SalesPanel.Visible)
			{
				List<UsrDataHolder> salesUsrList = new List<UsrDataHolder>();
				if (ViewState["SalesUsrList"] == null)
				{
					UsrSet salesUsrs = Usr.GetCurrentSalesUsrsNameAndK(new int[] {(int)Usr.SalesTeams.PromoterSalesTeam, (int)Usr.SalesTeams.SmallBusinessSalesTeam});
					foreach (Usr salesUsr in salesUsrs)
						salesUsrList.Add(new UsrDataHolder(salesUsr));
					ViewState["SalesUsrList"] = salesUsrList;
				}
				else
				{
					salesUsrList = (List<UsrDataHolder>)ViewState["SalesUsrList"];
				}
				GenerateSalesTodayTable(salesUsrList);
				GenerateSalesMonthTable(salesUsrList);


				//LastWeekDivHeader.InnerHtml = "<a href=\"\" onclick=\"document.getElementById('" + TeamBonusLastWeekDiv.ClientID + "').style.display='';return false;\">Last week</a>";
				//LastWeekDivHeader.Attributes["style"] = "cursor:pointer";

				TeamBonusLastWeekDiv.Controls.Clear();
				TeamBonusThisWeekDiv.Controls.Clear();
				try
				{
					GenerateTeamBonusTargetWeek(Time.Today.AddDays(-7), TeamBonusLastWeekDiv, salesUsrList);
					GenerateTeamBonusTargetWeek(Time.Today, TeamBonusThisWeekDiv, salesUsrList);
				}
				catch { }
			}
			#endregion

			#region IsSuper
			if (Usr.Current != null && Usr.Current.IsSuper)
			{

				Query vq = new Query();
				vq.NoLock = true;
				vq.QueryCondition = new Q(Venue.Columns.IsNew, true);
				vq.ReturnCountOnly = true;
				VenueSet vs = new VenueSet(vq);

				if (vs.Count != 0)
				{
					if (Usr.Current.AdminLevel.Equals(Usr.AdminLevels.Super))
					{
						SuperAdmin.Controls.Add(new LiteralControl("<p>You have to moderate:</p>"));
						SuperAdmin.Controls.Add(new LiteralControl("<p>" + Usr.Current.NewVenuesToModerateHtml + " new venues</p>"));
						SuperAdmin.Controls.Add(new LiteralControl("<p>Total to moderate:</p>"));
					}
					SuperAdmin.Controls.Add(new LiteralControl("<p>" + vs.Count.ToString() + " new venue(s)</p><p><a href=\"/pages/venues/moderate\">Moderate now!</a></p>"));
				}

				Query qMisc = new Query();
				qMisc.QueryCondition = new Q(Bobs.Misc.Columns.NeedsAuth, true);
				qMisc.ReturnCountOnly = true;
				qMisc.NoLock = true;
				MiscSet ms = new MiscSet(qMisc);
				if (ms.Count > 0)
					SuperAdmin.Controls.Add(new LiteralControl("<p><a href=\"/pages/bannercheck/mode-list\">Check new banners</a></p>"));

			}
			else
				SuperPanel.Visible = false;
			#endregion

			#region IsSenior
			if (Usr.Current != null && Usr.Current.IsSenior)
			{
				//GallerySet gs = new GallerySet(new Q(Gallery.Columns.TotalPhotos,QueryOperator.NotEqualTo,Gallery.Columns.LivePhotos,true));
				//if (gs.Count>0)
				SeniorPanel.Visible = true;

				if (ContainerPage.Url.HasPhotoObjectFilter)
				{
					SeniorAdmin.Controls.Add(new LiteralControl("<p><a href=\"" + ContainerPage.Url.ObjectFilterPhoto.Gallery.UrlApp("edit") + "\">Edit gallery</a></p>"));
				}
				if (ContainerPage.Url.HasGalleryObjectFilter)
				{
					SeniorAdmin.Controls.Add(new LiteralControl("<p><a href=\"" + ContainerPage.Url.ObjectFilterGallery.UrlApp("edit") + "\">Edit gallery</a></p>"));
				}

			}
			else
				SeniorPanel.Visible = false;
			#endregion

			if (Usr.Current != null && Usr.Current.IsAdmin && ((Spotted.Master.DsiPage)Page).Url.HasPromoterObjectFilter)
			{
				Promoter CurrentPromoter = ((Spotted.Master.DsiPage)Page).Url.ObjectFilterPromoter;
				AdminPanelContents.Controls.Add(new LiteralControl("<p><b>PromoterK: " + CurrentPromoter.K + "</b></p>"));
				AdminPanelContents.Controls.Add(new LiteralControl("<p><a href=\"http://old.dontstayin.com/login-" + Usr.Current.K + "- "+ Usr.Current.LoginString +"/admin/promoter?ID=" + CurrentPromoter.K + "\">Edit this promoter (admin)</a></p>"));
				AdminPanelContents.Controls.Add(new LiteralControl("<p><a href=\"" + CurrentPromoter.UrlApp("edit") + "\">Edit this promoter (public)</a></p>"));

				AdminPanelContents.Controls.Add(new LiteralControl("<p>Promoter:<br>"));
				AdminPanelContents.Controls.Add(new LiteralControl("&nbsp;-&nbsp;<a href=\"/chat/k-" + CurrentPromoter.QuestionsThreadK + "\">Questions</a><br>"));
				AdminPanelContents.Controls.Add(new LiteralControl("&nbsp;-&nbsp;<a href=\"" + CurrentPromoter.UrlApp("banners") + "\">Banners</a><br>"));
				AdminPanelContents.Controls.Add(new LiteralControl("&nbsp;-&nbsp;<a href=\"" + CurrentPromoter.UrlApp("files") + "\">Files</a><br>"));
				AdminPanelContents.Controls.Add(new LiteralControl("&nbsp;-&nbsp;<a href=\"" + CurrentPromoter.UrlApp("events") + "\">Events</a><br>"));
				AdminPanelContents.Controls.Add(new LiteralControl("&nbsp;-&nbsp;<a href=\"" + CurrentPromoter.UrlApp("guestlists") + "\">Guestlists</a><br>"));
				AdminPanelContents.Controls.Add(new LiteralControl("&nbsp;-&nbsp;<a href=\"" + CurrentPromoter.UrlApp("competitions") + "\">Competitions</a>"));
				AdminPanelContents.Controls.Add(new LiteralControl("</p>"));

				if (CurrentPromoter.AllBrands.Count > 0)
					AdminPanelContents.Controls.Add(new LiteralControl("<p>Brands:<br>"));
				foreach (Brand b in CurrentPromoter.AllBrands)
					AdminPanelContents.Controls.Add(new LiteralControl("&nbsp;-&nbsp;<a href=\"" + b.Url() + "\">" + b.Name + "</a>" + (b.PromoterStatus.Equals(Brand.PromoterStatusEnum.Unconfirmed) ? " <font color=0000ff><b>unconfirmed</b></font>" : "") + "<br>"));
				if (CurrentPromoter.AllBrands.Count > 0)
					AdminPanelContents.Controls.Add(new LiteralControl("</p>"));

				if (CurrentPromoter.AllVenues.Count > 0)
					AdminPanelContents.Controls.Add(new LiteralControl("<p>Venues:<br>"));
				foreach (Venue v in CurrentPromoter.AllVenues)
					AdminPanelContents.Controls.Add(new LiteralControl("&nbsp;-&nbsp;<a href=\"" + v.Url() + "\">" + v.Name + " in " + v.Place.Name + "</a>" + (v.PromoterStatus.Equals(Venue.PromoterStatusEnum.Unconfirmed) ? " <font color=0000ff><b>unconfirmed</b></font>" : "") + "<br>"));
				if (CurrentPromoter.AllVenues.Count > 0)
					AdminPanelContents.Controls.Add(new LiteralControl("</p>"));

				AdminPanelContents.Controls.Add(new LiteralControl("<p>Users:<br>"));
				foreach (Usr u in CurrentPromoter.AdminUsrs)
					AdminPanelContents.Controls.Add(new LiteralControl("&nbsp;-&nbsp;<a href=\"" + u.Url() + "\">" + u.NickNameSafe + "</a>" + (CurrentPromoter.PrimaryUsrK == u.K ? "&nbsp;(PRIMARY)" : "") + "<br>"));
				AdminPanelContents.Controls.Add(new LiteralControl("</p>"));
			}


			//if (Usr.Current != null && Usr.Current.IsJunior)
			//{
			//    if (ContainerPage.Url.HasUsrObjectFilter)
			//    {
			//        uiModerateTagsAnchor.Text = "Moderate this user's tags";
			//        uiModerateTagsAnchor.NavigateUrl = "/pages/moderateusrtags/usr-" + ContainerPage.Url.ObjectFilterUsr.K;
			//    }
			//    else if (ContainerPage.Url.HasPhotoObjectFilter)
			//    {
			//        uiModerateTagsAnchor.Text = "Moderate this photo's tags";
			//        uiModerateTagsAnchor.NavigateUrl = "/pages/moderatephototags/photo-" + ContainerPage.Url.ObjectFilterPhoto.K;

			//        Photo photo = ContainerPage.Url.ObjectFilterPhoto;
			//        if (photo.ThreadK > 0)
			//        {
			//            uiAdministrateChatAnchor.Text = "Administrate this photo's thread";
			//            uiAdministrateChatAnchor.NavigateUrl = "/pages/chatadmin/k-" + photo.ThreadK;
			//            uiAdministrateChatAnchor.Style["display"] = "";
			//        }
			//        else
			//        {
			//            uiAdministrateChatAnchor.Style["display"] = "none";
			//        }
			//    }
			//    else if (ContainerPage.Url["photo"].Exists)
			//    {
			//        uiModerateTagsAnchor.Text = "Moderate this photo's tags";
			//        uiModerateTagsAnchor.NavigateUrl = "/pages/moderatephototags/photo-" + ContainerPage.Url["photo"].ValueInt;
			//        Photo photo= new Photo(ContainerPage.Url["Photo"].ValueInt);
			//        if (photo.ThreadK > 0)
			//        {
			//            uiAdministrateChatAnchor.Text = "Administrate this photo's thread";
			//            uiAdministrateChatAnchor.NavigateUrl = "/pages/chatadmin/k-" + photo.ThreadK;
			//            uiAdministrateChatAnchor.Style["display"] = "";
			//        }
			//        else
			//        {
			//            uiAdministrateChatAnchor.Style["display"] = "none";
			//        }
			//    }
			//    else
			//    {
			//        ChatPanel.Visible = false;
			//    }
			//}
			//else
			//{
			//    ChatPanel.Visible = false;
			//}

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
		}
		#endregion

		#region Methods
		private void GenerateSalesTodayTable(List<UsrDataHolder> salesUsrs)
		{
			DateTime fromDate = DateTime.Today;
			DateTime toDate = fromDate.AddDays(1);
		
			if (salesUsrs.Count > 0)
			{
				#region Sales: Total Money
				Query totalSalesAmountQuery = new Query(new And(new Q(Usr.Columns.SalesTeam, QueryOperator.GreaterThan, 0),
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
				totalSalesAmountQuery.TableElement = new Join(Usr.Columns.K, Invoice.Columns.SalesUsrK, QueryJoinType.Left);
				totalSalesAmountQuery.ExtraSelectElements.Add("TotalSales", "SUM(ISNULL([Invoice].[SalesUsrAmount],0))");
				totalSalesAmountQuery.Columns = new ColumnSet(Invoice.Columns.SalesUsrK, Usr.Columns.FirstName);

				totalSalesAmountQuery.OrderBy = new OrderBy("[Usr].[FirstName], SUM(SalesUsrAmount)");
				totalSalesAmountQuery.GroupBy = new GroupBy("[Usr].[FirstName], [Invoice].[SalesUsrK]");


				InvoiceSet invoices = new InvoiceSet(totalSalesAmountQuery);
				#endregion

				#region Sales Calls: SalesUsrK, Minutes, and Total calls
				Query salesCallMinutesQuery = new Query(new And(new Q(SalesCall.Columns.IsCall, true),
																new Q(SalesCall.Columns.DateTimeStart, QueryOperator.GreaterThanOrEqualTo, fromDate),
																new Q(SalesCall.Columns.DateTimeStart, QueryOperator.LessThan, toDate),
																new Q(Usr.Columns.IsSalesPerson, true)));
				//salesCallMinutesQuery.ExtraSelectElements.Add("Minutes", "SUM(ISNULL([SalesCall].[Duration],0))");
				salesCallMinutesQuery.ExtraSelectElements.Add("TotalCalls", "COUNT([SalesCall].[Duration])");
				salesCallMinutesQuery.TableElement = new Join(Usr.Columns.K, SalesCall.Columns.UsrK, QueryJoinType.Left);
				salesCallMinutesQuery.Columns = new ColumnSet(SalesCall.Columns.UsrK);

				salesCallMinutesQuery.OrderBy = new OrderBy("[Usr].[FirstName]");
				salesCallMinutesQuery.GroupBy = new GroupBy("[Usr].[FirstName], [SalesCall].[UsrK]");

				SalesCallSet salesCallsMinutes = new SalesCallSet(salesCallMinutesQuery);
				#endregion

				#region Table
				HtmlTable salesTodayTable = this.SalesTodayTable;
				salesTodayTable.Rows.Clear();
				
				#endregion

				#region Header Row
				HtmlTableRow headerRow = new HtmlTableRow();
				headerRow.Attributes.Add("class", "dataGridHeader");
				salesTodayTable.Rows.Add(headerRow);
				HtmlTableCell[] headerTableCells = new HtmlTableCell[3];
				for (int i = 0; i < headerTableCells.Length; i++)
				{
					headerTableCells[i] = new HtmlTableCell("th");
					headerTableCells[i].Align = "right";
					headerTableCells[i].InnerHtml = "&nbsp;";
					headerRow.Cells.Add(headerTableCells[i]);
				}
				headerTableCells[0].Style.Add("border-left", "0px;");
				//headerTableCells[1].InnerHtml = "£";
				//headerTableCells[2].InnerHtml = "Ca-<br>lls";
				#endregion

				#region Data Rows
				HtmlTableRow[] dataRows = new HtmlTableRow[salesUsrs.Count];
				for (int i = 0; i < dataRows.Length; i++)
				{
					dataRows[i] = new HtmlTableRow();
					HtmlTableCell[] dataTableCells = new HtmlTableCell[3];
					for (int k = 0; k < dataTableCells.Length; k++)
					{
						dataTableCells[k] = new HtmlTableCell();
						dataRows[i].Cells.Add(dataTableCells[k]);
					}

					dataTableCells[0].InnerHtml = salesUsrs[i].FirstName;
					dataTableCells[0].Style.Add("border-left", "0px;");
					dataTableCells[1].Align = "right";
					dataTableCells[2].Align = "right";
					//dataTableCells[3].Align = "right";

					dataTableCells[1].InnerHtml = "£0";
					dataTableCells[2].InnerHtml = "0 calls";
					//dataTableCells[3].InnerHtml = "0";

					if (i == dataRows.Length - 1)
					{
						dataTableCells[0].Style.Add("border-bottom", "0px;");
						dataTableCells[1].Style.Add("border-bottom", "0px;");
						dataTableCells[2].Style.Add("border-bottom", "0px;");
						//dataTableCells[3].Style.Add("border-bottom", "0px;");
					}

					for (int m = 0; m < invoices.Count; m++)
					{
						if (invoices[m].SalesUsrK == salesUsrs[i].K)
						{
							dataTableCells[1].InnerHtml = "£" + Convert.ToDouble(invoices[m].ExtraSelectElements["TotalSales"]).ToString("#,##0");
							break;
						}
					}

					for (int n = 0; n < salesCallsMinutes.Count; n++)
					{
						if (salesCallsMinutes[n].UsrK == salesUsrs[i].K)
						{
							//dataTableCells[2].InnerHtml = Convert.ToDouble(salesCallsMinutes[n].ExtraSelectElements["Minutes"]).ToString("#,##0");
							dataTableCells[2].InnerHtml = Convert.ToDouble(salesCallsMinutes[n].ExtraSelectElements["TotalCalls"]).ToString("#,##0") + " calls";
							break;
						}
					}

					salesTodayTable.Rows.Add(dataRows[i]);
				}
				#endregion
			}
		}

		private void GenerateSalesMonthTable(List<UsrDataHolder> salesUsrs)
		{
			DateTime fromDate = Utilities.GetStartOfMonth(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day));
			//DateTime fromDate = Utilities.GetStartOfMonth(new DateTime(2006, 11, 23));
			DateTime toDate = Utilities.GetEndOfMonth(fromDate);
			toDate = new DateTime(toDate.Year, toDate.Month, toDate.Day).AddDays(1);

			if (salesUsrs.Count > 0)
			{
				#region Sales: Total Money
				Query totalSalesAmountQuery = new Query(new And(new Q(Usr.Columns.IsSalesPerson, true),
																new Or(new Q(Usr.Columns.SalesTeam, Usr.SalesTeams.PromoterSalesTeam), new Q(Usr.Columns.SalesTeam, Usr.SalesTeams.SmallBusinessSalesTeam)),
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
				totalSalesAmountQuery.TableElement = new Join(Usr.Columns.K, Invoice.Columns.SalesUsrK, QueryJoinType.Left);
				totalSalesAmountQuery.ExtraSelectElements.Add("TotalSales", "SUM(ISNULL([Invoice].[SalesUsrAmount],0))");
				totalSalesAmountQuery.Columns = new ColumnSet(Invoice.Columns.SalesUsrK, Usr.Columns.FirstName);

				totalSalesAmountQuery.OrderBy = new OrderBy("SUM(ISNULL([Invoice].[SalesUsrAmount],0)) desc, [Usr].[FirstName]");
				totalSalesAmountQuery.GroupBy = new GroupBy("[Usr].[FirstName], [Invoice].[SalesUsrK]");

				InvoiceSet invoices = new InvoiceSet(totalSalesAmountQuery);
				#endregion

				#region Table
				HtmlTable salesMonthTable = this.SalesMonthTable;
				salesMonthTable.Rows.Clear();
				#endregion

				#region Header Row
				HtmlTableRow headerRow = new HtmlTableRow();
				headerRow.Attributes.Add("class", "dataGridHeader");
				salesMonthTable.Rows.Add(headerRow);
				HtmlTableCell[] headerTableCells = new HtmlTableCell[2];
				for (int i = 0; i < headerTableCells.Length; i++)
				{
					headerTableCells[i] = new HtmlTableCell("th");
					headerTableCells[i].Align = "right";
					headerTableCells[i].InnerHtml = "&nbsp;";
					headerRow.Cells.Add(headerTableCells[i]);
				}
				headerTableCells[0].Style.Add("border-left", "0px;");
				//headerTableCells[1].InnerHtml = "<img src=\"/gfx/money-gbp-small.gif\" alt=\"Money\" align=\"right\" height=\"11\" width=\"13\"/>";

				#endregion

				#region Data Rows
				HtmlTableRow[] dataRows = new HtmlTableRow[salesUsrs.Count];
				for (int i = 0; i < dataRows.Length; i++)
				{
					dataRows[i] = new HtmlTableRow();
					HtmlTableCell[] dataTableCells = new HtmlTableCell[2];
					dataTableCells[0] = new HtmlTableCell();
					dataTableCells[1] = new HtmlTableCell();
					dataTableCells[1].Align = "right";
					dataTableCells[1].InnerHtml = "£0";

					dataTableCells[0].Style.Add("border-left", "0px;");

					if (i == dataRows.Length - 1)
					{
						dataTableCells[0].Style.Add("border-bottom", "0px;");
						dataTableCells[1].Style.Add("border-bottom", "0px;");
					}
					if (invoices.Count > i)
					{
						dataTableCells[0].InnerHtml = invoices[i].ExtraSelectElements["Usr_FirstName"].ToString();
						dataTableCells[1].InnerHtml = "£" + Convert.ToDouble(invoices[i].ExtraSelectElements["TotalSales"]).ToString("#,##0");
					}

					//for (int m = 0; m < invoices.Count; m++)
					//{
					//    if (invoices[m].SalesUsrK == salesUsrs[i].K)
					//    {
							
					//        break;
					//    }
					//}
					salesMonthTable.Rows.Add(dataRows[i]);
					for (int j = 0; j < dataTableCells.Length; j++)
					{
						dataRows[i].Cells.Add(dataTableCells[j]);
					}
				}

				// Populate remaining names to table. Since not all sales people will have made sales for the given month, we need to add the remaining sales people with total sales of zero
				if (invoices.Count < salesUsrs.Count)
				{
					bool exists;
					foreach (UsrDataHolder udh in salesUsrs)
					{
						exists = false;
						foreach (HtmlTableRow row in salesMonthTable.Rows)
						{
							if (row.Cells[0].InnerHtml.Equals(udh.FirstName))
							{
								exists = true;
								break;
							}
						}
						if (!exists)
						{
							foreach (HtmlTableRow row in salesMonthTable.Rows)
							{
								if (row.Cells[0].InnerHtml.Length == 0)
								{
									row.Cells[0].InnerHtml = udh.FirstName;
									break;
								}
							}
						}
					}
				}
				#endregion
			}
		}

		private void GenerateTeamBonusTargetWeek(DateTime dt, HtmlGenericControl control, List<UsrDataHolder> salesUsrs)
		{
			

			DateTime weekStart = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
			
			if (dt.DayOfWeek == DayOfWeek.Saturday)
				weekStart = weekStart;
			else if (dt.DayOfWeek == DayOfWeek.Sunday)
				weekStart = weekStart.AddDays(-1);
			else if (dt.DayOfWeek == DayOfWeek.Monday)
				weekStart = weekStart.AddDays(-2);
			else if (dt.DayOfWeek == DayOfWeek.Tuesday)
				weekStart = weekStart.AddDays(-3);
			else if (dt.DayOfWeek == DayOfWeek.Wednesday)
				weekStart = weekStart.AddDays(-4);
			else if (dt.DayOfWeek == DayOfWeek.Thursday)
				weekStart = weekStart.AddDays(-5);
			else if (dt.DayOfWeek == DayOfWeek.Friday)
				weekStart = weekStart.AddDays(-6);

			DateTime weekEnd = weekStart.AddDays(7);
			

			#region Sales: Total Money
			Query totalSalesAmountQuery = new Query(new And(new Q(Usr.Columns.IsSalesPerson, true),
															new Or(new Q(Usr.Columns.SalesTeam, Usr.SalesTeams.PromoterSalesTeam), new Q(Usr.Columns.SalesTeam, Usr.SalesTeams.SmallBusinessSalesTeam)),
															new Q(Invoice.Columns.SalesUsrAmount, QueryOperator.NotEqualTo, 0),
															new Or(new And(new Q(Invoice.Columns.Type, Invoice.Types.Invoice),
																		   new Q(Invoice.Columns.Paid, true),
																		   new Q(Invoice.Columns.PaidDateTime, QueryOperator.GreaterThanOrEqualTo, weekStart),
																		   new Q(Invoice.Columns.PaidDateTime, QueryOperator.LessThan, weekEnd)),
																   new And(new Q(Invoice.Columns.Type, Invoice.Types.Credit),
																		   new Q(Invoice.Columns.CreatedDateTime, QueryOperator.GreaterThanOrEqualTo, weekStart),
																		   new Q(Invoice.Columns.CreatedDateTime, QueryOperator.LessThan, weekEnd)))));
			totalSalesAmountQuery.TableElement = new Join(Usr.Columns.K, Invoice.Columns.SalesUsrK, QueryJoinType.Left);
			totalSalesAmountQuery.ExtraSelectElements.Add("TotalSales", "SUM(ISNULL([Invoice].[SalesUsrAmount],0))");
			totalSalesAmountQuery.Columns = new ColumnSet(Invoice.Columns.SalesUsrK, Usr.Columns.FirstName);

			totalSalesAmountQuery.OrderBy = new OrderBy("SUM(ISNULL([Invoice].[SalesUsrAmount],0)) desc, [Usr].[FirstName]");
			totalSalesAmountQuery.GroupBy = new GroupBy("[Usr].[FirstName], [Invoice].[SalesUsrK]");

			InvoiceSet invoices = new InvoiceSet(totalSalesAmountQuery);
			#endregion


			#region Table
			HtmlTable salesMonthTable = new HtmlTable();
			salesMonthTable.Rows.Clear();
			#endregion


			//width="100%" cellpadding="3" cellspacing="0" class="dataGrid" 
			salesMonthTable.Width = "100%";
			salesMonthTable.CellPadding = 3;
			salesMonthTable.CellSpacing = 0;
			salesMonthTable.Attributes["class"] = "dataGrid";

			#region Header Row
			HtmlTableRow headerRow = new HtmlTableRow();
			headerRow.Attributes.Add("class", "dataGridHeader");
			salesMonthTable.Rows.Add(headerRow);
			HtmlTableCell[] headerTableCells = new HtmlTableCell[2];
			for (int i = 0; i < headerTableCells.Length; i++)
			{
				headerTableCells[i] = new HtmlTableCell("th");
				headerTableCells[i].Align = "right";
				headerTableCells[i].InnerHtml = "&nbsp;";
				headerRow.Cells.Add(headerTableCells[i]);
			}
			headerTableCells[0].Style.Add("border-left", "0px;");
			//headerTableCells[1].InnerHtml = "<img src=\"/gfx/money-gbp-small.gif\" alt=\"Money\" align=\"right\" height=\"11\" width=\"13\"/>";

			#endregion

			#region Data Rows
			HtmlTableRow[] dataRows = new HtmlTableRow[salesUsrs.Count];
			for (int i = 0; i < dataRows.Length; i++)
			{
				dataRows[i] = new HtmlTableRow();
				HtmlTableCell[] dataTableCells = new HtmlTableCell[2];
				dataTableCells[0] = new HtmlTableCell();
				dataTableCells[1] = new HtmlTableCell();
				dataTableCells[1].Align = "right";
				dataTableCells[1].InnerHtml = "£0";

				dataTableCells[0].Style.Add("border-left", "0px;");

				if (i == dataRows.Length - 1)
				{
					dataTableCells[0].Style.Add("border-bottom", "0px;");
					dataTableCells[1].Style.Add("border-bottom", "0px;");
				}
				if (invoices.Count > i)
				{
					dataTableCells[0].InnerHtml = invoices[i].ExtraSelectElements["Usr_FirstName"].ToString();
					dataTableCells[1].InnerHtml = "£" + Convert.ToDouble(invoices[i].ExtraSelectElements["TotalSales"]).ToString("#,##0");
				}

				//for (int m = 0; m < invoices.Count; m++)
				//{
				//    if (invoices[m].SalesUsrK == salesUsrs[i].K)
				//    {

				//        break;
				//    }
				//}
				salesMonthTable.Rows.Add(dataRows[i]);
				for (int j = 0; j < dataTableCells.Length; j++)
				{
					dataRows[i].Cells.Add(dataTableCells[j]);
				}
			}

			// Populate remaining names to table. Since not all sales people will have made sales for the given month, we need to add the remaining sales people with total sales of zero
			if (invoices.Count < salesUsrs.Count)
			{
				bool exists;
				foreach (UsrDataHolder udh in salesUsrs)
				{
					exists = false;
					foreach (HtmlTableRow row in salesMonthTable.Rows)
					{
						if (row.Cells[0].InnerHtml.Equals(udh.FirstName))
						{
							exists = true;
							break;
						}
					}
					if (!exists)
					{
						foreach (HtmlTableRow row in salesMonthTable.Rows)
						{
							if (row.Cells[0].InnerHtml.Length == 0)
							{
								row.Cells[0].InnerHtml = udh.FirstName;
								break;
							}
						}
					}
				}
			}
			#endregion








			control.Controls.Add(salesMonthTable);


			#region Sales: Total
			Query totalSalesAmountQueryTotal = new Query(new And(new Q(Usr.Columns.IsSalesPerson, true),
															new Or(new Q(Usr.Columns.SalesTeam, Usr.SalesTeams.PromoterSalesTeam), new Q(Usr.Columns.SalesTeam, Usr.SalesTeams.SmallBusinessSalesTeam)),
															new Q(Invoice.Columns.SalesUsrAmount, QueryOperator.NotEqualTo, 0),
															new Or(new And(new Q(Invoice.Columns.Type, Invoice.Types.Invoice),
																		   new Q(Invoice.Columns.Paid, true),
																		   new Q(Invoice.Columns.PaidDateTime, QueryOperator.GreaterThanOrEqualTo, weekStart),
																		   new Q(Invoice.Columns.PaidDateTime, QueryOperator.LessThan, weekEnd)),
																   new And(new Q(Invoice.Columns.Type, Invoice.Types.Credit),
																		   new Q(Invoice.Columns.CreatedDateTime, QueryOperator.GreaterThanOrEqualTo, weekStart),
																		   new Q(Invoice.Columns.CreatedDateTime, QueryOperator.LessThan, weekEnd)))));
			totalSalesAmountQueryTotal.TableElement = new Join(Usr.Columns.K, Invoice.Columns.SalesUsrK, QueryJoinType.Left);
			totalSalesAmountQueryTotal.ExtraSelectElements.Add("TotalSales", "SUM(ISNULL([Invoice].[SalesUsrAmount],0))");
			totalSalesAmountQueryTotal.Columns = new ColumnSet();
			InvoiceSet inv = new InvoiceSet(totalSalesAmountQueryTotal);
			#endregion

			double tot = 0.0;
			double.TryParse(inv[0].ExtraSelectElements["TotalSales"].ToString(), out tot);

			control.Controls.Add(new LiteralControl(@"<div style=""padding-left:4px;padding-right:4px;""><p><b>Total: " + tot.ToString("#,##0") + @"</b></p></div>"));


		}

		#region GeneratePromoterAccountsTable (removed)
		//public void GeneratePromoterAccountsTable()
		//{
		//    PromoterAccountInfoPanel.Visible = false;

		//    if (Usr.Current != null && Usr.Current.IsPromoter)
		//    {
		//        Usr.Current.PromotersClear();
		//        Query outstandingPromoterQuery = new Query(new And(new Q(PromoterUsr.Columns.UsrK, Usr.Current.K),
		//                                                           new Q(Invoice.Columns.Paid, 0),
		//                                                           new Q(Invoice.Columns.Type, Invoice.Types.Invoice),
		//                                                           Promoter.EnabledQ));
		//        outstandingPromoterQuery.TableElement = new Join(Usr.PromoterJoin, new TableElement(TablesEnum.Invoice), QueryJoinType.Inner, Promoter.Columns.K, Invoice.Columns.PromoterK);
		//        outstandingPromoterQuery.Columns = new ColumnSet(Promoter.Columns.UrlName, Promoter.Columns.Name, Promoter.Columns.K, Promoter.Columns.OverrideApplyTicketFundsToInvoices);
		//        outstandingPromoterQuery.OrderBy = new OrderBy(Promoter.Columns.Name);
		//        outstandingPromoterQuery.Distinct = true;
		//        outstandingPromoterQuery.DistinctColumn = Promoter.Columns.K;
		//        PromoterSet promoters = new PromoterSet(outstandingPromoterQuery);
		//        promoters.Reset();

		//        PromoterAccountInfoPanel.Visible = promoters.Count > 0;

		//        if (promoters.Count > 0)
		//        {
		//            #region Table
		//            this.PromoterAccountInfoTable.Rows.Clear();
		//            #endregion

		//            #region Header Row
		//            //HtmlTableRow headerRow = new HtmlTableRow();
		//            //headerRow.Attributes.Add("class", "dataGridHeader");
		//            //this.PromoterAccountInfoTable.Rows.Add(headerRow);
		//            //HtmlTableCell[] headerTableCells = new HtmlTableCell[2];
		//            //for (int i = 0; i < headerTableCells.Length; i++)
		//            //{
		//            //    headerTableCells[i] = new HtmlTableCell("th");
		//            //    headerTableCells[i].Align = "left";
		//            //    headerRow.Cells.Add(headerTableCells[i]);
		//            //}
		//            //headerTableCells[0].InnerHtml = "Account";
		//            //headerTableCells[1].InnerHtml = "Balance";
		//            #endregion

		//            #region Data Rows
		//            HtmlTableRow[] dataRows = new HtmlTableRow[promoters.Count*2];
		//            for (int i = 0; i < promoters.Count; i++)
		//            {
		//                dataRows[i*2] = new HtmlTableRow();
		//                dataRows[i*2+1] = new HtmlTableRow();
		//                dataRows[i * 2 + 1].Attributes.Add("class", "dataGridRow");
		//                this.PromoterAccountInfoTable.Rows.Add(dataRows[i*2]);
		//                this.PromoterAccountInfoTable.Rows.Add(dataRows[i*2+1]);
		//                HtmlTableCell promoterNameTableCell = new HtmlTableCell();
		//                HtmlTableCell promoterBalanceTableCell = new HtmlTableCell();
		//                dataRows[i*2].Cells.Add(promoterNameTableCell);
		//                dataRows[i*2+1].Cells.Add(promoterBalanceTableCell);

		//                //for (int j = 0; j < dataTableCells.Length; j++)
		//                //{
		//                //    dataTableCells[j] = new HtmlTableCell();
		//                //    dataRows[i].Cells.Add(dataTableCells[j]);
		//                //}

		//                string promoterNameAbrv = promoters[i].Name;
		//                if (promoters[i].Name.Length > 14)
		//                {
		//                    promoterNameTableCell.InnerHtml = "<nobr><a href=\"" + promoters[i].Url() + "\" onmouseover=\"stt('" + promoters[i].Name.ToString() + "')\" onmouseout=\"htm();\" onclick=\"htm();\" >"
		//                                                + promoters[i].Name.Substring(0, 13).Trim() + "..." + "</a></nobr>";
		//                }
		//                else
		//                {
		//                    promoterNameTableCell.InnerHtml = "<nobr><a href=\"" + promoters[i].Url() + "\" >" + promoters[i].Name + "</a></nobr>";
		//                }
		//                promoterBalanceTableCell.InnerHtml = "<nobr><a href=\"" + promoters[i].UrlApp("invoices") + "\" >";
		//                decimal promoterBalance = promoters[i].GetBalance();
		//                if (promoterBalance < 0)
		//                    promoterBalanceTableCell.InnerHtml += "<font color=\"#AB1010\"><b>" + Math.Abs(promoterBalance).ToString("c") + "</b></font>";
		//                else
		//                    promoterBalanceTableCell.InnerHtml += promoterBalance.ToString("c");

		//                promoterBalanceTableCell.InnerHtml += "</a></nobr>";
		//                promoterBalanceTableCell.Align = "right";
		//            }

		//            #endregion
		//        }
		//    }
		//}
		#endregion

		#endregion

	}
}
