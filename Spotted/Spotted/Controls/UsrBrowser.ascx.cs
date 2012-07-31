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

namespace Spotted.Controls
{
	public partial class UsrBrowser : UserControl
	{
		public string HeaderText{ get; set; }
		public string DescriptionText{ get; set; }
		public string NoRecordsDisplayedText { get; set; }
		public IPicHasIconObjectPage BrowsingObject { get; set; }
		public Q BaseQ { get; set; }
		public TableElement JoinToUsrTable { get; set; }
		public int TotalSetCount { get; set; }
		public OrderBy OrderByNewest { get; set; }
		public bool ShowRealNames { get; set; } // default false
		public bool AlwaysShowNicknames { get; set; } // default false
		public bool JoinToBuddyTable { get; set; } // default false


		private static readonly int recordsPerPage = 36;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack)
			{
				this.uiHeader.Set((IPicObjectPage)BrowsingObject);
				this.uiHeader.Header = HeaderText;
				this.uiDescriptionLabel.Text = DescriptionText;
				this.uiIconHtmlLiteral.Text = BrowsingObject.IconHtml();
			}
			if (TotalSetCount == 0)
			{
				return;
			}

			Query q = new Query();
			q.Columns = Usr.LinkColumns;
			if (ShowRealNames)
				q.Columns = new ColumnSet(q.Columns, Usr.Columns.FirstName, Usr.Columns.LastName);
			if (JoinToBuddyTable)
			{
				q.Columns = new ColumnSet(q.Columns, Buddy.Columns.SkeletonName, Usr.Columns.Email);
			}
			if (JoinToUsrTable != null)
			{
				q.TableElement = JoinToUsrTable;
			}
			
			if (UsrsListFirstLetter == null || TotalSetCount <= recordsPerPage)
				q.QueryCondition = BaseQ;
			else
				q.QueryCondition = new And(BaseQ, new Q(Usr.Columns.NickName, QueryOperator.TextStartsWith, UsrsListFirstLetter));

			if (OrderByNewest != null && UsrsListNew)
				q.OrderBy = OrderByNewest;
			else
				q.OrderBy = new OrderBy(Usr.Columns.NickName);

			q.Paging.RecordsPerPage = recordsPerPage;
			q.Paging.RequestedPage = UsrsListPage;

			UsrSet us = new UsrSet(q);

			if (UsrsListPage != us.Paging.ReturnedPage)
				UsrsListPage = us.Paging.ReturnedPage;

			uiUsrsDataList.DataSource = us;
			uiUsrsDataList.ItemTemplate = new UsrDisplay(ShowRealNames, AlwaysShowNicknames);
			uiUsrsDataList.DataBind();

			uiUsrsDataListP.Visible = us.Count > 0;
			if (us.Count == 0)
			{
				uiUsrsDataListNoRecordsLiteral.Text = "<p><small>" + NoRecordsDisplayedText + "</small></p>";
			}

			if (us.Paging.ShowNoLinks)
				uiUsrsListPageLinksP.Visible = false;
			else
			{
				if (us.Paging.ShowPrevPageLink)
					uiUsrsListPageLinks.Controls.Add(new LiteralControl("<a href=\"" + Url(UsrsListFirstLetter, UsrsListPage < 3 ? 0 : UsrsListPage - 1, UsrsListNew) + "\"><img src=\"/gfx/icon-back-12.png\" style=\"margin-right:3px;\" width=\"12\" height=\"21\" align=\"absmiddle\" border=\"0\">prev page</a> "));
				else
					uiUsrsListPageLinks.Controls.Add(new LiteralControl("<small><img src=\"/gfx/icon-back-12.png\" style=\"margin-right:3px;\" width=\"12\" height=\"21\" align=\"absmiddle\" border=\"0\">prev page</small> "));

				uiUsrsListPageLinks.Controls.Add(new LiteralControl(" ... "));

				if (us.Paging.ShowNextPageLink)
					uiUsrsListPageLinks.Controls.Add(new LiteralControl("<a href=\"" + Url(UsrsListFirstLetter, UsrsListPage > 0 ? UsrsListPage + 1 : 0, UsrsListNew) + "\">next page<img src=\"/gfx/icon-forward-12.png\" style=\"margin-left:3px;\" width=\"12\" height=\"21\" align=\"absmiddle\" border=\"0\"></a>"));
				else
					uiUsrsListPageLinks.Controls.Add(new LiteralControl("<small>next page<img src=\"/gfx/icon-forward-12.png\" style=\"margin-left:3px;\" width=\"12\" height=\"21\" align=\"absmiddle\" border=\"0\"></small>"));

			}

