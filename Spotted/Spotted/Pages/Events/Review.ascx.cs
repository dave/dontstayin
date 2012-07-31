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

namespace Spotted.Pages.Events
{
	public partial class Review : EventUserControl
	{
		protected TextBox SummaryTextBox, ReviewBody;
		protected Label StatusLabel;
		protected Panel DeleteReviewPanel, CantEditPanel, InfoPanel;
		protected Button DeleteButton;

		private void Page_Load(object sender, System.EventArgs e)
		{
			Usr.KickUserIfNotLoggedIn();

			if (CurrentEvent.DateTime > DateTime.Today)
			{
				CantEditPanel.Visible = true;
				InfoPanel.Visible = false;
			}
			
			DeleteButton.Attributes["onclick"] = "return (confirm('Are you sure?'));";
			if (!Page.IsPostBack)
			{
				if (CurrentThread != null)
				{
					SummaryTextBox.Text = CurrentThread.Subject;
					ReviewHtml.LoadHtml(CurrentThread.FirstComment.Text);
					DeleteReviewPanel.Visible = true;
				}
			}
		}
		protected Thread CurrentThread
		{
			get
			{
				if (currentThread == null)
				{
					ThreadSet ts = new ThreadSet(
						new Query(
							new And(
								new Q(Thread.Columns.UsrK, Usr.Current.K),
								new Q(Thread.Columns.ParentObjectType, Model.Entities.ObjectType.Event),
								new Q(Thread.Columns.ParentObjectK, CurrentEvent.K),
								new Q(Thread.Columns.IsReview, true)
							)
						)
					);
					if (ts.Count == 1)
					{
						currentThread = ts[0];
					}
					else if (ts.Count == 0)
						return null;
					else
						throw new Exception("Multiple event reviews!");

				}
				return currentThread;
			}
			set
			{
				currentThread = value;
			}
		}
		Thread currentThread;


		public void ReviewSave(object o, System.EventArgs e)
		{
			if (Page.IsValid)
			{
				if (SummaryTextBox.Text.Length > 50)
					throw new Exception("Summary too long");

				int threadK = 0;
				if (CurrentThread == null)
				{
					//save new review
					Thread.Maker m = new Thread.Maker();
					m.Subject = SummaryTextBox.Text;
					m.Body = ReviewHtml.GetHtml();
					m.ParentType = Model.Entities.ObjectType.Event;
					m.ParentK = CurrentEvent.K;
					m.DuplicateGuid = Guid.NewGuid();
					m.Review = true;
					m.PostingUsr = Usr.Current;
					Thread.MakerReturn r = m.Post();

					threadK = r.Thread.K;

				}
				else
				{

					CurrentThread.Subject = Cambro.Web.Helpers.StripHtml(SummaryTextBox.Text);
					CurrentThread.FirstComment.Text = ReviewHtml.GetHtml();
					CurrentThread.FirstComment.Update();
					CurrentThread.FirstComment.IsEdited = true;
					CurrentThread.FirstComment.EditDateTime = DateTime.Now;
					CurrentThread.Update();

					threadK = CurrentThread.K;
				}
				Usr.Current.AttendEvent(CurrentEvent.K, true, null, null);
				Thread t = new Thread(threadK);
				Response.Redirect(t.Url());
			}
		}
		public void DeleteClick(object o, System.EventArgs e)
		{
			if (CurrentThread != null)
			{
				Bobs.Delete.DeleteAll(CurrentThread);
				Response.Redirect(CurrentEvent.Url());
			}
		}
		#region CurrentEvent
		//public Event CurrentEvent
		//{
		//    get
		//    {
		//        return ContainerPage.Url.ObjectFilterEvent;
		//    }
		//}
		#endregion
	}
}
