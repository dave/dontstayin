<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StyledSetup.ascx.cs" Inherits="Spotted.Pages.Promoters.StyledSetup" %>
<script src="/misc/ColourPicker.js" type="text/javascript"></script>
<script type="text/javascript">
	function SelectCss()
	{
		var cssDropDownList = document.getElementById('<%= CssDropDownList.ClientID %>');
		document.getElementById('UploadCssDiv').style.display = (cssDropDownList.value == '' ? '' : 'none');			
			
		GeneratePreviewStyle();	
	}
	
	function GeneratePreviewStyle()
	{
		var cssDropDownList = document.getElementById('<%= CssDropDownList.ClientID %>');
		var previewIframe = window.frames[0].document;
		var previewIframeHead = previewIframe.getElementsByTagName("head")[0];
		var customStyleSheet = previewIframe.getElementById("CustomStyleSheet");
		var cssUrlTextBox = document.getElementById('<%= CssUrlHiddenTextBox.ClientID %>');
		var logoUrlTextBox = document.getElementById('<%= LogoUrlHiddenTextBox.ClientID %>');
		var backgroundUrlTextBox = document.getElementById('<%= BackgroundUrlHiddenTextBox.ClientID %>');
		var logoAlignDropDownList = document.getElementById('<%= LogoAlignDropDownList.ClientID %>');
		var noRepeatBackgroundCheckBox = document.getElementById('<%= NoRepeatBackgroundCheckBox.ClientID %>');
		
		var fontFamilyDropDownList = document.getElementById('<%= FontFamilyDropDownList.ClientID %>');
		
		var bodyFontColourTextBox = document.getElementById('<%= BodyTextColourInput.ClientID %>');
		var bodyBackgroundColourTextBox = document.getElementById('<%= BodyBackgroundColourInput.ClientID %>');
		var bodyFontColourSampleTextBox = document.getElementById('BodyTextColourSample');
		var bodyBackgroundColourSampleTextBox = document.getElementById('BodyBackgroundColourSample');
		var bodyFontSizeDropDownList = document.getElementById('<%= BodyFontSizeDropDownList.ClientID %>');
		var bodyFontWeightDropDownList = document.getElementById('<%= BodyFontWeightDropDownList.ClientID %>');
		var bodyFontTextDecorationDropDownList = document.getElementById('<%= BodyTextDecorationDropDownList.ClientID %>');
		var bodyFontTextAlignDropDownList = document.getElementById('<%= BodyTextAlignDropDownList.ClientID %>');
		
		var headerFontColourTextBox = document.getElementById('<%= HeaderTextColourInput.ClientID %>');
		var headerBackgroundColourTextBox = document.getElementById('<%= HeaderBackgroundColourInput.ClientID %>');
		var headerFontColourSampleTextBox = document.getElementById('HeaderTextColourSample');
		var headerBackgroundColourSampleTextBox = document.getElementById('HeaderBackgroundColourSample');
		var headerFontSizeDropDownList = document.getElementById('<%= HeaderFontSizeDropDownList.ClientID %>');
		var headerFontWeightDropDownList = document.getElementById('<%= HeaderFontWeightDropDownList.ClientID %>');
		var headerFontTextDecorationDropDownList = document.getElementById('<%= HeaderTextDecorationDropDownList.ClientID %>');
		var headerFontTextAlignDropDownList = document.getElementById('<%= HeaderTextAlignDropDownList.ClientID %>');
		
		var linksFontColourTextBox = document.getElementById('<%= LinksTextColourInput.ClientID %>');
		var linksBackgroundColourTextBox = document.getElementById('<%= LinksBackgroundColourInput.ClientID %>');
		var linksFontColourSampleTextBox = document.getElementById('LinksTextColourSample');
		var linksBackgroundColourSampleTextBox = document.getElementById('LinksBackgroundColourSample');
		var linksFontSizeDropDownList = document.getElementById('<%= LinksFontSizeDropDownList.ClientID %>');
		var linksFontWeightDropDownList = document.getElementById('<%= LinksFontWeightDropDownList.ClientID %>');
		var linksFontTextDecorationDropDownList = document.getElementById('<%= LinksTextDecorationDropDownList.ClientID %>');
		var linksFontTextAlignDropDownList = document.getElementById('<%= LinksTextAlignDropDownList.ClientID %>');
		
		var linksHoverFontColourTextBox = document.getElementById('<%= LinksHoverTextColourInput.ClientID %>');
		var linksHoverBackgroundColourTextBox = document.getElementById('<%= LinksHoverBackgroundColourInput.ClientID %>');
		var linksHoverFontColourSampleTextBox = document.getElementById('LinksHoverTextColourSample');
		var linksHoverBackgroundColourSampleTextBox = document.getElementById('LinksHoverBackgroundColourSample');
		var linksHoverFontSizeDropDownList = document.getElementById('<%= LinksHoverFontSizeDropDownList.ClientID %>');
		var linksHoverFontWeightDropDownList = document.getElementById('<%= LinksHoverFontWeightDropDownList.ClientID %>');
		var linksHoverFontTextDecorationDropDownList = document.getElementById('<%= LinksHoverTextDecorationDropDownList.ClientID %>');
		var linksHoverFontTextAlignDropDownList = document.getElementById('<%= LinksHoverTextAlignDropDownList.ClientID %>');
	
		SetSampleBackgroundColour(bodyFontColourTextBox, bodyFontColourSampleTextBox);
		SetSampleBackgroundColour(bodyBackgroundColourTextBox, bodyBackgroundColourSampleTextBox);
		SetSampleBackgroundColour(headerFontColourTextBox, headerFontColourSampleTextBox);
		SetSampleBackgroundColour(headerBackgroundColourTextBox, headerBackgroundColourSampleTextBox);
		SetSampleBackgroundColour(linksFontColourTextBox, linksFontColourSampleTextBox);
		SetSampleBackgroundColour(linksBackgroundColourTextBox, linksBackgroundColourSampleTextBox);
		SetSampleBackgroundColour(linksHoverFontColourTextBox, linksHoverFontColourSampleTextBox);
		SetSampleBackgroundColour(linksHoverBackgroundColourTextBox, linksHoverBackgroundColourSampleTextBox);
		
		var newStyle = "";
	
		if(customStyleSheet != null)
		{
			customStyleSheet.href = cssDropDownList.value;
			if(cssDropDownList.value == '' && cssUrlTextBox.value != '')
			{
				customStyleSheet.href = cssUrlTextBox.value;
			}
		}
		
		
		if(logoUrlTextBox.value != '')
		{
			newStyle += logoUrlTextBox.value;
			if(logoAlignDropDownList.value != '')
				newStyle += 'div.MainImage{background-position: ' + logoAlignDropDownList.value + ';} ';
		}
		else if(cssDropDownList.value != '')
		{
			newStyle += "div.MainImage{background-image: url('/gfx/default-styled-main.gif');width:800px;height:100px;}";
		}
		if(backgroundUrlTextBox.value != '')
		{
			newStyle += "body, #form1, div.ParentDiv{background: url('" + backgroundUrlTextBox.value + "') " + (noRepeatBackgroundCheckBox.checked ? "no-repeat" : "repeat") + ";}";
		}
		
		var bodyStyle = GenerateElementStyle(bodyFontColourTextBox, bodyBackgroundColourTextBox, bodyFontSizeDropDownList, bodyFontWeightDropDownList, bodyFontTextDecorationDropDownList, bodyFontTextAlignDropDownList);
		if(fontFamilyDropDownList.value != '')
			bodyStyle += 'font-family: ' + fontFamilyDropDownList.value + '; ';
		
		if(bodyStyle != '')
			newStyle += "body, #form1, div.BodyDiv, body div, body div.OuterDiv, body div.WelcomeDiv, body div.WelcomeDiv a:link, body div.WelcomeDiv a:visited, body div.WelcomeDiv a:hover {" + bodyStyle + "} ";
			
		if(bodyFontTextAlignDropDownList.value == 'left')
			newStyle += 'div.OuterDiv{left:0%;margin-left:0px;}';
		else if(bodyFontTextAlignDropDownList.value == 'center')
			newStyle += 'div.OuterDiv{left:50%;margin-left:-400px;}';
		else if(bodyFontTextAlignDropDownList.value == 'right')
			newStyle += 'div.OuterDiv{left:100%;margin-left:-800px;}';
			
		var headerStyle = GenerateElementStyle(headerFontColourTextBox, headerBackgroundColourTextBox, headerFontSizeDropDownList, headerFontWeightDropDownList, headerFontTextDecorationDropDownList, headerFontTextAlignDropDownList);
		if(headerStyle != '')
			newStyle += "div h2{" + headerStyle + "} ";
		var linksStyle = GenerateElementStyle(linksFontColourTextBox, linksBackgroundColourTextBox, linksFontSizeDropDownList, linksFontWeightDropDownList, linksFontTextDecorationDropDownList, linksFontTextAlignDropDownList);
		if(linksStyle != '')
		{
			newStyle += "div.Link, div a.Link:link, div a.Link:visited{" + linksStyle + "} ";
		}
		var linksHoverStyle = GenerateElementStyle(linksHoverFontColourTextBox, linksHoverBackgroundColourTextBox, linksHoverFontSizeDropDownList, linksHoverFontWeightDropDownList, linksHoverFontTextDecorationDropDownList, linksHoverFontTextAlignDropDownList);
		if(linksHoverStyle != '')
			newStyle += "div a.Link:hover{" + linksHoverStyle + "} ";
		

		if(previewIframeHead != null)
		{
			for(i=previewIframeHead.childNodes.length-1;i>=0;i--) 
			{
				if(previewIframeHead.childNodes[i].tagName == 'STYLE' || (previewIframeHead.childNodes[i].tagName == 'LINK' && previewIframeHead.childNodes[i].id == 'StyleTag'))
				{
					previewIframeHead.removeChild(previewIframeHead.childNodes[i]);
				}				
			}

			newcss = previewIframe.createElement("style");
			newcss.type="text/css";
			newcss.media="all"	
		
			if(newcss.styleSheet)
			{// IE
				newcss.styleSheet.cssText = newStyle;				
			} 
			else 
			{// w3c
				var cssText = document.createTextNode(newStyle);		
				newcss.appendChild(cssText);				
			}
			previewIframeHead.appendChild(newcss);
		}	

		
		// This fixes browser bug with not showing background
		document.getElementById("PreviewIframe").style.display = 'inline';
		document.getElementById("PreviewIframe").style.display = 'block';			
	}
	
	function SetSampleBackgroundColour(fontColourTextBox, fontColourSampleTextBox)
	{
		if(fontColourTextBox.value != '')
		{
			if(fontColourTextBox.value == 'transparent')
				fontColourSampleTextBox.style.backgroundColor = '#FFFFFF';
			else
				fontColourSampleTextBox.style.backgroundColor = fontColourTextBox.value;			
		}
	}
	
	function GenerateElementStyle(fontColourTextBox, backgroundColourTextBox, fontSizeDropDownList, fontWeightDropDownList, fontTextDecorationDropDownList, fontTextAlignDropDownList)
	{
		var output = "";
		if(fontColourTextBox.value != '')
			output += 'color: ' + fontColourTextBox.value + '; ';
		if(backgroundColourTextBox.value != '')
			output += 'background-color: ' + backgroundColourTextBox.value + '; ';
		if(fontSizeDropDownList.value != '')
			output += 'font-size: ' + fontSizeDropDownList.value + '; ';
		if(fontWeightDropDownList.value != '')
		{
			if(fontWeightDropDownList.value.indexOf('normal') >= 0)
				output += 'font-style: normal; font-weight: normal;';
			else if(fontWeightDropDownList.value.indexOf('bold italic') >= 0)
				output += 'font-style: italic; font-weight: bold;';
			else if(fontWeightDropDownList.value.indexOf('italic') >= 0)
				output += 'font-style: italic; font-weight: normal;';
			else if(fontWeightDropDownList.value.indexOf('bold') >= 0)
				output += 'font-style: normal; font-weight: bold;';
		}
		if(fontTextDecorationDropDownList.value != '')
			output += 'text-decoration: ' + fontTextDecorationDropDownList.value + '; ';
		if(fontTextAlignDropDownList.value != '')
			output += 'text-align: ' + fontTextAlignDropDownList.value + '; ';
				
		return output;
	}
	
	function ClearLogoImage()
	{
		document.getElementById('<%= LogoUrlHiddenTextBox.ClientID %>').value = '';
		GeneratePreviewStyle();
	}
	
	function ClearBackgroundImage()
	{
		document.getElementById('<%= BackgroundUrlHiddenTextBox.ClientID %>').value = '';
		GeneratePreviewStyle();
	}
