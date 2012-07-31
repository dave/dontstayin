<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PhotoControl.ascx.cs" Inherits="Spotted.Controls.PhotoControl" %>
<%@ Register src="TaggingControl.ascx" tagname="TaggingControl" tagprefix="uc1" %>
<%@ Register src="BuddyChooser.ascx" tagname="BuddyChooser" tagprefix="uc2" %>
<%@ Register TagPrefix="Banners" TagName="Generator" Src="/Controls/Banners/Generator.ascx" %>
<asp:Panel ID="uiContent" runat="server" Visible='<%# Photo != null%>'>

	<div runat="server" id="uiPrevPhotoButtonDiv" style="float:left; width:20px; margin-top:10px;">
		<img id="uiPrevPhotoButton" runat="server" style="cursor: pointer; position:relative; z-index:1;" src="/gfx/icon-back-12.png" width="12" height="21" />
	</div>
	<div runat="server" id="uiNextPhotoButtonDiv" style="float:right; text-align:right; width:20px; margin-top:10px;">
		<img id="uiNextPhotoButton" runat="server" style="cursor: pointer; position:relative; z-index:1;" src="/gfx/icon-forward-12.png" width="12" height="21" />
	</div>
	
	<div runat="server" id="uiBannerHolder" align="center" style="margin-bottom:5px; margin-top:10px;">
		<div style="width:450px;" align="left">
			<small>DontStayIn is sponsored by:</small>
		</div>
		<div style="width:450px;" class="BorderBlack All" align="center">
			<div runat="server" ID="uiBannerPlaceHolder" style="height:50px;width=450px;" class="BackgroundBlack">
				<Banners:Generator runat="Server" Position="PhotoBanner" EnableViewState="False" ID="BannerPhoto" ShowClickHelper="True" ClickHelperTopOffset="0" ClickHelperLeftOffset="75" />
			</div>
		</div>
		<div style="width:450px;" align="right">
			<small>This banner has no association with the event shown in the photo below</small>
		</div>
	</div>
	
	<div runat="server" id="uiPhotoDiv" style="position:relative; text-align:center; margin-top:10px;">

		<!-- This is from the Development branch... -->
		<span runat="server" id="uiPhotoHolder">
			<asp:ImageButton 
				ID="uiPhoto" 
				runat="server" 
				style="cursor: pointer;" 
				CausesValidation="false" 
				ImageUrl="<%# WebPath %>" 
				Width="<%# Photo == null ? 0 : Photo.WebWidth %>" 
				Height="<%# Photo == null ? 0 : Photo.WebHeight %>" />
			<div style="position:relative; text-align:left;">
				<div runat="server" id="uiPhotoOverlay" style="position:absolute;" class="NoStyle"></div>
			</div>
		</span>


		<span runat="server" id="uiFlashHolder"></span>
	</div>
	
	<p runat="server" id="uiPhotoGalleryLinkHolder" style="text-align:center;">
		<a href="<%# PhotoUrl %>" runat="server" id="uiPhotoGalleryLink">Find this photo in its gallery</a>
	</p>
	
	<p style="text-align:center;">
		<span id="uiTakenByDetailsSpan" runat="server"><%# TakenByDetailsHtml %></span>
	</p>
	
	<p ID="uiUsrsInPhotoSpan" runat="server" style="text-align:center;"></p>
	<asp:Panel ID="uiBuddyChooserPanel" runat="server" DefaultButton="uiBuddySpottedButton">
		<p style="text-align:center;">
			<asp:LinkButton ID="uiUsrSpottedToggleButton" runat="server" CausesValidation="false">
				<%# UsrSpotted ? "I'm not in this photo" : "I've been spotted!" %>
			</asp:LinkButton>
			○ <asp:LinkButton ID="uiUseAsProfilePictureButton" Runat="server" CausesValidation="False" OnClick="UseAsProfilePictureClick">Use as my profile picture</asp:LinkButton>
		</p>
		<p style="text-align:center;" ID="uiBuddyChooserPanelInner" runat="server">
			<uc2:BuddyChooser ID="uiBuddyChooser" runat="server" /> 
			<asp:LinkButton runat="server" id="uiBuddySpottedButton" ValidationGroup="BuddyValidation">is in this photo!</asp:LinkButton>
			<asp:CustomValidator ID="uiBuddyValidator" runat="server" Display="Dynamic" 
				ErrorMessage="Invalid user - try again!" 
				onservervalidate="uiBuddyValidator_ServerValidate1" 
				ValidationGroup="BuddyValidation" />
		</p>
	</asp:Panel>
	
	<p style="text-align:center;" runat="server" id="uiCompetitionPanel">
		<a runat="server" ID="uiAddToCompetitionGroup" href="#"><img runat="server" id="uiAddToCompetitionGroupImg" class="Block" style="border:0px; margin-left:auto; margin-right:auto; margin-bottom:4px;" /></a>
		<small>Check out the <a runat="server" id="uiCompetitionGroupLink" /></a> group</small>
		<asp:HiddenField runat="server" ID="uiAddToCompetitionGroupImgAddButtonUrl" />
		<asp:HiddenField runat="server" ID="uiAddToCompetitionGroupImgRemoveButtonUrl" />
	</p>
	
	<!--<p style="text-align:center;">
		<small><a id="uiQuickBrowserUrl" runat="server" href="<%# QuickBrowserUrl %>">Find this photo in the quick browser</a></small>
	</p>-->
	<p style="text-align:center;">
		<meta name="title" content="Test1" />
		<meta name="description" content="Test2" />
		<link rel="image_src" href="<%# Photo != null ? Photo.WebPath : "" %>" />
		
		<script>
			function fbs_click() 
			{
				u = document.getElementById('<%# uiLinkToThis.ClientID %>').value.replace(/localhost/, "solo.dontstayin.com");
				t = document.title;
				window.open('http://www.facebook.com/sharer.php?u='+encodeURIComponent(u)+'&t='+encodeURIComponent(t),'sharer','toolbar=0,status=0,width=626,height=436');
				return false;
			}
		</script>
		<a href="/" onclick="return fbs_click()" target="_blank">Share on Facebook</a> ○
		<asp:LinkButton ID="uiIsFavouritePhotoToggleButton" runat="server" CausesValidation="false"> <%# IsFavouritePhoto ? "Remove from" : "Add to" %>&nbsp;my favourites</asp:LinkButton>
		<br />
		<a runat="server" id="uiSendLink" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">Email to a friend</a> ○
		<a runat="server" id="uiReportLink" onclick="try { return WhenLoggedInAnchor(this); } catch(ex) { return false; }">Report to a moderator</a> ○
		<a href="#" onclick="try { return WhenLoggedIn(function(){document.getElementById('DownloadDiv').style.display = (document.getElementById('DownloadDiv').style.display == '') ? 'none' : '';}); } catch(ex) {return false;}">Download this photo</a> ○
		<a href="#" onclick="try { return WhenLoggedIn(function(){document.getElementById('EmbedDiv').style.display = (document.getElementById('EmbedDiv').style.display == '') ? 'none' : '';}); } catch(ex) {return false;}">Link / embed code</a>
		<span ID="uiAddToGroupTopPhotosSpan" runat="server" 
			visible="<%# Usr.Current != null && Usr.Current.IsGroupModerator %>"><br /><a runat="server" id="uiAddToGroupLink">Add to group top photos</a> </span>
		<span ID="uiAddToFrontPageSpan" runat="server" 
			visible="<%# DisplayMakeFrontPageOptions %>"><br /><a 
			href="#" onclick="document.getElementById('<%# PhotoOfWeekDiv.ClientID %>').style.display='';return false;">Admin options</a> </span>
	</p>
	<div ID="DownloadDiv" style="text-align:center; padding:5px; margin:10px; display:none;" class="BorderKeyline All">
		<p class="BigCenter">
			<span id="uiDownloadPhotoLinkHtml" runat="server"><%# DownloadPhotoLinkHtml %></span>
		</p>
		<p>
			<small>
				This photo remains copyright of <span id="uiCopyrightUsrLinkSpan" runat="server"><%# UsrLink %></span> and any use of this photo must be 
				authorised by the copyright holder. This is in accordance with our
				<a href="/pages/legal">terms and conditions</a>.
			</small>
		</p>
	</div>
	<div ID="EmbedDiv" style="text-align:center; padding:5px; margin:10px; display:none;" class="BorderKeyline All">
		<p style="text-align:center;">
			Link to this <span id="uiPhotoVideoLabel1" runat="server"><%# PhotoVideoLabel %></span>:
			<input type="text" readonly="readonly" value="<%# Link %>" onClick="this.select();" id="uiLinkToThis" runat="server" class="TextBox" style="width:400px;" />
		</p>
		<p style="text-align:center;">
			Embed this <span id="uiPhotoVideoLabel2" runat="server"><%# PhotoVideoLabel%></span>:
			<input type="text" readonly="readonly" value="<%# EmbedHtml %>" onClick="this.select();" id="uiEmbedThis" runat="server" class="TextBox" style="width:400px;" />
		</p>
	</div>
	<div Runat="server" ID="PhotoOfWeekDiv" Visible="<%# DisplayMakeFrontPageOptions %>" style="margin:2px; padding:5px; display:none;" class="BorderKeyline All">
		<h2>Admin options</h2>
		<p>
			The admin options in the Quick Links bar only work when you're on a photo URL (not a gallery URL). <a href="" onclick="document.location = document.getElementById('<%# uiLinkToThis.ClientID %>').value;return false;">Go to the photo URL for this photo</a>.
		</p>
		<h2>Photo of the week?</h2>
		<p>
			You're an admin. You can choose to display this photo on the front page 
			of the site along with a comment. <a href="/" onclick="document.location = document.getElementById('<%# uiLinkToThis.ClientID %>').value + '/frontpagecrop';return false;">Add to / remove from front page</a>
		</p>
		
	</div>
	<div Runat="server" ID="Div1" Visible="<%# DisplayMakeFrontPageOptions %>" style="margin:2px; padding-right:15px; padding-left:15px; padding-bottom:5px; padding-top:0px;" class="BorderKeyline All">
		<p runat="server" id="uiPhotoUsage" style="font-size:20px; font-weight:bold;">
			<%# Photo != null && Usr.Current != null && Usr.Current.IsAdmin ? Photo.Usr.PhotoUsageAdminString : "" %>
		</p>
	</div>
</asp:Panel>

<input runat="server" id="uiDisplayMakeFrontPageOptions" type="hidden" />
<input runat="server" id="uiUsrIsLoggedIn" type="hidden" />
<input runat="server" id="uiDisableBanner" type="hidden" />
<input runat="server" id="uiFirstTimeLoading" type="hidden" />
<input runat="server" id="uiOverlayEnabled" type="hidden" />
<input runat="server" id="uiOverlayWidth" type="hidden" />
<input runat="server" id="uiOverlayHeight" type="hidden" />
