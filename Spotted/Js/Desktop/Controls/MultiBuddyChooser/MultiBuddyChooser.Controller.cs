using System.Html;
using System;
using System.Collections.Generic;
//using Spotted.WebServices.Controls;
//using Spotted.WebServices.Controls.MultiBuddyChooser;
//using CreateUserFromEmail = SpottedScript.Behaviours.CreateUserFromEmail.Controller;
//using CreateUsersFromEmails = SpottedScript.Behaviours.CreateUsersFromEmails.Controller;
using Js.Library;
using jQueryApi;
using Js.ClientControls;
using MultiBuddyChooserService = Js.Controls.MultiBuddyChooser.Service;

namespace Js.Controls.MultiBuddyChooser
{
	public class Controller
	{
		OptionElement[] contextPlaces = new OptionElement[] { };
		OptionElement[] contextMusicTypes = new OptionElement[] { };
		OptionElement[] allPlaces = new OptionElement[] { };
		OptionElement[] allMusicTypes = new OptionElement[] { };

		public string[] SelectedValues 
		{
			get 
			{
				Array selections = this.view.uiBuddyMultiSelector.GetSelections().ToArray();
				string[] selectedValues = new string[selections.Length];
				for (int i = 0; i < selections.Length; i++)
				{
					selectedValues[i] = ((string[])selections[i])[1];
				}
				return selectedValues; ; 
			} 
		}

		
		View view;
		CreateUserFromEmailController createUserFromEmailBehaviour;
		CreateUsersFromEmailsController createUsersFromEmailsBehaviour;
		public Controller(View view) 
		{
			//Trace.Write("SpottedScript.Controls.MultiBuddyChooser.Ctor");
			this.view = view;
			view.uiAddByMusicAndPlaceJ.Click(AddByMusicAndPlaceButtonClick);
			view.uiAddAllButtonJ.Click(AddAllButtonClick);
			view.uiShowAllTownsAndMusicJ.Click(ShowAllTownsAndMusicCheckBoxClick);
			view.uiShowAddAllJ.Click(ShowAddAll);
			view.uiShowAddByJ.Click(ShowAddBy);
			view.uiShowBuddyListJ.Click(ShowBuddyList);
			view.uiJustBuddiesRadioJ.Click(AutoCompleteQueryGroupClick);
			view.uiAllMembersRadioJ.Click(AutoCompleteQueryGroupClick);

			CopyValuesFromSelectListToArray(this.view.uiMusicTypes, contextMusicTypes);
			CopyValuesFromSelectListToArray(this.view.uiPlaces, contextPlaces);

			createUserFromEmailBehaviour = new CreateUserFromEmailController(this.view.uiBuddyMultiSelector.HtmlAutoComplete);
			createUsersFromEmailsBehaviour = new CreateUsersFromEmailsController(this.view.uiBuddyMultiSelector.HtmlAutoComplete);

			this.oldItemRemoved = this.view.uiBuddyMultiSelector.ItemRemoved;
			this.view.uiBuddyMultiSelector.ItemRemoved = this.OnMultiSelectorItemRemoved;
		}

		private void AutoCompleteQueryGroupClick(jQueryEvent e)
		{
			if (this.view.uiJustBuddiesRadio.Checked)
			{
				this.view.uiBuddyMultiSelector.HtmlAutoComplete.SetWebMethod("GetBuddies");
			}
			else
			{
				this.view.uiBuddyMultiSelector.HtmlAutoComplete.SetWebMethod("GetBuddiesThenUsrs");
			}
		}

		private void OnMultiSelectorItemRemoved(string key, string value)
		{
			string optionText = (string) this.clickedOptions[value];
			if (optionText != null)
			{
				this.clickedOptions.Remove(value);
				OptionElement option = (OptionElement) Document.CreateElement("OPTION");
				option.InnerHTML = optionText;
				option.Value = value;
				int i = 0;
				if (this.view.uiBuddyList.ChildNodes.Length == 0)
				{
					this.view.uiBuddyList.AppendChild(option);
				}
				else
				{
					if (this.view.uiBuddyList.ChildNodes.Length < 150)
					{
						while (this.view.uiBuddyList.ChildNodes[i].InnerHTML.LocaleCompare(optionText) < 0 && i < this.view.uiBuddyList.ChildNodes.Length - 1)
						{
							i++;
						}
					}
					this.view.uiBuddyList.InsertBefore(option, this.view.uiBuddyList.ChildNodes[i]);
				}
			}
			if (oldItemRemoved != null) oldItemRemoved(key, value);
		}

