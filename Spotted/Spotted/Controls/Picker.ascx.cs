using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bobs;
using System.Text;
using SpottedScript.Controls.Picker.Shared;

namespace Spotted.Controls
{
	[ValidationProperty("ValidationPropertyValue")]
	[ClientScript]
	public partial class Picker : EnhancedUserControl
	{
		public Picker()
		{
		}

		protected void Page_Init(object sender, EventArgs e)
		{
			JQuery.Include(Page, "selectboxes", "selectboxes");

			if (!Page.IsPostBack)
			{
				#region defaults
				try
				{
					if (!countrySet)
					{
						Country defaultCountry = Usr.Current != null ? Usr.Current.Home.Country : Visit.Current.CountryK > 0 ? new Country(Visit.Current.CountryK) : null;
						if (defaultCountry != null)
							Country = defaultCountry;
					}
				}
				catch { }

				try
				{
					if (!placeSet)
					{
						Place defaultPlace = Usr.Current != null ? Usr.Current.Home : null;
						if (defaultPlace != null)
							Place = defaultPlace;
					}
				}
				catch { }
				#endregion

				#region pre-set options
				if (!optionKeySet) OptionKey = false;
				if (!optionMeSet) OptionMe = false;
				if (!optionSpotterSet) OptionSpotter = false;
				if (!optionBrandSet) OptionBrand = true;
				if (!optionCountrySet) OptionCountry = true;
				if (!optionPlaceSet) OptionPlace = true;
				if (!optionVenueSet) OptionVenue = true;
				if (!optionMusicSet) OptionMusic = false;
				if (!optionDateSet) OptionDate = true;
				if (!optionDateDaySet) OptionDateDay = false;
				if (!optionEventSet) OptionEvent = true;
				if (!optionGoogleSet) OptionGoogle = false;
				#endregion
			}
		}

		public string ValidationPropertyValue
		{
			get
			{
				string s = 
					ValidationType == "country" ? (Country != null ? Country.K.ToString() : "") :
					ValidationType == "place" ? (Place != null ? Place.K.ToString() : "") :
					ValidationType == "venue" ? (Venue != null ? Venue.K.ToString() : "") :
					Event != null ? Event.K.ToString() : "";
				return s;
			}
		}
		public string ValidationType
		{
			get
			{
				return this.ViewState["ValidationType"] == null ? "" : (string)this.ViewState["ValidationType"];
			}
			set
			{
				this.ViewState["ValidationType"] = value;
			}
		}
		
		protected void Page_Load(object sender, EventArgs e)
		{

		}

		protected void Page_PreRender(object sender, EventArgs e)
		{
			DateSelectedLabel.Text = OptionDateDay ? "Today" : "This month";
		}

