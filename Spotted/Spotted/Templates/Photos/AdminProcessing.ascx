<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdminProcessing.ascx.cs" Inherits="Spotted.Templates.Photos.AdminProcessing" %>
#<%#CurrentPhoto.K.ToString()%> - <%#CurrentPhoto.ProcessingSummaryHtml%> 
<asp:LinkButton Runat="server" 
	OnCommand="Command" 
	CommandName="Delete" 
	CommandArgument="<%#CurrentPhoto.K%>" 
	ID="DeleteLinkButton">Delete</asp:LinkButton>
<asp:LinkButton Runat="server" 
	OnCommand="Command" 
	CommandName="Retry" 
	CommandArgument="<%#CurrentPhoto.K%>" 
	ID="RetryLinkButton">Retry</asp:LinkButton>
