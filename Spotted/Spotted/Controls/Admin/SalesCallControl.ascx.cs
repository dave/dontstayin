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

namespace Spotted.Controls.Admin
{
	public partial class SalesCallControl : System.Web.UI.UserControl
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			Cambro.Web.Helpers.TieButton(NoteTextBox, SaveNoteButton);
			if (CurrentSalesCall.Effective)
				EffectiveButton.Visible = false;

			if (!Page.IsPostBack)
			{
				if (CurrentSalesCall.Promoter.SalesNextCall > DateTime.Today)
					NextCallCal.Date = CurrentSalesCall.Promoter.SalesNextCall;
			}

			CallHeader.Text = "Sales call to ";
			if (CurrentSalesCall.Direction.Equals(SalesCall.Directions.Incoming))
				CallHeader.Text = "Incoming call from ";

			NoteTextBox.Attributes["onfocus"] = "if(this.value=='add a note here...')this.value='';";

			this.DataBind();
		}
		#region SaveNextCallClick
		protected void SaveNextCallClick(object sender, EventArgs eventArgs)
		{
			
			try
			{
				DateTime dt = NextCallCal.Date;
				if (dt < DateTime.Today)
				{
					SaveNextCallErrorLabel.Visible = true;
					return;
				}

				CurrentSalesCall.Note = CurrentSalesCall.Note + (CurrentSalesCall.Note.Length>0?"\n":"") + "Next call date changed to " + dt.ToShortDateString();
				CurrentSalesCall.Update();

				CurrentSalesCall.Promoter.ManualNote = "Next call date changed to " + dt.ToShortDateString() + " [" + Usr.Current.NickName + " " + DateTime.Now.ToShortDateString() + "]\n" + CurrentSalesCall.Promoter.ManualNote;
				CurrentSalesCall.Promoter.Update();

				CurrentSalesCall.Promoter.SalesHold = false;
				CurrentSalesCall.Promoter.SalesNextCall = dt;
				CurrentSalesCall.Promoter.Update();
				SaveNextCallDoneLabel.Visible = true;

			}
			catch
			{
				SaveNextCallErrorLabel.Visible=true;

			}

			if (Page is Master.DsiPage && ((Master.DsiPage)Page).ContentUserControl is Pages.Promoters.Home)
			{
				((Pages.Promoters.Home)((Master.DsiPage)Page).ContentUserControl).BindAdminPanel(true);
			}
			
			
		}
		#endregion
		#region SaveNoteClick
		protected void SaveNoteClick(object sender, EventArgs eventArgs)
		{
			if (Cambro.Web.Helpers.Strip(NoteTextBox.Text).Length > 0)
			{
				CurrentSalesCall.Note = CurrentSalesCall.Note + (CurrentSalesCall.Note.Length > 0 ? "\n" : "") + Cambro.Web.Helpers.Strip(NoteTextBox.Text);
				CurrentSalesCall.Update();

				CurrentSalesCall.Promoter.ManualNote = Cambro.Web.Helpers.Strip(NoteTextBox.Text) + " [" + Usr.Current.NickName + " " + DateTime.Now.ToShortDateString() + "]\n" + CurrentSalesCall.Promoter.ManualNote;
				CurrentSalesCall.Promoter.Update();

				if (Page is Master.DsiPage && ((Master.DsiPage)Page).ContentUserControl is Pages.Promoters.Home)
				{
					((Pages.Promoters.Home)((Master.DsiPage)Page).ContentUserControl).BindAdminPanel(true);
				}

				NoteTextBox.Text = "";
				NoteSavedLabel.Visible = true;
			}
		}
		#endregion
		#region EffectiveClick
		protected void EffectiveClick(object sender, EventArgs eventArgs)
		{
			CurrentSalesCall.Effective = true;
			CurrentSalesCall.Update();
			CurrentSalesCall.EffectiveAction();
			EffectiveButton.Visible = false;

			if (Page is Master.DsiPage && ((Master.DsiPage)Page).ContentUserControl is Pages.Promoters.Home)
			{
				((Pages.Promoters.Home)((Master.DsiPage)Page).ContentUserControl).BindAdminPanel(true);
			}

		}
		#endregion
		#region HangUpClick
		protected void HangUpClick(object sender, EventArgs eventArgs)
		{
			CurrentSalesCall.Dismissed = true;
			if (CurrentSalesCall.InProgress)
			{
				CurrentSalesCall.InProgress = false;
				CurrentSalesCall.DateTimeEnd = DateTime.Now;

				if (CurrentSalesCall.DateTimeEnd < CurrentSalesCall.DateTimeStart)
					CurrentSalesCall.DateTimeEnd = CurrentSalesCall.DateTimeStart;

				TimeSpan ts = CurrentSalesCall.DateTimeEnd - CurrentSalesCall.DateTimeStart;
				CurrentSalesCall.Duration = ts.TotalMinutes;
			}
			CurrentSalesCall.Update();
			this.Visible = false;

			if (Page is Master.DsiPage && ((Master.DsiPage)Page).ContentUserControl is Pages.Promoters.Home)
			{
				((Pages.Promoters.Home)((Master.DsiPage)Page).ContentUserControl).BindAdminPanel(true);
			}
		}
		#endregion
		protected void Page_Init(object sender, EventArgs e)
		{
			this.ID = "SalesCall" + CurrentSalesCall.K;
		}
		#region CurrentSalesCall
		public SalesCall CurrentSalesCall
		{
			get
			{
				return currentSalesCall;
			}
			set
			{
				currentSalesCall = value;
			}
		}
		SalesCall currentSalesCall;
		#endregion
	}

}
