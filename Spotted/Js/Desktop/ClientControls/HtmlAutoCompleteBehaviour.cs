
using System.Html;
using System;
//using Net.Comet;
using Js.Library;
using jQueryApi;
using Login = Js.Controls.Login.PageImplementation;
using Js.AutoCompleteLibrary;


namespace Js.ClientControls
{
	enum HtmlAutoCompleteMode
	{
		Suggest = 1,
		Complete = 2
	}
	
	public delegate void KeyValuePairAction(KeyValuePair suggestion);
	public delegate void KeyStringPairAction(KeyStringPair suggestion);
	public delegate Suggestion[] TransformSuggestions(Suggestion[] suggestions, int maxNumberToGet);
	
	public class HtmlAutoCompleteBehaviour
	{
		int popupLeftOffset = 0;
		int popupTopOffset = 0;
		private bool rightAlign = false;
		public Action OnSuggestionsRequested;
		Js.ClientControls.HtmlAutoComplete.WebServiceRemoteSuggestionsGetter remoteSuggestionsGetter;
		public SuggestionsCollection Suggestions = new SuggestionsCollection();
		public Action onhighlight;
		public KeyStringPairAction ItemChosen;
		public Action OnTextPasted;
		internal InputElement input;
		InputElement hiddenInput;
		Element anchor;
		PopupMenu _popupMenu;
		
		public void SetWebMethod(string methodName)
		{
			this.remoteSuggestionsGetter.MethodName = methodName;
		}
		HtmlAutoCompleteMode mode;
		WatermarkExtender watermarker;
		public PairListField Parameters;
		public jQueryEventHandler OnFocus;
		public HtmlAutoCompleteBehaviour(InputElement input, InputElement hiddenInput, Element anchor, bool isSuggest, InputElement parametersHiddenField)
		{
			//ElementAttribute cometAtt = input.GetAttributeNode(HtmlAutoCompleteAttributes.CometServiceUrl);

			remoteSuggestionsGetter = new Js.ClientControls.HtmlAutoComplete.WebServiceRemoteSuggestionsGetter(
				input.GetAttributeNode(HtmlAutoCompleteAttributes.WebServiceUrl).Value,
				input.GetAttributeNode(HtmlAutoCompleteAttributes.WebServiceMethod).Value
			);
			
			this.mode = isSuggest == true ? HtmlAutoCompleteMode.Suggest : HtmlAutoCompleteMode.Complete;
			this.input = input;
			this.hiddenInput = hiddenInput;
			this.anchor = anchor == null ? input : anchor;
			jQuery.FromElement(input).Blur(delegate(jQueryEvent e) { Window.SetTimeout(DoBlur, 250); });
			jQuery.FromElement(input).Keydown(this.HandleKeyDown);
			jQuery.FromElement(input).Keyup(this.HandleKeyUp);
			jQuery.FromElement(input).Focus(CallOnFocus);

			ElementAttribute waterMarkNode = input.GetAttributeNode(HtmlAutoCompleteAttributes.Watermark);
			if (waterMarkNode != null)
			{
				watermarker = new WatermarkExtender(input, input.GetAttributeNode(HtmlAutoCompleteAttributes.Watermark).Value);
			}
			ElementAttribute popupLeftNode = input.GetAttributeNode(HtmlAutoCompleteAttributes.PopupLeftOffset);
			popupLeftOffset = popupLeftNode == null ? 0 : int.Parse(popupLeftNode.Value);
			ElementAttribute popupTopNode = input.GetAttributeNode(HtmlAutoCompleteAttributes.PopupTopOffset);
			popupTopOffset = popupTopNode == null ? 0 : int.Parse(popupTopNode.Value);
			ElementAttribute rightAlignNode = input.GetAttributeNode(HtmlAutoCompleteAttributes.RightAlign);
			rightAlign = rightAlignNode == null ? false : bool.Parse(rightAlignNode.Value);
			
			if (input.GetAttributeNode(HtmlAutoCompleteAttributes.PopupLeftOffset) != null)
			{
				popupLeftOffset = int.Parse(input.GetAttributeNode(HtmlAutoCompleteAttributes.PopupLeftOffset).Value);
			}
			Parameters = new PairListField(parametersHiddenField);
			Suggestions.OnSuggestionsChanged = delegate() { DisplaySuggestionsInPopupMenu(); };

			this.remoteSuggestionsGetter.OnAllSuggestionsReceived = delegate() 
			{ 
				RemoveLowPrioritySuggestionsAndSetRemainingSuggestionsToLowPriority();
				HideAjaxIcon();
			};
			this.remoteSuggestionsGetter.OnSuggestionsRequested = delegate()
			                                                      {
			                                                      	ShowAjaxIcon();
			                                                      };
			
			this.remoteSuggestionsGetter.OnSuggestionReceived = delegate(Suggestion[] newSuggestions) 
			{
				Trace.Write("Received" + newSuggestions.Length + "suggestions");
				if (TransformReceivedSuggestions != null)
				{
					AddSuggestions(TransformReceivedSuggestions(newSuggestions, maxNumberOfItemsToGet));
				}
				else
				{
					AddSuggestions(newSuggestions);
				}
			};
			this.remoteSuggestionsGetter.OnAbortCurrentRequest= delegate()
			                                                      {
			                                                      	HideAjaxIcon();

			                                                      };
			

		}

