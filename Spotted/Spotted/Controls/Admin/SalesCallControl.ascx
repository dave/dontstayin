<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesCallControl.ascx.cs" Inherits="Spotted.Controls.Admin.SalesCallControl" %>
<div class="ContentBorder ClearAfter" style="padding-top:5px;padding-bottom:5px;">
	<div style="width:300px;float:left;">
		<h2 style="margin-bottom:0px;">
			<asp:Label runat="server" ID="CallHeader" /><a href="<%= CurrentSalesCall.Promoter.Url() %>"><%= CurrentSalesCall.Promoter.Name %></a>
		</h2>
		<p style="margin-top:0px;">
			<asp:TextBox runat="server" ID="NoteTextBox" Columns="45" Text="add a note here..."></asp:TextBox>
			<button runat="server" id="SaveNoteButton" onserverclick="SaveNoteClick" causesvalidation="false">Save</button>
			<asp:Label runat="server" ID="NoteSavedLabel" ForeColor="blue" Visible="false" EnableViewState="false">Saved</asp:Label>
		</p>
	</div>
	<div style="width:170px;float:left;">
		<h2 style="margin-bottom:0px;">
			Next call
		</h2>
		<p style="margin-top:-7px;">
			<dsi:Cal runat="server" ID="NextCallCal" />
			<button runat="server" id="SaveNextCallButton" onserverclick="SaveNextCallClick" causesvalidation="false">Save</button>
			<asp:Label runat="server" ID="SaveNextCallDoneLabel" ForeColor="blue" Visible="false" EnableViewState="false">Saved</asp:Label>
			<asp:Label runat="server" ID="SaveNextCallErrorLabel" ForeColor="red" Visible="false" EnableViewState="false">ERROR!</asp:Label>
		</p>
	</div>
	<div style="width:70px;float:left;">
		<p>
			<button runat="server" id="EffectiveButton" onserverclick="EffectiveClick" style="width:60px; height:50px;" causesvalidation="false">Effective</button>
		</p>
	</div>
	<div style="width:60px;float:left;">
		<p>
			<button runat="server" id="HangUpButton" onserverclick="HangUpClick" style="width:60px; height:50px;" causesvalidation="false">Hang up</button>
		</p>
	</div>
</div>
