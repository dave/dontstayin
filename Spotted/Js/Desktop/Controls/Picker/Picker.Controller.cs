using System;
using System.Html;
using System.Collections.Generic;
using jQueryApi;
using Js.Library;
using jQueryApi.History;
using Js.jQuerySelectBoxesAPI;
using Js.ClientControls;

namespace Js.Controls.Picker
{
	public class Controller
	{

		//T (name) - reads the hidden fields and returns a proper var
		//void initialise(name) - adds click / change handlers
		//void (name)(control events) - reads values from controls and adds a history point, and then initialises the next controls in the sequence
		//void navigate(name) - sets the value of the control (if it's not correct), and updates the hidden fields
		//updateUI(name) - sets the visibility and state of the controls using the state of the whole control

		View view;
		
		#region public Controller(View view)
		public Controller(View view)
		{
			this.view = view;
			//Application.Navigate += new HistoryEventHandler(navigate);
			jQuery.Window.Bind("hashchange", navigate);
		
			if (Misc.BrowserIsIE)
				jQuery.OnDocumentReady(initialise);
			else
				initialise();
		}
		#endregion
		#region initialise()
		void initialise()
		{
			Debug("initialise");

			initialiseSearchType();
			initialiseKey();
			initialiseMe();
			initialiseSpotter();
			initialiseBrand();
			initialiseCountry();
			initialisePlace();
			initialiseVenue();
			initialiseMusic();
			initialiseDate();
			initialiseEvent();

			navigateSearchType(view.SelectedSearchTypeHidden.Value);
			navigateKey(view.SelectedKeyHidden.Value);
			navigateSpotter(view.SelectedSpotterHidden.Value);
			navigateBrand(view.SelectedBrandHidden.Value);
			navigateCountry(view.SelectedCountryHidden.Value);
			navigatePlace(view.SelectedPlaceHidden.Value);
			navigateVenue(view.SelectedVenueHidden.Value);
			navigateMusic(view.SelectedMusicHidden.Value);
			navigateDate(view.SelectedDateHidden.Value);
			navigateEvent(view.SelectedEventHidden.Value);
			
			if (Misc.BrowserIsIE)
				addHistory("", "");

			updateUI();
			
			initialiseFirstUnknownControl();

			view.Holder.Style.Display = "";

			jQuery.Window.Trigger("hashchange");

			initialiseFinished = true;

			if (initialNavigate)
			{
				navigate(initialHistoryEvent);
			}
		}
		#endregion
		#region Navigate
		bool firstEverNavigate = true;
		jQueryEvent initialHistoryEvent = null;
		bool initialNavigate = false;
		bool initialiseFinished = false;
		void navigate(jQueryEvent e)
		{
			Debug("navigate");

			if (!initialiseFinished)
			{
				initialNavigate = true;
				initialHistoryEvent = e;
				return;
			}

			if (jQueryHistory.GetState(view.clientId + "_SearchType") != null ||
				jQueryHistory.GetState(view.clientId + "_Key") != null ||
				jQueryHistory.GetState(view.clientId + "_Spotter") != null ||
				jQueryHistory.GetState(view.clientId + "_Brand") != null ||
				jQueryHistory.GetState(view.clientId + "_Country") != null ||
				jQueryHistory.GetState(view.clientId + "_Place") != null ||
				jQueryHistory.GetState(view.clientId + "_Venue") != null ||
				jQueryHistory.GetState(view.clientId + "_Music") != null ||
				jQueryHistory.GetState(view.clientId + "_Date") != null ||
				jQueryHistory.GetState(view.clientId + "_Event") != null)
			{
				navigateSearchType(getVal(e, "SearchType"));
				navigateKey(getVal(e, "Key"));
				navigateSpotter(getVal(e, "Spotter"));
				navigateBrand(getVal(e, "Brand"));
				navigateCountry(getVal(e, "Country"));
				navigatePlace(getVal(e, "Place"));
				navigateVenue(getVal(e, "Venue"));
				navigateMusic(getVal(e, "Music"));
				navigateDate(getVal(e, "Date"));
				navigateEvent(getVal(e, "Event"));

				if (firstEverNavigate)
				{
					//Debug("navigate - firstEverNavigate - EventSelectionSepcificationChanged != null = " + (EventSelectionSepcificationChanged != null));
					firstEverNavigate = false;
					updateUI();

					if (EventSelectionSepcificationChanged != null)
					{
						//Debug("navigate - EventSelectionSepcificationChanged");
						EventSelectionSepcificationChanged(this, new EventSelectionArgs(GetCurrentEventSelectionSepcification()));
					}

					if (Event != null && (SearchType == SearchTypes.Key || SearchType == SearchTypes.Brand || SearchType == SearchTypes.Venue) && SelectedEventChanged != null) SelectedEventChanged(this, new ObjectArgs(Event));
					if (Venue != null && SearchType == SearchTypes.Venue && SelectedVenueChanged != null) SelectedVenueChanged(this, new ObjectArgs(Venue));
					if (Place != null && (SearchType == SearchTypes.Venue || SearchType == SearchTypes.Music) && SelectedPlaceChanged != null) SelectedPlaceChanged(this, new ObjectArgs(Place));
					if (Country != null && (SearchType == SearchTypes.Venue || SearchType == SearchTypes.Music) && SelectedCountryChanged != null) SelectedCountryChanged(this, new ObjectArgs(Country));
					if (Brand != null && SearchType == SearchTypes.Brand && SelectedBrandChanged != null) SelectedBrandChanged(this, new ObjectArgs(Brand));
					if (Spotter != null && Spotter.Length > 0 && SearchType == SearchTypes.Spotter && SelectedSpotterChanged != null) SelectedSpotterChanged(this, new StringArgs(Spotter));
					if (Key != null && Key.Length > 0 && SearchType == SearchTypes.Key && SelectedKeyChanged != null) SelectedKeyChanged(this, new StringArgs(Key));
				}

				//updateUI();//?????????????????????????????????????????????
			}
		}
		string getVal(jQueryEvent e, string item)
		{
			object val = jQueryHistory.GetState(view.clientId + "_" + item);

			if (val == null)
				return "";
			else
				return val.ToString().DecodeUri();
		}
		#endregion
		#region updateUI
		void updateUI()
		{
			//Debug("updateUI");

			updateUISearchType();
			updateUIKey(false);
			updateUISpotter();
			updateUIBrand(false);
			updateUICountry(false);
			updateUIPlace(false);
			updateUIVenue(false);
			updateUIMusic(false);
			updateUIDate(false);
			updateUIEvent();
		}
		#endregion
		//public void HandlersSet()
		//{
		//    //navigate happens BEFORE our controllers start, so we have to wait until here.

		//    firstEverNavigate = true;

		//    if (initialNavigate)
		//        navigate(initialHistoryEvent);
		//}


		#region EventSelectionSepcification
		public EventSelectionEvent EventSelectionSepcificationChanged = null; //Fired when brand, venue, music, town, or date is changed
		public EventSelectionSpecification GetCurrentEventSelectionSepcification()
		{
			return new EventSelectionSpecification(
				SearchType == SearchTypes.Brand ? Brand : null,
				SearchType == SearchTypes.Venue || SearchType == SearchTypes.Music ? Place : null,
				SearchType == SearchTypes.Venue ? Venue : null,
				SearchType == SearchTypes.Music ? Music : null,
				Date,
				SearchType == SearchTypes.Me);
		}
		public void FireEventSelectionChange()
		{
			if (EventSelectionSepcificationChanged != null)
				EventSelectionSepcificationChanged(this, new EventSelectionArgs(GetCurrentEventSelectionSepcification()));
		}
		#endregion

