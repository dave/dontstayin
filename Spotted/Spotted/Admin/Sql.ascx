<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Sql.ascx.cs" Inherits="Spotted.Admin.Sql" %>
<div class="ContentBorder">
	<p>
		Password: <asp:TextBox runat="server" TextMode="Password" ID="Password"></asp:TextBox>
		
	</p>
	<p>
		<asp:TextBox runat="server" ID="Query" TextMode="MultiLine" Rows="30" Columns="150" />
	</p>
	<p>
		<asp:Button runat="server" OnClick="Run" Text="Run" />
	</p>
</div>
