<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Spottings.ascx.cs" Inherits="Spotted.Pages.Usrs.Spottings" %>

<asp:Panel Runat="server" ID="PanelSpottings">
	<dsi:UsrIntro runat="server" ID="UsrIntro" CloseDiv="false">
		<p>
			<img src="/gfx/spotter.gif" border="0" align="absmiddle" style="margin-right:3px;" runat="server" id="SpotterIcon">Listed 
			below are the people I've spotted:
		</p>
		<p>
			<asp:PlaceHolder Runat="server" ID="ListOrder"></asp:PlaceHolder>
		</p>
		<p>
			<asp:PlaceHolder Runat="server" ID="ListLinks"></asp:PlaceHolder>
		</p>
	</dsi:UsrIntro>
		<p runat="server" id="ListPageLinksP" align="center">
			<asp:PlaceHolder Runat="server" ID="ListPageLinks"></asp:PlaceHolder>
		</p>
		<style>
			.ForceNarrow
			{
				overflow:hidden;
				width:83px;
			}
		</style>
		<p runat="server" id="DataListP" class="CleanLinks">
			<asp:DataList Runat="server" 
				RepeatLayout="Table" 
				RepeatColumns="6"
				ID="SpottingsDataList" 
				RepeatDirection="Horizontal"
				ItemStyle-HorizontalAlign="Center"
				ItemStyle-VerticalAlign="top"
				CellSpacing="10">
			</asp:DataList></p>
		<p runat="server" id="NoRecordsP">
			<small>No members here. You can all view a list of all members ordered by the date they signed up - click <a href="" runat="server" id="NoRecordsNewAnchor">newest members</a>.</small>
		</p>
	</div>
</asp:Panel>
