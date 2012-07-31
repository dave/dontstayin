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
	public partial class DateProfile : System.Web.UI.UserControl
	{
		protected HtmlImage Pic;
		protected Spotted.CustomControls.h1 NicknameH1;
		protected Label NicknameLabel, NicknameLabel1, UsrCommentsLabel, UsrSpottedLabel, UsrMusicTypesLabel, UsrPlaceVisitLabel;
		protected Label PersonalStatementLabel;
		protected HtmlGenericControl PhotosIFrame;
		public RadioButton RadioYes, RadioNo, RadioMaybe, RadioWrongSex;
		public Button BackButton;
		protected Panel PersonalStatementPanel, PhotosPanel;
		protected HtmlImage WebImg;
		#region UsrK
		public int UsrK
		{
			get
			{
				return usrK;
			}
			set
			{
				usrK = value;
			}
		}
		private int usrK;
		#endregion
		#region ThisUsr
		public Usr ThisUsr
		{
			get
			{
				if (thisUsr == null && UsrK > 0)
					thisUsr = new Usr(UsrK);
				return thisUsr;
			}
		}
		Usr thisUsr;
		#endregion
		private void Page_Load(object sender, System.EventArgs e) { }
		public void Bind()
		{
			RadioMaybe.Checked = false;
			RadioYes.Checked = false;
			RadioNo.Checked = false;
			RadioWrongSex.Checked = false;

			Pic.Src = ThisUsr.PicPath;
			NicknameH1.InnerHtml = ThisUsr.NickNameSafe;
			NicknameLabel.Text = ThisUsr.NickName;
			NicknameLabel1.Text = ThisUsr.NickName;

			Query commentCountQ = new Query();
			commentCountQ.TableElement = Comment.ThreadJoin;
			commentCountQ.QueryCondition = new And(new Q(Comment.Columns.UsrK, UsrK), new Q(Thread.Columns.Private, false));
			commentCountQ.ReturnCountOnly = true;
			CommentSet cs = new CommentSet(commentCountQ);
			UsrCommentsLabel.Text = cs.Count.ToString() + " comment" + (cs.Count == 1 ? "" : "s");

			int photosMeCount = ThisUsr.PhotosMeCount;
			if (photosMeCount == 0)
				UsrSpottedLabel.Text = "I've not been spotted yet";
			else if (photosMeCount == 1)
				UsrSpottedLabel.Text = "I've been spotted once";
			else
				UsrSpottedLabel.Text = "I've been spotted " + photosMeCount + " times";

			UsrMusicTypesLabel.Text = "";
			for (int i = 0; i < ThisUsr.MusicTypesFavourite.Count; i++)
			{
				UsrMusicTypesLabel.Text += (i == 0 ? "" : (i == ThisUsr.MusicTypesFavourite.Count - 1 ? " and " : ", ")) + ThisUsr.MusicTypesFavourite[i].GenericName;
			}

			UsrPlaceVisitLabel.Text = "";
			PlaceSet ps = ThisUsr.PlacesVisit(Place.NameColumns, 0);
			for (int i = 0; i < ps.Count; i++)
			{
				UsrPlaceVisitLabel.Text += (i == 0 ? "" : (i == ps.Count - 1 ? " and " : ", ")) + ps[i].Name;
			}

			if (ThisUsr.PersonalStatement.Length > 0)
			{
				string html = ThisUsr.PersonalStatement;
				if (html.IndexOf("<p>") == -1)
				{
					html = html.Replace("\n", "<br>");
					html = "<p>" + html + "</p>";
				}
				PersonalStatementLabel.Text = html;
			}
			else
			{
				PersonalStatementPanel.Visible = false;
				PersonalStatementLabel.Text = "";
			}

			PhotosIFrame.Attributes["src"] = "/popup/datephotolist/usrk-" + ThisUsr.K.ToString();

			if (Usr.Current.DateSexFemale && Usr.Current.DateSexMale)
				RadioWrongSex.Visible = false;
			else
			{
				RadioWrongSex.Text = RadioWrongSex.Text.Replace("?", Usr.Current.DateSexMale ? "boy" : "girl");
				RadioWrongSex.Attributes["onclick"] = "document.getElementById('WrongSexP').style.display=''";
			}

			try
			{
				UsrDate ud = new UsrDate(Usr.Current.K, ThisUsr.K);
				if (ud.Status.Equals(UsrDate.StatusEnum.Maybe))
					RadioMaybe.Checked = true;
				else if (ud.Status.Equals(UsrDate.StatusEnum.Yes))
					RadioYes.Checked = true;
				else if (ud.Status.Equals(UsrDate.StatusEnum.No))
					RadioNo.Checked = true;
			}
			catch { }

			Query q = new Query();
			q.OrderBy = new OrderBy(new OrderBy(Photo.Columns.WeightedSexyRating, OrderBy.OrderDirection.Descending), new OrderBy(Photo.Columns.WeightedCoolRating, OrderBy.OrderDirection.Descending));
			q.TableElement = Photo.UsrMeJoin;
			q.QueryCondition = new Q(Usr.Columns.K, UsrK);
			q.TopRecords = 1;
			PhotoSet psMe = new PhotoSet(q);
			if (psMe.Count == 1)
			{
				PhotosPanel.Visible = true;
				WebImg.Src = psMe[0].WebPath;
			}
			else
				PhotosPanel.Visible = false;
		}
		public event EventHandler Next;
		public event EventHandler Back;
		public void NextClick(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				if (Next != null)
					Next(this, new EventArgs());
			}
		}
		public void BackClick(object o, System.EventArgs e)
		{
			if (Back != null)
				Back(this, new EventArgs());
		}
		public void RadioVal(object o, ServerValidateEventArgs e)
		{
			e.IsValid = (RadioMaybe.Checked || RadioYes.Checked || RadioNo.Checked || RadioWrongSex.Checked);
		}

	}
}
