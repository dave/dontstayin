<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TagTest.ascx.cs" Inherits="Spotted.Pages.TagTest" %>


<dsi:h1 runat="server">Output</dsi:h1>
<div style="min-height:100px;" runat="server" id="TagOut" class="ContentBorder NoStyle Padding">
	<p>(empty)</p>
</div>

<dsi:h1 runat="server">Tag test</dsi:h1>
<div class="ContentBorder">
	<p>
		<asp:TextBox runat="server" ID="TagIn" Rows="30" Columns="100" TextMode="MultiLine"></asp:TextBox>
	</p>
	<p>
		<asp:Button runat="server" OnClick="Go" Text="Test tag" /> <asp:Button runat="server" OnClick="Clear" Text="Clear" />
	</p>
	
</div>
