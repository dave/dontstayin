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

namespace Spotted.Pages.Promoters
{
	public partial class Intro : PromoterUserControl
	{
		#region Page_Init
		protected override void Page_Init(object sender, System.EventArgs e)
		{

		}
		#endregion

		#region Page_Load
		private void Page_Load(object sender, System.EventArgs e)
		{
		}
		#endregion

		#region Intro
		protected Panel PanelNoAccount, PanelPromoterList;
		protected Repeater PromoterRepeater;

		private void Intro_Load(object sender, System.EventArgs e)
		{
			if (Mode.Equals(Modes.Intro))
			{
				if (Usr.Current != null && Usr.Current.IsPromoter)
				{
					ChangePanel(PanelPromoterList);

					Usr.Current.PromotersClear();
					PromoterSet ps = Usr.Current.Promoters(new ColumnSet(Promoter.Columns.UrlName, Promoter.Columns.Name));
					if (ps.Count == 1)
					{
						Response.Redirect(ps[0].Url());
					}
					else
					{
						PromoterRepeater.DataSource = ps;
						PromoterRepeater.DataBind();
					}
				}
				else
				{
					ChangePanel(PanelNoAccount);
				}
			}
		}
		#endregion

		#region PageMode
		Modes Mode
		{
			get
			{
				return Modes.Intro;
			}
		}
		public enum Modes
		{
			Promoter,
			Intro
		}
		#endregion

		#region ChangePanel
		void ChangePanel(Panel p)
		{
			PanelNoAccount.Visible = p.Equals(PanelNoAccount);
			PanelPromoterList.Visible = p.Equals(PanelPromoterList);
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
			this.Load += new System.EventHandler(this.Intro_Load);
		}
		#endregion
	}
}
