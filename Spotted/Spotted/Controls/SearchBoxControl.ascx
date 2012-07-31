<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchBoxControl.ascx.cs" Inherits="Spotted.Controls.SearchBoxControl" %>
<asp:Panel ID="uiPanel" DefaultButton="uiSubmitSearchButton">
	<asp:TextBox ID="uiSearchQuery" runat="server" Width="180px" MaxLength="100"></asp:TextBox>
	<ajaxToolkit:AutoCompleteExtender 
			runat="server" 
			ID="uiTagAutoComplete" 
			TargetControlID="uiSearchQuery"
			ServiceMethod="GetTagSearchString"
			ServicePath="~\WebServices\AutoComplete.asmx"
			MinimumPrefixLength="1" 
			CompletionInterval="1000"
			EnableCaching="false"
			CompletionSetCount="5" 
			CompletionListCssClass="autocomplete_completionListElement" 
			CompletionListItemCssClass="autocomplete_listItem" 
			CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
			DelimiterCharacters="," >
			 
		</ajaxToolkit:AutoCompleteExtender>
	<asp:Button ID="uiSubmitSearchButton" runat="server" Text="Search" CausesValidation="false" />
</asp:Panel>
