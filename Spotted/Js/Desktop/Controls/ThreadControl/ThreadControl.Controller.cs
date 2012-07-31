using System;
using System.Html;
using jQueryApi;
using Js.Library;
using Js.Controls.CommentsDisplay;
using LoginPageImplementation = Js.Controls.Login.PageImplementation;
//using ScriptSharpLibrary;
//using Spotted.WebServices.Controls;
//using Sys.WebForms;
//using CommentsDisplayController = SpottedScript.Controls.CommentsDisplay.Controller;
//using Login = SpottedScript.Controls.Navigation.Login.PageImplementation;
//using Spotted.WebServices.Controls.CommentsDisplay;
//using Utils;

namespace Js.Controls.ThreadControl
{
	public class Controller
	{
		private View view;

		public Js.Controls.CommentsDisplay.Controller uiCommentsDisplay { get { return view.uiCommentsDisplay; } }
		public EventHandler OnThreadCreated;
		public EventHandler OnCommentPosted;
		private string duplicateGuid;

		public int CurrentParentObjectK
		{
			get
			{
				try { return int.Parse(view.uiParentObjectK.Value); }
				catch { return 0; }
			}
			set
			{
				view.uiParentObjectK.Value = value.ToString();
			}
		}
		int CurrentParentObjectType
		{
			get
			{
				try { return int.Parse(view.uiParentObjectType.Value); }
				catch { return 0; }
			}
		}

		public Controller(View view)
		{
			this.view = view;
			view.CommentHtml.SaveButton.SetAttribute("onclick", "");
			view.CommentHtml.SaveButtonJ.Click(postCommentClick);
			createNewDuplicateGuid();

			if (view.AddThreadAdvancedCheckBox != null)
				view.AddThreadAdvancedCheckBoxJ.Click(advancedCheckBoxClicked);

			view.uiCommentsDisplay.threadCommentsProvider.OnCommentPosted = new EventHandler(commentPosted);
			view.uiCommentsDisplay.threadCommentsProvider.OnThreadCreated = new EventHandler(threadCreated);
			view.uiCommentsDisplay.OnCommentsDisplayed = commentsDisplayed;
		}

		private void commentsDisplayed(object o, EventArgs e)
		{
			view.CommentHtml.ClearHtml();
			try
			{
				if (view.uiMultiBuddyChooser != null)
					view.uiMultiBuddyChooser.Clear();
			}
			catch
			{
				// ignore - probably user not logged in.
			}
		}

		private void commentPosted(object o, EventArgs e)
		{
			if (OnCommentPosted != null)
				OnCommentPosted(null, e);
			createNewDuplicateGuid();
			view.CommentHtml.ClearHtml();
		}

		private void advancedCheckBoxClicked(jQueryEvent e)
		{
			// don't e.PreventDefault();
			LoginPageImplementation.WhenLoggedIn(
				new Action(
					delegate()
					{
						view.AddThreadAdvancedPanel.Style.Display = view.AddThreadAdvancedCheckBox.Checked ? "" : "none";
					}
				)
			);
		}
		private void createNewDuplicateGuid()
		{
			Service.GetNewGuid(getNewGuidSuccess, Trace.WebServiceFailure, null, -1);
		}
		private void getNewGuidSuccess(string guid, object userContext, string methodName)
		{
			duplicateGuid = guid;
		}

		private void threadCreated(object o, EventArgs e)
		{
			if (OnThreadCreated != null)
				OnThreadCreated(o, e);
		}

		private void postCommentClick(jQueryEvent e)
		{
			e.PreventDefault();

			LoginPageImplementation.WhenLoggedIn(
				new Action(
					delegate()
					{
						postCommentClickInner();
					}
				)
			);
		}
		private void postCommentClickInner()
		{

			

			string rawCommentHtml = view.CommentHtml.RawHtml;

			if (rawCommentHtml.Trim().Length == 0)
			{
				return;
			}

			Misc.ShowWaitingCursor();
			string[] inviteUsrOptions = view.uiMultiBuddyChooser.SelectedValues;

			// should threadcommentsprovider be taking care of this?
			if (!view.AddThreadAdvancedCheckBox.Checked || view.AddThreadPublicRadioButton.Checked || view.AddThreadNewPublicRadioButton.Checked)
			{
				if (view.uiCommentsDisplay.threadCommentsProvider.ThreadK == 0)
				{
					view.uiCommentsDisplay.threadCommentsProvider.CreatePublicThread(this.CurrentParentObjectType, this.CurrentParentObjectK, duplicateGuid, rawCommentHtml,
						view.CommentHtml.Formatting, view.AddThreadAdvancedCheckBox.Checked && view.AddThreadNewsCheckBox.Checked, inviteUsrOptions);
				}
				else if (view.AddThreadAdvancedCheckBox.Checked && view.AddThreadNewPublicRadioButton.Checked)
				{
					view.uiCommentsDisplay.threadCommentsProvider.CreateNewPublicThread(this.CurrentParentObjectType, this.CurrentParentObjectK, duplicateGuid,
						rawCommentHtml, view.CommentHtml.Formatting, view.AddThreadNewsCheckBox.Checked, inviteUsrOptions);
				}
				else
				{
					view.uiCommentsDisplay.threadCommentsProvider.CreateReply(this.CurrentParentObjectType, this.CurrentParentObjectK, view.uiCommentsDisplay.threadCommentsProvider.ThreadK, duplicateGuid,
						rawCommentHtml, view.CommentHtml.Formatting, view.uiCommentsDisplay.threadCommentsProvider.LastKnownCommentK, inviteUsrOptions);
				}

			}
			else if (view.AddThreadAdvancedCheckBox.Checked && view.AddThreadPrivateRadioButton.Checked)
			{
				Service.CreatePrivateThread(this.CurrentParentObjectType, this.CurrentParentObjectK, duplicateGuid, rawCommentHtml,
					view.CommentHtml.Formatting, inviteUsrOptions, view.AddThreadSealedCheckBox.Checked,
					createPrivateThreadSuccess, null, null, -1);
			}
			else if (view.AddThreadAdvancedCheckBox.Checked && view.AddThreadGroupRadioButton.Checked)
			{
				int groupK = int.Parse(view.AddThreadGroupDropDown.Value);
				Service.CreateNewThreadInGroup(groupK, this.CurrentParentObjectType, this.CurrentParentObjectK, duplicateGuid, rawCommentHtml,
					view.CommentHtml.Formatting, view.AddThreadNewsCheckBox.Checked, inviteUsrOptions, view.AddThreadGroupPrivateCheckBox.Checked,
					createNewThreadInGroupSuccess, null, null, -1);
			}
		}



		void createPrivateThreadSuccess(string newThreadUrl, object userContext, string methodName)
		{
			Misc.HideWaitingCursor();
			Misc.Redirect(newThreadUrl);
		}

		void createNewThreadInGroupSuccess(string newThreadUrl, object userContext, string methodName)
		{
			Misc.HideWaitingCursor();
			Misc.Redirect(newThreadUrl);
		}
	}

}
