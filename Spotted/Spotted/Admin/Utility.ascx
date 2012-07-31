<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Utility.ascx.cs" Inherits="Spotted.Admin.Utility" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Drawing" %>
<%@ Import Namespace="System.Drawing.Imaging" %>
<%@ Import Namespace="System.Drawing.Drawing2D" %>

<h1>Daves admin tools - don't touch</h1>
<div class="ContentBorder">
	<p><asp:Button Runat="server" onclick="SortIsPromoter" Text="SortIsPromoter" ID="Button21" NAME="Button1" OnClientClick="return confirm('Are you sure?')"></asp:Button></p>
	<p><asp:Button Runat="server" onclick="Test" Text="Test" ID="Button1" NAME="Button1" OnClientClick="return confirm('Are you sure?')"></asp:Button></p>
	<p><asp:Button Runat="server" onclick="SortArticleThreadK" Text="SortArticleThreadK" ID="Button2" NAME="Button1" OnClientClick="return confirm('Are you sure?')"></asp:Button></p>
	<p><asp:Button Runat="server" onclick="CopyPics" Text="CopyPics" ID="Button3" NAME="Button1" OnClientClick="return confirm('Are you sure?')"></asp:Button></p>
	<p><asp:Button ID="Button4" runat="server" NAME="Button1" OnClick="CampDsiEmails" Text="CampDsiEmails" OnClientClick="return confirm('Are you sure?')" /></p>
	<p><asp:Button ID="Button5" runat="server" NAME="Button1" OnClick="AddGarethDarren" Text="AddGarethDarren" OnClientClick="return confirm('Are you sure?')" /></p>
	<p>
		<asp:Button ID="Button7" runat="server" NAME="Button1" OnClick="SendSalesSummaryNow" OnClientClick="return confirm('Are you sure?')"
			Text="SendSalesSummaryNow" /></p>
	<p>
		<asp:Button ID="Button8" runat="server" NAME="Button1" OnClick="SendPromoterIntros" OnClientClick="return confirm('Are you sure?')"
			Text="SendPromoterIntros" /></p>
	<p>
		<asp:Button ID="Button9" runat="server" NAME="Button1" OnClick="SendSpotterInvites" OnClientClick="return confirm('Are you sure?')"
			Text="SendSpotterInvites" /></p>
			
	<p>
		<asp:Button ID="Button10" runat="server" NAME="Button1" OnClick="SendSpotterInvitesAll" OnClientClick="return confirm('Are you sure?')"
			Text="SendSpotterInvitesAll" /></p>
			
	<p>
		<asp:Button ID="Button11" runat="server" NAME="Button1" OnClick="SendGlobalEmail" OnClientClick="return confirm('Are you sure?')"
			Text="SendGlobalEmail" /></p>
	<p>
		<asp:Button ID="Button12" runat="server" NAME="Button1" OnClick="CopyPageTimeLog" OnClientClick="return confirm('Are you sure?')"
			Text="CopyPageTimeLog" /></p>		
			
			
	<p>
		<asp:Button ID="Button14" runat="server" NAME="Button1" OnClick="UpdateIpCountry" OnClientClick="return confirm('Are you sure?')"
			Text="UpdateIpCountry" /></p>	
		<p><asp:Button ID="Button15" runat="server" NAME="Button1" OnClick="AddFabeDamola" Text="AddFabeDamola" OnClientClick="return confirm('Are you sure?')" /></p>
		<p><asp:Button ID="Button16" runat="server" NAME="Button1" OnClick="SortPromoterNotes" Text="SortPromoterNotes" OnClientClick="return confirm('Are you sure?')" /></p>	
		
	<p>
		<asp:Button ID="Button17" runat="server" NAME="Button1" OnClick="UpdatePromoterRandom" OnClientClick="return confirm('Are you sure?')"
			Text="UpdatePromoterRandom" /></p>
	<p>
		<asp:Button ID="Button18" runat="server" NAME="Button1" OnClick="AddMailNotes" OnClientClick="return confirm('Are you sure?')"
			Text="AddMailNotes" /></p>
			
				<p>
		<asp:Button ID="Button19" runat="server" NAME="Button1" OnClick="FixPromoterQuestions" OnClientClick="return confirm('Are you sure?')"
			Text="FixPromoterQuestions" /></p>
			
				<p>
		<asp:Button ID="Button20" runat="server" NAME="UpdateOldDBInvoicesButton" OnClick="UpdateOldDBInvoices_Click" OnClientClick="return confirm('Are you sure?')"
			Text="Update Old DB Invoices"/></p>
			
	<p><asp:Button ID="Button23" runat="server" OnClick="FixAllPromoterQuestionsThreads" Text="Fix All Promoter Questions Threads" OnClientClick="return confirm('Are you sure?')" /></p>
	
	<p><asp:Button ID="Button24" runat="server" OnClick="EmailAfterEventFeedback" Text="Send Feedback Emails for Yesterdays Events" OnClientClick="return confirm('Are you sure?')" /></p>
	
	<p><asp:Button ID="Button25" runat="server" OnClick="EmailAllEndedTicketRuns" Text="Send Emails for Recently Ended Ticket Runs" OnClientClick="return confirm('Are you sure?')" /></p>
	
	<p><asp:Button ID="Button26" runat="server" OnClick="TestAsyncThreadStartForMailing" Text="Test Async Thread Start For Mailing" OnClientClick="return confirm('Are you sure?')"/></p>
	
	<p><asp:Button ID="Button27" runat="server" OnClick="FixAllCampaignCreditBalances" Text="Fix All Campaign Credit Balances" OnClientClick="return confirm('Are you sure?')"/></p>
	<p>Ticket Run K <asp:TextBox ID="TicketRunK" runat="server" Width="50"></asp:TextBox><asp:Button ID="Button22" runat="server" OnClick="AssignRandomTicketCodesToTicketRun" Text="Assign Random Ticket Codes To Ticket Run" OnClientClick="return confirm('Are you sure?')"/></p>
	
	<p><asp:Button ID="Button28" runat="server" OnClick="MailTicketMoneyReserveToAccounts" Text="Mail Ticket Money Reserve To Accounts@DSI.com" OnClientClick="return confirm('Are you sure?')"/></p>
	
	
</div>
<asp:PlaceHolder Runat="server" ID="OutPh"></asp:PlaceHolder>
