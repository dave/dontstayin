<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MyPrivacy.ascx.cs" Inherits="Spotted.Pages.MyPrivacy" %>
<%@ Register TagPrefix="DsiControl" TagName="ExDirectory" Src="/Controls/ExDirectoryPrivacyOption.ascx" %>

<script language="JavaScript">
  function UnsubscribeConfirmation()
  {
	document.getElementById("<%= PrefsUpdateButton.ClientID  %>").onclick = document.getElementById("<%= UnsubscribeCheckBox.ClientID  %>").checked ? new Function('return confirm(\'Are you sure you want to unsubscribe from DontStayIn? You will not be able to use DontStayIn while you are unsubscribed.\')') : null;
  }
</script>
<asp:Panel runat="server" ID="PanelChange">
	<dsi:h1 runat="server">My privacy</dsi:h1>
	<div class="ContentBorder">
		<div style="margin-bottom:7px;margin-top:5px;">
			<asp:CheckBox id="SendSpottedEmails" Runat="server" 
				Text=" Weekly update email" Checked="True"></asp:CheckBox>
			<div style="margin-left:24px;">
				<small>
					Send me the weekly email, containing details of parties in my area playing my favourite music.
				</small>
			</div>
		</div>
		<div style="margin-bottom:7px;margin-top:5px;">
			<asp:CheckBox id="SendInvites" Runat="server" 
				Text=" Party invites" Checked="True"></asp:CheckBox>
			<div style="margin-left:24px;">
				<small>
					Send me party invites, e-flyers and guestlist offers.
				</small>
			</div>
		</div>
		<p>
			Whatever you choose, we promise never to give your details to anyone else.
		</p>
	</div>
	
	<dsi:h1 runat="server" ID="H17a" NAME="H17a">Facebook settings</dsi:h1>
	<div class="ContentBorder">
		<div style="margin-bottom:7px;margin-top:5px;">
			<asp:CheckBox id="FacebookStory" Runat="server" Text=" Update my Facebook when I..." Checked="True"></asp:CheckBox>
		</div>
		<div runat="server" id="FacebookStoryPanel" style="margin-left:24px;">
			<div style="margin-bottom:7px;margin-top:5px;"><asp:CheckBox id="FacebookStoryAttendEvent"    Runat="server" Text=" attend events" Checked="True"></asp:CheckBox></div>
			<div style="margin-bottom:7px;margin-top:5px;"><asp:CheckBox id="FacebookStorySpotted"        Runat="server" Text=" get spotted in photos" Checked="True"></asp:CheckBox></div>
			<div style="margin-bottom:7px;margin-top:5px;"><asp:CheckBox id="FacebookStoryUploadPhoto"    Runat="server" Text=" upload photos" Checked="True"></asp:CheckBox></div>
			<div style="margin-bottom:7px;margin-top:5px;"><asp:CheckBox id="FacebookStoryPhotoFeatured"  Runat="server" Text=" have a photo featured on the front page" Checked="True"></asp:CheckBox></div>
			<div style="margin-bottom:7px;margin-top:5px;"><asp:CheckBox id="FacebookStoryNewTopic"       Runat="server" Text=" post new topics" Checked="True"></asp:CheckBox></div>
			<div style="margin-bottom:7px;margin-top:5px;"><asp:CheckBox id="FacebookStoryLaugh"          Runat="server" Text=" laugh at something" Checked="True"></asp:CheckBox></div>
			<div style="margin-bottom:7px;margin-top:5px;"><asp:CheckBox id="FacebookStoryFavourite"      Runat="server" Text=" put photos on my favourites" Checked="True"></asp:CheckBox></div>
			<div style="margin-bottom:7px;margin-top:5px;"><asp:CheckBox id="FacebookStoryFavouriteTopic" Runat="server" Text=" put topics on my favourites" Checked="True"></asp:CheckBox></div>
			<div style="margin-bottom:7px;margin-top:5px;"><asp:CheckBox id="FacebookStoryPostNews"       Runat="server" Text=" post news" Checked="True"></asp:CheckBox></div>
			<div style="margin-bottom:7px;margin-top:5px;"><asp:CheckBox id="FacebookStoryEventReview"    Runat="server" Text=" post an event review" Checked="True"></asp:CheckBox></div>
			<div style="margin-bottom:7px;margin-top:5px;"><asp:CheckBox id="FacebookStoryPublishArticle" Runat="server" Text=" publish an article" Checked="True"></asp:CheckBox></div>
			<div style="margin-bottom:7px;margin-top:5px;"><asp:CheckBox id="FacebookStoryNewBuddy"       Runat="server" Text=" make a new buddy" Checked="True"></asp:CheckBox></div>
			<div style="margin-bottom:7px;margin-top:5px;"><asp:CheckBox id="FacebookStoryJoinGroup"      Runat="server" Text=" join a group" Checked="True"></asp:CheckBox></div>
			<div style="margin-bottom:7px;margin-top:5px;"><asp:CheckBox id="FacebookStoryBuyTicket"      Runat="server" Text=" buy tickets" Checked="True"></asp:CheckBox></div>
		</div>

		<div style="margin-bottom:7px;margin-top:5px;">
			<asp:CheckBox id="FacebookEventAdd" Runat="server" Text=" Add an event to Facebook when I add an event on Don't Stay In <small>(coming soon)</small>" Checked="True"></asp:CheckBox>
		</div>

		<div style="margin-bottom:7px;margin-top:5px;">
			<asp:CheckBox id="FacebookEventAttend" Runat="server" Text=" Add me on Facebook when I attend an event on Don't Stay In <small>(coming soon)</small>" Checked="True"></asp:CheckBox>
		</div>

	</div>
		
	<dsi:h1 runat="server" ID="H17" NAME="H17">Inbox emails</dsi:h1>
	<div class="ContentBorder">
		<div style="margin-bottom:7px;margin-top:5px;">
			<asp:CheckBox id="InboxEmailsCheckBox" Runat="server" 
				Text=" Send me inbox emails" Checked="True"></asp:CheckBox>
			<div style="margin-left:24px;">
				<small>
					When something new comes into your inbox, we can send you an email. If you get loads of stuff in your inbox, you may want to turn these alert emails off.
				</small>
			</div>
		</div>
	</div>
	
	<dsi:h1 runat="server" ID="H18" NAME="H18">Unsubscribe</dsi:h1>
	<div class="ContentBorder">
		<div style="margin-bottom:7px;margin-top:5px;">
			<asp:CheckBox id="UnsubscribeCheckBox" Runat="server" 
				Text=" Unsubscribe from DontStayIn" Checked="False" onselect="UnsubscribeConfirmation()" onclick="UnsubscribeConfirmation();"></asp:CheckBox>
			<div style="margin-left:24px;">
				<small>
					You can stop DontStayIn from sending you any further emails by using this page. You won't be able to use DontStayIn while you're unsubscribed. Also, your profile picture will be deleted and other users will not be able to view your profile page.
					<br />You may subscribe again to DontStayIn at a later date, if you choose so.
				</small>
			</div>
		</div>
	</div>
	
	<dsi:h1 runat="server" ID="H19" NAME="H19">Privacy policy</dsi:h1>
	<div class="ContentBorder">
		<div style="margin-bottom:7px;margin-top:5px;">
			<a href="/pages/legalinformationpolicy">Privacy policy</a>
		</div>
	</div>
	
	<dsi:h1 runat="server" ID="H12" NAME="H12">Extra security</dsi:h1>
	<div class="ContentBorder">
		<p>
			<asp:CheckBox Runat="server" ID="EnhancedSecurity" Text=" Give me extra security - require my password to log in"></asp:CheckBox>
		</p>
		<p>
			If this box is ticked, you'll have to enter your password after clicking a link in an email.
		</p>
	</div>

	<DsiControl:ExDirectory runat="server" ID="ExDirectory"></DsiControl:ExDirectory>
	
	<a name="SaveBox"></a>
	<dsi:h1 runat="server">Save changes</dsi:h1>
	<div class="ContentBorder">
		<p>When you have finished, click the button below to save your changes.</p>
		<p>
			<asp:Button id="PrefsUpdateButton" OnClick="PrefsUpdateClick" Runat="server" Text="Update"></asp:Button>&nbsp;
			<asp:Label id="SuccessLabel" Runat="server" ForeColor="#0000ff"></asp:Label>
		</p>
	</div>
</asp:Panel>
