<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HtmlCleanerTest.ascx.cs" Inherits="Spotted.Admin.HtmlCleanerTest" %>
<a href="/admin/htmlcleanertest">Reload</a>
<br />&nbsp;<br />
<asp:TextBox runat="server" ID="Input" TextMode="MultiLine" Rows="10" Columns="100"></asp:TextBox>
<br />&nbsp;<br />
<asp:Button ID="Button1" runat="server" OnClick="TestClick" Text="Test" /><asp:Button runat="server" OnClick="BtnClick" Text="Encode" />
<br />&nbsp;<br />
<asp:TextBox runat="server" ID="Output" TextMode="MultiLine" Rows="10" Columns="100"></asp:TextBox>
<br />&nbsp;<br />
<asp:PlaceHolder runat="server" ID="PlaceHolder1"></asp:PlaceHolder>
<script runat="server">
	#region BtnClick
	protected void BtnClick(object sender, EventArgs eventArgs)
	{
		string outp = Cambro.Web.Helpers.CleanHtml(Input.Text);
		Output.Text = outp;
		PlaceHolder1.Controls.Clear();
		PlaceHolder1.Controls.Add(new LiteralControl(outp));
	}
	#endregion
	#region TestClick
	protected void TestClick(object sender, EventArgs eventArgs)
	{
		PlaceHolder1.Controls.Clear();
		PlaceHolder1.Controls.Add(new LiteralControl(Input.Text));
	}
	#endregion
</script>
