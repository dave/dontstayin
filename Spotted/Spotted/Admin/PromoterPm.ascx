<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PromoterPm.ascx.cs" Inherits="Spotted.Admin.PromoterPm" %>
<p>
	You can send a PM to ALL the promoters by entering it below:
</p>
<p>
	Message ID for excluding previously sent messages (should be unique):
	<asp:TextBox Runat="server" id="MessageId"></asp:TextBox>
</p>
<p>
	<asp:TextBox Runat="server" ID="CommentTextBox" TextMode="MultiLine" Columns="80" Rows="10"></asp:TextBox>
</p>
<p>
	<asp:Button Runat="server" OnClick="SendComment" Text="Send now" ID="SendCommentButton"></asp:Button>
</p>



<p>
	We can clear the UsrThreadView data on each of these threads so that everyone gets emailed all replies.
	<asp:Button ID="Button1" Runat="server" OnClick="ClearViewData" Text="Clear now"></asp:Button>
	Do this before sending a PM on this page.
</p>

<p>
	We can clear the UsrThreadView data on each of these threads for ADMINS so that the admins get replies. This doesn't 
	clear the UsrThreadView for non-admins - so they still see the red "NEW" markers.
	<asp:Button Runat="server" OnClick="ClearViewDataAdmin" Text="Clear now" ID="Button2" NAME="Button1"></asp:Button>
	Do this after sending a PM on this page.
</p>
<p>
	If daveB posts the message he will automatically watch all the threads! Click this button to ignore them all:
	<asp:Button Runat="server" OnClick="DaveIgnore" Text="DaveB Ignore" ID="Button3" NAME="Button1"></asp:Button>
	
</p>
