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
	public partial class ModerateNews : DsiUserControl
	{
		protected Panel ItemsPanel;
		protected Repeater ItemsRepeater;
		protected HtmlGenericControl OutputP;
		protected DataGrid ModeratorsDataGrid;

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
					Query tsq = new Query();
					tsq.QueryCondition = new And(
						new Q(Thread.Columns.NewsStatus, Thread.NewsStatusEnum.Recommended),
						new Q(Thread.Columns.NewsModeratorUsrK, UsrK));
					tsq.TopRecords = 10;
					tsq.NoLock = true;
					ThreadSet ts = new ThreadSet(tsq);
					if (ts.Count == 0)
						ItemsPanel.Visible = false;
					else
					{
						ItemsRepeater.DataSource = ts;
						ItemsRepeater.ItemTemplate = this.LoadTemplate("/Templates/Threads/NewsAdmin.ascx");
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
			if (!Usr.Current.CanNewsModerator())
				throw new Exception("Only news mods!");
			ContainerPage.SetPageTitle("News moderation");
			Bind();

			ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "DbButtonInit", "DbButtonInit(" + Bobs.Vars.LanguageString + ");", true);
		}
	}
}
