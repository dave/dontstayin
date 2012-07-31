<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExceptionLogging.ascx.cs" Inherits="Spotted.Admin.ExceptionLogging" %>

<h2>
	Top 50 Exceptions logged in the last 24 hours
</h2>

<asp:GridView runat="server" ID="GridView" AutoGenerateColumns="false">
	<Columns>
		<asp:BoundField HeaderText="Message" DataField="Message" ItemStyle-Width="400"/>
		<asp:BoundField HeaderText="MasterPath" DataField="MasterPath" />
		<asp:BoundField HeaderText="PagePath" DataField="PagePath" />
		<asp:TemplateField HeaderText="Count">
			<ItemTemplate>
				<%# ((SpottedException) Container.DataItem).ExtraSelectElements["Count"] %>
			</ItemTemplate>
		</asp:TemplateField>
	</Columns>
</asp:GridView>

