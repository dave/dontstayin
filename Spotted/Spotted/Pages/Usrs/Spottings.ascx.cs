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

namespace Spotted.Pages.Usrs
{
	public partial class Spottings : UsrUserControl
	{
		protected override void Page_Init(object sender, System.EventArgs e)
		{
			base.Page_Init(sender, e);
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if (Mode.Equals(Modes.Spottings))
					ChangePanel(PanelSpottings);
			}
		}
		#region PanelSpottings
		protected Panel PanelSpottings;
		protected HtmlImage SpotterIcon;
		protected DataList SpottingsDataList;
		protected HtmlAnchor NoRecordsNewAnchor;
		protected PlaceHolder ListLinks, ListOrder, ListPageLinks;
		protected HtmlGenericControl NoRecordsP, DataListP, ListPageLinksP;
		protected Spotted.CustomControls.UsrIntro UsrIntro;
		private void PanelSpottings_Load(object sender, System.EventArgs e)
		{
			if (Mode.Equals(Modes.Spottings))
			{
				SpotterIcon.Src = ThisUsr.SpotterIconPath;

				int totalItems = ThisUsr.SpottingsTotal;

				ContainerPage.SetPageTitle("People spotted by "+ThisUsr.NickName);
				UsrIntro.Header = "People spotted by " + ThisUsr.NickName;
				
				int recordsPerPage = 36;
				
				Query q = new Query();
				q.Columns = Usr.LinkColumns;
				
				if (ListNew)
					q.OrderBy = new OrderBy(Usr.Columns.DateTimeSignUp, OrderBy.OrderDirection.Descending);
				else
					q.OrderBy = new OrderBy(Usr.Columns.NickName);

				q.TableElement = new Join(
					Usr.Columns.K,
					UsrPhotoMe.Columns.UsrK);
				q.TableElement = new Join(
					q.TableElement,
					new TableElement(TablesEnum.Photo),
					QueryJoinType.Inner,
					new And(new Q(UsrPhotoMe.Columns.PhotoK, Photo.Columns.K, true), new Q(Photo.Columns.UsrK, ThisUsr.K))
				);
				
				Q nickNameQ = null;
				if (ListFirstLetter != null)
					nickNameQ = new Q(Usr.Columns.NickName, QueryOperator.TextStartsWith, ListFirstLetter);

				q.QueryCondition = new And(
					new Q(Usr.Columns.IsEmailVerified, true),
					new Q(Usr.Columns.IsSkeleton, false),
					nickNameQ
				);
				q.Distinct = true;
				q.DistinctColumn = Usr.Columns.K;

				q.Paging.RecordsPerPage = recordsPerPage;
				q.Paging.RequestedPage = ListPage;

				UsrSet us = new UsrSet(q);

				if (ListPage != us.Paging.ReturnedPage)
					ListPage = us.Paging.ReturnedPage;

				SpottingsDataList.DataSource = us;
				SpottingsDataList.ItemTemplate = this.LoadTemplate("/Templates/Usrs/Spottings.ascx");
				SpottingsDataList.DataBind();

				DataListP.Visible = us.Count > 0;
				NoRecordsP.Visible = us.Count == 0;
				NoRecordsNewAnchor.HRef = ThisUsr.UrlApp("spottings");

				if (us.Paging.ShowNoLinks)
					ListPageLinksP.Visible = false;
				else
				{
					if (us.Paging.ShowPrevPageLink)
						ListPageLinks.Controls.Add(new LiteralControl("<a href=\"" + ThisUsr.UrlApp("spottings", "name", ListFirstLetter, "page", (ListPage < 3 ? "" : ((int)ListPage - 1).ToString()), "") + "\"><img src=\"/gfx/icon-back-12.png\" style=\"margin-right:3px;\" width=\"12\" height=\"21\" align=\"absmiddle\" border=\"0\">prev page</a> "));
					else
						ListPageLinks.Controls.Add(new LiteralControl("<small><img src=\"/gfx/icon-back-12.png\" style=\"margin-right:3px;\" width=\"12\" height=\"21\" align=\"absmiddle\" border=\"0\">prev page</small> "));

					ListPageLinks.Controls.Add(new LiteralControl(" ... "));

					if (us.Paging.ShowNextPageLink)
						ListPageLinks.Controls.Add(new LiteralControl("<a href=\"" + ThisUsr.UrlApp("spottings", "name", ListFirstLetter, "page", ((int)ListPage + 1).ToString(), "") + "\">next page<img src=\"/gfx/icon-forward-12.png\" style=\"margin-left:3px;\" width=\"12\" height=\"21\" align=\"absmiddle\" border=\"0\"></a>"));
					else
						ListPageLinks.Controls.Add(new LiteralControl("<small>next page<img src=\"/gfx/icon-forward-12.png\" style=\"margin-left:3px;\" width=\"12\" height=\"21\" align=\"absmiddle\" border=\"0\"></small>"));

				}
				if (ThisUsr.SpottingsTotal > recordsPerPage)
				{
					if (ListNew)
						ListOrder.Controls.Add(new LiteralControl("Order by date: <b>Newest members first</b>"));
					else
						ListOrder.Controls.Add(new LiteralControl("Order by date: <a href=\"" + ThisUsr.UrlApp("spottings") + "\">Newest members first</a>"));

					ListLinks.Controls.Add(new LiteralControl("Order by nickname: "));

					for (int i = 1; i <= 26; i++)
					{
						if (ListFirstLetter!=null && ListFirstLetter.Equals(((char)(64 + i)).ToString().ToLower()))
							ListLinks.Controls.Add(new LiteralControl("<b>" + ((char)(64 + i)).ToString() + "</b> "));
						else
							ListLinks.Controls.Add(new LiteralControl("<a href=\"" + ThisUsr.UrlApp("spottings", "name", ((char)(64 + i)).ToString().ToLower(), "") + "\">" + ((char)(64 + i)).ToString() + "</a> "));
					}
				}
			}
		}
		#region ListFirstLetter
		string ListFirstLetter
		{
			get
			{
				if (ContainerPage.Url["name"].Exists)
					return ContainerPage.Url["name"].ToString().ToLower();
				else
					return null;
			}
		}
		#endregion
		#region ListPage
		int ListPage
		{
			get
			{
				if (listPage == 0)
				{
					listPage = 1;
					if (ContainerPage.Url["page"].Exists && ContainerPage.Url["page"].IsInt)
						listPage = ContainerPage.Url["page"];
				}
				return listPage;
			}
			set
			{
				listPage = value;
			}
		}
		int listPage = 0;
		#endregion
		#region ListNew
		bool ListNew
		{
			get
			{
				return ListFirstLetter == null;
			}
		}
		#endregion
		#endregion

		#region PageMode
		Modes Mode
		{
			get
			{
				return Modes.Spottings;
			}
		}
		public enum Modes
		{
			None,
			Spottings
		}
		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelSpottings.Visible = p.Equals(PanelSpottings);
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
			this.Load += new System.EventHandler(this.PanelSpottings_Load);
		}
		#endregion
	}
}