		#region SearchType
		SearchTypes SearchType
		{
			get
			{
				if (SearchTypeHasMultipleOptions)
				{
					return
						view.SelectedSearchTypeHidden.Value == "key" ? SearchTypes.Key :
						view.SelectedSearchTypeHidden.Value == "me" ? SearchTypes.Me :
						view.SelectedSearchTypeHidden.Value == "spotter" ? SearchTypes.Spotter :
						view.SelectedSearchTypeHidden.Value == "venue" ? SearchTypes.Venue :
						view.SelectedSearchTypeHidden.Value == "brand" ? SearchTypes.Brand : 
						view.SelectedSearchTypeHidden.Value == "music" ? SearchTypes.Music : 
						view.SelectedSearchTypeHidden.Value == "google" ? SearchTypes.Google : SearchTypes.Unknown;
				}
				else if (OptionKey)
					return SearchTypes.Key;
				else if (OptionMe)
					return SearchTypes.Me;
				else if (OptionSpotter)
					return SearchTypes.Spotter;
				else if (OptionBrand)
					return SearchTypes.Brand;
				else if (OptionCountry)
					return SearchTypes.Venue;
				else if (OptionMusic)
					return SearchTypes.Music;
				else if (OptionGoogle)
					return SearchTypes.Google;
				else
					return SearchTypes.Unknown;
			}
		}
		bool SearchTypeHasMultipleOptions
		{
			get
			{
				int options = (OptionKey ? 1 : 0) + (OptionMe ? 1 : 0) + (OptionSpotter ? 1 : 0) + (OptionBrand ? 1 : 0) + (OptionCountry ? 1 : 0) + (OptionMusic ? 1 : 0) + (OptionGoogle ? 1 : 0);
				return options > 1;
			}
		}
		void initialiseSearchType()
		{
			view.SearchTypeKeyJ.Click(searchTypeRadioClick);
			view.SearchTypeMeJ.Click(searchTypeRadioClick);
			view.SearchTypeSpotterJ.Click(searchTypeRadioClick);
			view.SearchTypeVenueJ.Click(searchTypeRadioClick);
			view.SearchTypeBrandJ.Click(searchTypeRadioClick);
			view.SearchTypeMusicJ.Click(searchTypeRadioClick);
			view.SearchTypeGoogleJ.Click(searchTypeRadioClick);
			view.SearchTypeKey.ParentNode.Style.Display = OptionKey ? "block" : "none";
			view.SearchTypeMe.ParentNode.Style.Display = OptionMe ? "block" : "none";
			view.SearchTypeSpotter.ParentNode.Style.Display = OptionSpotter ? "block" : "none";
			view.SearchTypeVenue.ParentNode.Style.Display = OptionVenue ? "block" : "none";
			view.SearchTypeBrand.ParentNode.Style.Display = OptionBrand ? "block" : "none";
			view.SearchTypeMusic.ParentNode.Style.Display = OptionMusic ? "block" : "none";
			view.SearchTypeGoogle.ParentNode.Style.Display = OptionGoogle ? "block" : "none";
		}
		void searchTypeRadioClick(jQueryEvent e)
		{
			string value =
				view.SearchTypeKey.Checked ? "key" : 
				view.SearchTypeMe.Checked ? "me" : 
				view.SearchTypeSpotter.Checked ? "spotter" : 
				view.SearchTypeVenue.Checked ? "venue" : 
				view.SearchTypeBrand.Checked ? "brand" : 
				view.SearchTypeMusic.Checked ? "music" : 
				view.SearchTypeGoogle.Checked ? "google" : "unknown"; 

			if (value == "unknown")
			{
				if (SearchType == SearchTypes.Key)
					view.SearchTypeKey.Checked = true;
				else if (SearchType == SearchTypes.Me)
					view.SearchTypeMe.Checked = true;
				else if (SearchType == SearchTypes.Spotter)
					view.SearchTypeSpotter.Checked = true;
				else if (SearchType == SearchTypes.Venue)
					view.SearchTypeVenue.Checked = true;
				else if (SearchType == SearchTypes.Brand)
					view.SearchTypeBrand.Checked = true;
				else if (SearchType == SearchTypes.Music)
					view.SearchTypeMusic.Checked = true;
				else if (SearchType == SearchTypes.Google)
					view.SearchTypeGoogle.Checked = true;

				if (!firstEverNavigate)
				{
					if (EventSelectionSepcificationChanged != null)
						EventSelectionSepcificationChanged(this, new EventSelectionArgs(null));
				}
			}
			else
			{
				addHistory("SearchType", value);

				//Moved to navigateSearchType
				//initialiseFirstUnknownControl();
				//initialiseEventDropDown();
				//if (!firstEverNavigate)
				//{
				//	if (EventSelectionSepcificationChanged != null)
				//		EventSelectionSepcificationChanged(this, new EventSelectionArgs(GetCurrentEventSelectionSepcification()));
				//}
			}
		}
		void initialiseFirstUnknownControl()
		{
			if (SearchType == SearchTypes.Spotter)
			{
				if (Spotter.Length == 0)
					initialiseSpotterControl();
			}
			else if (SearchType == SearchTypes.Key)
			{
				if (Key.Length == 0)
					initialiseKeyControl();
			}
			else if (SearchType == SearchTypes.Brand)
			{
				if (Brand == null)
					initialiseBrandDropDown();
				else if (Event == null)
					initialiseEventDropDown();
			}
			else if (SearchType == SearchTypes.Venue || SearchType == SearchTypes.Music)
			{
				if (Country == null)
				{
					initialiseCountryDropDown();
				}
				else if (Place == null)
				{
					initialisePlaceDropDown();
				}
				else if (SearchType == SearchTypes.Venue && Venue == null || SearchType == SearchTypes.Music)
				{
					if (SearchType == SearchTypes.Venue && Venue == null)
					{
						initialiseVenueDropDown(null);
					}
					else if (SearchType == SearchTypes.Music)
					{
						initialiseMusicDropDown();
					}
				}
				else if (Event == null)
				{
					initialiseEventDropDown();
				}
			}
			else if (SearchType == SearchTypes.Google)
			{
				//TODO
			}
			
		}

		string navigateSearchTypePreviousValue = "";
		void navigateSearchType(string value)
		{
			//Debug("navigateSearchType");

			if (value.Length == 0 || value == navigateSearchTypePreviousValue) return;

			if (value == "key" && !view.SearchTypeKey.Checked) view.SearchTypeKey.Checked = true;
			if (value == "me" && !view.SearchTypeMe.Checked) view.SearchTypeMe.Checked = true;
			if (value == "spotter" && !view.SearchTypeSpotter.Checked) view.SearchTypeSpotter.Checked = true;
			if (value == "venue" && !view.SearchTypeVenue.Checked) view.SearchTypeVenue.Checked = true;
			if (value == "brand" && !view.SearchTypeBrand.Checked) view.SearchTypeBrand.Checked = true;
			if (value == "music" && !view.SearchTypeMusic.Checked) view.SearchTypeMusic.Checked = true;
			if (value == "google" && !view.SearchTypeGoogle.Checked) view.SearchTypeGoogle.Checked = true;

			view.SelectedSearchTypeHidden.Value = value;

			if (!firstEverNavigate)
				updateUI();

			navigateSearchTypePreviousValue = value;


			//Moved from searchTypeRadioClick
			initialiseFirstUnknownControl();
			initialiseEventDropDown();
			if (!firstEverNavigate)
			{
				if (EventSelectionSepcificationChanged != null)
					EventSelectionSepcificationChanged(this, new EventSelectionArgs(GetCurrentEventSelectionSepcification()));
			}
		}
		void updateUISearchType()
		{
			view.SearchTypeHolder.Style.Display = SearchTypeHasMultipleOptions ? "" : "none";
		}
		#endregion

		#region Key
		bool OptionKey { get { return bool.Parse(view.OptionKeyHidden.Value); } }
		public StringEvent SelectedKeyChanged = null;
		bool keyControlIsInitialised = false;

		string Key
		{
			get
			{
				if (!OptionKey || view.SelectedKeyHidden.Value == null)
					return "";

				return view.SelectedKeyHidden.Value;
			}
		}

		void initialiseKey()
		{
			if (!OptionKey)
				return;

			view.KeySearchButtonJ.Click(keyChange);
			view.KeySelectedChangeLinkJ.Click(keySelectedChangeLinkClick);
		}

		void keySelectedChangeLinkClick(jQueryEvent e)
		{
			e.PreventDefault();

			initialiseKeyControl();

		}
		void initialiseKeyControl()
		{
			if (!OptionKey)
				return;

			keyControlIsInitialised = true;

			if (Key != null && Key.Length > 0)
			{
				view.KeyTextBox.Value = Key;
			}

			updateUIKey(true);
		}
		void keyChange(jQueryEvent e)
		{
			e.PreventDefault();

			if (view.KeyTextBox.Value == null || view.KeyTextBox.Value.Length == 0)
				return;

			addHistory("Key", view.KeyTextBox.Value);

			//Moved to navigateKey
			//initialiseEventDropDown();
			
		}
		string navigateKeyPreviousValue = "";
		void navigateKey(string value)
		{
			if (!OptionKey || value.Length == 0 || value == navigateKeyPreviousValue) return;

			if (value == "0")
			{
				view.KeyTextBox.Value = "";
				view.KeySelectedLabel.InnerHTML = "None selected";
				view.SelectedKeyHidden.Value = "";

				if (!firstEverNavigate)
				{
					if (SelectedKeyChanged != null)
						SelectedKeyChanged(this, new StringArgs(null));
				}
			}
			else
			{
				if (keyControlIsInitialised && view.KeyTextBox.Value != value)
				{
					view.KeyTextBox.Value = value;
				}

				if (!keyControlIsInitialised)
					view.KeySelectedLabel.InnerHTML = value;

				view.SelectedKeyHidden.Value = value;

				if (!firstEverNavigate)
				{
					if (SelectedKeyChanged != null)
						SelectedKeyChanged(this, new StringArgs(value));
				}
			}

			navigateKeyPreviousValue = value;

			//Moved from keyChange
			initialiseEventDropDown();
		}

		void updateUIKey(bool recursive)
		{
			view.KeyHolder.Style.Display = OptionKey && SearchType == SearchTypes.Key ? "" : "none";

			view.KeySelectedHolder.Style.Display = !keyControlIsInitialised ? "" : "none";
			view.KeyChoiceHolder.Style.Display = keyControlIsInitialised ? "" : "none";

			if (recursive)
				updateUIEvent();

		}
		#endregion

		#region Me
		bool OptionMe { get { return bool.Parse(view.OptionMeHidden.Value); } }
		void initialiseMe()
		{

		}
		#endregion

		#region Spotter
		bool OptionSpotter { get { return bool.Parse(view.OptionSpotterHidden.Value); } }
		public StringEvent SelectedSpotterChanged = null;
		bool spotterControlIsInitialised = false;

		string Spotter
		{
			get
			{
				if (!OptionSpotter || view.SelectedSpotterHidden.Value == null)
					return "";

				return view.SelectedSpotterHidden.Value;
			}
		}

		void initialiseSpotter()
		{
			if (!OptionSpotter)
				return;

			view.SpotterSearchButtonJ.Click(spotterChange);
			view.SpotterSelectedChangeLinkJ.Click(spotterSelectedChangeLinkClick);
		}

