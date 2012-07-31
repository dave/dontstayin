<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateControl.ascx.cs" Inherits="Spotted.Controls.DateControl" %>
<dsi:InlineScript runat="server" ID="uiSetDaysInMonthScript">
<script>
	function DaysInMonth(iMonth, iYear)
	{
		return 32 - new Date(iYear, iMonth-1, 32).getDate();
	}
	function setDaysInMonth()
	{
		var month = document.getElementById('<%= uiMonth.ClientID %>').value;
		var year = document.getElementById('<%= uiYear.ClientID %>').value;
		var dayDrop = document.getElementById('<%= uiDay.ClientID %>');
		var day = dayDrop.value;
		var daysInMonth = DaysInMonth(month, year);
		var startIndex = Math.min(daysInMonth, dayDrop.options.length);
		dayDrop.length = daysInMonth;
		for (var i=startIndex; i < daysInMonth; i++)
		{
			dayDrop.options[i].text = (i+1).toString();
			dayDrop.options[i].value = (i+1).toString();
		}
		for (var i=startIndex; i < dayDrop.options.count; i++)
		{
			dayDrop.options.removeAt(i);
		}
		dayDrop.value = Math.min(day, daysInMonth);
	}
</script>
</dsi:InlineScript>
<table cellpadding="0" cellspacing="0" border="1">
	<tr>
		<td runat="server" id="uiDayTd" rowspan="2"><asp:DropDownList runat="server" ID="uiDay"/>&nbsp;</td>
		<td rowspan="2"><asp:DropDownList runat="server" ID="uiMonth"/>&nbsp;</td>
		<td rowspan="2"><asp:TextBox runat="server" ID="uiYear" MaxLength="4" Columns="4" /></td>
		<td valign="bottom">
			<img src="/gfx/plus.gif" width="9" height="9" style="cursor:pointer;display:block;" onclick='document.getElementById("<%= uiYear.ClientID %>").value=(parseInt(document.getElementById("<%= uiYear.ClientID %>").value)+1);<%= ShowDay ? "setDaysInMonth();" : "" %>' />
		</td>
	</tr>
	<tr>
		<td valign="top">
			<img src="/gfx/minus.gif" width="9" height="9" style="cursor:pointer;display:block;" onclick='document.getElementById("<%= uiYear.ClientID %>").value=(parseInt(document.getElementById("<%= uiYear.ClientID %>").value)-1);<%= ShowDay ? "setDaysInMonth();" : "" %>'/>
		</td>
	</tr>
</table>
