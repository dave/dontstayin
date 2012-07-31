<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MailingReport.ascx.cs" Inherits="Spotted.Blank.MailingReport" %>

<div style="border:1px solid #000000; margin:20px; padding:20px;">
<p>
	After uploading the results to Royal Mail, click the button below to generate a report. Print one copy per bag, plus a spare to give to Tim. Remember to print them Landscape.
</p>
<asp:Button ID="Button2" runat="server" OnClick="GetReport" Text="Get report" />
<p>
	After clicking the button, you will get a summary sheet. <b>Print this in LANDSCAPE mode</b>.
</p>
<p>
	To change the e*Pro password, enter the new password here: <asp:TextBox runat="server" ID="Pass"></asp:TextBox> <button runat="server" onserverclick="UpdatePass"></button>
</p>
</div>

