<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PhotoBrowser.ascx.cs" Inherits="Spotted.Controls.PhotoBrowser" %>
<%@ Import Namespace="SpottedLibrary.Controls.PhotoBrowserControl" %>
<%@ Register src="PaginationControl2.ascx" tagname="PaginationControl2" tagprefix="uc1" %>

<script>
	function BlowUpIcon(smallIcon, iconSize)
	{
		try
		{
			// table cell two steps up
			//cssclass = smallIcon.parentNode.parentNode.className;
			// actual icon is next in current cell
			//src = smallIcon.parentNode.childNodes[1].src
			//document.getElementById('<%= uiBlowUpIconSpan.ClientID %>').innerHTML = "<img class=\"" + cssclass + "\" src=\"" + src + "\" style=\"z-index:20;position:absolute;top:" + (smallIcon.offsetTop - 50 + (iconSize / 2)) + "px;left:" + (smallIcon.offsetLeft - 50 + (iconSize/2)) + "px;\" width=\"100\" height=\"100\" />";

			smallIcon.parentNode.parentNode.style.zIndex = "1000";
			smallIcon.parentNode.childNodes[2].style.display = "";
			
		}
		catch (e) 
		{
			// ignore- hover may have executed before page fully loaded
		}
	}
	function HideIcon(smallIcon)
	{
		//document.getElementById('<%= uiBlowUpIconSpan.ClientID %>').innerHTML = "";

		smallIcon.parentNode.parentNode.style.zIndex = "50";
		smallIcon.parentNode.childNodes[2].style.display = "none";
	}
</script>

<div runat="server" id="uiPhotoRepeaterContainer">
	<uc1:PaginationControl2 ID="uiPaginationControl" runat="server" UrlPrefix="Photo" />
	<div style="position:relative; left:-1px; width:600px;" class="BorderBlack All ClearAfter">
		<asp:Repeater ID="uiPhotoRepeater" runat="server" DataSource='<%# CurrentPageItems %>'>
			<ItemTemplate>
				<div 
					id="<%# this.TableCellsPrefix + Container.ItemIndex %>"
					style="xborder:1px solid #00ff00; float:left; position:relative; width:<%# Common.Properties.PhotoBrowser.IconSize %>px; height:<%# Common.Properties.PhotoBrowser.IconSize %>px; <%#((Photo)Container.DataItem).K == 0 ? "display:none;" : "" %>"
					class="<%# ((Photo)Container.DataItem).Highlight ? "PhotoBrowserCellHighlight" : "" %>"
				><a 
					href="<%# ((Photo) Container.DataItem).Link %>"
				><img 
					src="/gfx/1pix.gif" 
					border="0" 
					width="<%# Common.Properties.PhotoBrowser.IconSize %>" 
					height="<%# Common.Properties.PhotoBrowser.IconSize %>" 
					style="display:block; position:absolute; z-index:50; background-color:transparent; xborder:1px solid #ff0000;" 
					onmouseover="BlowUpIcon(this, <%# Common.Properties.PhotoBrowser.IconSize %>);eval(this.attributes.getNamedItem('rolloverMouseOverText').value);" 
					onmouseout="HideIcon(this);htm();" 
					rolloverMouseOverText="<%# ((Photo) Container.DataItem).RolloverMouseOverText %>" 
				/><img 
					src="<%# ((Photo)Container.DataItem).IconPath %>" 
					border="0" 
					style="display:block; position:absolute;" 
					class="<%#((Photo)Container.DataItem).Highlight ? "PhotoBrowserImageHighlight" : "PhotoBrowserImage" %>"
					width="<%# Common.Properties.PhotoBrowser.IconSize - (((Photo)Container.DataItem).Highlight ? 2 : 0) %>" 
					height="<%# Common.Properties.PhotoBrowser.IconSize - (((Photo)Container.DataItem).Highlight ? 2 : 0) %>" 
				/><img 
					src="<%# ((Photo)Container.DataItem).ThumbPath %>" 
					border="0" 
					style="display:block; position:absolute; display:none; top:-<%# (((Photo)Container.DataItem).ThumbHeight - 75) / 2 %>px; left:-<%# (((Photo)Container.DataItem).ThumbWidth - 75) / 2 %>px;" 
					width="<%# ((Photo)Container.DataItem).ThumbWidth %>" 
					height="<%# ((Photo)Container.DataItem).ThumbHeight %>" 
				/></a></div>
			</ItemTemplate>
		</asp:Repeater>
	</div>
</div>
<span runat="server" id="uiBlowUpIconSpan"></span>
<input runat="server" id="uiIconsPerPage" type="hidden" />
<input runat="server" id="uiIconsPerRow" type="hidden" />
<input runat="server" id="uiIconSize" type="hidden" />
<input runat="server" id="uiTableCellsPrefix" type="hidden" />
