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

namespace Spotted.Pages.Photos
{
	public partial class AddToGroup : DsiUserControl
	{
		protected HtmlImage PhotoImg;
		protected HtmlAnchor PhotoAnchor;
		protected Repeater GroupRepeater;
		protected Panel GroupPanel, RepeaterPanel;
		protected Label GroupLabel;
		protected CheckBox ShowCheckBox;
		protected TextBox CaptionTextBox;


		public void Page_Init(object o, System.EventArgs e)
		{
			if (CurrentPhoto != null)
				CurrentPhoto.AddRelevant(ContainerPage);
		}
		private void Page_Load(object sender, System.EventArgs e)
		{

			Usr.KickUserIfNotLoggedIn("You must be logged in to use this page.");
			this.SetPageTitle("Add this photo to the group homepage");
			PhotoImg.Src = CurrentPhoto.IconPath;
			PhotoAnchor.HRef = CurrentPhoto.Url();

			if (CurrentGroup == null)
			{
				GroupPanel.Visible = false;

				Query q = new Query();
				q.TableElement = new Join(
					new TableElement(TablesEnum.Group),
					new TableElement(TablesEnum.GroupUsr),
					QueryJoinType.Inner,
					new And(
					new Q(Bobs.Group.Columns.K, GroupUsr.Columns.GroupK, true),
					new Q(GroupUsr.Columns.Moderator, true),
					new Q(GroupUsr.Columns.UsrK, Usr.Current.K)));
				GroupSet gs = new GroupSet(q);
				if (gs.Count == 0)
					throw new Exception("No groups!");
				else if (gs.Count == 1)
					Response.Redirect(CurrentPhoto.UrlApp("addtogroup", "groupk", gs[0].K.ToString()));
				else
				{
					GroupRepeater.DataSource = gs;
					GroupRepeater.DataBind();
				}
			}
			else
			{
				RepeaterPanel.Visible = false;

				GroupUsr gu = CurrentGroup.GetGroupUsr(Usr.Current);
				if (gu == null || !gu.Moderator)
					throw new DsiUserFriendlyException("You can't add a photo to this group!");

				if (!Page.IsPostBack)
				{
					try
					{
						GroupPhoto gp = new GroupPhoto(CurrentGroup.K, CurrentPhoto.K);
						ShowCheckBox.Checked = gp.ShowOnFrontPage;
						CaptionTextBox.Text = gp.Caption;
					}
					catch
					{

					}

					CaptionCompetitionCheckBox.Visible = Usr.Current.IsAdmin && CurrentGroup.K == Vars.CompetitionGroupK;
					if (CaptionCompetitionCheckBox.Visible)
					{
						CaptionCompetitionCheckBox.Checked = CurrentPhoto.IsInCaptionCompetition;
					}
				}
			}


		}

		public void Update_Click(object o, System.EventArgs e)
		{
			GroupUsr gu = CurrentGroup.GetGroupUsr(Usr.Current);
			if (gu == null || !gu.Moderator)
				throw new DsiUserFriendlyException("You can't add a photo to this group!");

			string caption = Cambro.Misc.Utility.Snip(Cambro.Web.Helpers.Strip(CaptionTextBox.Text, true, true, true, true), 200);
			try
			{
				GroupPhoto gp = new GroupPhoto(CurrentGroup.K, CurrentPhoto.K);
				if (ShowCheckBox.Checked != gp.ShowOnFrontPage || !caption.Equals(gp.Caption))
				{
					if (ShowCheckBox.Checked != gp.ShowOnFrontPage)
					{
						gp.ShowOnFrontPage = ShowCheckBox.Checked;
						gp.DateTime = DateTime.Now;
					}
					gp.Caption = caption;
					gp.AddedByUsrK = Usr.Current.K;
					gp.Update();
				}
			}
			catch (BobNotFound)
			{
				if (ShowCheckBox.Checked)
				{
					GroupPhoto gp = new GroupPhoto();
					gp.GroupK = CurrentGroup.K;
					gp.PhotoK = CurrentPhoto.K;
					gp.Caption = caption;
					gp.DateTime = DateTime.Now;
					gp.AddedByUsrK = Usr.Current.K;
					gp.ShowOnFrontPage = true;
					gp.Update();

				}
			}

			if (CurrentGroup.K == Vars.CompetitionGroupK && CurrentPhoto.IsInCaptionCompetition != CaptionCompetitionCheckBox.Checked)
			{
				CurrentPhoto.IsInCaptionCompetition = CaptionCompetitionCheckBox.Checked;
				CurrentPhoto.Update();
			}


			Response.Redirect(CurrentGroup.Url());
		}

		#region CurrentGroup
		public Bobs.Group CurrentGroup
		{
			get
			{
				if (currentGroup == null && ContainerPage.Url["groupk"].IsInt)
					currentGroup = new Bobs.Group(ContainerPage.Url["groupk"]);
				return currentGroup;
			}
			set
			{
				currentGroup = value;
			}
		}
		private Bobs.Group currentGroup;
		#endregion

		#region CurrentPhoto
		public Photo CurrentPhoto
		{
			get
			{
				return ContainerPage.Url.ObjectFilterPhoto;
			}
		}
		#endregion

	}
}
