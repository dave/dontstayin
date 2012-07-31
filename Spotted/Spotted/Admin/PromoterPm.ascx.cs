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

namespace Spotted.Admin
{
	public partial class PromoterPm : AdminUserControl
	{
		protected Button SendCommentButton;
		protected TextBox MessageId;
		#region ClearViewData
		public void ClearViewDataAdmin(object o, System.EventArgs e)
		{
			ClearViewDataGeneric(true);
		}
		public void ClearViewData(object o, System.EventArgs e)
		{
			ClearViewDataGeneric(false);
		}

		protected void ClearViewDataGeneric(bool AdminOnly)
		{
			Query q = new Query();
			q.TableElement = new Join(
				new Join(ThreadUsr.Columns.ThreadK, Thread.Columns.K),
				new TableElement(TablesEnum.Promoter),
				QueryJoinType.Inner,
				Thread.Columns.K,
				Promoter.Columns.QuestionsThreadK);
			q.NoLock = true;
			ThreadUsrSet tus = new ThreadUsrSet(q);
			Cambro.Web.Helpers.WriteAlertHeader();
			int count = 0;
			foreach (ThreadUsr tu in tus)
			{
				bool doIt = true;
				if (AdminOnly) // if we're only removing this item from Admin's inboxes
				{
					//only make the change if the ThreadUsr is for an admin (leave the thread in the inbox for non-admins)
					doIt = (tu.UsrK == 1 || tu.UsrK == 2 || tu.UsrK == 4 || tu.UsrK == 78392);

					//leave this thread in the inbox if the last post was NOT made by an admin!
					if (!(tu.Thread.LastPostUsrK == 1 ||
						tu.Thread.LastPostUsrK == 2 ||
						tu.Thread.LastPostUsrK == 4 ||
						tu.Thread.LastPostUsrK == 78392))
						doIt = false;
				}

				if (doIt)
				{
					if (tu.IsInbox)
					{
						tu.ChangeStatus(ThreadUsr.StatusEnum.Archived, true);
					}
					tu.ViewDateTime = DateTime.Now;
					tu.ViewComments = tu.Thread.TotalComments;
					tu.ViewDateTimeLatest = DateTime.Now;
					tu.ViewCommentsLatest = tu.Thread.TotalComments;
					tu.Update();
					if (count % 100 == 0)
						Cambro.Web.Helpers.WriteAlert(count.ToString() + "/" + tus.Count + " - ThreadK=" + tu.ThreadK + " - UsrK=" + tu.UsrK, 1);
				}
				count++;
			}
			Cambro.Web.Helpers.WriteAlert("Done", 2);
			Cambro.Web.Helpers.WriteAlertFooter("/admin/promoterpm");
		}
		#endregion
		#region DaveIgnore
		public void DaveIgnore(object o, System.EventArgs e)
		{
			Usr Dave = new Usr(4);
			Cambro.Web.Helpers.WriteAlertHeader();

			Cambro.Web.Helpers.WriteAlert("Selecting...", 1);
			Query q = new Query();
			if (Vars.DevEnv)
				q.TopRecords = 50;
			PromoterSet bs = new PromoterSet(q);
			for (int count = 0; count < bs.Count; count++)
			{
				Promoter p = bs[count];

				// Do work here!
				try
				{
					Thread t = new Thread(p.QuestionsThreadK);

					ThreadUsr tu = t.GetThreadUsr(Dave);
					tu.ChangeStatus(ThreadUsr.StatusEnum.Ignore, DateTime.Now, true, true);
					if (count % 100 == 0)
						Cambro.Web.Helpers.WriteAlert("Done " + count + "/" + bs.Count + " - " + p.UrlName, 2);
				}
				catch
				{
					Cambro.Web.Helpers.WriteAlert("Done " + count + "/" + bs.Count + " - EXCEPTION! " + p.UrlName, 3);
				}

				bs.Kill(count);

			}
			Cambro.Web.Helpers.WriteAlert("Done!", 4);
			Cambro.Web.Helpers.WriteAlertFooter("/admin/promoterpm");
		}
		#endregion
		protected TextBox CommentTextBox;
		public void SendComment(object o, System.EventArgs e)
		{
			Cambro.Web.Helpers.WriteAlertHeader();
			Cambro.Web.Helpers.WriteAlert("Selecting...", 1);

			Query q = new Query();
			if (Vars.DevEnv)
				q.TopRecords=50;

			q.QueryCondition = new Or(
				new Q(Promoter.Columns.LastMessage, QueryOperator.IsNull, null),
				new Q(Promoter.Columns.LastMessage, QueryOperator.NotEqualTo, int.Parse(MessageId.Text))
			);

			q.OrderBy = new OrderBy(Promoter.Columns.K);


			PromoterSet bs = new PromoterSet(q);
			

			Cambro.Web.Helpers.WriteAlert("Done selecting...", 1);


			for (int count = 0; count < bs.Count; count++)
			{
				Promoter p = bs[count];

				Thread t = new Thread(p.QuestionsThreadK);

				Comment.Maker m = t.GetCommentMaker();
				m.Body = CommentTextBox.Text;
				m.DuplicateGuid = Guid.NewGuid();
				m.PostingUsr = Usr.Current;
				m.CurrentThreadUsr = t.GetThreadUsr(Usr.Current);
				m.RunAsync = false;
				m.Post(null);

				p.LastMessage = int.Parse(MessageId.Text);
				p.Update();

				if (count % 10 == 0)
					Cambro.Web.Helpers.WriteAlert(count.ToString() + "/" + bs.Count + " - (k=" + p.K + ")" + p.Name, 2);

				bs.Kill(count);

			}
			Cambro.Web.Helpers.WriteAlert("Done!", 3);
			Cambro.Web.Helpers.WriteAlertFooter("/admin/promoterpm");

			CommentTextBox.Text = "";
		}
		private void Page_Load(object sender, System.EventArgs e)
		{
			SendCommentButton.Attributes["onclick"] = "if (confirm('are you sure?')){return confirm('Are you SUPER SURE???');};";
		}

	}
}
