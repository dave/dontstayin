using System.DHTML;
using Sys.UI;
using Spotted.WebServices.Controls.CommentsDisplay;
using System;
using Login = SpottedScript.Controls.Navigation.Login.PageImplementation;
using ScriptSharpLibrary;

namespace SpottedScript.Controls.Html
{
	public class Controller
	{
		View view;
		internal DOMElement SaveButton
		{
			get { return view.SaveButton; }
		}
		internal string RawHtml
		{
			get { return view.HtmlTextBox.Value; }
		}
		internal bool Formatting
		{
			get { return view.AdvancedFormattingTrueRadio.Checked; }
		}
		internal void ClearHtml()
		{
			view.HtmlTextBox.Value = "";
		}
		public Controller(View view)
		{
			this.view = view;

			if (view.LinkUrlButton != null) // otherwise not logged in
			{
				DomEvent.AddHandler(view.LinkUrlButton, "click", new DomEventHandler(linkUrlButtonClicked));
				DomEvent.AddHandler(view.LinkUrlPanelBackButton, "click", new DomEventHandler(linkUrlBackButtonClicked));
				DomEvent.AddHandler(view.FlashSwfUrlButton, "click", new DomEventHandler(flashSwfUrlButtonClicked));
				DomEvent.AddHandler(view.FlashSwfUrlPanelBackButton, "click", new DomEventHandler(flashSwfUrlBackButtonClicked));
				DomEvent.AddHandler(view.VideoFlvButton, "click", new DomEventHandler(videoFlvUrlButtonClicked));
				DomEvent.AddHandler(view.VideoFlvPanelBackButton, "click", new DomEventHandler(videoFlvUrlBackButtonClicked));
				DomEvent.AddHandler(view.AdvancedTagsToggleButton, "click", new DomEventHandler(advancedTagsToggleButtonClicked));

				DomEvent.AddHandler(view.AdvancedParseNowButton, "click", new DomEventHandler(advancedParseNowButtonClicked));

				DomEvent.AddHandler(view.PreviewButton, "click", new DomEventHandler(previewButtonClicked));
				DomEvent.AddHandler(view.HidePreviewButton, "click", new DomEventHandler(hidePreviewButtonClicked));
			}
		}

		#region Link click / back
		void linkUrlButtonClicked(DomEvent e)
		{
			e.PreventDefault();
			setLinkPanelVisibility(true);
		}
		void linkUrlBackButtonClicked(DomEvent e)
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
		void flashSwfUrlButtonClicked(DomEvent e)
		{
			e.PreventDefault();
			setFlashSwfPanelVisibility(true);
		}
		void flashSwfUrlBackButtonClicked(DomEvent e)
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
		void videoFlvUrlButtonClicked(DomEvent e)
		{
			e.PreventDefault();
			setVideoFlvPanelVisibility(true);
		}
		void videoFlvUrlBackButtonClicked(DomEvent e)
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

		void advancedTagsToggleButtonClicked(DomEvent e)
		{
			e.PreventDefault();
			view.AdvancedTagsPanel.Style.Display = view.AdvancedTagsPanel.Style.Display == "none" ? "" : "none";
		}

		void advancedParseNowButtonClicked(DomEvent e)
		{
			e.PreventDefault();
			Misc.ShowWaitingCursor();
			Service.CleanHtml(view.HtmlTextBox.Value, cleanHtmlSuccess, null, null, -1);
		}
		void cleanHtmlSuccess(string cleanHtml, object userContext, string methodName)
		{
			Misc.HideWaitingCursor();
			view.HtmlTextBox.Value = cleanHtml;
		}

		#region preview
		void previewButtonClicked(DomEvent e)
		{
			e.PreventDefault();

			Login.WhenLoggedIn(
				new Action(
					delegate()
					{
						Service.GetPreviewHtml(view.uiPreviewType.Value != "" ? int.ParseInvariant(view.uiPreviewType.Value) : 0, view.HtmlTextBox.Value, Formatting, getPreviewHtmlSuccess, null, null, -1);
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
		void hidePreviewButtonClicked(DomEvent e)
		{
			e.PreventDefault();
			view.PreviewButton.InnerHTML = "Preview";
			view.HidePreviewButton.Style.Display =  "none";
			view.PreviewPanelContainer.Style.Display = "none";
		}
		#endregion
	}
}