		void spotterSelectedChangeLinkClick(jQueryEvent e)
		{
			e.PreventDefault();

			initialiseSpotterControl();

		}
		void initialiseSpotterControl()
		{
			if (!OptionSpotter)
				return;

			spotterControlIsInitialised = true;

			if (Spotter != null && Spotter.Length > 0)
			{
				view.SpotterTextBox.Value = Spotter;
			}

			updateUISpotter();
		}
		void spotterChange(jQueryEvent e)
		{
			e.PreventDefault();

			if (view.SpotterTextBox.Value == null || view.SpotterTextBox.Value.Length == 0)
				return;

			addHistory("Spotter", view.SpotterTextBox.Value);
		}
		string navigateSpotterPreviousValue = "";
		void navigateSpotter(string value)
		{
			if (!OptionSpotter || value.Length == 0 || value == navigateSpotterPreviousValue) return;

			if (value.Length == 0)
			{
				view.SpotterTextBox.Value = "";
				view.SpotterSelectedLabel.InnerHTML = "None selected";
				view.SelectedSpotterHidden.Value = "";

				if (!firstEverNavigate)
				{
					if (SelectedSpotterChanged != null)
						SelectedSpotterChanged(this, new StringArgs(null));
				}
			}
			else
			{
				if (spotterControlIsInitialised && view.SpotterTextBox.Value != value)
				{
					view.SpotterTextBox.Value = value;
				}

				if (!spotterControlIsInitialised)
					view.SpotterSelectedLabel.InnerHTML = value;

				view.SelectedSpotterHidden.Value = value;

				if (!firstEverNavigate)
				{
					if (SelectedSpotterChanged != null)
						SelectedSpotterChanged(this, new StringArgs(value));
				}
			}

			navigateSpotterPreviousValue = value;
		}

		void updateUISpotter()
		{
			view.SpotterHolder.Style.Display = OptionSpotter && SearchType == SearchTypes.Spotter ? "" : "none";

			view.SpotterSelectedHolder.Style.Display = !spotterControlIsInitialised ? "" : "none";
			view.SpotterChoiceHolder.Style.Display = spotterControlIsInitialised ? "" : "none";

		}
		#endregion

		#region Brand
		bool OptionBrand { get { return bool.Parse(view.OptionBrandHidden.Value); } }
		public ObjectEvent SelectedBrandChanged = null;
		bool brandDropDownIsInitialised = false;

		ObjectStub Brand
		{
			get
			{
				if (!OptionBrand || view.SelectedBrandHidden.Value == null || view.SelectedBrandHidden.Value.Length == 0)
					return null;

				return ObjectStub.FromString(view.SelectedBrandHidden.Value);
			}
		}

		void initialiseBrand()
		{
			if (!OptionBrand)
				return;

			view.BrandAutoComplete.ItemChosen = new KeyStringPairAction(brandDropDownChange);
			view.BrandSelectedChangeLinkJ.Click(brandSelectedChangeLinkClick);
		}

		void brandSelectedChangeLinkClick(jQueryEvent e)
		{
			e.PreventDefault();

			initialiseBrandDropDown();

		}
		void initialiseBrandDropDown()
		{
			if (!OptionBrand)
				return;
		
			brandDropDownIsInitialised = true;

			if (Brand != null)
			{
				view.BrandAutoComplete.Value = Brand.K.ToString();
				view.BrandAutoComplete.Text = Brand.Name;
			}

			updateUIBrand(true);
		}
		void brandDropDownChange(KeyStringPair e)
		{
			if (e.Value == null || e.Value.Length == 0)
				return;

			ObjectStub brand = new ObjectStub(int.Parse(e.Value, 10), e.Key);
			if (brand.K == 0)
			{
				return;
			}
			else
			{
				addHistory("Brand", brand.ToString());

				//moved to navigateBrand
				//initialiseDateDropDowns();
				//initialiseEventDropDown();
			}
		}
		string navigateBrandPreviousValue = "";
		void navigateBrand(string value)
		{
			if (!OptionBrand || value.Length == 0 || value == navigateBrandPreviousValue) return;

			if (value == "0")
			{
				view.BrandAutoComplete.Text = "";
				view.BrandAutoComplete.Value = "";
				view.BrandSelectedLabel.InnerHTML = "None selected";
				view.SelectedBrandHidden.Value = "";

				if (!firstEverNavigate)
				{
					if (SelectedBrandChanged != null)
						SelectedBrandChanged(this, new ObjectArgs(null));

					if (EventSelectionSepcificationChanged != null)
						EventSelectionSepcificationChanged(this,
							new EventSelectionArgs(null));
				}				
			}
			else
			{
				ObjectStub brand = ObjectStub.FromString(value);

				if (brandDropDownIsInitialised && view.BrandAutoComplete.Value != brand.K.ToString())
				{
					view.BrandAutoComplete.Value = brand.K.ToString();
					view.BrandAutoComplete.Text = brand.Name;
				}

				if (!brandDropDownIsInitialised)
					view.BrandSelectedLabel.InnerHTML = brand.Name;

				view.SelectedBrandHidden.Value = brand.ToString();

				if (!firstEverNavigate)
				{
					if (SelectedBrandChanged != null)
						SelectedBrandChanged(this, new ObjectArgs(brand));

					if (EventSelectionSepcificationChanged != null)
						EventSelectionSepcificationChanged(this,
							new EventSelectionArgs(
								new EventSelectionSpecification(
									SearchType == SearchTypes.Brand ? brand : null,
									SearchType == SearchTypes.Venue || SearchType == SearchTypes.Music ? Place : null,
									SearchType == SearchTypes.Venue ? Venue : null,
									SearchType == SearchTypes.Music ? Music : null,
									Date,
									SearchType == SearchTypes.Me)));
				}
			}

			navigateBrandPreviousValue = value;

			//moved from brandDropDownChange
			initialiseDateDropDowns();
			initialiseEventDropDown();
		}

		void updateUIBrand(bool recursive)
		{
			view.BrandHolder.Style.Display = OptionBrand && SearchType == SearchTypes.Brand ? "" : "none";

			view.BrandSelectedHolder.Style.Display = !brandDropDownIsInitialised ? "" : "none";
			view.BrandChoiceHolder.Style.Display = brandDropDownIsInitialised ? "" : "none";

			if (recursive)
				updateUIDate(true);

		}
		#endregion

		#region Country
		bool OptionCountry { get { return bool.Parse(view.OptionCountryHidden.Value); } }
		public ObjectEvent SelectedCountryChanged = null;
		//JQueryObject CountryDropDownJ;
		bool countryDropDownIsInitialised = false;

		ObjectStub Country
		{
			get
			{
				if (!OptionCountry || view.SelectedCountryHidden.Value == null || view.SelectedCountryHidden.Value.Length == 0)
					return null;

				return ObjectStub.FromString(view.SelectedCountryHidden.Value);
			}
		}

		void initialiseCountry()
		{
			if (!OptionCountry)
				return;

			view.CountryDropDownJ.Change(countryDropDownChange);
			view.CountrySelectedChangeLinkJ.Click(countrySelectedChangeLinkClick);
		}

		void countrySelectedChangeLinkClick(jQueryEvent e)
		{
			e.PreventDefault();

			initialiseCountryDropDown();
			
		}
		void initialiseCountryDropDown()
		{
			if (!OptionCountry)
				return;

			if (!countryDropDownIsInitialised)
				jQuerySelectBoxes.FromObject(view.CountryDropDown).AjaxAddOption("/support/getcached.aspx?type=country", null, false, new ActionObject(countryDropDownInitialised), null);
			else
				countryDropDownInitialised(null);
		}
		void countryDropDownInitialised(object args)
		{
			countryDropDownIsInitialised = true;

			if (Country != null)
			{
				bool setValue = setK(view.CountryDropDown, Country.K);

				if (!setValue)
					addHistorys(new string[] {"Country", "0", "Place", "0", "Venue", "0", "Event", "0"});
			}

			updateUICountry(true);
		}
		void countryDropDownChange(jQueryEvent e)
		{
			ObjectStub country = new ObjectStub(getK(view.CountryDropDown), getText(view.CountryDropDown));
			if (country.K == 0)
			{
				if (Country == null)
					setIndex(view.CountryDropDown, 0);
				else
					setK(view.CountryDropDown, Country.K);
			}
			else
			{
				addHistory("Country", country.ToString());

				//moved to navigateCountry
				//initialisePlaceDropDown();
			}
		}
		string navigateCountryPreviousValue = "";
		void navigateCountry(string value)
		{
			if (!OptionCountry || value.Length == 0 || value == navigateCountryPreviousValue) return;

			if (value == "0")
			{
				setIndex(view.CountryDropDown, 0);
				view.CountrySelectedLabel.InnerHTML = "None selected";
				view.SelectedCountryHidden.Value = "";

				if (SelectedCountryChanged != null)
					SelectedCountryChanged(this, new ObjectArgs(null));
			}
			else
			{

				ObjectStub country = ObjectStub.FromString(value);

				if (countryDropDownIsInitialised && getK(view.CountryDropDown) != country.K)
					setK(view.CountryDropDown, country.K);

				if (!countryDropDownIsInitialised)
					view.CountrySelectedLabel.InnerHTML = country.Name;

				view.SelectedCountryHidden.Value = country.ToString();

				if (SelectedCountryChanged != null)
					SelectedCountryChanged(this, new ObjectArgs(country));
			}

			navigateCountryPreviousValue = value;

			//moved from countryDropDownChange
			initialisePlaceDropDown();
		}

		void updateUICountry(bool recursive)
		{
			view.CountryHolder.Style.Display = OptionCountry && (SearchType == SearchTypes.Venue || SearchType == SearchTypes.Music) ? "" : "none";

			view.CountrySelectedHolder.Style.Display = !countryDropDownIsInitialised ? "" : "none";
			view.CountryChoiceHolder.Style.Display = countryDropDownIsInitialised ? "" : "none";

			if (recursive)
				updateUIPlace(true);

		}
		#endregion

		#region Place
		bool OptionPlace { get { return bool.Parse(view.OptionPlaceHidden.Value); } }
		public ObjectEvent SelectedPlaceChanged = null;
		//JQueryObject PlaceDropDownJ;
		bool placeDropDownIsInitialised = false;
		int placeDropDownCountryK;
		int placeDropDownPreviouslySelectedIndex;

		ObjectStub Place
		{
			get
			{
				if (!OptionPlace || view.SelectedPlaceHidden.Value == null || view.SelectedPlaceHidden.Value.Length == 0)
					return null;

				return ObjectStub.FromString(view.SelectedPlaceHidden.Value);
			}
		}

