<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyMusic.ascx.cs" Inherits="Spotted.Pages.MyMusic" %>
<%@ Register TagPrefix="Controls" TagName="MusicTypes" Src="/Controls/MusicTypes.ascx" %>

<dsi:h1 runat="server">Music I listen to</dsi:h1>
<div class="ContentBorder">
	<p>
		Tick your favourite music types. If you have a preference - e.g. "US House", tick the subcategory.
		If you don't have a preference, there's no need to tick all of the subcategories, just tick the main category - e.g. "All House".
	</p>
	<p>
		<asp:Button ID="Button1" Runat="server" runat="server" OnClick="Update" Text="Update" /> <asp:Label Runat="server" ID="UpdatedLabel" ForeColor="#0000ff" Visible="False">Changes saved.</asp:Label>
	</p>
	<p>
		<Controls:MusicTypes Runat="server" ID="MusicTypes"/>
	</p>
</div>
