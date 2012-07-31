<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Invoice.ascx.cs" Inherits="Spotted.Blank.Invoice" %>
<style>
	.{
		font-family: Verdana, sans-serif;
	}
	.mygrid{
		position:relative;
		left:-4px;
	}
</style>
<div style="width:100%;height:100%;padding:30px;" width="100%" height="100%">
<div style="width:100%;height:100%;padding:30px;border:1px solid #000000;" width="100%" height="100%">

	<div align=right style="font-size:40px;font-weight:bold;margin-bottom:40px;">
		<b>Invoice</b><img src="/gfx/dsi-60.gif" align=absmiddle style="margin-left:15px;">
	</div>
	
	<table width=100% cellpadding=0 cellspacing=0 style="margin-bottom:20px;">
		<tr>
			<td valign=top>
				<div style="font-size:14px;">
					<b>To</b><br>
					<%= CurrentInvoice.Promoter.ContactName %><br>
					<%= CurrentInvoice.Promoter.Name %><br>
					<%= CurrentInvoice.Promoter.AddressStreet %><br>
					<%= CurrentInvoice.Promoter.AddressArea %><br>
					<%= CurrentInvoice.Promoter.AddressTown %><br>
					<%= CurrentInvoice.Promoter.AddressCounty %><br>
					<%= CurrentInvoice.Promoter.AddressPostcode %>
				</div>
			</td>
			<td valign=top align=right>
				<div style="font-size:14px;">
					<b>From</b><br />
					Development Hell Limited<br />
					Greenhill House, Thorpe Road<br />
					Peterborough, PE3 6RU<br />
					0207 835 5599<br />
					accounts@dontstayin.com
				</div>
			</td>
		</tr>
	</table>

	<div style="font-size:14px;margin-bottom:13px;">
		<b>Reference</b><br>
		WEB<%= CurrentInvoice.K.ToString("0000") %>
	</div>

	<div style="font-size:14px;margin-bottom:13px;">
		<b>Invoice created</b><br>
		<%= CurrentInvoice.CreatedDateTime %>
	</div>

	
	<div style="font-size:14px;margin-bottom:13px;">
		<asp:DataGrid Width=100% Runat="server" ID="ItemsDataGrid" 
			CssClass=mygrid
			
			GridLines="Both" AutoGenerateColumns="False"
			HeaderStyle-Font-Bold="True"
			Font-Size=14px
			BorderWidth=1 CellPadding=3 ItemStyle-VerticalAlign="Top">
			<Columns>
				<asp:TemplateColumn HeaderText="Item" HeaderStyle-Width=100%>
					<ItemTemplate>
						<%#((Bobs.InvoiceItem)(Container.DataItem)).Description%>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Tax&nbsp;code">
					<ItemTemplate>
						<nobr>T<%#((Bobs.InvoiceItem)(Container.DataItem)).TaxCode%></nobr>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Price">
					<ItemTemplate>
						<nobr><%#((Bobs.InvoiceItem)(Container.DataItem)).Price.ToString("c")%></nobr>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Vat">
					<ItemTemplate>
						<nobr><%#((Bobs.InvoiceItem)(Container.DataItem)).Vat.ToString("c")%></nobr>
					</ItemTemplate>
				</asp:TemplateColumn>
				<asp:TemplateColumn HeaderText="Total">
					<ItemTemplate>
						<nobr><%#((Bobs.InvoiceItem)(Container.DataItem)).Total.ToString("c")%></nobr>
					</ItemTemplate>
				</asp:TemplateColumn>
			</Columns>
		</asp:DataGrid>
	</div>
	
	<div style="font-size:14px;margin-bottom:13px;">
		<b>Summary</b><br>
		Total: <%= CurrentInvoice.Price.ToString("c") %><br>
		Total vat: <%= CurrentInvoice.Vat.ToString("c") %><br>
		Grand total: <%= CurrentInvoice.Total.ToString("c") %>
	</div>

	<div style="font-size:14px;margin-bottom:13px;">
		<b>Payment received</b><br>
		<%= CurrentInvoice.PaidDateTime %>
	</div>
	
	<div style="font-size:14px;margin-bottom:13px;">
		<b>Development Hell Limited:</b> Registered in England and Wales number 04333049<br>
		<b>Greenhill House, Thorpe Road, Peterborough, PE3 6RU</b><br>
		<b>Vat number:</b> 796 5005 04
	</div>
</div></div>