		void initialisePlace()
		{
			//Debug("initialisePlace");

			if (!OptionPlace)
				return;

			//PlaceDropDownJ = JQueryAPI.JQuery(view.PlaceDropDown);
			view.PlaceDropDownJ.Change(placeDropDownChange);
			view.PlaceSelectedChangeLinkJ.Click(placeSelectedChangeLinkClick);
		}

		void placeSelectedChangeLinkClick(jQueryEvent e)
		{
			e.PreventDefault();

			initialisePlaceDropDown();

		}
		void initialisePlaceDropDown()
		{
			//Debug("initialisePlaceDropDown");

			if (!OptionPlace)
				return;

			if (!placeDropDownIsInitialised || placeDropDownCountryK != Country.K)
			{
				placeDropDownCountryK = Country.K;
				jQuerySelectBoxes.FromObject(view.PlaceDropDown).AjaxAddOption("/support/getcached.aspx?type=place&countryk=" + Country.K + "&return=k", null, false, new ActionObject(placeDropDownInitialised), null);
			}
			else
				placeDropDownInitialised(null);
		}
		void placeDropDownInitialised(object args)
		{
			//Debug("placeDropDownInitialised");

			placeDropDownIsInitialised = true;

			if (Place != null)
			{
				bool setValue = setK(view.PlaceDropDown, Place.K);

				if (!setValue)
					addHistorys(new string[] { "Place", "0", "Venue", "0", "Event", "0" });
			}

			placeDropDownPreviouslySelectedIndex = getIndex(view.PlaceDropDown);

			updateUIPlace(true);
		}
		void placeDropDownChange(jQueryEvent e)
		{
			//Debug("placeDropDownChange");

			int k = getK(view.PlaceDropDown);
			if (k > 0)
			{
				ObjectStub place = new ObjectStub(getK(view.PlaceDropDown), getText(view.PlaceDropDown));
				addHistory("Place", place.ToString());

				//Moved to navigatePlace
				//initialiseVenueDropDown(null);
				//initialiseDateDropDowns();
				//initialiseMusicDropDown();
			}
			else
			{
				//user has clicked on one of the description or padding items... must reset to what was previously selected.
				setIndex(view.PlaceDropDown, placeDropDownPreviouslySelectedIndex);
			}
			placeDropDownPreviouslySelectedIndex = getIndex(view.PlaceDropDown);
		}
		string navigatePlacePreviousValue = "";
		void navigatePlace(string value)
		{
			//Debug("navigatePlace value=" + value);

			if (!OptionPlace || value.Length == 0 || value == navigatePlacePreviousValue) return;

			if (value == "0")
			{
				setIndex(view.PlaceDropDown, 0);
				view.PlaceSelectedLabel.InnerHTML = "None selected";
				view.SelectedPlaceHidden.Value = "";

				if (!firstEverNavigate)
				{
					if (SelectedPlaceChanged != null)
						SelectedPlaceChanged(this, new ObjectArgs(null));

					if (EventSelectionSepcificationChanged != null)
						EventSelectionSepcificationChanged(this,
							new EventSelectionArgs(null));
				}
			}
			else
			{
				
				ObjectStub place = ObjectStub.FromString(value);

				if (placeDropDownIsInitialised && getK(view.PlaceDropDown) != place.K)
					setK(view.PlaceDropDown, place.K);

				if (!placeDropDownIsInitialised)
					view.PlaceSelectedLabel.InnerHTML = place.Name;

				view.SelectedPlaceHidden.Value = place.ToString();

				if (!firstEverNavigate)
				{
					if (SelectedPlaceChanged != null)
					{
						SelectedPlaceChanged(this, new ObjectArgs(place));
					}

					if (EventSelectionSepcificationChanged != null)
					{
						EventSelectionSepcificationChanged(this,
							new EventSelectionArgs(
								new EventSelectionSpecification(
									SearchType == SearchTypes.Brand ? Brand : null,
									SearchType == SearchTypes.Venue || SearchType == SearchTypes.Music ? place : null,
									SearchType == SearchTypes.Venue ? Venue : null,
									SearchType == SearchTypes.Music ? Music : null,
									Date,
									SearchType == SearchTypes.Me)));
					}
				}
			}

			navigatePlacePreviousValue = value;

			//moved from placeDropDownChange
			initialiseVenueDropDown(null);
			initialiseDateDropDowns();
			initialiseMusicDropDown();
		}

		void updateUIPlace(bool recursive)
		{
			//Debug("updateUIPlace");

			view.PlaceHolder.Style.Display = 
				OptionPlace &&
				(SearchType == SearchTypes.Venue || SearchType == SearchTypes.Music) && Country != null && 
				(!placeDropDownIsInitialised || placeDropDownCountryK == Country.K) 
				? "" : "none";

			view.PlaceSelectedHolder.Style.Display = !placeDropDownIsInitialised ? "" : "none";
			view.PlaceChoiceHolder.Style.Display = placeDropDownIsInitialised ? "" : "none";

			if (recursive)
				updateUIVenue(true);
		}
		#endregion

		#region Venue
		bool OptionVenue { get { return bool.Parse(view.OptionVenueHidden.Value); } }
		public ObjectEvent SelectedVenueChanged = null;
		//JQueryObject VenueDropDownJ;
		//JQueryObject VenueByLetterDropDownJ;
		bool venueDropDownIsInitialised = false;
		int venueDropDownPlaceK;
		bool venueByLetterDropDownIsInitialised = false;
		int venueByLetterDropDownPlaceK;
		string venueByLetterDropDownLetter;

		int venueDropDownPreviouslySelectedIndex;

		ObjectStub Venue
		{
			get
			{
				if (!OptionVenue || view.SelectedVenueHidden.Value == null || view.SelectedVenueHidden.Value.Length == 0)
					return null;

				return ObjectStub.FromString(view.SelectedVenueHidden.Value);
			}
		}
		
		bool VenueDropDownIsVenueSelectedCurrently { get { return VenueDropDownVenueSelectedCurrently > 0; } }
		int VenueDropDownVenueSelectedCurrently
		{
			get
			{
				return getK(view.VenueDropDown);
			}
		}
		bool VenueDropDownIsLetterSelectedCurrently { get { return VenueDropDownLetterSelectedCurrently.Length > 0; } }
		string VenueDropDownLetterSelectedCurrently
		{
			get
			{
				string value = getValue(view.VenueDropDown);
				if (value.IndexOf("*") > -1)
					return value.Substr(value.Length - 1, 1);
				else
					return "";
			}
		}
		bool VenueByLetterDropDownIsVenueSelectedCurrently { get { return VenueByLetterDropDownVenueSelectedCurrently > 0; } }
		int VenueByLetterDropDownVenueSelectedCurrently
		{
			get
			{
				return getK(view.VenueByLetterDropDown);
			}
		}

		void initialiseVenue()
		{
			if (!OptionVenue)
				return;

			//VenueDropDownJ = JQueryAPI.JQuery(view.VenueDropDown);
			//VenueByLetterDropDownJ = JQueryAPI.JQuery(view.VenueByLetterDropDown);
			view.VenueDropDownJ.Change(venueDropDownChange);
			view.VenueByLetterDropDownJ.Change(venueByLetterDropDownChange);
			view.VenueSelectedChangeLinkJ.Click(venueSelectedChangeLinkClick);
		}

		void venueSelectedChangeLinkClick(jQueryEvent e)
		{
			e.PreventDefault();

			initialiseVenueDropDown(null);

		}
		void initialiseVenueDropDown(ObjectStub changeVenue)
		{
			//Debug("initialiseVenueDropDown changeVenue=" + (changeVenue == null ? "null" : changeVenue.ToString()));

			if (!OptionVenue)
				return;

			if (!venueDropDownIsInitialised || venueDropDownPlaceK != Place.K)
			{
				//Debug("getting venues");
				venueDropDownPlaceK = Place.K;
				jQuerySelectBoxes.FromObject(view.VenueDropDown).AjaxAddOption(
					"/support/getcached.aspx?type=venue&placek=" + Place.K, 
					null, 
					false, 
					new ActionObject(venueDropDownInitialised),
					new object[] { changeVenue }
				);
			}
			else
			{
				venueDropDownInitialised(changeVenue);
			}
		}
		
