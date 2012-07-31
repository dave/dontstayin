<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegisterNewDomain.ascx.cs" Inherits="Spotted.Admin.RegisterNewDomain" %>
<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo" %>
<%@ Register TagPrefix="DsiControls" TagName="OptionsList" Src="/Controls/OptionsList.ascx" %>

<p><b>Select promoter:</b></p>
<p><js:HtmlAutoComplete Width="168px" ID="uiPromotersAutoComplete" runat="server"  WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetPromotersWithK"/></p>
<button runat="server" onserverclick="PromoterSelected">Go</button>

<p>&nbsp;</p>

<asp:Panel runat="server" id="uiNewDomainDetailsPanel">
	<p><b>Domains currently registered:</b></p>
	<asp:Literal runat="server" ID="uiDomainsRegistered"></asp:Literal>

	<p><b>New Domain</b></p>
	<table>
		<tr>
			<td>Domain name:</td>
			<td>http://www.<asp:TextBox runat="server" id="uiDomainName" Columns="46" MaxLength="46"></asp:TextBox>.com</td>
		</tr>
		<tr>
			<td></td>
			<td><button runat="server" onserverclick="TestAvailability">Test availability</button><asp:Label runat="server" ID="uiDomainAvailability"></asp:Label></td>
		</tr>
		<tr>
			<td>Domain type:</td>
			<td>
				<DsiControls:OptionsList runat="server" ID="uiOptionsList" OptionPanelsContainerID="uiOptions" RepeatColumns="5"></DsiControls:OptionsList>
			</td>
		</tr>
	</table>
	
	<div id="uiOptions">
		<div runat="server" id="uiBrandDiv">
			<p><b>Brand details</b></p>
			<table>
				<tr>
					<td>Brand:</td>
					<td><asp:DropDownList runat="server" ID="uiBrandsDdl" DataTextField="Name" DataValueField="K"></asp:DropDownList></td>
				</tr>
				<tr>
					<td>Application:</td>
					<td><asp:TextBox runat="server" id="uiBrandRedirectApp" MaxLength="50"></asp:TextBox> ("tickets" for branded micro-site, "hottickets" for dsi hot-tickets page, leave blank for brand home page)</td>
				</tr>
			</table>
		</div>
		<div runat="server" id="uiVenueDiv">
			<p><b>Venue details</b></p>
			<table>
				<tr>
					<td>Venue:</td>
					<td><asp:DropDownList runat="server" ID="uiVenuesDdl" DataTextField="FriendlyName" DataValueField="K"></asp:DropDownList></td>
				</tr>
				<tr>
					<td>Application:</td>
					<td><asp:TextBox runat="server" id="uiVenueRedirectApp" MaxLength="50"></asp:TextBox> ("tickets" for branded micro-site, "hottickets" for dsi hot-tickets page, leave blank for venue home page)</td>
				</tr>
			</table>
		</div>
		<div runat="server" id="uiEventDiv">
			<p><b>Event details</b></p>
			<table>
				<tr>
					<td>Event K:</td>
					<td><asp:TextBox runat="server" id="uiEventK" MaxLength="50"></asp:TextBox></td>
				</tr>
			</table>
		</div>
		<div runat="server" id="uiGroupDiv">
			<p><b>Group details</b></p>
			<table>
				<tr>
					<td>Group K:</td>
					<td><asp:TextBox runat="server" id="uiGroupK" MaxLength="50"></asp:TextBox></td>
				</tr>
			</table>
		</div>
		<div runat="server" id="uiCustomUrlDiv">
			<p><b>Enter a custom url</b></p>
			<table>
				<tr>
					<td>Custom url:</td>
					<td>http://www.dontstayin.com/<asp:TextBox runat="server" id="uiCustomUrlText" MaxLength="199"></asp:TextBox></td>
				</tr>
			</table>
		</div>
	</div>
	
	<p><asp:Button runat="server" id="uiRegisterDomainButton" OnClick="RegisterDomain" Text="Register Domain" /></p>
</asp:Panel>

<asp:Panel runat="server" ID="uiTestRedirectPanel" Visible="false">
	<p><b>Domain successfully added:</b></p>
	<p><asp:Literal runat="server" ID="uiAddedDomain"></asp:Literal></p>
</asp:Panel>

<asp:Label runat="server" ID="uiErrorLbl" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>


<script>
function ConfirmDomain()
{
	return confirm('Are you sure you want to register http://www.' + document.getElementById('<%= uiDomainName.ClientID %>').value + '.com?');
}
</script>
