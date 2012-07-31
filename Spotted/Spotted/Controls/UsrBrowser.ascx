<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsrBrowser.ascx.cs" Inherits="Spotted.Controls.UsrBrowser" %>
<dsi:CustomIntro runat="server" ID="uiHeader" CloseDiv="False">
	<p>
		<asp:Literal runat="server" ID="uiIconHtmlLiteral"/>
		<asp:Label runat="server" id="uiDescriptionLabel"/>
	</p>
	<p>
		<asp:PlaceHolder Runat="server" ID="uiUsrsListOrder"></asp:PlaceHolder>
	</p>
	<p>
		<asp:PlaceHolder Runat="server" ID="uiUsrsListLinks"></asp:PlaceHolder>
	</p></dsi:CustomIntro>
	
	<p runat="server" id="uiUsrsListPageLinksP" align="center">
		<asp:PlaceHolder Runat="server" ID="uiUsrsListPageLinks"></asp:PlaceHolder>
	</p>
	<style>
		.ForceNarrow
		{
			overflow:hidden;
			width:83px;
		}
	</style>
	<p runat="server" id="uiUsrsDataListP" class="CleanLinks">
		<asp:DataList Runat="server" 
			RepeatLayout="Table" 
			RepeatColumns="6"
			ID="uiUsrsDataList" 
			RepeatDirection="Horizontal"
			ItemStyle-HorizontalAlign="Center"
			ItemStyle-VerticalAlign="top"
			CellSpacing="10">
		</asp:DataList>
	</p>
	<asp:Literal runat="server" id="uiUsrsDataListNoRecordsLiteral" />
</div>