		void venueDropDownInitialised(object args)
		{
			//Debug("venueDropDownInitialised");
			ObjectStub changeVenue = (ObjectStub)args;
			venueDropDownIsInitialised = true;

			if (Venue != null)
			{
				bool setValue = setK(view.VenueDropDown, Venue.K);

				if (!setValue)
					addHistorys(new string[] { "Venue", "0", "Event", "0" });
			}

			if (changeVenue != null)
			{
				bool setValue = setK(view.VenueDropDown, changeVenue.K);
				if (setValue)
					venueDropDownChange(null);
			}

			venueDropDownPreviouslySelectedIndex = getIndex(view.VenueDropDown);

			updateUIVenue(true);
		}

		
		void venueDropDownChange(jQueryEvent e)
		{
			if (VenueDropDownIsVenueSelectedCurrently)
			{	
				ObjectStub venue = new ObjectStub(VenueDropDownVenueSelectedCurrently, getText(view.VenueDropDown));
				addHistory("Venue", venue.ToString());

				//Moved to navigateVenue
				//updateUIVenue(true); // This will update the venueByLetterDropDown if it's in the wrong visibility state...
				//initialiseDateDropDowns();
				//initialiseEventDropDown();
			}
			else if (VenueDropDownIsLetterSelectedCurrently)
			{
				string letter = VenueDropDownLetterSelectedCurrently;
				if (!venueByLetterDropDownIsInitialised || venueByLetterDropDownPlaceK != Place.K || venueByLetterDropDownLetter != letter)
				{
					venueByLetterDropDownPlaceK = Place.K;
					venueByLetterDropDownLetter = letter;
					jQuerySelectBoxes.FromObject(view.VenueByLetterDropDown).AjaxAddOption("/support/getcached.aspx?type=venuebyletter&placek=" + Place.K + "&letter=" + letter, null, false, new ActionObject(venueByLetterDropDownInitialised), null);
				}
				else
					updateUIVenue(true);
			}
			else
			{
				//user has clicked on one of the description or padding items... must reset to what was previously selected.
				setIndex(view.VenueDropDown, venueDropDownPreviouslySelectedIndex);
			}
			venueDropDownPreviouslySelectedIndex = getIndex(view.VenueDropDown);
		}
		void venueByLetterDropDownChange(jQueryEvent e)
		{
			if (VenueByLetterDropDownIsVenueSelectedCurrently)
			{
				ObjectStub venue = new ObjectStub(VenueByLetterDropDownVenueSelectedCurrently, getText(view.VenueByLetterDropDown));
				addHistory("Venue", venue.ToString());
				
				//Moved to navigateVenue
				//initialiseDateDropDowns();
				//initialiseEventDropDown();
			}
		}
		void venueByLetterDropDownInitialised(object args)
		{
			venueByLetterDropDownIsInitialised = true;
			addHistorys(new string[] { "Venue", "0", "Event", "0" });
			updateUIVenue(true);
		}
		string navigateVenuePreviousValue = "";
		void navigateVenue(string value)
		{
			if (!OptionVenue || value.Length == 0 || value == navigateVenuePreviousValue) return;

			if (value == "0")
			{
				if (!VenueDropDownIsLetterSelectedCurrently)
					setIndex(view.VenueDropDown, 0);

				view.VenueSelectedLabel.InnerHTML = "None selected";
				view.SelectedVenueHidden.Value = "";

				if (!firstEverNavigate)
				{
					if (SelectedVenueChanged != null)
						SelectedVenueChanged(this, new ObjectArgs(null));

					if (EventSelectionSepcificationChanged != null)
						EventSelectionSepcificationChanged(this,
							new EventSelectionArgs(null));
				}
			}
			else
			{

				ObjectStub venue = ObjectStub.FromString(value);

				if (venueDropDownIsInitialised && getK(view.VenueDropDown) != venue.K)
					setK(view.VenueDropDown, venue.K);

				if (!venueDropDownIsInitialised)
					view.VenueSelectedLabel.InnerHTML = venue.Name;

				view.SelectedVenueHidden.Value = venue.ToString();

				if (!firstEverNavigate)
				{
					if (SelectedVenueChanged != null)
						SelectedVenueChanged(this, new ObjectArgs(venue));

					if (EventSelectionSepcificationChanged != null)
						EventSelectionSepcificationChanged(this,
							new EventSelectionArgs(
								new EventSelectionSpecification(
									SearchType == SearchTypes.Brand ? Brand : null,
									SearchType == SearchTypes.Venue || SearchType == SearchTypes.Music ? Place : null,
									SearchType == SearchTypes.Venue ? venue : null,
									SearchType == SearchTypes.Music ? Music : null,
									Date,
									SearchType == SearchTypes.Me)));
				}
			}

			navigateVenuePreviousValue = value;


			//Added by DaveB (taken from venueDropDownChange)
			updateUIVenue(true); // This will update the venueByLetterDropDown if it's in the wrong visibility state...
			initialiseDateDropDowns();
			initialiseEventDropDown();
		}

		void updateUIVenue(bool recursive)
		{
			view.VenueHolder.Style.Display = 
				OptionVenue &&
				SearchType == SearchTypes.Venue && 
				Place != null &&
				(!venueDropDownIsInitialised || venueDropDownPlaceK == Place.K) 
				? "" : "none";

			view.VenueByLetterDropDown.Style.Display =
				OptionVenue &&
				venueByLetterDropDownIsInitialised && 
				Place != null && 
				venueByLetterDropDownPlaceK == Place.K && 
				VenueDropDownIsLetterSelectedCurrently && 
				venueByLetterDropDownLetter == VenueDropDownLetterSelectedCurrently 
				? "" : "none";

			view.VenueSelectedHolder.Style.Display = !venueDropDownIsInitialised ? "" : "none";
			view.VenueChoiceHolder.Style.Display = venueDropDownIsInitialised ? "" : "none";

			if (recursive)
				updateUIMusic(true);
		}
		#endregion

		#region Music
		bool OptionMusic { get { return bool.Parse(view.OptionMusicHidden.Value); } }
		public ObjectEvent SelectedMusicChanged = null;
		//JQueryObject MusicDropDownJ;
		bool musicDropDownIsInitialised = false;
		int musicDropDownPreviouslySelectedIndex;

		ObjectStub Music
		{
			get
			{
				if (!OptionMusic || view.SelectedMusicHidden.Value == null || view.SelectedMusicHidden.Value.Length == 0)
					return null;

				return ObjectStub.FromString(view.SelectedMusicHidden.Value);
			}
		}

		bool MusicIsSelectedCurrently { get { return MusicSelectedCurrently > 0; } }
		int MusicSelectedCurrently
		{
			get
			{
				return getK(view.MusicDropDown);
			}
		}
		
		void initialiseMusic()
		{
			if (!OptionMusic)
				return;

			//MusicDropDownJ = JQueryAPI.JQuery(view.MusicDropDown);
			view.MusicDropDownJ.Change(musicDropDownChange);
			view.MusicSelectedChangeLinkJ.Click(musicSelectedChangeLinkClick);
		}

		void musicSelectedChangeLinkClick(jQueryEvent e)
		{
			e.PreventDefault();

			initialiseMusicDropDown();

		}
		void initialiseMusicDropDown()
		{
			if (!OptionMusic)
				return;

			if (!musicDropDownIsInitialised)
			{
				jQuerySelectBoxes.FromObject(view.MusicDropDown).AjaxAddOption("/support/getcached.aspx?type=music", null, false, new ActionObject(musicDropDownInitialised), null);
			}
			else
				musicDropDownInitialised(null);
		}
		void musicDropDownInitialised(object args)
		{
			musicDropDownIsInitialised = true;

			if (Music != null)
			{
				bool setValue = setK(view.MusicDropDown, Music.K);

				if (!setValue)
					addHistorys(new string[] { "Music", "0", "Event", "0" });
			}

			musicDropDownPreviouslySelectedIndex = getIndex(view.MusicDropDown);

			updateUIMusic(true);
		}
		void musicDropDownChange(jQueryEvent e)
		{
			if (MusicIsSelectedCurrently)
			{
				string musicTypeName = getText(view.MusicDropDown);
				if (musicTypeName.Substr(0, 4) == "... ")
					musicTypeName = musicTypeName.Substr(4);
				ObjectStub music = new ObjectStub(MusicSelectedCurrently, musicTypeName);
				addHistory("Music", music.ToString());

				//Moved to navigateMusic
				//initialiseDateDropDowns();

			}
			else
			{
				//user has clicked on one of the description or padding items... must reset to what was previously selected.
				setIndex(view.MusicDropDown, musicDropDownPreviouslySelectedIndex);
			}
			musicDropDownPreviouslySelectedIndex = getIndex(view.MusicDropDown);
		}
		string navigateMusicPreviousValue = "";
		void navigateMusic(string value)
		{
			if (!OptionMusic || value.Length == 0 || value == navigateMusicPreviousValue) return;

			if (value == "0")
			{
				setIndex(view.MusicDropDown, 0);
				view.MusicSelectedLabel.InnerHTML = "None selected";
				view.SelectedMusicHidden.Value = "";

				if (!firstEverNavigate)
				{
					if (SelectedMusicChanged != null)
						SelectedMusicChanged(this, new ObjectArgs(null));

					if (EventSelectionSepcificationChanged != null)
						EventSelectionSepcificationChanged(this,
							new EventSelectionArgs(null));
				}
			}
			else
			{

				ObjectStub music = ObjectStub.FromString(value);

				if (musicDropDownIsInitialised && getK(view.MusicDropDown) != music.K)
					setK(view.MusicDropDown, music.K);

				if (!musicDropDownIsInitialised)
					view.MusicSelectedLabel.InnerHTML = music.Name;

				view.SelectedMusicHidden.Value = music.ToString();

				if (!firstEverNavigate)
				{
					if (SelectedMusicChanged != null)
						SelectedMusicChanged(this, new ObjectArgs(music));

					if (EventSelectionSepcificationChanged != null)
						EventSelectionSepcificationChanged(this,
							new EventSelectionArgs(
								new EventSelectionSpecification(
									SearchType == SearchTypes.Brand ? Brand : null,
									SearchType == SearchTypes.Venue || SearchType == SearchTypes.Music ? Place : null,
									SearchType == SearchTypes.Venue ? Venue : null,
									SearchType == SearchTypes.Music ? music : null,
									Date,
									SearchType == SearchTypes.Me)));
				}
			}

			navigateMusicPreviousValue = value;

			//Moved here from musicDropDownChange
			initialiseDateDropDowns();
		}

		void updateUIMusic(bool recursive)
		{
			view.MusicHolder.Style.Display = 
				OptionMusic &&
				SearchType == SearchTypes.Music && 
				Place != null
				? "" : "none";

			view.MusicSelectedHolder.Style.Display = !musicDropDownIsInitialised ? "" : "none";
			view.MusicChoiceHolder.Style.Display = musicDropDownIsInitialised ? "" : "none";

			if (recursive)
				updateUIDate(true);
		}

		#endregion

		#region Date
		bool OptionDate { get { return bool.Parse(view.OptionDateHidden.Value); } }
		bool OptionDateDay { get { return bool.Parse(view.OptionDateDayHidden.Value); } }
		int OptionDateDayIncrement { get { return int.Parse(view.OptionDateDayIncrementHidden.Value, 10); } }
		public DateEvent SelectedDateChanged = null;
		//jQueryObject DateDayDropDownJ;
		bool dateDropDownsAreInitialised = false;

