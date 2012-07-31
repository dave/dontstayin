<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TimeControl.ascx.cs" Inherits="Spotted.Controls.TimeControl" %>
<script language="JavaScript">
  function Focus()
  {
	if(document.getElementById("<%=  HourTextBoxControl.ClientID  %>").value == "HH")
	{
		document.getElementById("<%=  HourTextBoxControl.ClientID  %>").value = "";
	}
	if(document.getElementById("<%=  MinuteTextBoxControl.ClientID  %>").value == "MM")
	{
		document.getElementById("<%=  MinuteTextBoxControl.ClientID  %>").value = "";
	}
  }  
</script>
<table border="0" cellpadding="0" cellspacing="0" onclick="Focus();" onfocus="Focus();">
	<tr>
		<td class="BorderKeyline All BackgroundWhite"><nobr><asp:TextBox ID="HourTextBoxControl" runat="server" Text="HH" Width="22" MaxLength="2" BorderWidth="0"></asp:TextBox>:<asp:TextBox ID="MinuteTextBoxControl" runat="server" Text="MM" Width="25" MaxLength="2" BorderWidth="0"></asp:TextBox></nobr></td>
		<td><asp:RangeValidator id="HourRangeValidator" Runat="server" ErrorMessage="<nobr>Hour: 0-23</nobr>" ControlToValidate="HourTextBoxControl" Display="Dynamic" MaximumValue="23" MinimumValue="0" Type="Integer" ValidationGroup="TimeValidation"></asp:RangeValidator>
			<asp:RangeValidator id="MinuteRangeValidator" Runat="server" ErrorMessage="<nobr>Min: 0-59</nobr>" ControlToValidate="MinuteTextBoxControl" Display="Dynamic" MaximumValue="59" MinimumValue="0" Type="Integer" ValidationGroup="TimeValidation"></asp:RangeValidator></td>
	</tr>
</table>
