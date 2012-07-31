<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Picker.ascx.cs" Inherits="Spotted.Controls.Picker" %>

<asp:TextBox runat="server" ID="Debug" TextMode="MultiLine" Columns="96" Rows="10" style="display:none;" />


<asp:HiddenField runat="server" ID="SelectedSearchTypeHidden" />
<asp:HiddenField runat="server" ID="SelectedKeyHidden" />
<asp:HiddenField runat="server" ID="SelectedSpotterHidden" />
<asp:HiddenField runat="server" ID="SelectedBrandHidden" />
<asp:HiddenField runat="server" ID="SelectedCountryHidden" />
<asp:HiddenField runat="server" ID="SelectedPlaceHidden" />
<asp:HiddenField runat="server" ID="SelectedVenueHidden" />
<asp:HiddenField runat="server" ID="SelectedMusicHidden" />
<asp:HiddenField runat="server" ID="SelectedDateHidden" />
<asp:HiddenField runat="server" ID="SelectedEventHidden" />

<asp:HiddenField runat="server" ID="OptionKeyHidden" />
<asp:HiddenField runat="server" ID="OptionMeHidden" />
<asp:HiddenField runat="server" ID="OptionSpotterHidden" />
<asp:HiddenField runat="server" ID="OptionBrandHidden" />
<asp:HiddenField runat="server" ID="OptionCountryHidden" />
<asp:HiddenField runat="server" ID="OptionPlaceHidden" />
<asp:HiddenField runat="server" ID="OptionVenueHidden" />
<asp:HiddenField runat="server" ID="OptionMusicHidden" />
<asp:HiddenField runat="server" ID="OptionDateHidden" />
<asp:HiddenField runat="server" ID="OptionDateDayHidden" />
<asp:HiddenField runat="server" ID="OptionDateDayIncrementHidden" />
<asp:HiddenField runat="server" ID="OptionEventHidden" />
<asp:HiddenField runat="server" ID="OptionGoogleHidden" />


<style>
	.Picker .Label
	{
		width:80px;
		float:left;
		text-align:left;
		padding-right:5px;
	}
	.Picker .Control
	{
		float:left;
	}
	.Picker .Holder
	{
		margin-bottom:5px;
	}
	.Picker .BrandAutoComplete input
	{
		padding-left:3px;
	}
</style>