		DateStub Date
		{
			get
			{
				if (!OptionDate || view.SelectedDateHidden.Value == null || view.SelectedDateHidden.Value.Length == 0)
				{
					Date d = new Date();
					return new DateStub(
						d.GetFullYear(),
						d.GetMonth() + 1,
						OptionDateDay ? d.GetDate() : 0);
				}

				DateStub date = DateStub.FromString(view.SelectedDateHidden.Value);
				if (!OptionDateDay)
					date.Day = 0;
				return date;
			}
		}
		string[] DateMonths;
		void initialiseDate()
		{
			//Debug("initialiseDate");

			if (!OptionDate)
				return;

			//DateDayDropDownJ = JQueryAPI.JQuery(view.DateDayDropDown);
			DateMonths = new string[] { "", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
			view.DateDayDropDownJ.Change(dateDayDropDownChange);
			view.DateMonthDropDownJ.Change(dateMonthDropDownChange);
			view.DateYearTextBoxJ.Change(dateYearTextBoxChange);
			view.DateYearTextBoxJ.Keyup(dateYearTextBoxKeyUp);
			view.DateYearPlusImgJ.Click(dateYearPlusClick);
			view.DateYearMinusImgJ.Click(dateYearMinusClick);
			view.DateSelectedChangeLinkJ.Click(dateSelectedChangeLinkClick);
			view.DateMonthPlusImgJ.Click(dateMonthPlusClick);
			view.DateMonthMinusImgJ.Click(dateMonthMinusClick);
			view.DateDayPlusImgJ.Click(dateDayPlusClick);
			view.DateDayMinusImgJ.Click(dateDayMinusClick);
		}

		void dateSelectedChangeLinkClick(jQueryEvent e)
		{
			e.PreventDefault();

			initialiseDateDropDowns();

		}
		void initialiseDateDropDowns()
		{
			//Debug("initialiseDateDropDowns");

			if (!OptionDate)
				return;

			if (!dateDropDownsAreInitialised)
			{
				for (int i = 1; i <= 12; i++)
				{
					addOption(
						view.DateMonthDropDown,
						i.ToString(),
						DateMonths[i]);
				}
			
				DateStub d = Date;
				
				view.DateYearTextBox.Value = d.Year.ToString();
				setValue(view.DateMonthDropDown, d.Month.ToString());
				dateRefreshDays(d.Year, d.Month);
				setValue(view.DateDayDropDown, d.Day.ToString());
				dateDropDownsAreInitialised = true;

			}

			updateUIDate(true);

		}
		bool dateRefreshDaysHasRunBefore = false;
		int dateRefreshDaysPreviousNumberOfDaysInMonth = 0;
		int dateRefreshDaysPreviousFirstDayOfWeek = 0;
		void dateRefreshDays(int year, int month)
		{
			//Debug("dateRefreshDays year:" + year.ToString() + " month: "+month.ToString() );

			if (!OptionDateDay)
				return;

			int days = DateStub.DaysInMonth(year, month);
			int firstDayOfWeek = new Date(year, month - 1, 1).GetDay();

			if (!dateRefreshDaysHasRunBefore ||
				dateRefreshDaysPreviousNumberOfDaysInMonth != days ||
				dateRefreshDaysPreviousFirstDayOfWeek != firstDayOfWeek)
			{
				dateRefreshDaysPreviousNumberOfDaysInMonth = days;
				dateRefreshDaysPreviousFirstDayOfWeek = firstDayOfWeek;
				int previouslySelectedIndex = getIndex(view.DateDayDropDown);

				jQuerySelectBoxes.FromObject(view.DateDayDropDown).Empty();
				for (int i = 1; i <= days; i++)
				{
					int dayOfWeek = new Date(year, month - 1, i).GetDay();
					//OptionElement op = (OptionElement)Document.CreateElement("OPTION");
					//op.Value = i.ToString();
					//op.Text = (i < 10 ? "0" : "") + i.ToString() + (dayOfWeek == 6 ? " Sat" : dayOfWeek == 0 ? " Sun" : "");
					//view.DateDayDropDown.Add(op, null);
					addOption(
						view.DateDayDropDown,
						i.ToString(),
						(i < 10 ? "0" : "") + i.ToString() + (dayOfWeek == 6 ? " Sat" : dayOfWeek == 0 ? " Sun" : ""));

				}
				if (previouslySelectedIndex > -1)
					setIndex(view.DateDayDropDown, previouslySelectedIndex);

				dateRefreshDaysHasRunBefore = true;
			}
		}
		

		void dateDayDropDownChange(jQueryEvent e)
		{
			//Debug("dateDayDropDownChange");

			int newDay = getValueInt(view.DateDayDropDown);
			DateStub newDate = new DateStub(Date.Year, Date.Month, newDay);
			addHistory("Date", newDate.ToString());

			//Moved to navigateDate
			//initialiseEventDropDown();
		}
		void dateMonthDropDownChange(jQueryEvent e)
		{
			//Debug("dateMonthDropDownChange");

			int newMonth = getValueInt(view.DateMonthDropDown);
			dateRefreshDays(Date.Year, newMonth);
			int newDay = OptionDateDay ? getValueInt(view.DateDayDropDown) : 0;
			DateStub newDate = new DateStub(Date.Year, newMonth, newDay);
			addHistory("Date", newDate.ToString());

			//Moved to navigateDate
			//initialiseEventDropDown();
		}
		void dateYearTextBoxKeyUp(jQueryEvent e)
		{
			dateYearTextBoxKeyUpChange(false);
		}
		void dateYearTextBoxChange(jQueryEvent e)
		{
			dateYearTextBoxKeyUpChange(true);
		}
		void dateYearTextBoxKeyUpChange(bool change)
		{
			try
			{
				RegularExpression regex = new RegularExpression("[^0-9]", "g");
				if (!regex.Test(view.DateYearTextBox.Value))
				{
					int newYear = int.Parse(view.DateYearTextBox.Value, 10);
					if (newYear > 1900 && newYear < 2100)
					{
						if (newYear != Date.Year)
						{
							dateRefreshDays(newYear, Date.Month);
							int newDay = OptionDateDay ? getValueInt(view.DateDayDropDown) : 0;
							DateStub newDate = new DateStub(newYear, Date.Month, newDay);
							addHistory("Date", newDate.ToString());
							
							//Moved to navigateDate
							//initialiseEventDropDown();
						}
					}
					else
					{
						if (change)
							view.DateYearTextBox.Value = Date.Year.ToString();
					}
				}
				else
				{
					if (change)
						view.DateYearTextBox.Value = Date.Year.ToString();
				}
			}
			catch
			{
				if (change)
					view.DateYearTextBox.Value = Date.Year.ToString();
			}
		}
		void dateYearPlusClick(jQueryEvent e)
		{
			datePlusMinus(1, "y");
		}
		void dateYearMinusClick(jQueryEvent e)
		{
			datePlusMinus(-1, "y");
		}
		void dateMonthPlusClick(jQueryEvent e)
		{
			datePlusMinus(1, "m");
		}
		void dateMonthMinusClick(jQueryEvent e)
		{
			datePlusMinus(-1, "m");
		}
		void dateDayPlusClick(jQueryEvent e)
		{
			datePlusMinus(OptionDateDayIncrement, "d");
		}
		void dateDayMinusClick(jQueryEvent e)
		{
			datePlusMinus(-OptionDateDayIncrement, "d");
		}
		void dateChangePrivate(DateStub newDate)
		{
			DateStub oldDate = new DateStub(Date.Year, Date.Month, Date.Day);
			if (newDate.Year != oldDate.Year || newDate.Month != oldDate.Month)
			{
				dateRefreshDays(newDate.Year, newDate.Month);
				//newDate.Day = getValueInt(view.DateDayDropDown);
			}

			if (newDate.Day != oldDate.Day)
				setValue(view.DateDayDropDown, newDate.Day.ToString());

			if (newDate.Month != oldDate.Month)
				setValue(view.DateMonthDropDown, newDate.Month.ToString());

			if (newDate.Year != oldDate.Year)
				view.DateYearTextBox.Value = newDate.Year.ToString();

			if (!OptionDateDay && newDate.Day > 0)
				newDate.Day = 0;

			addHistory("Date", newDate.ToString());
			
			//Moved to navigateDate
			//initialiseEventDropDown();
		}
		DateStub datePlusMinus(int modifier, string unit)
		{
			DateStub newDate = new DateStub(Date.Year, Date.Month, Date.Day).Modify(modifier, unit);

			dateChangePrivate(newDate);
			
			return newDate;
		}

		public DateStub DateModify(int modifier, string unit)
		{
			return datePlusMinus(modifier, unit);
		}
		public void DateChange(DateStub newDate)
		{
			dateChangePrivate(newDate);
		}

		string navigateDatePreviousValue = "";
		void navigateDate(string value)
		{
			//Debug("navigateDate");

			if (!OptionDate || value.Length == 0 || value == navigateDatePreviousValue) return;

			if (value == "0")
			{
				//How do we zero the date? Set to todays date?
				//setIndex(view.DateDropDown, 0);
				view.DateSelectedLabel.InnerHTML = "None selected";
				view.SelectedDateHidden.Value = "";

				if (!firstEverNavigate)
				{
					if (SelectedDateChanged != null)
						SelectedDateChanged(this, new DateArgs(null));

					if (EventSelectionSepcificationChanged != null)
						EventSelectionSepcificationChanged(this,
							new EventSelectionArgs(null));
				}
			}
			else
			{
				DateStub date = DateStub.FromString(value);
				
				if (dateDropDownsAreInitialised)
				{
					view.DateYearTextBox.Value = date.Year.ToString();
					setValue(view.DateMonthDropDown, date.Month.ToString());
					dateRefreshDays(date.Year, date.Month);
					setValue(view.DateDayDropDown, date.Day.ToString());
				}

				if (!dateDropDownsAreInitialised)
					view.DateSelectedLabel.InnerHTML = date.ToFriendlyString();

				view.SelectedDateHidden.Value = date.ToString();

				if (!firstEverNavigate)
				{
					if (SelectedDateChanged != null)
						SelectedDateChanged(this, new DateArgs(date));

					if (EventSelectionSepcificationChanged != null)
						EventSelectionSepcificationChanged(this,
							new EventSelectionArgs(
								new EventSelectionSpecification(
									SearchType == SearchTypes.Brand ? Brand : null,
									SearchType == SearchTypes.Venue || SearchType == SearchTypes.Music ? Place : null,
									SearchType == SearchTypes.Venue ? Venue : null,
									SearchType == SearchTypes.Music ? Music : null,
									date,
									SearchType == SearchTypes.Me)));
				}
			}

			navigateDatePreviousValue = value;

			//Moved here from dateDayDropDownChange / dateMonthDropDownChange / dateYearTextBoxKeyUpChange / dateChangePrivate
			initialiseEventDropDown();
		}

		void updateUIDate(bool recursive)
		{
			//Debug("updateUIDate");

			view.DateHolder.Style.Display = 
				OptionDate &&
				(
					(SearchType == SearchTypes.Venue && Venue != null) || 
					(SearchType == SearchTypes.Venue && !OptionVenue && Place != null) || 
					(SearchType == SearchTypes.Music && Place != null && Music != null) || 
					(SearchType == SearchTypes.Brand && Brand != null)
				)
				? "" : "none";

			view.DateDayHolder.Style.Display = OptionDateDay ? "" : "none";

			view.DateSelectedHolder.Style.Display = !dateDropDownsAreInitialised ? "" : "none";
			view.DateChoiceHolder.Style.Display = dateDropDownsAreInitialised ? "" : "none";

			if (recursive)
			    updateUIEvent();
		}
		#endregion

		#region Event
		bool OptionEvent { get { return bool.Parse(view.OptionEventHidden.Value); } }
		public ObjectEvent SelectedEventChanged = null;
		//JQueryObject EventListBoxJ;
		bool eventDropDownIsInitialised = false;
		int eventDropDownVenueK;
		int eventDropDownBrandK;
		string eventDropDownKey;
		DateStub eventDropDownDate;
		int eventDropDownPreviouslySelectedIndex;
		
		ObjectStub Event
		{
			get
			{
				if (!OptionEvent || view.SelectedEventHidden.Value == null || view.SelectedEventHidden.Value.Length == 0)
					return null;

				return ObjectStub.FromString(view.SelectedEventHidden.Value);
			}
		}

		void initialiseEvent()
		{
			if (!OptionEvent)
				return;

			//EventListBoxJ = JQueryAPI.JQuery(view.EventListBox);
			view.EventListBoxJ.Change(eventDropDownChange);
			view.EventSelectedChangeLinkJ.Click(eventSelectedChangeLinkClick);
		}

		void eventSelectedChangeLinkClick(jQueryEvent e)
		{
			e.PreventDefault();

			initialiseEventDropDown();

		}
		void initialiseEventDropDown()
		{
			if (!OptionEvent)
				return;

			if (!eventDropDownIsInitialised ||
				((SearchType == SearchTypes.Venue || SearchType == SearchTypes.Music) && Venue != null && eventDropDownVenueK != Venue.K) ||
				(SearchType == SearchTypes.Brand && Brand != null && eventDropDownBrandK != Brand.K) ||
				(SearchType == SearchTypes.Key && Key != null && eventDropDownKey != Key) ||
				eventDropDownDate.Year != Date.Year ||
				eventDropDownDate.Month != Date.Month)
			{
				if ((SearchType == SearchTypes.Venue || SearchType == SearchTypes.Music) && Venue != null && Venue.K == 1)
					return; //Specific case for "all venues" option.

				eventDropDownDate = Date;
				eventDropDownVenueK = Venue != null && (SearchType == SearchTypes.Venue || SearchType == SearchTypes.Music) ? Venue.K : 0;
				eventDropDownBrandK = Brand != null && SearchType == SearchTypes.Brand ? Brand.K : 0;
				eventDropDownKey = Key != null && SearchType == SearchTypes.Key ? Key : "0";

				if (eventDropDownVenueK == 0 && eventDropDownBrandK == 0 && eventDropDownKey == "0")
					return;

				requestId++;

				int currentRequestId = requestId;
				int currentLoadId = loadId;

				jQuerySelectBoxes.FromObject(view.EventListBoxJ).AjaxAddOption1(
					"/support/getcached.aspx?type=event&key=" + eventDropDownKey + "&venuek=" + eventDropDownVenueK + "&brandk=" + eventDropDownBrandK + "&date=" + eventDropDownDate.ToString(), 
					null, 
					false, 
					new ActionObjectObject(eventDropDownInitialised), 
					new object[] { currentRequestId }
				);

				Window.SetTimeout(
					delegate
					{
						if (loadId == currentLoadId)
						{
							view.EventListBoxJ.Empty();
							addOption(view.EventListBox, "0", "Loading...");
						}
					},
					100);
			}
			else
				eventDropDownConfigure();
		}
		int requestId = 0;
		int loadId = 0;
		void eventDropDownInitialised(object args, object data)
		{
			int requestIdFromArgs = int.Parse(args.ToString(), 10);
			if (requestId == requestIdFromArgs)
			{

				loadId++;

				view.EventListBoxJ.Empty();
				jQuerySelectBoxes.FromObject(view.EventListBox).AddOption(data, false);

				eventDropDownConfigure();

			}
		}
		void eventDropDownConfigure()
		{
			eventDropDownIsInitialised = true;

			view.EventListBox.Size = view.EventListBox.Options.Length < 3 ? 3 : view.EventListBox.Options.Length;

			if (Event != null)
			{
				bool setValue = setK(view.EventListBox, Event.K);

				if (!setValue)
				{
					addHistory("Event", "0");
					unSelect(view.EventListBox);
				}
			}

			eventDropDownPreviouslySelectedIndex = getIndex(view.EventListBox);

			updateUIEvent();
		}
		void eventDropDownChange(jQueryEvent e)
		{
			int k = getK(view.EventListBox);
			if (k > 0)
			{
				ObjectStub _event = new ObjectStub(getK(view.EventListBox), getText(view.EventListBox));
				addHistory("Event", _event.ToString());
			}
			else
			{
				//user has clicked on one of the description or padding items... must reset to what was previously selected.
				setIndex(view.EventListBox, eventDropDownPreviouslySelectedIndex);
			}
			eventDropDownPreviouslySelectedIndex = getIndex(view.EventListBox);
		}
		string navigateEventPreviousValue = "";
		void navigateEvent(string value)
		{
			if (!OptionEvent || value.Length == 0 || value == navigateEventPreviousValue) return;

			if (value == "0")
			{
				setIndex(view.EventListBox, 0);
				view.EventSelectedLabel.InnerHTML = "None selected";
				view.SelectedEventHidden.Value = "";

				if (SelectedEventChanged != null)
					SelectedEventChanged(this, new ObjectArgs(null));
			}
			else
			{
				ObjectStub _event = ObjectStub.FromString(value);

				if (eventDropDownIsInitialised && getK(view.EventListBox) != _event.K)
					setK(view.EventListBox, _event.K);

				if (!eventDropDownIsInitialised)
					view.EventSelectedLabel.InnerHTML = _event.Name.Substr(11);

				view.SelectedEventHidden.Value = _event.ToString();

				if (SelectedEventChanged != null)
					SelectedEventChanged(this, new ObjectArgs(_event));
			}

			navigateEventPreviousValue = value;
		}

		void updateUIEvent()
		{

			view.EventHolder.Style.Display = 
				OptionEvent &&
				(
				((SearchType == SearchTypes.Venue || SearchType == SearchTypes.Music) && Venue != null && Venue.K > 1) || 
				(SearchType == SearchTypes.Brand && Brand != null && Brand.K > 1) ||
				(SearchType == SearchTypes.Key && Key != null && Key.Length > 0)
				)
				? "" : "none";

			view.EventSelectedHolder.Style.Display = !eventDropDownIsInitialised ? "" : "none";
			view.EventChoiceHolder.Style.Display = eventDropDownIsInitialised ? "" : "none";

		}

		#endregion

		#region Google
		bool OptionGoogle { get { return bool.Parse(view.OptionGoogleHidden.Value); } }
		#endregion

		#region Utils
		RegularExpression regexQuote = new RegularExpression("'");
		void addOption(SelectElement sel, string key, string value)
		{
			OptionElement op = (OptionElement)Document.CreateElement("OPTION");
			op.Text = value;
			op.Value = key;
			try
			{
				sel.Add(op, null);
			}
			catch
			{
				sel.Add(op);
			}
		}
		void addHistory(string key, string value)
		{
			if (key.Length > 0)
				firstEverNavigate = false;

			Dictionary<object, object> d = new Dictionary<object, object>();
			if (key.Length > 0)
				d[view.clientId + "_" + key] = value.EncodeUri().ReplaceRegex(regexQuote, "&#39;");

			jQueryHistory.PushState(d);
		}
		void addHistorys(string[] keysAndValues)
		{
			firstEverNavigate = false;
			Dictionary<object, object> d = new Dictionary<object, object>();
			for (int i = 0; i < keysAndValues.Length; i = i + 2)
			{
				d[view.clientId + "_" + keysAndValues[i]] = keysAndValues[i + 1].EncodeUri().ReplaceRegex(regexQuote, "&#39;");
			}
			jQueryHistory.PushState(d);
		}
		string getValue(SelectElement sel)
		{
			return sel.SelectedIndex == -1 ? "" : ((OptionElement)sel.Options[sel.SelectedIndex]).Value;
		}
		int getValueInt(SelectElement sel)
		{
			return sel.SelectedIndex == -1 ? 0 : int.Parse(((OptionElement)sel.Options[sel.SelectedIndex]).Value, 10);
		}
		int getIndex(SelectElement sel)
		{
			return sel.SelectedIndex;
		}
		int getK(SelectElement sel)
		{
			try
			{
				string value = ((OptionElement)sel.Options[sel.SelectedIndex]).Value;
				value = value.Substr(5, value.Length - 5);
				return int.Parse(value, 10);
			}
			catch
			{
				return 0;
			}
		}
		string getText(SelectElement sel)
		{
			return ((OptionElement)sel.Options[sel.SelectedIndex]).Text;
		}
		void setIndex(SelectElement sel, int index)
		{
			try
			{
				OptionElement op = index > sel.Options.Length - 1 ? (OptionElement)sel.Options[sel.Options.Length - 1] : (OptionElement)sel.Options[index];
				op.Selected = true;
			}
			catch { }
		}
		void unSelect(SelectElement sel)
		{
			for (int i = 0; i < sel.Options.Length; i++)
			{
				OptionElement op = (OptionElement)sel.Options[i];
				op.Selected = false;
			}
		}
		bool setValue(SelectElement sel, string value)
		{
			for (int i = 0; i < sel.Options.Length; i++)
			{
				OptionElement op = (OptionElement)sel.Options[i];
				if (op.Value.ToLowerCase() == value.ToLowerCase())
				{
					op.Selected = true;
					return true;
				}
			}
			return false;
		}
		bool setK(SelectElement sel, int value)
		{
			for (int i = 0; i < sel.Options.Length; i++)
			{
				OptionElement op = (OptionElement)sel.Options[i];
				if (op.Value.Substr(5, op.Value.Length - 5).ToLowerCase() == value.ToString().ToLowerCase())
				{
					op.Selected = true;
					return true;
				}
			}
			return false;
		}
		#endregion

		#region Debug
		int debugCount = 0;
		public void Debug(string text)
		{
			view.Debug.Style.Display = "";
			debugCount++;
			view.Debug.Value = debugCount.ToString() + " " + text + "\n" + view.Debug.Value;
		}
		#endregion


	}

