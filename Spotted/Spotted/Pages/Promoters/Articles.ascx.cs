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

namespace Spotted.Pages.Promoters
{
	public partial class Articles : PromoterUserControl
	{
		#region Page_Init
		protected override void Page_Init(object sender, System.EventArgs e)
		{
			base.Page_Init(sender, e);
		}
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();

			if (!EnsureSecure)
				throw new DsiUserFriendlyException("You can't view this page!");


		}

		protected Spotted.CustomControls.PromoterIntro PromoterIntro;

		#region ArticlePanel
		protected Panel ArticlePanel, NoArticlePanel;
		protected HtmlAnchor ArticleAddLink;
		protected DataGrid ArticleDataGrid;
		public void ArticlePanel_Load(object o, System.EventArgs e)
		{
			if (EnsureSecure)
			{
				ArticleListBind();
			}
		}
		void ArticleListBind()
		{
			if (EnsureSecure)
			{
				Query q = new Query();

				q.TableElement = new TableElement(TablesEnum.Article);
				q.TableElement = new Join(q.TableElement, new TableElement(TablesEnum.Event), QueryJoinType.Left, Article.Columns.EventK, Event.Columns.K);
				q.TableElement = new Join(q.TableElement, new TableElement(TablesEnum.Venue), QueryJoinType.Left, Article.Columns.VenueK, Venue.Columns.K);
				q.TableElement = new Join(q.TableElement, new TableElement(TablesEnum.EventBrand), QueryJoinType.Left, Event.Columns.K, EventBrand.Columns.EventK);
				q.TableElement = new Join(q.TableElement, new TableElement(TablesEnum.Brand), QueryJoinType.Left, EventBrand.Columns.BrandK, Brand.Columns.K);

				//q.TableElement = new Join(new Join(Article.Columns.EventK, EventBrand.Columns.EventK), Brand.Columns.K, EventBrand.Columns.BrandK);

				q.QueryCondition = 
					new Or(
						new And(
							new Q(Brand.Columns.PromoterStatus, Brand.PromoterStatusEnum.Confirmed),
							new Q(Brand.Columns.PromoterK, CurrentPromoter.K)
						),
						new And(
							new Q(Venue.Columns.PromoterStatus, Venue.PromoterStatusEnum.Confirmed),
							new Q(Venue.Columns.PromoterK, CurrentPromoter.K)
						)
					);

				q.Distinct = true;
				q.DistinctColumn = Article.Columns.K;
	
				q.OrderBy = new OrderBy(Article.Columns.AddedDateTime, OrderBy.OrderDirection.Descending);

				ArticleSet ars = new ArticleSet(q);
				ArticlePanel.Visible = ars.Count > 0;
				NoArticlePanel.Visible = ars.Count == 0;
				if (ars.Count > 0)
				{
					ArticleDataGrid.AllowPaging = ars.Count > ArticleDataGrid.PageSize;
					ArticleDataGrid.DataSource = ars;
					ArticleDataGrid.DataBind();
				}
			}
		}
		public void ArticleDataGridChangePage(object o, DataGridPageChangedEventArgs e)
		{
			if (EnsureSecure)
			{
				ArticleDataGrid.CurrentPageIndex = e.NewPageIndex;
				ArticleListBind();
			}
		}
		#endregion

		#region EnsureSecure
		public bool EnsureSecure
		{
			get
			{
				if (!doneEnsureSecure)
				{
					ensureSecure = Usr.Current.IsAdmin || (Usr.Current.IsEnabledPromoter(CurrentPromoter.K));
					doneEnsureSecure = true;
				}
				return ensureSecure;
			}
			set
			{
				ensureSecure = value;
			}
		}
		private bool doneEnsureSecure = false;
		private bool ensureSecure;
		#endregion
		#region PageMode
		Modes Mode
		{
			get
			{
				if (ContainerPage.Url[0].Equals("Event"))
					return Modes.Event;
				else
					return Modes.None;
			}
		}
		public enum Modes
		{
			None,
			Event
		}
		#endregion
		#region ChangePanel
		void ChangePanel(Panel p)
		{
			//PanelEvent.Visible = p.Equals(PanelEvent);
		}
		#endregion

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
			this.Load += new System.EventHandler(this.ArticlePanel_Load);
		}
		#endregion
	}
}
