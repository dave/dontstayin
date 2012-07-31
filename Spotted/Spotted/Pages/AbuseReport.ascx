<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AbuseReport.ascx.cs" Inherits="Spotted.Pages.AbuseReport" %>

<asp:Panel Runat="server" ID="PanelNone">
	<dsi:h1 runat="server" ID="Header" NAME="H18">Photo abuse</dsi:h1>
	<div class="ContentBorder">
		<p>
			All photo abuse reports have been resolved.
		</p>
	</div>
</asp:Panel>
<asp:Panel Runat="server" ID="PanelAbuse">
	<dsi:h1 runat="server" ID="H11" NAME="H18">Photo abuse</dsi:h1>
	<div class="ContentBorder">
		<p>
			PhotoK: <asp:Label Runat="server" ID="PhotoKLabel"></asp:Label>
		</p>
		<p>
			Description: <asp:Label Runat="server" ID="PhotoStringLabel"></asp:Label>
		</p>
		<asp:Panel Runat="server" ID="PhotoPanel">
			<p>
				<a href="" runat="server" id="PhotoAnchor" target="_blank"><img src="" runat="server" id="PhotoImg" border="0" class="BorderBlack All"></a>
			</p>
		</asp:Panel>
		<asp:Panel Runat="server" ID="NoPhotoPanel">
			<p>
				The photo doesn't exist in the database. It's probably been deleted.
			</p>
		</asp:Panel>
		<h2>
			Photo posted by: 
		</h2>
		<p runat="server" id="AbuseByP"/>
		<h2>
			Abuse reported by:
		</h2>
		<p runat="server" id="ReportByP"/>
		<h2>
			Report description:
		</h2>
		<p runat="server" id="ReportDescriptionP" style="font-size:12px;font-weight:bold;"/>
		
		<asp:Panel Runat="server" ID="ThisGalleryPanel">
			<h2>
				This gallery:
			</h2>
			<p>
				If the photo above is abusive, please check out the gallery below as it may contain further abuses...
			</p>
			<p runat="server" id="ThisGalleryP"></p>
		</asp:Panel>
		
		<h2>
			Recent galleries:
		</h2>
		<p>
			If the photo above is abusive, also please check out other galleries recently added by this user:
		</p>
		<p runat="server" id="GalleriesP"></p>
		
		<asp:Panel Runat="server" ID="ActionsPanel">
			<h2>
				Resolution actions
			</h2>
			<p>
				<asp:RadioButton Runat="server" GroupName="Radios" ID="OverturnRadio" Text="Overturn this abuse report - there is no abuse"></asp:RadioButton>
			</p>
			<p>
				<asp:RadioButton Runat="server" GroupName="Radios" ID="NoAbuseRadio" Text="This report was helpful, but there was no abuse. Do NOT delete the photo."></asp:RadioButton>
			</p>
			<p>
				<asp:RadioButton Runat="server" GroupName="Radios" ID="NoAbuseDeleteRadio" Text="This photo should be deleted, but there was no abuse - e.g. picture was upside-down"></asp:RadioButton>
			</p>
			<p>
				<asp:RadioButton Runat="server" GroupName="Radios" ID="AbuseDeleteRadio" Text="This photo should be deleted (the user who uploaded it will be penalised)"></asp:RadioButton>
			</p>
			<p>
				<asp:RadioButton Runat="server" GroupName="Radios" ID="AbuseDeleteWatchRadio" Text="This photo should be deleted (the user who uploaded it will be penalised), and all further photos uploaded by this user should be moderated prior to going live"></asp:RadioButton>
			</p>
			<p>
				<asp:RadioButton Runat="server" GroupName="Radios" ID="AbuseDeleteBanRadio" Text="This photo should be deleted, and the user should be banned"></asp:RadioButton>
			</p>
			<p>
				<asp:RadioButton Runat="server" GroupName="Radios" ID="AbuseDeleteBanModerateRadio" Text="This photo should be deleted, the user should be banned and all their galleries should be removed pending moderation"></asp:RadioButton>
			</p>
			<p>
				Please enter a few words to explain why this report has been upheld/overturned:<br>
				<asp:TextBox Runat="server" TextMode="MultiLine" Columns="70" Rows="5" ID="ResolveDescriptionTextBox"></asp:TextBox>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Runat="server" Display="Dynamic" ControlToValidate="ResolveDescriptionTextBox" ErrorMessage="Please enter a short description"></asp:RequiredFieldValidator>
			</p>
			<p>
				<asp:Button ID="Button1" Runat="server" OnClick="Action_Click" Text="Resolve"></asp:Button>
			</p>
		</asp:Panel>
		<asp:Panel Runat="server" ID="ResolvedPanel">
			<h2>Report resolved</h2>
			<p>
				Resolution: <asp:Label Runat="server" ID="ResolvedLabel"/>
			</p>
			<p runat="server" id="ResolveDescriptionP" style="font-size:12px;font-weight:bold;"/>
		</asp:Panel>
	</div>
</asp:Panel>