	#region DateStub
	public class DateStub
	{
		public int Year;
		public int Month;
		public int Day;
		public DateStub(int year, int month, int day)
		{
			Year = year;
			Month = month;
			Day = day;
		}
		public static DateStub FromString(string value)
		{
			int year = int.Parse(value.Substr(0, 4), 10);
			int month = int.Parse(value.Substr(4, 2), 10);
			int day = int.Parse(value.Substr(6, 2), 10);

			return new DateStub(year, month, day);
		}
		public override string ToString()
		{
			return Year.ToString() + (Month < 10 ? "0" : "") + Month.ToString() + (Day < 10 ? "0" : "") + Day.ToString();
		}
		public string ToFriendlyString()
		{
			return Year.ToString() + "-" + (Month < 10 ? "0" : "") + Month.ToString() + (Day > 0 ? ("-" + (Day < 10 ? "0" : "") + Day.ToString()) : "");
		}
		public Date ToDateTime()
		{
			return new Date(Year, Month - 1, Day);
		}

		public DateStub Modify(int modifier, string unit)
		{
			DateStub newDate = new DateStub(this.Year, this.Month, this.Day);
			if (unit == "y")
			{
				newDate.Year = this.Year + modifier;
			}
			else if (unit == "m")
			{
				#region month
				if (modifier > 0)
				{
					if (this.Month + modifier > 12)
					{
						newDate.Month = this.Month + modifier - 12;
						newDate.Year++;
					}
					else
						newDate.Month = this.Month + modifier;
				}
				else
				{
					if (this.Month + modifier < 1)
					{
						newDate.Month = 12 - (this.Month + modifier);
						newDate.Year--;
					}
					else
						newDate.Month = this.Month + modifier;
				}
				#endregion
			}
			else if (unit == "d")
			{
				#region day
				int daysInOldMonth = DaysInMonth(this.Year, this.Month);
				if (modifier > 0)
				{
					if (this.Day + modifier > daysInOldMonth)
					{
						if (this.Month == 12)
						{
							newDate.Year++;
							newDate.Month = 1;
							newDate.Day = this.Day + modifier - daysInOldMonth;
						}
						else
						{
							newDate.Month++;
							newDate.Day = this.Day + modifier - daysInOldMonth;
						}
					}
					else
						newDate.Day = this.Day + modifier;
				}
				else
				{
					if (this.Day + modifier < 1)
					{
						if (this.Month == 1)
						{
							newDate.Year--;
							newDate.Month = 12;
							newDate.Day = DaysInMonth(newDate.Year, newDate.Month) + this.Day + modifier;
						}
						else
						{
							newDate.Month--;
							newDate.Day = DaysInMonth(newDate.Year, newDate.Month) + this.Day + modifier;
						}
					}
					else
						newDate.Day = this.Day + modifier;
				}
				#endregion
			}
			return newDate;
		}
		public static int DaysInMonth(int year, int month)
		{
			int[] m = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

			if (month != 2)
				return m[month - 1];

			if (year % 4 != 0)
				return m[1];

			if (year % 100 == 0 && year % 400 != 0)
				return m[1];

			return m[1] + 1;
		}
		/// <summary>
		/// Week day
		/// </summary>
		/// <returns>0 = Sunday, 1 = Monday, etc.</returns>
		public int DayOfWeek()
		{
			return this.ToDateTime().GetDay();
		}
		public DateStub PreviousMonday()
		{
			int d = this.DayOfWeek();
			int modifier = d == 0 ? -6 : d == 1 ? 0 : d == 2 ? -1 : d == 3 ? -2 : d == 4 ? -3 : d == 5 ? -4 : d == 6 ? -5 : 0;
			return this.Modify(modifier, "d");
		}
		public DateStub NextSunday()
		{
			int d = this.DayOfWeek();
			int modifier = d == 0 ? 0 : d == 1 ? 6 : d == 2 ? 5 : d == 3 ? 4 : d == 4 ? 3 : d == 5 ? 2 : d == 6 ? 1 : 0;
			return this.Modify(modifier, "d");
		}
		public string MonthNameFull
		{
			get
			{
				int m = Month;
				return m == 1 ? "January" :	m == 2 ? "February" : m == 3 ? "March" : m == 4 ? "April" : m == 5 ? "May" : m == 6 ? "June" : m == 7 ? "July" : m == 8 ? "August" : m == 9 ? "September" : m == 10 ? "October" : m == 11 ? "November" : m == 12 ? "December" : "";
			}
		}
		#region Debug
		int debugCount = 0;
		void Debug(string text)
		{
			InputElement debug = (InputElement)Document.GetElementById("Content_Debug");
			debug.Style.Display = "";
			debugCount++;
			debug.Value = debugCount.ToString() + " " + text + "\n" + debug.Value;
		}
		#endregion
	}

