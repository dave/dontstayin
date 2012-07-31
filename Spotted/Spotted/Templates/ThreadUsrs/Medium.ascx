<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Medium.ascx.cs" Inherits="Spotted.Templates.ThreadUsrs.Medium" %>
<table cellspacing="5" cellpadding="0" width="100%">
	<tr>
		<td valign="top" align="right" style="padding-top:1px; padding-right:7px;" rowspan="2">
			<a href="<%#CurrentUsr.Url()%>"><img src="<%#CurrentUsr.AnyPicPath%>" width="50" height="50" class="BorderBlack All" align="top" border="0" <%#CurrentUsr.RolloverNoPic%>></a></td>
		<td valign="top" style="padding-left:2px;padding-right:10px;" width="100%">
			<small>
				<asp:PlaceHolder Runat="server" ID="EmailPh">
					<%#HttpUtility.HtmlEncode(CurrentUsr.Email)%>	
				</asp:PlaceHolder>
				<asp:PlaceHolder Runat="server" ID="NamePh">
					<%#BuddyStart%><a href="<%#CurrentUsr.Url()%>"><%#CurrentUsr.NickNameSafe%></a><%#BuddyEnd%>
				</asp:PlaceHolder>
				<asp:PlaceHolder Runat="server" ID="SkeletonPh">
					???
				</asp:PlaceHolder>
				<asp:PlaceHolder Runat="server" ID="InvitedPh">
					was invited by <a href="" runat="server" id="InvitingUsrAnchor"/><%#FriendlyInviteDateTime%>.
				</asp:PlaceHolder>
				<asp:PlaceHolder Runat="server" ID="OwnerPh">
					started this topic <%#FriendlyInviteDateTime%>.
				</asp:PlaceHolder>
				<div><img src="<%#CurrentThreadUsr.IsWatching?"/gfx/icon-eye-up.png":"/gfx/icon-eye-dn.png"%>" height="21" width="26"></div>
			</small>
		</td>
	</tr>
</table>
