using System;
using System.DHTML;
using ScriptSharpLibrary;
using Sys.UI;
using Utils;

namespace SpottedScript.Controls.SiteSearchBox
{
	public class Controller
	{
		private readonly View view;
		private TransformSuggestions oldTransformReceivedSuggestions;
		private KeyStringPairAction oldItemChosen;
		private Action oldOnSuggestionsRequested;

		public Controller(View view)
		{
			this.view = view;
			this.oldTransformReceivedSuggestions = this.view.uiAuto.TransformReceivedSuggestions;
			this.view.uiAuto.TransformReceivedSuggestions = TransformReceivedSuggestions;

			this.oldItemChosen = this.view.uiAuto.ItemChosen;
			this.view.uiAuto.ItemChosen = ItemChosen;

			this.oldOnSuggestionsRequested = view.uiAuto.OnSuggestionsRequested;
			view.uiAuto.OnSuggestionsRequested = OnSuggestionsRequested;
		}

		private void OnSuggestionsRequested()
		{
			this.view.uiAuto.AddSuggestion(GetGoogleSearchSuggestion());
		}

		private void ItemChosen(KeyStringPair suggestion)
		{
			if (suggestion.Value == "{google}")
			{
				//string googleSearchCode = "%0A%3Cstyle%20type%3D%22text%2Fcss%22%3E%0A%40import%20url%28http%3A%2F%2Fwww.google.com%2Fcse%2Fapi%2Fbranding.css%29%3B%0A%3C%2Fstyle%3E%0A%3Cdiv%20class%3D%22cse-branding-right%22%20style%3D%22background-color%3A%23FFFFFF%3Bcolor%3A%23000000%22%3E%0A%20%20%3Cdiv%20class%3D%22cse-branding-form%22%3E%0A%20%20%20%20%3Cform%20action%3D%22%2Fpages%2FSearchResults%22%20id%3D%22cse-search-box%22%3E%0A%20%20%20%20%20%20%3Cdiv%3E%0A%20%20%20%20%20%20%20%20%3Cinput%20type%3D%22hidden%22%20name%3D%22cx%22%20value%3D%22partner-pub-3401092463304336%3A97ut1yppc4j%22%20%2F%3E%0A%20%20%20%20%20%20%20%20%3Cinput%20type%3D%22hidden%22%20name%3D%22cof%22%20value%3D%22FORID%3A10%22%20%2F%3E%0A%20%20%20%20%20%20%20%20%3Cinput%20type%3D%22hidden%22%20name%3D%22ie%22%20value%3D%22ISO-8859-1%22%20%2F%3E%0A%20%20%20%20%20%20%20%20%3Cinput%20id%3D%22qInput%22%20type%3D%22text%22%20name%3D%22q%22%20size%3D%2231%22%20%2F%3E%0A%20%20%20%20%20%20%20%20%3Cinput%20id%3D%22submitButton%22%20type%3D%22submit%22%20name%3D%22sa%22%20value%3D%22Search%22%20%2F%3E%0A%20%20%20%20%20%20%3C%2Fdiv%3E%0A%20%20%20%20%3C%2Fform%3E%0A%20%20%3C%2Fdiv%3E%0A%20%20%3Cdiv%20class%3D%22cse-branding-logo%22%3E%0A%20%20%20%20%3Cimg%20src%3D%22http%3A%2F%2Fwww.google.com%2Fimages%2Fpoweredby_transparent%2Fpoweredby_FFFFFF.gif%22%20alt%3D%22Google%22%20%2F%3E%0A%20%20%3C%2Fdiv%3E%0A%20%20%3Cdiv%20class%3D%22cse-branding-text%22%3E%0A%20%20%20%20Custom%20Search%0A%20%20%3C%2Fdiv%3E%0A%3C%2Fdiv%3E%0A%0A";
				//string html = googleSearchCode.Unescape();
				//DOMElement div = Document.CreateElement("DIV");
				//div.InnerHTML = html;
				//view.GoogleSearchCode.AppendChild(div);
				//((InputElement) Document.GetElementById("qInput")).Value = suggestion.Key;
				//Document.GetElementById("submitButton").Click();

				//http://www.google.co.uk/search?hl=en-GB&q=site:dontstayin.com+pacha
				Script.Eval("window.location = 'http://www.google.co.uk/search?q=site:dontstayin.com+" + suggestion.Key.EncodeURI().Replace(new RegularExpression("'", "g"), "\\'") + "';");
			}
			else
			{
				Script.Eval("window.location = '" + suggestion.Value + "';");
			}
		}

		private Suggestion[] TransformReceivedSuggestions(Suggestion[] suggestions, int maxNumberToGet)
		{
			Suggestion.AddSuggestionAtTop(suggestions, this.GetGoogleSearchSuggestion());
			if (this.oldTransformReceivedSuggestions != null) suggestions = oldTransformReceivedSuggestions(suggestions, maxNumberToGet);
			return suggestions;
		}

		private Suggestion GetGoogleSearchSuggestion()
		{
			Suggestion googleSearchSuggestion = new Suggestion();
			googleSearchSuggestion.html = Suggestion.GetPicTitleDetailTemplateHtml("/gfx/icon40-google.png", "Search DSI using Google", "");
			googleSearchSuggestion.text = this.view.uiAuto.Text;
			googleSearchSuggestion.value = "{google}";
			return googleSearchSuggestion;
		}
	}
	
}
