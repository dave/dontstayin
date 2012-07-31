using System;
using System.DHTML;
using JQ;
using ScriptSharpLibrary;
using SpottedScript.Controls.VenueCreator;
using Spotted.WebServices.Controls.EventCreator;
using Sys.Serialization;
using Sys.UI;
using Utils;

namespace SpottedScript.Controls.EventCreator
{
	public delegate void EventInfoCallback(EventInfo eventInfo);
	public class Controller
	{
		private View view;
		private EventInfoCallback callback;
		public static Controller Instance;
		public DomEventHandler oldOnFocus;
		private readonly TransformSuggestions oldTransformReceivedSuggestions;
		private readonly KeyStringPairAction oldItemChosen;
		private EventInfo EventInfo;
		public Controller(View view)
		{
			if (Instance == null)
			{
				Instance = this;
				this.view = view;
			}
			this.oldOnFocus = this.view.uiEventName.OnFocus;
			this.view.uiEventName.OnFocus = OnEventNameFocus;
			this.oldTransformReceivedSuggestions = this.view.uiEventName.TransformReceivedSuggestions;
			this.view.uiEventName.TransformReceivedSuggestions = TransformReceivedSuggestions;
			this.oldItemChosen = this.view.uiEventName.ItemChosen;
			this.view.uiEventName.ItemChosen = ItemChosen;
			view.uiEventName.Parameters.Set("returnInfo", true);
			DomEvent.AddHandler(view.uiContainer, "keydown", OnContainerKeyDown);
			DomEvent.AddHandler(view.uiAdd, "click", OnAddClick);
			Misc.AddHoverText(view.uiSummary, "click to select an event");
		}

		private void OnAddClick(DomEvent e)
		{
			int[] brands = new int[]{};
			if (view.uiBrand.Value.Length > 0)
			{
				brands[0] = int.ParseInvariant(view.uiBrand.Value);
			}
			Service.AddEvent(
					view.uiCal.GetDate(),
					view.uiVenueGetter.GetVenue().k,
					view.uiEventName.Text,
					view.uiSummary.Value,
					brands,
					AddEventSuccess,
					Trace.WebServiceFailure,
					null,
					5000);
			e.PreventDefault();
		}

		 

	 

		private void OnContainerKeyDown(DomEvent e)
		{
			if (e.KeyCode == (int) Key.Esc)
			{
				EventChosen(null);
			}
		}

		private void ItemChosen(KeyStringPair suggestion)
		{
			if (suggestion.Value == "{addMethod:quick}")
			{
				JQueryAPI.JQuery(this.view.uiAddOptionsPanel).slideDown(delegate() { this.view.uiSummary.Focus();});
				this.view.uiEventName.Text = suggestion.Key;
			}
			else if (suggestion.Value == "{filloutFields}")
			{
				if (this.view.uiCal.GetDate() == null)
				{
					this.view.uiCal.Focus();
				}else
				{
					this.view.uiVenueGetter.Focus();
				}
				this.view.uiEventName.Text = "";
				this.view.uiEventName.Value = "";

			}
			//else if (suggestion.Value == "{addMethod:wizard}")
			//{
			//    Script.Literal("debugger");
			//    Window.Navigate("/pages/events/edit");
			//}
			else
			{
				this.EventChosen((EventInfo) JavaScriptSerializer.Deserialize(suggestion.Value));
			}
			//if (this.oldItemChosen != null) this.oldItemChosen(suggestion);
		}
	 

		private void EventChosen(EventInfo chosenEvent)
		{
			this.view.uiContainer.Style.Display = "none";
			callback(chosenEvent);
		}

		private void AddEventSuccess(EventInfo result, object userContext, string methodName)
		{
			EventChosen(result);
		}

		private Suggestion[] TransformReceivedSuggestions(Suggestion[] suggestions, int maxNumberOfItemsToGet)
		{
			if (view.uiVenueGetter.GetVenue() != null && view.uiCal.GetDate() != null && view.uiEventName.Value == "")
			{
				string eventString = this.view.uiEventName.Text + " @ " + view.uiVenueGetter.GetVenue().name + " on " + view.uiCal.GetDate().Format("ddd dd/MM/yyyy");
				Suggestion quickAdd = new Suggestion();
				quickAdd.html = Suggestion.GetPicTitleDetailTemplateHtml("@/gfx/icon40-eventcreator-add.png", "Add <b>" + eventString + "</b>", "You'll be able to add more details later if you like");
				quickAdd.value = "{addMethod:quick}";
				quickAdd.text = this.view.uiEventName.Text;
				Suggestion.AddSuggestion(suggestions, quickAdd, maxNumberOfItemsToGet);
			}
			else
			{
				Suggestion notInDbSuggestion = new Suggestion();
				notInDbSuggestion.html = Suggestion.GetPicTitleDetailTemplateHtml(@"/gfx/icon40-eventcreator-noadd.png",
				                                                                  "Can't add <b>" + this.view.uiEventName.Text +
				                                                                  "</b>", "Fill out all the fields");
				notInDbSuggestion.value = "{filloutFields}";
				notInDbSuggestion.text = this.view.uiEventName.Text;
				Suggestion.AddSuggestion(suggestions, notInDbSuggestion, maxNumberOfItemsToGet);
			}
			if (oldTransformReceivedSuggestions != null)
			{
				suggestions = oldTransformReceivedSuggestions(suggestions, maxNumberOfItemsToGet);
			}
			return suggestions;
			
		}
		

		private void OnEventNameFocus(DomEvent e)
		{
			if (this.view.uiCal.GetDate() != null)
			{
				this.view.uiEventName.Parameters.Set("date", this.view.uiCal.GetDate());
			}
			else
			{
				this.view.uiEventName.Parameters.Set("date", null);
			}
			VenueInfo vi = view.uiVenueGetter.GetVenue();
			if (vi == null)
			{
				this.view.uiEventName.Parameters.Set("venueK", null);
				
			}
			else
			{
				this.view.uiEventName.Parameters.Set("venueK", this.view.uiVenueGetter.GetVenue().k);
			}
			if (oldOnFocus != null) oldOnFocus(e);
		}

	
	 

		public void CreateEventUsingEventInfo(EventInfo eventInfo, EventInfoCallback callback)
		{
			if (eventInfo == null)
			{
				this.ShowPopup(null, null, null, callback);
			}
			else
			{
				this.ShowPopup(eventInfo.date, eventInfo.venueInfo, eventInfo.name, callback);
			}
		}
		public void ShowPopup(DateTime date, VenueInfo info, string text, EventInfoCallback callback)
		{
			this.view.uiContainer.Style.Display = "";//TODO: popup
			this.callback = callback;
			this.view.uiEventName.Text = text != null ? text : "";
			this.view.uiEventName.Focus();
			if (info != null)
			{
				this.view.uiVenueGetter.SetVenue(info);
			}
			else
			{
				this.view.uiVenueGetter.Focus();
			}
			if (date != null)
			{
				this.view.uiCal.SetDate(date);
			}
			else
			{
				this.view.uiCal.Focus();
			}
			if (date != null && info != null && callback != null)
			{
				this.view.uiEventName.RequestSuggestions();
			}
		}

		 
	}
}

