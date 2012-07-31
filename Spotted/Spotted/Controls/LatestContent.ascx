<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LatestContent.ascx.cs" Inherits="Spotted.Controls.LatestContent" %>
<!--%@ OutputCache Duration="600" VaryByCustom="Country;PageName" VaryByParam="*" %-->
<%@ Register TagPrefix="Spotted" TagName="EventList" Src="/Controls/EventList.ascx" %>

<asp:Panel Runat="server" ID="ArticlesPanel">
	<asp:DataList Runat="server" ID="ArticlesDataList" RepeatLayout="Flow" RepeatDirection="Horizontal"></asp:DataList>
</asp:Panel>

<asp:Panel Runat="server" ID="ContentPanel" EnableViewState="False">

	<div class="ClearAfter" runat="server" id="SummaryPanel">
		<div style="width:309px; border:0px; padding-left:8px; position:relative; float:right;"><asp:PlaceHolder Runat="server" ID="RightPh"/></div>
		<div style="width:309px; border:0px; padding-right:8px; position:relative; float:left;"><asp:PlaceHolder Runat="server" ID="LeftPh"/></div>
	</div>
	
	
	<asp:Panel Runat="server" ID="CompPanel">
		<dsi:h1 runat="server">Competitions<!--<img src="/gfx/icon-comp.png" border="0" width="26" height="21" class="TabbedHeadingIcon">--></dsi:h1>
		<div class="ContentBorder Padding">
			<div class="LatestPanel CleanLinks" runat="server" id="CompPanelInner">
				<p>
					<asp:DataList Runat="server" ID="CompDataList" RepeatLayout="Flow" RepeatDirection="Horizontal"></asp:DataList>
				</p>
			</div>
			<div align="right" runat="server" id="CompArchiveDiv" class="LatestPanelArchiveLink">
				<small><a href="" runat="server" id="CompArchiveAnchor">Competitions archive<img src="/gfx/icon-calendar.png" border="0" align="absmiddle" style="margin-left:3px;" width="26" height="21"></a></small>
			</div>
		</div>
	</asp:Panel>
	<asp:Panel Runat="server" ID="NewsPanel">
		<dsi:h1 ID="H1" runat="server">News<!--<img src="/gfx/icon-news.png" border="0" width="26" height="21" class="TabbedHeadingIcon">--></dsi:h1>
		<div class="ContentBorder Padding">
			<div class="LatestPanel CleanLinks" runat="server" id="NewsPanelInner">
				<p>
					<asp:DataList Runat="server" ID="NewsDataList" RepeatLayout="Flow" RepeatDirection="Horizontal"></asp:DataList>
				</p>
			</div>
			<div align="right" runat="server" id="NewsArchiveDiv" class="LatestPanelArchiveLink">
				<small><a href="" runat="server" id="NewsArchiveAnchor">News archive<img src="/gfx/icon-calendar.png" border="0" align="absmiddle" style="margin-left:3px;" width="26" height="21"></a></small>
			</div>
		</div>
	</asp:Panel>
	<asp:Panel Runat="server" ID="ReviewsPanel">
		<dsi:h1 ID="H2" runat="server">Reviews<!--<img src="/gfx/icon-chatter.png" border="0" width="26" height="21" class="TabbedHeadingIcon">--></dsi:h1>
		<div class="ContentBorder Padding">
			<div class="LatestPanel CleanLinks" runat="server" id="ReviewsPanelInner">
				<p>
					<asp:DataList Runat="server" ID="ReviewsDataList" RepeatLayout="Flow" RepeatDirection="Horizontal"></asp:DataList>
				</p>
			</div>
			<div align="right" runat="server" id="ReviewsArchiveDiv" class="LatestPanelArchiveLink">
				<small><a href="" runat="server" id="ReviewsArchiveAnchor">Reviews archive<img src="/gfx/icon-calendar.png" border="0" align="absmiddle" style="margin-left:3px;" width="26" height="21"></a></small>
			</div>
		</div>
	</asp:Panel>
	<asp:Panel Runat="server" ID="GroupsPanel">
		<dsi:h1 runat="server">Groups<!--<img src="/gfx/icon-group.png" border="0" width="26" height="21" class="TabbedHeadingIcon">--></dsi:h1>
		<div class="ContentBorder Padding">
			<div class="LatestPanel CleanLinks" runat="server" id="GroupsPanelInner">
				<p>
					<asp:DataList Runat="server" ID="GroupsDataList" RepeatLayout="Flow" RepeatDirection="Horizontal"></asp:DataList>
				</p>
			</div>
			<div align="right" runat="server" id="GroupsDiv" class="LatestPanelArchiveLink">
				<small><a href="" runat="server" id="GroupsAnchor">All groups<img src="/gfx/icon-group.png" border="0" align="absmiddle" style="margin-left:3px;" width="26" height="21"></a></small>
			</div>
		</div>
	</asp:Panel>
	
</asp:Panel>

<asp:Panel Runat="server" ID="GalleriesPanel">
	<dsi:h1 runat="server">
		Galleries
	</dsi:h1>
	<div class="ContentBorder">
		<div style="padding:0px;" class="CleanLinks">
			<p>
				<asp:DataList Runat="server" ID="GalleriesDataList" ItemStyle-Width="25%" RepeatLayout="Table" RepeatDirection="Horizontal" RepeatColumns="4" ItemStyle-VerticalAlign=Top ItemStyle-HorizontalAlign=Center></asp:DataList>
			</p>
		</div>
		<div align="right" runat="server" id="GalleriesArchiveDiv"><small><a href="" runat="server" id="GalleriesArchiveAnchor">Galleries archive<img src="/gfx/icon-calendar.png" border="0" align="absmiddle" style="margin-left:3px;" width="26" height="21"></a></small></div>
	</div>
</asp:Panel>


