<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Multi.ascx.cs" Inherits="Spotted.Controls.Multi" %>
<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo" %>
<table cellpadding="0" cellspacing="0">
	<tr>
		<td>
			<select runat="server" id="SelectBox" NAME="SelectBox" multiple/></td>
		<td valign="bottom" style="position:relative;left:-1px;">
			<button runat="server" id="RemoveButton" style="margin-bottom:0;border:solid 1px #999999;margin-left:-1;font-weight:bold;width:60px;">Remove</button><br>
			<button runat="server" id="AddButton" style="border:solid 1px #999999;margin-top:-1;margin-left:-1;font-weight:bold;width:60px;">Add</button>
		</td>
	</tr>
</table><table cellpadding="0" cellspacing="0">
	<tr>
		<td style="position:relative;top:-2px;" colspan="2" XMLNS:DbCombo="http://schemas.cambro.net/dbcombo">
			<DbCombo:DbCombo Runat="server" ID="Combo"
				OnLoad="DbComboOnLoad"
				CloseResultsOnBlur="True" 
				CloseResultsOnClick="False" 
				CloseResultsOnEnter="False" 
				CloseResultsOnTab="False" />
		</td>
	</tr>
</table>
<input runat="server" type="hidden" id="Values" value=""/>
<input runat="server" type="hidden" id="Texts" value=""/>
<script>
function DbComboMultiOnSelect_<%= this.ClientID %>(Value, Text, SelectionType)
{
	var UniqueID = "<%= this.UniqueID %>";
	var ClientID = "<%= this.ClientID %>";
	if (SelectionType==2)
	{
		DbComboMultiAddItemGeneric(ClientID, UniqueID, Value, Text);
	}
}
</script>