</script>
<div>
	<dsi:h1 id="H1Header" runat="server"></dsi:h1>
	<div class="ContentBorder">
		<p>Please follow the following 5 steps to customise styling for your pages.
		</p>
	</div>
	<asp:Panel ID="CustomiseOptionsPanel" runat="server">
		<dsi:h1 runat="server">Step 1 - Choose your colours</dsi:h1>
		<div class="ContentBorder">
			<p>
				<span style="height:20px; vertical-align:bottom;"><select id="CssDropDownList" runat="server" onchange="SelectCss();"></select></span>
				<span id="UploadCssDiv" style="display:none;vertical-align:top;"><input type="file" runat="server" id="InputCssFile" size="40" style="height:19px; margin-left:2px;"/> 
				<button ID="UploadCssButton" runat="server" style="height:19px;" onserverclick="UploadCssButton_Click">Upload</button>
				<asp:TextBox ID="CssUrlHiddenTextBox" runat="server" style="display:none;"></asp:TextBox></span>
			</p>
		</div>			
		
		<dsi:h1 runat="server">Step 2 - Upload images</dsi:h1>
		<div class="ContentBorder">
			<table cellpadding=0 cellspacing=3 border=0>
				<tr>
					<td>Logo image</td>
					<td><input type="file" runat="server" id="InputLogoFile" size="33" style="height:19px;"/> 
						<button ID="UploadLogoButton" runat="server" onserverclick="UploadLogoButton_Click">Upload</button>
						<asp:TextBox ID="LogoUrlHiddenTextBox" runat="server" style="display:none; width:230px;"></asp:TextBox>
					</td>
					<td style="padding-left:8px;">Align&nbsp;<select id="LogoAlignDropDownList" runat="server" onchange="GeneratePreviewStyle();"></select>
					</td>
					<td style="padding-left:8px;"><a href="#" onclick="ClearLogoImage()"><%=Utilities.IconHtml(Utilities.Icon.Delete) %></a></td>
				</tr>
				<tr>
					<td >Background image</td>
					<td>
						<input type="file" runat="server" id="InputBackgroundFile" size="33" style="height:19px;"/> 
						<button ID="UploadBackgroundButton" runat="server" onserverclick="UploadBackgroundButton_Click">Upload</button>
						<asp:TextBox ID="BackgroundUrlHiddenTextBox" runat="server" style="display:none;"></asp:TextBox>
					</td>
					<td style="padding-left:8px;">
						<input id="NoRepeatBackgroundCheckBox" runat="server" type="checkbox" onkeyup="GeneratePreviewStyle();" onclick="GeneratePreviewStyle();" >no repeat</input>
					</td>
					<td style="padding-left:8px;"><a href="#" onclick="ClearBackgroundImage()"><%=Utilities.IconHtml(Utilities.Icon.Delete) %></a></td>
				</tr>
			</table>
		</div>	
		<dsi:h1 runat="server">Step 3 - Customise</dsi:h1>
		<div class="ContentBorder">
		<div id="colorpicker201" class="colorpicker201"></div>
		<table cellpadding=0 cellspacing=3 border=0 id="CustomiseTable" runat="server">
			<tr style="padding-bottom: 8px;">
				<td>Font</td><td colspan=4><select title='font-family' name='ffamily' id="FontFamilyDropDownList" runat="server" onchange="GeneratePreviewStyle();"></select></td>
			</tr>
			<tr class="dataGridHeader">
				<th>Tags</th><th><nobr>Text color</nobr></th><th>Background</th><th><nobr>Font size</nobr></th><th><nobr>Font style</nobr></th><th><nobr>Underline</nobr></th><th><nobr>Align</nobr></th>
			</tr>
			<tr>
				<td>Body</td>
				<td><input type="text" size="11" ID="BodyTextColourInput" readonly="true" style="cursor:pointer;" onfocus="showColorGrid2(this.id,'BodyTextColourSample');" runat="server" value="" ><input type="text" ID="BodyTextColourSample" readonly="true" style="width:18px;" value="""></td>
				<td><input type="text" size="11" ID="BodyBackgroundColourInput" readonly="true" style="cursor:pointer;" onfocus="showColorGrid2(this.id,'BodyBackgroundColourSample');" runat="server" value=""><input type="text" ID="BodyBackgroundColourSample" readonly="true" style="width:18px;" value=""></td>
				<td><select id="BodyFontSizeDropDownList" runat="server" onchange="GeneratePreviewStyle();" /></td>
				<td><select id="BodyFontWeightDropDownList" runat="server" onchange="GeneratePreviewStyle();"/></td>
				<td><select id="BodyTextDecorationDropDownList" runat="server" onchange="GeneratePreviewStyle();"/></td>
				<td><select id="BodyTextAlignDropDownList" runat="server" onchange="GeneratePreviewStyle();"/></td>
			</tr>
			<tr>
				<td>Headers</td>
				<td><input type="text" size="11" ID="HeaderTextColourInput" readonly="true" style="cursor:pointer;" onfocus="showColorGrid2(this.id,'HeaderTextColourSample');" runat="server" value="" onchange="GeneratePreviewStyle();"><input type="text" ID="HeaderTextColourSample" readonly="true" style="width:18px;" value=""></td>
				<td><input type="text" size="11" ID="HeaderBackgroundColourInput" readonly="true" style="cursor:pointer;" onfocus="showColorGrid2(this.id,'HeaderBackgroundColourSample');" runat="server" value="" onchange="GeneratePreviewStyle();"><input type="text" ID="HeaderBackgroundColourSample" readonly="true" style="width:18px;" value=""></td>
				<td><select id="HeaderFontSizeDropDownList" runat="server" onchange="GeneratePreviewStyle();"/></td>
				<td><select id="HeaderFontWeightDropDownList" runat="server" onchange="GeneratePreviewStyle();"/></td>
				<td><select id="HeaderTextDecorationDropDownList" runat="server" onchange="GeneratePreviewStyle();"/></td>
				<td><select id="HeaderTextAlignDropDownList" runat="server" onchange="GeneratePreviewStyle();"/></td>
			</tr>
			<tr>
				<td>Links</td>
				<td><input type="text" size="11" ID="LinksTextColourInput" readonly="true" style="cursor:pointer;" onfocus="showColorGrid2(this.id,'LinksTextColourSample');" runat="server" value="" onchange="GeneratePreviewStyle();"><input type="text" ID="LinksTextColourSample" readonly="true" style="width:18px;" value=""></td>
				<td><input type="text" size="11" ID="LinksBackgroundColourInput" readonly="true" style="cursor:pointer;" onfocus="showColorGrid2(this.id,'LinksBackgroundColourSample');" runat="server" value="" onchange="GeneratePreviewStyle();"><input type="text" ID="LinksBackgroundColourSample" readonly="true" style="width:18px;" value=""></td>
				<td><select id="LinksFontSizeDropDownList" runat="server" onchange="GeneratePreviewStyle();"/></td>
				<td><select id="LinksFontWeightDropDownList" runat="server" onchange="GeneratePreviewStyle();"/></td>
				<td><select id="LinksTextDecorationDropDownList" runat="server" onchange="GeneratePreviewStyle();"/></td>
				<td><select id="LinksTextAlignDropDownList" runat="server" onchange="GeneratePreviewStyle();"/></td>
			</tr>
			<tr>
				<td><nobr>Links hover</nobr></td>
				<td><input type="text" size="11" ID="LinksHoverTextColourInput" readonly="true" style="cursor:pointer;" onfocus="showColorGrid2(this.id,'LinksHoverTextColourSample');" runat="server" value="" onchange="GeneratePreviewStyle();"><input type="text" ID="LinksHoverTextColourSample" readonly="true" style="width:18px;" value=""></td>
				<td><input type="text" size="11" ID="LinksHoverBackgroundColourInput" readonly="true" style="cursor:pointer;" onfocus="showColorGrid2(this.id,'LinksHoverBackgroundColourSample');" runat="server" value="" onchange="GeneratePreviewStyle();"><input type="text" ID="LinksHoverBackgroundColourSample" readonly="true" style="width:18px;" value=""></td>
				<td><select id="LinksHoverFontSizeDropDownList" runat="server" style="display:none;" onchange="GeneratePreviewStyle();"/></td>
				<td><select id="LinksHoverFontWeightDropDownList" runat="server" style="display:none;" onchange="GeneratePreviewStyle();"/></td>
				<td><select id="LinksHoverTextDecorationDropDownList" runat="server" onchange="GeneratePreviewStyle();"/></td>
				<td><select id="LinksHoverTextAlignDropDownList" runat="server" style="display:none;" onchange="GeneratePreviewStyle();"/></td>
			</tr>
		</table>
	</div>	
	</asp:Panel>
	<div style="margin-bottom:13px;">
		<dsi:h1 runat="server">Step 4 - Preview</dsi:h1>
		<center><iframe id="PreviewIframe" src="<%= StyledObject.UrlStyled() %>" frameborder=1 scrolling="no" style="zoom:0.68; width:883px; height:680px;" onload="SelectCss();"></iframe></center>
	</div>
	<dsi:h1 runat="server">Step 5 - Save</dsi:h1>
	<div class="ContentBorder">
		<p>
			<button id="SaveCustomStyleButton" runat="server" onserverclick="SaveCustomStyleButton_Click">Save</button><a href="" id="LinkToStyledPages" target="_blank" runat="server" style="margin-left:20px;" visible="false">Click here to go to your styled pages.</a>
		</p>
	</div>
</div>
<div style="width:0px; height:420px; z-index:9000; left:250px; top:0px; background-color:#CCCCCC; filter:alpha(opacity=10); position:absolute;"></div>
