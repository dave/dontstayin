<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MailingUpload.ascx.cs" Inherits="Spotted.Blank.MailingUpload" %>

<div style="border:1px solid #000000; margin:20px; padding:20px;">
<p>
	Click the button below to send details of the current mailing to Royal Mail. <b>DO NOT CLICK THE BUTTON MORE THAN ONCE!!!</b>
</p>
<asp:Button ID="Button1" runat="server" OnClick="Upload" Text="Upload" />
<p>
	After clicking the button, you will get the bag-tags. <b>Print them on LABELS</b>.
</p>
<h2>If it fails, you probably have to update the password...</h2>
<p>
Login: <a href="http://www.epro.royalmail.com" target="_blank">www.epro.royalmail.com</a><br />
Access code: 10702<br />
User name: dave@dontstayin.com<br />
Old password: <asp:Label runat="server" ID="OldPass"></asp:Label>
</p>
<p>
	When choosing a new password, enter it below before cut+pasting it into the ePro box. Make sure it's at least 8 chars, and has a number and a capital letter in.
</p>
<p>
	To change the e*Pro password, enter the new password here: <asp:TextBox runat="server" ID="Pass"></asp:TextBox> <button runat="server" onserverclick="UpdatePass">Update</button>
</p>
</div>

<asp:Button ID="Button2" runat="server" OnClick="GetReport" Text="Don't click this button" />
