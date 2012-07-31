<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GroupJoin.ascx.cs" Inherits="Spotted.Controls.GroupJoin" %>

<asp:Panel Runat="server" ID="PanelJoin">
	<a ID="<%= CurrentGroup.K.ToString() %>"></a>
	<dsi:h1 runat="server" ID="JoinTitle"></dsi:h1>
	<div class="ContentBorder">
		<table cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td valign="top" style="padding-right:7px;" runat="server" id="JoinPicCell">
					<p><a href="" runat="server" id="JoinPicAnchor"><img src="" runat="server" id="JoinPicImg" class="BorderBlack All" width="100" height="100" border="0" /></a></p>
				</td>
				<td width="100%" valign="top">
					
					<h2>Group info</h2>
					<p>
						<asp:Label Runat="server" ID="JoinNameLabel" /> has 
						<asp:PlaceHolder Runat="server" ID="JoinMembersLinkPh" />. 
						<span runat="server" id="PrivacySpan"/>
						<asp:PlaceHolder Runat="server" ID="JoinModsPh"/>
					</p>

					<h2>What's it all about?</h2>
					<p runat="server" id="JoinDescP"/>
						
					<asp:Panel runat="server" ID="CanJoinPanel">
						<asp:Panel Runat="server" ID="JoinRulesPanel">
							<h2>
								Group rules
							</h2>
							<p runat="server" id="JoinRulesP"/>
							<p>
								<asp:CheckBox Runat="server" ID="JoinAgreeCheckBox" Text="I agree to the group rules"/>
							</p>
							<asp:CustomValidator Runat="server" OnServerValidate="JoinAgreeCheckBox_Val" EnableClientScript="false" Display="Dynamic"
								ErrorMessage="<p>You must agree to the group rules before becoming a member.</p>" ID="JoinAgreeCustomValidator"/>
						</asp:Panel>
						
						<asp:Panel runat="server" ID="SonyPanel">
							<h2>
								How many mega pixels does the K800i have?
							</h2>
							<p>
								<asp:RadioButton runat="server" GroupName="SonyMegaPixels" ID="SonyMegaPixels10" Text="1.0 million" /><br />
								<asp:RadioButton runat="server" GroupName="SonyMegaPixels" ID="SonyMegaPixels32" Text="3.2 million" />
							</p>
							<asp:CustomValidator ID="SonyCustomValidator1" runat="server" Display="dynamic" ErrorMessage="<p>Oops - try again!</p>" EnableClientScript="false" OnServerValidate="SonyMegaPixels_Val"></asp:CustomValidator>
							<h2>
								What makes the K800i so cool for clubbing?
							</h2>
							<p>
								<asp:RadioButton runat="server" GroupName="SonySpecialFeature" ID="SonySpecialFeatureFlash" Text="It has a built in flash!" /><br />
								<asp:RadioButton runat="server" GroupName="SonySpecialFeature" ID="SonySpecialFeatureBottle" Text="It has a bottle opener" />
							</p>
							<asp:CustomValidator ID="SonyCustomValidator2" runat="server" Display="dynamic" ErrorMessage="<p>Oops - try again!</p>" EnableClientScript="false" OnServerValidate="SonySpecialFeature_Val"></asp:CustomValidator>
						</asp:Panel>

						<h2>
							New topics
						</h2>
						<table cellpadding="0" cellspacing="0">
							<tr>
								<td>
									<img src="/gfx/icon-eye-dn.png" style="cursor:pointer;" border="0" align="left" height="21" width="26" runat="server" id="GroupJoinEyeImage"/>
								</td>
								<td style="padding-left:3px;">
									<asp:RadioButton runat="server" ID="JoinWatchRadio" ValidationGroup="JoinWatchRadio" GroupName="JoinWatchRadio" Text="Watch this group for new topics <small>(they'll all go into your inbox)</small>" /><br />
									<asp:RadioButton runat="server" ID="JoinIgnoreRadio" ValidationGroup="JoinWatchRadio" GroupName="JoinWatchRadio" Text="Don't watch this group <small>(you can find new topics on the group page)</small>" />
								</td>
							</tr>
						</table>
						<asp:CustomValidator Runat="server" OnServerValidate="JoinWatchRadio_Val" EnableClientScript="false" Display="Dynamic"
							ErrorMessage="<p>Do you want to watch the group for new topics?</p>" ID="JoinWatchCustomValidator"/>
						<p>
							<button runat="server" onserverclick="JoinCancelClick" CausesValidation="false" ID="CancelButton">Cancel</button>
							<asp:Button Runat="server" OnClick="JoinJoinClick" Text="Join this group" ID="JoinButton" OnClientClick="try { return WhenLoggedInButton(this); } catch(ex) { return false; }" />
						</p>
					</asp:Panel>
					<asp:Panel runat="server" ID="CanNotJoinPanel">
						<p runat="server" ID="CanNotJoinP" style="font-weight:bold;"/>
						<p runat="server" id="GroupHomePageP" style="font-weight:bold;" visible="false">Go to the <asp:HyperLink ID="GroupHomePageLink" runat="server" Text="group home page"></asp:HyperLink> for more information about this group.</p>
					</asp:Panel>
				</td>
			</tr>
		</table>
		
	</div>
</asp:Panel>