		private ItemChangeDelegate oldItemRemoved;
		bool buddyListLoaded = false;
		private void ShowBuddyList(jQueryEvent e)
		{
			if (this.view.uiShowBuddyList.Checked && !buddyListLoaded)
			{
				Service.GetBuddiesSelectListHtml(GetBuddiesCallback, Trace.WebServiceFailure, null, 0);
				this.SetBuddyListInnerHTML("<OPTION value='-1'>Loading...</OPTION>");
			}
			this.view.uiBuddyListPanel.Style.Display = this.view.uiShowBuddyList.Checked ? "" : "none";
		}

		private void GetBuddiesCallback(string result, object userContext, string methodName)
		{
			view.uiBuddyListJ.Unbind("click").Unbind("keydown");
			//DomEvent.ClearHandlers(this.view.uiBuddyList);
			this.SetBuddyListInnerHTML(result);
			
			view.uiBuddyListJ.Click(BuddyListClicked);
			view.uiBuddyListJ.Keydown(BuddyListKeyPressed);
			this.clickedOptions = new Dictionary<object, object>();
			buddyListLoaded = true;
			this.view.uiBuddyListPanel.Style.Display = "";
			this.view.uiBuddyList.Focus();
			if (this.view.uiBuddyList.ChildNodes.Length > 0)
			{
				((OptionElement) this.view.uiBuddyList.ChildNodes[0]).Selected = true;
			}
		}

		private void BuddyListKeyPressed(jQueryEvent e)
		{
			if (e.Which == 32 /*Key.Space*/ || e.Which == 13 /*Key.Enter*/)
			{
				this.view.uiBuddyList.Click();
			}
		}

		private void SetBuddyListInnerHTML(string innerHTML)
		{
			if ((bool)Script.Literal("ie"))
			{
				this.view.uiBuddyList.InnerHTML = "";
				string selectHTML = this.view.uiBuddyListPanel.InnerHTML;
				this.view.uiBuddyListPanel.InnerHTML = selectHTML.Substring(0, selectHTML.IndexOf("</SELECT>")) + innerHTML + "</SELECT>";
			}
			else
			{
				this.view.uiBuddyList.InnerHTML = innerHTML;
			}
		
		}

		private Dictionary<object, object> clickedOptions = new Dictionary<object, object>();
		private void BuddyListClicked(jQueryEvent e)
		{
			int selectedIndex = this.view.uiBuddyList.SelectedIndex;
			if (selectedIndex > -1)
			{
				int scrollTop = this.view.uiBuddyList.ScrollTop;
				OptionElement option = (OptionElement) this.view.uiBuddyList.Options[selectedIndex];
				this.clickedOptions[option.Value] = option.InnerHTML;
				this.view.uiBuddyMultiSelector.AddItem(option.InnerHTML, option.Value);
				this.view.uiBuddyList.SelectedIndex = -1;
				this.view.uiBuddyList.Remove(selectedIndex);
				this.view.uiBuddyList.ScrollTop = scrollTop;
			}
			if (this.view.uiBuddyList.ChildNodes.Length > selectedIndex && selectedIndex != -1)
			{
				((OptionElement) this.view.uiBuddyList.ChildNodes[selectedIndex]).Selected = true;
			}
			this.view.uiBuddyList.Focus();
		}

		private void ShowAddAll(jQueryEvent e)
		{
			this.view.uiAddAll.Style.Display = this.view.uiShowAddAll.Checked ? "" : "none";
		}
		private void ShowAddBy(jQueryEvent e)
		{
			this.view.uiAddBy.Style.Display = this.view.uiShowAddBy.Checked ? "" : "none";
		}
		private void CopyValuesFromSelectListToArray(SelectElement el, OptionElement[] options)
		{
			for (int i = 0; i < el.Options.Length; i++)
			{
				options[options.Length] = (OptionElement) el.Options[i];
			}
		}

