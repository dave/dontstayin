<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DateProfile.ascx.cs" Inherits="Spotted.Controls.DateProfile" %>

<dsi:h1 runat="server" ID="NicknameH1"></dsi:h1>
<div class="ContentBorder">
	<p>
		<table cellpadding="0" cellspacing="0" border="0">
			<tr>
				<td valign="top">
					<img src="" border="0" width="100" height="100" style="margin-top:3px;" class="BorderBlack All" runat="server" id="Pic">
				</td>
				<td valign="top" style="padding-left:8px;" width="100%">
					<p style="margin-top:2px;">
						Hi, I'm <asp:Label Runat="server" ID="NicknameLabel"/>. 
						I've posted <asp:Label Runat="server" ID="UsrCommentsLabel"/>, and <asp:Label Runat="server" ID="UsrSpottedLabel"/>
					</p>
					<p>
						I listen to <asp:Label Runat="server" ID="UsrMusicTypesLabel"></asp:Label>. 
						I go out in <asp:Label Runat="server" ID="UsrPlaceVisitLabel"></asp:Label>
					</p>
					<asp:Panel Runat="server" ID="PersonalStatementPanel">
						<p>
							<b>A bit about me :</b>
						</p>
						<asp:Label Runat="server" ID="PersonalStatementLabel"/>
					</asp:Panel>
				</td>
			</tr>
		</table>
	</p>
</div>

<dsi:h1 runat="server" ID="H12">Would you like to be introduced?</dsi:h1>
<div class="ContentBorder">
	<p>
		Remember, your selection is <b>kept secret</b>. The only time it will ever be revealed 
		is if you and <asp:label Runat="server" ID="NicknameLabel1"/> both click Yes to each 
		others profiles.
	</p>
	<p>
		<asp:RadioButton Runat="server" ID="RadioYes" Text="Yes" GroupName="Radios"></asp:RadioButton><br>
		<asp:RadioButton Runat="server" ID="RadioNo" Text="No" GroupName="Radios"></asp:RadioButton><br>
		<asp:RadioButton Runat="server" ID="RadioMaybe" Text="Maybe" GroupName="Radios" Checked="True"></asp:RadioButton><br>
		<asp:RadioButton Runat="server" ID="RadioWrongSex" Text="Wait there, this isn't a ?!!!" GroupName="Radios"></asp:RadioButton>
	</p>
	<p>
		<asp:Button Runat="server" OnClick="BackClick" Text="&lt;- Back" Enabled="False" ID="BackButton" CausesValidation="False"/>
		<asp:Button ID="Button1" Runat="server" OnClick="NextClick" Text="Next -&gt;" /> 
		<asp:CustomValidator ID="CustomValidator1" Runat="server" EnableClientScript="False" Display="Dynamic" OnServerValidate="RadioVal" ErrorMessage="Please select one of the options above!"/>
	</p>
	<p id="WrongSexP" style="display:none;">
		Please only click this if the photo is DEFINATELY the wrong sex - we have to 
		follow up manually each time this option is chosen.
	</p>
</div>

<asp:Panel Runat="server" ID="PhotosPanel">
	<dsi:h1 runat="server" TopLink="true" ID="H11" NAME="H11">Selected photo</dsi:h1>
	<div class="ContentBorder" style="padding:0px;">
		<p align="center">
			<img src="" id="WebImg" class="BorderBlack All" runat="server">
		</p>
	</div>

	<dsi:h1 runat="server" TopLink="true" ID="H13" NAME="H11">More photos</dsi:h1>
	<div class="ContentBorder" style="padding:0px;">
		<iframe src="" width="100%" height="118" frameborder="0" runat="server" id="PhotosIFrame"></iframe>
	</div>
</asp:Panel>
