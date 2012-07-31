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
