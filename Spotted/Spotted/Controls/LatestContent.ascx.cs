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
using ArchiveObjectType = Model.Entities.ArchiveObjectType;

namespace Spotted.Controls
{
	public partial class LatestContent : System.Web.UI.UserControl
	{
		public LatestContent()
		{
			this.PreRender += new System.EventHandler(this.NewsPanel_Load);
			this.PreRender += new System.EventHandler(this.ArticlesPanel_Load);
			this.PreRender += new System.EventHandler(this.GalleriesPanel_Load);
			this.PreRender += new System.EventHandler(this.ReviewsPanel_Load);
			this.PreRender += new System.EventHandler(this.CompPanel_Load);
			this.PreRender += new System.EventHandler(this.GroupsPanel_Load);
			this.PreRender += new System.EventHandler(this.EventsPanel_Load);
		}
		protected Panel ContentPanel;

		#region ArticlesPanel
		protected Panel ArticlesPanel;
		protected DataList ArticlesDataList;
		int ArticleCount
		{
			get
			{
				return (int)(Items);
			}
		}
		private void ArticlesPanel_Load(object o, EventArgs e)
		{
			if (OnlyShowThreads)
			{
				ArticlesPanel.Visible = false;
				return;
			}
			Query q = new Query();
			Q RelevanceQ = null;
			Q NewArticlesQ = new Q(true);
			if (Discussable == null)
			{
				RelevanceQ = new Q(Article.Columns.Relevance, Article.RelevanceEnum.Worldwide);
				NewArticlesQ = new NotQ(new And(new Q(Article.Columns.ShowAboveFoldOnFrontPage, true), new Q(Article.Columns.EnabledDateTime, QueryOperator.GreaterThan, DateTime.Now.AddHours(-48))));//new Q(Article.Columns.ShowAboveFoldOnFrontPage, false);
				
			}
			else if (Discussable.UsedDiscussable is Country)
				RelevanceQ = new Q(Article.Columns.CountryK, Discussable.UsedDiscussable.K);
			else if (Discussable.UsedDiscussable is Place)
				RelevanceQ = new Q(Article.Columns.PlaceK, Discussable.UsedDiscussable.K);
			else if (Discussable.UsedDiscussable is Venue)
				RelevanceQ = new Q(Article.Columns.VenueK, Discussable.UsedDiscussable.K);
			else if (Discussable.UsedDiscussable is Event)
				RelevanceQ = new Q(Article.Columns.EventK, Discussable.UsedDiscussable.K);
			else if (Discussable.UsedDiscussable is Brand)
			{
				RelevanceQ = new Q(Brand.Columns.K, Discussable.UsedDiscussable.K);
				q.TableElement = new Join(
						new TableElement(TablesEnum.Article),
						Event.BrandJoin,
						QueryJoinType.Inner,
						Article.Columns.EventK,
						Event.Columns.K);
			}
			else if (Discussable.UsedDiscussable is Group)
			{

				RelevanceQ = new Q(Group.Columns.K, Discussable.UsedDiscussable.K);
				q.TableElement = new Join(
					new TableElement(TablesEnum.Article),
					Event.GroupJoin,
					QueryJoinType.Inner,
					Article.Columns.EventK,
					Event.Columns.K);
			}

			q.Columns = Templates.Articles.LatestNew.Columns;
			q.TableElement = Templates.Articles.LatestNew.PerformJoins(q.TableElement);

			q.QueryCondition = new And(
				Article.EnabledQueryCondition,
				RelevanceQ,
				NewArticlesQ
				);
			q.OrderBy = new OrderBy(Article.Columns.EnabledDateTime, OrderBy.OrderDirection.Descending);
			q.TopRecords = ArticleCount;
			ArticleSet ars = new ArticleSet(q);

			if (ars.Count == 0)
			{
				ArticlesPanel.Visible = false;
			}
			else
			{
				if (Discussable == null)
				{
					ArticlesArchiveUrl = Archive.GetUrl(ars[0].EnabledDateTime.Year, ars[0].EnabledDateTime.Month, 0, ArchiveObjectType.Article, new string[] { }, "");
				}
				else if (Discussable.UsedDiscussable is IHasArchive && ars.Count == ArticleCount)
				{
					ArticlesArchiveUrl = ((IHasArchive)Discussable.UsedDiscussable).UrlArchiveDate(ars[0].EnabledDateTime.Year, ars[0].EnabledDateTime.Month, 0, ArchiveObjectType.Article);
				}
				else
				{
					ArticlesArchiveVisible = false;
				}

				ArticlesDataList.DataSource = ars;
				ArticlesDataList.ItemTemplate = this.LoadTemplate("/Templates/Articles/LatestNew.ascx");
				ArticlesDataList.DataBind();

			}
		}
		#endregion

