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

namespace Spotted.Pages.Venues
{
	public partial class Moderate : DsiUserControl
	{
		protected Panel ItemsPanel;
		protected Repeater ItemsRepeater;
		protected Button DeleteSelectedButton;
		protected HtmlGenericControl OutputP;
		protected DataGrid ModeratorsDataGrid;

		#region EnableSelected
		public void EnableSelected(object o, System.EventArgs e)
		{
			OutputP.InnerHtml = "";
			foreach (string str in Request.Form.Keys)
			{
				if (str.StartsWith("ucAdminEventSelectedK") && Request.Form[str].Equals("1"))
				{
					string str1 = str.Substring(21);
					OutputP.InnerHtml += "Enabling event " + str1 + "...";
					try
					{
						int eventK = int.Parse(str1);
						Event ev = new Event(eventK);
						ev.IsNew = false;
						ev.IsEdited = false;
						ev.Update();
						OutputP.InnerHtml += " Done.";
					}
					catch
					{
						OutputP.InnerHtml += " <b>FAILED</b> - exception while enabling event. Please contact admin with details.";
					}
					OutputP.InnerHtml += "<br>";
				}


				if (str.StartsWith("ucAdminVenueSelectedK") && Request.Form[str].Equals("1"))
				{
					string str1 = str.Substring(21);
					OutputP.InnerHtml += "Enabling venue " + str1 + "...";
					try
					{
						int venueK = int.Parse(str1);
						Venue ven = new Venue(venueK);
						ven.IsNew = false;
						ven.IsEdited = false;
						ven.Update();
						OutputP.InnerHtml += " Done.";
					}
					catch
					{
						OutputP.InnerHtml += " <b>FAILED</b> - exception while enabling venue. Please contact admin with details.";
					}
					OutputP.InnerHtml += "<br>";
				}
			}

			OutputP.Visible = true;

			Bind();
		}
		#endregion
		#region DeleteSelected
		public void DeleteSelected(object o, System.EventArgs e)
		{
			OutputP.InnerHtml = "";
			foreach (string str in Request.Form.Keys)
			{
				if (str.StartsWith("ucAdminEventSelectedK") && Request.Form[str].Equals("1"))
				{
					string str1 = str.Substring(21);
					OutputP.InnerHtml += "Deleting event " + str1 + "...";
					try
					{
						int eventK = int.Parse(str1);
						Event ev = new Event(eventK);
						if (ev.IsNew || ev.IsEdited)
						{
							Event.DeleteReturnStatus status = ev.DeleteAllUsr(Usr.Current);
							if (status.Equals(Event.DeleteReturnStatus.FailComments))
							{
								OutputP.InnerHtml += " <b>FAILED</b> - event has " + ev.TotalComments + " comments. Please contact admin to delete this event.";
							}
							else if (status.Equals(Event.DeleteReturnStatus.FailPhotos))
							{
								OutputP.InnerHtml += " <b>FAILED</b> - event has " + ev.TotalPhotos + " photos. Please contact admin to delete this event.";
							}
							else if (status.Equals(Event.DeleteReturnStatus.FailNoPermission))
							{
								OutputP.InnerHtml += " <b>FAILED</b> - no permission to delete this event. Please contact admin with details.";
							}
							else if (status.Equals(Event.DeleteReturnStatus.FailException))
							{
								OutputP.InnerHtml += " <b>FAILED</b> - exception while deleting event. Please contact admin with details.";
							}
							else if (status.Equals(Event.DeleteReturnStatus.FailPromoter))
							{
								OutputP.InnerHtml += " <b>FAILED</b> - event has promoter objects - e.g. banners, guestlists or competitions. Please contact admin to delete this event.";
							}
							else if (status.Equals(Event.DeleteReturnStatus.Success))
							{
								OutputP.InnerHtml += " Done.";
							}
						}
						else
						{
							OutputP.InnerHtml += " <b>FAILED</b> - event is not new - someone must have enabled it. Please contact admin with details.";
						}
					}
					catch
					{
						OutputP.InnerHtml += " <b>FAILED</b> - exception while deleting event. Maybe someone already deleted this event.";
					}
					OutputP.InnerHtml += "<br>";
				}


				if (str.StartsWith("ucAdminVenueSelectedK") && Request.Form[str].Equals("1"))
				{
					string str1 = str.Substring(21);
					OutputP.InnerHtml += "Deleting venue " + str1 + "...";
					try
					{
						int venueK = int.Parse(str1);
						Venue ven = new Venue(venueK);
						if (ven.IsNew || ven.IsEdited)
						{
							Venue.DeleteReturnStatus status = ven.DeleteAllUsr(Usr.Current);
							if (status.Equals(Venue.DeleteReturnStatus.FailComments))
							{
								OutputP.InnerHtml += " <b>FAILED</b> - venue has " + ven.TotalComments + " comments. Please contact admin to delete this venue.";
							}
							else if (status.Equals(Venue.DeleteReturnStatus.FailEvents))
							{
								OutputP.InnerHtml += " <b>FAILED</b> - venue has " + ven.Events.Count + " photos. Please contact admin to delete this venue.";
							}
							else if (status.Equals(Venue.DeleteReturnStatus.FailPhotos))
							{
								OutputP.InnerHtml += " <b>FAILED</b> - venue has more than 5 photos. Please contact admin to delete this venue.";
							}
							else if (status.Equals(Venue.DeleteReturnStatus.FailNoPermission))
							{
								OutputP.InnerHtml += " <b>FAILED</b> - no permission to delete this venue. Please contact admin with details.";
							}
							else if (status.Equals(Venue.DeleteReturnStatus.FailException))
							{
								OutputP.InnerHtml += " <b>FAILED</b> - exception while deleting venue. Please contact admin with details.";
							}
							else if (status.Equals(Venue.DeleteReturnStatus.FailPromoter))
							{
								OutputP.InnerHtml += " <b>FAILED</b> - venue has promoter objects - e.g. banners, guestlists or competitions. Please contact admin with details.";
							}
							else if (status.Equals(Venue.DeleteReturnStatus.Success))
							{
								OutputP.InnerHtml += " Done.";
							}
						}
						else
						{
							OutputP.InnerHtml += " <b>FAILED</b> - venue is not new - someone must have enabled it. Please contact admin with details.";
						}
					}
					catch
					{
						OutputP.InnerHtml += " <b>FAILED</b> - exception while deleting venue. Maybe someone already deleted this venue.";
					}
					OutputP.InnerHtml += "<br>";
				}
			}

			OutputP.Visible = true;

			Bind();


		}
		#endregion

