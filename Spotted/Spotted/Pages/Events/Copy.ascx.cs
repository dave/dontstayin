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
using System.IO;

namespace Spotted.Pages.Events
{
	public partial class Copy : DsiUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			ContainerPage.SetPageTitle("Copy event");
			if (!Page.IsPostBack)
			{
				ChangePanel(PanelStart);
				if (ContainerPage.Url.HasEventObjectFilter)
				{
					this.uiEventPicker.Event = ContainerPage.Url.ObjectFilterEvent;
				}
			}

			this.PanelConflict_Load(sender, e);
			this.PanelReview_Load(sender, e);
			this.PanelSaved_Load(sender, e);
		}

		#region PanelStart

		public void PanelStartAddButtonClick(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				EventSet evs = new EventSet(
					new Query(
						new And(
							new Q(Event.Columns.VenueK, SelectedEvent.VenueK),
							new Q(Event.Columns.DateTime, NewDateCalendar.SelectedDate)
						)
					)
				);
				if (evs.Count > 0)
				{
					PanelConflictCheckbox.Checked = false;
					ChangePanel(PanelConflict);
				}
				else
				{
					int newK = DuplicateEvent();
					this.ViewState["NewEventK"] = newK;
					BindPanelReview(newK);
					ChangePanel(PanelReview);
				}
			}
		}

		public void SameDateVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = SelectedEvent != null && SelectedEvent.DateTime != NewDateCalendar.SelectedDate;
		}
		public void SelectDateVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = NewDateCalendar.SelectedDate > new DateTime(1970, 1, 1);
		}

		#region SelectedEvent
		public Event SelectedEvent
		{
			get
			{
				return this.uiEventPicker.Event;
			}
		}
		#endregion

		#endregion

		#region PanelConflict
		public void PanelConflict_Load(object o, System.EventArgs e)
		{
			if (SelectedEvent != null && NewDateCalendar.SelectedDate > new DateTime(1970, 1, 1))
			{
				EventSet evs = new EventSet(
					new Query(
						new And(
							new Q(Event.Columns.VenueK, SelectedEvent.VenueK),
							new Q(Event.Columns.DateTime, NewDateCalendar.SelectedDate)
						)
					)
				);
				if (evs.Count > 0)
				{
					PanelConflictDataGrid.DataSource = evs;
					PanelConflictDataGrid.DataBind();
				}
			}
		}
		public void PanelConflictBack(object o, System.EventArgs e)
		{
			ChangePanel(PanelStart);
		}
		public void PanelConflictNext(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				int newK = DuplicateEvent();
				this.ViewState["NewEventK"] = newK;
				BindPanelReview(newK);
				ChangePanel(PanelReview);
			}
		}
		public void PanelConflictVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = PanelConflictCheckbox.Checked;
		}
		#endregion

		//PanelReview
		#region PanelReview
		public void PanelReview_Load(object o, System.EventArgs e)
		{
			if (this.ViewState["NewEventK"] != null)
			{
				BindPanelReview(int.Parse(this.ViewState["NewEventK"].ToString()));
			}
		}
		public void BindPanelReview(int NewEventK)
		{
			Event NewEvent = new Event(NewEventK);
			ReviewVenue.Text = NewEvent.Venue.Name + " in " + NewEvent.Venue.Place.Name;
			ReviewDate.Text = NewEvent.DateTime.ToShortDateString();
			ReviewName.Text = NewEvent.Name;
			ReviewStartTime.Text = NewEvent.StartTime.ToString();
			ReviewShortDetailsHtml.Text = NewEvent.ShortDetailsHtmlRender;
			ReviewLongDetailsHtml.Text = NewEvent.LongDetailsHtmlRender;
			ReviewCapacity.Text = (NewEvent.Capacity == 0 ? "(not specified)" : NewEvent.Capacity.ToString());

			//TODO: Show brands

			if (NewEvent.MusicTypesString.Length != 0)
			{
				ReviewMusicTypes.Text = NewEvent.MusicTypesString;
			}
			else
			{
				ReviewMusicTypes.Text = "None selected";
			}

			ReviewPicTd.Visible = NewEvent.HasPic;
			ReviewNoPicTd.Visible = !NewEvent.HasPic;
			if (NewEvent.HasPic)
				ReviewPicImg.Src = NewEvent.PicPath;

			ReviewEditAnchor.HRef = NewEvent.UrlApp("edit", "page", "details");


		}
		public void ReviewNextClick(object o, System.EventArgs e)
		{
			ChangePanel(PanelSaved);
		}
		#endregion

		#region PanelSaved
		void PanelSaved_Load(object o, EventArgs e)
		{
			if (this.ViewState["NewEventK"] != null)
			{
				int newEventK = int.Parse(this.ViewState["NewEventK"].ToString());
				Event NewEvent = new Event(newEventK);
				//ucBannerPreview.EventK = newEventK;
				//ucBannerPreview.Bind();
				PanelSavedEventLink.HRef = NewEvent.Url();
				PanelSavedEventLink.InnerText = NewEvent.FriendlyName;
				PanelSavedSignUpLink.HRef = NewEvent.SpotterSignUpUrl;
			}

		}
		public void PanelSavedAnotherClick(object o, System.EventArgs e)
		{
			this.ViewState["NewEventK"] = null;
			ChangePanel(PanelStart);
		}
		#endregion


		public int DuplicateEvent()
		{
			Transaction t = null;//new Transaction();
			Event ev = new Event();
			try
			{
				ev.AddedDateTime = DateTime.Now;
				ev.VenueK = SelectedEvent.VenueK;
				ev.Name = SelectedEvent.Name;
				ev.StartTime = SelectedEvent.StartTime;
				ev.DateTime = NewDateCalendar.SelectedDate;
				ev.ShortDetailsHtml = SelectedEvent.ShortDetailsHtml;
				ev.LongDetailsHtml = SelectedEvent.LongDetailsHtml;
				ev.IsDescriptionText = SelectedEvent.IsDescriptionText;
				ev.IsDescriptionCleanHtml = SelectedEvent.IsDescriptionCleanHtml;
				ev.Capacity = SelectedEvent.Capacity;
				ev.AdminNote = "Event copied from event " + SelectedEvent.K.ToString() + " by owner " + DateTime.Now.ToString();

				if (SelectedEvent.AdminNote.Length > 0)
					ev.AdminNote += "\nprevious admin note: \n*******\n" + SelectedEvent.AdminNote + "\n*******";

				ev.OwnerUsrK = Usr.Current.K;

				ev.IsNew = true;
				ev.ModeratorUsrK = Usr.GetEventModeratorUsrK();
				
				ev.InitUrlFragment();
				ev.Update(t);
				if (SelectedEvent.Pic != Guid.Empty)
				{
					ev.Pic = Guid.NewGuid();

					Storage.AddToStore(
						Storage.GetFromStore(Storage.Stores.Pix, SelectedEvent.Pic, "jpg"),
						Storage.Stores.Pix,
						ev.Pic,
						"jpg",
						ev,
						"Pic");

				}
				ev.Update(t);
				

				foreach (Brand b in SelectedEvent.Brands)
				{
					EventBrand eb = new EventBrand();
					eb.BrandK = b.K;
					eb.EventK = ev.K;
					eb.Update(t);
				}

				ev.Venue.UpdateTotalEvents(t);
				ev.Owner.UpdateEventCount(t);

				if (SelectedEvent.MusicTypes.Count > 0)
				{
					foreach (MusicType mt in SelectedEvent.MusicTypes)
					{
						EventMusicType emt = new EventMusicType();
						emt.EventK = ev.K;
						emt.MusicTypeK = mt.K;
						emt.Update(t);
					}
				}
				ev.UpdateMusicTypesString(t);

				//t.Commit();
			}
			catch (Exception ex)
			{
				//t.Rollback();
				ev.DeleteAll(null);
				if (!ev.Pic.Equals(Guid.Empty))
				{
					Storage.RemoveFromStore(Storage.Stores.Pix, ev.Pic, "jpg");
				}
				throw ex;
			}
			finally
			{
				//t.Close();
			}

			return ev.K;

		}

		protected void ChangePanel(Panel p)
		{
			PanelStart.Visible = p.Equals(PanelStart);
			PanelConflict.Visible = p.Equals(PanelConflict);
			PanelReview.Visible = p.Equals(PanelReview);
			PanelSaved.Visible = p.Equals(PanelSaved);
		}

	 
	 
	}
}
