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
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO;
using Spotted.Controls;

namespace Spotted.Pages
{
	public partial class MyArticles : DsiUserControl
	{
		protected Panel CantEditPanel;
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("You must log in to view your articles");

			ContainerPage.SetPageTitle("My articles");

			if ((CurrentArticle != null && !Usr.Current.CanEdit(CurrentArticle)) || (CurrentPara != null && !Usr.Current.CanEdit(CurrentPara.Article)))
				ChangePanel(CantEditPanel);
			this.CurrentArticles_Load(sender, e);
			this.AddArticlePanel_Load(sender, e);
			this.EditArticlePanel_Load(sender, e);
			this.EditArticleParaPanel_Load(sender, e);
			this.EditArticleParaPhotoPanel_Load(sender, e);
		}

		#region CurrentArticlesPanel
		public void CurrentArticles_Load(object o, System.EventArgs e)
		{
			if (ContainerPage.Url["Mode"].Equals("List"))
			{
				ChangePanel(CurrentArticlesPanel);
				ArticlesDataGrid_Bind();
			}
		}
		public void ArticlesDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			ArticlesDataGrid.CurrentPageIndex = e.NewPageIndex;
			ArticlesDataGrid_Bind();
		}
		void ArticlesDataGrid_Bind()
		{
			Query q = new Query();
			if (Usr.Current.IsAdmin)
				q.QueryCondition = new Or(new Q(Article.Columns.OwnerUsrK, Usr.Current.K), new Q(Article.Columns.Status, Article.StatusEnum.Edit), new Q(Article.Columns.Status, Article.StatusEnum.Enabled));
			else if (Usr.Current.IsSuper)
				q.QueryCondition = new Or(new Q(Article.Columns.OwnerUsrK, Usr.Current.K), new Q(Article.Columns.Status, Article.StatusEnum.Edit));
			else
				q.QueryCondition = new Q(Article.Columns.OwnerUsrK, Usr.Current.K);

			q.OrderBy = new OrderBy(Article.Columns.AddedDateTime, OrderBy.OrderDirection.Descending);

			ArticleSet ars = new ArticleSet(q);

			if (ars.Count == 0)
			{
				NoArticlesDataGridPanel.Visible = true;
				ArticlesDataGridPanel.Visible = false;

			}
			else
			{
				ArticlesDataGrid.Columns[3].Visible = Usr.Current.IsSuper;
				NoArticlesDataGridPanel.Visible = false;
				ArticlesDataGridPanel.Visible = true;
				ArticlesDataGrid.AllowPaging = (ars.Count > ArticlesDataGrid.PageSize);
				ArticlesDataGrid.DataSource = ars;
				ArticlesDataGrid.DataBind();
			}
		}
		#endregion

		#region AddArticlePanel
		#region AddArticlePanel_Load
		public void AddArticlePanel_Load(object o, System.EventArgs e)
		{
			if (ContainerPage.Url["Mode"].Equals("Add"))
			{
				ChangePanel(AddArticlePanel);
				if (CurrentEvent != null)
					AddArticleSubjectMatterPanel.Visible = false;
				this.DataBind();

			}
		}
		#endregion
		#region DbComboArticleObject
		[Cambro.Web.DbCombo.ResultsMethod(true)]
		public static object DbComboArticleObject(
			Cambro.Web.DbCombo.ServerMethodArgs args)
		{
			int type = 0;
			bool future = false;
			bool attend = false;
			if (args.ClientState != null)
			{
				if (args.ClientState["Type"] != null)
					type = int.Parse(args.ClientState["Type"].ToString());

				if (args.ClientState["Future"] != null)
					future = true;

				if (args.ClientState["Attend"] != null)
					attend = true;
			}
			if (type == 1) //Event
			{
				string join = "";
				if (attend)
					join = " INNER JOIN UsrEventAttended ON (Event.K = UsrEventAttended.EventK AND UsrEventAttended.UsrK = " + Usr.Current.K + ") ";

				string date = "";
				if (future)
					date = " AND Event.DateTime>=GETDATE() ";

				DataView dv;
				SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
				try
				{
					SqlCommand myCommand = new SqlCommand("select top " + Cambro.Misc.Db.PNum(args.Top) + " Event.Name+' @ '+Venue.Name+' in '+Place.Name+' ('+Country.FriendlyName+'), '+CONVERT(VarChar,Event.DateTime,3) as DbComboText, Event.K as DbComboValue from Event INNER JOIN Venue ON Event.VenueK = Venue.K INNER JOIN Place ON Venue.PlaceK=Place.K INNER JOIN Country ON Place.CountryK = Country.K " + join + " WHERE Event.Name+' @ '+Venue.Name+' in '+Place.Name+' ('+Country.FriendlyName+'), '+CONVERT(VarChar,Event.DateTime,3) like '%" + Cambro.Misc.Db.PStr(args.Query) + "%' " + date + " order by Event.Name, Venue.Name, Place.Name, Event.DateTime DESC", conn);
					DataSet dataset = new DataSet();
					SqlDataAdapter adapter = new SqlDataAdapter();
					adapter.SelectCommand = myCommand;
					Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
					adapter.Fill(dataset);
					dv = dataset.Tables[0].DefaultView;
				}
				finally
				{
					conn.Close();
					conn.Dispose();
				}
				return dv;
			}
			else if (type == 2) //Venue
			{
				DataView dv;
				SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
				try
				{
					SqlCommand myCommand = new SqlCommand("select top " + Cambro.Misc.Db.PNum(args.Top) + " Venue.Name+' in '+Place.Name+' ('+Country.FriendlyName+')' as DbComboText, Venue.K as DbComboValue from Venue INNER JOIN Place ON Venue.PlaceK=Place.K INNER JOIN Country ON Place.CountryK = Country.K WHERE Venue.Name+' in '+Place.Name+' ('+Country.FriendlyName+')' like '%" + Cambro.Misc.Db.PStr(args.Query) + "%' order by Venue.Name, Place.Name", conn);
					DataSet dataset = new DataSet();
					SqlDataAdapter adapter = new SqlDataAdapter();
					adapter.SelectCommand = myCommand;
					Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
					adapter.Fill(dataset);
					dv = dataset.Tables[0].DefaultView;
				}
				finally
				{
					conn.Close();
					conn.Dispose();
				}
				return dv;
			}
			else if (type == 3) //Place
			{
				DataView dv;
				SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
				try
				{
					SqlCommand myCommand = new SqlCommand("select top " + Cambro.Misc.Db.PNum(args.Top) + " Place.Name + ' (' + CASE WHEN Region.K IS NULL THEN '' ELSE Region.Name + ', ' END + Country.FriendlyName+')' as DbComboText, Place.K as DbComboValue from Place LEFT JOIN Region ON Place.RegionK = Region.K INNER JOIN Country ON Place.CountryK = Country.K WHERE Place.Enabled=1 AND Place.Name + ' (' + CASE WHEN Region.K IS NULL THEN '' ELSE Region.Name + ', ' END + Country.FriendlyName+')' like '%" + Cambro.Misc.Db.PStr(args.Query) + "%' order by Place.Name, Region.Name, Country.FriendlyName", conn);
					DataSet dataset = new DataSet();
					SqlDataAdapter adapter = new SqlDataAdapter();
					adapter.SelectCommand = myCommand;
					Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
					adapter.Fill(dataset);
					dv = dataset.Tables[0].DefaultView;
				}
				finally
				{
					conn.Close();
					conn.Dispose();
				}
				return dv;
			}
			else if (type == 4) //Country
			{
				DataView dv;
				SqlConnection conn = new SqlConnection(Vars.DefaultConnectionString);
				try
				{
					SqlCommand myCommand = new SqlCommand("select top " + Cambro.Misc.Db.PNum(args.Top) + " Country.FriendlyName as DbComboText, Country.K as DbComboValue from Country WHERE Country.FriendlyName LIKE '%" + Cambro.Misc.Db.PStr(args.Query) + "%' ORDER BY Country.FriendlyName", conn);
					DataSet dataset = new DataSet();
					SqlDataAdapter adapter = new SqlDataAdapter();
					adapter.SelectCommand = myCommand;
					Bobs.Global.LogSqlQuery(Bobs.Global.QueryTypes.Select);
					adapter.Fill(dataset);
					dv = dataset.Tables[0].DefaultView;
				}
				finally
				{
					conn.Close();
					conn.Dispose();
				}
				return dv;
			}
			else if (type == 5) //General
			{
				return new Cambro.Web.DbCombo.SimpleResult(
					"DbComboText", new string[] { "No need to select anything in here, you've selected 'General' above." },
					"DbComboValue", new string[] { "-1" }
					);
			}
			else
			{
				return new Cambro.Web.DbCombo.SimpleResult(
					"DbComboText", new string[] { "Please select an article subject matter type by clicking an option above" },
					"DbComboValue", new string[] { "" }
					);

			}
		}
		#endregion
		
		#region AddButtonDone
		public void AddButtonDone(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				//Add article and paragraphs
				Article art = new Article();
				art.AddedDateTime = DateTime.Now;
				art.Title = Cambro.Web.Helpers.StripHtml(AddArticleTitleTextBox.Text.Trim());
				art.Summary = Cambro.Web.Helpers.StripHtml(AddArticleSummaryTextBox.Text.Trim());
				string[] paraAry = AddArticleBodyHtml.GetParaHtml();
				art.Update();
				int order = 1;
				foreach (string s in paraAry)
				{
					if (s.Length > 0)
					{
						Para p = new Para();

						p.ArticleK = art.K;
						p.Page = 1;
						p.Order = order;
						order++;
						p.Type = Para.TypeEnum.Para;
						p.PhotoK = 0;
						p.Text = s;
						p.ThreadK = 0;
						p.Update();
					}
				}
				art.OwnerUsrK = Usr.Current.K;
				art.Status = Article.StatusEnum.New;
				if (CurrentEvent != null)
				{
					art.ParentObjectType = Model.Entities.ObjectType.Event;
					art.Relevance = Model.Entities.Article.RelevanceEnum.Venue;
					art.ParentObjectK = CurrentEvent.K;
				}
				else if (AddArticleScopeEvent.Checked)
				{
					art.ParentObjectType = Model.Entities.ObjectType.Event;
					art.Relevance = Model.Entities.Article.RelevanceEnum.Venue;
					art.ParentObjectK = this.AddArticleScopeMultiPicker.Event.K;
				}
				else if (AddArticleScopeVenue.Checked)
				{
					art.ParentObjectType = Model.Entities.ObjectType.Venue;
					art.Relevance = Model.Entities.Article.RelevanceEnum.Venue;
					art.ParentObjectK = this.AddArticleScopeMultiPicker.Venue.K;
				}
				else if (AddArticleScopePlace.Checked)
				{
					art.ParentObjectType = Model.Entities.ObjectType.Place;
					art.Relevance = Model.Entities.Article.RelevanceEnum.Place;
					art.ParentObjectK = this.AddArticleScopeMultiPicker.Place.K;
				}
				else if (AddArticleScopeCountry.Checked)
				{
					art.ParentObjectType = Model.Entities.ObjectType.Country;
					art.Relevance = Model.Entities.Article.RelevanceEnum.Country;
					art.ParentObjectK = this.AddArticleScopeMultiPicker.Country.K;
				}
				else
				{
					art.ParentObjectType = Model.Entities.ObjectType.None;
					art.Relevance = Model.Entities.Article.RelevanceEnum.Worldwide;
				}
				art.HasSingleThread = true;
				art.AdminNote = "Article added by owner " + DateTime.Now.ToString();
				art.Update();
				art.UpdateAncestorLinks();

				Response.Redirect(UrlInfo.PageUrl("myarticles", "mode", "edit", "k", art.K.ToString()));
			}
		}
		#endregion
		#region AddButtonCancel
		public void AddButtonCancel(object o, System.EventArgs e)
		{
			//Add article and paragraphs
			Response.Redirect(UrlInfo.PageUrl("myarticles", "mode", "list"));
		}
		#endregion
		#region AddArticleTitleCapsVal
		public void AddArticleTitleCapsVal(object o, ServerValidateEventArgs e)
		{
			CapsVal(AddArticleTitleTextBox, e);
		}
		#endregion
		#region AddArticleSummaryCapsVal
		public void AddArticleSummaryCapsVal(object o, ServerValidateEventArgs e)
		{
			CapsVal(AddArticleSummaryTextBox, e);
		}
		#endregion
		#region AddArticleTitlePunctuationVal
		public void AddArticleTitlePunctuationVal(object o, ServerValidateEventArgs e)
		{
			PunctuationVal(AddArticleTitleTextBox, e);
		}
		#endregion
		#region AddArticleSummaryPunctuationVal
		public void AddArticleSummaryPunctuationVal(object o, ServerValidateEventArgs e)
		{
			PunctuationVal(AddArticleSummaryTextBox, e);
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

		protected Picker AddArticleScopeMultiPicker;
		protected void AddArticleScopeCheckChange(object o, EventArgs e)
		{
			AddArticleScopeMultiFinderPanel.Visible = AddArticleScopeEvent.Checked || AddArticleScopeVenue.Checked || AddArticleScopePlace.Checked || AddArticleScopeCountry.Checked;

			AddArticleScopeMultiPicker.OptionEvent = AddArticleScopeEvent.Checked;
			AddArticleScopeMultiPicker.OptionDate = AddArticleScopeEvent.Checked;
			AddArticleScopeMultiPicker.OptionBrand = AddArticleScopeEvent.Checked;
			AddArticleScopeMultiPicker.OptionVenue = AddArticleScopeEvent.Checked || AddArticleScopeVenue.Checked;
			AddArticleScopeMultiPicker.OptionPlace = AddArticleScopeEvent.Checked || AddArticleScopeVenue.Checked || AddArticleScopePlace.Checked;
			AddArticleScopeMultiPicker.OptionCountry = AddArticleScopeEvent.Checked || AddArticleScopeVenue.Checked || AddArticleScopePlace.Checked || AddArticleScopeCountry.Checked;

			AddArticleScopeMultiPicker.ValidationType =
				AddArticleScopeEvent.Checked ? "event" :
				AddArticleScopeVenue.Checked ? "venue" :
				AddArticleScopePlace.Checked ? "place" :
				AddArticleScopeCountry.Checked ? "country" : "";

			ContainerPage.AnchorSkip("AddArticleScope");

		}
 		#endregion

		public void CapsVal(TextBox tb, ServerValidateEventArgs e)
		{
			string textOnly = Cambro.Web.Helpers.StripHtmlDoubleSpacesLineFeeds(tb.Text);

			int lower = new Regex("[^a-z]").Replace(textOnly, String.Empty).Length;
			int upper = new Regex("[^A-Z]").Replace(textOnly, String.Empty).Length;
			int total = lower + upper;

			if ((total > 20 && upper > 0.4 * total) ||
				(total > 10 && upper > 0.5 * total))
			{
				e.IsValid = false;
				tb.Text = tb.Text.ToLower();
			}
			else
				e.IsValid = true;

		}
		public void PunctuationVal(TextBox tb, ServerValidateEventArgs e)
		{
			string textOnly = Cambro.Web.Helpers.StripHtmlDoubleSpacesLineFeeds(tb.Text);

			string withoutSpaces = new Regex("[ ]").Replace(textOnly, String.Empty);

			int chars = new Regex("[^a-zA-Z]").Replace(withoutSpaces, String.Empty).Length;
			int punc = new Regex("[a-zA-Z]").Replace(withoutSpaces, String.Empty).Length;
			int total = chars + punc;

			if ((total > 20 && punc > 0.2 * total) ||
				(total > 10 && punc > 0.3 * total))
				e.IsValid = false;
			else
				e.IsValid = true;

		}


		#region EditArticlePanel


		public void EditArticleIndexPublishClick(object o, System.EventArgs e)
		{
			if (ContainerPage.Url["Mode"].Equals("Edit"))
			{
				if ((CurrentArticle != null && !Usr.Current.CanEdit(CurrentArticle)) || (CurrentPara != null && !Usr.Current.CanEdit(CurrentPara.Article)))
				{
					ChangePanel(CantEditPanel);
					return;
				}
				if (!CurrentArticle.Pic.Equals(Guid.Empty) && CurrentArticle.Status.Equals(Article.StatusEnum.New))
				{
					if (Usr.Current.IsAdmin)
					{
						CurrentArticle.EnableArticle(Usr.Current, false, true);
					}
					else if (CurrentArticle.ParentObjectType == Model.Entities.ObjectType.Event || CurrentArticle.ParentObjectType == Model.Entities.ObjectType.Venue)
					{
						CurrentArticle.EnableArticle(Usr.Current, false, true);
					}
					else
					{
						CurrentArticle.Status = Article.StatusEnum.Edit;
					}
					CurrentArticle.Update();

					Mailer sm = new Mailer();
					sm.Subject = "Article published";
					sm.Body += "<p>New article published:</p>";
					sm.Body += "<p><a href=\"[LOGIN(" + CurrentArticle.Url() + ")]\">" + CurrentArticle.Name + "</a></p>";
					sm.Body += "<p><a href=\"[LOGIN(" + UrlInfo.PageUrl("myarticles", "mode", "edit", "k", CurrentArticle.K.ToString()) + ")]\">Edit (my articles)</a></p>";
					sm.Body += "<p><a href=\"http://old.dontstayin.com/login-" + Usr.Current.K + "- " + Usr.Current.LoginString + "/admin/article?ID=" + CurrentArticle.K.ToString() + "\">Edit (admin)</a></p>";
					sm.TemplateType = Mailer.TemplateTypes.AdminNote;
					sm.To = "articles@dontstayin.com";
					sm.Send();

					Response.Redirect("/pages/myarticles/mode-list");
				}
			}
		}

		public void EditArticlePanel_Load(object o, System.EventArgs e)
		{
			if (ContainerPage.Url["Mode"].Equals("Edit"))
			{
				if ((CurrentArticle != null && !Usr.Current.CanEdit(CurrentArticle)) || (CurrentPara != null && !Usr.Current.CanEdit(CurrentPara.Article)))
				{
					ChangePanel(CantEditPanel);
					return;
				}

				EditArticleAdminTab.Visible = Usr.Current.IsAdmin;

				EditArticleAdminPanel.Visible = Usr.Current.IsAdmin;
				if (Usr.Current.IsAdmin)
				{
					EditArticleStatusDisplay.Visible = CurrentArticle.Status == Model.Entities.Article.StatusEnum.Enabled;
					if (CurrentArticle.Status == Model.Entities.Article.StatusEnum.Enabled)
					{
						EditArticleStatusDisplay.InnerHtml = "Article enabled " + CurrentArticle.EnabledDateTime.ToString() + " by " + CurrentArticle.EnabledUsr.Link();
					}
				}

				EditArticleContentTab.HRef = CurrentArticle.UrlEdit("tab", "content");
				EditArticleTitleTab.HRef = CurrentArticle.UrlEdit("tab", "title");
				EditArticleLinkTab.HRef = CurrentArticle.UrlEdit("tab", "link");
				EditArticleIconTab.HRef = CurrentArticle.UrlEdit("tab", "icon");
				EditArticlePhotosTab.HRef = CurrentArticle.UrlEdit("tab", "photos");
				EditArticleAdminTab.HRef = CurrentArticle.UrlEdit("tab", "admin");

				if (ContainerPage.Url["Tab"].Equals("Content") || ContainerPage.Url["Tab"].IsNull)
				{
					BindBodyPageRepeater();
					ChangePanel(EditArticleBodyPanel);
					EditArticleContentTab.Attributes["class"] = "TabbedHeading Selected";
				}
				else if (ContainerPage.Url["Tab"].Equals("Title"))
				{
					ChangePanel(EditArticleTitleSummaryPanel);
					EditArticleTitleTab.Attributes["class"] = "TabbedHeading Selected";
				}
				else if (ContainerPage.Url["Tab"].Equals("Link"))
				{
					ChangePanel(EditArticleSubjectMatterPanel);
					EditArticleLinkTab.Attributes["class"] = "TabbedHeading Selected";
					
				}
				else if (ContainerPage.Url["Tab"].Equals("Icon"))
				{
					EditArticlePicture.InputObject = CurrentArticle;
					if (!Page.IsPostBack)
						EditArticlePicture.InitPic();
					ChangePanel(EditArticlePicturePanel);
					EditArticleIconTab.Attributes["class"] = "TabbedHeading Selected";
				}
				else if (ContainerPage.Url["Tab"].Equals("Photos") || ContainerPage.Url["Photos"] == 1)
				{
					#region Photo upload panel
					EditArticlePhotoUploadLink.HRef = "/pages/galleries/add/articlek-" + CurrentArticle.K.ToString();
					EditArticlePhotoEditLink.HRef = "/pages/galleries/add/articlek-" + CurrentArticle.K.ToString();
					PhotoSet ps = new PhotoSet(new Query(new Q(Photo.Columns.ArticleK, CurrentArticle.K), Photo.DateTimeOrder(OrderBy.OrderDirection.Ascending)));
					if (ps.Count == 0)
						EditArticlePhotoNoPhotosPanel.Visible = false;
					else
					{
						EditArticlePhotoNoPhotosPanel.Visible = true;
						EditArticlePhotoGalleryDataList.DataSource = ps;
						EditArticlePhotoGalleryDataList.ItemTemplate = this.LoadTemplate("/Templates/Photos/MyArticlesUpload.ascx");
						EditArticlePhotoGalleryDataList.DataBind();
					}
					#endregion
					ChangePanel(EditArticlePhotoUploadPanel);
					EditArticlePhotosTab.Attributes["class"] = "TabbedHeading Selected";
				}
				else if (ContainerPage.Url["Tab"].Equals("Admin") && Usr.Current.IsAdmin)
				{
					ChangePanel(EditArticleAdminPanel);
					EditArticleAdminTab.Attributes["class"] = "TabbedHeading Selected";
				}


				EditArticleIndexPublishPanel.Visible = CurrentArticle.Status.Equals(Article.StatusEnum.New) && !CurrentArticle.Pic.Equals(Guid.Empty);
				EditArticleIndexToDoPanel.Visible = CurrentArticle.Status.Equals(Article.StatusEnum.New) && CurrentArticle.Pic.Equals(Guid.Empty);
				EditArticleIndexPanelPreviewAnchor.HRef = CurrentArticle.Url();
				EditArticleIndexPanelArticleNameLabel.Text = CurrentArticle.Name;
				

				if (!Page.IsPostBack)
				{
					EditArticleTitleTextBox.Text = CurrentArticle.Title;
					EditArticleSummaryTextBox.Text = CurrentArticle.Summary;
					if (CurrentArticle.ParentObjectType.Equals(Model.Entities.ObjectType.Event))
					{
						EditArticleScopeEvent.Checked = true;
						updateEditArticleScope();
						EditArticleScopeMultiPicker.Event = CurrentArticle.ParentEvent;
					}
					else if (CurrentArticle.ParentObjectType.Equals(Model.Entities.ObjectType.Venue))
					{
						EditArticleScopeVenue.Checked = true;
						updateEditArticleScope();
						EditArticleScopeMultiPicker.Venue = CurrentArticle.ParentVenue;
					}
					else if (CurrentArticle.ParentObjectType.Equals(Model.Entities.ObjectType.Place))
					{
						EditArticleScopePlace.Checked = true;
						updateEditArticleScope();
						EditArticleScopeMultiPicker.Place = CurrentArticle.ParentPlace;
					}
					else if (CurrentArticle.ParentObjectType.Equals(Model.Entities.ObjectType.Country))
					{
						EditArticleScopeCountry.Checked = true;
						updateEditArticleScope();
						EditArticleScopeMultiPicker.Country = CurrentArticle.ParentCountry;
					}
					else
					{
						EditArticleScopeGeneral.Checked = true;
						updateEditArticleScope();
					}
					EditArticleRelevanceEvent.Checked = 
						CurrentArticle.ParentObjectType.Equals(Model.Entities.ObjectType.Event)
						|| CurrentArticle.ParentObjectType.Equals(Model.Entities.ObjectType.Venue)
						|| CurrentArticle.ParentObjectType.Equals(Model.Entities.ObjectType.Place)
						|| CurrentArticle.ParentObjectType.Equals(Model.Entities.ObjectType.Country)
						|| CurrentArticle.ParentObjectType.Equals(Model.Entities.ObjectType.None);
					EditArticleRelevanceVenue.Checked = 
						CurrentArticle.ParentObjectType.Equals(Model.Entities.ObjectType.Venue)
						|| CurrentArticle.ParentObjectType.Equals(Model.Entities.ObjectType.Place)
						|| CurrentArticle.ParentObjectType.Equals(Model.Entities.ObjectType.Country)
						|| CurrentArticle.ParentObjectType.Equals(Model.Entities.ObjectType.None);
					EditArticleRelevancePlace.Checked = 
						CurrentArticle.ParentObjectType.Equals(Model.Entities.ObjectType.Place)
						|| CurrentArticle.ParentObjectType.Equals(Model.Entities.ObjectType.Country)
						|| CurrentArticle.ParentObjectType.Equals(Model.Entities.ObjectType.None);
					EditArticleRelevanceCountry.Checked = 
						CurrentArticle.ParentObjectType.Equals(Model.Entities.ObjectType.Country)
						|| CurrentArticle.ParentObjectType.Equals(Model.Entities.ObjectType.None);
					EditArticleRelevanceCountry.Checked =
						CurrentArticle.ParentObjectType.Equals(Model.Entities.ObjectType.None);


					if (Usr.Current.IsAdmin)
					{
						EditArticleAdminStatusDrop.SelectedValue = ((int)CurrentArticle.Status).ToString();
						EditArticleRelevanceGeneral.Checked = CurrentArticle.Relevance == Model.Entities.Article.RelevanceEnum.Worldwide;
						EditArticleRelevanceCountry.Checked = CurrentArticle.Relevance == Model.Entities.Article.RelevanceEnum.Worldwide || CurrentArticle.Relevance == Model.Entities.Article.RelevanceEnum.Country;
						EditArticleRelevancePlace.Checked = CurrentArticle.Relevance == Model.Entities.Article.RelevanceEnum.Worldwide || CurrentArticle.Relevance == Model.Entities.Article.RelevanceEnum.Country || CurrentArticle.Relevance == Model.Entities.Article.RelevanceEnum.Place;
						EditArticleRelevanceVenue.Checked = CurrentArticle.Relevance == Model.Entities.Article.RelevanceEnum.Worldwide || CurrentArticle.Relevance == Model.Entities.Article.RelevanceEnum.Country || CurrentArticle.Relevance == Model.Entities.Article.RelevanceEnum.Place || CurrentArticle.Relevance == Model.Entities.Article.RelevanceEnum.Venue;
						EditArticleRelevanceEvent.Checked = CurrentArticle.Relevance == Model.Entities.Article.RelevanceEnum.Worldwide || CurrentArticle.Relevance == Model.Entities.Article.RelevanceEnum.Country || CurrentArticle.Relevance == Model.Entities.Article.RelevanceEnum.Place || CurrentArticle.Relevance == Model.Entities.Article.RelevanceEnum.Venue || CurrentArticle.Relevance == Model.Entities.Article.RelevanceEnum.Event;

						EditArticleRelevanceCountry.Visible = CurrentArticle.ParentObjectType == Model.Entities.ObjectType.Country || CurrentArticle.ParentObjectType == Model.Entities.ObjectType.Place || CurrentArticle.ParentObjectType == Model.Entities.ObjectType.Venue || CurrentArticle.ParentObjectType == Model.Entities.ObjectType.Event;
						EditArticleRelevancePlace.Visible =  CurrentArticle.ParentObjectType == Model.Entities.ObjectType.Place || CurrentArticle.ParentObjectType == Model.Entities.ObjectType.Venue || CurrentArticle.ParentObjectType == Model.Entities.ObjectType.Event;
						EditArticleRelevanceVenue.Visible = CurrentArticle.ParentObjectType == Model.Entities.ObjectType.Venue || CurrentArticle.ParentObjectType == Model.Entities.ObjectType.Event;
						EditArticleRelevanceEvent.Visible = CurrentArticle.ParentObjectType == Model.Entities.ObjectType.Event;

						EditArticleRelevanceGeneral.Enabled = CurrentArticle.ParentObjectType != Model.Entities.ObjectType.None;
						EditArticleRelevanceCountry.Enabled = CurrentArticle.ParentObjectType != Model.Entities.ObjectType.Country;
						EditArticleRelevancePlace.Enabled = CurrentArticle.ParentObjectType != Model.Entities.ObjectType.Place;
						EditArticleRelevanceVenue.Enabled = CurrentArticle.ParentObjectType != Model.Entities.ObjectType.Venue;
						EditArticleRelevanceEvent.Enabled = CurrentArticle.ParentObjectType != Model.Entities.ObjectType.Event;

						EditArticleMixmag.Checked = CurrentArticle.IsMixmagNews;
						EditArticleExtended.Checked = CurrentArticle.IsExtendedDisplay;

						for ( int i = 0; i < MixmagSectionsListBox.Rows; i++ )
						{
							MixmagSectionsListBox.Items[i].Selected = CurrentArticle.MixmagIsSection(i);
						}

						EditArticleRelevanceFrontPageAboveFold.Checked = CurrentArticle.ShowAboveFoldOnFrontPage;
					}

					

					this.DataBind();
					if (ContainerPage.Url["ParaK"].IsInt)
					{
						Para p = new Para(ContainerPage.Url["ParaK"]);
						ChangePanel(EditArticleBodyPanel);
						ContainerPage.AnchorSkip("ArticlePage" + p.Page.ToString());
					}
				}
			}
		}
		

		protected Label EditArticleTitleSummaryPanelSavedLabel;
		public void EditArticleTitleSummaryPanelSave(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				EditArticleSave();
				EditArticleTitleSummaryPanelSavedLabel.Visible = true;
			}
		}
		protected Label EditArticleSubjectMatterPanelSavedLabel;
		public void EditArticleSubjectMatterPanelSave(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				EditArticleSave();
				EditArticleSubjectMatterPanelSavedLabel.Visible = true;
			}
		}
		protected Panel EditArticlePicturePanelSavedLabel;
		public void EditArticlePictureSave(object o, System.EventArgs e)
		{
			EditArticlePicturePanelSavedLabel.Visible = true;
		}
		public void EditArticleSave()
		{
			if (Page.IsValid)
			{
				CurrentArticle.Title = Cambro.Web.Helpers.StripHtml(EditArticleTitleTextBox.Text.Trim());
				CurrentArticle.Summary = Cambro.Web.Helpers.StripHtml(EditArticleSummaryTextBox.Text.Trim());
				if (EditArticleScopeEvent.Checked)
				{
					CurrentArticle.ParentObjectType = Model.Entities.ObjectType.Event;
					CurrentArticle.ParentObjectK = EditArticleScopeMultiPicker.Event.K;
					
					if (!Usr.Current.IsAdmin && CurrentArticle.Relevance != Model.Entities.Article.RelevanceEnum.Event)
						CurrentArticle.Relevance = Model.Entities.Article.RelevanceEnum.Venue;
				}
				else if (EditArticleScopeVenue.Checked)
				{
					CurrentArticle.ParentObjectType = Model.Entities.ObjectType.Venue;
					CurrentArticle.ParentObjectK = EditArticleScopeMultiPicker.Venue.K;

					if (!Usr.Current.IsAdmin)
						CurrentArticle.Relevance = Model.Entities.Article.RelevanceEnum.Venue;
					else if (CurrentArticle.Relevance.Equals(Model.Entities.Article.RelevanceEnum.Event))
						CurrentArticle.Relevance = Model.Entities.Article.RelevanceEnum.Venue;
					
				}
				else if (EditArticleScopePlace.Checked)
				{
					CurrentArticle.ParentObjectType = Model.Entities.ObjectType.Place;
					CurrentArticle.ParentObjectK = EditArticleScopeMultiPicker.Place.K;
					
					if (!Usr.Current.IsAdmin)
						CurrentArticle.Relevance = Model.Entities.Article.RelevanceEnum.Place;
					else if (CurrentArticle.Relevance.Equals(Model.Entities.Article.RelevanceEnum.Event) ||
						CurrentArticle.Relevance.Equals(Model.Entities.Article.RelevanceEnum.Venue))
						CurrentArticle.Relevance = Model.Entities.Article.RelevanceEnum.Place;
					
					
				}
				else if (EditArticleScopeCountry.Checked)
				{
					CurrentArticle.ParentObjectType = Model.Entities.ObjectType.Country;
					CurrentArticle.ParentObjectK = EditArticleScopeMultiPicker.Country.K;

					if (!Usr.Current.IsAdmin)
						CurrentArticle.Relevance = Model.Entities.Article.RelevanceEnum.Country; 
					else if (CurrentArticle.Relevance.Equals(Model.Entities.Article.RelevanceEnum.Event) ||
						CurrentArticle.Relevance.Equals(Model.Entities.Article.RelevanceEnum.Venue) ||
						CurrentArticle.Relevance.Equals(Model.Entities.Article.RelevanceEnum.Place))
						CurrentArticle.Relevance = Model.Entities.Article.RelevanceEnum.Country;
					
				}
				else
				{
					CurrentArticle.ParentObjectType = Model.Entities.ObjectType.None;
					CurrentArticle.Relevance = Model.Entities.Article.RelevanceEnum.Worldwide;
				}
				CurrentArticle.Update();
				CurrentArticle.UpdateAncestorLinks();
				
			}
		}
		public void BindBodyPageRepeater()
		{
			CurrentArticle = null;
			int[] pageArray = new int[CurrentArticle.LastPage];
			for (int i = 1; i <= CurrentArticle.LastPage; i++)
				pageArray[i - 1] = i;
			EditArticleBodyPageRepeater.ItemTemplate = this.LoadTemplate("/Templates/Articles/EditPage.ascx");
			EditArticleBodyPageRepeater.DataSource = pageArray;
			EditArticleBodyPageRepeater.DataBind();
		}

		protected void EditArticleScopeCheckChange(object o, EventArgs e)
		{
			updateEditArticleScope();
		}
		protected Picker EditArticleScopeMultiPicker;
		void updateEditArticleScope()
		{
			EditArticleScopeMultiFinderPanel.Visible = EditArticleScopeEvent.Checked || EditArticleScopeVenue.Checked || EditArticleScopePlace.Checked || EditArticleScopeCountry.Checked;

			EditArticleScopeMultiPicker.OptionEvent = EditArticleScopeEvent.Checked;
			EditArticleScopeMultiPicker.OptionDate = EditArticleScopeEvent.Checked;
			EditArticleScopeMultiPicker.OptionBrand = EditArticleScopeEvent.Checked;
			EditArticleScopeMultiPicker.OptionVenue = EditArticleScopeEvent.Checked || EditArticleScopeVenue.Checked;
			EditArticleScopeMultiPicker.OptionPlace = EditArticleScopeEvent.Checked || EditArticleScopeVenue.Checked || EditArticleScopePlace.Checked;
			EditArticleScopeMultiPicker.OptionCountry = EditArticleScopeEvent.Checked || EditArticleScopeVenue.Checked || EditArticleScopePlace.Checked || EditArticleScopeCountry.Checked;

			EditArticleScopeMultiPicker.ValidationType =
				EditArticleScopeEvent.Checked ? "event" :
				EditArticleScopeVenue.Checked ? "venue" :
				EditArticleScopePlace.Checked ? "place" :
				EditArticleScopeCountry.Checked ? "country" : "";

			ContainerPage.AnchorSkip("EditArticleScope");

		}
		//void updateRelevanceBoxes()
		//{
		//    EditArticleRelevanceCountry.Visible = EditArticleScopeEvent.Checked || EditArticleScopeVenue.Checked || EditArticleScopePlace.Checked || EditArticleScopeCountry.Checked;
		//    EditArticleRelevancePlace.Visible = EditArticleScopeEvent.Checked || EditArticleScopeVenue.Checked || EditArticleScopePlace.Checked;
		//    EditArticleRelevanceVenue.Visible = EditArticleScopeEvent.Checked || EditArticleScopeVenue.Checked;
		//    EditArticleRelevanceEvent.Visible = EditArticleScopeEvent.Checked;

		//    EditArticleRelevanceGeneral.Enabled = !EditArticleScopeGeneral.Checked;
		//    EditArticleRelevanceCountry.Enabled = !EditArticleScopeCountry.Checked;
		//    EditArticleRelevancePlace.Enabled = !EditArticleScopePlace.Checked;
		//    EditArticleRelevanceVenue.Enabled = !EditArticleScopeVenue.Checked;
		//    EditArticleRelevanceEvent.Enabled = !EditArticleScopeEvent.Checked;

		//    if (!Page.IsPostBack)
		//    {
		//        EditArticleRelevanceGeneral.Checked = EditArticleScopeGeneral.Checked;
		//        EditArticleRelevanceCountry.Checked = EditArticleScopeCountry.Checked;
		//        EditArticleRelevancePlace.Checked = EditArticleScopePlace.Checked;
		//        EditArticleRelevanceVenue.Checked = EditArticleScopeVenue.Checked;
		//        EditArticleRelevanceEvent.Checked = EditArticleScopeEvent.Checked;
		//    }
		//}
		protected void EditArticleRelevanceChecked(object o, EventArgs e)
		{
			//updateRelevanceBoxes();

			if (o != EditArticleRelevanceGeneral)
				EditArticleRelevanceGeneral.Checked = (o == EditArticleRelevanceGeneral && EditArticleRelevanceGeneral.Checked);
			
			if (o != EditArticleRelevanceCountry)
				EditArticleRelevanceCountry.Checked = o == EditArticleRelevanceGeneral || (o == EditArticleRelevanceCountry && EditArticleRelevanceCountry.Checked);

			if (o != EditArticleRelevancePlace)
				EditArticleRelevancePlace.Checked = o == EditArticleRelevanceGeneral || o == EditArticleRelevanceCountry || (o == EditArticleRelevancePlace && EditArticleRelevancePlace.Checked);

			if (o != EditArticleRelevanceVenue)
				EditArticleRelevanceVenue.Checked = o == EditArticleRelevanceGeneral || o == EditArticleRelevanceCountry || o == EditArticleRelevancePlace || (o == EditArticleRelevanceVenue && EditArticleRelevanceVenue.Checked);

			//if (o != EditArticleRelevanceEvent)
			//	EditArticleRelevanceEvent.Checked = o == EditArticleRelevanceGeneral || o == EditArticleRelevanceCountry || o == EditArticleRelevancePlace || EditArticleRelevanceVenue.Checked || (o == EditArticleRelevanceEvent && EditArticleRelevanceEvent.Checked);

		}


		#region EditArticleTitleCapsVal
		public void EditArticleTitleCapsVal(object o, ServerValidateEventArgs e)
		{
			CapsVal(EditArticleTitleTextBox, e);
		}
		#endregion
		#region EditArticleSummaryCapsVal
		public void EditArticleSummaryCapsVal(object o, ServerValidateEventArgs e)
		{
			CapsVal(EditArticleSummaryTextBox, e);
		}
		#endregion
		#region EditArticleTitlePunctuationVal
		public void EditArticleTitlePunctuationVal(object o, ServerValidateEventArgs e)
		{
			PunctuationVal(EditArticleTitleTextBox, e);
		}
		#endregion
		#region EditArticleSummaryPunctuationVal
		public void EditArticleSummaryPunctuationVal(object o, ServerValidateEventArgs e)
		{
			PunctuationVal(EditArticleSummaryTextBox, e);
		}
		#endregion

		protected void EditArticleAdminStatusSaveClick(object o, EventArgs e)
		{
			if (Usr.Current.IsAdmin)
			{
				if (EditArticleRelevanceGeneral.Checked)
					CurrentArticle.Relevance = Model.Entities.Article.RelevanceEnum.Worldwide;
				else if (EditArticleRelevanceCountry.Checked)
					CurrentArticle.Relevance = Model.Entities.Article.RelevanceEnum.Country;
				else if (EditArticleRelevancePlace.Checked)
					CurrentArticle.Relevance = Model.Entities.Article.RelevanceEnum.Place;
				else if (EditArticleRelevanceVenue.Checked)
					CurrentArticle.Relevance = Model.Entities.Article.RelevanceEnum.Venue;
				else if (EditArticleRelevanceEvent.Checked)
					CurrentArticle.Relevance = Model.Entities.Article.RelevanceEnum.Event;

				CurrentArticle.ShowAboveFoldOnFrontPage = EditArticleRelevanceFrontPageAboveFold.Checked;

				CurrentArticle.IsMixmagNews = EditArticleMixmag.Checked;
				CurrentArticle.IsExtendedDisplay = EditArticleExtended.Checked;

				for (int i = 0; i < MixmagSectionsListBox.Rows; i++)
				{
					if (MixmagSectionsListBox.Items[i].Selected)
						CurrentArticle.MixmagSectionAdd(i);
					else
						CurrentArticle.MixmagSectionRemove(i);
				}

				Article.StatusEnum newStatus = (Article.StatusEnum)int.Parse(EditArticleAdminStatusDrop.SelectedValue);
				if (newStatus == Model.Entities.Article.StatusEnum.Enabled && CurrentArticle.Status != Model.Entities.Article.StatusEnum.Enabled)
				{
					CurrentArticle.EnableArticle(Usr.Current, false, CurrentArticle.EnabledDateTime == DateTime.MinValue);
				}
				if (CurrentArticle.Status != newStatus)
				{
					CurrentArticle.Status = newStatus;
				}

				CurrentArticle.Update();
				CurrentArticle.UpdateAncestorLinks();
			}


		}

 		#endregion

		#region CurrentArticle
		public Article CurrentArticle
		{
			get
			{
				if (currentArticle == null && CurrentArticleK > 0)
					currentArticle = new Article(CurrentArticleK);
				if (currentArticle == null && CurrentArticleK == 0 && CurrentPara != null)
					return CurrentPara.Article;
				return currentArticle;
			}
			set
			{
				currentArticle = value;
			}
		}
		private Article currentArticle;
		#endregion

		#region CurrentArticleK
		public int CurrentArticleK
		{
			get
			{
				if (ContainerPage.Url["Mode"].Equals("Edit"))
					return ContainerPage.Url["K"];
				else
					return 0;
			}
		}
		#endregion

		#region EditArticleParaPanel
		public void EditArticleParaPanel_Load(object o, System.EventArgs e)
		{
			if (ContainerPage.Url["Mode"].Equals("Para"))
			{
				if ((CurrentArticle != null && !Usr.Current.CanEdit(CurrentArticle)) || (CurrentPara != null && !Usr.Current.CanEdit(CurrentPara.Article)))
				{
					ChangePanel(CantEditPanel);
					return;
				}
				ChangePanel(EditArticleParaPanel);
				if (!Page.IsPostBack && ContainerPage.Url["New"].IsNull)
				{
					EditArticleParaTypeTitle.Checked = CurrentPara.Type.Equals(Para.TypeEnum.Title);
					EditArticleParaTypePara.Checked = CurrentPara.Type.Equals(Para.TypeEnum.Para);
					EditArticleParaHtml.LoadHtml(CurrentPara.Text);//.Replace("<br>", "\n").Replace("<br />", "\n");
				}
			}
		}
		public void EditArticleParaAddPhotoClick(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				Para p = EditArticleParaSave();
				Response.Redirect("/pages/myarticles/mode-paraphoto/k-" + p.K.ToString());
			}
		}
		public void EditArticleParaCancelClick(object o, System.EventArgs e)
		{
			Response.Redirect("/pages/myarticles/mode-edit/k-" + CurrentPara.ArticleK.ToString() + "/parak-" + CurrentPara.K.ToString());
		}
		public void EditArticleParaSaveClick(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				Para p = EditArticleParaSave();
				Response.Redirect("/pages/myarticles/mode-edit/k-" + CurrentPara.ArticleK.ToString() + "/parak-" + CurrentPara.K.ToString());
			}
		}
		public Para EditArticleParaSave()
		{
			Para p = null;
			if (ContainerPage.Url["New"] == 1)
			{
				p = new Para();

				p.ArticleK = CurrentPara.ArticleK;
				p.Page = CurrentPara.Page;

				Query qHigher = new Query();
				qHigher.QueryCondition = new And(
					new Q(Para.Columns.ArticleK, CurrentPara.ArticleK),
					new Q(Para.Columns.Page, CurrentPara.Page),
					new Q(Para.Columns.Order, QueryOperator.GreaterThanOrEqualTo, CurrentPara.Order)
				);
				qHigher.TopRecords = 2;
				qHigher.OrderBy = new OrderBy(Para.Columns.Order, OrderBy.OrderDirection.Ascending);
				ParaSet ps = new ParaSet(qHigher);
				if (ps.Count == 2)
				{
					//put this para in between
					p.Order = (ps[0].Order + ps[1].Order) / 2.0;
				}
				else if (ps.Count == 1)
				{
					p.Order = ps[0].Order + 1.0;
				}
				else
				{
					throw new Exception("Prror placing para sdfmhdsaljkhsdaflkjh. Contact support quoting this error.");
				}
			}
			else
			{
				p = CurrentPara;
			}

			if (EditArticleParaTypeTitle.Checked)
				p.Type = Para.TypeEnum.Title;
			else if (EditArticleParaTypePara.Checked)
				p.Type = Para.TypeEnum.Para;
			else
				p.Type = Para.TypeEnum.Photo;

			if (p.Type.Equals(Para.TypeEnum.Title))
				p.Text = EditArticleParaHtml.GetPlainText();
			else
				p.Text = EditArticleParaHtml.GetHtml();

			p.Update();

			if (ContainerPage.Url["New"] == 1)
			{
				p.Article.ReOrder(CurrentPara.Page);
			}
			return p;

		}
		public void EditArticleParaTitleVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = !EditArticleParaTypeTitle.Checked || EditArticleParaHtml.GetPlainText().Length <= 100;
		}
		public void EditArticleParaTypeVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = EditArticleParaTypeTitle.Checked || EditArticleParaTypePara.Checked;
		}

		#region CurrentPara
		public Para CurrentPara
		{
			get
			{
				if (currentPara == null && CurrentParaK > 0)
					currentPara = new Para(CurrentParaK);
				return currentPara;
			}
			set
			{
				currentPara = value;
			}
		}
		private Para currentPara;
		#endregion

		#region CurrentParaK
		public int CurrentParaK
		{
			get
			{
				if (ContainerPage.Url["Mode"].Equals("Para") ||
					ContainerPage.Url["Mode"].Equals("ParaPhoto"))
					return ContainerPage.Url["K"];
				else
					return 0;
			}
		}
		#endregion

		#endregion

		#region EditArticleParaPhotoPanel
		public void EditArticleParaPhotoPanel_Load(object o, System.EventArgs e)
		{
			if (ContainerPage.Url["Mode"].Equals("ParaPhoto"))
			{
				if ((CurrentArticle != null && !Usr.Current.CanEdit(CurrentArticle)) || (CurrentPara != null && !Usr.Current.CanEdit(CurrentPara.Article)))
				{
					ChangePanel(CantEditPanel);
					return;
				}
				if (!Page.IsPostBack)
				{
					ChangePanel(EditArticleParaPhotoPanel);
					if (CurrentPara.HasPicPrivate)
					{
						EditArticleParaPhotoHidden.Value = CurrentPara.PhotoK.ToString();
						if (CurrentPara.Photo.Article != null)
							EditArticleParaPhotoSourceUploadedCheck.Checked = true;
						else if (CurrentPara.Photo.Event != null && CurrentArticle.ParentObjectType.Equals(Model.Entities.ObjectType.Event) && CurrentPara.Photo.EventK == CurrentArticle.ParentObjectK)
							EditArticleParaPhotoSourceEventCheck.Checked = true;
						else
						{
							EditArticleParaPhotoSourceMiscCheck.Checked = true;
							EditArticleParaPhotoSourceKTextBox.Text = new Photo(CurrentPara.PhotoK).Url();
						}

						if (CurrentPara.PicPhoto != null)
						{
							EditArticleParaPhotoSaveNoResizeP.Visible = CurrentPara.PicPhoto.OriginalHeight <= 450 && CurrentPara.PicPhoto.OriginalWidth <= 565
								&& CurrentPara.PicPhoto.OriginalWidth >= 100 && CurrentPara.PicPhoto.OriginalHeight >= 100;
							EditArticleParaPhotoCropperPanel.Visible = true;
							EditArticleParaPhotoCropper.ImageUrl = CurrentPara.PicPhoto.CropPath;
							EditArticleParaPhotoCropper.ImageGuid = CurrentPara.PicPhoto.Crop;
							EditArticleParaPhotoCropper.ImageStore = Storage.Stores.Pix;
							EditArticleParaPhotoCropper.CropHeight = CurrentPara.PicHeight;
							EditArticleParaPhotoCropper.CropWidth = CurrentPara.PicWidth;
							if (CurrentPara.PicState.Length > 0)
								EditArticleParaPhotoCropper.SetState(CurrentPara.PicState);
						}
						else if (CurrentPara.Photo != null)
						{
							if (CurrentPara.Photo.MediaType.Equals(Photo.MediaTypes.Image))
							{
								EditArticleParaPhotoSaveNoResizeP.Visible = CurrentPara.Photo.OriginalHeight <= 450 && CurrentPara.Photo.OriginalWidth <= 565
								&& CurrentPara.Photo.OriginalWidth >= 100 && CurrentPara.Photo.OriginalHeight >= 100;
								EditArticleParaPhotoCropperPanel.Visible = true;
								EditArticleParaPhotoCropper.ImageUrl = CurrentPara.Photo.CropPath;
								EditArticleParaPhotoCropper.ImageGuid = CurrentPara.Photo.Crop;
								EditArticleParaPhotoCropper.ImageStore = Storage.Stores.Pix;
							}
							else if (CurrentPara.Photo.MediaType.Equals(Photo.MediaTypes.Video))
							{
								EditArticleParaPhotoVideoPanel.Visible = true;
								EditArticleParaPhotoCropperPanel.Visible = false;
								EditArticleParaPhotoVideo.Width = EditArticleParaPhotoCurrent.VideoMedWidth;
								EditArticleParaPhotoVideo.Height = EditArticleParaPhotoCurrent.VideoMedHeight;
								EditArticleParaPhotoVideo.JpgUrl = EditArticleParaPhotoCurrent.WebPath;
								EditArticleParaPhotoVideo.VideoUrl = EditArticleParaPhotoCurrent.VideoMedPath;
								EditArticleParaPhotoVideo.AutoStart = false;
							}
						}

						if (CurrentPara.PhotoType == Model.Entities.Para.PhotoTypes.None)
							EditArticleParaPhotoPositionHidden.Checked = true;
						else if (CurrentPara.PhotoAlign.Equals(Para.PhotoAlignEnum.Top))
							EditArticleParaPhotoPositionTop.Checked = true;
						else if (CurrentPara.PhotoAlign.Equals(Para.PhotoAlignEnum.Left))
							EditArticleParaPhotoPositionLeft.Checked = true;
						else if (CurrentPara.PhotoAlign.Equals(Para.PhotoAlignEnum.Right))
							EditArticleParaPhotoPositionRight.Checked = true;
						else if (CurrentPara.PhotoAlign.Equals(Para.PhotoAlignEnum.Bottom))
							EditArticleParaPhotoPositionBottom.Checked = true;
						



					}
				}
				EditArticleParaPhotoUploadLink.HRef = "/pages/galleries/add/articlek-" + CurrentArticle.K;
				PhotosIFrame.Attributes["src"] = "/popup/paraphotolist/k-" + CurrentArticle.K + "#Photo" + EditArticleParaPhotoHidden.Value;
				EditArticleParaPhotoSourceIFrame.Style["padding"] = "0px";
				EditArticleParaPhotoSourceIFrame.Style["margin-left"] = "24px";
				EditArticleParaPhotoSourceEventIFrame.Style["padding"] = "0px";
				EditArticleParaPhotoSourceEventIFrame.Style["margin-left"] = "24px";

				if (CurrentArticle.ParentObjectType.Equals(Model.Entities.ObjectType.Event))
				{
					GallerySet gs = new GallerySet(new Query(new And(new Q(Gallery.Columns.EventK, CurrentArticle.ParentObjectK), new Q(Gallery.Columns.TotalPhotos, QueryOperator.GreaterThan, 0)), new OrderBy(Gallery.Columns.Name)));
					EditArticleParaPhotoSourceEventPanel.Style["display"] = gs.Count > 0 ? null : "none";
					if (gs.Count > 0)
					{
						EditArticleParaPhotoSourceEventGalleryDropDown.DataSource = gs;
						EditArticleParaPhotoSourceEventGalleryDropDown.DataTextField = "Name";
						EditArticleParaPhotoSourceEventGalleryDropDown.DataValueField = "K";
						EditArticleParaPhotoSourceEventGalleryDropDown.Attributes["onchange"] =
							"if(document.forms[0].elements['" + EditArticleParaPhotoSourceEventGalleryDropDown.ClientID + "'].selectedIndex==0)" +
							"{document.getElementById('" + EditArticleParaPhotoSourceEventIFrame.ClientID + "').style.display='none';}else" +
							"{document.getElementById('" + EditArticleParaPhotoSourceEventIFrame.ClientID + "').style.display='';" +
							"document.getElementById('" + PhotosEventIFrame.ClientID + "').src='/popup/paraphotolist/galleryk-'+" +
							"document.forms[0].elements['" + EditArticleParaPhotoSourceEventGalleryDropDown.ClientID + "'][" +
								"document.forms[0].elements['" + EditArticleParaPhotoSourceEventGalleryDropDown.ClientID + "'].selectedIndex].value;}";
						EditArticleParaPhotoSourceEventGalleryDropDown.DataBind();
						EditArticleParaPhotoSourceEventGalleryDropDown.Items.Insert(0, new ListItem("select gallery here...", "0"));
						if (!Page.IsPostBack && EditArticleParaPhotoSourceEventCheck.Checked)
							EditArticleParaPhotoSourceEventGalleryDropDown.SelectedValue = CurrentPara.Photo.GalleryK.ToString();


					}
				}
				else
					EditArticleParaPhotoSourceEventPanel.Style["display"] = "none";

				EditArticleParaPhotoSourceUploadedCheck.Attributes["onclick"] =
					"document.getElementById('" + EditArticleParaPhotoSourceEventIFrame.ClientID + "').style.display='none';" +
					"document.getElementById('" + EditArticleParaPhotoSourceMiscRefP.ClientID + "').style.display='none';" +
					"document.getElementById('" + EditArticleParaPhotoSourceEventGalleriesP.ClientID + "').style.display='none';" +
					"document.getElementById('" + EditArticleParaPhotoSourceIFrame.ClientID + "').style.display='';";
				if (EditArticleParaPhotoSourceUploadedCheck.Checked)
				{
					EditArticleParaPhotoSourceIFrame.Style["display"] = null;
				}

				EditArticleParaPhotoSourceEventCheck.Attributes["onclick"] =
					"document.getElementById('" + EditArticleParaPhotoSourceEventIFrame.ClientID + "').style.display=" +
						"(document.forms[0].elements['" + EditArticleParaPhotoSourceEventGalleryDropDown.ClientID + "'].selectedIndex=='0'?'none':'');" +
					"document.getElementById('" + EditArticleParaPhotoSourceMiscRefP.ClientID + "').style.display='none';" +
					"document.getElementById('" + EditArticleParaPhotoSourceEventGalleriesP.ClientID + "').style.display='';" +
					"document.getElementById('" + EditArticleParaPhotoSourceIFrame.ClientID + "').style.display='none';";
				if (EditArticleParaPhotoSourceEventCheck.Checked)
				{
					EditArticleParaPhotoSourceEventGalleriesP.Style["display"] = null;
					EditArticleParaPhotoSourceEventGalleriesP.Style["margin-left"] = "24px";
					if (EditArticleParaPhotoSourceEventGalleryDropDown.SelectedValue != "0")
					{
						EditArticleParaPhotoSourceEventIFrame.Style["display"] = null;
						PhotosEventIFrame.Attributes["src"] = "/popup/paraphotolist/galleryk-" + int.Parse(EditArticleParaPhotoSourceEventGalleryDropDown.SelectedValue) + "#Photo" + EditArticleParaPhotoHidden.Value;
					}
				}

				EditArticleParaPhotoSourceMiscCheck.Attributes["onclick"] = "document.getElementById('" + EditArticleParaPhotoSourceEventIFrame.ClientID + "').style.display='none';document.getElementById('" + EditArticleParaPhotoSourceMiscRefP.ClientID + "').style.display='';document.getElementById('" + EditArticleParaPhotoSourceEventGalleriesP.ClientID + "').style.display='none';document.getElementById('" + EditArticleParaPhotoSourceIFrame.ClientID + "').style.display='none';";
				if (EditArticleParaPhotoSourceMiscCheck.Checked)
				{
					EditArticleParaPhotoSourceMiscRefP.Style["display"] = null;
					EditArticleParaPhotoSourceMiscRefP.Style["margin-left"] = "24px";
				}

			}
		}

		public void EditArticleParaPhotoCancelClick(object o, System.EventArgs e)
		{
			Response.Redirect("/pages/myarticles/mode-edit/k-" + CurrentPara.ArticleK.ToString() + "/parak-" + CurrentPara.K.ToString());
		}
		public void EditArticleParaPhotoSaveClick(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				SaveImage(true);
			}
		}
		public void EditArticleParaPhotoSaveNoResize(object o, System.EventArgs e)
		{
			if (EditArticleParaPhotoCurrent.OriginalHeight <= 450 && EditArticleParaPhotoCurrent.OriginalWidth <= 565 && EditArticleParaPhotoCurrent.OriginalWidth >= 100 && EditArticleParaPhotoCurrent.OriginalHeight >= 100)
			{
				SaveImage(false);
			}
		}
		void SaveImage(bool useCropper)
		{
			bool deletePreviousPic = false;
			Guid previousPic = Guid.Empty;

			if (EditArticleParaPhotoCurrent.MediaType.Equals(Photo.MediaTypes.Image))
			{
				CurrentPara.PhotoK = EditArticleParaPhotoCurrent.K;
				CurrentPara.PicPhotoK = EditArticleParaPhotoCurrent.K;
				CurrentPara.PhotoType = Para.PhotoTypes.Custom;
				if (!CurrentPara.Pic.Equals(Guid.Empty))
				{
					deletePreviousPic = true;
					previousPic = CurrentPara.Pic;
				}
				CurrentPara.Pic = Guid.NewGuid();
				if (useCropper)
				{
					EditArticleParaPhotoCropper.Store(CurrentPara.Pic, CurrentPara, "Pic");
					CurrentPara.PicWidth = EditArticleParaPhotoCropper.CropWidth;
					CurrentPara.PicHeight = EditArticleParaPhotoCropper.CropHeight;
					CurrentPara.PicState = EditArticleParaPhotoCropper.GetState();
				}
				else
				{
					Storage.AddToStore(
						Storage.GetFromStore(Storage.Stores.Master, EditArticleParaPhotoCurrent.Master, "jpg"),
						Storage.Stores.Pix,
						CurrentPara.Pic,
						"jpg",
						CurrentPara,
						"Pic"
					);
					CurrentPara.PicWidth = EditArticleParaPhotoCurrent.OriginalWidth;
					CurrentPara.PicHeight = EditArticleParaPhotoCurrent.OriginalHeight;
					CurrentPara.PicState = "0$0$1$" + EditArticleParaPhotoCurrent.OriginalWidth + "$" + EditArticleParaPhotoCurrent.OriginalHeight;
				}

				if (EditArticleParaPhotoPositionTop.Checked)
					CurrentPara.PhotoAlign = Para.PhotoAlignEnum.Top;
				else if (EditArticleParaPhotoPositionLeft.Checked)
					CurrentPara.PhotoAlign = Para.PhotoAlignEnum.Left;
				else if (EditArticleParaPhotoPositionRight.Checked)
					CurrentPara.PhotoAlign = Para.PhotoAlignEnum.Right;
				else if (EditArticleParaPhotoPositionBottom.Checked)
					CurrentPara.PhotoAlign = Para.PhotoAlignEnum.Bottom;
				else if (EditArticleParaPhotoPositionHidden.Checked)
					CurrentPara.PhotoType = Model.Entities.Para.PhotoTypes.None;
			}
			else if (EditArticleParaPhotoCurrent.MediaType.Equals(Photo.MediaTypes.Video))
			{
				CurrentPara.PhotoK = EditArticleParaPhotoCurrent.K;
				CurrentPara.PhotoType = Para.PhotoTypes.VideoMed;

				if (!CurrentPara.Pic.Equals(Guid.Empty))
				{
					deletePreviousPic = true;
					previousPic = CurrentPara.Pic;
				}
				CurrentPara.PicPhotoK = 0;
				CurrentPara.PicWidth = 0;
				CurrentPara.PicHeight = 0;
				CurrentPara.PicState = "";
				CurrentPara.Pic = Guid.Empty;

				if (EditArticleParaPhotoPositionTop.Checked)
					CurrentPara.PhotoAlign = Para.PhotoAlignEnum.Top;
				else
					CurrentPara.PhotoAlign = Para.PhotoAlignEnum.Bottom;
			}

			CurrentPara.Update();
			if (deletePreviousPic)
				Storage.RemoveFromStore(Storage.Stores.Pix, previousPic, "jpg");

			Response.Redirect("/pages/myarticles/mode-edit/k-" + CurrentPara.ArticleK.ToString() + "/parak-" + CurrentPara.K.ToString());
		}
		public void EditArticleParaPhotoPositionVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = EditArticleParaPhotoPositionTop.Checked ||
				EditArticleParaPhotoPositionLeft.Checked ||
				EditArticleParaPhotoPositionRight.Checked ||
				EditArticleParaPhotoPositionBottom.Checked ||
				EditArticleParaPhotoPositionHidden.Checked;
			if (!e.IsValid)
				ContainerPage.AnchorSkip("EditArticleParaPhotoPositionAnchor");
		}
		public void EditArticleParaPhotoShowCommand(object o, CommandEventArgs e)
		{
			ShowPreviews();
		}
		public void EditArticleParaPhotoUpdatePreviews(object o, System.EventArgs e)
		{
			EditArticleParaPhotoHidden.Value = EditArticleParaPhotoSourceKTextBox.Text.Substring(EditArticleParaPhotoSourceKTextBox.Text.LastIndexOf("-") + 1);
			ShowPreviews();
		}
		void ShowPreviews()
		{
			if (EditArticleParaPhotoCurrent == null)
				EditArticleParaPhotoUpdatePreviewsError.Visible = true;
			else if (EditArticleParaPhotoCurrent.MediaType.Equals(Photo.MediaTypes.Image))
			{
				EditArticleParaPhotoVideoPanel.Visible = false;
				EditArticleParaPhotoCropperPanel.Visible = true;
				EditArticleParaPhotoCropper.ImageUrl = EditArticleParaPhotoCurrent.CropPath;
				EditArticleParaPhotoCropper.ImageGuid = EditArticleParaPhotoCurrent.Crop;
				EditArticleParaPhotoCropper.ImageStore = Storage.Stores.Pix;
				EditArticleParaPhotoCropper.SetState("0$0$0$200$200");
				EditArticleParaPhotoSaveNoResizeP.Visible = EditArticleParaPhotoCurrent.OriginalHeight <= 450 && EditArticleParaPhotoCurrent.OriginalWidth <= 565
					&& EditArticleParaPhotoCurrent.OriginalHeight >= 100 && EditArticleParaPhotoCurrent.OriginalWidth >= 100;
				EditArticleParaPhotoUpdatePreviewsError.Visible = false;
			}
			else if (EditArticleParaPhotoCurrent.MediaType.Equals(Photo.MediaTypes.Video))
			{
				EditArticleParaPhotoVideoPanel.Visible = true;
				EditArticleParaPhotoCropperPanel.Visible = false;
				EditArticleParaPhotoVideo.Width = EditArticleParaPhotoCurrent.VideoMedWidth;
				EditArticleParaPhotoVideo.Height = EditArticleParaPhotoCurrent.VideoMedHeight;
				EditArticleParaPhotoVideo.JpgUrl = EditArticleParaPhotoCurrent.WebPath;
				EditArticleParaPhotoVideo.VideoUrl = EditArticleParaPhotoCurrent.VideoMedPath;
				EditArticleParaPhotoVideo.AutoStart = true;
			}
			this.ContainerPage.AnchorSkip("EditArticleParaPhotoUpdatePreviewsButton");
		}
		#region EditArticleParaPhotoCurrent
		public Photo EditArticleParaPhotoCurrent
		{
			get
			{
				if (editArticleParaPhotoCurrent == null && EditArticleParaPhotoK > 0)
				{
					try
					{
						editArticleParaPhotoCurrent = new Photo(EditArticleParaPhotoK);
					}
					catch { }
				}
				return editArticleParaPhotoCurrent;
			}
			set
			{
				editArticleParaPhotoCurrent = value;
			}
		}
		private Photo editArticleParaPhotoCurrent;
		int EditArticleParaPhotoK
		{
			get
			{
				if (EditArticleParaPhotoHidden.Value.Length > 0)
					return int.Parse(EditArticleParaPhotoHidden.Value);
				else
					return 0;
			}
		}
		#endregion

		#endregion

		public void ChangePanel(Panel p)
		{
			CurrentArticlesPanel.Visible = p.Equals(CurrentArticlesPanel);
			AddArticlePanel.Visible = p.Equals(AddArticlePanel);
			CantEditPanel.Visible = p.Equals(CantEditPanel);

			EditArticleTitleSummaryPanel.Visible = p.Equals(EditArticleTitleSummaryPanel);
			
			EditArticleSubjectMatterPanel.Visible = p.Equals(EditArticleSubjectMatterPanel);
			
			EditArticlePicturePanel.Visible = p.Equals(EditArticlePicturePanel);
			
			EditArticleBodyPanel.Visible = p.Equals(EditArticleBodyPanel);
			
			EditArticlePhotoUploadPanel.Visible = p.Equals(EditArticlePhotoUploadPanel);
			EditArticleAdminPanel.Visible = p.Equals(EditArticleAdminPanel);
			
			EditArticleParaPanel.Visible = p.Equals(EditArticleParaPanel);

			EditArticleParaPhotoPanel.Visible = p.Equals(EditArticleParaPhotoPanel);

			if (p.Equals(EditArticleTitleSummaryPanel) ||
				p.Equals(EditArticleSubjectMatterPanel) ||
				p.Equals(EditArticlePicturePanel) ||
				p.Equals(EditArticleBodyPanel) ||
				p.Equals(EditArticlePhotoUploadPanel) ||
				p.Equals(EditArticleAdminPanel))
			{
				EditArticleIndexPanel.Visible = true;
			}
			else
			{
				EditArticleIndexPanel.Visible = false;
			}
		}

		 
	}
}
