<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Cal.ascx.cs" Inherits="Spotted.Controls.Cal" %>

<asp:Panel Runat="server" ID="CalendarPanel">
	<dsi:h1 runat="server" ID="H14">Calendar</dsi:h1>
	<div class="ContentBorder">
		<table cellpadding="3" cellspacing="0" border="0" width="100%">
			<tr>
				<td align="center" width="20%" valign="middle" style="font-size:12px;">
					<nobr><b><a href="" runat="server" id="CalendarBackLink"><img src="/gfx/icon-back-12.png" style="margin-right:3px;" width="12" height="21" align="absmiddle" border="0"><asp:Label Runat="server" ID="CalendarBackLabel"/></a></b></nobr>
				</td>
				<td runat="server" id="CalendarTitleCell" align="center" width="60%" valign="middle" style="font-size:12px;">
					<b><a href="" runat="server" id="CalendarMiddleLink"></a></b>
				</td>
				<td align="center" width="20%" valign="middle" style="font-size:12px;">
					<nobr><b><a href="" runat="server" id="CalendarForwardLink"><asp:Label Runat="server" ID="CalendarForwardLabel"/><img src="/gfx/icon-forward-12.png" style="margin-left:3px;" width="12" height="21" align="absmiddle" border="0"></a></b></nobr>
				</td>
			</tr>
		</table>
		<center>
			<asp:Calendar 
				Runat="server" 
				ID="DateCal" 
				NextPrevFormat="FullMonth"
				
				
				SelectedDayStyle-ForeColor="Black"
				
				NextPrevStyle-Font-Bold="True"
				DayHeaderStyle-CssClass = "CalDayHeader"
				
				ShowDayHeader="True"
				ShowTitle="False"
			
				CellPadding="3"
				BorderWidth="0" 
				OnDayRender="DateCal_DayRender" 
				Width="60%"></asp:Calendar>
		</center>
	</div>
</asp:Panel>