		#region SearchType
		public SearchTypes SearchType
		{
			get
			{
				return SelectedSearchTypeHidden.Value.Length == 0 ? SearchTypes.Unknown : (SearchTypes)Enum.Parse(typeof(SearchTypes), SelectedSearchTypeHidden.Value, true);
			}
			set
			{
				SelectedSearchTypeHidden.Value = value.ToString().ToLower();
			}
		}
		#endregion
		#region Key
		public string Key
		{
			get
			{
				return SelectedKeyHidden.Value;
			}
			set
			{
				SelectedKeyHidden.Value = value;
			}
		}
		#endregion
		#region Spotter
		public string Spotter
		{
			get
			{
				return SelectedSpotterHidden.Value;
			}
			set
			{
				SelectedSpotterHidden.Value = value;
			}
		}
		#endregion
		#region Brand
		public Brand Brand
		{
			get
			{
				if (SelectedBrandHidden.Value.Length == 0 || SelectedBrandHidden.Value == "0")
					return null;

				if (brand == null)
					brand = new Brand(int.Parse(SelectedBrandHidden.Value.Substring(0, SelectedBrandHidden.Value.IndexOf("-"))));

				return brand;
			}
			set
			{
				SelectedBrandHidden.Value = value == null ? "" : (value.K.ToString() + "-" + value.Name.ToString());
				brand = null;
			}
		}
		Brand brand;
		#endregion
		#region Country
		public Country Country
		{
			get
			{
				if (SelectedCountryHidden.Value.Length == 0 || SelectedCountryHidden.Value == "0")
					return null;

				if (country == null)
					country = new Country(int.Parse(SelectedCountryHidden.Value.Substring(0, SelectedCountryHidden.Value.IndexOf("-"))));

				return country;
			}
			set
			{
				SelectedCountryHidden.Value = value == null ? "" : (value.K.ToString() + "-" + value.Name.ToString());
				country = null;
				countrySet = true;
			}
		}
		Country country;
		bool countrySet = false;
		#endregion
		#region Place
		public Place Place
		{
			get
			{
				if (SelectedPlaceHidden.Value.Length == 0 || SelectedPlaceHidden.Value == "0")
					return null;

				if (place == null)
					place = new Place(int.Parse(SelectedPlaceHidden.Value.Substring(0, SelectedPlaceHidden.Value.IndexOf("-"))));

				return place;
			}
			set
			{
				SelectedPlaceHidden.Value = value == null ? "" : (value.K.ToString() + "-" + value.Name.ToString());
				place = null;
				placeSet = true;
				if (value != null)
					Country = value.Country;
			}
		}
		Place place;
		bool placeSet = false;
		#endregion
		#region Venue
		public Venue Venue
		{
			get
			{
				if (SelectedVenueHidden.Value.Length == 0 || SelectedVenueHidden.Value == "0" || SelectedVenueHidden.Value.StartsWith("1-"))
					return null;

				if (venue == null)
					venue = new Venue(int.Parse(SelectedVenueHidden.Value.Substring(0, SelectedVenueHidden.Value.IndexOf("-"))));

				return venue;
			}
			set
			{
				SelectedVenueHidden.Value = value == null ? "" : (value.K.ToString() + "-" + value.Name.ToString());
				venue = null;
				if (value != null)
					Place = value.Place;
			}
		}
		Venue venue;
		#endregion
		#region Music
		public MusicType Music
		{
			get
			{
				if (SelectedMusicHidden.Value.Length == 0 || SelectedMusicHidden.Value == "0")
					return null;

				if (music == null)
					music = new MusicType(int.Parse(SelectedMusicHidden.Value.Substring(0, SelectedMusicHidden.Value.IndexOf("-"))));

				return music;
			}
			set
			{
				SelectedMusicHidden.Value = value == null ? "" : (value.K.ToString() + "-" + value.Name.ToString());
				music = null;
				musicSet = true;
			}
		}
		MusicType music;
		bool musicSet = false;
		#endregion
		#region Date
		public DateTime Date
		{
			get
			{
				if (SelectedDateHidden.Value.Length == 0 || SelectedDateHidden.Value == "0")
				    return DateTime.MinValue;

				return new DateTime(
					int.Parse(SelectedDateHidden.Value.Substring(0, 4)),
					int.Parse(SelectedDateHidden.Value.Substring(4, 2)),
					int.Parse(SelectedDateHidden.Value.Substring(6, 2)) > 0 ? int.Parse(SelectedDateHidden.Value.Substring(6, 2)) : 1 );

			}
			set
			{
				SelectedDateHidden.Value = value.ToString("yyyyMMdd");
			}
		}
		#endregion
		#region Event
		public Event Event
		{
			get
			{
				if (SelectedEventHidden.Value.Length == 0 || SelectedEventHidden.Value == "0")
					return null;

				if (_event == null)
					_event = new Event(int.Parse(SelectedEventHidden.Value.Substring(0, SelectedEventHidden.Value.IndexOf("-"))));

				return _event;
			}
			set
			{
				SelectedEventHidden.Value = value == null ? "" : (value.K.ToString() + "-" + value.Name.ToString());
				_event = null;
				if (value != null)
					Venue = value.Venue;
			}
		}
		Event _event;
		#endregion

