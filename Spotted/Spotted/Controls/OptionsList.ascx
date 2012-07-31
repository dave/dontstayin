<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OptionsList.ascx.cs" Inherits="Spotted.Controls.OptionsList" %>

<dsi:RadioButtonListWithAttributesInViewState runat="server" ID="uiList" RepeatColumns="2" RepeatDirection="Vertical" RepeatLayout="Table" Width="100%"></dsi:RadioButtonListWithAttributesInViewState>

<dsi:InlineScript runat="server">
<script>
function <%= this.ClientID %>_ShowHidePanels(panelID) {
	var optionPanelsContainer = document.getElementById('<%= OptionPanelsContainerID %>');
	for (i=0; i < optionPanelsContainer.childNodes.length; i++) {
		if (optionPanelsContainer.childNodes[i].id == undefined) continue;
		optionPanelsContainer.childNodes[i].style.display = optionPanelsContainer.childNodes[i].id == panelID ? '' : 'none';
	}
}
</script>
</dsi:InlineScript>
