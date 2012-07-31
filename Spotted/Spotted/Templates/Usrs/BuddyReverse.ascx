<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BuddyReverse.ascx.cs" Inherits="Spotted.Templates.Usrs.BuddyReverse" %>
<table cellspacing="5" cellpadding="0" width="100%">
	<tr>
		<td valign="top" align="right" style="padding-top:1px; padding-right:7px;" rowspan="2" class="BorderKeyline Right">
			<a href="" runat="server" id="PicAnchor"><img src="<%#CurrentUsr.AnyPicPath%>" width="50" height="50" class="BorderBlack All" align="top" border="0"></a></td>
		<td valign="top" style="padding-left:2px;padding-right:10px;" width="100%">
			<small>
				<asp:PlaceHolder Runat="server" ID="EmailPh">
					<%#HttpUtility.HtmlEncode(CurrentUsr.Email)%>	
				</asp:PlaceHolder>
				<asp:PlaceHolder Runat="server" ID="NamePh">
					<a href="<%#CurrentUsr.Url()%>"><%#CurrentUsr.NickNameSafe%></a> 
				</asp:PlaceHolder>
				<asp:PlaceHolder Runat="server" ID="NotCanInvitePh">
					<br>(you can't invite them to chat topics)
				</asp:PlaceHolder>
			</small>
		</td>
	</tr>
</table>
