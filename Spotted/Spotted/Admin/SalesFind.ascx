<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesFind.ascx.cs" Inherits="Spotted.Admin.SalesFind" %>
<dsi:h1 runat="server" id="FindByUser" >Find by user</dsi:h1>
<asp:Panel runat="server" ID="uiFindByUserPanel" DefaultButton="uiLookupUserButton" class="ContentBorder">
	<p>
		<js:HtmlAutoComplete ID="uiUserAutoComplete" runat="server" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetUsersWithK"  Width="300px" Watermark="Type nickname or K"/>
		<asp:Button runat="server" ID="uiLookupUserButton" Text="Lookup user"/>
		<asp:Label runat="server" ID="uiUserIsNotPromoterPanel" Text="User is not a promoter" ForeColor="Red"></asp:Label>
	</p>
	
</asp:Panel>



<dsi:h1 runat="server" id="FindByBrand" >Find by brand</dsi:h1>
<asp:Panel runat="server" ID="uiFindByBrandPanel" DefaultButton="uiGoToPromoterPageByBrandButton" class="ContentBorder">
	<p>
		<js:HtmlAutoComplete ID="uiBrandsAutoComplete" runat="server" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetBrands" Width="300px" Watermark="Type brand name"/>
		<asp:Button runat="server" Text="Go to promoter page" ID="uiGoToPromoterPageByBrandButton" />
	</p>
	
</asp:Panel>

<dsi:h1 runat="server" id="FindByPromoter" >Find by promoter</dsi:h1>
<asp:Panel runat="server" ID="uiFindByPromoterPanel" DefaultButton="uiGoToPromoterPageByPromoterButton" class="ContentBorder">
	<p>
		<js:HtmlAutoComplete ID="uiPromoterAutoComplete" runat="server" WebServiceUrl="/WebServices/AutoComplete.asmx" WebServiceMethod="GetPromotersWithK"  Width="300px" Watermark="Type promoter name"/>
		<asp:Button runat="server" Text="Go to promoter page" ID="uiGoToPromoterPageByPromoterButton" />
		<asp:Label runat="server" ID="uiBrandPromoterIsNullPanel" Text="Brand does not have a promoter" ForeColor="Red"></asp:Label>
	</p>
	
</asp:Panel>


