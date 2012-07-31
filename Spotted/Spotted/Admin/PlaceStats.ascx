<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PlaceStats.ascx.cs" Inherits="Spotted.Admin.PlaceStats" %>
<h1>Place stats for <asp:Label Runat="server" ID="PlaceName"></asp:Label></h1>
<div class="ContentBorder">
	<p>
		<table runat="server" id="Tab" cellpadding="2" cellspacing="0">
			<tr>
				<th></th>
				<td valign="bottom" colspan="5" align="center">All users</td>
				<th valign="bottom">&nbsp;&nbsp;&nbsp;</th>
				<td valign="bottom" colspan="5" align="center">Users active in the last month</td>
			</tr>
			<tr>
				<th></th>
				<th valign="bottom">All users</th>
				<th valign="bottom">Email</th>
				<th valign="bottom">Flyers</th>
				<th valign="bottom">Invites</th>
				<th valign="bottom">Texts</th>
				<th valign="bottom">&nbsp;&nbsp;&nbsp;</th>
				<th valign="bottom">All users</th>
				<th valign="bottom">Email</th>
				<th valign="bottom">Flyers</th>
				<th valign="bottom">Invites</th>
				<th valign="bottom">Texts</th>
			</tr>
		</table>
	</p>
</div>
