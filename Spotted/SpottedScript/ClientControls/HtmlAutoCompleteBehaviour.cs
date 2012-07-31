using JQ;
using Sys.UI;
using System.DHTML;
using Sys.Net;
using System;
using Sys;
using Net.Comet;
using ScriptSharpLibrary.HtmlAutoComplete;
using Utils;
using Login = SpottedScript.Controls.Navigation.Login.PageImplementation;

namespace ScriptSharpLibrary
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
		WebServiceRemoteSuggestionsGetter remoteSuggestionsGetter;
		public SuggestionsCollection Suggestions = new SuggestionsCollection();
		public Action onhighlight;
		public KeyStringPairAction ItemChosen;
		public Action OnTextPasted;
		internal InputElement input;
		InputElement hiddenInput;
		DOMElement anchor;
		PopupMenu _popupMenu;
		
		public void SetWebMethod(string methodName)
		{
			this.remoteSuggestionsGetter.MethodName = methodName;
		}
		HtmlAutoCompleteMode mode;
		WatermarkExtender watermarker;
		public PairListField Parameters;
		public DomEventHandler OnFocus;
		public HtmlAutoCompleteBehaviour(InputElement input, InputElement hiddenInput, DOMElement anchor, bool isSuggest, InputElement parametersHiddenField)
		{
			DOMAttribute cometAtt = input.GetAttributeNode(HtmlAutoCompleteAttributes.CometServiceUrl);

			remoteSuggestionsGetter = new WebServiceRemoteSuggestionsGetter(
				input.GetAttributeNode(HtmlAutoCompleteAttributes.WebServiceUrl).Value,
				input.GetAttributeNode(HtmlAutoCompleteAttributes.WebServiceMethod).Value
			);
			
			this.mode = isSuggest == true ? HtmlAutoCompleteMode.Suggest : HtmlAutoCompleteMode.Complete;
			this.input = input;
			this.hiddenInput = hiddenInput;
			this.anchor = anchor == null ? input : anchor;
			DomEvent.AddHandler(input, "blur", delegate(DomEvent e) { Window.SetTimeout(DoBlur, 250); });
			DomEvent.AddHandler(input, "keydown", this.HandleKeyDown);
			DomEvent.AddHandler(input, "keyup", this.HandleKeyUp);
			DomEvent.AddHandler(input, "focus", CallOnFocus);

			DOMAttribute waterMarkNode = input.GetAttributeNode(HtmlAutoCompleteAttributes.Watermark);
			if (waterMarkNode != null)
			{
				watermarker = new WatermarkExtender(input, input.GetAttributeNode(HtmlAutoCompleteAttributes.Watermark).Value);
			}
			DOMAttribute popupLeftNode = input.GetAttributeNode(HtmlAutoCompleteAttributes.PopupLeftOffset);
			popupLeftOffset = popupLeftNode == null ? 0 : int.ParseInvariant(popupLeftNode.Value);
			DOMAttribute popupTopNode = input.GetAttributeNode(HtmlAutoCompleteAttributes.PopupTopOffset);
			popupTopOffset = popupTopNode == null ? 0 : int.ParseInvariant(popupTopNode.Value);
			DOMAttribute rightAlignNode = input.GetAttributeNode(HtmlAutoCompleteAttributes.RightAlign);
			rightAlign = rightAlignNode == null ? false : bool.Parse(rightAlignNode.Value);
			
			if (input.GetAttributeNode(HtmlAutoCompleteAttributes.PopupLeftOffset) != null)
			{
				popupLeftOffset = int.ParseInvariant(input.GetAttributeNode(HtmlAutoCompleteAttributes.PopupLeftOffset).Value);
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

		private void CallOnFocus(DomEvent e)
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
				Offset offset = JQueryAPI.JQuery(anchor).Offset();
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
					DOMAttribute cssAtt = input.GetAttributeNode(HtmlAutoCompleteAttributes.PopupMenuClassName);
					DOMAttribute highlightedCssAtt = input.GetAttributeNode(HtmlAutoCompleteAttributes.PopupMenuHighlightedItemClassName);
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
	 
		void HandleKeyDown(DomEvent e)
		{
			if (e.KeyCode == (int)Key.Up) { if (popupMenu != null) popupMenu.IndexOfCurrentlyHighlightedItem -= 1; }
			else if (e.KeyCode == (int)Key.Down) { if (popupMenu != null) { popupMenu.IndexOfCurrentlyHighlightedItem += 1; } }
			
			else if (e.KeyCode == (int)Key.Esc)
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
			else if ((e.KeyCode == (int)Key.Tab && !e.ShiftKey) || e.KeyCode == (int)Key.Enter)
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
						if (e.KeyCode == (int)Key.Enter) e.PreventDefault();
						ItemSelected(this.popupMenu.CurrentlyHighlightedItem);

						return;
					}
				}
			}
			else if (e.KeyCode == (int)Key.Backspace && mode == HtmlAutoCompleteMode.Complete)
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
			else if (e.KeyCode == (int)Key.Del && mode == HtmlAutoCompleteMode.Complete) { this.hiddenInput.Value = ""; RequestSuggestions(); ; }
		}
		void HandleKeyUp(DomEvent e)
		{
			if (e.KeyCode != (int)Key.Down
			&&	e.KeyCode != (int)Key.End
			&&	e.KeyCode != (int)Key.Enter
			&&	e.KeyCode != (int)Key.Esc
			&&	e.KeyCode != (int)Key.Home
			&&	e.KeyCode != (int)Key.Left
			&&	e.KeyCode != (int)Key.PageDown
			&&	e.KeyCode != (int)Key.PageUp
			// &&	e.KeyCode != (int)Key.Tab
			&&	e.KeyCode != (int)Key.Up)
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
					new Callback(delegate() { this.remoteSuggestionsGetter.RequestSuggestions(this.input.Value, this.Parameters.ToDictionary(), maxNumberOfItemsToGet); }),
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
				DOMAttribute att = input.GetAttributeNode(HtmlAutoCompleteAttributes.MaxNumberOfItemsToGet);
				return att == null ? 10 : int.ParseInvariant(att.Value);
			}
		}
		int minWidth
		{
			get
			{
				DOMAttribute att = input.GetAttributeNode(HtmlAutoCompleteAttributes.MinimumPopupWidth);
				return att == null ? 0 : int.ParseInvariant(att.Value);
			}
		}
		int maxWidth
		{
			get
			{
				DOMAttribute att = input.GetAttributeNode(HtmlAutoCompleteAttributes.MaximumPopupWidth);
				return att == null ? 0 : int.ParseInvariant(att.Value);
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
			DOMAttribute att = input.GetAttributeNode("AutoPostBackFunction");
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
