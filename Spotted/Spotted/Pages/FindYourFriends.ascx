<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FindYourFriends.ascx.cs" Inherits="Spotted.Pages.FindYourFriends" %>
<%@ Register TagPrefix="DsiControls" TagName="BuddyImporter" Src="/Controls/BuddyImporter.ascx" %>
<%@ Register TagPrefix="DsiControls" TagName="OptionsList" Src="/Controls/OptionsList.ascx" %>
<%@ Register TagPrefix="DsiControls" TagName="BuddyControl" Src="/Controls/BuddyControl.ascx" %>
<%@ Register TagPrefix="DsiControls" TagName="NewUserWizardOptions" Src="/Controls/NewUserWizardOptions.ascx" %>

<DsiControls:NewUserWizardOptions runat="server"></DsiControls:NewUserWizardOptions>

<dsi:h1 runat="server">Find your friends</dsi:h1>
<div class="ContentBorder Picker ClearAfter">

	<p align="center" style="font-weight:bold;margin-bottom:0px;">
		<img src="/gfx/icon-group.png" border="0" runat="server" width="26" height="21" id="TopIcon">
	</p>
	<p align="center" style="font-weight:bold;">
		You can find your friends by using one of the following options:
	</p>
	
	<div runat="server" id="SearchTypeHolder" class="Holder ClearAfter">
		<div class="Label">
			Search by:
		</div>
		<div class="Control">
			<asp:RadioButton runat="server" GroupName="SearchType" ID="SearchType1" Text="Search by Don't Stay In nickname" style="display:block;" onclick="Update();" />
			<asp:RadioButton runat="server" GroupName="SearchType" ID="SearchType2" Text="Search by spotter code" style="display:block;" onclick="Update();" />
			<asp:RadioButton runat="server" GroupName="SearchType" ID="SearchType3" Text="Search by real name" style="display:block;" onclick="Update();" />
			<asp:RadioButton runat="server" GroupName="SearchType" ID="SearchType4" Text="Use our friend inviter" style="display:block;" onclick="Update();" />
		</div>
	</div>

	
	<div id="uiUsernamePanel" style="display:none">
		<div class="Label">
			Enter nickname:
		</div>
		<div class="Control">
			<asp:Textbox runat="server" ID="uiUserName"></asp:Textbox><asp:CustomValidator ClientValidationFunction="UsernameVal" runat="server" ErrorMessage="*"></asp:CustomValidator>
			<button runat="server" id="uiUserNameButton" onserverclick="LookUpUserName">Go</button>
			<DsiControls:BuddyControl runat="server" id="uiUserNameBuddyControl" FindMethod="Nickname" ImageSize="Small" NameStyle="font-weight:bold"></DsiControls:BuddyControl>
		</div>
	</div>
	<div id="uiSpotterCodePanel" style="display:none">
		<div class="Label">
			Spotter code:
		</div>
		<div class="Control">
			<asp:Textbox ID="uiSpotterCode" runat="server"></asp:Textbox><asp:CustomValidator ClientValidationFunction="SpotterCodeVal" runat="server" ErrorMessage="*"></asp:CustomValidator>
			<button runat="server" id="uiSpotterCodeButton" onserverclick="LookUpSpotter">Go</button>
			<p><asp:Label runat="server" ID="uiInvalidSpottedCode" ForeColor="Red" Text="Invalid spotter code" Visible="false"></asp:Label></p>
			<DsiControls:BuddyControl runat="server" id="uiSpotterBuddyControl" FindMethod="SpotterCode" ImageSize="Small" NameStyle="font-weight:bold"></DsiControls:BuddyControl>
		</div>
	</div>
	<div id="uiRealNamePanel" style="display:none">
		<div class="ClearAfter">
			<div class="Label">
				First name:
			</div>
			<div class="Control">
				<asp:Textbox ID="uiFirstName" runat="server"></asp:Textbox><asp:CustomValidator ClientValidationFunction="FirstNameVal" runat="server" ErrorMessage="*"></asp:CustomValidator>
			</div>
		</div>
		<div class="ClearAfter">
			<div class="Label">
				Last name:
			</div>
			<div class="Control">
				<asp:Textbox ID="uiLastName" runat="server"></asp:Textbox><asp:CustomValidator ClientValidationFunction="LastNameVal" runat="server" ErrorMessage="*"></asp:CustomValidator>
				<button runat="server" id="uiRealNameButton" onserverclick="LookUpRealName">Go</button>
			</div>
		</div>
		<div class="Label">
			&nbsp;
		</div>
		<div class="Control">
			<DsiControls:BuddyControl runat="server" id="uiRealNameBuddyControl" FindMethod="RealName" ImageSize="Small" NameStyle="font-weight:bold"></DsiControls:BuddyControl>
		</div>
	</div>
	<div id="uiContactImporterPanel" style="display:none">
		<div class="Control">
			<DsiControls:BuddyImporter runat="server" ID="uiBuddyImporter"></DsiControls:BuddyImporter>
		</div>
	</div>
		
	<script>
		function Update()
		{
			document.getElementById("uiUsernamePanel").style.display = document.getElementById("<% = SearchType1.ClientID %>").checked ? "" : "none";
			document.getElementById("uiSpotterCodePanel").style.display = document.getElementById("<% = SearchType2.ClientID %>").checked ? "" : "none";
			document.getElementById("uiRealNamePanel").style.display = document.getElementById("<% = SearchType3.ClientID %>").checked ? "" : "none";
			document.getElementById("uiContactImporterPanel").style.display = document.getElementById("<% = SearchType4.ClientID %>").checked ? "" : "none";
		}
		Update();
	</script>
	
</div>

<script>
function UsernameVal(source, args)
{
	args.IsValid = (document.getElementById("uiUsernamePanel").style.display == "none" || document.getElementById("<%= uiUserName.ClientID %>").value.trim().length > 1);
}
function SpotterCodeVal(source, args)
{
	args.IsValid = (document.getElementById("uiSpotterCodePanel").style.display == "none" || document.getElementById("<%= uiSpotterCode.ClientID %>").value.trim() != "");
}
function FirstNameVal(source, args)
{
	args.IsValid = (document.getElementById("uiRealNamePanel").style.display == "none" || document.getElementById("<%= uiFirstName.ClientID %>").value.trim() != "");
}
function LastNameVal(source, args)
{
	args.IsValid = (document.getElementById("uiRealNamePanel").style.display == "none" || document.getElementById("<%= uiLastName.ClientID %>").value.trim() != "");
}
</script>

<style>
	.Picker .Label
	{
		width:80px;
		float:left;
		text-align:left;
		padding-right:5px;
	}
	.Picker .Control
	{
		float:left;
	}
	.Picker .Holder
	{
		margin-bottom:5px;
	}
	.Picker .BrandAutoComplete input
	{
		padding-left:3px;
	}
</style>

