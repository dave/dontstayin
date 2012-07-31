<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BannersPending.ascx.cs" Inherits="Spotted.Pages.Promoters.BannersPending" %>

<%@ Register TagPrefix="Controls" TagName="Payment" Src="/Controls/Payment.ascx" %>
<script>
function EnsureBannersSelected(sender, args)
{
	checkBoxes = new Array(<%= BannerCheckBoxClientIDsAsList %>);
	for (i=0; i<checkBoxes.length; i++)
	{
		if (document.getElementById(checkBoxes[i]).checked)
		{
			args.IsValid = true;
			return;
		}
	}
	args.IsValid = false;
}
</script>

<dsi:PromoterIntro runat="server" ID="PromoterIntro" Header="Banners">
	<p>
		<a href="<%= CurrentPromoter.UrlApp("banners") %>"><img src="/gfx/icon-view.png" width="26" height="21" border="0" 
			align="absmiddle" style="margin-right:3px;">view banners</a>
	</p>
	<p>
		<a href="<%= CurrentPromoter.UrlApp("banneredit","mode","add") %>"><img src="/gfx/icon-add.png" width="26" height="21" border="0" 
			align="absmiddle" style="margin-right:3px;">add a banner</a>
	</p>
</dsi:PromoterIntro>

<dsi:h1 runat="server" ID="H1Title">Pending Banners</dsi:h1>
<div class="ContentBorder">

	<p>
		<asp:Label ID="NoPendingBannersLabel" runat="server" Text="There are currently no banners awaiting booking"></asp:Label>
	</p>
	
	<asp:Panel ID="BookBannersPanel" runat="server">
	<p>Select which banners you would like to book:</p>
	<asp:GridView ID="BannerGrid" runat="server" AutoGenerateColumns="False" PageSize="25" HeaderStyle-CssClass="dataGridHeader"
				CssClass="dataGrid" EnableViewState="true" AlternatingRowStyle-CssClass="dataGridAltItem"
				GridLines="None" BorderWidth="0" CellPadding="3" ShowHeader="true" onrowdatabound="BannerGrid_RowDataBound">
		<Columns>
			<asp:TemplateField>
				<ItemTemplate>
					<asp:CheckBox runat="server" ID="CheckBox" Text="" />
					<asp:Label ID="BannerK" runat="server" Text="<%#((Bobs.Banner)Container.DataItem).K %>" Visible="false"></asp:Label>
					<asp:Label ID="TotalRequiredImpressions" runat="server" Text="<%#((Bobs.Banner)Container.DataItem).TotalRequiredImpressions %>" Visible="false"></asp:Label>
					<asp:Label ID="Position" runat="server" Text="<%#((Bobs.Banner)Container.DataItem).Position.ToString() %>" Visible="false"></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Banner">
				<ItemTemplate>
					<%#Utilities.Link(((Bobs.Banner)Container.DataItem).OptionsUrl(), ((Bobs.Banner)Container.DataItem).Name)%>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Price" ItemStyle-HorizontalAlign="Right">
				<ItemTemplate>
					<%#((Bobs.Banner)Container.DataItem).PriceString %>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Dates">
				<ItemTemplate>
					<nobr><%#((Bobs.Banner)Container.DataItem).FirstDay.ToString("ddd dd MMM") %> - </nobr><nobr><%#((Bobs.Banner)Container.DataItem).LastDay.ToString("ddd dd MMM") %></nobr>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Position">
				<ItemTemplate>
					<%#Utilities.CamelCaseToString(((Bobs.Banner)Container.DataItem).Position.ToString()) %>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="<nobr>Link target</nobr>">
				<ItemTemplate>
					<%# ((Bobs.Banner)Container.DataItem).LinkTargetHtml%>
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>
	
	<p></p>

	<button ID="BookBannersButton" runat="server" onserverclick="BookBannersButton_Click" causesvalidation="true">Book banners</button>
	<asp:CustomValidator ID="EnsureBannersSelectedValidator" runat="server" 
	 ClientValidationFunction="EnsureBannersSelected" OnServerValidate="EnsureBannersSelected"
		ErrorMessage="Please select a banner!" ></asp:CustomValidator></asp:Panel>

	<asp:Panel ID="PaymentPanel" runat="server">
		<Controls:Payment Runat="server" id="Payment" OnPaymentDone="PaymentReceived"/>
		<button id="CancelButton" runat="server" onserverclick="CancelButton_Click">&lt; Back</button>
	</asp:Panel>
	
	<asp:Panel ID="ConfirmedPanel" runat="server">
		<p><h2>Thank you. The following banners are now successfully booked:</h2></p>
		<p>
		<asp:GridView ID="BookedBannersGridView" runat="server" AutoGenerateColumns="False" PageSize="25" HeaderStyle-CssClass="dataGridHeader"
				CssClass="dataGrid" EnableViewState="true" AlternatingRowStyle-CssClass="dataGridAltItem"
				GridLines="None" BorderWidth="0" CellPadding="3" ShowHeader="true">
		<Columns>
			<asp:TemplateField HeaderText="Banner">
				<ItemTemplate>
					<%#((Bobs.Banner)Container.DataItem).Link() %>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Price" ItemStyle-HorizontalAlign="Right">
				<ItemTemplate>
					<%#((Bobs.Banner)Container.DataItem).PriceString %>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Dates">
				<ItemTemplate>
							<nobr><%#((Bobs.Banner)Container.DataItem).FirstDay.ToString("ddd dd MMM") %> -</nobr> <nobr><%#((Bobs.Banner)Container.DataItem).LastDay.ToString("ddd dd MMM") %></nobr>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Position">
				<ItemTemplate>
					<%#Utilities.CamelCaseToString(((Bobs.Banner)Container.DataItem).Position.ToString()) %>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="<nobr>Link target</nobr>">
				<ItemTemplate>
					<%# ((Bobs.Banner)Container.DataItem).LinkTargetHtml%>
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	</asp:GridView>
		</p>
	</asp:Panel>
</div>
