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

namespace Spotted.Pages
{
	public partial class ModeratePhotoTags : DsiUserControl
	{
		#region Photo
		private Photo photo;
		public Photo Photo
		{
			get
			{
				if (photo == null)
				{
					photo = new Photo(ContainerPage.Url["photo"].ValueInt);
				}
				return photo;
			}
		}
		#endregion

		protected void Page_Load(object sender, EventArgs e)
		{
			if (Usr.Current == null || !Usr.Current.IsJunior)
			{
				throw new DsiUserFriendlyException("Only chat mods!");
			}
			ContainerPage.SetPageTitle("Moderate tags");
			Bind();
		}

		private void Bind()
		{

			uiPhotoImg.Src = Photo.WebPath;
			uiPhotoImg.Width = Photo.WebWidth;
			uiPhotoImg.Height = Photo.WebHeight;

			TagPhotoSet tagPhotoSet = new TagPhotoSet(new Query(new Q(Bobs.TagPhoto.Columns.PhotoK, Photo.K)));


			if (tagPhotoSet.Count == 0)
			{
				uiNoTags.Visible = true;
				uiPhotoTags.Visible = false;
			}
			else
			{
				uiNoTags.Visible = false;
				uiPhotoTags.Visible = true;
				uiPhotoTags.DataSource = tagPhotoSet;
				uiPhotoTags.DataBind();
			}
		}

		protected void OnRowCommand(object o, GridViewCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "Disable":
					{
						new TagPhoto(int.Parse((string)e.CommandArgument)).SetDisabledAndUpdate(true);
						Bind();
						break;
					}
				case "Enable":
					{
						new TagPhoto(int.Parse((string)e.CommandArgument)).SetDisabledAndUpdate(false);
						Bind();
						break;
					}
				case "Block":
					{
						new Tag(int.Parse((string)e.CommandArgument)).SetBlockedAndUpdate(true);
						Bind();
						break;
					}
				case "Unblock":
					{
						new Tag(int.Parse((string)e.CommandArgument)).SetBlockedAndUpdate(false);
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
					button.Attributes["onclick"] = "return confirm('Permanently block \"" + ((TagPhoto)e.Row.DataItem).Tag.TagText +
						"\" from the tagging system?');";
				}
			}
		}
	}
}