		public bool ArticlesArchiveVisible = true;
		public string ArticlesArchiveUrl = "";

		#region NewsPanel
		private void NewsPanel_Load(object o, EventArgs e)
		{
			if (OnlyShowThreads)
			{
				NewsPanel.Visible = false;
				return;
			}


			Q RelevanceQ = null;

			if (Discussable == null)
			{
				RelevanceQ = new Q(true);
			}
			else if (Discussable.UsedDiscussable is Country)
				RelevanceQ = new Q(Thread.Columns.CountryK, Discussable.UsedDiscussable.K);
			else if (Discussable.UsedDiscussable is Place)
				RelevanceQ = new Q(Thread.Columns.PlaceK, Discussable.UsedDiscussable.K);
			else if (Discussable.UsedDiscussable is Venue)
				RelevanceQ = new Q(Thread.Columns.VenueK, Discussable.UsedDiscussable.K);
			else if (Discussable.UsedDiscussable is Event)
				RelevanceQ = new Q(Thread.Columns.EventK, Discussable.UsedDiscussable.K);
			else if (Discussable.UsedDiscussable is Brand)
				RelevanceQ = Group.ThreadsQ(((Brand)Discussable.UsedDiscussable).GroupK);
			else if (Discussable.UsedDiscussable is Group)
				RelevanceQ = Group.ThreadsQWithLinkedEvents((Group)Discussable.UsedDiscussable);

			Query q = new Query();

			if (Discussable != null && Discussable.UsedDiscussable is Group)
				q.TableElement = Thread.EventGroupLeftJoin;

			q.TableElement = Templates.Threads.NewsLatest.PerformJoins(q.TableElement);
			q.Columns = Templates.Threads.NewsLatest.Columns;
			q.QueryCondition = new And(
				RelevanceQ,
				new Q(Thread.Columns.IsNews, true),
				new Q(Thread.Columns.GroupPrivate, false),
				new Q(Thread.Columns.PrivateGroup, false));
			q.OrderBy = Thread.NewsOrder;
			q.TopRecords = Items;

			ThreadSet tsNews = new ThreadSet(q);

			if (tsNews.Count == 0)
				NewsPanel.Visible = false;
			else
			{
				NewsDataList.DataSource = tsNews;
				NewsDataList.ItemTemplate = this.LoadTemplate("/Templates/Threads/NewsLatest.ascx");
				NewsDataList.DataBind();

				if (Discussable == null)
					NewsArchiveAnchor.HRef = Archive.GetUrl(tsNews[0].DateTime.Year, tsNews[0].DateTime.Month, 0, ArchiveObjectType.News, new string[] { }, "");
				else if (Discussable.UsedDiscussable is IHasArchive && tsNews.Count == Items)
					NewsArchiveAnchor.HRef = ((IHasArchive)Discussable.UsedDiscussable).UrlArchiveDate(tsNews[0].DateTime.Year, tsNews[0].DateTime.Month, 0, ArchiveObjectType.News);
				else
				{
					NewsArchiveDiv.Visible = false;
					NewsPanelInner.Attributes["class"] = "LatestPanel Big CleanLinks";
				}
			}
		}
		#endregion