		#region Options
		public bool OptionKey { get { return bool.Parse(OptionKeyHidden.Value); } set { OptionKeyHidden.Value = value.ToString().ToLower(); optionKeySet = true; } } bool optionKeySet = false;
		public bool OptionMe { get { return bool.Parse(OptionMeHidden.Value); } set { OptionMeHidden.Value = value.ToString().ToLower(); optionMeSet = true; } } bool optionMeSet = false;
		public bool OptionSpotter { get { return bool.Parse(OptionSpotterHidden.Value); } set { OptionSpotterHidden.Value = value.ToString().ToLower(); optionSpotterSet = true; } } bool optionSpotterSet = false;
		public bool OptionBrand { get { return bool.Parse(OptionBrandHidden.Value); } set { OptionBrandHidden.Value = value.ToString().ToLower(); optionBrandSet = true; } } bool optionBrandSet = false;
		public bool OptionCountry { get { return bool.Parse(OptionCountryHidden.Value); } set { OptionCountryHidden.Value = value.ToString().ToLower(); optionCountrySet = true; } } bool optionCountrySet = false;
		public bool OptionPlace { get { return bool.Parse(OptionPlaceHidden.Value); } set { OptionPlaceHidden.Value = value.ToString().ToLower(); optionPlaceSet = true; } } bool optionPlaceSet = false;
		public bool OptionVenue { get { return bool.Parse(OptionVenueHidden.Value); } set { OptionVenueHidden.Value = value.ToString().ToLower(); optionVenueSet = true; } } bool optionVenueSet = false;
		public bool OptionMusic { get { return bool.Parse(OptionMusicHidden.Value); } set { OptionMusicHidden.Value = value.ToString().ToLower(); optionMusicSet = true; } } bool optionMusicSet = false;
		public bool OptionDate { get { return bool.Parse(OptionDateHidden.Value); } set { OptionDateHidden.Value = value.ToString().ToLower(); optionDateSet = true; } } bool optionDateSet = false;
		public bool OptionDateDay { get { return bool.Parse(OptionDateDayHidden.Value); } set { OptionDateDayHidden.Value = value.ToString().ToLower(); optionDateDaySet = true; } } bool optionDateDaySet = false;
		public int OptionDateDayIncrement { get { if (OptionDateDayIncrementHidden.Value.Length == 0 || OptionDateDayIncrementHidden.Value == "0") return 1; return int.Parse(OptionDateDayIncrementHidden.Value); } set { OptionDateDayIncrementHidden.Value = value.ToString(); } }
		public bool OptionEvent { get { return bool.Parse(OptionEventHidden.Value); } set { OptionEventHidden.Value = value.ToString().ToLower(); optionEventSet = true; } } bool optionEventSet = false;
		public bool OptionGoogle { get { return bool.Parse(OptionGoogleHidden.Value); } set { OptionGoogleHidden.Value = value.ToString().ToLower(); optionGoogleSet = true; } } bool optionGoogleSet = false;
		#endregion

