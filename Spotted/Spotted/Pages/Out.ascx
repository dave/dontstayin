<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Out.ascx.cs" Inherits="Spotted.Pages.Out" %>
<dsi:h1 runat="server">Who's going out?</dsi:h1>
<div class="ContentBorder">
	<p runat="server" id="TopPChoose" visible="false">
		Show me <a href="/pages/out/girls">girls</a> or <a href="/pages/out/boys">boys</a>...
	</p>
	<p runat="server" id="TopP">
		<div style="height:100px;">
			<div class="AlignOuter BackgroundColorVeryLight" 
				style="width:50px; height:100px; float:left; cursor:pointer;" 
				onmouseover="this.className='AlignOuter BackgroundColorLight';" 
				onmouseout="this.className='AlignOuter BackgroundColorVeryLight';"
				onclick="history.go(-1); return false;">
				<div class="AlignMiddle">
					<div class="AlignInner">
						<a href="/" class="NoStyle" onclick="history.go(-1); return false;">Back</a>
					</div>
				</div>
			</div>

			<div style="width:500px; position:absolute; z-index:100; margin-left:50px; margin-right:100px;">
				<div style="text-align:center;">
					<img runat="server" id="Thumb1" src="http://pix-eu.dontstayin.com/db34db5c-27dc-4344-a59f-b6a8a1112bf5.jpg" width="100" height="100" 
					/><img runat="server" id="Thumb2" src="http://pix-eu.dontstayin.com/10d49081-d072-4e98-bbf5-b6d8e05228fb.jpg" width="100" height="100" 
					/><img runat="server" id="Thumb3" src="http://pix-eu.dontstayin.com/ec688d65-2ebc-45b0-90b0-9f47df77421b.jpg" width="100" height="100" 
					/><img runat="server" id="Thumb4" src="http://pix-eu.dontstayin.com/ec688d65-2ebc-45b0-90b0-9f47df77421b.jpg" width="100" height="100" 
					/><img runat="server" id="Thumb5" src="http://pix-eu.dontstayin.com/ec688d65-2ebc-45b0-90b0-9f47df77421b.jpg" width="100" height="100" />
					<asp:PlaceHolder runat="server" ID="ParaTop" />
				</div>
			</div>

			<div class="AlignOuter BackgroundColorVeryLight" 
				style="width:50px; height:100px; float:right; cursor:pointer;" 
				onmouseover="this.className='AlignOuter BackgroundColorLight';" 
				onmouseout="this.className='AlignOuter BackgroundColorVeryLight';"
				onclick="document.location = document.getElementById('<% = Next.ClientID %>').href">
				<div class="AlignMiddle">
					<div class="AlignInner">
						<a runat="server" id="Next" href="/" class="NoStyle">Next</a>
					</div>
				</div>
			</div>
		
			<style>
				.AlignOuter { display:table; }
				.AlignMiddle { display:table-cell; vertical-align:middle; }
				.AlignInner { text-align:center; }
			</style>

			<script>
				function change(src, width, height, link) {
					document.getElementById('<% = Web.ClientID %>').src = src;
					document.getElementById('<% = Web.ClientID %>').width = width;
					document.getElementById('<% = Web.ClientID %>').height = height;
					document.getElementById('<% = Link.ClientID %>').href = link;
				}
			</script>

		</div>
	</p>
	<asp:PlaceHolder runat="server" ID="Para" />
	<p style="margin-top:15px;" runat="server" id="WebHolder">
		<center>
			<a runat="server" id="Link" href="/" class="NoStyle"><img runat="server" id="Web" src="http://pix-eu.dontstayin.com/d2f55225-9a39-47e4-8d81-5f57ca6805d8.jpg" width="600" height="400" /></a>
		</center>
	</p>
	<p runat="server" id="BottomPara" style="text-align:center;">
		Showing: [girls] [boys] [all], going out in [UK] [worldwide]
	</p>
</div>