		#region CompPanel
		protected Panel CompPanel;
		protected DataList CompDataList;
		protected HtmlGenericControl CompArchiveDiv;
		protected HtmlAnchor CompArchiveAnchor;
		public void CompPanel_Load(object o, System.EventArgs e)
		{
			if (OnlyShowThreads)
			{
				CompPanel.Visible = false;
				return;
			}

			Q RelevanceQ = null;
			if (Discussable == null)
				RelevanceQ = new Q(true);
			else if (Discussable.UsedDiscussable is Event)
				RelevanceQ = new Q(Comp.Columns.EventK, Discussable.UsedDiscussable.K);
			else if (Discussable.UsedDiscussable is Venue)
				RelevanceQ = new Q(Event.Columns.VenueK, Discussable.UsedDiscussable.K);
			else if (Discussable.UsedDiscussable is Place)
				RelevanceQ = new Q(Venue.Columns.PlaceK, Discussable.UsedDiscussable.K);
			else if (Discussable.UsedDiscussable is Country)
				RelevanceQ = new Q(Place.Columns.CountryK, Discussable.UsedDiscussable.K);
			else if (Discussable.UsedDiscussable is Brand)
				RelevanceQ = new Or(
					new Q(Brand.Columns.K, Discussable.UsedDiscussable.K),
					new Q(Comp.Columns.BrandK, Discussable.UsedDiscussable.K));
			else if (Discussable.UsedDiscussable is Group)
				RelevanceQ = new Q(Group.Columns.K, Discussable.UsedDiscussable.K);

			TableElement t = new TableElement(TablesEnum.Comp);
			if (Discussable != null && Discussable.UsedDiscussable is Venue)
				t = new Join(Comp.Columns.EventK, Event.Columns.K);
			else if (Discussable != null && Discussable != null && Discussable.UsedDiscussable is Place)
				t = new Join(new Join(Comp.Columns.EventK, Event.Columns.K), new TableElement(TablesEnum.Venue), QueryJoinType.Inner, Event.Columns.VenueK, Venue.Columns.K);
			else if (Discussable != null && Discussable.UsedDiscussable is Country)
				t = new Join(
						new Join(
							new JoinLeft(Comp.Columns.EventK, Event.Columns.K),
							new TableElement(TablesEnum.Venue),
							QueryJoinType.Left,
							Event.Columns.VenueK,
							Venue.Columns.K),
						new TableElement(TablesEnum.Place),
						QueryJoinType.Left,
						Venue.Columns.PlaceK,
						Place.Columns.K);
			else if (Discussable != null && Discussable.UsedDiscussable is Brand)
				t = new Join(new TableElement(TablesEnum.Comp), Event.BrandJoin, QueryJoinType.Left, Comp.Columns.EventK, Event.Columns.K);
			else if (Discussable != null && Discussable.UsedDiscussable is Group)
				t = new Join(new TableElement(TablesEnum.Comp), Event.GroupJoin, QueryJoinType.Inner, Comp.Columns.EventK, Event.Columns.K);

			t = Templates.Comps.Latest.PerformJoins(t);

			Query q = new Query();
			q.Columns = Templates.Comps.Latest.Columns;

			q.QueryCondition = new And(
				new Q(Comp.Columns.Status, Comp.StatusEnum.Enabled),
				new Q(Comp.Columns.DateTimeClose, QueryOperator.GreaterThan, DateTime.Now),
				new Q(Comp.Columns.DateTimeStart, QueryOperator.LessThan, DateTime.Now),
				RelevanceQ
				);
			q.OrderBy = new OrderBy(new OrderBy(Comp.Columns.PrizeValueRange, OrderBy.OrderDirection.Descending), new OrderBy(OrderBy.OrderDirection.Random));
			q.TopRecords = Items;
			q.TableElement = t;
			CompSet cs = new CompSet(q);

			if (cs.Count == 0)
				CompPanel.Visible = false;
			else
			{
				CompPanel.Visible = true;
				CompDataList.DataSource = cs;
				CompDataList.ItemTemplate = this.LoadTemplate("/Templates/Comps/Latest.ascx");
				CompDataList.DataBind();

				if (Discussable == null)
					CompArchiveAnchor.HRef = Archive.GetUrl(DateTime.Now.Year, DateTime.Now.Month, 0, ArchiveObjectType.Comp, new string[] { }, "");
				else if (Discussable.UsedDiscussable is IHasArchive && cs.Count == Items)
					CompArchiveAnchor.HRef = ((IHasArchive)Discussable.UsedDiscussable).UrlArchiveDate(DateTime.Now.Year, DateTime.Now.Month, 0, ArchiveObjectType.Comp);
				else
				{
					CompArchiveDiv.Visible = false;
					CompPanelInner.Attributes["class"] = "LatestPanel Big CleanLinks";
				}
			}
		}
		#endregion

