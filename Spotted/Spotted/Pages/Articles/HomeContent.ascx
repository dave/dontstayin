<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomeContent.ascx.cs" Inherits="Spotted.Pages.Articles.HomeContent" %>
<dsi:h1 runat="server">
	<asp:Label Runat="server" ID="TitleLabel"/>
</dsi:h1>
<div class="ContentBorder ArticleBorder">
	<asp:Panel Runat="server" ID="FrontPage">
		
		<p runat="server" id="PicInfoP" style="width:100%;">
			<!--<img class="BorderBlack All" runat="server" id="Pic" align="right" width="100" height="100"/>-->
			<small runat="server" id="InfoP">
				This article is about
				<asp:PlaceHolder Runat="server" ID="EventPlaceHolder">
					<a runat="server" id="EventAnchor"></a> @ 
				</asp:PlaceHolder>
				<asp:PlaceHolder Runat="server" ID="VenuePlaceHolder">
					<a runat="server" id="VenueAnchor"></a> in 
				</asp:PlaceHolder>
				<asp:PlaceHolder Runat="server" ID="PlacePlaceHolder">
					<a runat="server" id="PlaceAnchor"></a></asp:PlaceHolder><asp:PlaceHolder Runat="server" ID="DatePlaceHolder">, <span runat="server" id="DateSpan"/>
				</asp:PlaceHolder>
				<br />
			</small>
		</p>
		
		<p align="center" runat="server" id="PagePTop"/>
	</asp:Panel>
	<asp:Panel Runat="server" ID="ParaPanel" style="width: 100%;" CssClass="ClearNow">
		<asp:DataList Runat="server" ID="ParaDataList" RepeatLayout="Flow" RepeatDirection="Horizontal"/>
		<asp:PlaceHolder Runat="server" ID="ContentPlaceHolder"/>
	</asp:Panel>
	
	<div style="clear:both;">
		
		<p align="center" style="margin-bottom:15px;" runat="server" id="PagePBottom"/>

		<p runat="server" id="OwnerP">
			<center>Article by <a runat="server" id="OwnerLink"/>, viewed <asp:Label Runat="server" ID="ViewsLabel" /></center>
		</p>
		<p runat="server" id="NonMixmagP">
			<center><small>Anyone can add an article to DontStayIn - <a href="/pages/myarticles/mode-list">click here to add your own!</a></small></center>
		</p>
		<p runat="server" id="MixmagP">
			<center>This is a <a href="/pages/mixmag"><img src="/gfx/logo-mixmag-small.png" border="0" align="absmiddle" width="100" height="22"></a> article.</center>
		</p>
	</div>
</div>