		private void CallOnFocus(jQueryEvent e)
		{
			e.PreventDefault();
			Login.WhenLoggedIn(
				new Action(
					delegate()
					{
						this.Focus();
						if (OnFocus != null)
							OnFocus(e);
					}
				)
			);
		}


		public TransformSuggestions TransformReceivedSuggestions;
		private ImageElement ajaxIcon = null;
		void HideAjaxIcon()
		{
			if (ajaxIcon != null)
			{
				ajaxIcon.Style.Display = "none";	
			}
		}
		void ShowAjaxIcon()
		{
			if (ajaxIcon == null)
			{
				ajaxIcon = (ImageElement) Document.CreateElement("IMG");
				ajaxIcon.Src = "/Gfx/autocomplete-loading.gif";
				ajaxIcon.Style.Height = "16px";
				ajaxIcon.Style.Width = "16px";
				ajaxIcon.Style.Position = "absolute";
				jQueryPosition offset = jQuery.FromElement(anchor).GetOffset();
				ajaxIcon.Style.Left = (offset.Left + anchor.ClientWidth - 18) + "px";
				ajaxIcon.Style.Top = (offset.Top + 2) + "px";
				ajaxIcon.Style.ZIndex = 200;
				Document.Body.AppendChild(ajaxIcon);
			}
			ajaxIcon.Style.Display = "";
		}
 
		public void AddSuggestion(Suggestion newSuggestion)
		{
			AddSuggestions(new Suggestion[] { newSuggestion });
		}
		private void AddSuggestions(Suggestion[] newSuggestions)
		{
			Trace.Write("Adding " + newSuggestions.Length + " suggestions. Suggestions length = " + Suggestions.Count);
			Suggestions.AddRange(newSuggestions);
			while (Suggestions.Count > maxNumberOfItemsToGet)
			{
				Trace.Write("Suggestions length " + Suggestions.Count + " Removing suggestion");
				Suggestions.RemoveAt(Suggestions.Count - 1);
			}
			Trace.Write("Finished adding " + newSuggestions.Length + " suggestions. Suggestions length = " + Suggestions.Count);
		}
		 
		public void DisplaySuggestionsInPopupMenu()
		{
			Trace.Write("DisplaySuggestionsInPopupMenu"); 
			popupMenu.Clear();
			for (int i = 0; i < Suggestions.Count; i++)
			{
				Suggestion suggestion = (Suggestion)Suggestions[i];
				KeyValuePair pair = new KeyValuePair();
				pair.Key = suggestion.html;
				KeyValuePair value = new KeyValuePair();
				value.Key = suggestion.text;
				value.Value = mode == HtmlAutoCompleteMode.Complete ? suggestion.value : suggestion.text;
				pair.Value = value;
				popupMenu.AddItem(pair);
			}
			HighlightFirstSuggestion();
			
			
			popupMenu.Show(anchor, minWidth, maxWidth, popupTopOffset, popupLeftOffset, rightAlign);
		}
		
