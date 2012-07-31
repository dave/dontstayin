<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Stats.ascx.cs" Inherits="Spotted.Admin.Stats" %>

<dsi:h1 runat="server" ID="H11" NAME="H11">DontStayIn web stats</dsi:h1>
<div class="ContentBorder">
	<p>
		These stats update every ten minutes - they were last updated at <%= DateTime.Now.ToString("HH:mm:ss") %>.
	</p>
	<p>
		There <asp:Label Runat="server" ID="UsersOnline5MinLabel"></asp:Label> online now*
	</p>
	<p>
		<asp:Label Runat="server" ID="MaxUsersOnline5MinDateLabel"></asp:Label> there were <asp:Label Runat="server" ID="MaxUsersOnline5MinLabel"></asp:Label> users online*
	</p>
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
		<br>
		<table cellpadding="3" cellspacing="0" border="0" class="dataGridStats">
			<tr class="dataGridHeader">
				<td></td>
				<td style="text-align:right;">Unique<br>visitors</td>
				<td style="text-align:right;">Members<br>online</td>
				<td style="text-align:right;">Page<br>impressions&dagger;</td>
				<td style="text-align:right;">Photo&nbsp;views</td>
			</tr>
			<tr>
				<th style="text-align:right;">Last 5 min:</th>
				<td><%= Number(15,1) %></td>
				<td><%= Number(15,12) %></td>
				<td><small>n/a</small></td>
				<td><small>n/a</small></td>
			</tr>
			<tr class="dataGridAltItem">
				<th style="text-align:right;">Last 30 min:</th>
				<td><%= Number(130,1) %></td>
				<td><%= Number(130,12) %></td>
				<td><small>n/a</small></td>
				<td><small>n/a</small></td>
			</tr>
			<tr>
				<th style="text-align:right;">Today:</th>
				<td><%= Number(0,1) %></td>
				<td><%= Number(0,12) %></td>
				<td><%= Number(0,2) %></td>
				<td><%= Number(0,11) %></td>
			</tr>
			<tr class="dataGridAltItem">
				<th style="text-align:right;">Est. today total:</th>
				<td><small>n/a</small></td>
				<td><small>n/a</small></td>
				<td><%= Number(10,2) %></td>
				<td><%= Number(10,11) %></td>
			</tr>
			<tr>
				<th style="text-align:right;">Yesterday:</th>
				<td><%= Number(1,1) %></td>
				<td><%= Number(1,12) %></td>
				<td><%= Number(1,2) %></td>
				<td><%= Number(1,11) %></td>
			</tr>
			<tr class="dataGridAltItem">
				<th style="text-align:right;">Last 7 days:</th>
				<td><%= Number(2,1) %></td>
				<td><%= Number(2,12) %></td>
				<td><%= Number(2,2) %></td>
				<td><%= Number(2,11) %></td>
			</tr>
			<tr>
				<th style="text-align:right;">Last 30 days:</th>
				<td><%= Number(3,1) %></td>
				<td><%= Number(3,12) %></td>
				<td><%= Number(3,2) %></td>
				<td><%= Number(3,11) %></td>
			</tr>
		</table>
	</p>
	<p>
		<br>
		<table cellpadding="3" cellspacing="0" border="0" class="dataGridStats">
			<tr class="dataGridHeader">
				<td></td>
				<td style="text-align:right;">New<br>photos</td>
				<td style="text-align:right;">Events<br>added</td>
				<td style="text-align:right;">New<br>members</td>
				<td style="text-align:right;">New&nbsp;members<br>(active)</td>
				<td style="text-align:right;">Discussion<br>comments</td>
				<td style="text-align:right;">Live&nbsp;chat<br>messages</td>
			</tr>
			<tr>
				<th style="text-align:right;">Today:</th>
				<td><%= Number(0,4) %></td>
				<td><%= Number(0,5) %></td>
				<td><%= Number(0,6) %></td>
				<td><%= Number(0,13) %></td>
				<td><%= Number(0,8) %></td>
				<td><%= Number(0,10) %></td>
			</tr>
			<tr class="dataGridAltItem">
				<th style="text-align:right;">Yesterday:</th>
				<td><%= Number(1,4) %></td>
				<td><%= Number(1,5) %></td>
				<td><%= Number(1,6) %></td>
				<td><%= Number(1,13) %></td>
				<td><%= Number(1,8) %></td>
				<td><%= Number(1,10) %></td>
			</tr>
			<tr>
				<th style="text-align:right;">Last 7 days:</th>
				<td><%= Number(2,4) %></td>
				<td><%= Number(2,5) %></td>
				<td><%= Number(2,6) %></td>
				<td><%= Number(2,13) %></td>
				<td><%= Number(2,8) %></td>
				<td><%= Number(2,10) %></td>
			</tr>
			<tr class="dataGridAltItem">
				<th style="text-align:right;">Last 30 days:</th>
				<td><%= Number(3,4) %></td>
				<td><%= Number(3,5) %></td>
				<td><%= Number(3,6) %></td>
				<td><%= Number(3,13) %></td>
				<td><%= Number(3,8) %></td>
				<td><%= Number(3,10) %></td>
			</tr>
			<tr>
				<th style="text-align:right;">Total:</th>
				<td><%= Number(5,4) %></td>
				<td><%= Number(5,5) %></td>
				<td><%= Number(5,6) %></td>
				<td><%= Number(5,13) %></td>
				<td><%= Number(5,8) %></td>
				<td><%= Number(5,10) %></td>
			</tr>
		</table>
		<br><br>
	</p>
	<p>
		<small>
			* DontStayIn uses a 5 minute timeout to show how many users are online. Some sites
			use a 30 minute timeout. This means that people who've not used the site for up to 30 minutes 
			are still counted as being active. If we used a 30 minute timeout, the number of users online 
			now would be <asp:Label Runat="server" ID="UsersOnline30MinLabel"></asp:Label>! Our maximum 
			ever users online using the 30 minute timeout is 
			<asp:Label Runat="server" ID="MaxUsersOnline30MinLabel"></asp:Label>.
		</small>
	</p>
	<p>
		<small>
			&dagger; This figure only includes pages that show adverts. The total number of 
			pages served is actually higher than this.
		</small>
	</p>
</div>
