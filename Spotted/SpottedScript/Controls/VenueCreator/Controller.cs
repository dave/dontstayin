using System;
using System.DHTML;
using Spotted.WebServices.Controls.VenueCreator;
using ScriptSharpLibrary;
using Sys.Serialization;
using Sys.UI;
using Utils;

namespace SpottedScript.Controls.VenueCreator
{
	public class Controller
	{
		private View view;
		private VenueInfoCallback callback;
		internal static Controller Instance;
		private readonly TransformSuggestions oldTransformReceivedSuggestions;
		private readonly KeyStringPairAction oldNameSuggestItemChosen;
		private readonly KeyStringPairAction oldPlaceItemChosen;
		public Controller(View view)
		{
			if (Instance == null)
			{
				Instance = this;
				this.view = view;
			}
			this.oldTransformReceivedSuggestions = this.view.uiNameSuggest.TransformReceivedSuggestions;
			this.view.uiNameSuggest.TransformReceivedSuggestions = TransformReceivedSuggestions;
			
			this.oldNameSuggestItemChosen = view.uiNameSuggest.ItemChosen;
			this.view.uiNameSuggest.ItemChosen = this.NameSuggestItemChosen;

			this.oldPlaceItemChosen = this.view.uiPlace.ItemChosen;
			this.view.uiPlace.ItemChosen = this.PlaceItemChosen;

			this.view.uiPlace.Parameters.Set("returnInfo", true);

			DomEvent.AddHandler(view.uiPostCode, "blur", OnPostCodeBlur);
			DomEvent.AddHandler(view.uiContainer, "keydown", OnContainerKeyDown);
		}

		private void OnContainerKeyDown(DomEvent e)
		{
			if (e.KeyCode == (int)Key.Esc)
			{
				VenueChosen(null, null, null);
			}
		}
										                        
		private RegularExpression regex = new RegularExpression(@"^((([A-PR-UWYZ](\d([A-HJKSTUW]|\d)?|[A-HK-Y]\d([ABEHMNPRVWXY]|\d)?))\s*(\d[ABD-HJLNP-UW-Z]{2})?)|GIR\s*0AA)$", "i");
		private string oldBorder;
		private void OnPostCodeBlur(DomEvent e)
		{
			if (view.uiPostCode.Value == "" || view.uiPostCode.Disabled || regex.Test(view.uiPostCode.Value))
			{
				if (this.oldBorder != null)
				{
					view.uiPostCode.Style.Border = "";
				}
			}
			else
			{
				oldBorder = view.uiPostCode.Style.Border;
				view.uiPostCode.Focus();
			}
		}

		private void PlaceItemChosen(KeyStringPair suggestion)
		{
			PlaceInfo placeInfo = (PlaceInfo) JavaScriptSerializer.Deserialize(suggestion.Value);
			SetPlaceText(placeInfo.country.k);
			this.view.uiNameSuggest.Parameters.Set("placeK", placeInfo.k);
			if (this.oldPlaceItemChosen != null) this.oldPlaceItemChosen(suggestion);
		}
		void SetPlaceText(int countryK)
		{
			view.uiPostCode.Disabled = (countryK != 224);
			if (view.uiPostCode.Disabled)
			{
				view.uiPostCode.Value = "UK only";
			}
		}
		private void NameSuggestItemChosen(KeyStringPair suggestion)
		{
			if (suggestion.Value == "{addMethod:quick}")
			{
				PlaceInfo pi = (PlaceInfo) JavaScriptSerializer.Deserialize(view.uiPlace.Value);
				Service.CreateVenue(
					view.uiNameSuggest.Text, 
					pi.k,
					view.uiPostCode.Disabled ? "" : view.uiPostCode.Value,
					VenueChosen,
					Trace.WebServiceFailure,
					null,
					5000);
			}
			else if (suggestion.Value == "{filloutFields}")
			{
				this.view.uiPlace.Focus();
			}
			//else if (suggestion.Value == "{addMethod:wizard}")
			//{
			//    Script.Literal("debugger");
			//    Window.Navigate("/pages/venues/edit");
			//}
			else
			{
				this.VenueChosen((VenueInfo)JavaScriptSerializer.Deserialize(suggestion.Value), null, null);
			}
			if (this.oldNameSuggestItemChosen != null) this.oldNameSuggestItemChosen(suggestion);
		}


		private void VenueChosen(VenueInfo result, object userContext, string methodName)
		{
			this.view.uiContainer.Style.Display = "none";//TODO: hide popup
			if (callback != null) callback(result);
		}


		private Suggestion[] TransformReceivedSuggestions(Suggestion[] suggestions, int maxNumberOfItemsToGet)
		{
			if (view.uiPlace.Value != null && view.uiNameSuggest.Text != "")
			{
				string venueString = this.view.uiNameSuggest.Text + ", " + this.view.uiPlace.Text;
				Suggestion quickAdd = new Suggestion();
				quickAdd.html = Suggestion.GetPicTitleDetailTemplateHtml("@/gfx/icon40-eventcreator-venue-add.png", "Add <b>" + venueString + "</b> as a new venue", "You'll be able to add more details later if you like");
				quickAdd.value = "{addMethod:quick}";
				quickAdd.text = this.view.uiNameSuggest.Text;
				Suggestion.AddSuggestion(suggestions, quickAdd, maxNumberOfItemsToGet);
			

			}
			else
			{
				Suggestion notInDbSuggestion = new Suggestion();
				notInDbSuggestion.html = Suggestion.GetPicTitleDetailTemplateHtml(@"/gfx/icon40-eventcreator-venue-noadd.png", "Can't add <b>" + this.view.uiNameSuggest.Text + "</b> as a new place", "Fill out all the fields");
				notInDbSuggestion.value = "{filloutFields}";
				notInDbSuggestion.text = notInDbSuggestion.value;
				Suggestion.AddSuggestion(suggestions, notInDbSuggestion, maxNumberOfItemsToGet);
			}
			if (oldTransformReceivedSuggestions != null)
			{
				suggestions = oldTransformReceivedSuggestions(suggestions, maxNumberOfItemsToGet);
			}
			return suggestions;

		}

		public void CreateVenue(string name, KeyValuePair place, VenueInfoCallback callbackIn)
		{
			this.view.uiContainer.Style.Display = ""; //TODO: make popup
			this.view.uiNameSuggest.Focus();
			if (name != null)
			{
				//not sure if this is the right thing to do or not! maybe a search should be done to show similar venues!
				this.view.uiNameSuggest.Value = name;
				this.view.uiNameSuggest.Text = name;
				this.view.uiPlace.Focus();
			}
			if (place != null)
			{
				this.view.uiPlace.Text = place.Key;
				this.view.uiPlace.Value = place.Value.ToString();
			}
			if (place != null && name != null)
			{
				this.view.uiNameSuggest.Focus();
				this.view.uiNameSuggest.RequestSuggestions();
			}
			this.callback = callbackIn;
		}
	}
}

