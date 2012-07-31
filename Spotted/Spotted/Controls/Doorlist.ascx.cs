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
using System.Collections.Generic;
using Bobs;

namespace Spotted.Controls
{
	public partial class Doorlist : System.Web.UI.UserControl
	{
		public bool NoTicketsToDisplay { get; private set; }

		#region CurrentEvent
		private Event currentEvent;
		public Event CurrentEvent
		{
			get
			{
				if (currentEvent == null && ViewState["EventK"] != null)
				{
					currentEvent = new Event((int)ViewState["EventK"]);
				}
				return currentEvent;
			}
			set
			{
				currentEvent = value;
				if (value != null)
					ViewState["EventK"] = value.K;
				else
					ViewState["EventK"] = null;
			}
		}
		#endregion
		#region ForExport
		private bool? forExport;
		public bool ForExport 
		{
			get
			{
				if (!forExport.HasValue)
				{
					if (ViewState["ForExport"] != null)
					{
						forExport = (bool)ViewState["ForExport"];
					}
					else
					{
						forExport = false;
					}
				}
				return forExport.Value;
			}
			set
			{
				forExport = value;
				ViewState["ForExport"] = value;
			}
		}
		#endregion
		public int PromoterK { get; set; }

		#region Variables
		TicketRunSet CurrentTicketRuns;
		DoorlistOrderBy ListOrderBy
		{
			get
			{
				return (DoorlistOrderBy)int.Parse(OrderByDropDownList.SelectedValue);
			}
		}
		string ExportFileName = "Doorlist";
		public bool isExportXLSFile = false;
		public bool isExportCSVFile = false;
		#endregion

		public override void DataBind()
		{
			if (CurrentEvent == null || CurrentEvent.K == 0)
				throw new Exception("No event or ticket run selected");

			VerifyUserPermissions();
			SetupOrderByDropDownList();
			SetupTicketRunSelection();
			ExportFileName = Utilities.GetAsciiOnly("Doorlist: " + CurrentEvent.Name + " on " + Utilities.DateToString(CurrentEvent.DateTime));
			EventName.InnerHtml = CurrentEvent.Name + " on " + Utilities.DateToString(CurrentEvent.DateTime);

			this.uiLogo.Visible = ForExport;
			this.uiExportOptions.Visible = ForExport;
			this.DoorlistGridView.EnableViewState = !ForExport;

			// Hide doorlist if not all ticket runs have ended. Admins can view doorlist at any time.
			if (!Usr.Current.IsAdmin && CurrentTicketRuns != null)
			{
				foreach (TicketRun tr in CurrentTicketRuns)
				{
					if (tr.Status != TicketRun.TicketRunStatus.Ended && tr.Status != TicketRun.TicketRunStatus.SoldOut)
					{
						HideOnPrintP.Visible = false;
						DoorlistGridView.Visible = false;
						EndAllTicketRunsPanel.Visible = true;
						break;
					}
				}
			}

			DisplayDoorList();

		}

		#region Enum
		public enum DoorlistOrderBy
		{
			FirstName,
			LastName,
			CardNumberEnd,
			Code
		}
		#endregion

		#region Properties
		public bool IsExportFile
		{
			get
			{
				return isExportXLSFile || isExportCSVFile;
			}
		}
		#endregion