		protected override void LoadViewState(object savedState)
		{
			base.LoadViewState(savedState);

			#region selections
			if (this.ViewState["SelectedSearchType"] != null) SearchType = (SearchTypes)this.ViewState["SelectedSearchType"];
			if (this.ViewState["SelectedKey"] != null) SelectedKeyHidden.Value = (string)this.ViewState["SelectedKey"];
			if (this.ViewState["SelectedSpotter"] != null) SelectedSpotterHidden.Value = (string)this.ViewState["SelectedSpotter"];
			if (this.ViewState["SelectedBrand"] != null) SelectedBrandHidden.Value = (string)this.ViewState["SelectedBrand"];
			if (this.ViewState["SelectedCountry"] != null) SelectedCountryHidden.Value = (string)this.ViewState["SelectedCountry"];
			if (this.ViewState["SelectedPlace"] != null) SelectedPlaceHidden.Value = (string)this.ViewState["SelectedPlace"];
			if (this.ViewState["SelectedVenue"] != null) SelectedVenueHidden.Value = (string)this.ViewState["SelectedVenue"];
			if (this.ViewState["SelectedMusic"] != null) SelectedMusicHidden.Value = (string)this.ViewState["SelectedMusic"];
			if (this.ViewState["SelectedDate"] != null) SelectedDateHidden.Value = (string)this.ViewState["SelectedDate"];
			if (this.ViewState["SelectedEvent"] != null) SelectedEventHidden.Value = (string)this.ViewState["SelectedEvent"];
			#endregion
			#region options
			if (this.ViewState["OptionKey"] != null) OptionKey = (bool)this.ViewState["OptionKey"];
			if (this.ViewState["OptionMe"] != null) OptionMe = (bool)this.ViewState["OptionMe"];
			if (this.ViewState["OptionSpotter"] != null) OptionSpotter = (bool)this.ViewState["OptionSpotter"];
			if (this.ViewState["OptionBrand"] != null) OptionBrand = (bool)this.ViewState["OptionBrand"];
			if (this.ViewState["OptionCountry"] != null) OptionCountry = (bool)this.ViewState["OptionCountry"];
			if (this.ViewState["OptionPlace"] != null) OptionPlace = (bool)this.ViewState["OptionPlace"];
			if (this.ViewState["OptionVenue"] != null) OptionVenue = (bool)this.ViewState["OptionVenue"];
			if (this.ViewState["OptionMusic"] != null) OptionMusic = (bool)this.ViewState["OptionMusic"];
			if (this.ViewState["OptionDate"] != null) OptionDate = (bool)this.ViewState["OptionDate"];
			if (this.ViewState["OptionDateDay"] != null) OptionDateDay = (bool)this.ViewState["OptionDateDay"];
			if (this.ViewState["OptionDateDayIncrement"] != null) OptionDateDayIncrement = (int)this.ViewState["OptionDateDayIncrement"];
			if (this.ViewState["OptionEvent"] != null) OptionEvent = (bool)this.ViewState["OptionEvent"];
			if (this.ViewState["OptionGoogle"] != null) OptionGoogle = (bool)this.ViewState["OptionGoogle"];
			#endregion
		}

		protected override object SaveViewState()
		{
			#region selections
			this.ViewState["SelectedSearchType"] = SearchType;
			this.ViewState["SelectedKey"] = Key;
			this.ViewState["SelectedSpotter"] = Spotter;
			this.ViewState["SelectedBrand"] = Brand == null ? "" : (Brand.K + "-" + Brand.Name);
			this.ViewState["SelectedCountry"] = Country == null ? "" : (Country.K + "-" + Country.Name);
			this.ViewState["SelectedPlace"] = Place == null ? "" : (Place.K + "-" + Place.Name);
			this.ViewState["SelectedVenue"] = Venue == null ? "" : (Venue.K + "-" + Venue.Name);
			this.ViewState["SelectedMusic"] = Music == null ? "" : (Music.K + "-" + Music.Name);
			this.ViewState["SelectedDate"] = Date == DateTime.MinValue ? "" : (Date.ToString("yyyyMMdd"));
			this.ViewState["SelectedEvent"] = Event == null ? "" : (Event.K + "-" + Event.Name);
			#endregion
			#region options
			this.ViewState["OptionKey"] = OptionKey;
			this.ViewState["OptionMe"] = OptionMe;
			this.ViewState["OptionSpotter"] = OptionSpotter;
			this.ViewState["OptionBrand"] = OptionBrand;
			this.ViewState["OptionCountry"] = OptionCountry;
			this.ViewState["OptionPlace"] = OptionPlace;
			this.ViewState["OptionVenue"] = OptionVenue;
			this.ViewState["OptionMusic"] = OptionMusic;
			this.ViewState["OptionDate"] = OptionDate;
			this.ViewState["OptionDateDay"] = OptionDateDay;
			this.ViewState["OptionDateDayIncrement"] = OptionDateDayIncrement;
			this.ViewState["OptionEvent"] = OptionEvent;
			this.ViewState["OptionGoogle"] = OptionGoogle;
			#endregion

			return base.SaveViewState();
		}

	}
	
}
