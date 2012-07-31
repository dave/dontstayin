<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SpottersChecklist.ascx.cs" Inherits="Spotted.Controls.SpottersChecklist" %>
<p>
	<style>
		td.CheckListCell
		{
			padding-top:5px;
		}
		table.CheckListTable td
		{
			padding-bottom:5px;
		}
	</style>
	<table cellpadding="2" cellspacing="0" class="CheckListTable">
		<tr>
			<td valign="top">
				<asp:CheckBox Runat="server" ID="Checkbox3"/>
			</td>
			<td valign="top" class="CheckListCell">
				<label for='<%= Checkbox3.ClientID %>'>
					Make sure you have enough cards for the event you are covering. 
					We will send you a pack of cards when you sign up, and we'll 
					send you refills whenever you run out.
				</label>
			</td>
		</tr>
		<tr>
			<td valign="top">
				<asp:CheckBox Runat="server" ID="Checkbox6"/>
			</td>
			<td valign="top" class="CheckListCell">
				<label for='<%= Checkbox6.ClientID %>'>
					<b>DON'T HASSLE PEOPLE.</b>
					Always ask people before you take their photo - say "Hi, can I take your photo 
					for DontStayIn?". If someone says <i>no</i> just say <i>no problem</i> and walk away.  
					
				</label>
			</td>
		</tr>
		<tr>
			<td valign="top">
				<asp:CheckBox Runat="server" ID="Checkbox9"/>
			</td>
			<td valign="top" class="CheckListCell">
				<label for='<%= Checkbox9.ClientID %>'>
					Try to upload your photos within 48 hours of the event.
				</label>
			</td>
		</tr>
		<tr>
			<td valign="top">
				<asp:CheckBox Runat="server" ID="Checkbox2"/>
			</td>
			<td valign="top" class="CheckListCell">
				<label for='<%= Checkbox2.ClientID %>'>
					Before the event, make sure your camera has the correct date and time 
					settings.
				</label>
			</td>
		</tr>
	</table>
</p>
