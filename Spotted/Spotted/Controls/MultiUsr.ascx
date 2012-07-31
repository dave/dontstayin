<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MultiUsr.ascx.cs" Inherits="Spotted.Controls.MultiUsr" %>
<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo" %>
<table cellpadding="0" cellspacing="0" border="0">
	<tr>
		<td valign="bottom" XMLNS:DbCombo="http://schemas.cambro.net/dbcombo" style="position:relative;top:1px;">
			<select runat="server" id="SelectBox" NAME="SelectBox" style="margin-bottom:-2px;" multiple/><br>
			<DbCombo:DbCombo Runat="server" ID="Combo"
				ServerMethod="DbComboGetMultiUsrs"
				ServerType="Spotted.DbComboQueries"
				OnLoad="DbComboOnLoad"
				CloseResultsOnBlur="True" 
				CloseResultsOnClick="False" 
				CloseResultsOnEnter="False" 
				CloseResultsOnTab="False"/>
		</td>
		<td valign="bottom" style="position:relative;left:-2px;" rowspan="2">
			<span runat="server" id="PicHolder"><img src="/gfx/dsi-sign-100.png" width="100" height="100" style="border-top:1px solid #999999;border-right:1px solid #999999;border-left:1px solid #999999;"></span><br>
			<button runat="server" id="AddButton" style="border:solid 1px #999999;margin-top:-1px;font-weight:bold;width:42px;height:16px;">Add</button>
			<button runat="server" id="RemoveButton" style="margin-bottom:0;border:solid 1px #999999;margin-left:-1px;font-weight:bold;width:61px;height:16px;">Remove</button>
		</td>
	</tr>
</table>
<input runat="server" type="hidden" id="Values" value=""/>
<input runat="server" type="hidden" id="Texts" value=""/>
<script>
function DbComboMultiUsrOnSelect_<%= this.ClientID %>(Value, Text, SelectionType)
{
	var UniqueID = "<%= this.UniqueID %>";
	var ClientID = "<%= this.ClientID %>";
	if (SelectionType==2)
	{
		DbComboMultiUsrAddItemGeneric(ClientID, UniqueID, Value, Text);
	}
	else if (SelectionType==1 || SelectionType==3 || SelectionType==8 || SelectionType==9 || SelectionType==10 || SelectionType==11)
	{
		DbComboMultiUsrDisplayPicture(ClientID, UniqueID, Value, Text);
	}
	else if (SelectionType==4 || SelectionType==5 || SelectionType==6 || SelectionType==7)
	{
		DbComboMultiUsrClearPicture(ClientID, UniqueID, "");
	}
}
</script>
