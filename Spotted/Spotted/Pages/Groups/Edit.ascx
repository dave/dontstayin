<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="Spotted.Pages.Groups.Edit" %>
<%@ Register TagPrefix="dsi" TagName="Html" Src="/Controls/Html.ascx" %>
<%@ Register TagPrefix="Spotted" TagName="Pic" Src="/Controls/Pic.ascx" %>
<dsi:GroupIntro runat="server" ID="GroupIntro" Header="Group options">
	<p>
		<img src="/gfx/icon-group.png" border="0" align="absmiddle" style="margin-right:3px;">Your 
		member options for the <a href="" runat="server" id="OptionsGroupAnchor"></a> group:
	</p>
	<p runat="server" id="OptionsMenuP"/>
	<p runat="server" id="EditOptionsP"/>
	<p runat="server" id="OptionsInviteP" EnableViewState="false"/>
</dsi:GroupIntro>
<asp:UpdatePanel runat="server">
	<ContentTemplate>
		<asp:Panel Runat="server" ID="PanelTheme">
			<dsi:h1 runat="server" ID="Header" NAME="H18">What is yor group about?</dsi:h1>
			<div class="ContentBorder">
				<asp:ValidationSummary Runat="server" ShowSummary="True" HeaderText="You've made some mistakes" CssClass="ValidationSummaryDiv" Font-Bold="True" DisplayMode="BulletList" ID="Validationsummary6" NAME="Validationsummary1"/>
				<p>
					Choose a theme for your group from the list below. We've included some 
					examples below each heading, but they're not intended to be a complete 
					list.
				</p>
				<p>
					Don't worry if your exact topic isn't listed - just choose the 
					category that best matches what you want to chat about. If you don't 
					think your group fits in anywhere, choose "Other".
				</p>
				<p style="margin-top:15px;">
					<style>
						.ThemesRadioButtonList td
						{
							vertical-align:top;
						}
					</style>
					<asp:RadioButtonList Runat="server" ID="ThemesRadioButtonList" CssClass="ThemesRadioButtonList" RepeatLayout="Table" RepeatColumns="2" RepeatDirection="Horizontal"></asp:RadioButtonList>
					<asp:CustomValidator Runat="server" OnServerValidate="PanelTheme_Val" Display="None" EnableClientScript="False"
						ErrorMessage="Please choose a theme. If you don't think your group fits in anywhere, choose 'Other'." ID="Customvalidator1" NAME="Customvalidator1"/>
				</p>
				<p>
					<asp:Button ID="Button1" Runat="server" OnClick="PanelTheme_Next" Text="Next -&gt;"></asp:Button>
				</p>
				<p runat="server" id="PanelThemeSaveP">
					<asp:Button Runat="server" OnClick="SaveAndExit_Click" Text="Save and Exit" ID="Button9"/>
				</p>
			</div>
		</asp:Panel>
		<asp:Panel Runat="server" ID="PanelLocation">
			<dsi:h1 runat="server" ID="H11" NAME="H18">Location based?</dsi:h1>
			<div class="ContentBorder">
				<asp:ValidationSummary Runat="server" ShowSummary="True" HeaderText="You've made some mistakes" CssClass="ValidationSummaryDiv" Font-Bold="True" DisplayMode="BulletList" ID="Validationsummary2" NAME="Validationsummary1"/>
				<p>
					Most groups are relevant worldwide, but some have a location that's important. 
					If your group is particularly relevant to a country or town, choose it below:
				</p>
				<asp:RadioButton Runat="server" GroupName="LocationType" ID="LocationTypeNone" Text="<span style=font-size:14px;font-weight:bold;>&nbsp;Worldwide</span><p style=margin-left:26px;margin-top:0px;margin-bottom:10px;>The group is relevant worldwide, or is not location based</p>" OnCheckedChanged="LocationType_Change" AutoPostBack="True"></asp:RadioButton>
				<asp:RadioButton Runat="server" GroupName="LocationType" ID="LocationTypeCountry" Text="<span style=font-size:14px;font-weight:bold;>&nbsp;Choose a country...</span><p style=margin-left:26px;margin-top:0px;margin-bottom:10px;>The group is relevant to a whole country</p>" OnCheckedChanged="LocationType_Change" AutoPostBack="True"></asp:RadioButton>
				<asp:RadioButton Runat="server" GroupName="LocationType" ID="LocationTypePlace" Text="<span style=font-size:14px;font-weight:bold;>&nbsp;Choose a country and a town...</span><p style=margin-left:26px;margin-top:0px;margin-bottom:10px;>The group is relevant to a specific town</p>" OnCheckedChanged="LocationType_Change" AutoPostBack="True"></asp:RadioButton>
				<p style="margin-left:26px;" runat="server" id="LocationCountryP">
					<asp:DropDownList Runat="server" ID="LocationCountryDropDown" OnSelectedIndexChanged="LocationCountryDropDown_Change" AutoPostBack="True"></asp:DropDownList>
				</p>
				<p style="margin-left:26px;" runat="server" id="LocationPlaceP">
					<asp:DropDownList Runat="server" ID="LocationPlaceDropDown"></asp:DropDownList>
				</p>
				<asp:CustomValidator Runat="server" OnServerValidate="PanelLocation_Val" Display="None" EnableClientScript="False"
					ErrorMessage="Please choose a location type. If you're not sure, choose 'Worldwide'." ID="Customvalidator2" NAME="Customvalidator1"/>
				<p>
					<button runat="server" onserverclick="PanelLocation_Back" runat="server" id="PanelLocationBackButton">&lt;- Back</button>
					<asp:Button ID="Button2" Runat="server" OnClick="PanelLocation_Next" Text="Next -&gt;"></asp:Button>
				</p>
				<p runat="server" id="PanelLocationSaveP">
					<asp:Button Runat="server" OnClick="SaveAndExit_Click" Text="Save and Exit" ID="Button10"/>
				</p>
			</div>
		</asp:Panel>
		<asp:Panel Runat="server" ID="PanelMusicType">
			<dsi:h1 runat="server" ID="H12" NAME="H18">About music?</dsi:h1>
			<div class="ContentBorder">
				<asp:ValidationSummary Runat="server" ShowSummary="True" HeaderText="You've made some mistakes" CssClass="ValidationSummaryDiv" Font-Bold="True" DisplayMode="BulletList" ID="Validationsummary1" NAME="Validationsummary1"/>
				<p>
					If your group is about a particular type of music, choose it below. If your 
					group is about more than one type of music, choose "All Music". If it's not 
					about music at all, choose "Not about music".
				</p>
				<p style="margin-top:15px;">
					<style>
						.MusicTypesRadioButtonList td
						{
							vertical-align:top;
						}
					</style>
					<asp:RadioButtonList Runat="server" ID="MusicTypesRadioButtonList" CssClass="MusicTypesRadioButtonList" RepeatLayout="Table" RepeatColumns="2" RepeatDirection="Horizontal"></asp:RadioButtonList>
					<asp:CustomValidator Runat="server" OnServerValidate="PanelMusicType_Val" Display="None" EnableClientScript="False"
						ErrorMessage="Please choose an option below. If your group is about more than one type of music, choose 'All Music'. If it's not about music at all, choose 'Not about music'." ID="Customvalidator3" NAME="Customvalidator1"/>
				</p>
				<p>
					<button runat="server" onserverclick="PanelMusicType_Back" ID="Button3">&lt;- Back</button>
					<asp:Button Runat="server" OnClick="PanelMusicType_Next" Text="Next -&gt;" ID="Button4" NAME="Button2"></asp:Button>
				</p>
				<p runat="server" id="PanelMusicTypeSaveP">
					<asp:Button Runat="server" OnClick="SaveAndExit_Click" Text="Save and Exit" ID="Button11"/>
				</p>
			</div>
		</asp:Panel>
		<asp:Panel Runat="server" ID="PanelDetails">
			<dsi:h1 runat="server" ID="H13" NAME="H18">Details</dsi:h1>
			<div class="ContentBorder">
				<asp:ValidationSummary Runat="server" ShowSummary="True" HeaderText="You've made some mistakes" CssClass="ValidationSummaryDiv" Font-Bold="True" DisplayMode="BulletList" ID="Validationsummary3" NAME="Validationsummary1"/>
				<asp:Panel Runat="server" ID="NamePanel">
					<h2>
						Name
					</h2>
					<p>
						Enter a short name for your group below (max 50 characters): 
					</p>
					<p>
						<asp:TextBox Runat="server" ID="NameTextBox" Columns="50" MaxLength="50" style="border:1px solid #A58319;" TabIndex="100" />
						<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Runat="server" Display="None" ErrorMessage="Please enter a group name" ControlToValidate="NameTextBox" />
						<asp:RegularExpressionValidator Runat="server" Display="None" ValidationExpression="^[a-zA-Z].*" 
							ErrorMessage="Please try another group name. It must start with a letter" ControlToValidate="NameTextBox" ID="Regularexpressionvalidator1" NAME="Regularexpressionvalidator1"/>
						<asp:CustomValidator Runat="server" Display="None" ControlToValidate="NameTextBox" OnServerValidate="Punctuation_Val" EnableClientScript="False"
							ErrorMessage="Please try another group name. At least 50% of the characters must be lower-case. Please do not decorate your name with upper-case or punctuation!" ID="Customvalidator4" NAME="Customvalidator4"/>
						<asp:CustomValidator Runat="server" Display="None" ControlToValidate="NameTextBox" OnServerValidate="NameLength_Val" EnableClientScript="False"
							ErrorMessage="Please enter between 5 and 50 characters for the group name" ID="Customvalidator5" NAME="Customvalidator4"/>
						<asp:CustomValidator Runat="server" Display="None" ControlToValidate="NameTextBox" OnServerValidate="StartEnd_Val" EnableClientScript="False"
							ErrorMessage="Please change your group name - it should not start with the word 'the' or end with the word 'group'." ID="Customvalidator99" NAME="Customvalidator99"/>
					</p>
				</asp:Panel>

				<h2>
					Short description
				</h2>
				<p>
					Enter a quick description of the group (max 200 characters):
				</p>
				<p>
					<asp:TextBox Runat="server" ID="DescriptionTextBox" Columns="80" MaxLength="200" style="border:1px solid #A58319;" TabIndex="101" />
					<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Runat="server" Display="None" ErrorMessage="Please enter a description" ControlToValidate="DescriptionTextBox"/>
					<asp:CustomValidator Runat="server" Display="None" OnServerValidate="DescriptionLength_Val" EnableClientScript="False"
						ErrorMessage="Please enter between 20 and 200 characters for the short description" ID="Customvalidator6" NAME="Customvalidator4"/>
					<asp:CustomValidator ID="CustomValidator7" Runat="server" Display="None" ControlToValidate="DescriptionTextBox" OnServerValidate="Punctuation_Val"
						ErrorMessage="Please re-phrase your description. At least 50% of the characters must be lower-case. Please do not decorate your description with upper-case or punctuation!"/>
				</p>
				
				<h2>
					Intro
				</h2>
				<p>
					You can include more details here, and also HTML tags. Don't worry 
					about making it perfect now - you can update it later.
				</p>
				<p>
					<dsi:Html runat="server" id="IntroHtml" DisableSaveButton="true" TabIndexBase="102" />
				</p>
				
				<h2>
					Posting rules
				</h2>
				<p>
					As the group owner, you can have rules for posting to your group chat 
					forum. Each new member joining your group must read and agree to the 
					rules. You can moderate your own forum and delete offending posts. If 
					you would like to have rules, please explain them below (max 500 chars):
				</p>
				<p>
					<asp:TextBox Runat="server" ID="RulesTextBox" Columns="80" Rows="5" TextMode="MultiLine" style="border:1px solid #A58319;" TabIndex="122"/>
				</p>
				
				<p>
					<button runat="server" onserverclick="PanelDetails_Back" ID="Button5" tabindex="123">&lt;- Back</button>
					<asp:Button Runat="server" OnClick="PanelDetails_Next" Text="Next -&gt;" ID="Button6" NAME="Button2" TabIndex="124"></asp:Button>
				</p>
				<p runat="server" id="PanelDetailsSaveP">
					<asp:Button ID="Button7" Runat="server" OnClick="SaveAndExit_Click" Text="Save and Exit" TabIndex="125"/>
				</p>
			</div>
		</asp:Panel>
		<asp:Panel Runat="server" ID="PanelMembership">
			<dsi:h1 runat="server" ID="H14" NAME="H18">Membership restrictions</dsi:h1>
			<div class="ContentBorder">
				<asp:ValidationSummary Runat="server" ShowSummary="True" HeaderText="You've made some mistakes" CssClass="ValidationSummaryDiv" Font-Bold="True" DisplayMode="BulletList" ID="Validationsummary4" NAME="Validationsummary1"/>
				<p>
					When new members join your group, you can have two levels of security. 
					With "Open", anyone can join instantly by agreeing to your posting rules. 
				</p>
				<p id="P1" runat="server" visible="false">
					"Members review applications" means that before becoming a member, someone 
					must apply for membership - their applications can be accpeted or rejected 
					by current members.
				</p>
				<p>
					With "Moderators review applications", you will assign several people
					as membership moderators. Only your membership moderator team has the 
					power to accept or reject new membership applications.
				</p>
				<asp:RadioButton Runat="server" GroupName="Membership" ID="MembershipPublic" Text="<span style=font-size:14px;font-weight:bold;>&nbsp;Open</span><p style=margin-left:26px;margin-top:0px;margin-bottom:10px;>Anyone may join if they read and agree to the rules</p>"></asp:RadioButton>
				<asp:RadioButton Visible="False" Runat="server" GroupName="Membership" ID="MembershipMember" Text="<span style=font-size:14px;font-weight:bold;>&nbsp;Members review applications</span><p style=margin-left:26px;margin-top:0px;margin-bottom:10px;>New membership applications may be accepted or rejected by any current members</p>"></asp:RadioButton>
				<asp:RadioButton Runat="server" GroupName="Membership" ID="MembershipModerator" Text="<span style=font-size:14px;font-weight:bold;>&nbsp;Moderators review applications</span><p style=margin-left:26px;margin-top:0px;margin-bottom:10px;>New membership applications may only be accepted or rejected by membership moderators</p>"></asp:RadioButton>
				<asp:CustomValidator Runat="server" OnServerValidate="PanelMembership_Val" Display="None" EnableClientScript="False"
					ErrorMessage="Please choose an option below. If you're not sure, choose 'Open'." ID="Customvalidator8" NAME="Customvalidator1"/>
				<p>
					<button runat="server" onserverclick="PanelMembership_Back" ID="Button8">&lt;- Back</button>
					<asp:Button Runat="server" OnClick="PanelMembership_Next" Text="Next -&gt;" ID="Button12" NAME="Button2"></asp:Button>
				</p>
				<p runat="server" id="PanelMembershipSaveP">
					<asp:Button Runat="server" OnClick="SaveAndExit_Click" Text="Save and Exit" ID="Button13"/>
				</p>
			</div>
		</asp:Panel>
		<asp:Panel Runat="server" ID="PanelPrivate">
			<dsi:h1 runat="server" ID="H15" NAME="H18">Privacy</dsi:h1>
			<div class="ContentBorder">
				<asp:ValidationSummary Runat="server" ShowSummary="True" HeaderText="You've made some mistakes" CssClass="ValidationSummaryDiv" Font-Bold="True" DisplayMode="BulletList" ID="Validationsummary5" NAME="Validationsummary1"/>
				<p>
					Various parts of your group may be public or private.
					
					If you have a private home-page, it is hidden from non-members unless 
					they have been invited to join. Groups with a private home-page must 
					have a private members list and chat forum.
				</p>
				<p>
					If the chat forum is private, non-members can't see any messages.
				</p>
				<p>
					If the membership list is private, non-members can't see the 
					list of members.
				</p>
				
				<h2>Group home-page</h2>
				<asp:RadioButton Runat="server" GroupName="GroupPage" ID="GroupPagePublic" Text="<span style=font-weight:bold;>&nbsp;Public</span><p style=margin-left:24px;margin-top:0px;margin-bottom:5px;>The group page may be seen by the general public</p>"></asp:RadioButton>
				<asp:RadioButton Runat="server" GroupName="GroupPage" ID="GroupPagePrivate" Text="<span style=font-weight:bold;>&nbsp;Private</span><p style=margin-left:24px;margin-top:0px;margin-bottom:10px;>Only current members or people that have been invited to the group may see the group page</p>"></asp:RadioButton>
				
				<h2>Members list</h2>
				<span id="MembersListRadioSpan" runat="server"><asp:RadioButton Runat="server" GroupName="MembersList" ID="MembersListPublic" Text="<span style=font-weight:bold;>&nbsp;Public</span><p style=margin-left:24px;margin-top:0px;margin-bottom:5px;>The list of members can be seen by the general public</p>"></asp:RadioButton></span>
				<asp:RadioButton Runat="server" GroupName="MembersList" ID="MembersListPrivate" Text="<span style=font-weight:bold;>&nbsp;Private</span><p style=margin-left:24px;margin-top:0px;margin-bottom:10px;>Only members can see the list of members</p>"></asp:RadioButton>
				
				<h2>Chat forum</h2>
				<span id="ChatForumRadioSpan" runat="server"><asp:RadioButton Runat="server" GroupName="ChatForum" ID="ChatForumPublic" Text="<span style=font-weight:bold;>&nbsp;Public</span><p style=margin-left:24px;margin-top:0px;margin-bottom:5px;>The chat forum may be read by the general public (only members may post)</p>"></asp:RadioButton></span>
				<asp:RadioButton Runat="server" GroupName="ChatForum" ID="ChatForumPrivate" Text="<span style=font-weight:bold;>&nbsp;Private</span><p style=margin-left:24px;margin-top:0px;margin-bottom:10px;>Only members can see the chat forum</p>"></asp:RadioButton>
				
				<dsi:InlineScript runat="server">
					<script>
						function UpdateRadioButtons(){
							var GroupPagePublic = document.getElementById("<%= GroupPagePublic.ClientID %>");
							var GroupPagePrivate = document.getElementById("<%= GroupPagePrivate.ClientID %>");
							
							var MembersListRadioSpan = document.getElementById("<%= MembersListRadioSpan.ClientID %>");
							var MembersListPublic = document.getElementById("<%= MembersListPublic.ClientID %>");
							var MembersListPrivate = document.getElementById("<%= MembersListPrivate.ClientID %>");
							
							var ChatForumRadioSpan = document.getElementById("<%= ChatForumRadioSpan.ClientID %>");
							var ChatForumPublic = document.getElementById("<%= ChatForumPublic.ClientID %>");
							var ChatForumPrivate = document.getElementById("<%= ChatForumPrivate.ClientID %>");
							
							if (GroupPagePrivate.checked)
							{
								MembersListRadioSpan.disabled = true;
								MembersListRadioSpan.className = "Disabled";
								MembersListPublic.disabled = true;
								MembersListPublic.checked = false;
								
								ChatForumRadioSpan.disabled = true;
								ChatForumRadioSpan.className = "Disabled";
								ChatForumPublic.disabled = true;
								ChatForumPublic.checked = false;
							}
							else
							{
								MembersListRadioSpan.disabled = false; 
								MembersListRadioSpan.className = "";
								MembersListPublic.disabled = false;
								
								ChatForumRadioSpan.disabled  = false;
								ChatForumRadioSpan.className = "";
								ChatForumPublic.disabled  = false;
							}
						}
						setTimeout("UpdateRadioButtons();",25);
					</script>
				</dsi:InlineScript>
				
				<asp:CustomValidator Runat="server" OnServerValidate="PanelPrivate_Val" Display="None" EnableClientScript="False"
					ErrorMessage="Please choose from the options below. If you're not sure, choose 'Public' for all three. Groups with a private home-page must have a private members list and chat forum." ID="Customvalidator9" NAME="Customvalidator1"/>
					
				<p>
					<button runat="server" onserverclick="PanelPrivate_Back" ID="Button14">&lt;- Back</button>
					<asp:Button Runat="server" OnClick="PanelPrivate_Next" Text="Next -&gt;" ID="Button15" NAME="Button2"></asp:Button>
				</p>
				<p runat="server" id="PanelPrivateSaveP">
					<asp:Button Runat="server" OnClick="SaveAndExit_Click" Text="Save and Exit" ID="Button16"/>
				</p>
			</div>
		</asp:Panel>

	</ContentTemplate>
</asp:UpdatePanel>

<asp:Panel Runat="server" ID="PanelPic">
	<dsi:h1 runat="server" ID="H16" NAME="H18">Picture</dsi:h1>
	<div class="ContentBorder">
		<p>
			To upload a picture for this group, use the controls below:
		</p>
		<Spotted:Pic Runat="server" ID="GroupPic" OnActionSaved="PicSaved" OnActionNoPic="PicNoPic"/>
	</div>
</asp:Panel>
