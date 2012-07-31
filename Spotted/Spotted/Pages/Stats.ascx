<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Stats.ascx.cs" Inherits="Spotted.Pages.Stats" %>

<dsi:h1 runat="server">DontStayIn web stats</dsi:h1>
<div class="ContentBorder">
	<h2>
		Online now
	</h2>
	<p>
		<asp:Label Runat="server" ID="UsersOnline5MinLabel"/> online now (active in the last 5 minutes).
		<asp:Label Runat="server" ID="MaxUsersOnline5MinDateLabel"></asp:Label> there were <asp:Label Runat="server" ID="MaxUsersOnline5MinLabel"></asp:Label> members online.
	</p>
	<h2>
		Site traffic
	</h2>
	<p>
		<style>
			tr.dataGridAltItem td 
			{
				background-color:#FED551;
			}

			.dataGridStats td
			{
				text-align:right;
				border-right:solid 1px #CBA21E;
				border-left:solid 0px #CBA21E;
			}
			.dataGridHeader td
			{
				text-align:right;
				border-right:solid 1px #CBA21E;
				border-left:solid 0px #CBA21E;
				
			}
			td.dataGridPlainCell
			{
				border:solid 0px #ffffff;
			}
		</style>
		<table cellpadding="3" cellspacing="0" border="0" class="dataGridStats">
			<tr class="dataGridHeader">
				<td></td>
				<td style="text-align:right;">Members<br />logged on</td>
				<td style="text-align:right;" runat="server" visible="false">Page<br>impressions</td>
				<td style="text-align:right;">Photo&nbsp;views</td>
			</tr>
			<tr>
				<th style="text-align:right;">Today:</th>
				<td><%= Number(0,7) %></td>
				<td runat="server" visible="false"><%= Number(0,2) %></td>
				<td><%= Number(0,11) %></td>
			</tr>
			<tr class="dataGridAltItem">
				<th style="text-align:right;">Yesterday:</th>
				<td><%= Number(1,7) %></td>
				<td runat="server" visible="false"><%= Number(1,2) %></td>
				<td><%= Number(1,11) %></td>
			</tr>
			<tr>
				<th style="text-align:right;">Month to date&dagger;:</th>
				<td><%= Number(3,7) %></td>
				<td runat="server" visible="false"><%= Number(3,2) %></td>
				<td><%= Number(3,11) %></td>
			</tr>
		</table>
	</p>
	<h2>
		Database stats
	</h2>
	<p>
		<table cellpadding="3" cellspacing="0" border="0" class="dataGridStats">
			<tr class="dataGridHeader">
				<td></td>
				<td style="text-align:right;">New<br>photos</td>
				<td style="text-align:right;">Events<br>added</td>
				<td style="text-align:right;">* New<br>members</td>
				<td style="text-align:right;">Discussion<br>comments</td>
			</tr>
			<tr>
				<th style="text-align:right;">Today:</th>
				<td><%= Number(0,4) %></td>
				<td><%= Number(0,5) %></td>
				<td><%= Number(0,13) %></td>
				<td><%= Number(0,8) %></td>
			</tr>
			<tr class="dataGridAltItem">
				<th style="text-align:right;">Yesterday:</th>
				<td><%= Number(1,4) %></td>
				<td><%= Number(1,5) %></td>
				<td><%= Number(1,13) %></td>
				<td><%= Number(1,8) %></td>
			</tr>
			<tr>
				<th style="text-align:right;">Month to date&dagger;:</th>
				<td><%= Number(3,4) %></td>
				<td><%= Number(3,5) %></td>
				<td><%= Number(3,13) %></td>
				<td><%= Number(3,8) %></td>
			</tr>
			<tr class="dataGridAltItem">
				<th style="text-align:right;">Total:</th>
				<td><%= Number(5,4) %></td>
				<td><%= Number(5,5) %></td>
				<td><%= Number(5,13) %></td>
				<td><%= Number(5,8) %></td>
			</tr>
		</table>
	</p>
	<p>
		<small>* This only counts members who have completed their details and verified their email address.</small>
	</p>
	<p>
		<small>&dagger; Actually this is the last 31 days.</small>
	</p>
</div>
