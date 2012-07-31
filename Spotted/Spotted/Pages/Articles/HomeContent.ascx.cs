using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;
using Spotted.Master;

namespace Spotted.Pages.Articles
{
	public partial class HomeContent : System.Web.UI.UserControl
	{
		private void Page_Load(object sender, System.EventArgs e)
		{

			if (CurrentArticle == null)
				return;

			ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "DbButtonInit", "DbButtonInit(" + Bobs.Vars.LanguageString + ");", true);

			

			if (!CurrentArticle.CanView(Usr.Current))
				throw new DsiUserFriendlyException("Article not enabled");

			CurrentArticle.Views++;
			CurrentArticle.Update();

			TitleLabel.Text = CurrentArticle.Title;

			if (CurrentArticle.OverrideContents.Length == 0)
			{
				ContentPlaceHolder.Visible = false;
				ParaDataList.DataSource = CurrentArticle.GetParaInPage(PageIndex);
				ParaDataList.ItemTemplate = this.LoadTemplate("/Templates/Articles/ParaTemplate.ascx");
				ParaDataList.DataBind();
			}
			else
			{
				ParaDataList.Visible = false;
				ContentPlaceHolder.Controls.Clear();
				ContentPlaceHolder.Controls.Add(this.LoadControl(CurrentArticle.OverrideContents));
			}
			if (CurrentArticle.HideOwner)
				OwnerP.Visible = false;

			if (CurrentArticle.ParentObjectType.Equals(Model.Entities.ObjectType.Event))
			{
				EventAnchor.InnerText = CurrentArticle.ParentEvent.Name;
				EventAnchor.HRef = CurrentArticle.ParentEvent.Url();
				DateSpan.InnerText = CurrentArticle.ParentEvent.FriendlyDate(false);

				VenueAnchor.InnerText = CurrentArticle.ParentEvent.Venue.Name;
				VenueAnchor.HRef = CurrentArticle.ParentEvent.Venue.Url();

				PlaceAnchor.InnerText = CurrentArticle.ParentEvent.Venue.Place.Name;
				PlaceAnchor.HRef = CurrentArticle.ParentEvent.Venue.Place.Url();
			}
			else if (CurrentArticle.ParentObjectType.Equals(Model.Entities.ObjectType.Venue))
			{
				VenueAnchor.InnerText = CurrentArticle.ParentVenue.Name;
				VenueAnchor.HRef = CurrentArticle.ParentVenue.Url();

				PlaceAnchor.InnerText = CurrentArticle.ParentVenue.Place.Name;
				PlaceAnchor.HRef = CurrentArticle.ParentVenue.Place.Url();

				EventPlaceHolder.Visible = false;
				DatePlaceHolder.Visible = false;
			}
			else if (CurrentArticle.ParentObjectType.Equals(Model.Entities.ObjectType.Place))
			{
				PlaceAnchor.InnerText = CurrentArticle.ParentPlace.Name;
				PlaceAnchor.HRef = CurrentArticle.ParentPlace.Url();

				EventPlaceHolder.Visible = false;
				VenuePlaceHolder.Visible = false;
				DatePlaceHolder.Visible = false;
			}
			else
			{
				InfoP.Visible = false;
			}

			OwnerLink.HRef = CurrentArticle.Owner.Url();
			OwnerLink.InnerText = CurrentArticle.Owner.NickName;
			CurrentArticle.Owner.MakeRollover(OwnerLink);

			if (CurrentArticle.Views == 1)
				ViewsLabel.Text = " once";
			else
				ViewsLabel.Text = CurrentArticle.Views.ToString("#,##0") + " times";

	
			if (CurrentArticle.HasPic)
				Pic.Src = CurrentArticle.PicPath;

			PicInfoP.Visible = CurrentArticle.HasPic || InfoP.Visible;

			PagePTop.Visible = CurrentArticle.LastPage > 1;
			PagePBottom.Visible = CurrentArticle.LastPage > 1;
			if (CurrentArticle.LastPage > 1)
			{
				PagePTop.Controls.Clear();
				PagePTop.Controls.Add(new LiteralControl("Page "));
				for (int i = 1; i <= CurrentArticle.LastPage; i++)
				{
					if (i != PageIndex)
						PagePTop.Controls.Add(new LiteralControl("<a href=\"" + CurrentArticle.Url("P", i.ToString()) + "\">" + i.ToString() + "</a> "));
					else
						PagePTop.Controls.Add(new LiteralControl(i.ToString() + " "));
				}
				PagePBottom.Controls.Clear();
				if (PageIndex == 1)
					PagePBottom.Controls.Add(new LiteralControl("&lt;- Previous page "));
				else
					PagePBottom.Controls.Add(new LiteralControl("<a href=\"" + CurrentArticle.Url("P", ((int)(PageIndex - 1)).ToString()) + "\">&lt;- Previous page</a> "));

				PagePBottom.Controls.Add(new LiteralControl(" | " + PageIndex.ToString() + " of " + CurrentArticle.LastPage.ToString() + " | "));

				if (PageIndex == CurrentArticle.LastPage)
					PagePBottom.Controls.Add(new LiteralControl("Next page -&gt;"));
				else
					PagePBottom.Controls.Add(new LiteralControl("<a href=\"" + CurrentArticle.Url("P", ((int)(PageIndex + 1)).ToString()) + "\">Next page -&gt;</a> "));

			}

			NonMixmagP.Visible = !CurrentArticle.IsMixmagNews;
			MixmagP.Visible = CurrentArticle.IsMixmagNews;
		}
	

		#region CurrentArticle
		public Article CurrentArticle
		{
			get; set;
		}
		#endregion

		#region PageIndex
		public int PageIndex
		{
			get
			{
				if (((DsiPage)Page).Url["P"].IsInt)
					return ((DsiPage)Page).Url["P"];
				else
					return 1;
			}
		}
		#endregion
	}
}
