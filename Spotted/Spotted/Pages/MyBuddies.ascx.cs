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
	public partial class MyBuddies : DsiUserControl
	{
		protected Panel PanelNoBuddies, PanelBuddies;
		protected Panel FullBuddyListPanel, HalfBuddyListPanel, ReverseHalfBuddyListPanel;
		protected DataList FullBuddyList, HalfBuddyList, ReverseHalfBuddyList;

		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn("You've got to log in to see your buddy list");

			#region BuddySets
			Query fullQ = new Query();
			fullQ.TableElement = new Join(Buddy.Columns.BuddyUsrK, Usr.Columns.K);
			fullQ.QueryCondition = new And(
				new Q(Buddy.Columns.UsrK, Usr.Current.K),
				new Q(Buddy.Columns.FullBuddy, true));
			fullQ.Columns = Templates.Usrs.Buddy.Columns;
			fullQ.OrderBy = new OrderBy(Usr.Columns.NickName);
			BuddySet FullBuddySet = new BuddySet(fullQ);

			Query halfQ = new Query();
			halfQ.TableElement = new Join(Buddy.Columns.BuddyUsrK, Usr.Columns.K);
			halfQ.QueryCondition = new And(
				new Q(Buddy.Columns.UsrK, Usr.Current.K),
				new Q(Buddy.Columns.FullBuddy, false));
			halfQ.Columns = Templates.Usrs.Buddy.Columns;
			halfQ.OrderBy = new OrderBy(Usr.Columns.NickName);
			BuddySet HalfBuddySet = new BuddySet(halfQ);

			Query reverseQ = new Query();
			reverseQ.TableElement = new Join(Buddy.Columns.UsrK, Usr.Columns.K);
			reverseQ.QueryCondition = new And(
				new Q(Buddy.Columns.BuddyUsrK, Usr.Current.K),
				new Q(Buddy.Columns.FullBuddy, false));
			reverseQ.Columns = Templates.Usrs.BuddyReverse.Columns;
			reverseQ.OrderBy = new OrderBy(Usr.Columns.NickName);
			BuddySet ReverseBuddySet = new BuddySet(reverseQ);

			#endregion

			if (FullBuddySet.Count == 0 && HalfBuddySet.Count == 0 && ReverseBuddySet.Count == 0)
				ChangePanel(PanelNoBuddies);
			else
			{
				ChangePanel(PanelBuddies);

				FullBuddyListPanel.Visible = (FullBuddySet.Count > 0);
				HalfBuddyListPanel.Visible = (HalfBuddySet.Count > 0);
				ReverseHalfBuddyListPanel.Visible = (ReverseBuddySet.Count > 0);


				if (FullBuddySet.Count > 0)
				{
					FullBuddyList.DataSource = FullBuddySet;
					FullBuddyList.ItemTemplate = this.LoadTemplate("/Templates/Usrs/Buddy.ascx");
					FullBuddyList.DataBind();
				}
				if (HalfBuddySet.Count > 0)
				{
					HalfBuddyList.DataSource = HalfBuddySet;
					HalfBuddyList.ItemTemplate = this.LoadTemplate("/Templates/Usrs/Buddy.ascx");
					HalfBuddyList.DataBind();
				}
				if (ReverseBuddySet.Count > 0)
				{
					ReverseHalfBuddyList.DataSource = ReverseBuddySet;
					ReverseHalfBuddyList.ItemTemplate = this.LoadTemplate("/Templates/Usrs/BuddyReverse.ascx");
					ReverseHalfBuddyList.DataBind();
				}
			}



		}

		void ChangePanel(Panel p)
		{
			PanelNoBuddies.Visible = p.Equals(PanelNoBuddies);
			PanelBuddies.Visible = p.Equals(PanelBuddies);

		}

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
		}
		#endregion
	}
}
