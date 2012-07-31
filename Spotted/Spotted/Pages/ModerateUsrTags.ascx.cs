using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Bobs;
using System.Linq;
using System.Collections.Generic;

namespace Spotted.Pages
{
	public partial class ModerateUsrTags : DsiUserControl
	{
		#region ThisUsr
		private Usr thisUsr;
		public Usr ThisUsr
		{
			get
			{
				if (thisUsr == null)
				{
					thisUsr = new Usr(ContainerPage.Url["usr"].ValueInt);
				}
				return thisUsr;
			}
		}
		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Usr.Current == null || !Usr.Current.IsJunior)
			{
				throw new DsiUserFriendlyException("Only chat mods!");
			}
			uiInfo.Columns[3].Visible = Usr.Current.IsAdmin;
			ContainerPage.SetPageTitle("Moderate tags");
			Bind();
		}

		private void Bind()
		{
			List<TagPhotoHistory> tps = new List<TagPhotoHistory>(ThisUsr.ChildTagPhotoHistorys().AllItems());
			if (uiTypeOfAction.SelectedValue != "All actions")
			{
				tps = new List<TagPhotoHistory>(tps.Where(item => item.K == item.TagPhoto.MostRecentTagPhotoHistory.K));
			}
			//Query query = new Query(new Q(TagPhotoHistory.Columns.UsrK, usrK));

			//TagPhotoHistorySet tps = new TagPhotoHistorySet(query);
			if (tps.Count == 0)
			{
				uiNoTags.Visible = true;
				uiInfo.Visible = false;
			}
			else
			{
				uiNoTags.Visible = false;
				uiInfo.Visible = true;
				uiInfo.DataSource = tps;
				uiInfo.DataBind();
			}
		}

		protected void OnRowCommand(object o, GridViewCommandEventArgs e)
		{
			TagPhotoHistory tph = new TagPhotoHistory(int.Parse((string) e.CommandArgument));
			switch (e.CommandName)
			{
				case "Disable":
					{
						tph.TagPhoto.SetDisabledAndUpdate(true);
						Bind();
						break;
					}
				case "Enable":
					{
						tph.TagPhoto.SetDisabledAndUpdate(false);
						Bind();
						break;
					}
				case "Block":
					{
						
						tph.TagPhoto.Tag.SetBlockedAndUpdate(true);
						Bind();
						break;
					}
				case "Unblock":
					{
						tph.TagPhoto.Tag.SetBlockedAndUpdate(false);
						Bind();
						break;
					}
				default: throw new NotImplementedException();
			}
		}

		protected void OnRowDataBound(object o, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				LinkButton button = e.Row.FindControl("uiBlockTag") as LinkButton;
				if (button != null)
				{
					button.Attributes["onclick"] = "return confirm('Permanently block \"" + ((TagPhotoHistory)e.Row.DataItem).TagPhoto.Tag.TagText +
						"\" from the tagging system?');";
				}
			}
		}
	}
}
