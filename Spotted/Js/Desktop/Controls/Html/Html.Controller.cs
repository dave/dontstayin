//using System.DHTML;
//using Sys.UI;
//using Spotted.WebServices.Controls.CommentsDisplay;
//using System;
//using Login = SpottedScript.Controls.Login.PageImplementation;
//using ScriptSharpLibrary;

using System;
using System.Collections.Generic;
using System.Html;
using jQueryApi;
using Js.Library;
using CommentsDisplayService = Js.Controls.CommentsDisplay.Service;
using LoginPageImplementation = Js.Controls.Login.PageImplementation;


namespace Js.Controls.Html
{
	public class Controller
	{
		View view;
		public Element SaveButton
		{
			get { return view.SaveButton; }
		}
		public jQueryObject SaveButtonJ
		{
			get { return view.SaveButtonJ; }
		}
		public string RawHtml
		{
			get { return view.HtmlTextBox.Value; }
		}
		public bool Formatting
		{
			get { return view.AdvancedFormattingTrueRadio.Checked; }
		}
		public void ClearHtml()
		{
			view.HtmlTextBox.Value = "";
		}
		public Controller(View view)
		{
			this.view = view;

			if (view.LinkUrlButton != null) // otherwise not logged in
			{
				view.LinkUrlButtonJ.Click(linkUrlButtonClicked);
				view.LinkUrlPanelBackButtonJ.Click(linkUrlBackButtonClicked);
				view.FlashSwfUrlButtonJ.Click(flashSwfUrlButtonClicked);
				view.FlashSwfUrlPanelBackButtonJ.Click(flashSwfUrlBackButtonClicked);
				view.VideoFlvButtonJ.Click(videoFlvUrlButtonClicked);
				view.VideoFlvPanelBackButtonJ.Click(videoFlvUrlBackButtonClicked);
				view.AdvancedTagsToggleButtonJ.Click(advancedTagsToggleButtonClicked);
				view.AdvancedParseNowButtonJ.Click(advancedParseNowButtonClicked);
				view.PreviewButtonJ.Click(previewButtonClicked);
				view.HidePreviewButtonJ.Click(hidePreviewButtonClicked);
			}
		}

		#region Link click / back
		void linkUrlButtonClicked(jQueryEvent e)
		{
			e.PreventDefault();
			setLinkPanelVisibility(true);
		}
		void linkUrlBackButtonClicked(jQueryEvent e)
		{
			e.PreventDefault();
			setLinkPanelVisibility(false);
		}
		void setLinkPanelVisibility(bool moreOptions)
		{
			view.LinkMainPanel.Style.Display = moreOptions ? "none" : "";
			view.LinkUrlPanel.Style.Display = moreOptions ? "" : "none";
		}
		#endregion
		#region FlashSwf click / back
		void flashSwfUrlButtonClicked(jQueryEvent e)
		{
			e.PreventDefault();
			setFlashSwfPanelVisibility(true);
		}
		void flashSwfUrlBackButtonClicked(jQueryEvent e)
		{
			e.PreventDefault();
			setFlashSwfPanelVisibility(false);
		}
		void setFlashSwfPanelVisibility(bool moreOptions)
		{
			view.FlashMainPanel.Style.Display = moreOptions ? "none" : "";
			view.FlashSwfUrlPanel.Style.Display = moreOptions ? "" : "none";
		}
		#endregion
		#region VideoFlv click / back
		void videoFlvUrlButtonClicked(jQueryEvent e)
		{
			e.PreventDefault();
			setVideoFlvPanelVisibility(true);
		}
		void videoFlvUrlBackButtonClicked(jQueryEvent e)
		{
			e.PreventDefault();
			setVideoFlvPanelVisibility(false);
		}
		void setVideoFlvPanelVisibility(bool moreOptions)
		{
			view.VideoMainPanel.Style.Display = moreOptions ? "none" : "";
			view.VideoFlvPanel.Style.Display = moreOptions ? "" : "none";
		}
		#endregion

		void advancedTagsToggleButtonClicked(jQueryEvent e)
		{
			e.PreventDefault();
			view.AdvancedTagsPanel.Style.Display = view.AdvancedTagsPanel.Style.Display == "none" ? "" : "none";
		}

		void advancedParseNowButtonClicked(jQueryEvent e)
		{
			e.PreventDefault();
			Misc.ShowWaitingCursor();
			CommentsDisplayService.CleanHtml(view.HtmlTextBox.Value, cleanHtmlSuccess, null, null, -1);
		}
		void cleanHtmlSuccess(string cleanHtml, object userContext, string methodName)
		{
			Misc.HideWaitingCursor();
			view.HtmlTextBox.Value = cleanHtml;
		}

		#region preview
		void previewButtonClicked(jQueryEvent e)
		{
			e.PreventDefault();

			LoginPageImplementation.WhenLoggedIn(
				new Action(
					delegate()
					{
						CommentsDisplayService.GetPreviewHtml(
							view.uiPreviewType.Value != "" ? int.Parse(view.uiPreviewType.Value) : 0, 
							view.HtmlTextBox.Value, 
							Formatting, 
							getPreviewHtmlSuccess, 
							null, 
							null,
							-1);
					}
				)
			);

			
		}
		void getPreviewHtmlSuccess(string[] htmlAndScript, object userContext, string methodName)
		{
			view.PreviewPanel.InnerHTML = htmlAndScript[0];
			Script.Eval(htmlAndScript[1]);
			view.HidePreviewButton.Style.Display = "";
			view.PreviewButton.InnerHTML = "Update preview";
			view.PreviewPanelContainer.Style.Display = "";
		}
		void hidePreviewButtonClicked(jQueryEvent e)
		{
			e.PreventDefault();
			view.PreviewButton.InnerHTML = "Preview";
			view.HidePreviewButton.Style.Display =  "none";
			view.PreviewPanelContainer.Style.Display = "none";
		}
		#endregion
	}
}
