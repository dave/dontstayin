<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MixmagListings.ascx.cs" Inherits="Spotted.Admin.MixmagListings" %>
<div class="ContentBorder">
	<h2>Choose issue</h2>
	<p>
		<asp:DropDownList runat="server" ID="IssueDrop"></asp:DropDownList>
	</p>
	<h2>Zone</h2>
	<p>
		<asp:DropDownList runat="server" ID="ZoneDrop" />
	</p>
	<p>
		<asp:Button runat="server" OnClick="Generate" Text="Generate" />
	</p>
</div>