		void AddAllButtonClick(jQueryEvent ev)
		{
			AddByGeneric(ev, true);
		}
		void AddByMusicAndPlaceButtonClick(jQueryEvent ev)
		{
			AddByGeneric(ev, false);
		}
		void AddByGeneric(jQueryEvent ev, bool addAll)
		{
			string text = "All buddies";
			string value = "";

			if (!addAll)
			{
				text += view.uiPlaces.Value == "-1" ? "" : " who visit " + view.uiPlaces.Options[view.uiPlaces.SelectedIndex].InnerHTML;
				text += view.uiMusicTypes.Value == "1" ? "" : ((view.uiPlaces.Value == "-1" ? " who" : " and") + " listen to " + view.uiMusicTypes.Options[view.uiMusicTypes.SelectedIndex].InnerHTML.Trim());
				value = "{'MusicTypeK' : '" + view.uiMusicTypes.Value + "','PlaceK' : '" + view.uiPlaces.Value + "'}";
			}
			else
			{
				value = "{'MusicTypeK' : '1','PlaceK' : '-1'}";
			}
			string id = "expandClicker" + Math.Floor(Math.Random() * 10000000);
			view.uiBuddyMultiSelector.AddItem(text + " - <a href=\"\" id=\"" + id + "\" class=\"MultiSelectorExpandButton\" onmouseover=\"stt('Expand this to show buddies (might take a while)');\" onmouseout=\"htm();\">show</a>", value);
			jQuery.FromElement(Document.GetElementById(id)).Click(
				delegate(jQueryEvent e)
				{
					Script.Literal("try{htm();}catch(e){}");
					e.PreventDefault();
					Service.ResolveUsrsFromMultiBuddyChooserValues(
						new string[] { value },
						delegate(Dictionary<string, object> result, object context, string methodName)
						{
							try
							{
								this.view.uiBuddyMultiSelector.RemoveItem(Document.GetElementById(id).ParentNode.ParentNode);
							}
							catch { }

							foreach (string key in result.Keys)
							{
								this.view.uiBuddyMultiSelector.AddItem(key.ToString(), (string)result[key]);
							}
						},
						Trace.WebServiceFailure,
						null,
						30000
					);
				}
			);
			ev.PreventDefault();
		}


		private void FillOptionArrayFromValues(Pair[] options, OptionElement[] array)
		{
			for (int i = 0; i < options.Length;i++ )
			{
				Pair de = options[i];
				OptionElement el = (OptionElement)Document.CreateElement("OPTION");

				el.InnerHTML = de.key.Unescape().Replace("&", "&amp;").Replace(" ", "&nbsp;");
			
				el.Value = de.value;
				array[array.Length] = el;
			}
		}
		
		void ShowAllTownsAndMusicCheckBoxClick(jQueryEvent ev)
		{
			if (allPlaces.Length == 0)
			{
				Service.GetPlacesAndMusicTypes(
					delegate(GetMusicTypesAndPlacesResult result, object userContext, string methodName)
					{
						if (allPlaces.Length == 0)
						{
							FillOptionArrayFromValues(result.musicTypes, allMusicTypes);
							FillOptionArrayFromValues(result.places, allPlaces);
						}
						FillSelect(this.view.uiMusicTypes, this.view.uiShowAllTownsAndMusic.Checked ? allMusicTypes : contextMusicTypes);
						FillSelect(this.view.uiPlaces, this.view.uiShowAllTownsAndMusic.Checked ? allPlaces : contextPlaces);
					},
					Trace.WebServiceFailure,
					null, 30000);
				return;
			}
			else
			{
				FillSelect(this.view.uiMusicTypes, this.view.uiShowAllTownsAndMusic.Checked ? allMusicTypes : contextMusicTypes);
				FillSelect(this.view.uiPlaces, this.view.uiShowAllTownsAndMusic.Checked ? allPlaces : contextPlaces);
			}
		}

		private void FillSelect(SelectElement el, OptionElement[] options)
		{
			ClearSelect(el);
			for (int i = 0; i < options.Length; i++)
			{
				el.AppendChild(options[i]);
			}
		}

		private static void ClearSelect(SelectElement el)
		{
			while (el.Options.Length > 0)
			{
				el.RemoveChild(el.Options[el.Options.Length - 1]);
			}
		}

		public void Clear()
		{
			try
			{
				this.view.uiBuddyMultiSelector.Clear();
			}
			catch 
			{
				// ignore
			}
		}
	}
}
