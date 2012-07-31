using System;
using ScriptSharpLibrary;
using SpottedScript.Controls.VenueCreator;
using Sys.Serialization;
using Sys.UI;
using VenueCreatorController = SpottedScript.Controls.VenueCreator.Controller;
namespace SpottedScript.Controls.VenueGetter
{
	public class Controller
	{
		private readonly View view;
		private readonly TransformSuggestions oldTransformReceivedSuggestions;
		private readonly KeyStringPairAction oldItemChosen;
		
		public Controller(View view)
		{
			this.view = view;
			this.oldTransformReceivedSuggestions = view.uiAuto.TransformReceivedSuggestions;
			view.uiAuto.TransformReceivedSuggestions = TransformReceivedSuggestions;

			this.oldItemChosen = view.uiAuto.ItemChosen;
			view.uiAuto.ItemChosen = ItemChosen;
			view.uiAuto.Parameters.Set("returnInfo", true);
			DomEvent.AddHandler(view.uiSelectedItemPanel, "click", OnSelectedItemClick);
			Misc.AddHoverText(view.uiSelectedItemPanel, "Click here to change");

		}

		private void OnSelectedItemClick(DomEvent e)
		{
			this.Focus();
			this.view.uiAuto.RequestSuggestions();
		}

		public void SetVenue(VenueInfo venueInfo)
		{
			if (venueInfo != null)
			{
				this.view.uiSelectedItemPanel.Style.Display = "";
				
				this.view.uiSelectedItemPanel.InnerHTML = Suggestion.GetPicTitleDetailTemplateHtml(
					venueInfo.picPath,
					venueInfo.name,
					PlaceInfo.NameWithCountry(venueInfo.place)
				);
				
				this.view.uiAuto.Text = venueInfo.name + ", " + venueInfo.place.name + ", " + venueInfo.place.country.name;
				this.view.uiAuto.Value = JavaScriptSerializer.Serialize(venueInfo);
				this.view.uiAuto.input.Style.Display = "none";
				this.view.uiSelectedItemPanel.Style.Display = "";

			}
			else
			{
				this.view.uiAuto.input.Style.Display = "";
				this.view.uiSelectedItemPanel.Style.Display = "none";
				this.view.uiAuto.Text = "";
				this.view.uiAuto.Value = "";
			}

		}
		internal VenueInfo GetVenue()
		{
			try
			{
				return (VenueInfo) JavaScriptSerializer.Deserialize(this.view.uiAuto.Value);
			}catch(Exception ex)
			{
				return null;
			}
		}
		private void ItemChosen(KeyStringPair suggestion)
		{

			if (suggestion.Value.StartsWith("|create|"))
			{
				string[] parts = suggestion.Value.Split("|");
				string venueName = parts[parts.Length - 1];
				VenueCreatorController.Instance.CreateVenue(venueName, null, SetVenue);
			}
			else
			{

				SetVenue((VenueInfo) JavaScriptSerializer.Deserialize(suggestion.Value));
				if (this.oldItemChosen != null) oldItemChosen(suggestion);
			}

		}
 

		private Suggestion[] TransformReceivedSuggestions(Suggestion[] suggestions, int maxNumberOfItemsToGet)
		{
			if (oldTransformReceivedSuggestions != null) suggestions = oldTransformReceivedSuggestions(suggestions, maxNumberOfItemsToGet);
			if (this.view.uiAuto.Text != "" && this.view.uiAuto.Value == null)
			{
				
				Suggestion createNewVenueSuggestion = new Suggestion();
				createNewVenueSuggestion.html = Suggestion.GetPicTitleDetailTemplateHtml(@"/gfx/icon40-eventcreator-add.png", "Add <b>" + view.uiAuto.Text + "</b>", "Other people will be able to add details about the venue later");
				createNewVenueSuggestion.value = "|create|" + view.uiAuto.Text + "";
				createNewVenueSuggestion.text = createNewVenueSuggestion.value;
				Suggestion.AddSuggestion(suggestions, createNewVenueSuggestion, maxNumberOfItemsToGet);
			}

			return suggestions;
		}



		internal void Focus()
		{
			this.view.uiAuto.input.Style.Display = "";
			this.view.uiSelectedItemPanel.Style.Display = "none";
			this.view.uiAuto.Focus();
		}
	}
}
