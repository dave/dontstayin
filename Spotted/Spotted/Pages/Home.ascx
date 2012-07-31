<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Home.ascx.cs" Inherits="Spotted.Pages.Home" %>
<%@ Register TagPrefix="Home" TagName="Content" Src="/Pages/HomeContent.ascx" %>
<%@ Register TagPrefix="Controls" TagName="Latest" Src="/Controls/Latest.ascx" %>
<%@ Register TagPrefix="Controls" TagName="ExploreBox" Src="/Controls/ExploreBox.ascx" %>
<%@ Register TagPrefix="Controls" TagName="OutBox" Src="/Controls/OutBox.ascx" %>
<%@ Register TagPrefix="DsiControls" TagName="NewUserWizardOptions" Src="/Controls/NewUserWizardOptions.ascx" %>

<div class="ContentBorder Padding HeaderSpace" 
	 onmouseover = "TopPhotoMouseOver()"
	 onmouseout  = "TopPhotoMouseOut()"
	>
	<div class="PhotoHolder" runat="server" id="TopPhotoHolder">
		<div class="PhotoOverlay Black Top Right" runat="server" id="PhotoLinksHolder" style="display:none;">
			<asp:PlaceHolder runat="server" id="PhotoLinksPh" />
		</div>
		<div class="PhotoOverlay Black Bottom Right" runat="server" id="PhotoSoptterHolder" style="display:none;">
			<div style="float:left;">
				Photo by <a href="/" runat="server" id="PhotoSpotterLink">DaveB-DSI</a>
			</div>
		</div>
		<div class="PhotoOverlay White Bottom Left" runat="server" id="TopPhotoArchiveHolder" style="display:none;">
			<a href="/pages/top/photos">Archive</a> / <a href="/groups/front-page-suggestions">suggest</a>
		</div>
		<a href="/uk/bournemouth/venue-2020/2009/may/09/photo-11821373" runat="server" id="PhotoAnchor">
			<img src="http://pix-cdn.dontstayin.com/8575f398-07c2-4621-b487-0f486d895dd9.jpg" width="600" height="250" border="0" class="Block" runat="server" id="PhotoImg">
		</a>
		<div class="PhotoOverlay White Bottom Left" runat="server" id="PhotoCaption">
			Deadmau5 @ 2020
		</div>
	</div><asp:PlaceHolder runat="server" ID="RoadblockPh" />
</div>
<script>
		function UrlDecode(psEncodeString) 
		{
			var lsRegExp = /\+/g;
			return unescape(String(psEncodeString).replace(lsRegExp," "));
		}
		function TopPhotoChangeImage(photoPath, photoUrl, caption, nickName)
		{
			document.getElementById("<% = PhotoImg.ClientID %>").src = photoPath;
			document.getElementById("<% = PhotoAnchor.ClientID %>").href = photoUrl;
			document.getElementById("<% = PhotoCaption.ClientID %>").innerHTML = UrlDecode(caption);
			document.getElementById("<% = PhotoSpotterLink.ClientID %>").innerHTML = nickName;
			document.getElementById("<% = PhotoSpotterLink.ClientID %>").href = "/members/" + nickName.toLowerCase();

		}
		var TopPhotoMouseOverNow = false;
		function TopPhotoMouseOver()
		{
			TopPhotoMouseOverNow = true;
			try
			{
				document.getElementById('<% = TopPhotoArchiveHolder.ClientID %>').style.display = ''; document.getElementById('<% = PhotoLinksHolder.ClientID %>').style.display = ''; document.getElementById('<% = PhotoSoptterHolder.ClientID %>').style.display = '';
			} catch (ex) { }
		}
		function TopPhotoMouseOut()
		{
			TopPhotoMouseOverNow = false;
			setTimeout("TopPhotoMouseOutAfterDelay()", 50);
		}
		function TopPhotoMouseOutAfterDelay()
		{
			if (!TopPhotoMouseOverNow)
			{
				try
				{
					TopPhotoMouseOverNow = false;
					document.getElementById('<% = TopPhotoArchiveHolder.ClientID %>').style.display = 'none';
					document.getElementById('<% = PhotoLinksHolder.ClientID %>').style.display = 'none';
					document.getElementById('<% = PhotoSoptterHolder.ClientID %>').style.display = 'none';
				} catch (ex) { }
			}
		}
	</script>

<asp:Panel Runat="server" ID="NewArticlesPanel">
	<asp:DataList Runat="server" ID="NewArticlesDataList" RepeatLayout="Flow" RepeatDirection="Horizontal"></asp:DataList>
</asp:Panel>

<DsiControls:NewUserWizardOptions runat="server"></DsiControls:NewUserWizardOptions>

<Controls:OutBox runat="server" ID="OutBox" />

<Controls:ExploreBox runat="server" ID="ExploreBox" />

<div id="ExploreBoxFindHolder" class="ContentBorder" style="display:none;">
	<p>
		If your spotter card has a code on it, enter it here:

		<asp:TextBox runat="server" ID="SpotterCode" style="height:18px;font-size:14px; width:100px;"></asp:TextBox>
		<button runat="server" onserverclick="SpotterCodeClick" id="SpotterCodeButton" causesvalidation="false">Go!</button>
		<asp:Label runat="server" ID="SpotterCodeError" class="ForegroundAttentionRed" Visible="false">Oops - not found!</asp:Label>
	</p>
</div>

<asp:PlaceHolder runat="server" ID="FrontPageBannerPh" />

<Home:Content Runat="server" ID="HomeContent" />

<Controls:Latest runat="server" ID="Latest" Items="7" />

