<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BannerEditMusic.ascx.cs" Inherits="Spotted.Blank.BannerEditMusic" %>
<%@ Register TagPrefix="Controls" TagName="MusicTypes" Src="/Controls/MusicTypes.ascx" %>
<link href="/support/style.css" rel="stylesheet" type="text/css" />
<style>.ClearAfter:after {	content: "<%= Vars.Opera ? "" : "." %>"; }</style>
<asp:Panel runat="server" class="ContentBorder">
	<Controls:MusicTypes Runat="server" ID="uiMusicTypesControl"/>
	<asp:Button ID="uiSaveButton" runat="server" Text="Save" OnClick="uiSaveButton_Click"/>
	<input id="uiCancelButton" type="button" value="Cancel" onclick="closeWindowAndReturn()"/>
	<script>
		
		if(<%= SaveWasClicked.ToString().ToLower() %>){
			opener.SetMusicTargettingString('<%= GetCommaSeparatedStringOfMusicTypeKs() %>');
		    closeWindowAndReturn();
		}
		function closeWindowAndReturn(){
			window.close();
			opener.focus();
		}
	</script>
</asp:Panel>
