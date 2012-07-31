<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Small.ascx.cs" Inherits="Spotted.Templates.ThreadUsrs.Small" %>
<small>
	<img src="<%#CurrentThreadUsr.IsWatching?"/gfx/eye-up.png":"/gfx/eye-dn.png"%>" align="absmiddle" style="margin-right:0px;margin-top:0px;margin-bottom:-4px;" height="21" width="26">
	<asp:PlaceHolder Runat="server" ID="EmailPh">
		<%#HttpUtility.HtmlEncode(CurrentUsr.Email)%>
	</asp:PlaceHolder>
	<asp:PlaceHolder Runat="server" ID="NamePh">
		<%#BuddyStart%><a href="<%#CurrentUsr.Url()%>" <%#CurrentUsr.Rollover%>><%#CurrentUsr.NickNameSafe%></a><%#BuddyEnd%>
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
</small>