		private void HighlightFirstSuggestion()
		{
			if (mode == HtmlAutoCompleteMode.Complete && popupMenu.CurrentlyHighlightedItem == null)
			{
				popupMenu.IndexOfCurrentlyHighlightedItem = 0;
			}
		}

		private void RemoveLowPrioritySuggestionsAndSetRemainingSuggestionsToLowPriority()
		{
			RemoveLowPrioritySuggestions();
			SetSuggestionsToLowPriority();
		}

		private void RemoveLowPrioritySuggestions()
		{
			for (int i = 0; i < Suggestions.Count; i++)
			{
				if (Suggestions[i].priority == -1)
				{
					Suggestions.RemoveAt(i);
					i--;
					continue;
				}
			}
		}

		private void SetSuggestionsToLowPriority()
		{
			for (int i = 0; i < Suggestions.Count; i++)
			{
				Suggestions[i].priority = -1;

			}
		}
		PopupMenu popupMenu
		{
			get
			{
				if (_popupMenu == null)
				{
					ElementAttribute cssAtt = input.GetAttributeNode(HtmlAutoCompleteAttributes.PopupMenuClassName);
					ElementAttribute highlightedCssAtt = input.GetAttributeNode(HtmlAutoCompleteAttributes.PopupMenuHighlightedItemClassName);
					_popupMenu = new PopupMenu
					(
						cssAtt == null ? HtmlAutoCompleteAttributes.PopupMenuClassName : cssAtt.Value,
						highlightedCssAtt == null ? HtmlAutoCompleteAttributes.PopupMenuHighlightedItemClassName : highlightedCssAtt.Value

					);
					_popupMenu.ItemClick = ItemSelected;
					_popupMenu.ItemHighlighted = onhighlight;
				}
				return _popupMenu;
			}
		}
		
		void DoBlur()
		{
			if (this.hiddenInput.Value == "")
			{
				Cancel();
			}
			else
			{
				popupMenu.Hide();
			}
		}
	 