	#endregion

	#region ObjectStub
	public class ObjectStub
	{
		public string Name;
		public int K;
		public ObjectStub(int k, string name)
		{
			K = k;
			Name = name;
		}
		public static ObjectStub FromString(string value)
		{
			if (value == null || value.Length == 0 || value == "0")
				return null;

			int k = int.Parse(value.Substr(0, value.IndexOf('-')), 10);
			string name = value.Substr(value.IndexOf('-') + 1, value.Length - value.IndexOf('-') - 1);
			return new ObjectStub(k, name);
		}
		public override string ToString()
		{
			return K.ToString() + "-" + Name;
		}

	}
	#endregion

	#region EventSelectionSpecification
	public class EventSelectionSpecification
	{
		public ObjectStub Brand;
		public ObjectStub Place;
		public ObjectStub Venue;
		public ObjectStub Music;
		public DateStub Date;
		public bool Me;
		public EventSelectionSpecification(ObjectStub brand, ObjectStub place, ObjectStub venue, ObjectStub music, DateStub date, bool me)
		{
			Brand = brand;
			Place = place;
			Venue = venue;
			Music = music;
			Date = date;
			Me = me;
		}
		public override string ToString()
		{
			return "Brand: " + (Brand == null ? "null" : Brand.ToString()) + "<br />" +
				"Place: " + (Place == null ? "null" : Place.ToString()) + "<br />" +
				"Venue: " + (Venue == null ? "null" : Venue.ToString()) + "<br />" +
				"Music: " + (Music == null ? "null" : Music.ToString()) + "<br />" +
				"Date: " + (Date == null ? "null" : Date.ToFriendlyString()) + "<br />" +
				"Me: " + Me.ToString().ToLowerCase();
		}
	}
	#endregion

	public delegate void ActionObjectStub(ObjectStub arg);
	
	#region StringArgs
	public class StringArgs : EventArgs
	{
		public string Val;
		public StringArgs(string val)
		{
			this.Val = val;
		}
	}
	public delegate void StringEvent(object o, StringArgs e);
	#endregion

	#region ObjectArgs
	public class ObjectArgs : EventArgs
	{
		public ObjectStub Object;
		public ObjectArgs(ObjectStub ob)
		{
			this.Object = ob;
		}
	}
	public delegate void ObjectEvent(object o, ObjectArgs e);
	#endregion

	#region DateArgs
	public class DateArgs : EventArgs
	{
		public DateStub Date;
		public DateArgs(DateStub d)
		{
			this.Date = d;
		}
	}
	public delegate void DateEvent(object o, DateArgs e);
	#endregion

	#region EventSelectionArgs
	public class EventSelectionArgs : EventArgs
	{
		public EventSelectionSpecification Specification;
		public EventSelectionArgs(EventSelectionSpecification specification)
		{
			this.Specification = specification;
		}
	}
	public delegate void EventSelectionEvent(object o, EventSelectionArgs e);
	#endregion

}