		#region ReviewsPanel
		public void ReviewsPanel_Load(object o, System.EventArgs e)
		{
			if (OnlyShowThreads)
			{
				ReviewsPanel.Visible = false;
				return;
			}
			Query q = new Query();
			Q RelevanceQ = new Q(false);
			if (Discussable == null)
				RelevanceQ = new Q(true);
			else if (Discussable.UsedDiscussable is Country)
				RelevanceQ = new Q(Thread.Columns.CountryK, Discussable.UsedDiscussable.K);
			else if (Discussable.UsedDiscussable is Place)
				RelevanceQ = new Q(Thread.Columns.PlaceK, Discussable.UsedDiscussable.K);
			else if (Discussable.UsedDiscussable is Venue)
				RelevanceQ = new Q(Thread.Columns.VenueK, Discussable.UsedDiscussable.K);
			else if (Discussable.UsedDiscussable is Event)
				RelevanceQ = new Q(Thread.Columns.EventK, Discussable.UsedDiscussable.K);
			else if (Discussable.UsedDiscussable is Brand)
			{
				RelevanceQ = new Q(EventBrand.Columns.BrandK, Discussable.UsedDiscussable.K);
				q.TableElement = Thread.EventBrandJoin;
			}
			else if (Discussable.UsedDiscussable is Group)
			{
				RelevanceQ = new Q(GroupEvent.Columns.GroupK, Discussable.UsedDiscussable.K);
				q.TableElement = Thread.EventGroupJoin;
			}

			

		 

			q.TableElement = Templates.Threads.ReviewLatest.PerformJoins(q.TableElement);
			q.Columns = Templates.Threads.ReviewLatest.Columns;
			q.QueryCondition = new And(
				new Q(Thread.Columns.IsReview, true),
				RelevanceQ,
				new Q(Thread.Columns.Private, false),
				new Q(Thread.Columns.GroupPrivate, false),
				new Q(Thread.Columns.PrivateGroup, false)
			);
			q.OrderBy = new OrderBy(Thread.Columns.DateTime, OrderBy.OrderDirection.Descending);
			q.TopRecords = Items;
			ThreadSet ts = new ThreadSet(q);

			if (ts.Count == 0)
				ReviewsPanel.Visible = false;
			else
			{
				ReviewsDataList.DataSource = ts;
				ReviewsDataList.ItemTemplate = this.LoadTemplate("/Templates/Threads/ReviewLatest.ascx");
				ReviewsDataList.DataBind();

				if (Discussable == null)
					ReviewsArchiveAnchor.HRef = Archive.GetUrl(ts[0].DateTime.Year, ts[0].DateTime.Month, 0, ArchiveObjectType.Review, new string[] { }, "");
				else if (Discussable.UsedDiscussable is IHasArchive && ts.Count == Items)
					ReviewsArchiveAnchor.HRef = ((IHasArchive)Discussable.UsedDiscussable).UrlArchiveDate(ts[0].DateTime.Year, ts[0].DateTime.Month, 0, ArchiveObjectType.Review);
				else
				{
					ReviewsArchiveDiv.Visible = false;
					ReviewsPanelInner.Attributes["class"] = "LatestPanel Big CleanLinks";
				}
			}
		}
		#endregion