		void Bind()
		{
			Query q = new Query();
			q.NoLock = true;
			q.QueryCondition = new Q(Usr.Columns.AdminLevel, Usr.AdminLevels.Super);
			q.OrderBy = new OrderBy(Usr.Columns.NickName);
			q.Columns = new ColumnSet(Usr.LinkColumns, Usr.Columns.DateTimeLastPageRequest, Usr.Columns.IsLoggedOn, Usr.Columns.FirstName, Usr.Columns.LastName, Usr.Columns.IsSkeleton);
			UsrSet us = new UsrSet(q);
			ModeratorsDataGrid.DataSource = us;
			ModeratorsDataGrid.DataBind();

			if (ContainerPage.Url["usrk"].IsInt)
			{
				int UsrK = ContainerPage.Url["usrk"];
				if (ContainerPage.Url["type"] == 1)
				{
					Query vsq = new Query();
					vsq.QueryCondition = new And(new Q(Venue.Columns.IsNew, true),
						new Q(Venue.Columns.ModeratorUsrK, UsrK));
					vsq.TopRecords = 10;
					vsq.NoLock = true;
					VenueSet vs = new VenueSet(vsq);
					if (vs.Count == 0)
						ItemsPanel.Visible = false;
					else
					{
						ItemsRepeater.DataSource = vs;
						ItemsRepeater.ItemTemplate = this.LoadTemplate("/Templates/Venues/Admin.ascx");
						ItemsRepeater.DataBind();
					}
				}
				else
				{
					ItemsPanel.Visible = false;
				}
			}
			else
			{
				ItemsPanel.Visible = false;
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Usr.Current.IsSuper)
				throw new Exception("Only super admin!");
			ContainerPage.SetPageTitle("New events and venues");
			DeleteSelectedButton.Attributes["onclick"] = "return confirm('Are you sure? This will delete all selected items, and any objects that are under them (e.g. if you delete a venue, it will delete ALL events at that venue!)')";
			Bind();
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
	}
}
