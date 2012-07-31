<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DomainStats.ascx.cs" Inherits="Spotted.Admin.DomainStats" %>
<%@ Register TagPrefix="DbCombo" Namespace="Cambro.Web.DbCombo" Assembly="Cambro.Web.DbCombo" %>

<p>Select promoter:</p>
<p><js:HtmlAutoComplete runat="server" ID="uiPromoterHtmlAutoComplete" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetPromotersWithK" Width="150px" /></p>
<p><button runat="server" onserverclick="PromoterSelected">Get Domains</button></p>

<asp:DropDownList runat="server" ID="uiDomainsList" DataTextField="DomainName" DataValueField="K" Visible="false"></asp:DropDownList>
<p><button runat="server" ID="uiSelectDomainButton" onserverclick="DomainSelected" visible="false">Go</button></p>

<asp:GridView runat="server" ID="uiGridView" Visible="false" AutoGenerateColumns="false">
	<Columns>
		<asp:BoundField HeaderText="Date" DataField="Date" DataFormatString="{0:ddd dd MMM yy}" HtmlEncode="false" />
		<asp:BoundField HeaderText="Hits" DataField="Hits" DataFormatString="{0:N0}" HtmlEncode="false" />
	</Columns>
</asp:GridView>
