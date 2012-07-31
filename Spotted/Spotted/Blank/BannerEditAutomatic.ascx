<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BannerEditAutomatic.ascx.cs" Inherits="Spotted.Blank.BannerEditAutomatic" %>

<link rel="stylesheet" type="text/css" href="/support/style.css"/>
<style>.ClearAfter:after {	content: "<%= Vars.Opera ? "" : "." %>"; }</style>
<script>window.focus();</script>
<script>
function CloseWindow()
{
	window.close();
	opener.focus();
}
function Cancel()
{
	CloseWindow();
}
function Save(xml)
{
	CloseWindow();
	try
	{
		opener.SetAutomaticEventBanner(xml);
	}
	catch(ex){}
}
</script>
<table cellpadding=0 cellspacing=0 border=0><tr><td>

	<dsi:h1 runat="server" ID="H120">Customise an automatic banner</dsi:h1>
	<div class="ContentBorder">
		<p>
			<div style="border:1px solid #000000;">
				<asp:PlaceHolder Runat="server" ID="CustomiseBanner" EnableViewState="False"></asp:PlaceHolder>
			</div>
		</p>
		<p>
			First line:<br>
			<asp:TextBox Runat="server" ID="CustomiseFirstLine" Columns="80"></asp:TextBox>
		</p>
		<p>
			First line font size: <br>
			<asp:TextBox Runat="server" ID="CustomiseFirstLineSize" Columns="3"></asp:TextBox> pixels <small>(leave as 0 for automatic size)</small>
		</p>
		<p>
			Second line:<br>
			<asp:TextBox Runat="server" ID="CustomiseSecondLine" Columns="80"></asp:TextBox>
		</p>
		<p>
			Third line:<br>
			<asp:TextBox Runat="server" ID="CustomiseThirdLine" Columns="80"></asp:TextBox>
		</p>
		<p>
			<small>
				Replacements:<br>
				[Date] - the date of your event, in 'user-friendly' style - e.g. "next Friday (15 Dec)".<br>
				[CapsDate] - the same thing, but with a capital letter at the start. <br>
				[DateShort] and [CapsDateShort] - do the same thing, but without the date in brackets on the end.
			</small>
		</p>
		<p>
			<asp:Button ID="Button9" Runat="server" OnClick="Customise_Preview" Text="Preview"></asp:Button>
		</p>
		<p>
			<input type="button" OnClick="Cancel();" value="Cancel" />
			<asp:Button ID="SaveButton" OnClick="SaveButton_Click" runat="server" Text="Save"/>
		</p>
		<p>
			<asp:Button ID="RemoveButton" OnClick="RemoveButton_Click" runat="server" Text="Remove customisations"/>
		</p>
	</div>
</td></tr></table>

<%= SaveScript %>
