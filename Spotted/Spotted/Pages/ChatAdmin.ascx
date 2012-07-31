<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChatAdmin.ascx.cs" Inherits="Spotted.Pages.ChatAdmin" %>
<asp:Panel Runat="server" ID="PanelOptions">
	<dsi:h1 runat="server" ID="Header" NAME="H18">Advanced topic options</dsi:h1>
	<div class="ContentBorder">
		<p>
			<img src="/gfx/icon-discuss.png" border="0" align="absmiddle" style="margin-right:3px;">On 
			this page you can change some of the properties of this topic.
		</p>
		<p>
			Topic: <a href="" runat="server" id="ThreadSubjectAnchor" target="_blank"></a>
		</p>
		<p>
			Forum: <a href="" runat="server" id="ThreadForumAnchor" target="_blank"></a>
		</p>
		<p>
			Group: <a href="" runat="server" id="ThreadGroupAnchor" target="_blank"></a>
		</p>
	</div>
	<dsi:h1 runat="server" ID="H12" NAME="H18">Options</dsi:h1>
	<div class="ContentBorder">
		<p>
			<span runat="server" id="ClosedSpan"><asp:CheckBox Runat="server" ID="ClosedCheckBox" Text="Closed <small> - posting disabled</small>"></asp:CheckBox></span>
		</p>
		<p>
			<span runat="server" id="NewsSpan"><asp:CheckBox Runat="server" ID="NewsCheckBox" Text="News <small> - suggest this topic to our news admin team</small>"></asp:CheckBox></span>
		</p>
		<p>
			<span runat="server" id="PrivateSpan"><asp:CheckBox Runat="server" ID="PrivateCheckBox" Text="Private <small> - only people who have been invited can read the topic</small>"></asp:CheckBox></span>
		</p>
		<p>
			<span runat="server" id="SealedSpan"><asp:CheckBox Runat="server" ID="SealedCheckBox" Text="Sealed <small> - only the owner may invite new participants (only for private topics)</small>"></asp:CheckBox></span>
		</p>
		<p>
			<asp:Button ID="Button1" Runat="server" OnClick="UpdateOptions_Click" Text="Update options" CausesValidation="False"></asp:Button>
		</p>
		<script>
			
			var NewsSpan = document.getElementById('<%= NewsSpan.ClientID %>');
			var PrivateSpan = document.getElementById('<%= PrivateSpan.ClientID %>');
			var SealedSpan = document.getElementById('<%= SealedSpan.ClientID %>');
			
			var NewsCheckBox = document.getElementById('<%= NewsCheckBox.ClientID %>');
			var PrivateCheckBox = document.getElementById('<%= PrivateCheckBox.ClientID %>');
			var SealedCheckBox = document.getElementById('<%= SealedCheckBox.ClientID %>');
			
			var IsGroup = <%= CurrentThread.GroupK>0?"true":"false" %>;
			
		</script>
	</div>
	<asp:Panel Runat="server" ID="ChangeForumPanel">
		<dsi:h1 runat="server" ID="H11" NAME="H18">Change forum</dsi:h1>
		<div class="ContentBorder">
			<p>
				You can change the forum that this topic is posted in below:
			</p>
			<p>
				<asp:RadioButton Runat="server" onclick="UpdateCombo();" ID="ScopeEvent" GroupName="Scope" Text="An event"/>
				<asp:CheckBox Runat="server" onclick="CheckEventRadio();UpdateCombo();" ID="ScopeEventFuture" Text="<small>Just future events</small>"/>
				<asp:CheckBox Runat="server" onclick="CheckEventRadio();UpdateCombo();" ID="ScopeEventAttend" Text="<small>Just events I'm attending</small>"/><br>
				<asp:RadioButton Runat="server" onclick="UpdateCombo();" ID="ScopeVenue" GroupName="Scope" Text="A venue"/><br>
				<asp:RadioButton Runat="server" onclick="UpdateCombo();" ID="ScopePlace" GroupName="Scope" Text="A town or place"/><br>
				<asp:RadioButton Runat="server" onclick="UpdateCombo();" ID="ScopeCountry" GroupName="Scope" Text="A country"/><br>
				<asp:RadioButton Runat="server" onclick="UpdateCombo();" ID="ScopeGeneral" GroupName="Scope" Text="General"/>
			</p>
			<asp:CustomValidator Runat="server" Display="Dynamic" EnableClientScript="False" OnServerValidate="ScopeVal" ErrorMessage="<p>Please select a subject matter above</p>" ID="Customvalidator3" NAME="Customvalidator1"/>
			<p>
				If you've not chosen 'General' above, choose the forum that you would like to move this topic to with the drop-down below:
			</p>
			<p>
				<js:HtmlAutoComplete runat="server" ID="uiObjectMultiComplete" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetObjects" Width="580px"/>
				
			</p>
			<asp:CustomValidator Runat="server" Display="Dynamic" EnableClientScript="False" OnServerValidate="DbComboVal" ErrorMessage="<p>Please select an item from the drop-down above</p>" ID="Customvalidator4" NAME="Customvalidator1"/>
			<p>
				<small>
					To use this special drop-down, type the first few letters of the item in the box, 
					and a list of results should appear.
				</small>
			</p>
			<p>
				<asp:Button ID="Button2" Runat="server" OnClick="ChangeForum_Click" Text="Change forum now"/>
			</p>
			<script language=javascript>
		 
				function CheckEventRadio()
				{
					$get("<%=  ScopeEvent.ClientID  %>").checked=true;
				}
				function UpdateCombo()
				{
					 
					var behaviour = <%=uiObjectMultiComplete.ClientID %>Behaviour;
					if ($get("<%=  ScopeEventFuture.ClientID  %>").checked)
					{
						behaviour.parameters.set("future", "true");
					}else{
						behaviour.parameters.set("future", null);
					}
					
					if ($get("<%=  ScopeEventAttend.ClientID  %>").checked)
					{
						behaviour.parameters.set("attend", "true");
					}else{
						behaviour.parameters.set("attend", null);
					}
					var type;
					if ($get("<%=  ScopeEvent.ClientID  %>").checked) { type = "1"; }
					else if ($get("<%=  ScopeVenue.ClientID  %>").checked) { type = "2"; }
					else if ($get("<%=  ScopePlace.ClientID  %>").checked) { type = "3"; }
					else if ($get("<%=  ScopeCountry.ClientID  %>").checked) { type = "4"; }
					else if ($get("<%=  ScopeGeneral.ClientID  %>").checked) { type = "5"; }
					else { type = "0"; }
					behaviour.parameters.set("Type", type);
				}

			</script>
		</div>
	</asp:Panel>
	
	<asp:Panel runat="server" ID="AdminPanel">
		<dsi:h1 runat="server">Admin options</dsi:h1>
		<div class="ContentBorder">
			<p>
				<asp:Button runat="server" OnClick="ChangeToPrimary" Text="Make this thread the primary thread of it's parent" />
			</p>
		</div>
	</asp:Panel>
	
</asp:Panel>
