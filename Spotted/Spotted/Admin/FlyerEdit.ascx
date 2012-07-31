<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FlyerEdit.ascx.cs" Inherits="Spotted.Admin.FlyerEdit" %>
<%@ Register TagPrefix="DsiControls" TagName="TimeControl" Src="/Controls/TimeControl.ascx" %>

<script>
function openPopupFocusSize(url,width,height)
{
	var popUp = window.open(url, popUp, 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=1,width='+width+',height='+height);
	if (!popUp.opener)
		popUp.opener = self;
	popUp.focus();
}

function SetMusicTargettingString(musicTargettingString)
{
	document.getElementById('<%=uiMusicTargettingTextBox.ClientID%>').value = 
		(musicTargettingString.toString() == '1')
			? 'all music types'
			: musicTargettingString.split(',').length + ' music type' + (musicTargettingString.split(',').length == 1 ? '' : 's');
	document.getElementById('<%=uiMusicTargettingHidden.ClientID%>').value = musicTargettingString;
}

function SetPlaceTargettingString(placeTargettingString)
{
	placeTargettingString = unescape(placeTargettingString);
	document.getElementById('<%= uiPlaceTargettingHidden.ClientID %>').value = placeTargettingString;
	var numTowns = 0;
	if (placeTargettingString.toString().length > 0) numTowns = placeTargettingString.toString().split(',').length;
	var value = ((numTowns == 0) ? 'all' : numTowns) + ' town' + ((numTowns == 1) ? '' : 's') + ' selected';
	document.getElementById('<%= uiPlaceTargettingTextBox.ClientID %>').value = value;
}

</script>
<asp:Panel runat="server" ID="uiBasicInfo">
	<table>
		<tr>
			<td>
				K
			</td>
			<td>
				#<asp:Label runat="server" ID="uiFlyerKLabel"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				Promoter
			</td>
			<td>
				<js:HtmlAutoComplete Width="150px" ID="uiPromotersAutoComplete" runat="server" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetPromotersWithK"/>				
			</td>
		</tr>
		<tr>
			<td>
				Name of flyer run
			</td>
			<td>
				<asp:TextBox runat="server" ID="uiNameTextBox" Columns="100" MaxLength="100"></asp:TextBox>
				<asp:RequiredFieldValidator runat="server" ControlToValidate="uiNameTextBox" Text="Enter a name!"></asp:RequiredFieldValidator>
			</td>
		</tr>
		<tr>
			<td>
				Email Subject
			</td>
			<td>
				<asp:TextBox runat="server" ID="uiSubjectTextBox" Columns="150" MaxLength="150"></asp:TextBox>
				<asp:RequiredFieldValidator runat="server" ControlToValidate="uiSubjectTextBox" Text="Enter a subject!"></asp:RequiredFieldValidator>
			</td>
		</tr>
		<tr>
			<td>
				Email From
			</td>
			<td>
				<asp:TextBox runat="server" ID="uiFromDisplayNameTextBox" Columns="50" MaxLength="50"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>
				Background colour
			</td>
			<td>
				#<asp:TextBox runat="server" ID="uiBackgroundColor" Columns="10" MaxLength="6"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td>
				Link to URL
			</td>
			<td>
				<asp:TextBox runat="server" ID="uiUrlTextBox" Columns="100" MaxLength="250"></asp:TextBox>
				<asp:RequiredFieldValidator runat="server" ControlToValidate="uiUrlTextBox" Text="Enter a url!"></asp:RequiredFieldValidator>
			</td>
		</tr>
		<tr>
			<td>
				Send date / time
			</td>
			<td>
				<table><tr><td><dsi:Cal id="uiSendDate" runat="server" /></td><td><DsiControls:TimeControl ID="uiSendTime" runat="server" /></td></tr></table>
			</td>
		</tr>
		<tr>
			<td colspan="2">
				<p><b>Targetting:</b></p>
				<table>
					<tr>
						<td>
							Location targetting
						</td>
						<td>
							<asp:HiddenField runat="server" ID="uiPlaceTargettingHidden" /><asp:TextBox Enabled="false" ReadOnly="true" runat="server" ID="uiPlaceTargettingTextBox" Columns="25" /><button runat="server" id="uiPlaceTargettingButton">edit...</button> <button onclick="document.getElementById('<%= uiPlaceTargettingTextBox.ClientID %>').value = 'all towns'; document.getElementById('<%= uiPlaceTargettingHidden.ClientID %>').value = '';">all towns</button>
						</td>
					</tr>
					<tr>
						<td>
							Music targetting
						</td>
						<td>
							<asp:HiddenField runat="server" ID="uiMusicTargettingHidden" /><asp:TextBox Enabled="false" ReadOnly="true" runat="server" ID="uiMusicTargettingTextBox" Columns="25" /><button runat="server" id="uiMusicTargettingButton">edit...</button> <button onclick="document.getElementById('<%= uiMusicTargettingTextBox.ClientID %>').value = 'all music types'; document.getElementById('<%= uiMusicTargettingHidden.ClientID %>').value = '1';">all music</button>
						</td>
					</tr>
					<tr>
						<td>
							Target promoters only?
						</td>
						<td>
							<asp:CheckBox runat="server" ID="uiPromotersOnly" />
						</td>
					</tr>
				</table>
				<p><b>OR:</b></p>
				<table>
					<tr>
						<td valign="top">
							Target users by events:
						</td>
						<td>
							<js:HtmlAutoComplete Width="150px" ID="uiEvent" runat="server" Watermark="Enter event" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetEvents" /><button runat="server" onserverclick="AddEventToEvents" causesvalidation="false">Add</button><br />
							<js:HtmlAutoComplete Width="150px" ID="uiBrand" runat="server" Watermark="Enter brand" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetBrands" /><button runat="server" onserverclick="AddBrandEventsToEvents" causesvalidation="false">Add</button><br />
							
							<asp:HiddenField runat="server" ID="uiEventKs"></asp:HiddenField>
							<asp:Label runat="server" ID="uiEvents"></asp:Label>
						</td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td>
			</td>
			<td>
				<button runat="server" onserverclick="CalculateUsrBase" causesvalidation="false">Calculate user base</button>
				<asp:Label runat="server" ID="uiUsrBaseCountLabel"></asp:Label>
			</td>
		</tr>
		<tr>
			<td>
				Image
			</td>
			<td>
				<input type="file" runat="server" id="uiInputFile" size="40" style="height:18px;" />
				<asp:HyperLink runat="server" ID="uiPreviewFile" Visible="false" Text="Preview"></asp:HyperLink>
			</td>
		</tr>
		
		
		<tr>
			<td>
				Is HTML?
			</td>
			<td>
				<asp:CheckBox runat="server" ID="uiIsHtml" />
			</td>
		</tr>
		<tr>
			<td>
				HTML
			</td>
			<td>
				<asp:TextBox ID="uiHtml" runat="server" TextMode="MultiLine" Columns="100" Rows="20" />
			</td>
		</tr>
		<tr>
			<td>
				Alternative plain text
			</td>
			<td>
				<asp:TextBox ID="uiTextAlternative" runat="server" TextMode="MultiLine" Columns="100" Rows="20" />
			</td>
		</tr>
		
	</table>

	<p><asp:Button runat="server" ID="uiSaveButton" OnClick="Save" CausesValidation="true" Text="Save" />
	<asp:Label runat="server" ID="uiSavedLabel"></asp:Label></p>
	<p><asp:Button runat="server" ID="uiTestButton" OnClick="Test" Text="Send a test email to:"/><asp:TextBox runat="server" ID="uiTestEmail" Columns="40"></asp:TextBox><asp:Label runat="server" ID="uiTestEmailSuccess" ForeColor="Green"></asp:Label></p>

</asp:Panel>

