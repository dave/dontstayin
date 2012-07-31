using System;
using ScriptSharpLibrary;
using SpottedScript.Controls.EventCreator;
using SpottedScript.Controls.VenueCreator;
using Sys.UI;
using EventCreatorController = SpottedScript.Controls.EventCreator.Controller;
namespace SpottedScript.Controls.EventGetter
{
	public class Controller
	{
		private readonly View view;
		public EventInfo EventInfo;
		private string emptyHtml;
		public Controller(View view)
		{
			this.view = view;
			DomEvent.AddHandler(view.uiEventDisplayDiv, "mousedown", OnMouseDown);
			DomEvent.AddHandler(view.uiEventDisplayDiv, "keydown", OnKeyDown);
			this.emptyHtml = view.uiEventDisplayDiv.InnerHTML;
		}


		private void OnKeyDown(DomEvent e)
		{
			if ((Key)e.KeyCode == Key.Backspace || (Key)e.KeyCode == Key.Del)
			{
				this.EventInfo = null;
				this.view.uiEventDisplayDiv.InnerHTML = "";
			}
		}

		private void OnMouseDown(DomEvent e)
		{
			EventCreatorController.Instance.ShowPopup(
				this.EventInfo == null ? null : this.EventInfo.date,
				this.EventInfo == null ? null : this.EventInfo.venueInfo,
				this.EventInfo == null ? null : this.EventInfo.name,
				OnEventCreated);
		}

		private void OnEventCreated(EventInfo eventInfo)
		{
			this.EventInfo = eventInfo;
			if (eventInfo != null)
			{
				this.view.uiEventDisplayDiv.InnerHTML = Suggestion.GetPicTitleDetailTemplateHtml(eventInfo.picPath, eventInfo.name, VenueInfo.NameWithPlace(eventInfo.venueInfo));
			}
			else
			{
				this.view.uiEventDisplayDiv.InnerHTML = emptyHtml;
			}
		}
	}
}
