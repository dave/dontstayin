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

namespace Spotted.Pages
{
	public partial class GroupBrowser : DsiUserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			if (!Page.IsPostBack)
			{
				if (Mode.Equals(Modes.None))
					ChangePanel(PanelGroups);
			}
		}

		#region PanelGroups
		protected Panel PanelGroups;
		protected DataList GroupsDataList;
		private void PanelGroups_Load(object sender, System.EventArgs e)
		{
			if (Mode.Equals(Modes.None))
			{
				BindGroupsDataList();
			}
		}
		void BindGroupsDataList()
		{
			Q musicTypeQ = null;
			if (ContainerPage.Url.HasMusicFilter)
				musicTypeQ = new Q(Group.Columns.MusicTypeK, ContainerPage.Url.MusicFilterK);

			Q themeQ = null;
			if (ContainerPage.Url.HasThemeFilter)
				themeQ = new Q(Group.Columns.ThemeK, ContainerPage.Url.ThemeFilterK);

			Q countryQ = null;
			if (ContainerPage.Url.HasCountryObjectFilter)
				countryQ = new Q(Group.Columns.CountryK, ContainerPage.Url.ObjectFilterK);

			Q placeQ = null;
			if (ContainerPage.Url.HasPlaceObjectFilter)
				placeQ = new Q(Group.Columns.PlaceK, ContainerPage.Url.ObjectFilterK);

			Query q = new Query();
			q.Columns = Templates.Groups.GroupBrowser.Columns;
			q.OrderBy = new OrderBy(
				new OrderBy(Group.Columns.FavouriteCount, OrderBy.OrderDirection.Descending),
				new OrderBy(Group.Columns.TotalComments, OrderBy.OrderDirection.Descending),
				new OrderBy(Group.Columns.TotalMembers, OrderBy.OrderDirection.Descending));
			q.QueryCondition = new And(
				new Q(Group.Columns.TotalMembers, QueryOperator.GreaterThan, 10),
				new Q(Group.Columns.PrivateGroupPage, false),
				new Q(Group.Columns.BrandK, 0),
				musicTypeQ,
				themeQ,
				countryQ,
				placeQ);

			GroupSet gs = new GroupSet(q);

			GroupsDataList.DataSource = gs;
			GroupsDataList.ItemTemplate = this.LoadTemplate("/Templates/Groups/GroupBrowser.ascx");
			GroupsDataList.DataBind();



		}
		#endregion

		#region CurrentUsr
		public Usr CurrentUsr
		{
			get
			{
				return ContainerPage.Url.ObjectFilterUsr;
			}
		}
		#endregion

		#region PageMode
		Modes Mode
		{
			get
			{
				if (ContainerPage.Url[0].Equals("xxx"))
					return Modes.XXX;
				else
					return Modes.None;
			}
		}
		public enum Modes
		{
			None,
			XXX
		}
		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelGroups.Visible = p.Equals(PanelGroups);
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
			this.Load += new System.EventHandler(this.PanelGroups_Load);
		}
		#endregion
	}
}
