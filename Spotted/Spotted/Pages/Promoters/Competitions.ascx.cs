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

namespace Spotted.Pages.Promoters
{
	public partial class Competitions : PromoterUserControl
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
					if (CurrentComp != null)
					{
						if (CurrentComp.PromoterK != CurrentPromoter.K)
							throw new DsiUserFriendlyException("You don't have permission to access this competition!");
					}
				}
				ensureSecure = true;
			}
		}
		bool ensureSecure = false;
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			EnsureSecure();
			ContainerPage.SetPageTitle("Competition administration");

			if (Mode.Equals(Modes.List))
				ChangePanel(PanelList);
			if (Mode.Equals(Modes.Pic))
				ChangePanel(PanelPic);
			//if (Mode.Equals(Modes.Winners))
			//	ChangePanel(PanelWinners);
		}

		void RedirectDone()
		{
			if (CurrentEvent != null)
				Response.Redirect(CurrentPromoter.UrlEventOptions(CurrentEvent) + "#CompPanel");
			else
				Response.Redirect(CurrentPromoter.UrlApp("competitions"));

		}


		#region PanelEdit
		protected Panel PanelEdit;
		protected TextBox EditPrizesFirstNumber, EditPrizesFirstDesc, EditPrizesSecondNumber, EditPrizesSecondDesc,
			EditPrizesThirdNumber, EditPrizesThirdDesc;
		protected DropDownList EditPrizesValue;
		protected RadioButton EditLinkNoneRadio, EditLinkEventRadio, EditLinkBrandRadio;
		protected HtmlGenericControl EditLinkEventP, EditLinkBrandP;
		protected DropDownList EditLinkEventDropDown, EditLinkBrandDropDown;
		protected HtmlAnchor EditLinkEventAnchor, EditLinkBrandAnchor;
		protected TextBox EditQuestion, EditAnswer1, EditAnswer2, EditAnswer3;
		protected RadioButton EditCorrectRadio1, EditCorrectRadio2, EditCorrectRadio3;
		protected DropDownList EditPrizeContact;
		protected System.Web.UI.WebControls.Calendar EditDateClose, EditDateStart;
		protected HtmlTableRow EditLinkTr;
		#region PanelEdit_Load
		public void PanelEdit_Load(object o, System.EventArgs e)
		{
			EnsureSecure();
			if (Mode.Equals(Modes.Add) || Mode.Equals(Modes.Edit))
			{
				if (Mode.Equals(Modes.Edit))
				{
					if (CurrentComp == null)
						throw new DsiUserFriendlyException("Must select a competition!");
					if (!Usr.Current.CanEdit(CurrentComp))
						throw new DsiUserFriendlyException("You can't edit this competition now. Call our promoter hotline on 0207 835 5599 and ask us to make the changes.");
				}
				else if (Mode.Equals(Modes.Add))
				{
					if (!Page.IsPostBack)
					{

					}
				}
				ChangePanel(PanelEdit);

				if (CurrentEvent != null)
				{
					EditLinkTr.Visible = false;
				}
				else
				{
					#region Future events
					if (!Page.IsPostBack)
					{
						if (UpcomingEvents.Count == 0)
						{
							EditLinkEventP.Visible = false;
						}
						else if (UpcomingEvents.Count == 1)
						{
							EditLinkEventDropDown.Visible = false;
							EditLinkEventAnchor.HRef = UpcomingEvents[0].Url();
							EditLinkEventAnchor.InnerText = UpcomingEvents[0].FriendlyName;
						}
						else
						{
							EditLinkEventAnchor.Visible = false;
							EditLinkEventDropDown.DataSource = UpcomingEvents;
							EditLinkEventDropDown.DataTextField = "FriendlyName";
							EditLinkEventDropDown.DataValueField = "K";
							EditLinkEventDropDown.DataBind();
						}
						if (CurrentPromoter.AllBrands.Count == 0)
						{
							EditLinkBrandP.Visible = false;
						}
						else if (CurrentPromoter.AllBrands.Count == 1)
						{
							EditLinkBrandDropDown.Visible = false;
							EditLinkBrandAnchor.HRef = CurrentPromoter.AllBrands[0].Url();
							EditLinkBrandAnchor.InnerText = CurrentPromoter.AllBrands[0].Name;
						}
						else
						{
							EditLinkBrandAnchor.Visible = false;
							EditLinkBrandDropDown.DataSource = CurrentPromoter.AllBrands;
							EditLinkBrandDropDown.DataTextField = "Name";
							EditLinkBrandDropDown.DataValueField = "K";
							EditLinkBrandDropDown.DataBind();
						}
					}
					#endregion
				}
				if (!Page.IsPostBack)
				{
					EditPrizeContact.DataSource = CurrentPromoter.AdminUsrs;
					EditPrizeContact.DataTextField = "FullNameDetailed";
					EditPrizeContact.DataValueField = "K";
					EditPrizeContact.DataBind();
				}
				#region Set up the form for editing
				if (!Page.IsPostBack && Mode.Equals(Modes.Edit))
				{
					Comp c = CurrentComp;
					EditDateStart.SelectedDate = c.DateTimeStart;
					EditDateStart.VisibleDate = c.DateTimeStart;
					EditDateClose.SelectedDate = c.DateTimeClose;
					EditDateClose.VisibleDate = c.DateTimeClose;
					EditQuestion.Text = c.Question;
					EditAnswer1.Text = c.Answer1;
					EditAnswer2.Text = c.Answer2;
					EditAnswer3.Text = c.Answer3;
					EditCorrectRadio1.Checked = c.CorrectAnswer == 1;
					EditCorrectRadio2.Checked = c.CorrectAnswer == 2;
					EditCorrectRadio3.Checked = c.CorrectAnswer == 3;
					EditPrizesFirstDesc.Text = c.Prize;
					EditPrizesSecondDesc.Text = c.Prize2;
					EditPrizesThirdDesc.Text = c.Prize3;
					EditSponsorDescriptionHtml.LoadHtml(c.SponsorDetails);
					EditPrizesFirstNumber.Text = c.Winners.ToString();
					if (c.Winners2 > 0)
						EditPrizesSecondNumber.Text = c.Winners2.ToString();
					if (c.Winners3 > 0)
						EditPrizesThirdNumber.Text = c.Winners3.ToString();
					try
					{
						EditPrizeContact.SelectedValue = c.OwnerUsrK.ToString();
					}
					catch { }
					EditPrizesValue.SelectedValue = c.PrizeValueRange.ToString();
					EditLinkNoneRadio.Checked = c.LinkType.Equals(Comp.LinkTypes.None);
					EditLinkEventRadio.Checked = c.LinkType.Equals(Comp.LinkTypes.Event);
					EditLinkBrandRadio.Checked = c.LinkType.Equals(Comp.LinkTypes.Brand);
					if (c.BrandK > 0)
					{
						try
						{
							EditLinkBrandDropDown.SelectedValue = c.BrandK.ToString();
						}
						catch { }
					}
					if (c.EventK > 0)
					{
						try
						{
							EditLinkEventDropDown.SelectedValue = c.EventK.ToString();
						}
						catch { }
					}
				}
				#endregion


			}
		}
		#endregion
		#region UpcomingEvents
		public EventSet UpcomingEvents
		{
			get
			{
				if (upcomingEvents == null)
				{
					if (Edit && CurrentComp.EventK > 0)
						upcomingEvents = CurrentPromoter.GetUpcomingEvents(CurrentComp.EventK, false);
					else
						upcomingEvents = CurrentPromoter.GetUpcomingEvents(false);
				}
				return upcomingEvents;
			}
			set
			{
				upcomingEvents = value;
			}
		}
		private EventSet upcomingEvents;
		#endregion
		#region Calendar_Change
		public void Calendar_Change(object o, System.EventArgs e)
		{
			ContainerPage.AnchorSkip("CompEditCalendars");
		}
		#endregion
		#region PanelEdit_Save
		public void PanelEdit_Save(object o, System.EventArgs e)
		{
			EnsureSecure();
			if (Page.IsValid)
			{
				Comp c = null;
				if (Mode.Equals(Modes.Add))
				{
					c = new Comp();
					c.DateTimeAdded = DateTime.Now;
					c.PromoterK = CurrentPromoter.K;
					c.Status = Comp.StatusEnum.New;
				}
				else if (Mode.Equals(Modes.Edit))
				{
					c = CurrentComp;
				}
				c.DisplayType = Comp.DisplayTypes.New;
				c.DateTimeStart = EditDateStart.SelectedDate;
				c.DateTimeClose = new DateTime(EditDateClose.SelectedDate.Year, EditDateClose.SelectedDate.Month, EditDateClose.SelectedDate.Day, 12, 0, 0);
				c.Question = Cambro.Web.Helpers.StripHtml(EditQuestion.Text.Trim()).Truncate(200);
				c.Answer1 = Cambro.Web.Helpers.StripHtml(EditAnswer1.Text.Trim()).Truncate(100);
				c.Answer2 = Cambro.Web.Helpers.StripHtml(EditAnswer2.Text.Trim()).Truncate(100);
				c.Answer3 = Cambro.Web.Helpers.StripHtml(EditAnswer3.Text.Trim()).Truncate(100);
				c.CorrectAnswer = EditCorrectRadio1.Checked ? 1 : (EditCorrectRadio2.Checked ? 2 : 3);
				c.Prize = Cambro.Web.Helpers.StripHtml(EditPrizesFirstDesc.Text.Trim()).Truncate(200);
				c.Prize2 = Cambro.Web.Helpers.StripHtml(EditPrizesSecondDesc.Text.Trim()).Truncate(200);
				c.Prize3 = Cambro.Web.Helpers.StripHtml(EditPrizesThirdDesc.Text.Trim()).Truncate(200);
				c.SponsorDetails = EditSponsorDescriptionHtml.GetHtml();
				try
				{
					c.Winners = int.Parse(EditPrizesFirstNumber.Text.Trim());
				}
				catch
				{
					throw new DsiUserFriendlyException("Number of first prizes must be a number");
				}
				try
				{
					c.Winners2 = EditPrizesSecondNumber.Text.Trim().Equals("") ? 0 : int.Parse(EditPrizesSecondNumber.Text.Trim());
				}
				catch
				{
					throw new DsiUserFriendlyException("Number of second prizes must be a number or blank");
				}
				try
				{
					c.Winners3 = EditPrizesThirdNumber.Text.Trim().Equals("") ? 0 : int.Parse(EditPrizesThirdNumber.Text.Trim());
				}
				catch
				{
					throw new DsiUserFriendlyException("Number of third prizes must be a number or blank");
				}

				Usr u;
				try
				{
					u = new Usr(int.Parse(EditPrizeContact.SelectedValue));
				}
				catch
				{
					throw new DsiUserFriendlyException("Prize contact not valid!");
				}
				if (!u.IsEnabledPromoter(CurrentPromoter.K))
					throw new DsiUserFriendlyException("Prize contact not valid!");
				c.OwnerUsrK = u.K;
				try
				{
					c.PrizeValueRange = int.Parse(EditPrizesValue.SelectedValue);
				}
				catch
				{
					throw new DsiUserFriendlyException("Prize value not valid!");
				}
				if (CurrentEvent != null)
				{
					c.LinkType = Comp.LinkTypes.Event;
					c.BrandK = 0;
					c.EventK = CurrentEvent.K;
				}
				else if (EditLinkNoneRadio.Checked)
				{
					c.LinkType = Comp.LinkTypes.None;
					c.BrandK = 0;
					c.EventK = 0;
				}
				else if (EditLinkEventRadio.Checked)
				{
					c.LinkType = Comp.LinkTypes.Event;
					c.BrandK = 0;
					if (UpcomingEvents.Count == 1)
						c.EventK = UpcomingEvents[0].K;
					else
					{
						Event ev;
						try
						{
							ev = new Event(int.Parse(EditLinkEventDropDown.SelectedValue));
						}
						catch
						{
							throw new DsiUserFriendlyException("Can't use this event!");
						}

						if (!ev.IsPromoter(CurrentPromoter.K))
							throw new DsiUserFriendlyException("Can't use this event!");
						c.EventK = ev.K;
					}
				}
				else if (EditLinkBrandRadio.Checked)
				{
					c.LinkType = Comp.LinkTypes.Brand;
					c.EventK = 0;
					c.BrandK = 0;
					if (CurrentPromoter.AllBrands.Count == 1)
						c.BrandK = CurrentPromoter.AllBrands[0].K;
					else
					{
						Brand b;
						try
						{
							b = new Brand(int.Parse(EditLinkBrandDropDown.SelectedValue));
						}
						catch
						{
							throw new DsiUserFriendlyException("Can't use this brand!");
						}
						if (b.PromoterK != CurrentPromoter.K)
							throw new DsiUserFriendlyException("Can't use this brand!");
						c.BrandK = b.K;
					}
				}
				c.Update();

				if (c.HasPic)
					RedirectDone();
				else
					Response.Redirect(ContainerPage.Url.CurrentUrl("mode", "pic", "compk", c.K.ToString()));

			}
		}
		#endregion
		#region CorrectVal
		public void CorrectVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = EditCorrectRadio1.Checked || EditCorrectRadio2.Checked || EditCorrectRadio3.Checked;
		}
		#endregion
		#region DateCloseVal
		public void DateCloseVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = EditDateClose.SelectedDate > DateTime.Now;
		}
		#endregion
		#region DateStartVal
		public void DateStartVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = EditDateClose.SelectedDate > EditDateStart.SelectedDate;
		}
		#endregion
		#region DateStartVal1
		public void DateStartVal1(object o, ServerValidateEventArgs e)
		{
			e.IsValid = !EditPrizesValue.SelectedItem.Value.Equals("1") ||
				EditDateStart.SelectedDate >= EditDateClose.SelectedDate.AddDays(-14);
			if (!e.IsValid)
				EditDateStart.SelectedDate = EditDateClose.SelectedDate.AddDays(-14);
		}
		#endregion
		#region DateStartVal2
		public void DateStartVal2(object o, ServerValidateEventArgs e)
		{
			e.IsValid = !EditPrizesValue.SelectedItem.Value.Equals("2") ||
				EditDateStart.SelectedDate >= EditDateClose.SelectedDate.AddDays(-28);
			if (!e.IsValid)
				EditDateStart.SelectedDate = EditDateClose.SelectedDate.AddDays(-28);
		}
		#endregion
		#region DateStartVal3
		public void DateStartVal3(object o, ServerValidateEventArgs e)
		{
			e.IsValid = !EditPrizesValue.SelectedItem.Value.Equals("3") ||
				EditDateStart.SelectedDate >= EditDateClose.SelectedDate.AddDays(-42);
			if (!e.IsValid)
				EditDateStart.SelectedDate = EditDateClose.SelectedDate.AddDays(-42);
		}
		#endregion
		#region LinkRadioVal
		public void LinkRadioVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = EditLinkNoneRadio.Checked || EditLinkEventRadio.Checked || EditLinkBrandRadio.Checked;
		}
		#endregion
		#region PanelEdit_Cancel
		public void PanelEdit_Cancel(object o, System.EventArgs e)
		{
			RedirectDone();
		}
		#endregion

		#region CurrentEvent
		public Event CurrentEvent
		{
			get
			{
				if (currentEvent == null && ContainerPage.Url["eventk"].IsInt)
					currentEvent = new Event(ContainerPage.Url["eventk"]);
				return currentEvent;
			}
			set
			{
				currentEvent = value;
			}
		}
		private Event currentEvent;
		#endregion

		#endregion

		#region PanelPic
		protected Panel PanelPic;
		protected Panel PicUploadPanel, PanelPicSavedPanel;
		protected Controls.Pic PicUc;
		protected Panel PicUploadDefaultPanel;
		protected DataList PicUploadDefaultDataList;
		void PanelPic_Load(object o, EventArgs e)
		{
			EnsureSecure();
			if (Mode.Equals(Modes.Pic))
			{
				PicUc.InputObject = CurrentComp;
				PanelPicBindDefaultPics();

				if (!Page.IsPostBack)
					PanelPicBind();

			}
		}
		public void PanelPicBind()
		{
			PicUc.InitPic();
			PicUploadPanel.Visible = true;
		}
		void PanelPicBindDefaultPics()
		{
			ArrayList obs = new ArrayList();
			if (CurrentComp.EventK > 0)
			{
				if (CurrentComp.Event.HasPic)
					obs.Add(CurrentComp.Event.Pic);
				foreach (Brand b in CurrentComp.Event.Brands)
				{
					if (b.HasPic)
						obs.Add(b.Pic);
				}
				if (CurrentComp.Event.Venue.HasPic)
					obs.Add(CurrentComp.Event.Venue.Pic);
			}
			if (CurrentComp.BrandK > 0 && CurrentComp.Brand.HasPic)
				obs.Add(CurrentComp.Brand.Pic);
			if (CurrentComp.PromoterK > 0 && CurrentComp.Promoter.HasPic)
				obs.Add(CurrentComp.Promoter.Pic);

			if (obs.Count > 0)
			{
				PicUploadDefaultDataList.DataSource = obs;
				PicUploadDefaultDataList.DataBind();
				PicUploadDefaultPanel.Visible = true;
			}
			else
				PicUploadDefaultPanel.Visible = false;
		}
		protected void PanelPicNoPic(object o, EventArgs e)
		{
			PanelPicNext();
		}
		protected void PanelPicSaved(object o, EventArgs e)
		{
			PanelPicNext();
		}
		void PanelPicNext()
		{
			RedirectDone();
		}
		public void PicUploadDefaultSelect(object o, DataListCommandEventArgs e)
		{
			Guid g = new Guid(e.CommandArgument.ToString());

			bool hasOldPic = CurrentComp.HasPic;
			Guid oldPic = CurrentComp.HasPic ? CurrentComp.Pic : Guid.Empty;

			CurrentComp.Pic = Guid.NewGuid();
			
			Storage.AddToStore(
				Storage.GetFromStore(Storage.Stores.Pix, g, "jpg"),
				Storage.Stores.Pix,
				CurrentComp.Pic,
				"jpg",
				CurrentComp,
				"Pic");

			CurrentComp.Update();
			
			if (hasOldPic)
				Storage.RemoveFromStore(Storage.Stores.Pix, oldPic, "jpg");

			PanelPicNext();
		}
		#endregion

		#region Utility_Load
		public void Utility_Load(object o, System.EventArgs e)
		{
			if (Mode.Equals(Modes.Publish) || Mode.Equals(Modes.Delete))
			{
				EnsureSecure();
				if (CurrentComp == null)
					throw new DsiUserFriendlyException("No competition selected!");

				else if (Mode.Equals(Modes.Publish))
				{
					if (!CurrentComp.Status.Equals(Comp.StatusEnum.New))
						throw new DsiUserFriendlyException("Competition can't be published now.");
					if (!CurrentComp.HasAnyPic)
						throw new DsiUserFriendlyException("Competition can't be published now - you must add a picture. Click Back and add a picture.");

					if (CurrentComp.Status.Equals(Comp.StatusEnum.New) && CurrentComp.HasAnyPic)
					{
						CurrentComp.Status = Comp.StatusEnum.Published;
						CurrentComp.Update();

						Mailer admin = new Mailer();
						admin.TemplateType = Mailer.TemplateTypes.AdminNote;
						admin.Subject = "New competition published by promoter";
						admin.Body = "<p>New competition published by promoter</p>";
						admin.Body += "<p><a href=\"[LOGIN(" + CurrentComp.Url() + ")]\">Competition page</a></p>";
						admin.Body += "<p><a href=\"[LOGIN(" + CurrentComp.Promoter.UrlApp("competitions") + ")]\">Promoter competitions list</a></p>";
						admin.Body += "<p><a href=\"http://old.dontstayin.com/login-" + Usr.Current.K + "- " + Usr.Current.LoginString + "/admin/comp?ID=" + CurrentComp.K + "\">Edit (admin)</a></p>";
						admin.Body += "<p><a href=\"[LOGIN(" + CurrentComp.Promoter.UrlApp("competitions", "mode", "edit", "compk", CurrentComp.K.ToString()) + ")]\">Edit (promoter)</a></p>";
						admin.To = "competitions@dontstayin.com";
						admin.Send();
					}
				}
				else if (Mode.Equals(Modes.Delete))
				{
					if (!CurrentComp.Status.Equals(Comp.StatusEnum.New))
						throw new DsiUserFriendlyException("Competition can't be deleted now - call us if you need it deleted.");

					Delete.DeleteAll(CurrentComp);
				}
				RedirectDone();
			}
		}
		#endregion

		#region PanelList
		protected Panel PanelList;
		protected DataGrid CompDataGrid;
		public void PanelList_Load(object o, System.EventArgs e)
		{
			if (Mode.Equals(Modes.List))
			{
				BindComp();
			}
		}
		#region BindComp()
		void BindComp()
		{
			Query q = new Query();
			q.QueryCondition = new Q(Comp.Columns.PromoterK, CurrentPromoter.K);
			q.NoLock = true;
			q.OrderBy = new OrderBy(Comp.Columns.DateTimeClose, OrderBy.OrderDirection.Descending);
			CompSet cs = new CompSet(q);
			CompDataGrid.AllowPaging = (cs.Count > CompDataGrid.PageSize);
			CompDataGrid.DataSource = cs;
			CompDataGrid.DataBind();
		}
		#endregion
		#region CompDataGridChangePage
		public void CompDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			CompDataGrid.CurrentPageIndex = e.NewPageIndex;
			BindComp();
		}
		#endregion
		#endregion

		#region Mode
		Modes Mode
		{
			get
			{
				if (ContainerPage.Url["Mode"].Equals("List") || ContainerPage.Url["Mode"].IsNull)
					return Modes.List;
				if (ContainerPage.Url["Mode"].Equals("Delete"))
					return Modes.Delete;
				if (ContainerPage.Url["Mode"].Equals("Add"))
					return Modes.Add;
				if (ContainerPage.Url["Mode"].Equals("Edit"))
					return Modes.Edit;
				if (ContainerPage.Url["Mode"].Equals("Pic"))
					return Modes.Pic;
				if (ContainerPage.Url["Mode"].Equals("Publish"))
					return Modes.Publish;
				if (ContainerPage.Url["Mode"].Equals("Winners"))
					return Modes.Winners;
				else
					return Modes.None;
			}
		}
		public enum Modes
		{
			None,
			List,
			Delete,
			Publish,
			Add,
			Edit,
			Pic,
			Winners
		}
		#endregion


		bool Add
		{
			get
			{
				return Mode.Equals(Modes.Add);
			}
		}
		bool Edit
		{
			get
			{
				return Mode.Equals(Modes.Edit);
			}
		}

		#region CurrentComp
		public Comp CurrentComp
		{
			get
			{
				if (currentComp == null && ContainerPage.Url["CompK"].IsInt)
					currentComp = new Comp(ContainerPage.Url["CompK"]);
				return currentComp;
			}
			set
			{
				currentComp = value;
			}
		}
		Comp currentComp;
		#endregion

		void ChangePanel(Panel p)
		{
			PanelList.Visible = p.Equals(PanelList);
			PanelEdit.Visible = p.Equals(PanelEdit);
			PanelPic.Visible = p.Equals(PanelPic);
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
			this.Load += new System.EventHandler(this.PanelEdit_Load);
			this.Load += new System.EventHandler(this.PanelPic_Load);
		}
		#endregion
	}
}