		#region GalleriesPanel
		protected Panel GalleriesPanel;
		protected DataList GalleriesDataList;
		protected HtmlAnchor GalleriesArchiveAnchor;
		protected HtmlGenericControl GalleriesArchiveDiv;
		public void GalleriesPanel_Load(object o, System.EventArgs e)
		{
			if (OnlyShowThreads)
			{
				GalleriesPanel.Visible = false;
				return;
			}
			if (Discussable != null && Discussable.UsedDiscussable is Event)
			{
				GalleriesPanel.Visible = false;
				return;
			}
			int galleryCount = ((Items / 4) + 1) * 4;
			Query q = new Query();
			Q RelevanceQ = new Q(false);
			if (Discussable == null)
				RelevanceQ = new Q(true);
			else if (Discussable.UsedDiscussable is Country)
				RelevanceQ = new Q(Place.Columns.CountryK, Discussable.UsedDiscussable.K);
			else if (Discussable.UsedDiscussable is Place)
				RelevanceQ = new Q(Place.Columns.K, Discussable.UsedDiscussable.K);
			else if (Discussable.UsedDiscussable is Venue)
				RelevanceQ = new Q(Venue.Columns.K, Discussable.UsedDiscussable.K);
			else if (Discussable.UsedDiscussable is Brand)
			{
				RelevanceQ = new Q(Brand.Columns.K, Discussable.UsedDiscussable.K);
			}
			else if (Discussable.UsedDiscussable is Group)
				RelevanceQ = new Q(Group.Columns.K, Discussable.UsedDiscussable.K);



			if (Discussable != null && Discussable.UsedDiscussable is Brand)
				q.TableElement = new Join(
					new TableElement(TablesEnum.Gallery),
					Event.BrandJoin,
					QueryJoinType.Inner,
					Gallery.Columns.EventK,
					Event.Columns.K);
			else if (Discussable != null && Discussable.UsedDiscussable is Group)
				q.TableElement = new Join(
					new TableElement(TablesEnum.Gallery),
					Event.GroupJoin,
					QueryJoinType.Inner,
					Gallery.Columns.EventK,
					Event.Columns.K);
			else
			{
				q.TableElement = new TableElement(TablesEnum.Gallery);
			}

			q.TableElement = Templates.Galleries.Default.PerformJoins(q.TableElement, Discussable != null && (Discussable.UsedDiscussable is Group || Discussable.UsedDiscussable is Brand));
			q.Columns = Templates.Galleries.Default.Columns;

			q.QueryCondition = new And(
				new Or(
				new Q(Gallery.Columns.ArticleK, 0),
				new Q(Gallery.Columns.ArticleK, QueryOperator.IsNull, null)
				),
				Gallery.ShowOnSiteQ,
				RelevanceQ
			);
			q.OrderBy = new OrderBy(Gallery.Columns.LastLiveDateTime, OrderBy.OrderDirection.Descending);
			q.TopRecords = galleryCount;

			GallerySet gs = new GallerySet(q);

			if (gs.Count == 0)
				GalleriesPanel.Visible = false;
			else
			{
				GalleriesDataList.DataSource = gs;
				GalleriesDataList.ItemTemplate = this.LoadTemplate("/Templates/Galleries/Default.ascx");
				GalleriesDataList.DataBind();


				if (Discussable == null)
					GalleriesArchiveAnchor.HRef = Archive.GetUrl(gs[0].CreateDateTime.Year, gs[0].CreateDateTime.Month, 0, ArchiveObjectType.Gallery, new string[] { }, "");
				else if (Discussable.UsedDiscussable is IHasArchive && gs.Count == galleryCount)
					GalleriesArchiveAnchor.HRef = ((IHasArchive)Discussable.UsedDiscussable).UrlArchiveDate(gs[0].CreateDateTime.Year, gs[0].CreateDateTime.Month, 0, ArchiveObjectType.Gallery);
				else
					GalleriesArchiveDiv.Visible = false;
			}
		}
		#endregion

		#region OnlyShowThreads
		public bool OnlyShowThreads
		{
			get
			{
				return Discussable != null && (Discussable.ObjectType == Model.Entities.ObjectType.Photo || Discussable.ObjectType == Model.Entities.ObjectType.Article);
			}
		}
		#endregion

		#region GroupsPanel
		private void GroupsPanel_Load(object o, EventArgs e)
		{
			if (!(Discussable == null
				|| Discussable.ObjectType == Model.Entities.ObjectType.Country
				|| Discussable.ObjectType == Model.Entities.ObjectType.Place))
			{
				GroupsPanel.Visible = false;
				return;
			}

			Q RelevanceQ = new Q(true);
			if (Discussable != null && Discussable.UsedDiscussable is Country)
				RelevanceQ = new Q(Group.Columns.CountryK, Discussable.UsedDiscussable.K);
			else if (Discussable != null && Discussable.UsedDiscussable is Place)
				RelevanceQ = new Q(Group.Columns.PlaceK, Discussable.UsedDiscussable.K);

			Query q = new Query();
			q.Columns = Templates.Groups.Latest.Columns;

			q.QueryCondition = new And(
				new Or(
					new Q(Group.Columns.TotalComments, QueryOperator.GreaterThan, 20),
					new Q(Group.Columns.TotalMembers, QueryOperator.GreaterThan, 10)),
				RelevanceQ,
				new Q(Group.Columns.BrandK, 0),
				new Q(Group.Columns.Pic, QueryOperator.NotEqualTo, Guid.Empty),
				new Q(Group.Columns.PrivateGroupPage, false)
			);
			q.OrderBy = new OrderBy(OrderBy.OrderDirection.Random);
			q.TopRecords = Items;

			GroupSet gsGroup = new GroupSet(q);

			if (gsGroup.Count == 0)
				GroupsPanel.Visible = false;
			else
			{
				GroupsDataList.DataSource = gsGroup;
				GroupsDataList.ItemTemplate = this.LoadTemplate("/Templates/Groups/Latest.ascx");
				GroupsDataList.DataBind();
				if (Discussable == null)
					GroupsAnchor.HRef = "/groups";
				else if (Discussable.UsedDiscussable is IObjectPage && gsGroup.Count == Items)
					GroupsAnchor.HRef = ((IObjectPage)Discussable.UsedDiscussable).UrlApp("groups");
				else
				{
					GroupsDiv.Visible = false;
					GroupsPanelInner.Attributes["class"] = "LatestPanel Big CleanLinks";
				}
			}

		}
		#endregion

