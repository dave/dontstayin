<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FlashBanner.ascx.cs" Inherits="Spotted.Controls.Banners.FlashBanner" %>
<div id="Banner<%= Guid  %>_BannerDiv" style="text-align: center; width:<%= WidthString %>px; height:<%= HeightString %>px;"></div>
<dsi:InlineScript runat="server">
<script>
	var Banner<%=  Guid  %>_so = new SWFObject("<%= BannerUrl %>", "Banner<%=  Guid  %>_mymovie", <%= WidthString %>, <%= HeightString %>, "<%= FlashVersionString %>", "#ffffff");
	Banner<%=  Guid  %>_so.addParam("wmode", "transparent");
	Banner<%=  Guid  %>_so.addParam("AllowScriptAccess", "always");
	Banner<%=  Guid  %>_so.addVariable("targetTag", "<%= TargetTag %>");
	Banner<%=  Guid  %>_so.addVariable("linkTag", "<%= LinkTag %>");
	Banner<%=  Guid  %>_so.addParam("loop", "true");
	Banner<%=  Guid  %>_so.addParam("menu", "false");
	Banner<%=  Guid  %>_so.write("Banner<%=  Guid  %>_BannerDiv");
</script>
</dsi:InlineScript>