<div class="Picker" style="xpadding:10px; display:none;" enableviewstate="false" runat="server" id="Holder">
	<div runat="server" id="SearchTypeHolder" class="Holder ClearAfter">
		<div class="Label">
			Search by:
		</div>
		<div class="Control">
			<asp:RadioButton runat="server" GroupName="SearchType" ID="SearchTypeKey" Text="I have the K number" style="display:block;" />
			<asp:RadioButton runat="server" GroupName="SearchType" ID="SearchTypeSpotter" Text="I was given a card with a spotter code" style="display:block;" />
			<asp:RadioButton runat="server" GroupName="SearchType" ID="SearchTypeMe" Text="Events I attended" style="display:block;" />
			<asp:RadioButton runat="server" GroupName="SearchType" ID="SearchTypeVenue" Text="I know the venue" style="display:block;" />
			<asp:RadioButton runat="server" GroupName="SearchType" ID="SearchTypeBrand" Text="I know the name of the promotion" style="display:block;" />
			<asp:RadioButton runat="server" GroupName="SearchType" ID="SearchTypeMusic" Text="I know what music I'm after" style="display:block;" />
			<asp:RadioButton runat="server" GroupName="SearchType" ID="SearchTypeGoogle" Text="I want to search the whole site by google" style="display:block;" />
		</div>
	</div>
	<div runat="server" id="KeyHolder" class="Holder ClearAfter">
		<div class="Label">
			Key:
		</div>
		<div runat="server" id="KeySelectedHolder" class="Control">
			<asp:Label runat="server" ID="KeySelectedLabel" Text="None selected" /> <a href="/" runat="server" id="KeySelectedChangeLink">change</a>
		</div>
		<div runat="server" id="KeyChoiceHolder" class="Control">
			<asp:TextBox runat="server" ID="KeyTextBox" />
			<asp:Button runat="server" ID="KeySearchButton" Text="Go..." />
		</div>
	</div>
	<div runat="server" id="SpotterHolder" class="Holder ClearAfter">
		<div class="Label">
			Code:
		</div>
		<div runat="server" id="SpotterSelectedHolder" class="Control">
			<asp:Label runat="server" ID="SpotterSelectedLabel" Text="None selected" /> <a href="/" runat="server" id="SpotterSelectedChangeLink">change</a>
		</div>
		<div runat="server" id="SpotterChoiceHolder" class="Control">
			<asp:TextBox runat="server" ID="SpotterTextBox" />
			<asp:Button runat="server" ID="SpotterSearchButton" Text="Go..." />
		</div>
	</div>
	<div runat="server" id="BrandHolder" class="Holder ClearAfter">
		<div class="Label">
			Promotion:
		</div>
		<div runat="server" id="BrandSelectedHolder" class="Control">
			<asp:Label runat="server" ID="BrandSelectedLabel" Text="None selected" /> <a href="/" runat="server" id="BrandSelectedChangeLink">change</a>
		</div>
		<div runat="server" id="BrandChoiceHolder" class="Control">
			<js:HtmlAutoComplete runat="server" ID="BrandAutoComplete" CssClass="BrandAutoComplete" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetBrands" Width="250px" />
		</div>
	</div>
	<div runat="server" id="CountryHolder" class="Holder ClearAfter">
		<div class="Label">
			Country:
		</div>
		<div runat="server" id="CountrySelectedHolder" class="Control">
			<asp:Label runat="server" ID="CountrySelectedLabel" Text="None selected" /> <a href="/" runat="server" id="CountrySelectedChangeLink">change</a>
		</div>
		<div runat="server" id="CountryChoiceHolder"  class="Control">
			<asp:DropDownList runat="server" ID="CountryDropDown" />
		</div>
	</div>
	<div runat="server" id="PlaceHolder" class="Holder ClearAfter">
		<div class="Label">
			Town:
		</div>
		<div runat="server" id="PlaceSelectedHolder" class="Control">
			<asp:Label runat="server" ID="PlaceSelectedLabel" Text="None selected" /> <a href="/" runat="server" id="PlaceSelectedChangeLink">change</a>
		</div>
		<div runat="server" id="PlaceChoiceHolder" class="Control">
			<asp:DropDownList runat="server" ID="PlaceDropDown" />
		</div>
	</div>
	<div runat="server" id="VenueHolder" class="Holder ClearAfter">
		<div class="Label">
			Venue:
		</div>
		<div runat="server" id="VenueSelectedHolder" class="Control">
			<asp:Label runat="server" ID="VenueSelectedLabel" Text="None selected" /> <a href="/" runat="server" id="VenueSelectedChangeLink">change</a>
		</div>
		<div runat="server" id="VenueChoiceHolder" class="Control">
			<asp:DropDownList style="display:block;" runat="server" ID="VenueDropDown" />
			<asp:DropDownList style="display:block; margin-top:5px;" runat="server" ID="VenueByLetterDropDown" />
		</div>
	</div>
	<div runat="server" id="MusicHolder" class="Holder ClearAfter">
		<div class="Label">
			Music:
		</div>
		<div runat="server" id="MusicSelectedHolder" class="Control">
			<asp:Label runat="server" ID="MusicSelectedLabel" Text="All music" /> <a href="/" runat="server" id="MusicSelectedChangeLink">change</a>
		</div>
		<div runat="server" id="MusicChoiceHolder" class="Control">
			<asp:DropDownList runat="server" ID="MusicDropDown" />
		</div>
	</div>
	<div runat="server" id="DateHolder" class="Holder ClearAfter">
		<div class="Label">
			Date:
		</div>
		<div runat="server" id="DateSelectedHolder" class="Control">
			<asp:Label runat="server" ID="DateSelectedLabel" Text="" /> <a href="/" runat="server" id="DateSelectedChangeLink">change</a>
		</div>
		<div runat="server" id="DateChoiceHolder" class="Control" style="width:300px;">
			<div runat="server" id="DateDayHolder">
				<div style="float:left;">
					<asp:DropDownList runat="server" ID="DateDayDropDown" style="margin:0px;" />
				</div>
				<div style="float:left; position:relative; left:-1px; margin-right:6px;">
					<img runat="server" id="DateDayPlusImg" src="/gfx/plus.gif" width="9" height="9" style="cursor:pointer; display:block;" />
					<img runat="server" id="DateDayMinusImg" src="/gfx/minus.gif" width="9" height="9" style="cursor:pointer; display:block; position:relative; top:-1px;" />
				</div>
			</div>
			<div style="float:left;">
				<asp:DropDownList runat="server" ID="DateMonthDropDown" style="margin:0px;" />
			</div>
			<div style="float:left; position:relative; left:-1px; margin-right:6px;">
				<img runat="server" id="DateMonthPlusImg" src="/gfx/plus.gif" width="9" height="9" style="cursor:pointer; display:block;" />
				<img runat="server" id="DateMonthMinusImg" src="/gfx/minus.gif" width="9" height="9" style="cursor:pointer; display:block; position:relative; top:-1px;" />
			</div>
			<div style="float:left;">
				<asp:TextBox runat="server" ID="DateYearTextBox" Columns="4" MaxLength="4" style="margin:0px;" />
			</div>
			<div style="float:left; position:relative; left:-1px;">
				<img runat="server" id="DateYearPlusImg" src="/gfx/plus.gif" width="9" height="9" style="cursor:pointer; display:block;" />
				<img runat="server" id="DateYearMinusImg" src="/gfx/minus.gif" width="9" height="9" style="cursor:pointer; display:block; position:relative; top:-1px;" />
			</div>
		</div>
	</div>
	<div runat="server" id="EventHolder" class="Holder ClearAfter">
		<div class="Label">
			Event:
		</div>
		<div runat="server" id="EventSelectedHolder" class="Control">
			<asp:Label runat="server" ID="EventSelectedLabel" Text="None selected" /> <a href="/" runat="server" id="EventSelectedChangeLink">change</a>
		</div>
		<div runat="server" id="EventChoiceHolder" class="Control">
			<asp:ListBox runat="server" ID="EventListBox" Width="400px"></asp:ListBox>
		</div>
	</div>
</div>