			if (TotalSetCount > recordsPerPage)
			{
				if (OrderByNewest != null)
				{
					if (UsrsListNew)
						uiUsrsListOrder.Controls.Add(new LiteralControl("Order by date: <b>Newest members first</b>"));
					else
						uiUsrsListOrder.Controls.Add(new LiteralControl("Order by date: <a href=\"" + Url(null, 0, true) + "\">Newest members first</a>"));
				}

				uiUsrsListLinks.Controls.Add(new LiteralControl("Order by nickname: "));

				for (int i = 1; i <= 26; i++)
				{
					char c = (char)(64 + i);
					if (UsrsListFirstLetter != null && UsrsListFirstLetter.Equals(c.ToString().ToLower()))
						uiUsrsListLinks.Controls.Add(new LiteralControl("<b>" + c.ToString() + "</b> "));
					else
						uiUsrsListLinks.Controls.Add(new LiteralControl("<a href=\"" + Url(c.ToString().ToLower(), 0, false) + "\">" + c.ToString() + "</a> "));
				}
				if (UsrsListFirstLetter == null && !UsrsListNew)
					uiUsrsListLinks.Controls.Add(new LiteralControl("<b>All</b> "));
				else
					uiUsrsListLinks.Controls.Add(new LiteralControl("<a href=\"" + Url(null, 0, false) + "\">All</a> "));
			}
		}


		#region Url interpreting
		private readonly string FilterLetter = "letter";
		private readonly string FilterPage = "page";
		private readonly string FilterNewest = "new";
		private Spotted.Master.DsiPage ContainerPage
		{
			get { return (Spotted.Master.DsiPage)Parent.Page; }
		}

		string UsrsListFirstLetter
		{
			get
			{
				if (ContainerPage.Url[FilterNewest].Exists) return null;
				if (!ContainerPage.Url[FilterLetter].Exists) return null;
				else return ContainerPage.Url[FilterLetter].Value;
			}
		}
		int UsrsListPage
		{
			get
			{
				if (usrsListPage == 0)
				{
					usrsListPage = ContainerPage.Url[FilterPage].ValueInt;
				}
				return usrsListPage;
			}
			set
			{
				usrsListPage = value;
			}
		}
		int usrsListPage = 0;
		bool UsrsListNew
		{
			get { return ContainerPage.Url[FilterNewest].Exists; }
		}

		public string Url(string letter, int page, bool newest)
		{
			return ContainerPage.Url.CurrentUrl(FilterLetter, letter, FilterPage, page > 0 ? (int?)page : null, FilterNewest, newest ? "" : null);
		}
		#endregion

		#region UsrDisplay
		class UsrDisplay : ITemplate
		{
			private bool ShowRealNames { get; set; }
			private bool AlwaysShowNicknames { get; set; }

			public UsrDisplay(bool showRealNames, bool alwaysShowNicknames)
			{
				this.ShowRealNames = showRealNames;
				this.AlwaysShowNicknames = alwaysShowNicknames;
			}

			void ITemplate.InstantiateIn(Control container)
			{
				Panel p = new Panel();
				p.DataBinding += new EventHandler(dataBind);
				container.Controls.Add(p);
			}

			private void dataBind(object sender, EventArgs e)
			{
				Panel p = sender as Panel;
				Usr u = (Usr)((DataListItem)p.NamingContainer).DataItem;
				if ((u.NickName ?? "") == "")
				{
					Image img = new Image();
					img.ImageUrl = u.AnyPicPath;
					img.Width = 50;
					img.Height = 50;
					img.Attributes["class"] = "BorderBlack All";
					img.ImageAlign = ImageAlign.Top;
					img.BorderWidth = 1;
					u.MakeRolloverNoPic(img);
					p.Controls.Add(img);
					p.Controls.Add(new LiteralControl("<br />"));
					
					Panel name = new Panel();
					name.Style.Add("margin-top", "4px");
					name.Attributes["class"] = "ForceNarrow";

					Label label = new Label()
						{
							Text = u.JoinedBuddy.SkeletonName == "" ? u.Email.TruncateWithDots(10) : u.JoinedBuddy.SkeletonName,
						};
					label.Attributes["onmouseover"] = "stt('" + u.Email + "');";
					label.Attributes["onmouseout"] = "htm();";

					
					name.Controls.Add(label);
					p.Controls.Add(name);
				}
				else
				{

					Image img = new Image();
					img.ImageUrl = u.AnyPicPath;
					img.Width = 50;
					img.Height = 50;
					img.Attributes["class"] = "BorderBlack All";
					img.ImageAlign = ImageAlign.Top;
					img.BorderWidth = 1;
					u.MakeRolloverNoPic(img);

					HyperLink imgLink = new HyperLink();
					imgLink.Controls.Add(img);
					imgLink.NavigateUrl = u.Url();
					imgLink.Enabled = (AlwaysShowNicknames || u.AllowLinkToProfile());
					p.Controls.Add(imgLink);

					p.Controls.Add(new LiteralControl("<br />"));

					Panel name = new Panel();
					name.Style.Add("margin-top", "4px");
					name.Attributes["class"] = "ForceNarrow";

					HyperLink nameLink = new HyperLink();
					nameLink.NavigateUrl = (AlwaysShowNicknames || u.AllowLinkToProfile()) ? u.Url() : "";
					nameLink.Text = AlwaysShowNicknames ? u.NickName : u.DisplayName(Usr.Current == null ? (Buddy.BuddyFindingMethod?)Buddy.BuddyFindingMethod.Nickname : null);
					name.Controls.Add(nameLink);

					if (ShowRealNames && u.AllowLinkToProfile())
					{
						HtmlGenericControl realName = new HtmlGenericControl("small");
						realName.InnerText = " - " + u.FullName;
						name.Controls.Add(realName);
					}

					p.Controls.Add(name);
				}
			}
		}
		#endregion

	}
}
