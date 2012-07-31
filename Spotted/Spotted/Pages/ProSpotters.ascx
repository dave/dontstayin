<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProSpotters.ascx.cs" Inherits="Spotted.Pages.ProSpotters" %>

<asp:Panel Runat="server" ID="PanelProSpotters">
	<dsi:h1 runat="server" ID="H11">Pro spotters</dsi:h1>
	<div class="ContentBorder">
		<p>
			Pro spotters are our best photographers. Not only do they take loads of photos, 
			give out loads of cards and introduce loads of people to DontStayIn, but they 
			also take great photos. 
		</p>
		<p>
			Having an expensive camera <b>doesn't</b> automatically make you a pro spotter. 
			You'll have to know how to use it, and consistantly produce great photos with lots 
			of depth and light. 
		</p>
		<p>
			If you think you've got what it takes to be a pro spotter, 
			<a href="/pages/spotters">first sign up as a spotter</a>. When you've uploaded 
			some galleries that you think make the grade, contact 
			<a href="/members/johnb-dsi">JohnB</a>. We'll make you a pro spotter if we 
			think you're good enough.
		</p>
		<p>
			<style>
				.ProSpotterPicCell{
					padding-right:10px;
				}
			</style>
			<asp:DataList Runat="server" ID="ProSpottersDataList"/>
		</p>
	</div>
</asp:Panel>
