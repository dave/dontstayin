<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Html.ascx.cs" Inherits="Spotted.Controls.Html" %>

<div class="ClearAfter">
	<div style="padding-top:3px; padding-bottom:3px; text-align:right;" runat="server" id="HelpersDiv">
		<div class="HelperAnchorContainer">
			Helpers: 
			<a href="#" TabIndex="-1" onmousedown="try{HtmlControlReplaceText('<%=  this.ClientID  %>', 'MakeBold', false);}catch(ex){}return false;" onclick="return false;" class="HelperAnchor" accesskey="b">Bold</a>
			<a href="#" TabIndex="-1" onmousedown="try{HtmlControlReplaceText('<%=  this.ClientID  %>', 'MakeItalic', false);}catch(ex){}return false;" onclick="return false;" class="HelperAnchor" accesskey="i">Italic</a>
			<a href="#" TabIndex="-1" onmousedown="try{HtmlControlReplaceText('<%=  this.ClientID  %>', 'MakeSmall', false);}catch(ex){}return false;" onclick="return false;" class="HelperAnchor" accesskey="s">Small</a>
			<a runat="server" id="LinkAnchor" href="#" TabIndex="-1" onclick="return false;" class="HelperAnchor" accesskey="l">Link</a>
			<a runat="server" id="ImageAnchor" href="#" TabIndex="-1" onclick="return false;" class="HelperAnchor" accesskey="p">Picture</a>
			<a runat="server" id="VideoAnchor" href="#" TabIndex="-1" onclick="return false;" class="HelperAnchor" accesskey="v">Video</a>
			<!--<a runat="server" id="MixmagAnchor" href="#" TabIndex="-1" onclick="return false;" class="HelperAnchor" accesskey="m"><img src="/gfx/logo-mixmag-tiny.png" width="43" height="10" style="vertical-align:bottom; margin-bottom:1px; margin-left:1px; margin-right:2px;" />playlist</a>-->
			<a runat="server" id="FlashAnchor" href="#" TabIndex="-1" onclick="return false;" class="HelperAnchor" accesskey="f">Flash</a>
			<a runat="server" id="AdvancedAnchor" href="#" TabIndex="-1" onclick="return false;" class="HelperAnchor" accesskey="a">Advanced</a>
		</div>
		<div runat="server" id="MixmagDiv" class="HelperPanel" style="display:none;">
			<h3>Mixmag playlist</h3>
			<p>
				You can insert an awesome Mixmag playlist in your message. <a href="http://download.mixmag.net/needleshare/?playerwidth=<% = MixmagWidth %>" target="_blank" onclick="openPopupFocusSize('http://download.mixmag.net/needleshare/?playerwidth=<% = MixmagWidth %>',900,500);return false;">Click here and follow the instructions</a>, then copy and paste the code into your message.
			</p>
		</div>
		<div runat="server" id="LinkDiv" class="HelperPanel" style="display:none;">
			<asp:Panel runat="server" ID="LinkMainPanel">
				<h3>Insert a link</h3>
				<p>
					It's very simple to insert a link.
				</p>
				<p>
					<b>Just copy and paste the URL into DontStayIn!</b>
				</p>
				<p>
					<small>If you would like to customise the text that appears in the page for your link, <a runat="server" ID="LinkUrlButton" href="#">click here</a>.</small>
				</p>
			</asp:Panel>
			<asp:Panel runat="server" ID="LinkUrlPanel" style="display:none">
				<h3>Insert a URL link</h3>
				<p>
					<a runat="server" ID="LinkUrlPanelBackButton" href="#">&lt;- Back</a>
				</p>
				<p>
					1) Enter the URL: <asp:TextBox runat="server" Columns="50" ID="LinkUrlTextBox"></asp:TextBox>
				</p>
				<p>
					2) Select some text in the box below
				</p>
				<p>
					3) Click here to turn the selected text into a link: <button onmousedown="HtmlControlReplaceText('<%=  this.ClientID  %>', 'LinkUrl', false);return false;" onclick="return false;">Insert link</button>
				</p>
			</asp:Panel>
		</div>
		<div runat="server" id="ImageDiv" class="HelperPanel" style="display:none;">
			<asp:Panel runat="server" ID="ImageMainPanel">
				<h3>Insert an image, picture or photo</h3>
				<p>
					It's very simple to insert an image.
				</p>
				<p>
					<b>Just copy and paste the URL of the image into DontStayIn!</b>
				</p>
				<p>
					<small>If it's a DontStayIn photo, get the URL from the boxes BELOW the photo (not your browsers URL).</small>
				</p>
			</asp:Panel>
		</div>
		<div runat="server" id="VideoDiv" class="HelperPanel" style="display:none;">
			<asp:Panel runat="server" ID="VideoMainPanel">
				<h3>Insert a video</h3>
				<p>
					It's very simple to insert a video.
				</p>
				<p>
					For web videos (from sites like YouTube) just copy and paste the "embed" code into DontStayIn. It should start &lt;OBJECT or &lt;EMBED.
				</p>
				<p>
					For videos on DontStayIn just copy and paste the URL from the boxes below the video.
				</p>
				<p>
					If you have the URL of the FLV file, <a runat="server" ID="VideoFlvButton" href="#">click here</a>.
				</p>
			</asp:Panel>
			<asp:Panel runat="server" ID="VideoFlvPanel" style="display:none">
				<h3>Insert an FLV video</h3>
				<p>
					<a runat="server" ID="VideoFlvPanelBackButton" href="#">&lt;- Back</a>
				</p>
				<p>
					1) Enter the URL of the FLV video: <asp:TextBox runat="server" Columns="50" ID="VideoFlvUrlTextBox"></asp:TextBox>
				</p>
				<p>
					2) Enter the dimensions of the video - 
					width: <asp:TextBox runat="server" Columns="3" ID="VideoFlvWidthTextBox"></asp:TextBox>, 
					height: <asp:TextBox runat="server" Columns="3" ID="VideoFlvHeightTextBox"></asp:TextBox> pixels (optional)
				</p>
				<p>
					3) Position the cursor in the text-box below where you would like your video to appear
				</p>
				<p>
					4) Click here to insert your video: <button onmousedown="HtmlControlReplaceText('<%=  this.ClientID  %>', 'VideoFlv', true);return false;" onclick="return false;">Insert video</button>
				</p>
			</asp:Panel>
		</div>
		<div runat="server" id="FlashDiv" class="HelperPanel" style="display:none;">
			<asp:Panel runat="server" ID="FlashMainPanel">
				<h3>Insert a flash movie</h3>
				<p>
					It's very simple to insert a flash movie.
				</p>
				<p>
					Just copy and paste the "embed" code into DontStayIn. It should start &lt;OBJECT or &lt;EMBED.
				</p>
				<p>
					If you don't have the embed code, but you do have the URL of the SWF file, <a runat="server" ID="FlashSwfUrlButton" href="#">click here</a>.
				</p>
			</asp:Panel>
			<asp:Panel runat="server" ID="FlashSwfUrlPanel" style="display:none">
				<h3>Insert a flash movie</h3>
				<p>
					<a runat="server" ID="FlashSwfUrlPanelBackButton" href="#">&lt;- Back</a>
				</p>
				<p>
					1) Enter the URL of the SWF file: <asp:TextBox runat="server" Columns="50" ID="FlashSwfUrlUrlTextBox"></asp:TextBox>
				</p>
				<p>
					2) Enter the dimensions of the flash movie - 
					width: <asp:TextBox runat="server" Columns="3" ID="FlashSwfUrlWidthTextBox"></asp:TextBox>, 
					height: <asp:TextBox runat="server" Columns="3" ID="FlashSwfUrlHeightTextBox"></asp:TextBox> pixels
				</p>
				<p>
					3) Choose a draw method:
					<asp:DropDownList runat="server" ID="FlashSwfUrlDrawDropDownList">
						<asp:ListItem Value="auto" Text="auto (leave like this if you're unsure)" Enabled="true" />
						<asp:ListItem Value="click" Text="click (draw on click)" />
						<asp:ListItem Value="load" Text="load (draw on page load)" />
					</asp:DropDownList>
				</p>
				<p>
					4) Position the cursor in the text-box below where you would like your flash movie to appear
				</p>
				<p>
					5) Click here to insert your flash movie: <button onmousedown="HtmlControlReplaceText('<%=  this.ClientID  %>', 'FlashSwfUrl', true);return false;" onclick="return false;">Insert flash movie</button>
				</p>
			</asp:Panel>
		</div>
		<div runat="server" id="AdvancedDiv" class="HelperPanel" style="display:none;">
			<asp:Panel runat="server" ID="AdvancedFormattingPanel">
				<h3>Formatting</h3>
				<p>
					<asp:RadioButton ID="AdvancedFormattingTrueRadio" runat="server" GroupName="AdvancedFormattingGroup" Text="Normal" Checked="true" /> - <small>text and simple html - we convert line-breaks to html br tags</small>
				</p>
				<p>
					<asp:RadioButton ID="AdvancedFormattingFalseRadio" runat="server" GroupName="AdvancedFormattingGroup" Text="Advanced" /> - <small>html only - your html will be rendered without changes</small>
				</p>
			</asp:Panel>
			<asp:Panel runat="server" ID="AdvancedContainerPanel">
				<h3>Container</h3>
				<p>
					<asp:RadioButton ID="AdvancedContainerTrueRadio" runat="server" GroupName="AdvancedContainerGroup" Text="Enabled" Checked="true" /> - <small>this html will show in a white container div with padding</small>
				</p>
				<p>
					<asp:RadioButton ID="AdvancedContainerFalseRadio" runat="server" GroupName="AdvancedContainerGroup" Text="Disabled" /> - <small>this html will show against the site background with no borders or padding</small>
				</p>
			</asp:Panel>
			<asp:Panel runat="server" ID="AdvancedParseNowPanel">
				<h3>Parsing</h3>
				<p>
					Click the button to parse your HTML into dsi tags: <button id="AdvancedParseNowButton" runat="server">Parse now</button>
				</p>
				<p>
					To stop a URL changing into a dsi tag, put a ~ before the link e.g. ~http://www.dontstayin.com/
				</p>
			</asp:Panel>
			<h3>Tags</h3>
			<p>
				DontStayIn supports special tags that help you embed rich content. <a runat="server" ID="AdvancedTagsToggleButton" href="#">Click here for a full description</a>.
			</p>
			<asp:Panel runat="server" ID="AdvancedTagsPanel" style="display:none">
				<p>
					I'm still tweaking some of the advanced features... <a href="/groups/dontstayin-nerds/chat/k-2463009">Post here if you find bugs</a>.
				</p>
				<pre style="font-size:12px; padding:8px;">&lt;dsi:video 
		type = [dsi | flv | youtube | google | 
				metacafe | myspace | break | 
				collegehumor | redtube | 
				ebaumsworld | dailymotion | 
				jibjab | vimeo] 
		ref = [dsi-photo-k | site-ref]     // for type != flv
		src = [flv-url]                    // for type = flv<font class="ForegroundShade">
		width = [width]
		height = [height]
		draw = [auto* | click | load]
		nsfw = [true]</font>
	/> 

	&lt;dsi:audio 
		type = [mp3] 
		src = [mp3-url]<font class="ForegroundShade">
		width = [width]
		height = [height]</font>
	/>

	&lt;dsi:flash 
		src = [swf-url] 
		width = [width] 
		height = [height]<font class="ForegroundShade">
		draw = [auto* | click | load]
		nsfw = [false* | true]
		play = [true* | false]
		loop = [false* | true]
		menu = [false* | true]
		quality = [low | autolow | autohigh | medium | high | best]
		scale = [default | noorder | exactfit]
		align = [l | t | r | b]
		salign = [l | t | r | b | tl | tr | bl | br]
		wmode = [transparent* | window | opaque]
		bgcolor = [colour]
		base = [base-url]
		flashvars = [flashvars]</font>
	/>  

	&lt;dsi:object
		type = [usr | event | venue | place | 
				group | brand | article | photo | misc]
		ref = [object-k]<font class="ForegroundShade">
		style = [
			content: {text* | icon |       // for type = usr, event, venue,
					  text-under-icon};                  place, group, brand
			details: {none* | venue |      // for type = event, venue, place
					  place | country};
			date: {false* | true};         // for type = event
			snip: {number};                // for type = event
			rollover: {true* | false}      // for type = usr, photo
			photo: {icon* | thumb | web}   // for type = photo
			link: {true* | false}
		]</font>
	/>

	&lt;dsi:link
		type = [usr | event | venue | place | group | brand | photo | misc]
		ref = [object-k]
	>
		[your content]
	&lt;/dsi:link>

	&lt;dsi:quote
		ref = [usr-k]
	>
		[your content]
	&lt;/dsi:quote></pre>
			</asp:Panel>
		</div>
	</div>

	<div style="padding:0px; border-width:1px; margin-top:-1px; text-align:right; overflow:hidden;" class="BorderKeyline" runat="server" id="TextBoxDiv">
		<asp:TextBox Runat="server" ID="HtmlTextBox" AccessKey="n" TextMode="MultiLine" Rows="10" style="border-width:0px; margin:0px;" />
	</div>

	<div style="border-width:1px; margin-top:-1px; padding:2px; height:100px;" class="BorderKeyline ForegroundShade" runat="server" id="DisabledMessageDiv" visible="false"/>
	
	<div runat="server" id="ButtonsContainer" class="ClearAfter">
		<div style="float:left; width:200px;" runat="server" id="SaveDiv">
			<asp:Button runat="server" Text="Save" ID="SaveButton" OnClick="Save_Click" OnClientClick="try { return WhenLoggedInButton(this); } catch(ex) { return false; }" />
		</div>

		<div style="float:right; width:300px; text-align:right;">
			<button runat="server" ID="PreviewButton">Preview</button> 
			<button runat="server" ID="HidePreviewButton" style="display:none">Hide preview</button>
		</div>
	</div>
	
	<asp:Panel style="clear:both; overflow:scroll; border-width:1px; display:none;" runat="server" ID="PreviewPanelContainer" class="BorderKeyline ClearAfter">
		<div style="width:670px; padding:20px; padding-top:0px;" class="BodyBackgroundColour">
			<div style="width:670px; overflow:hidden;">
				<asp:Panel runat="server" ID="PreviewPanel"></asp:Panel>
			</div>
		</div>
	</asp:Panel>

	<input runat="server" type="hidden" id="uiEnabled" />
	<input runat="server" type="hidden" id="uiPreviewType" />
	<input runat="server" type="hidden" id="HelperPanelDisplayState" />
</div>
