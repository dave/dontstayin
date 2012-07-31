<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WeightedPages.ascx.cs" Inherits="Spotted.Admin.WeightedPages" %>
<h2>
	Weighted page stats (log at init)
</h2>
<p>
	This includes all redirects and exceptions
</p>
<p>
	<asp:PlaceHolder runat="server" ID="StatsInit" />
</p>


<h2>
	Weighted page stats (log at render)
</h2>
<p>
	This excludes all redirects and exceptions, but includes pages served to crawlers
</p>
<p>
	<asp:PlaceHolder runat="server" ID="StatsRender" />
</p>


<h2>
	Weighted page stats (log at render, no crawlers)
</h2>
<p>
	This excludes all redirects, exceptions, and pages served to crawlers
</p>
<p>
	<asp:PlaceHolder runat="server" ID="StatsRenderNoCrawlers" />
</p>
