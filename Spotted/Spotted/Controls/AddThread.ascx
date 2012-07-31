<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddThread.ascx.cs" Inherits="Spotted.Controls.AddThread" %>
<%@ Register TagPrefix="uc1" TagName="MultiBuddyChooser" Src="~/Controls/MultiBuddyChooser.ascx" %>
<%@ Register TagPrefix="dsi" TagName="Html" Src="~/Controls/Html.ascx" %>
<a name="NewThread"></a>
<asp:Panel Runat="server" ID="AddThreadNotGroupMemberPanel">
	<p>
		This is a group forum. Only group members may post here. Becoming a 
		member is easy, just click the link below:
	</p>
	<p class="BigCenter">
		<a href="" runat="server" id="AddThreadNotGroupMemberGroupPageAnchor" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">Join this group</a>
	</p>
</asp:Panel>
<asp:Panel Runat="server" ID="AddThreadLoginPanel">
	<p>
		To post a comment you must first log on - use the links below to log on or create a free account.
	</p>
	<p align="center">
		<table width="400" cellspacing="15">
			<tr>
				<td align="center" valign="top" width="50%">
					<p style="font-size:18px;font-weight:bold;">
						<a href="" onclick="DsiPageShowLoginNew();return false;">Log in</a>
					</p>
					<p>If you've already signed-up</p>
				</td>
				<td align="center" valign="top" width="50%">
					<p style="font-size:18px;font-weight:bold;">
						<a href="" onclick="DsiPageShowLoginNew();return false;">Sign up FREE!</a>
					</p>
					<p>If you've not used the site before</p>
				</td>
			</tr>
		</table>
	</p>
</asp:Panel>
<asp:Panel Runat="server" ID="AddThreadEmailVerifyPanel">
	<p>
		To post a comment you must <a href="/pages/emailverify">first verify your email address</a>.
	</p>
</asp:Panel>
<asp:RequiredFieldValidator Runat="server" ControlToValidate="AddThreadSubjectTextBox" Display="Dynamic" ErrorMessage="<p>Please enter a subject for your message</p>" ID="Requiredfieldvalidator1" NAME="Requiredfieldvalidator1"/>
<asp:RequiredFieldValidator Runat="server" ControlToValidate="CommentHtml" Display="Dynamic" ErrorMessage="<p>Please enter a message</p>" ID="Requiredfieldvalidator2" />
<p>
	Subject:<br />
	<asp:TextBox Runat="server" ID="AddThreadSubjectTextBox" AutoComplete="Off" style="border-width:1px" class="BorderKeyline" TabIndex="100"/>
</p>
<p>
	Your comment:<br />
	<dsi:Html runat="server" id="CommentHtml" PreviewType="Comment" DisableContainer="true" SaveButtonText="Post comment" OnSave="AddThreadPost_Click" TabIndexBase="101" />
</p>
<p>
	<asp:CheckBox Runat="server" ID="AddThreadAdvancedCheckBox" Text="Show advanced options..." TabIndex="120"/>
</p>
<asp:Panel Runat="server" ID="AddThreadAdvancedPanel" style="padding-left:20px;display:none;">
	<p>
		<span runat="server" id="AddThreadPublicRadioButtonSpan"><asp:RadioButton Runat="server" ID="AddThreadPublicRadioButton" GroupName="AddThreadType" Text="Public <small>- post a normal public topic</small><br>" Checked="True" TabIndex="121"/></span>
		<span runat="server" id="AddThreadPrivateRadioButtonSpan"><asp:RadioButton Runat="server" ID="AddThreadPrivateRadioButton" GroupName="AddThreadType" Text="Private <small>- only people who have been invited can read this topic</small><br>" TabIndex="122"/></span>
		<span runat="server" id="AddThreadGroupRadioButtonSpan"><asp:RadioButton Runat="server" ID="AddThreadGroupRadioButton" GroupName="AddThreadType" Text="Send to a group: " TabIndex="123"/><asp:DropDownList Runat="server" ID="AddThreadGroupDropDown" TabIndex="124"/><br></span>
	</p>
	<p>
		<span runat="server" id="AddThreadGroupPrivateCheckBoxSpan"><asp:CheckBox Runat="server" ID="AddThreadGroupPrivateCheckBox" Text="Group private <small>- only group members can read this topic</small>" TabIndex="125"/><br></span>
		<span runat="server" id="AddThreadNewsCheckBoxSpan"><asp:CheckBox Runat="server" ID="AddThreadNewsCheckBox" Text="Group news <small>- invite all group members</small>" TabIndex="127"/><br></span>
		<span runat="server" id="AddThreadEventCheckBoxSpan"><asp:CheckBox Runat="server" ID="AddThreadEventCheckBox" Text="Post about an event: "/><asp:DropDownList Runat="server" ID="AddThreadEventDropDown" TabIndex="126" style="width:250px;"/><br></span>
		<span runat="server" id="AddThreadSealedCheckBoxSpan"><asp:CheckBox Runat="server" ID="AddThreadSealedCheckBox" Text="Sealed <small>- only I can invite people to this topic</small>" TabIndex="128"/><br></span>
		<span runat="server" id="AddThreadInviteCheckBoxSpan"><asp:CheckBox Runat="server" ID="AddThreadInviteCheckBox" Text="Invite my buddies to this topic..." TabIndex="129"/><br></span>
		<asp:Panel Runat="server" ID="AddThreadInvitePanel" style="padding-left:20px;margin-top:-2px;">
			<a name="AddThreadInviteDropAnchor"></a>
			<uc1:MultiBuddyChooser ID="uiMultiBuddyChooser" runat="server" TabIndexBase="130"/>
		</asp:Panel>
		<dsi:InlineScript ID="InlineScript1" runat="server">
			<script>
				InitAddThread('<%= this.ClientID %>_', <%= AddThreadPrivateRadioButton.Checked?"true":"false" %>, <%= AddThreadGroupPrivateCheckBox.Checked?"true":"false" %>, <%= AddThreadNewsCheckBox.Checked?"true":"false" %>, <%= AddThreadSealedCheckBox.Checked?"true":"false" %>, <%= AddThreadInviteCheckBox.Checked?"true":"false" %>, <%= IsGroupMode %>);
			</script>
		</dsi:InlineScript>
	</p>
</asp:Panel>
	
