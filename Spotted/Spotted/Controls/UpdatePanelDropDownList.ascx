<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UpdatePanelDropDownList.ascx.cs" Inherits="Spotted.Controls.UpdatePanelDropDownList" %>
<asp:HiddenField runat="server" ID="uiValue" />
<asp:DropDownList runat="server" ID="uiList" OnSelectedIndexChanged="uiList_SelectedIndexChanged"></asp:DropDownList>
<dsi:InlineScript ID="InlineScript1" runat="server">
<script>
function <%= this.SetValueFunctionName %>(selectbox) {
	if (selectbox == undefined) return;
	var selectedOption;
	var i;
	for (i=0; i < selectbox.options.length; i++) {
		if (selectbox.options[i].selected) {
			selectedOption = selectbox.options[i];
			break;
		}
	}
	if (selectedOption != undefined) {
		document.getElementById('<%= uiValue.ClientID %>').value = selectedOption.value;
		if (selectedOption.value == '0') {
			for (i=0; i < selectbox.options.length; i++) {
				if (selectbox.options[i].value == '0') {
					selectbox.options[i].selected = true;
					break;
				}
			}
		}
	}
}
function <%= this.ClearFunctionName %>() {
	selectbox = document.getElementById('<%= uiList.ClientID %>');
	if (selectbox != undefined) {
		<%= this.SetValueFunctionName %>(selectbox);
		selectbox.innerHTML = '';
	}
}
</script>
</dsi:InlineScript>