		#region Parent
		object Parent
		{
			get
			{
				if (NamingContainer is Latest)
					return ((Latest)NamingContainer).Parent;
				else 
					return null;
			}
		}
		#endregion

		#region Discussable
		IDiscussable Discussable
		{
			get
			{
				if (Parent != null && Parent is IDiscussable)
					return (IDiscussable)Parent;
				else
					return null;
			}
		}
		#endregion

		#region EventsPanel
		private void EventsPanel_Load(object o, EventArgs e)
		{

			
		}
		#endregion
		
		#region ContaierPage
		Master.DsiPage ContaierPage
		{
			get
			{
				return (Master.DsiPage)Page;
			}
		}
		#endregion
		#region HasContent
		public bool HasContent
		{
			get
			{
				return GroupsPanel.Visible ||
					CompPanel.Visible ||
					ReviewsPanel.Visible ||
					GalleriesPanel.Visible ||
					NewsPanel.Visible ||
					ArticlesPanel.Visible;
			}
		}
		#endregion
		#region Items
		public int Items
		{
			get
			{
				if (NamingContainer is Latest)
					return ((Latest)NamingContainer).Items;
				else
					return 5;
			}
		}
		#endregion
 
		#region Page_PreRender
		protected PlaceHolder LeftPh, RightPh;
		private void Page_PreRender(object sender, System.EventArgs e)
		{
			this.Visible = HasContent;

			double LeftUnits = 0;
			double RightUnits = 0;

			PlaceIt(ref LeftUnits, ref RightUnits, NewsPanel, NewsDataList, 1.0, Items);
			PlaceIt(ref LeftUnits, ref RightUnits, CompPanel, CompDataList, 1.0, Items);
			PlaceIt(ref LeftUnits, ref RightUnits, ReviewsPanel, ReviewsDataList, 1.0, Items);
			PlaceIt(ref LeftUnits, ref RightUnits, GroupsPanel, GroupsDataList, 1.0, Items);


		}
		void PlaceIt(ref double LeftUnits, ref double RightUnits, Panel Content, DataList ItemsDataList, double ItemSize, int MaxCount)
		{
			if (LeftUnits > RightUnits)
			{
				//put it on the right
				ContentPanel.Controls.Remove(Content);
				RightPh.Controls.Add(Content);
				if (ItemsDataList.Items.Count > 0)
				{
					RightUnits += 1.0;
					//RightUnits += ItemsDataList.Items.Count * ItemSize; //items
					//RightUnits += 0.5; //header

					//if (ItemsDataList.Items.Count == MaxCount)
					//    RightUnits += 0.5; //footer
					//else
					//    RightUnits += 0.25; //no footer
				}

			}
			else
			{
				//put it on the left
				//put it on the right
				ContentPanel.Controls.Remove(Content);
				LeftPh.Controls.Add(Content);
				if (ItemsDataList.Items.Count > 0)
				{
					LeftUnits += 1.0;
					//LeftUnits += ItemsDataList.Items.Count * ItemSize; //items
					//LeftUnits += 0.5; //header

					//if (ItemsDataList.Items.Count == MaxCount)
					//    LeftUnits += 0.5; //footer
					//else
					//    LeftUnits += 0.25; //no footer
				}

			}
		}
		#endregion

		 
	}
}
