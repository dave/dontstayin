<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BuddyChooserNew.ascx.cs" Inherits="Spotted.Controls.BuddyChooser" %>
<js:HtmlAutoComplete runat="server" ID="uiHtmlAutoComplete"	WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetBuddiesThenUsrs" Watermark="Type name or email address" Width="170px" MinimumPopupWidth="170" MaximumPopupWidth="300" />
