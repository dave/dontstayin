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

namespace Spotted.Pages
{
	public partial class CaptionCompetition : DsiUserControl
	{
		private string competitionTitle
		{
			get
			{
				return ViewState["title"] as string ??
				       (ViewState["title"] = new Group(Vars.CompetitionGroupK).Name + " caption competition") as string;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			PhotoSet ps = getCompetitionPhotos(4);
			if (ps.Count > 0)
			{
				uiPhotoDataList.ItemTemplate = this.LoadTemplate("/Templates/GroupPhotos/CompetitionPhotoIcon.ascx");
				uiPhotoDataList.DataSource = ps;
				uiPhotoDataList.DataBind();

				Photo photo = ps[0];
				if (!Url["k"].IsNull)
				{
					int photoK = Url["k"].ValueInt;
					Photo p1 = ps.First(p => p.K == photoK);
					if (p1 != null)
					{
						photo = p1;
					}
					else
					{
						// could still be an old competition photo in which case allow it.
						Photo p2 = new Photo(photoK);
						if (p2.ChildGroupPhotos().First(g => g.GroupK == Vars.CompetitionGroupK) != null)
						{
							photo = p2;
						}
					}
				}
				uiPhoto.Src = photo.WebPath;
				uiPhoto.Width = photo.WebWidth;
				uiPhoto.Height = photo.WebHeight;
				uiPhotoUrl.HRef = photo.Url();

				uiCommentsDisplay.ParentObject = photo;
				if (photo.Thread != null)
				{
					uiCommentsDisplay.CurrentThread = photo.Thread;
					this.ThreadK.Value = photo.Thread.K.ToString();
				}
				uiCommentsDisplay.DataBind();
				currentPhotoK = photo.K;
			}

			duplicateGuid = Guid.NewGuid().ToString();
			ContainerPage.Title = competitionTitle;

			if (Usr.Current == null)
			{
				uiCaptionText.Enabled = false;
				uiCaptionText.Text = "Please sign in to post a caption!";
				uiPost.Disabled = true;
			}
			else
                uiCaptionText.Focus();
		}

		private int currentPhotoK
		{
			get { return int.Parse(PhotoK.Value); }
			set { PhotoK.Value = value.ToString(); }
		}
		private string duplicateGuid
		{
			get { return ViewState["duplicateGuid"] as string; }
			set { ViewState["duplicateGuid"] = value; }
		}

		private PhotoSet getCompetitionPhotos()
		{
			return getCompetitionPhotos(-1);
		}
		private PhotoSet getCompetitionPhotos(int top)
		{
			Query q = new Query();
			q.Columns = new ColumnSet(
				Templates.GroupPhotos.Icon.Columns,
				Photo.Columns.Web,
				Photo.Columns.WebHeight,
				Photo.Columns.WebWidth,
				Photo.Columns.ThreadK);

			q.TableElement = new Bobs.Join(
				new TableElement(TablesEnum.Photo),
				new TableElement(TablesEnum.GroupPhoto),
				QueryJoinType.Inner,
				new And(
					new Q(Photo.Columns.K, GroupPhoto.Columns.PhotoK, true),
					new Q(GroupPhoto.Columns.GroupK, Vars.CompetitionGroupK),
					new Q(GroupPhoto.Columns.ShowOnFrontPage, true),
					new Q(Photo.Columns.IsInCaptionCompetition, true)
					));
			q.OrderBy = new OrderBy(GroupPhoto.Columns.DateTime, OrderBy.OrderDirection.Descending);
			if (top > 0) q.TopRecords = top;

			return new PhotoSet(q);
		}


		protected void PostCaption(object sender, EventArgs e)
		{
			Group g = new Group(Vars.CompetitionGroupK);
			GroupUsr gu = g.GetGroupUsr(Usr.Current);

			if (gu == null || !g.IsMember(gu))
			{
				//join the group
				g.Join(Usr.Current, gu);
			}

			string caption = this.uiCaptionText.Text;
			if (caption.StartsWith("\"") || caption.StartsWith("“"))
				caption = caption.Substring(1);

			if (caption.EndsWith("\"") || caption.EndsWith("”"))
				caption = caption.Substring(0, caption.Length - 1);

			Spotted.WebServices.Controls.CommentsDisplay.Service service =
				new Spotted.WebServices.Controls.CommentsDisplay.Service();

			int? threadK = new Photo(currentPhotoK).ThreadK;

			if (threadK > 0)
			{
				service.CreateReply((int)Model.Entities.ObjectType.Photo, currentPhotoK, threadK.Value,
					duplicateGuid, caption, false, int.MaxValue, new string[0]);

				this.uiCommentsDisplay.DataBind();
			}
			else
			{
				int newThreadK =
					service.CreatePublicThread((int) Model.Entities.ObjectType.Photo, currentPhotoK, duplicateGuid, caption, false,
					                           false, new string[0]).threadK;

				this.uiCommentsDisplay.CurrentThread = new Thread(newThreadK);
				this.uiCommentsDisplay.DataBind();
			}


			duplicateGuid = Guid.NewGuid().ToString();
			this.uiCaptionText.Text = "";
		}
	}
}