		#region ViewState
		#region SaveViewState()
		protected override object SaveViewState()
		{
			this.ViewState["ExportFileName"] = ExportFileName;
			return base.SaveViewState();
		}
		#endregion
		#region LoadViewState()
		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);
			if (this.ViewState["ExportFileName"] != null) ExportFileName = (string)this.ViewState["ExportFileName"];
		}
		#endregion
		#endregion

		#region Methods
		public void VerifyUserPermissions()
		{
			Usr.KickUserIfNotLoggedIn();

			if (!(Usr.Current.IsAdmin || (Usr.Current.IsPromoter && CurrentEvent.IsPromoter(Usr.Current))))
			{
				bool match = false;
				PromoterUsrSet pus = new PromoterUsrSet(new Query(new Q(PromoterUsr.Columns.UsrK, Usr.Current.K)));
				foreach (PromoterUsr pu in pus)
				{
					try
					{
						new TicketPromoterEvent(pu.PromoterK, CurrentEvent.K);
						match = true;
						break;
					}
					catch { }
				}
				if (!match)
					throw new Exception("You cannot view this ticket run doorlist! It does not belong to your promoter account.");
			}
		}

		private void SetupOrderByDropDownList()
		{
			this.OrderByDropDownList.Items.Clear();
			this.OrderByDropDownList.Items.Add(new ListItem(Utilities.CamelCaseToString(DoorlistOrderBy.FirstName.ToString()), ((int)DoorlistOrderBy.FirstName).ToString()));
			this.OrderByDropDownList.Items.Add(new ListItem(Utilities.CamelCaseToString(DoorlistOrderBy.LastName.ToString()), ((int)DoorlistOrderBy.LastName).ToString()));
			this.OrderByDropDownList.Items.Add(new ListItem(Utilities.CamelCaseToString(DoorlistOrderBy.CardNumberEnd.ToString()), ((int)DoorlistOrderBy.CardNumberEnd).ToString()));
			//this.OrderByDropDownList.SelectedValue = ((int)DoorlistOrderBy.FirstName).ToString();
		}

		private void AddCodeListItemToOrderByDropDownList()
		{
			bool flag = true;
			foreach (ListItem li in this.OrderByDropDownList.Items)
			{
				if (li.Text == Utilities.CamelCaseToString(DoorlistOrderBy.Code.ToString()))
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				this.OrderByDropDownList.Items.Add(new ListItem(Utilities.CamelCaseToString(DoorlistOrderBy.Code.ToString()), ((int)DoorlistOrderBy.Code).ToString()));
			}
		}

		private void SetupTicketRunSelection()
		{
			Q q = new Q(TicketRun.Columns.EventK, CurrentEvent.K);
			Query ticketRunForEventQuery = new Query(q);
			if (PromoterK > 0)
			{
				ticketRunForEventQuery.QueryCondition = new And(q, new Q(TicketRun.Columns.PromoterK, PromoterK));
			}
			//ticketRunForEventQuery.Columns = new ColumnSet(TicketRun.Columns.K, TicketRun.Columns.PromoterK, TicketRun.Columns.EventK, TicketRun.Columns.Price, TicketRun.Columns.Name, 
			//                                               TicketRun.Columns.BrandK, TicketRun.Columns.StartDateTime, TicketRun.Columns.EndDateTime, TicketRun.Columns.Paused, 
			//                                               TicketRun.Columns.MaxTickets, TicketRun.Columns.FollowsTicketRunK, TicketRun.Columns.SoldTickets);
			ticketRunForEventQuery.OrderBy = new OrderBy(new OrderBy(TicketRun.Columns.ListOrder), new OrderBy(TicketRun.Columns.Price));

			CurrentTicketRuns = new TicketRunSet(ticketRunForEventQuery);

			this.TicketRunSelectRepeater.DataSource = CurrentTicketRuns;
			this.TicketRunSelectRepeater.DataBind();
		}

		private void DisplayDoorList()
		{
			if (!EndAllTicketRunsPanel.Visible)
			{
				List<int> ticketRunKs = new List<int>();
				foreach (RepeaterItem ri in TicketRunSelectRepeater.Items)
				{
					if (((CheckBox)ri.FindControl("TicketRunCheckBox")).Checked)
					{
						ticketRunKs.Add(Convert.ToInt32(((TextBox)ri.FindControl("TicketRunKTextBox")).Text));
						((HtmlTableRow)ri.FindControl("TicketRunRow")).Attributes.Remove("class");
					}
					else
						((HtmlTableRow)ri.FindControl("TicketRunRow")).Attributes.Add("class", "HideOnPrint");
				}

				if (ticketRunKs.Count == 0)
				{
					DoorlistGridView.Visible = false;
					HideOnPrintP.Visible = false;
					NoTicketsToDisplay = true;
					return;
				}


				//TemplateField firstNameColumn = (TemplateField)DoorlistGridView.Columns[0];
				//TemplateField lastNameColumn = (TemplateField)DoorlistGridView.Columns[1];
				//TemplateField codeColumn = (TemplateField)DoorlistGridView.Columns[2];
				//TemplateField cardNumberEndColumn = (TemplateField)DoorlistGridView.Columns[3];
				//TemplateField ticketRunNameColumn = (TemplateField)DoorlistGridView.Columns[4];
				//TemplateField ticketsColumn = (TemplateField)DoorlistGridView.Columns[5];
				//TemplateField cv2Column = (TemplateField)DoorlistGridView.Columns[6];

				//DoorlistGridView.Columns.Clear();


				Q selectTicketRunQ = new Q();

				selectTicketRunQ = new Q(Ticket.Columns.TicketRunK, ticketRunKs[0]);
				for (int i = 1; i < ticketRunKs.Count; i++)
				{
					selectTicketRunQ = new Or(selectTicketRunQ,
											  new Q(Ticket.Columns.TicketRunK, ticketRunKs[i]));
				}

				Query ticketQuery = new Query(new And(selectTicketRunQ,
													  Ticket.ValidTicketsQ,
													  new Or(new Q(Ticket.Columns.IsFraud, QueryOperator.IsNull, null), new Q(Ticket.Columns.IsFraud, false))
													  ));
				ticketQuery.Columns = new ColumnSet(Ticket.Columns.K, Ticket.Columns.FirstName, Ticket.Columns.LastName, Ticket.Columns.Code, Ticket.Columns.CardNumberEnd, Ticket.Columns.Quantity, Ticket.Columns.TicketRunK, Ticket.Columns.CardCheckedByPromoter, Ticket.Columns.CardCheckAttempts, Promoter.Columns.WillCheckCardsForPurchasedTickets);
				ticketQuery.TableElement = new Join(
					new Join(Ticket.Columns.TicketRunK, TicketRun.Columns.K),
					Promoter.Columns.K, TicketRun.Columns.PromoterK);
				ticketQuery.ExtraSelectElements.Add("TicketRunPriceName", "CASE WHEN LEN([TicketRun].[Name]) > 0 THEN '£' + CONVERT(varchar(9), CONVERT(money,[TicketRun].[Price])) + ' : ' + [TicketRun].[Name] ELSE '£' + CONVERT(varchar(9), CONVERT(money,[TicketRun].[Price])) END");


				for (int i = 0; i < 8; i++)
				{
					DoorlistGridView.Columns[i].Visible = false;
				}

				TemplateField codeColumn = null;

				if (ListOrderBy.Equals(DoorlistOrderBy.FirstName))
				{
					ticketQuery.OrderBy = new OrderBy(new OrderBy(Ticket.Columns.FirstName), new OrderBy(Ticket.Columns.LastName));
					//DoorlistGridView.Columns.Add(firstNameColumn);
					//DoorlistGridView.Columns.Add(lastNameColumn);
					//DoorlistGridView.Columns.Add(codeColumn);
					//DoorlistGridView.Columns.Add(cardNumberEndColumn);
					DoorlistGridView.Columns[0].Visible = true;
					DoorlistGridView.Columns[1].Visible = true;
					codeColumn = (TemplateField)DoorlistGridView.Columns[2];
					DoorlistGridView.Columns[2].Visible = true;
					DoorlistGridView.Columns[3].Visible = true;
				}
				else if (ListOrderBy.Equals(DoorlistOrderBy.LastName))
				{
					ticketQuery.OrderBy = new OrderBy(new OrderBy(Ticket.Columns.LastName), new OrderBy(Ticket.Columns.FirstName));
					//DoorlistGridView.Columns.Add(lastNameColumn);
					//DoorlistGridView.Columns.Add(firstNameColumn);
					//DoorlistGridView.Columns.Add(codeColumn);
					//DoorlistGridView.Columns.Add(cardNumberEndColumn);
					DoorlistGridView.Columns[1].Visible = true;
					DoorlistGridView.Columns[4].Visible = true;
					codeColumn = (TemplateField)DoorlistGridView.Columns[6];
					DoorlistGridView.Columns[6].Visible = true;
					DoorlistGridView.Columns[7].Visible = true;
				}
				else if (ListOrderBy.Equals(DoorlistOrderBy.CardNumberEnd))
				{
					ticketQuery.OrderBy = new OrderBy(new OrderBy(Ticket.Columns.CardNumberEnd), new OrderBy(Ticket.Columns.FirstName), new OrderBy(Ticket.Columns.LastName));
					//DoorlistGridView.Columns.Add(cardNumberEndColumn);
					//DoorlistGridView.Columns.Add(firstNameColumn);
					//DoorlistGridView.Columns.Add(lastNameColumn);
					//DoorlistGridView.Columns.Add(codeColumn);
					DoorlistGridView.Columns[3].Visible = true;
					DoorlistGridView.Columns[4].Visible = true;
					DoorlistGridView.Columns[5].Visible = true;
					codeColumn = (TemplateField)DoorlistGridView.Columns[6];
					DoorlistGridView.Columns[6].Visible = true;
				}
				else if (ListOrderBy.Equals(DoorlistOrderBy.Code))
				{
					ticketQuery.OrderBy = new OrderBy(new OrderBy(Ticket.Columns.Code), new OrderBy(Ticket.Columns.FirstName), new OrderBy(Ticket.Columns.LastName));
					//DoorlistGridView.Columns.Add(codeColumn);
					//DoorlistGridView.Columns.Add(firstNameColumn);
					//DoorlistGridView.Columns.Add(lastNameColumn);
					//DoorlistGridView.Columns.Add(cardNumberEndColumn);
					codeColumn = (TemplateField)DoorlistGridView.Columns[2];
					DoorlistGridView.Columns[2].Visible = true;
					DoorlistGridView.Columns[4].Visible = true;
					DoorlistGridView.Columns[5].Visible = true;
					DoorlistGridView.Columns[7].Visible = true;
				}
				//DoorlistGridView.Columns.Add(ticketRunNameColumn);
				//DoorlistGridView.Columns.Add(ticketsColumn);
				//DoorlistGridView.Columns.Add(cv2Column);

				TicketSet doorlistTickets = new TicketSet(ticketQuery);

				if (doorlistTickets.Count == 0)
				{
					DoorlistGridView.Visible = false;
					NoTicketsToDisplay = true;
					uiNoTickets.Visible = true;
					HideOnPrintP.Visible = false;
					return;
				}
				HideOnPrintP.Visible = true;
				uiNoTickets.Visible = false;

				DoorlistGridView.Visible = true;


				codeColumn.Visible = false;
				TemplateField cv2Column = (TemplateField)DoorlistGridView.Columns[10];
				cv2Column.Visible = false;
				foreach (Ticket ticket in doorlistTickets)
				{
					if (codeColumn.Visible == false && ticket.Code.Length > 0)
					{
						codeColumn.Visible = true;
					}
					if (cv2Column.Visible == false && ticket.ExtraSelectElements["Promoter_WillCheckCardsForPurchasedTickets"].ToString() == bool.TrueString)
					{
						cv2Column.Visible = true;
					}
					if (codeColumn.Visible && cv2Column.Visible) break;
				}
				doorlistTickets.Reset();

				if (codeColumn.Visible)
					AddCodeListItemToOrderByDropDownList();

				DoorlistGridView.DataSource = doorlistTickets;
				DoorlistGridView.DataBind();
			}
		}

		public string TicketCheckBoxes(Ticket ticket)
		{
			string output = "";
			if (!IsExportFile && ForExport)
			{
				for (int i = 0; i < ticket.Quantity; i++)
				{
					output += "<input id=\"Checkbox" + ticket.K.ToString() + "-" + i.ToString() + "\" type=\"checkbox\" />";
				}
			}
			return output;
		}

		private void OutputGridViewToExcelFile(bool isCSV)
		{
			Response.Clear();

			string fileName = Cambro.Web.Helpers.StripHtml(ExportFileName).Replace(" ", "_");

			if (isCSV)
				fileName += ".csv";
			else
				fileName += ".xls";

			Response.AddHeader("content-disposition", "attachment; filename=" + fileName);

			Response.Charset = "Utf-8";

			// If you want the option to open the Excel file without saving, then comment out the line below
			// Response.Cache.SetCacheability(HttpCacheability.NoCache);

			if (isCSV)
				Utilities.ExportGridViewToCsv(DoorlistGridView, Response);
			else
				Utilities.ExportGridViewToExcel(DoorlistGridView, Response);
		}

		#endregion

		#region Page Events
		protected void ExportToExcelLinkButton_Click(object sender, EventArgs e)
		{
			isExportXLSFile = true;
			DisplayDoorList();
			OutputGridViewToExcelFile(false);
		}

		protected void ExportToCSVLinkButton_Click(object sender, EventArgs e)
		{
			isExportCSVFile = true;
			DisplayDoorList();
			OutputGridViewToExcelFile(true);
		}

		protected void OrderByDropDownList_SelectedIndexChanged(object sender, EventArgs e)
		{
			DisplayDoorList();
		}

		protected void TicketRunCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			DisplayDoorList();
		}

		protected void EndAllTicketRunsButton_Click(object sender, EventArgs e)
		{
			SetupTicketRunSelection();
			foreach (TicketRun tr in CurrentTicketRuns)
			{
				tr.EndTicketRun();
			}
			HideOnPrintP.Visible = true;
			DoorlistGridView.Visible = true;
			EndAllTicketRunsPanel.Visible = false;
			DisplayDoorList();
		}

		protected void CancelButton_Click(object sender, EventArgs e)
		{

		}
		#endregion


		#region CV2list
		private Hashtable cv2s;
		public Hashtable CV2s
		{
			get
			{
				if (cv2s == null)
				{
					cv2s = new Hashtable();
					foreach (GridViewRow gvr in this.DoorlistGridView.Rows)
					{
						TextBox tb = (TextBox)gvr.FindControl("uiCV2");
						if (tb.Visible && tb.Text.Length > 0)
						{
							cv2s.Add((int)DoorlistGridView.DataKeys[gvr.RowIndex].Value, tb.Text);
						}
					}
				}
				return cv2s;
			}
			set
			{
				foreach (GridViewRow gvr in this.DoorlistGridView.Rows)
				{
					if (value.Contains((int)DoorlistGridView.DataKeys[gvr.RowIndex].Value))
					{
						((TextBox)gvr.FindControl("uiCV2")).Text = (string)value[(int)DoorlistGridView.DataKeys[gvr.RowIndex].Value];
					}
				}
			}
		}
		#endregion
	}
}
