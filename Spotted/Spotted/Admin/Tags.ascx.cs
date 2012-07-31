using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Bobs;
using Caching;

namespace Spotted.Admin
{
	public partial class Tags : AdminUserControl
	{
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			this.uiFilter.Click += (s, ev) => GetResults();
			this.uiSaveChanges.Click += new EventHandler(uiSaveChanges_Click);
			this.uiResults.RowDataBound += new GridViewRowEventHandler(uiResults_RowDataBound);
		}

		void uiResults_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				((CheckBox)e.Row.FindControl("uiBlockedCheckBox")).Attributes["onclick"] = "document.getElementById('" + this.uiSaveChanges.ClientID + "').disabled = 0";
				((CheckBox)e.Row.FindControl("uiShowInTagCloudCheckBox")).Attributes["onclick"] = "document.getElementById('" + this.uiSaveChanges.ClientID + "').disabled = 0";
			}
		}

		void uiSaveChanges_Click(object sender, EventArgs e)
		{

			foreach (GridViewRow row in uiResults.Rows)
			{
				int k = Convert.ToInt32(row.Cells[0].Text);
				Tag tag = new Tag(k);
				
				bool showInTagCloud = ((CheckBox)row.FindControl("uiShowInTagCloudCheckBox")).Checked;
				if (tag.ShowInTagCloud != showInTagCloud)
				{
					tag.ShowInTagCloud = showInTagCloud;
					tag.Update();
					(new Caching.CacheKeys.NamespaceCacheKey(CacheKeyPrefix.TagCloudVersion)).Invalidate();
				}
				bool blocked = ((CheckBox)row.FindControl("uiBlockedCheckBox")).Checked;
				if (tag.Blocked != blocked)
				{
					tag.SetBlockedAndUpdate(blocked);
				}
				
			}
			GetResults();
			
		}

		 

		private void GetResults()
		{
			Q tagTextQ = new Q(Tag.Columns.TagText, QueryOperator.TextContains, this.uiTagTextFilter.Text.Replace("*", "%"));
			Q isBlockedQ;
			switch (Convert.ToInt32(this.uiBlockedFilter.SelectedValue))
			{
				case -1: isBlockedQ = new Q(true); break;
				case 0: isBlockedQ = new Q(Tag.Columns.Blocked, false); break;
				case 1: isBlockedQ = new Q(Tag.Columns.Blocked, true); break;
				default: throw new NotImplementedException();
			}
			Q showInTagCloudQ;
			switch (Convert.ToInt32(this.uiShowInTagCloudFilter.SelectedValue))
			{
				case -1: showInTagCloudQ = new Q(true); break;
				case 0: showInTagCloudQ = new Q(Tag.Columns.ShowInTagCloud, false); break;
				case 1: showInTagCloudQ = new Q(Tag.Columns.ShowInTagCloud, true); break;
				default: throw new NotImplementedException();
			}
			TagSet set = new TagSet(new Query(new And(tagTextQ, isBlockedQ, showInTagCloudQ)));
			if (set.Count > 0)
			{
				this.uiResults.DataSource = set;
				this.uiResults.DataBind();
				this.uiResultsPanel.Visible = true;
				this.uiResultsTitle.Visible = true;
			}
			else
			{
				this.uiResultsPanel.Visible = false;
				this.uiResultsTitle.Visible = false;
			}
		}
	}
}
