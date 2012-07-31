<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="ThreadControl.ascx.cs" Inherits="Spotted.Controls.ThreadControl" %>
<%@ Register TagPrefix="dsi" TagName="Html" Src="/Controls/Html.ascx" %>
<%@ Register TagPrefix="dsi" TagName="CommentsDisplay" Src="/Controls/CommentsDisplay.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultiBuddyChooser" Src="~/Controls/MultiBuddyChooser.ascx" %>

<dsi:CommentsDisplay runat="server" id="uiCommentsDisplay"></dsi:CommentsDisplay>

<asp:Panel runat="server" ID="PostCommentPanel">
	<a name="PostComment"></a>
	<dsi:h1 ID="H1" runat="server" TopLink="true">Post a comment</dsi:h1>
	<div class="ContentBorder">
		<script>
			function QuoteNow(usrK)
			{
				try
				{
					QuoteGeneric(document.getElementById('<%=  CommentHtml.TextBoxClientID  %>'), usrK);
				}
				catch(ex){}
			}
			function FocusNow()
			{
				try
				{
					FocusGeneric(document.getElementById('<%=  CommentHtml.TextBoxClientID  %>'));
				}
				catch(ex){}
			}
		</script>
		<p>
			<div runat="server" ID="ThreadWatchButtonHolder" style="height:21px;" />
			<dsi:InlineScript ID="InlineScript3" runat="server">
				<script>
					DbButtonFull(
						"/gfx/icon-eye-up.png",
						"/gfx/icon-eye-dn.png",
						"","",
						"Ignore comments about this <%= ParentObjectTypeString(false) %>",
						"Watch for comments about this <%= ParentObjectTypeString(false) %>",
						"",
						"cursor:pointer;margin-right:3px;",
						"absmiddle",
						26,21,
						"SingleThreadBobWatch",
						"<%= ThreadWatchButtonArgs %>",
						<%= ThreadWatchButtonState %>,
						"ThreadWatchButton",
						"",
						"",
						"",
						"<%= ThreadWatchButtonHolder.ClientID %>");
				</script>
			</dsi:InlineScript>
		</p>
		<p runat="server" id="ThreadFavouriteButtonP">
			<div runat="server" ID="ThreadFavouriteButtonHolder" style="height:21px;" />
			<dsi:InlineScript ID="InlineScript2" runat="server">
				<script>
					DbButtonFull(
						"/gfx/icon-star-26-up.png",
						"/gfx/icon-star-26-dn.png",
						"","",
						"Remove this topic from my favourites",
						"Add this topic to my favourites",
						"",
						"cursor:pointer;margin-right:3px;",
						"absmiddle",
						26,21,
						"FavouriteTopic",
						"<%= ThreadFavouriteButtonThreadK %>",
						<%= ThreadFavouriteButtonState %>,
						"ThreadFavouriteButton",
						"",
						"",
						"",
						"<%= ThreadFavouriteButtonHolder.ClientID %>");
				</script>
			</dsi:InlineScript>
		</p>
		<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Runat="server" ControlToValidate="CommentHtml" Display="Dynamic" ErrorMessage="<p>Please enter a comment</p>"/>
		<asp:Panel Runat="server" ID="CommentGroupMemberPanel">
			<p>
				This topic was posted in a group forum. Only group members may post here. Becoming a 
				member is easy, just click the link below:
			</p>
			<p class="BigCenter">
				<a href="" runat="server" id="CommentGroupMemberAnchor">join this group</a>
			</p>
		</asp:Panel>
		<asp:Panel Runat="server" ID="CommentLoginPanel">
			<p>
				To post a comment you must first log on - use the links below to log on 
				or create a free account.
			</p>
			<p align="center">
				<table width="600" cellspacing="15">
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
		<asp:Panel Runat="server" ID="CommentEmailVerifyPanel">
			<p>
				To post a comment you must <a href="/pages/emailverify">first verify your email address</a>.
			</p>
		</asp:Panel>
		<p>
			<dsi:Html runat="server" id="CommentHtml" PreviewType="Comment" DisableContainer="true" SaveButtonText="Post comment" OnSave="CommentPost" TabIndexBase="100" />
		</p>
		<p runat="server" id="AddThreadAdvancedCheckBoxP">
			<asp:CheckBox Runat="server" ID="AddThreadAdvancedCheckBox" Text="Show advanced options..." TabIndex="120" />
		</p>
		<asp:Panel Runat="server" ID="AddThreadAdvancedPanel" style="padding-left:20px;display:'none';">
			<p>
				<span runat="server" id="AddThreadPublicRadioButtonSpan"><asp:RadioButton Runat="server" ID="AddThreadPublicRadioButton" GroupName="AddThreadType" Text="Reply <small>- add a comment to this topic</small><br>" Checked="True" TabIndex="121"/></span>
				<span runat="server" id="AddThreadNewPublicRadioButtonSpan"><asp:RadioButton Runat="server" ID="AddThreadNewPublicRadioButton" GroupName="AddThreadType" Text="..." TabIndex="122"/></span>
				<span runat="server" id="AddThreadPrivateRadioButtonSpan"><asp:RadioButton Runat="server" ID="AddThreadPrivateRadioButton" GroupName="AddThreadType" Text="..." TabIndex="123"/></span>
				<span runat="server" id="AddThreadGroupRadioButtonSpan"><asp:RadioButton Runat="server" ID="AddThreadGroupRadioButton" GroupName="AddThreadType" Text="..." TabIndex="124"/><asp:DropDownList Runat="server" ID="AddThreadGroupDropDown" TabIndex="125"/><br></span>
			</p>
			<p>
				<span runat="server" id="AddThreadGroupPrivateCheckBoxSpan"><asp:CheckBox Runat="server" ID="AddThreadGroupPrivateCheckBox" Text="Group private <small>- only group members can read this topic</small>" TabIndex="126"/><br></span>
				<span runat="server" id="AddThreadNewsCheckBoxSpan"><asp:CheckBox Runat="server" ID="AddThreadNewsCheckBox" Text="Group news <small>- invite all group members</small>" TabIndex="127"/><br></span>
				<span runat="server" id="AddThreadSealedCheckBoxSpan"><asp:CheckBox Runat="server" ID="AddThreadSealedCheckBox" Text="Sealed <small>- only I can invite people to this topic</small>" TabIndex="128"/><br></span>
				<span runat="server" id="AddThreadInviteCheckBoxSpan"><asp:CheckBox Runat="server" ID="AddThreadInviteCheckBox" Text="Invite my buddies to this topic..." TabIndex="129"/><br></span>
				<asp:Panel Runat="server" ID="AddThreadInvitePanel" style="padding-left:20px;margin-top:-2px;">
					<a name="AddThreadInviteDropAnchor"></a>
					<uc1:MultiBuddyChooser runat="server" ID="uiMultiBuddyChooser" TabIndexBase="130"></uc1:MultiBuddyChooser>
				</asp:Panel>
			</p>
			<dsi:InlineScript ID="InlineScript1" runat="server">
				<script>
					InitThreadControl('<%= this.ClientID %>_', <%= AddThreadGroupPrivateCheckBox.Checked?"true":"false" %>, <%= AddThreadNewsCheckBox.Checked?"true":"false" %>, <%= AddThreadSealedCheckBox.Checked?"true":"false" %>, <%= AddThreadInviteCheckBox.Checked?"true":"false" %>);
				</script>
			</dsi:InlineScript>
		</asp:Panel>
	</div>
</asp:Panel>

<input runat="server" id="uiThreadK" type="hidden" />
<input runat="server" id="uiParentObjectK" type="hidden" />
<input runat="server" id="uiParentObjectType" type="hidden" />