		void HandleKeyDown(jQueryEvent e)
		{
			if (e.Which == 38 /*Key.Up*/)
			{
				if (popupMenu != null) popupMenu.IndexOfCurrentlyHighlightedItem -= 1;
			}
			else if (e.Which == 40 /*Key.Down*/)
			{
				if (popupMenu != null) popupMenu.IndexOfCurrentlyHighlightedItem += 1;
			}
			else if (e.Which == 27 /*Key.Esc*/)
			{
				if (this.popupMenu.CurrentlyHighlightedItem != null)
				{
					Clear();
					e.PreventDefault();
				}
				else
				{
					Cancel();
				}
			} 
			else if ((e.Which == 9 /*Key.Tab*/ && !e.ShiftKey) || e.Which == 13 /*Key.Enter*/)
			{
				if (mode == HtmlAutoCompleteMode.Suggest)
				{
					if (SuggestionIsHighlighted)
					{
						e.PreventDefault();
						ItemSelected(this.popupMenu.CurrentlyHighlightedItem);
						popupMenu.Clear();
						return;
					}
					else
					{
						if (ValidSelectionHasBeenMade)
						{
							KeyValuePair pair = new KeyValuePair();
							pair.Key = this.input.Value;
							pair.Value = this.input.Value;
							ItemSelected(pair);
							return;
						}
					}
				}
				else if (mode == HtmlAutoCompleteMode.Complete)
				{
					if (SuggestionIsHighlighted && !ValidSelectionHasBeenMade)
					{
						if (e.Which == 13 /*Key.Enter*/) e.PreventDefault();
						ItemSelected(this.popupMenu.CurrentlyHighlightedItem);

						return;
					}
				}
			}
			else if (e.Which == 8 /*Key.Backspace*/ && mode == HtmlAutoCompleteMode.Complete)
			{
				Suggestions.Clear();
				if (this.ValidSelectionHasBeenMade)
				{
					this.hiddenInput.Value = ""; this.input.Value = ""; 
				}
				else
				{
					this.hiddenInput.Value = ""; RequestSuggestions(); ; 
				}
			}
			else if (e.Which == 46 /*Key.Del*/ && mode == HtmlAutoCompleteMode.Complete)
			{
				this.hiddenInput.Value = "";
				RequestSuggestions();
			}
		}
		void HandleKeyUp(jQueryEvent e)
		{
			if (
				e.Which != 40 /*Key.Down*/
				&& e.Which != 35 /*Key.End*/
				&& e.Which != 13 /*Key.Enter*/
				&& e.Which != 27 /*Key.Esc*/
				&& e.Which != 36 /*Key.Home*/
				&& e.Which != 37 /*Key.Left*/
				&& e.Which != 34 /*Key.PageDown*/
				&& e.Which != 33 /*Key.PageUp*/
				&& e.Which != 38 /*Key.Up*/
			)
			{
				if (mode == HtmlAutoCompleteMode.Suggest)
				{
					hiddenInput.Value = input.Value;
				}
				RequestSuggestions();
			}
		}
		int currentTimer = -2;
		public void RequestSuggestions()
		{
			SetSuggestionsToLowPriority();
			if (mode == HtmlAutoCompleteMode.Complete)
			{
				this.Value = "";
			}
			if (this.OnSuggestionsRequested != null) { Trace.Write("OnSuggestionsRequested"); this.OnSuggestionsRequested(); }
			if (currentTimer != -2) { Window.ClearTimeout(currentTimer); }
			if (this.input.Value.Trim().Length > 0)
			{
				currentTimer = Window.SetTimeout
				(
					new Callback(
						delegate(object o)
						{
							this.remoteSuggestionsGetter.RequestSuggestions(
								this.input.Value, 
								this.Parameters.ToDictionary(), 
								maxNumberOfItemsToGet); 
						}
					),
					200
				);
			}
		
		}
		bool ValidSelectionHasBeenMade { get { return hiddenInput.Value != null && hiddenInput.Value != ""; } }
		bool SuggestionIsHighlighted { get { return popupMenu.CurrentlyHighlightedItem != null; } }
		private void Cancel()
		{
			Clear();
			input.Value = "";
			hiddenInput.Value = "";
			this.remoteSuggestionsGetter.AbortCurrentRequest();
 
		}


		public void Clear()
		{
			Suggestions.Clear();
			popupMenu.Clear();
			this.remoteSuggestionsGetter.AbortCurrentRequest();
		}
		int maxNumberOfItemsToGet
		{
			get
			{
				ElementAttribute att = input.GetAttributeNode(HtmlAutoCompleteAttributes.MaxNumberOfItemsToGet);
				return att == null ? 10 : int.Parse(att.Value);
			}
		}
		int minWidth
		{
			get
			{
				ElementAttribute att = input.GetAttributeNode(HtmlAutoCompleteAttributes.MinimumPopupWidth);
				return att == null ? 0 : int.Parse(att.Value);
			}
		}
		int maxWidth
		{
			get
			{
				ElementAttribute att = input.GetAttributeNode(HtmlAutoCompleteAttributes.MaximumPopupWidth);
				return att == null ? 0 : int.Parse(att.Value);
			}
		}

		

		private void ItemSelected(object value)
		{
			this.remoteSuggestionsGetter.AbortCurrentRequest();
			KeyStringPair pair = (KeyStringPair)value;
			this.input.Value = pair.Key;
			this.hiddenInput.Value = pair.Value;
			Clear();
			if (ItemChosen != null) { ItemChosen(pair); }
			ElementAttribute att = input.GetAttributeNode("AutoPostBackFunction");
			if (att != null) { Script.Eval(att.Value); }
		}
	 
		public string Text { get { return input.Value; } set { input.Value = value; } }
		public string Value { get { return hiddenInput.Value; } set { hiddenInput.Value = value; } }
		public object CurrentlyHighlighedItem { get { return popupMenu.CurrentlyHighlightedItem; } }



		public void Focus()
		{
			this.input.Focus();
			if (watermarker != null) { watermarker.OnFocus(null); }
		}
	}

}
