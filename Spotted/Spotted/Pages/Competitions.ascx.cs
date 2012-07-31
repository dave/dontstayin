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
	public partial class Competitions : DsiUserControl
	{
		protected PlaceHolder PrizesPh;
		protected Panel MoreInfoPanel, MoreInfoPanel1;
		protected Label MoreInfoLabel, MoreInfoLabel1;
		#region CurrentComp
		public Comp CurrentComp
		{
			get
			{
				if (currentComp == null && CompK > 0)
					currentComp = new Comp(CompK);
				return currentComp;
			}
			set
			{
				currentComp = value;
			}
		}
		private Comp currentComp;
		#endregion
		#region CompK
		public int CompK
		{
			get
			{
				if (ContainerPage.Url[0].IsInt)
					return ContainerPage.Url[0];
				else
					return 0;
			}
		}
		#endregion

		protected PlaceHolder HtmlOverridePh;

		protected Panel CurrentCompPanel, EnteredPanel, EntryPanel, FinishedPanel,
			WinnersPanel, NoWinnersPanel, YouAreAWinnerPanel;
		protected Label QuestionLabel, SelectedAnswerLabel,
			DateTimeCloseLabel, DateTimeCloseLabel1, CorrentAnswerLabel;
		protected PlaceHolder WinnersPh;
		protected LinkButton EnterLinkButton1, EnterLinkButton2, EnterLinkButton3;
		protected HtmlAnchor OwnerAnchor;
		protected PlaceHolder PromoterDetailsPh;
		public void CurrentComp_PreRender(object o, System.EventArgs e)
		{

			if (DateTime.Now > CurrentComp.DateTimeStart)
			{
				HtmlRenderer r = new HtmlRenderer();
				r.LoadHtml(CurrentComp.SponsorDetails);
				PromoterDetailsPh.Controls.Clear();
				PromoterDetailsPh.Controls.Add(new LiteralControl(r.Render(PromoterDetailsPh)));

				QuestionLabel.Text = CurrentComp.Question;
				if (Usr.Current != null && Usr.Current.HasEntered(CompK))
				{
					CompEntry entry = Usr.Current.CompEntry(CompK);
					SelectedAnswerLabel.Text = entry.AnswerText;
					DateTimeCloseLabel.Text = Cambro.Misc.Utility.FriendlyDate(CurrentComp.DateTimeClose, false);
					EnteredPanel.Visible = true;
				}
				else
					EnteredPanel.Visible = false;

				if ((Usr.Current == null || !Usr.Current.HasEntered(CompK)) && CurrentComp.Running)
				{
					EnterLinkButton1.Text = CurrentComp.Answer1;
					EnterLinkButton2.Text = CurrentComp.Answer2;
					EnterLinkButton3.Text = CurrentComp.Answer3;
					DateTimeCloseLabel1.Text = Cambro.Misc.Utility.FriendlyDate(CurrentComp.DateTimeClose, false);
					EntryPanel.Visible = true;
				}
				else
					EntryPanel.Visible = false;

				if (!CurrentComp.Running)
				{
					CorrentAnswerLabel.Text = CurrentComp.CorrectAnswerText;
					FinishedPanel.Visible = true;
					WinnersPanel.Visible = CurrentComp.WinnersPicked;
					NoWinnersPanel.Visible = !CurrentComp.WinnersPicked;
					if (CurrentComp.WinnersPicked)
					{
						WinnersPh.Controls.Clear();
						UsrSet winners = CurrentComp.UsrWinners;
						bool currentUsrWinner = false;
						for (int prize = 1; prize <= 3; prize++)
						{
							CompEntrySet ces = new CompEntrySet(
								new Query(
									new And(
										new Q(CompEntry.Columns.CompK, CurrentComp.K),
										new Q(CompEntry.Columns.Winner, true),
										new Q(CompEntry.Columns.Prize, prize)
									)
								)
							);
							if (ces.Count > 0)
							{
								bool first = true;

								string prizeString = "1st prize - " + CurrentComp.Prize;
								if (prize == 2)
									prizeString = "2nd prize - " + CurrentComp.Prize2;
								else if (prize == 3)
									prizeString = "Runners up prize - " + CurrentComp.Prize3;


								WinnersPh.Controls.Add(new LiteralControl("<p>" + prizeString + ":</p><p>"));

								foreach (CompEntry ce in ces)
								{
									if (Usr.Current != null && ce.Usr.K == Usr.Current.K)
										currentUsrWinner = true;
									if (!first && (Usr.Current == null || Usr.Current.K != CurrentComp.Owner.K))
										WinnersPh.Controls.Add(new LiteralControl(", "));
									HtmlAnchor a = new HtmlAnchor();

									a.InnerText = ce.Usr.NickName;
									if (Usr.Current != null && Usr.Current.K == CurrentComp.Owner.K)
									{
										if (ce.WinnerThreadK > 0)
											a.HRef = ce.WinnerThread.Url();
										else
										{
											ce.Usr.MakeRollover(a);
											a.HRef = ce.Usr.Url();
										}
									}
									else
									{
										ce.Usr.MakeRollover(a);
										a.HRef = ce.Usr.Url();
									}
									WinnersPh.Controls.Add(a);
									if (Usr.Current != null && Usr.Current.K == CurrentComp.Owner.K)
									{
										WinnersPh.Controls.Add(new LiteralControl(" (" + ce.Usr.FirstName + " " + ce.Usr.LastName + ")<br>"));
									}
									first = false;
								}

								WinnersPh.Controls.Add(new LiteralControl("</p>"));
							}
						}
						if (currentUsrWinner)
						{
							CurrentComp.Owner.MakeRollover(OwnerAnchor);
							OwnerAnchor.InnerText = CurrentComp.Owner.NickName;
							OwnerAnchor.HRef = CurrentComp.Owner.Url();

							YouAreAWinnerPanel.Visible = true;
						}
						else
						{
							YouAreAWinnerPanel.Visible = false;
						}
					}
				}
				else
					FinishedPanel.Visible = false;

			}

		}
		public void Enter(object o, CommandEventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();
			if (CurrentComp != null && !Usr.Current.HasEntered(CompK) && CurrentComp.Running)
			{
				int answer = int.Parse(e.CommandArgument.ToString());
				if (answer < 1 || answer > 3)
					throw new Exception("fdkjhdsflkjh");
				CompEntry ce = new CompEntry();
				ce.UsrK = Usr.Current.K;
				ce.Answer = answer;
				ce.CompK = CompK;
				ce.Update();
				CurrentComp.Entries++;
				CurrentComp.Update();
				//CurrentComp_PreRender(null, null);
			}
		}

		protected Panel NoCompPanel;
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (CurrentComp.IsHtmlOverride)
			{
				HtmlOverridePh.Controls.Clear();
				HtmlOverridePh.Controls.Add(new LiteralControl(CurrentComp.SponsorDetails));
				CompPanel.Visible = false;
				NoCompPanel.Visible = false;
				return;
			}

			if (!CurrentComp.Status.Equals(Comp.StatusEnum.Enabled) || DateTime.Now < CurrentComp.DateTimeStart)
			{
				NoCompPanel.Visible = true;
				CompPanel.Visible = false;
			}
			else
			{
				if (CurrentComp != null)
					this.SetPageTitle("Win " + CurrentComp.Name);
				else
					this.SetPageTitle("Competition");

				NoCompPanel.Visible = false;
				CompPanel.Visible = true;


				PrizesPh.Controls.Clear();
				PrizesPh.Controls.Add(new LiteralControl("<li>" + CurrentComp.Winners.ToString() + " x " + CurrentComp.Prize + "</li>"));
				if (CurrentComp.Winners2 > 0)
					PrizesPh.Controls.Add(new LiteralControl("<li>" + CurrentComp.Winners2.ToString() + " x " + CurrentComp.Prize2 + "</li>"));
				if (CurrentComp.Winners3 > 0)
					PrizesPh.Controls.Add(new LiteralControl("<li>" + CurrentComp.Winners3.ToString() + " x " + CurrentComp.Prize3 + "</li>"));

				MoreInfoPanel.Visible = !CurrentComp.LinkType.Equals(Comp.LinkTypes.None);
				MoreInfoPanel1.Visible = !CurrentComp.LinkType.Equals(Comp.LinkTypes.None);
				if (CurrentComp.LinkType.Equals(Comp.LinkTypes.Event))
				{
					MoreInfoLabel.Text = CurrentComp.Event.FriendlyHtml(true, true, true, true);
					MoreInfoLabel1.Text = CurrentComp.Event.FriendlyHtml(true, true, true, true);
				}
				else if (CurrentComp.LinkType.Equals(Comp.LinkTypes.Brand))
				{
					MoreInfoLabel.Text = "<a href=\"" + CurrentComp.Brand.Url() + "\">" + CurrentComp.Brand.Name + "</a>";
					MoreInfoLabel1.Text = "<a href=\"" + CurrentComp.Brand.Url() + "\">" + CurrentComp.Brand.Name + "</a>";
				}


				if (Usr.Current != null && Usr.Current.IsAdmin)
				{
					ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a href=\"http://old.dontstayin.com/login-" + Usr.Current.K + "- " + Usr.Current.LoginString + "/admin/comp?ID=" + CurrentComp.K + "\">Edit comp (Admin)</a></p>"));
					ContainerPage.Menu.Admin.AdminPanelOther.Controls.Add(new LiteralControl("<p><a href=\"" + CurrentComp.Promoter.UrlApp("competitions", "mode", "edit", "compk", CurrentComp.K.ToString()) + "\">Edit comp (Promoter)</a></p>"));

				}


				this.DataBind();
			}
		}

		#region CompPanel
		protected Panel CompPanel;
		public void Comp_Load(object o, System.EventArgs e)
		{
			if (CurrentComp != null)
			{

			}
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
			this.Load += new System.EventHandler(Comp_Load);
			this.PreRender += new System.EventHandler(CurrentComp_PreRender);
		}
		#endregion
	}
}
