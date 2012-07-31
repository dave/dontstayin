<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpdateOptions.ascx.cs" Inherits="Spotted.Pages.UpdateOptions" %>

<dsi:h1 runat="server" ID="H19" NAME="H11">Weekly email options</dsi:h1>
<div class="ContentBorder">
	<p>
		Our weekly update email is sent every Wednesday morning to help you plan your 
		weekend! Promoters don't have to pay to be listed, so it's not just the big 
		events that are listed. Below are some options to make sure you only get sent 
		the events that you're interested in.
	</p>
	<p>
		<asp:CheckBox Runat="server" ID="EmailCheck" Text="&nbsp;Send me the update email" AutoPostBack="True" OnCheckedChanged="Save"/>
	</p>
	<asp:Panel Runat="server" ID="OptionsPanel">
		<h2>Music</h2>
		<p>
			Send me events playing my favourite music: 
		<p>
			<asp:Label Runat="server" ID="MusicLabel"/> <b><a href="/pages/mymusic/nextpage-updateoptions">change</a></b>
		</p>
		<p runat="server" id="GenericMusicP">
			<asp:CheckBox Runat="server" ID="MusicGeneric" Text="&nbsp;Also send me these general music types: " AutoPostBack="True" OnCheckedChanged="Save"/><small><asp:Label Runat="server" ID="GenericMusicLabel"/></small>
		</p>
		<h2>Places</h2>
		<p>
			Send me events in places I visit:
		</p>
		<p>
			<asp:Label Runat="server" ID="PlacesLabel"/> <b><a href="/pages/placesivisit/nextpage-updateoptions">change</a></b>
		</p>
	</asp:Panel>
	<p>
		<asp:Button ID="Button1" Runat="server" OnClick="Save" Text="Save changes"></asp:Button>
	</p>
</div>
