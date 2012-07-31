<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CaptionCompetition.ascx.cs" Inherits="Spotted.Pages.CaptionCompetition" EnableViewState="true" %>
<%@ Register TagPrefix="DsiControl" tagname="CommentsDisplay" src="/Controls/CommentsDisplay.ascx"  %>

<dsi:h1 runat="server">Caption competition photos</dsi:h1>
<div class="ContentBorder">
	<p align="center">
		<asp:DataList Runat="server" EnableViewState="true"
			ID="uiPhotoDataList" RepeatColumns="4" RepeatLayout="Table" 
			RepeatDirection="Horizontal" Width="100%" ItemStyle-Width="25%" 
			ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top"/>
	</p>
</div>

<dsi:h1 runat="server">Photo</dsi:h1>
<div class="ContentBorder">
	<p style="text-align:center">
		<a runat="server" id="uiPhotoUrl">
			<img runat="server" id="uiPhoto" class="BorderBlack All" />
		</a>
	</p>
</div>

<dsi:h1 runat="server">Enter your caption here!</dsi:h1>
<div class="ContentBorder">
	<center>
		<p><b>Enter a funny caption and you could win a Sony Ericsson W890i Walkman ® Phone:</b></p>
		<table>
			<tr>
				<td><img src="/gfx/caption-start.gif" width="26" height="20" /></td>
				<td><asp:TextBox runat="server" ID="uiCaptionText" TextMode="MultiLine" Width="400" Height="50"></asp:TextBox></td>
				<td><img src="/gfx/caption-end.gif" width="26" height="20" /></td>
			</tr>
		</table>
		<asp:RequiredFieldValidator runat="server" ControlToValidate="uiCaptionText" ErrorMessage="Please enter a caption!"></asp:RequiredFieldValidator>
		<p><button ID="uiPost" runat="server" onserverclick="PostCaption">Submit</button></p>
		<p><a href="/groups/ibiza-rocks">Competition Details</a></p>
	</center>
</div>

<DsiControl:CommentsDisplay ID="uiCommentsDisplay" runat="server" ParentObjectType="Photo" />

<input runat="server" id="ThreadK" type="hidden" />
<input runat="server" id="PhotoK" type="hidden" />
<input runat="server" id="PageNumber" type="hidden" />
